using Host.ModularArchitecture.Factory;
using ModularArchitecture.Modules.ExcelUpload.Infrastructure.Extensions;
using ModularArchitecture.Modules.Outlook.Infrastructure.Extensions;
using ModularArchitecture.Shared.Core;
using ModularArchitecture.Shared.Core.Extensions;
using ModularArchitecture.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Host.ModularArchitecture
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