using System.Threading.Tasks;
using AutoMapper;
using Gamification.Shared.Core.EventLogging;
using Gamification.Shared.Core.Interfaces;
using Gamification.Shared.Core.Interfaces.Services;
using Gamification.Shared.Core.Wrapper;
using Gamification.Shared.DTOs.Identity.EventLogs;

namespace Gamification.Shared.Infrastructure.Services
{
    public class EventLogService : IEventLogService
    {
        private readonly IEventLogger _logger;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public EventLogService(
            IMapper mapper,
            IEventLogger logger,
            IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<Result<string>> LogCustomEventAsync(LogEventRequest request)
        {
            var log = _mapper.Map<EventLog>(request);
            await _logger.SaveAsync(log, default, _context);
            return await Result<string>.SuccessAsync(data: log.Id.ToString());
        }
    }
}