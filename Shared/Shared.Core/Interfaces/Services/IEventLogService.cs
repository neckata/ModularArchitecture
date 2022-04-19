using Shared.Core.EventLogging;
using Shared.Core.Wrapper;
using Shared.DTOs.Identity.EventLogs;
using System.Threading.Tasks;

namespace Shared.Core.Interfaces
{
    public interface IEventLogService
    {
        Task<PaginatedResult<EventLog>> GetAllAsync(GetEventLogsRequest request);

        Task<Result<string>> LogCustomEventAsync(LogEventRequest request);
    }
}
