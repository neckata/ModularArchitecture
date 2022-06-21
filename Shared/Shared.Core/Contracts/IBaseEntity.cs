using ModularArchitecture.Shared.Core.Domain;
using System.Collections.Generic;

namespace ModularArchitecture.Shared.Core.Contracts
{
    /// <summary>
    /// Base entity to extend
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Events which occured in entity lifecycle
        /// </summary>
        public IReadOnlyCollection<Event> DomainEvents { get; }

        /// <summary>
        /// Add event in entity lifecycle(update/delete/add)
        /// </summary>
        /// <param name="domainEvent"></param>
        public void AddDomainEvent(Event domainEvent);

        /// <summary>
        /// Remove event from entity lifecycle
        /// </summary>
        /// <param name="domainEvent"></param>
        public void RemoveDomainEvent(Event domainEvent);

        /// <summary>
        /// Clear all events from entity
        /// </summary>
        public void ClearDomainEvents();
    }
}