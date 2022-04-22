using System;
using System.Linq;
using System.Reflection;
using Gamification.Shared.Core.Behaviors;
using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.Features.Common.Queries.Validators;
using Gamification.Shared.Core.Features.ExtendedAttributes.Commands;
using Gamification.Shared.Core.Features.ExtendedAttributes.Filters;
using Gamification.Shared.Core.Features.ExtendedAttributes.Queries;
using Gamification.Shared.Core.Interfaces.Serialization;
using Gamification.Shared.Core.Serialization;
using Gamification.Shared.Core.Settings;
using Gamification.Shared.Core.Wrapper;
using Gamification.Shared.DTOs.ExtendedAttributes;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gamification.Shared.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }

        public static IServiceCollection AddSerialization(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SerializationSettings>(config.GetSection(nameof(SerializationSettings)));
            var options = services.GetOptions<SerializationSettings>(nameof(SerializationSettings));
            services.AddSingleton<IJsonSerializerSettingsOptions, JsonSerializerSettingsOptions>();
            if (options.UseSystemTextJson)
            {
                services
                    .AddSingleton<IJsonSerializer, SystemTextJsonSerializer>()
                    .Configure<JsonSerializerSettingsOptions>(configureOptions =>
                    {
                        if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        {
                            configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                        }
                    });
            }
            else if (options.UseNewtonsoftJson)
            {
                services
                    .AddSingleton<IJsonSerializer, NewtonSoftJsonSerializer>();
            }

            return services;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName)
            where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var section = configuration.GetSection(sectionName);
            var options = new T();
            section.Bind(options);

            return options;
        }
    }
}