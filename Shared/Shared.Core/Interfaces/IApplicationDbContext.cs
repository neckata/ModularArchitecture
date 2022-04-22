using Gamification.Shared.Core.Entities;
using Gamification.Shared.Core.EventLogging;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Shared.Core.Interfaces
{
    public interface IApplicationDbContext : IDbContext
    {
        public DbSet<EventLog> EventLogs { get; set; }

        public DbSet<EntityReference> EntityReferences { get; set; }
    }
}