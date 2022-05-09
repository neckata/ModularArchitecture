﻿using System.Linq;
using ModularArchitecture.Shared.Core.Interfaces.Serialization;
using ModularArchitecture.Shared.Core.Serialization;
using ModularArchitecture.Shared.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ModularArchitecture.Shared.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
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