using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Slack.Core.Commands;
using Slack.Core.Interfaces;
using Slack.Core.Services;
using System.Reflection;

namespace ModularArchitecture.Modules.Slack.Infrastructure.Extensions
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
        public static IServiceCollection AddSlackInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ISlackClient, SlackClient>();
            services.AddMediatR(typeof(CreateActionCommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}