using System.Threading.Tasks;
using ModularArchitecture.Shared.Core.EventLogging;
using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Identity.EventLogs;

namespace ModularArchitecture.Shared.Core.Interfaces.Services
{
    public interface IEventLogService
    {
        Task<Result<string>> LogCustomEventAsync(LogEventRequest request);
    }
}