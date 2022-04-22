using Shared.Core.Entity;
using Shared.Core.EventLogging;
using Shared.Core.Interfaces;
using Shared.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Gamification.Bootstrapper")]
namespace Shared.Infrastructure.Persistence
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly PersistenceSettings _persistenceOptions;

        protected string Schema => "Application";

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<PersistenceSettings> persistenceOptions)
                : base(options)
        {
            _persistenceOptions = persistenceOptions.Value;
        }

        public DbSet<EventLog> EventLogs { get; set; }

        public DbSet<EntityReference> EntityReferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
