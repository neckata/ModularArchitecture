﻿using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Features.ExtendedAttributes.Filters;
using Gamification.Shared.Core.Mappings.Converters;
using Gamification.Shared.Core.Wrapper;
using Gamification.Shared.DTOs.ExtendedAttributes;
using MediatR;

namespace Gamification.Shared.Core.Features.ExtendedAttributes.Queries
{
    public class GetExtendedAttributesQuery<TEntityId, TEntity> : IRequest<PaginatedResult<GetExtendedAttributesResponse<TEntityId>>>
        where TEntity : class, IEntity<TEntityId>
    {
        public int PageNumber { get; }

        public int PageSize { get; }

        public string? SearchString { get; }

        public string[] OrderBy { get; }

        public TEntityId? EntityId { get; }

        public ExtendedAttributeType? Type { get; }

        public GetExtendedAttributesQuery(PaginatedExtendedAttributeFilter<TEntityId, TEntity> filter)
        {
            PageNumber = filter.PageNumber;
            PageSize = filter.PageSize;
            SearchString = filter.SearchString;
            OrderBy = new OrderByConverter().Convert(filter.OrderBy);
            EntityId = filter.EntityId;
            Type = filter.Type;
        }
    }
}