using Host.ModularArchitecture.ModuleResolver;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularArchitecture.Shared.Core.Extensions;
using ModularArchitecture.Shared.Infrastructure.Extensions;
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
                  .AddTransient<IModuleResolver, ModuleResolver.ModuleResolver>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure();
        }
    }
}