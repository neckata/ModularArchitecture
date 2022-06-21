using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Interfaces.Services;
using ModularArchitecture.Shared.Infrastructure.Utilities;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Infrastructure.Persistence
{
    internal class ModuleDbSeeder : IDatabaseSeeder
    {
        private readonly ILogger<ModuleDbSeeder> _logger;
        private readonly IApplicationDbContext _db;

        public ModuleDbSeeder(
            ILogger<ModuleDbSeeder> logger,
            IApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void Initialize()
        {
            AddModules();
            _db.SaveChanges();
        }

        private void AddModules()
        {
            Task.Run(async () =>
            {
                foreach (string ModuleName in ModuleTypes.Instance.Modules)
                {
                    Module Module = new Module { Name = ModuleName };
                    var ModuleInDb = await _db.Modules.FirstOrDefaultAsync(x => x.Name == ModuleName);
                    if (ModuleInDb == null)
                    {
                        await _db.Modules.AddAsync(Module);
                        _logger.LogInformation(string.Format("Added '{0}' to Modules", ModuleName));
                    }
                }
            }).GetAwaiter().GetResult();
        }
    }
}
