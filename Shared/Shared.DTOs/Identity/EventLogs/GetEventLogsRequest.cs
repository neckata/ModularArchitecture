using System;

namespace Gamification.Shared.DTOs.Identity.EventLogs
{
    public class GetEventLogsRequest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string? SearchString { get; set; }

        public string[]? OrderBy { get; set; }

        public Guid UserId { get; set; }

        public string? MessageType { get; set; }
    }
}