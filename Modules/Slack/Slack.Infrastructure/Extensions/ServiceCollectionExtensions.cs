using Slack.Core.Interfaces;
using Slack.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ModularArchitecture.Modules.Slack.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSlackInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ISlackClient, SlackConnectorClient>();
            return services;
        }
    }
}