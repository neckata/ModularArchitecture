using System;
using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Wrapper;
using Gamification.Shared.DTOs.ExtendedAttributes;
using MediatR;

namespace Gamification.Shared.Core.Features.ExtendedAttributes.Queries
{
    public class GetExtendedAttributeByIdQuery<TEntityId, TEntity> : IRequest<Result<GetExtendedAttributeByIdResponse<TEntityId>>>
        where TEntity : class, IEntity<TEntityId>
    {
        public Guid Id { get; protected set; }
    }
}