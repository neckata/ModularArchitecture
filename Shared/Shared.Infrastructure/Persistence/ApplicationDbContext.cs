using ModularArchitecture.Shared.Core.Contracts;
using ModularArchitecture.Shared.Core.Domain;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.EventLogging;
using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Interfaces.Serialization;
using ModularArchitecture.Shared.Core.Settings;
using ModularArchitecture.Shared.Core.Utilities;
using ModularArchitecture.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Infrastructure.Persistence
{
    internal class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, RoleClaim, IdentityUserToken<string>>,
        IApplicationDbContext
    {
        private readonly IEventLogger _eventLogger;
        private readonly PersistenceSettings _persistenceOptions;
        private readonly IJsonSerializer _json;

        protected string Schema => "Application";

        public DbSet<EventLog> EventLogs { get; set; }

        public DbSet<Connector> Connectors { get; set; }

        public DbSet<Action> Actions { get; set; }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IEventLogger eventLogger,
            IOptions<PersistenceSettings> persistenceOptions,
            IJsonSerializer json)
                : base(options)
        {
            _eventLogger = eventLogger;
            _persistenceOptions = persistenceOptions.Value;
            _json = json;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!string.IsNullOrWhiteSpace(Schema))
            {
                modelBuilder.HasDefaultSchema(Schema);
            }

            modelBuilder.Ignore<Event>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyModuleConfiguration(_persistenceOptions);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changes = OnBeforeSaveChanges();
            List<EntityEntry<IBaseEntity>> domainEntities = ChangeTracker
                  .Entries<IBaseEntity>()
                  .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                  .ToList();

            List<Event> domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            IEnumerable<Task> tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    var relatedEntriesChanges = changes.Where(x => domainEvent.RelatedEntities.Any(t => t == x.entityEntry.Entity.GetType())).ToList();
                    if (relatedEntriesChanges.Any())
                    {
                        Dictionary<string,string> oldValues = relatedEntriesChanges.ToDictionary(x => x.entityEntry.Entity.GetType().GetGenericTypeName(), y => y.oldValues);
                        Dictionary<string, string> newValues = relatedEntriesChanges.ToDictionary(x => x.entityEntry.Entity.GetType().GetGenericTypeName(), y => y.newValues);
                        var relatedChanges = (oldValues.Count == 0 ? null : _json.Serialize(oldValues), newValues.Count == 0 ? null : _json.Serialize(newValues));
                        await _eventLogger.SaveAsync(domainEvent, relatedChanges, this);
                    }
                    else
                    {
                        await _eventLogger.SaveAsync(domainEvent, (null, null), this);
                    }
                });
            await Task.WhenAll(tasks);

            return await SaveChangesAsync(true, cancellationToken);
        }

        private List<(EntityEntry entityEntry, string oldValues, string newValues)> OnBeforeSaveChanges()
        {
            var result = new List<(EntityEntry entityEntry, string oldValues, string newValues)>();
            ChangeTracker.DetectChanges();
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                Dictionary<string, object> previousData = new Dictionary<string, object>();
                Dictionary<string, object> currentData = new Dictionary<string, object>();
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    object originalValue = entry.GetDatabaseValues()?.GetValue<object>(propertyName);
                    switch (entry.State)
                    {
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Added:
                            currentData[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            previousData[propertyName] = originalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified && originalValue?.Equals(property.CurrentValue) == false)
                            {
                                previousData[propertyName] = originalValue;
                                currentData[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }

                string oldValues = previousData.Count == 0 ? null : _json.Serialize(previousData);
                string newValues = currentData.Count == 0 ? null : _json.Serialize(currentData);
                result.Add((entry, oldValues, newValues));
            }

            return result;
        }
    }
}