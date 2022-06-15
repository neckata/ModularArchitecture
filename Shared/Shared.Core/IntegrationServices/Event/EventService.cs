using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Interfaces.Services.Event;
using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Connector;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Core.IntegrationServices.Event
{
    public class EventService : IEventService
    {
        private readonly IApplicationDbContext _context;

        public EventService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResult<List<ConnectorResponse>>> GetAllConnectorsAsync()
        {
            List<ConnectorResponse> connectors = await _context.Connectors.AsNoTracking().Select(c => new ConnectorResponse { Id = c.Id, Name = c.Name }).ToListAsync();

            return await Result<List<ConnectorResponse>>.SuccessAsync(connectors);
        }

        public async Task<IResult<ConnectorResponse>> GetConnectorAsync(Guid connectorId)
        {
            ConnectorResponse connector = await _context.Connectors.AsNoTracking().Select(c => new ConnectorResponse { Id = c.Id, Name = c.Name }).FirstOrDefaultAsync(c => c.Id == connectorId);

            return await Result<ConnectorResponse>.SuccessAsync(connector);
        }
    }
}
