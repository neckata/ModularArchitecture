using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace Gamification.Shared.Core.Entities
{
    public class User : IdentityUser, IEntity<string>, IBaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CreatedBy { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

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
    }
}
