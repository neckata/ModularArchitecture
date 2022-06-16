using Host.ModularArchitecture.Factory;
using ModularArchitecture.Shared.Core.Extensions;
using ModularArchitecture.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

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
                  .AddMediatR(Assembly.GetExecutingAssembly())
                  .AddTransient<IConnectorFactory, ConnectorFactory>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure();
        }
    }
}