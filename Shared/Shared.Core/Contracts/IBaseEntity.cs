using System.Collections.Generic;
using Gamification.Shared.Core.Domain;

namespace Gamification.Shared.Core.Contracts
{
    public interface IBaseEntity
    {
        public IReadOnlyCollection<Event> DomainEvents { get; }

        public void AddDomainEvent(Event domainEvent);

        public void RemoveDomainEvent(Event domainEvent);

        public void ClearDomainEvents();
    }
}