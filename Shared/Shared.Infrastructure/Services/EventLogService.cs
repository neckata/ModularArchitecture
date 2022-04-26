using System.Threading.Tasks;
using AutoMapper;
using Gamification.Shared.Core.EventLogging;
using Gamification.Shared.Core.Interfaces.Services;
using Gamification.Shared.Core.Wrapper;
using Gamification.Shared.DTOs.Identity.EventLogs;

namespace Gamification.Shared.Infrastructure.Services
{
    public class EventLogService : IEventLogService
    {
        private readonly IEventLogger _logger;
        private readonly IMapper _mapper;

        public EventLogService(
            IMapper mapper,
            IEventLogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<string>> LogCustomEventAsync(LogEventRequest request)
        {
            var log = _mapper.Map<EventLog>(request);
            await _logger.SaveAsync(log, default);
            return await Result<string>.SuccessAsync(data: log.Id.ToString());
        }
    }
}