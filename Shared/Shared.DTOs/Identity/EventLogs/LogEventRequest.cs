using System;

namespace ModularArchitecture.Shared.DTOs.Identity.EventLogs
{
    public class LogEventRequest
    {
        public string Event { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }
    }
}