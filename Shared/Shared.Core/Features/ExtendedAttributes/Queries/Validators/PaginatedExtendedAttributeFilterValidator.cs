using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Features.ExtendedAttributes.Filters;
using Gamification.Shared.Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Gamification.Shared.Core.Features.ExtendedAttributes.Queries.Validators
{
    public abstract class PaginatedExtendedAttributeFilterValidator<TEntityId, TEntity> :
        AbstractValidator<PaginatedExtendedAttributeFilter<TEntityId, TEntity>>,
        IPaginatedFilterValidator<TEntityId, TEntity, PaginatedExtendedAttributeFilter<TEntityId, TEntity>>
            where TEntity : class, IEntity<TEntityId>
    {
        protected PaginatedExtendedAttributeFilterValidator(IStringLocalizer localizer)
        {
            IPaginatedFilterValidator<TEntityId, TEntity, PaginatedExtendedAttributeFilter<TEntityId, TEntity>>.UseRules(this, localizer);
        }
    }
}