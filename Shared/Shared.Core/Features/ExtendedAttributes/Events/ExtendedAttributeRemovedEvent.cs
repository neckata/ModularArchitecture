using System;
using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.Utilities;

namespace Gamification.Shared.Core.Features.ExtendedAttributes.Events
{
    public class ExtendedAttributeRemovedEvent<TEntity> : Event
    {
        public Guid Id { get; }

        public string EntityName { get; set; }

        public ExtendedAttributeRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            EntityName = typeof(TEntity).GetGenericTypeName();
            RelatedEntities = new[] { typeof(TEntity) };
        }
    }
}