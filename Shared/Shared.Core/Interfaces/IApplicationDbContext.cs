using Shared.Core.EventLogging;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Entity;

namespace Shared.Core.Interfaces
{
    public interface IApplicationDbContext : IDbContext
    {
        public DbSet<EventLog> EventLogs { get; set; }

        public DbSet<EntityReference> EntityReferences { get; set; }
    }
}
