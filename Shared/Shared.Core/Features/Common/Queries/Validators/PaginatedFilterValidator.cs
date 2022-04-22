using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Interfaces;
using Gamification.Shared.DTOs.Filters;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Gamification.Shared.Core.Features.Common.Queries.Validators
{
    public abstract class PaginatedFilterValidator<TEntityId, TEntity, TFilter> :
        AbstractValidator<TFilter>,
        IPaginatedFilterValidator<TEntityId, TEntity, TFilter>
            where TEntity : class, IEntity<TEntityId>
            where TFilter : PaginatedFilter
    {
        protected PaginatedFilterValidator(IStringLocalizer localizer)
        {
            IPaginatedFilterValidator<TEntityId, TEntity, TFilter>.UseRules(this, localizer);
        }
    }
}