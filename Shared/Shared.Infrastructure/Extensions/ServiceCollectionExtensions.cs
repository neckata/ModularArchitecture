using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using ModularArchitecture.Shared.Core.EventLogging;
using ModularArchitecture.Shared.Core.Exceptions;
using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Interfaces.Services;
using ModularArchitecture.Shared.Core.Settings;
using ModularArchitecture.Shared.Infrastructure.EventLogging;
using ModularArchitecture.Shared.Infrastructure.Interceptors;
using ModularArchitecture.Shared.Infrastructure.Middlewares;
using ModularArchitecture.Shared.Infrastructure.Persistence;
using ModularArchitecture.Shared.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using ModularArchitecture.Shared.Core.Interfaces.Services.Identity;
using ModularArchitecture.Shared.Core.Extensions;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using ModularArchitecture.Shared.Core.Services.Identity;
using ModularArchitecture.Shared.Core.Entities;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ModularArchitecture.Shared.Infrastructure.Permissions;
using ModularArchitecture.Shared.Core.Interfaces.Services.Event;
using ModularArchitecture.Shared.Core.IntegrationServices.Event;
using ModularArchitecture.Shared.Infrastructure.Utilities;

[assembly: InternalsVisibleTo("ModularArchitecture")]

namespace ModularArchitecture.Shared.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddPersistenceSettings(config);
            services.AddIdentityInfrastructure(config);
            services.AddScoped<IEventLogger, EventLogger>();
            services
                .AddDatabaseContext<ApplicationDbContext>()
                .AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddControllers()
                .AddMvcOptions(options =>
                {
                    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, propertyName) =>
                        throw new CustomException($"{propertyName}: value '{value}' is invalid.", statusCode: HttpStatusCode.BadRequest));
                });
            services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();
            services.AddApplicationLayer(config);
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSingleton<GlobalExceptionHandler>();
            services.AddSwaggerDocumentation();
            services.AddCorsPolicy();
            services.AddApplicationSettings(config);
            services.MapModules();

            return services;
        }

        private static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration config)
        {
            //Here add all services
            services.AddTransient<IEventLogService, EventLogService>();
            services.AddTransient<IEventService, EventService>();
            return services;
        }

        private static IServiceCollection AddPersistenceSettings(this IServiceCollection services, IConfiguration config)
        {
            return services
                .Configure<PersistenceSettings>(config.GetSection(nameof(PersistenceSettings)));
        }

        private static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)));
        }

        private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            var corsSettings = services.GetOptions<CorsSettings>(nameof(CorsSettings));
            return services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(corsSettings.Url);
                });
            });
        }

        private static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpContextAccessor()
                .AddScoped<ICurrentUser, CurrentUser>()
                .Configure<JwtSettings>(configuration.GetSection("JwtSettings"))
                .AddTransient<ITokenService, TokenService>()
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IDatabaseSeeder, IdentityDbSeeder>();
            services.AddTransient<IDatabaseSeeder, ConnectorDbSeeder>();
            services.AddPermissions(configuration);
            services.AddJwtAuthentication(configuration);
            return services;
        }

        private static IServiceCollection AddPermissions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            return services;
        }

        private static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, IConfiguration config)
        {
            JwtSettings jwtSettings = services.GetOptions<JwtSettings>(nameof(JwtSettings));
            byte[] key = Encoding.ASCII.GetBytes(jwtSettings.Key);
            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RoleClaimType = ClaimTypes.Role,
                        ClockSkew = TimeSpan.Zero
                    };
                    bearer.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            if (!context.Response.HasStarted)
                            {
                                throw new IdentityException("You are not Authorized.", statusCode: HttpStatusCode.Unauthorized);
                            }

                            return Task.CompletedTask;
                        },
                        OnForbidden = context =>
                        {
                            throw new IdentityException("You are not authorized to access this resource.", statusCode: HttpStatusCode.Forbidden);
                        },
                    };
                });
            return services;
        }

        private static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.IsDynamic)
                    {
                        string xmlFile = $"{assembly.GetName().Name}.xml";
                        string xmlPath = Path.Combine(baseDirectory, xmlFile);
                        if (File.Exists(xmlPath))
                        {
                            options.IncludeXmlComments(xmlPath);
                        }
                    }
                }

                options.AddSwaggerDocs();

                options.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out var methodInfo))
                    {
                        return false;
                    }

                    IEnumerable<ApiVersion> versions = methodInfo
                        .DeclaringType?
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    List<ApiVersion> maps = methodInfo
                        .GetCustomAttributes(true)
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToList();

                    return versions?.Any(v => $"v{v}" == version) == true
                           && (!maps.Any() || maps.Any(v => $"v{v}" == version));
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
                options.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Nullable = true,
                    Pattern = @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$",
                    Example = new OpenApiString("02:00:00")
                });
            });
        }

        private static void AddSwaggerDocs(this SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ModularArchitecture API"
            });
        }

        private static IServiceCollection MapModules(this IServiceCollection services)
        {
            List<Assembly> loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            string[] loadedPaths = loadedAssemblies.Where(p => !p.IsDynamic && p.GetName().Name != "ModularArchitecture").Select(a => a.GetName().Name).OrderBy(x => x).ToArray();

            string[] directoryPaths = Directory.GetDirectories(@"..\..\Modules");

            List<string> modules = new List<string>();

            foreach (var directoryPath in directoryPaths)
            {
                string moduleName = Path.GetFileName(Path.GetDirectoryName(directoryPath + "\\"));
                modules.Add(moduleName);

                string[] referencedPaths = Directory.GetFiles($@"{directoryPath}\{moduleName}.Infrastructure\bin\Debug\net5.0", "*.dll");

                List<string> toLoad = new List<string>();

                foreach (string path in referencedPaths)
                {
                    bool add = true;
                    foreach (var loadedPath in loadedPaths)
                    {
                        if (path.Contains(loadedPath))
                        {
                            add = false;
                            break;
                        }
                    }

                    if (add)
                        toLoad.Add(path);
                }

                ConnectorTypes.Instance.Modules = modules;

                toLoad.ForEach(filename =>
                {
                    Assembly a = Assembly.LoadFrom(Path.GetFullPath(filename));
                    AppDomain.CurrentDomain.Load(a.GetName());
                });

                Assembly module = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains($"{moduleName}.Infrastructure"));

                Type serviceCollectionExtensions = module.GetTypes().First(x => x.Name == "ServiceCollectionExtensions");

                services = (IServiceCollection)serviceCollectionExtensions.GetMethod($"Add{moduleName}Infrastructure").Invoke(null, new object[] { services });
            }

            return services;
        }
    }
}