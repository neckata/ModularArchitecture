using Gamification.Shared.Core.Wrapper;
using Gamification.Shared.DTOs.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gamification.Shared.Core.Interfaces.Services.Event
{
    public interface IEventService
    {
        Task<Result<List<ConnectorResponse>>> GetAllConnectorsAsync();

        Task<IResult<ConnectorResponse>> GetConnectorAsync(Guid connectorId);
    }
}
