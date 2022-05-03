using ExcelUpload.Core.Interfaces;
using ExcelUpload.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gamification.Modules.ExcelUpload.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExcelUploadInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExcelUploadClient, ExcelUploadConnectorClient>();
            services.AddControllers();
            return services;
        }
    }
}