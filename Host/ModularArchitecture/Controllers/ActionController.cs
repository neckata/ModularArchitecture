using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core;
using ModularArchitecture.Shared.Core.Constants;
using ModularArchitecture.Shared.Core.Enums;
using ModularArchitecture.Shared.Core.Interfaces.Services.Event;
using ModularArchitecture.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Host.ModularArchitecture.Controllers
{
    [ApiVersion("1")]
    public class ActionController : CommonBaseController
    {
        private readonly IConnectorFactory _connectorFactory;
        private readonly IEventService _eventService;

        public ActionController(IConnectorFactory connectorFactory, IEventService eventService)
        {
            _connectorFactory = connectorFactory;
            _eventService = eventService;
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Actions.Create)]
        public async Task<IActionResult> CreateActionAsync(ConnectorTypeEnum connector, CreateActionRequest request)
        {
            var connectorObject = _connectorFactory.CreateFor(connector);

            var response = await connectorObject.CreateActionAsync(request);

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Actions.Edit)]
        public async Task<IActionResult> UpdateActionAsync(ConnectorTypeEnum connector, UpdateActionRequest request)
        {
            var connectorObject = _connectorFactory.CreateFor(connector);

            var response = await connectorObject.UpdateActionAsync(request);

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Actions.View)]
        public async Task<IActionResult> GetActions(ConnectorTypeEnum connector)
        {
            var connectorObject = _connectorFactory.CreateFor(connector);

            var response = await connectorObject.GetActions();

            return Ok(response);
        }

        [HttpGet("GetConnectors")]
        [Authorize(Policy = Permissions.Connectors.View)]
        public async Task<IActionResult> GetConnectors()
        {
            var connecotrs = await _eventService.GetAllConnectorsAsync();

            return Ok(connecotrs);
        }

        [HttpGet("GetConnector")]
        [Authorize(Policy = Permissions.Connectors.View)]
        public async Task<IActionResult> GetConnector(Guid connectorId)
        {
            var connector = await _eventService.GetConnectorAsync(connectorId);

            return Ok(connector);
        }
    }
}
