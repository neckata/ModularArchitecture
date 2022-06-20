using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Slack.Core.Commands;
using Slack.Core.Interfaces;
using Slack.Core.Services;
using System.Reflection;

namespace ModularArchitecture.Modules.Slack.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSlackInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ISlackClient, SlackClient>();
            services.AddMediatR(typeof(CreateActionCommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}