using Microsoft.EntityFrameworkCore;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.EventLogging;

namespace ModularArchitecture.Shared.Core.Interfaces
{
    public interface IApplicationDbContext : IDbContext
    {
        public DbSet<EventLog> EventLogs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RoleClaim> RoleClaims { get; set; }

        public DbSet<Connector> Connectors { get; set; }

        public DbSet<Action> Actions { get; set; }
    }
}