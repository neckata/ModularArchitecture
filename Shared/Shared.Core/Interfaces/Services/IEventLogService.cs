using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Identity.EventLogs;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Core.Interfaces.Services
{
    public interface IEventLogService
    {
        Task<Result<string>> LogCustomEventAsync(LogEventRequest request);
    }
}