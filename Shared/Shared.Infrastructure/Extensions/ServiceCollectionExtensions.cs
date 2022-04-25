﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.EventLogging;
using Gamification.Shared.Core.Exceptions;
using Gamification.Shared.Core.Interfaces;
using Gamification.Shared.Core.Interfaces.Services;
using Gamification.Shared.Core.Settings;
using Gamification.Shared.Infrastructure.EventLogging;
using Gamification.Shared.Infrastructure.Interceptors;
using Gamification.Shared.Infrastructure.Middlewares;
using Gamification.Shared.Infrastructure.Persistence;
using Gamification.Shared.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Gamification.Shared.Core.Interfaces.Services.Identity;
using Gamification.Shared.Core.Extensions;

[assembly: InternalsVisibleTo("Gamification")]

namespace Gamification.Shared.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddPersistenceSettings(config);
            services
                .AddDatabaseContext<ApplicationDbContext>()
                .AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IEventLogger, EventLogger>();
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddControllers();
            services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();
            services.AddApplicationLayer(config);
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSingleton<GlobalExceptionHandler>();
            services.AddSwaggerDocumentation();
            services.AddCorsPolicy();
            services.AddApplicationSettings(config);

            return services;
        }

        private static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IEventLogService, EventLogService>();
            return services;
        }

        private static IServiceCollection AddPersistenceSettings(this IServiceCollection services, IConfiguration config)
        {
            return services
                .Configure<PersistenceSettings>(config.GetSection(nameof(PersistenceSettings)));
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

                    var versions = methodInfo
                        .DeclaringType?
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    var maps = methodInfo
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
                Title = "API v1",
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
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

        //public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services
        //        .AddHttpContextAccessor()
        //        .AddScoped<ICurrentUser, CurrentUser>()
        //        .Configure<JwtSettings>(configuration.GetSection("JwtSettings"))
        //        .AddTransient<ITokenService, TokenService>()
        //        .AddTransient<IIdentityService, IdentityService>()
        //        .AddTransient<IUserService, UserService>()
        //        .AddTransient<IRoleService, RoleService>()
        //        .AddTransient<IRoleClaimService, RoleClaimService>()
        //        .AddDatabaseContext<IdentityDbContext>()
        //        .AddScoped<IIdentityDbContext>(provider => provider.GetService<IdentityDbContext>())
        //        .AddIdentity<FluentUser, FluentRole>(options =>
        //        {
        //            options.Password.RequiredLength = 6;
        //            options.Password.RequireDigit = false;
        //            options.Password.RequireLowercase = false;
        //            options.Password.RequireNonAlphanumeric = false;
        //            options.Password.RequireUppercase = false;
        //            options.User.RequireUniqueEmail = true;
        //        })
        //        .AddEntityFrameworkStores<IdentityDbContext>()
        //        .AddDefaultTokenProviders();
        //    services.AddExtendedAttributeDbContextsFromAssembly(typeof(IdentityDbContext), Assembly.GetAssembly(typeof(IIdentityDbContext)));
        //    services.AddTransient<IDatabaseSeeder, IdentityDbSeeder>();
        //    services.AddPermissions(configuration);
        //    services.AddJwtAuthentication(configuration);
        //    return services;
        //}

        //public static IServiceCollection AddPermissions(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
        //        .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        //    return services;
        //}

        //internal static IServiceCollection AddJwtAuthentication(
        //    this IServiceCollection services, IConfiguration config)
        //{
        //    var jwtSettings = services.GetOptions<JwtSettings>(nameof(JwtSettings));
        //    byte[] key = Encoding.ASCII.GetBytes(jwtSettings.Key);
        //    services
        //        .AddAuthentication(authentication =>
        //        {
        //            authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //            authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        })
        //        .AddJwtBearer(bearer =>
        //        {
        //            bearer.RequireHttpsMetadata = false;
        //            bearer.SaveToken = true;
        //            bearer.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuerSigningKey = true,
        //                IssuerSigningKey = new SymmetricSecurityKey(key),
        //                ValidateIssuer = false,
        //                ValidateAudience = false,
        //                RoleClaimType = ClaimTypes.Role,
        //                ClockSkew = TimeSpan.Zero
        //            };
        //            bearer.Events = new JwtBearerEvents
        //            {
        //                OnChallenge = context =>
        //                {
        //                    context.HandleResponse();
        //                    if (!context.Response.HasStarted)
        //                    {
        //                        throw new IdentityException("You are not Authorized.", statusCode: HttpStatusCode.Unauthorized);
        //                    }

        //                    return Task.CompletedTask;
        //                },
        //                OnForbidden = context =>
        //                {
        //                    throw new IdentityException("You are not authorized to access this resource.", statusCode: HttpStatusCode.Forbidden);
        //                },
        //            };
        //        });
        //    return services;
        //}
    }
}