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
            CreateMap<PaginatedEventLogsFilter, GetEventLogsRequest>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<LogEventRequest, EventLog>().ReverseMap();
        }
    }
}