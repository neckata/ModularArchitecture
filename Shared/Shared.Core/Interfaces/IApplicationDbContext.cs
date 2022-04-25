using Gamification.Shared.Core.Entities;
using Gamification.Shared.Core.EventLogging;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Shared.Core.Interfaces
{
    public interface IApplicationDbContext : IDbContext
    {
        public DbSet<EventLog> EventLogs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RoleClaim> RoleClaims { get; set; }
    }
}