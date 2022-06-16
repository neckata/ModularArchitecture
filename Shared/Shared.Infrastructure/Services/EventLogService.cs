using System.Threading.Tasks;
using AutoMapper;
using ModularArchitecture.Shared.Core.EventLogging;
using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Interfaces.Services;
using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Identity.EventLogs;

namespace ModularArchitecture.Shared.Infrastructure.Services
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
            EventLog log = _mapper.Map<EventLog>(request);
            await _logger.SaveAsync(log, default, _context);
            return await Result<string>.SuccessAsync(data: log.Id.ToString());
        }
    }
}