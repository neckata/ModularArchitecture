using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Gamification.Shared.Core.EventLogging;
using Gamification.Shared.Core.Exceptions;
using Gamification.Shared.Core.Extensions;
using Gamification.Shared.Core.Interfaces;
using Gamification.Shared.Core.Interfaces.Services;
using Gamification.Shared.Core.Mappings.Converters;
using Gamification.Shared.Core.Wrapper;
using Gamification.Shared.DTOs.Identity.EventLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Gamification.Shared.Infrastructure.Services
{
    public class EventLogService : IEventLogService
    {
        private readonly IEventLogger _logger;
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EventLogService(
            IApplicationDbContext dbContext,
            IMapper mapper,
            IEventLogger logger)
        {
            _dbContext = dbContext;
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