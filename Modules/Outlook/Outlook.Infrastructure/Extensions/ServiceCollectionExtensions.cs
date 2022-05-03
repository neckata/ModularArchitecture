using Outlook.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gamification.Modules.Outlook.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOutlookInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IOutlookClient, OutlookConnectorClient>();
            services.AddControllers();
            return services;
        }
    }
}