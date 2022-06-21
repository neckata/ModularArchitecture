using Microsoft.EntityFrameworkCore;
using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Interfaces.Services.Event;
using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Core.IntegrationServices.Event
{
    /// <summary>
    /// Get connectors which are availabe for use
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IApplicationDbContext _context;

        /// <summary>
        /// EventService
        /// </summary>
        /// <param name="context"></param>
        public EventService(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all availabe connectors
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<List<ConnectorResponse>>> GetAllConnectorsAsync()
        {
            List<ConnectorResponse> connectors = await _context.Connectors.AsNoTracking().Select(c => new ConnectorResponse { Id = c.Id, Name = c.Name }).ToListAsync();

            return await Result<List<ConnectorResponse>>.SuccessAsync(connectors);
        }

        /// <summary>
        /// Gets Connector detailed information
        /// </summary>
        /// <param name="connectorId"></param>
        /// <returns></returns>
        public async Task<IResult<ConnectorResponse>> GetConnectorAsync(Guid connectorId)
        {
            ConnectorResponse connector = await _context.Connectors.AsNoTracking().Select(c => new ConnectorResponse { Id = c.Id, Name = c.Name }).FirstOrDefaultAsync(c => c.Id == connectorId);

            return await Result<ConnectorResponse>.SuccessAsync(connector);
        }
    }
}
