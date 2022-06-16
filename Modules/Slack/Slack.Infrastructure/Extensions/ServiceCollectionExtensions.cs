using Slack.Core.Interfaces;
using Slack.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Slack.Core.Commands;
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