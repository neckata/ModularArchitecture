using Outlook.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Outlook.Core.Commands;

namespace ModularArchitecture.Modules.Outlook.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOutlookInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IOutlookClient, OutlookConnectorClient>();
            services.AddMediatR(typeof(CreateActionCommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}