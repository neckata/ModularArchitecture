using Gamification.Shared.DTOs.Connector;
using Gamification.Shared.Core.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gamification.Shared.Core.Interfaces.Services.Connector
{
    public interface IConnectorService
    {
        Task<Result<List<ConnectorResponse>>> GetAllAsync();

        Task<IResult<ConnectorResponse>> GetAsync(string connectorId);

        Task<IResult<string>> UpdateAsync(UpdateConnectorRequest request);
    }
}
