using AutoMapper;
using ModularArchitecture.Shared.Core.EventLogging;
using ModularArchitecture.Shared.DTOs.Identity.EventLogs;

namespace ModularArchitecture.Shared.Infrastructure.Mappings
{
    public class EventLogProfile : Profile
    {
        public EventLogProfile()
        {
            CreateMap<LogEventRequest, EventLog>().ReverseMap();
        }
    }
}