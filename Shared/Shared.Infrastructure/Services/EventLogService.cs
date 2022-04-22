using Shared.Core.EventLogging;
using Shared.Core.Interfaces;
using Shared.Core.Wrapper;
using Shared.DTOs.Identity.EventLogs;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Services
{
    public class EventLogService : IEventLogService
    {
        public Task<PaginatedResult<EventLog>> GetAllAsync(GetEventLogsRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<string>> LogCustomEventAsync(LogEventRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
