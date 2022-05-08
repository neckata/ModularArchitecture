﻿using System.Threading.Tasks;
using Gamification.Shared.Core.Entities;
using Gamification.Shared.Core.Interfaces;
using Gamification.Shared.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Linq;
using Gamification.Shared.Core.Enums;
using System;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Shared.Infrastructure.Persistence
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
                var connectorsList = Enum.GetValues(typeof(ConnectorTypeEnum)).Cast<ConnectorTypeEnum>().Select(x => x.ToString());
                foreach (string connectorName in connectorsList)
                {
                    var connector = new Connector {Name = connectorName };
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
