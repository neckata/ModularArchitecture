using System.Threading.Tasks;
using Gamification.Shared.Core.EventLogging;
using Gamification.Shared.Core.Wrapper;
using Gamification.Shared.DTOs.Identity.EventLogs;

namespace Gamification.Shared.Core.Interfaces.Services
{
    public interface IEventLogService
    {
        Task<PaginatedResult<EventLog>> GetAllAsync(GetEventLogsRequest request);

        Task<Result<string>> LogCustomEventAsync(LogEventRequest request);
    }
}