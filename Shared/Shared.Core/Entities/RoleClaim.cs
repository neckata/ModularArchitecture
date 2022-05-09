using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ModularArchitecture.Shared.Core.Contracts;
using ModularArchitecture.Shared.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace ModularArchitecture.Shared.Core.Entities
{
    public class RoleClaim : IdentityRoleClaim<string>, IBaseEntity
    {
        public string Description { get; set; }

        public string Group { get; set; }

        public virtual Role Role { get; set; }

        private List<Event> _domainEvents;

        [NotMapped]
        public IReadOnlyCollection<Event> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(Event domainEvent)
        {
            _domainEvents ??= new List<Event>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(Event domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public RoleClaim()
            : base()
        {
        }

        public RoleClaim(string roleClaimDescription = null, string roleClaimGroup = null)
            : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }
    }
}
