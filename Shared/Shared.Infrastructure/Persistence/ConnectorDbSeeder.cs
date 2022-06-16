using System.Threading.Tasks;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ModularArchitecture.Shared.Infrastructure.Utilities;

namespace ModularArchitecture.Shared.Infrastructure.Persistence
{
    internal class ConnectorDbSeeder : IDatabaseSeeder
    {
        private readonly ILogger<ConnectorDbSeeder> _logger;
        private readonly IApplicationDbContext _db;

        public ConnectorDbSeeder(
            ILogger<ConnectorDbSeeder> logger,
            IApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void Initialize()
        {
            AddConnectors();
            _db.SaveChanges();
        }

        private void AddConnectors()
        {
            Task.Run(async () =>
            {
                foreach (string connectorName in ConnectorTypes.Instance.Modules)
                {
                    Connector connector = new Connector {Name = connectorName };
                    var connectorInDb = await _db.Connectors.FirstOrDefaultAsync(x=>x.Name == connectorName);
                    if (connectorInDb == null)
                    {
                        await _db.Connectors.AddAsync(connector);
                        _logger.LogInformation(string.Format("Added '{0}' to Connectors", connectorName));
                    }
                }
            }).GetAwaiter().GetResult();
        }
    }
}
