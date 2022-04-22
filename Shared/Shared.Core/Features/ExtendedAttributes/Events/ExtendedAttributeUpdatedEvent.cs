﻿using System;
using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.Utilities;
using Gamification.Shared.DTOs.ExtendedAttributes;

namespace Gamification.Shared.Core.Features.ExtendedAttributes.Events
{
    public class ExtendedAttributeUpdatedEvent<TEntityId, TEntity> : Event
        where TEntity : class, IEntity<TEntityId>
    {
        public TEntityId EntityId { get; set; }

        public string EntityName { get; set; }

        public ExtendedAttributeType Type { get; set; }

        public string Key { get; set; }

        public decimal? Decimal { get; set; }

        public string? Text { get; set; }

        public DateTime? DateTime { get; set; }

        public string? Json { get; set; }

        public bool? Boolean { get; set; }

        public int? Integer { get; set; }

        public string? ExternalId { get; set; }

        public string? Group { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public ExtendedAttributeUpdatedEvent(ExtendedAttribute<TEntityId, TEntity> extendedAttribute)
        {
            EntityId = extendedAttribute.EntityId;
            EntityName = typeof(TEntity).GetGenericTypeName();
            Type = extendedAttribute.Type;
            Key = extendedAttribute.Key;
            Decimal = extendedAttribute.Decimal;
            Text = extendedAttribute.Text;
            DateTime = extendedAttribute.DateTime;
            Json = extendedAttribute.Json;
            Boolean = extendedAttribute.Boolean;
            Integer = extendedAttribute.Integer;
            ExternalId = extendedAttribute.ExternalId;
            Group = extendedAttribute.Group;
            Description = extendedAttribute.Description;
            IsActive = extendedAttribute.IsActive;
            AggregateId = extendedAttribute.Id;
            RelatedEntities = new[] { typeof(TEntity) };
        }
    }
}