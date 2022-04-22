using System;
using Gamification.Shared.Core.EventLogging;
using Gamification.Shared.Core.Features.Common.Queries.Validators;
using Gamification.Shared.DTOs.Identity.EventLogs;
using Microsoft.Extensions.Localization;

namespace Gamification.Shared.Infrastructure.Validators
{
    public class PaginatedEventLogFilterValidator : PaginatedFilterValidator<Guid, EventLog, PaginatedEventLogsFilter>
    {
        public PaginatedEventLogFilterValidator(IStringLocalizer<PaginatedEventLogFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}