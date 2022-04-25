using Gamification.Shared.Core.EventLogging;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Shared.Core.Interfaces
{
    public interface IApplicationDbContext : IDbContext
    {
        public DbSet<EventLog> EventLogs { get; set; }
    }
}