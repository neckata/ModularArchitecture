using System;
using Gamification.Shared.DTOs.Filters;

namespace Gamification.Shared.DTOs.Identity.EventLogs
{
    public class PaginatedEventLogsFilter : PaginatedFilter
    {
        public string? SearchString { get; set; }

        public Guid UserId { get; set; }

        public string? Email { get; set; }

        public string? MessageType { get; set; }
    }
}