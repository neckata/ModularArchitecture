using Gamification.Factory;
using Gamification.Modules.ExcelUpload.Infrastructure.Extensions;
using Gamification.Modules.Outlook.Infrastructure.Extensions;
using Gamification.Shared.Core;
using Gamification.Shared.Core.Extensions;
using Gamification.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gamification
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                  .AddDistributedMemoryCache()
                  .AddSerialization(_config)
                  .AddSharedInfrastructure(_config)
                  .AddExcelUploadInfrastructure(_config)
                  .AddOutlookInfrastructure(_config);

            services.AddTransient<IConnectorFactory, ConnectorFactory>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure();
        }
    }
}