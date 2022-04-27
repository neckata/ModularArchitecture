using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamification.Shared.Core.Entities
{
    public class Connector : IEntity<string>, IBaseEntity
    {
        public string Id { get; set; }

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

        public Connector()
        {

        }
    }
}
