using Host.ModularArchitecture.Factory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Constants;
using ModularArchitecture.Shared.Core.Interfaces.Services.Event;
using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Connector;
using ModularArchitecture.Shared.Infrastructure.Controllers;
using ModularArchitecture.Shared.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.ModularArchitecture.Controllers
{
    /// <summary>
    /// Controller used for accessing available connectors and using their features
    /// </summary>
    [ApiVersion("1")]
    public class ActionController : CommonBaseController
    {
        private readonly IConnectorFactory _connectorFactory;
        private readonly IEventService _eventService;
        private readonly IMediator _mediator;

        /// <summary>
        /// ActionController
        /// </summary>
        /// <param name="connectorFactory"></param>
        /// <param name="eventService"></param>
        /// <param name="mediator"></param>
        public ActionController(IConnectorFactory connectorFactory, IEventService eventService, IMediator mediator)
        {
            _connectorFactory = connectorFactory;
            _eventService = eventService;
            _mediator = mediator;
        }

        /// <summary>
        /// Create action from specific connector
        /// </summary>
        /// <param name="connectorType">Connector</param>
        /// <param name="request">Action</param>
        /// <returns>Created action</returns>
        [HttpPut]
        [Authorize(Policy = Permissions.Actions.Create)]
        public async Task<IActionResult> CreateActionAsync(string connectorType, CreateActionRequest request)
        {
            object command = _connectorFactory.CreateCommand(connectorType, request, ActionsTypeEnum.Create);

            object response = await _mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Update action from specific connector
        /// </summary>
        /// <param name="connectorType">Connector</param>
        /// <param name="request">Action</param>
        /// <returns>Updated action</returns>
        [HttpPost]
        [Authorize(Policy = Permissions.Actions.Edit)]
        public async Task<IActionResult> UpdateActionAsync(string connectorType, UpdateActionRequest request)
        {
            object command = _connectorFactory.CreateCommand(connectorType, request, ActionsTypeEnum.Update);

            object response = await _mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Get actions from specific connector
        /// </summary>
        /// <param name="connectorType">Connector</param>
        /// <returns>List of actions</returns>
        [HttpGet]
        [Authorize(Policy = Permissions.Actions.View)]
        public async Task<IActionResult> GetActions(string connectorType)
        {
            object command = _connectorFactory.CreateCommand(connectorType, null, ActionsTypeEnum.View);

            object response = await _mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Get available connectors
        /// </summary>
        /// <returns>List of connectors</returns>
        [HttpGet("GetConnectors")]
        [Authorize(Policy = Permissions.Connectors.View)]
        public async Task<IActionResult> GetConnectors()
        {
            IResult<List<ConnectorResponse>> connectors = await _eventService.GetAllConnectorsAsync();

            return Ok(connectors);
        }

        /// <summary>
        /// Get detailed information about connector
        /// </summary>
        /// <param name="connectorId"></param>
        /// <returns>Connector</returns>
        [HttpGet("GetConnector")]
        [Authorize(Policy = Permissions.Connectors.View)]
        public async Task<IActionResult> GetConnector(Guid connectorId)
        {
            IResult<ConnectorResponse> connector = await _eventService.GetConnectorAsync(connectorId);

            return Ok(connector);
        }
    }
}
