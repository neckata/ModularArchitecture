using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace Gamification.Shared.Core.Entities
{
    public class Role : IdentityRole, IEntity<string>, IBaseEntity
    {
        public string Description { get; set; }

        public virtual ICollection<RoleClaim> RoleClaims { get; set; }

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

        public Role()
            : base()
        {
            RoleClaims = new HashSet<RoleClaim>();
        }

        public Role(string roleName, string roleDescription = null)
            : base(roleName)
        {
            RoleClaims = new HashSet<RoleClaim>();
            Description = roleDescription;
        }
    }
}
