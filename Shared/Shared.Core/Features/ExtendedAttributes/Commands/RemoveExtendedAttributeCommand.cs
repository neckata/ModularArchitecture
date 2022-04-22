using System;
using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Wrapper;
using MediatR;

namespace Gamification.Shared.Core.Features.ExtendedAttributes.Commands
{
    public class RemoveExtendedAttributeCommand<TEntityId, TEntity> : IRequest<Result<Guid>>
        where TEntity : class, IEntity<TEntityId>
    {
        public Guid Id { get; }

        public RemoveExtendedAttributeCommand(Guid entityExtendedAttributeId)
        {
            Id = entityExtendedAttributeId;
        }
    }
}