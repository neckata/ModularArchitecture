using Host.ModularArchitecture.Factory;
using ModularArchitecture.Shared.Core;
using ModularArchitecture.Shared.Core.Extensions;
using ModularArchitecture.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

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
                  .AddMediatR(Assembly.GetExecutingAssembly())
                  .AddTransient<IConnectorFactory, ConnectorFactory>();

            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Where(p => !p.IsDynamic && p.GetName().Name != "ModularArchitecture").Select(a => a.GetName().Name).OrderBy(x => x).ToArray();

            var referencedPaths = Directory.GetFiles(@"C:\Users\piked\source\repos\ModularArchitecture\Modules\Outlook\Outlook.Infrastructure\bin\Debug\net5.0", "*.dll");

            List<string> toLoad = new List<string>();

            foreach (var path in referencedPaths)
            {
                bool add = true;
                foreach (var loadedPath in loadedPaths)
                {
                    if (path.Contains(loadedPath))
                    {
                        add = false;
                        break;
                    }
                }

                if (add)
                    toLoad.Add(path);
            }

            toLoad.ForEach(filename =>
            {
                Assembly a = Assembly.LoadFrom(Path.GetFullPath(filename));
                AppDomain.CurrentDomain.Load(a.GetName());
            });

            Assembly Outlook = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains("Outlook.Infrastructure"));

            Type ServiceCollectionExtensions = Outlook.GetTypes().First(x => x.Name == "ServiceCollectionExtensions");

            services = (IServiceCollection)ServiceCollectionExtensions.GetMethod("AddOutlookInfrastructure").Invoke(null, new object[] { services, _config });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure();
        }
    }
}