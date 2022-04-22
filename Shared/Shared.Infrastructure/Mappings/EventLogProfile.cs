using AutoMapper;
using Gamification.Shared.Core.EventLogging;
using Gamification.Shared.Core.Mappings.Converters;
using Gamification.Shared.DTOs.Identity.EventLogs;

namespace Gamification.Shared.Infrastructure.Mappings
{
    public class EventLogProfile : Profile
    {
        public EventLogProfile()
        {
            CreateMap<LogEventRequest, EventLog>().ReverseMap();
        }
    }
}