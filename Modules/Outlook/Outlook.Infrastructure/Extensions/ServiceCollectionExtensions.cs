using Outlook.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gamification.Modules.Outlook.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOutlookInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IOutlookService, OutlookConnectorService>();
            services.AddControllers();
            return services;
        }
    }
}