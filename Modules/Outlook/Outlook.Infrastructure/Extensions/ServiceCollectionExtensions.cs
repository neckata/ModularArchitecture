using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Outlook.Core.Commands;
using Outlook.Core.Interfaces;
using Outlook.Core.Services;
using System.Reflection;

namespace ModularArchitecture.Modules.Outlook.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOutlookInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IOutlookClient, OutlookClient>();
            services.AddMediatR(typeof(CreateActionCommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}