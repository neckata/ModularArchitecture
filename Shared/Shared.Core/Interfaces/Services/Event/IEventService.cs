using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Core.Interfaces.Services.Event
{
    public interface IEventService
    {
        Task<IResult<List<ConnectorResponse>>> GetAllConnectorsAsync();

        Task<IResult<ConnectorResponse>> GetConnectorAsync(Guid connectorId);
    }
}
