using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Extensions;
using Gamification.Shared.DTOs.Filters;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Gamification.Shared.Core.Interfaces
{
    internal interface IPaginatedFilterValidator<TEntityId, TEntity, TFilter>
        where TEntity : class, IEntity<TEntityId>
        where TFilter : PaginatedFilter
    {
        static void UseRules(AbstractValidator<TFilter> validator, IStringLocalizer localizer)
        {
            validator.RuleFor(request => request.PageNumber)
                .GreaterThan(0).WithMessage(localizer["The {PropertyName} property must be greater than 0."]);
            validator.RuleFor(request => request.PageSize)
                .GreaterThan(0).WithMessage(localizer["The {PropertyName} property must be greater than 0."]);
            validator.RuleFor(request => request.OrderBy)
                .MustContainCorrectOrderingsFor(typeof(TEntity), localizer);
        }
    }
}