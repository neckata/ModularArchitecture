using Gamification.Shared.Core.Contracts;
using Gamification.Shared.DTOs.ExtendedAttributes;
using Gamification.Shared.DTOs.Filters;

namespace Gamification.Shared.Core.Features.ExtendedAttributes.Filters
{
    public class PaginatedExtendedAttributeFilter<TEntityId, TEntity> : PaginatedFilter
        where TEntity : class, IEntity<TEntityId>
    {
        public string? SearchString { get; set; }

        public TEntityId? EntityId { get; set; }

        public ExtendedAttributeType? Type { get; set; }
    }
}