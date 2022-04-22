using System;
using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Wrapper;
using Gamification.Shared.DTOs.ExtendedAttributes;
using MediatR;

namespace Gamification.Shared.Core.Features.ExtendedAttributes.Commands
{
    public class UpdateExtendedAttributeCommand<TEntityId, TEntity> : IRequest<Result<Guid>>
        where TEntity : class, IEntity<TEntityId>
    {
        public Guid Id { get; set; }

        public TEntityId EntityId { get; set; }

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

        public bool IsActive { get; set; } = true;
    }
}