using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Outlook.Core.Commands;
using Outlook.Core.Interfaces;
using Outlook.Core.Services;
using System.Reflection;

namespace ModularArchitecture.Modules.Outlook.Infrastructure.Extensions
{
    /// <summary>
    /// Extension of ServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Called by reflection in Startup, injecting all services in main assembly
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOutlookInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IOutlookClient, OutlookClient>();
            services.AddMediatR(typeof(CreateActionCommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}