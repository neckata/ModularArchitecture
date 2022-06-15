using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Constants;
using ModularArchitecture.Shared.Core.Interfaces.Services.Event;
using ModularArchitecture.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using Host.ModularArchitecture.Factory;
using ModularArchitecture.Shared.Infrastructure.Enums;
using System.Collections.Generic;
using ModularArchitecture.Shared.DTOs.Connector;
using ModularArchitecture.Shared.Core.Wrapper;

namespace Host.ModularArchitecture.Controllers
{
    [ApiVersion("1")]
    public class ActionController : CommonBaseController
    {
        private readonly IConnectorFactory _connectorFactory;
        private readonly IEventService _eventService;
        private readonly IMediator _mediator;

        public ActionController(IConnectorFactory connectorFactory, IEventService eventService, IMediator mediator)
        {
            _connectorFactory = connectorFactory;
            _eventService = eventService;
            _mediator = mediator;
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Actions.Create)]
        public async Task<IActionResult> CreateActionAsync(string connectorType, CreateActionRequest request)
        {
            object command = _connectorFactory.CreateCommand(connectorType, request, ActionsTypeEnum.Create);

            object response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Actions.Edit)]
        public async Task<IActionResult> UpdateActionAsync(string connectorType, UpdateActionRequest request)
        {
            object command = _connectorFactory.CreateCommand(connectorType, request, ActionsTypeEnum.Update);

            object response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Actions.View)]
        public async Task<IActionResult> GetActions(string connectorType)
        {
            object command = _connectorFactory.CreateCommand(connectorType, null, ActionsTypeEnum.View);

            object response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("GetConnectors")]
       [Authorize(Policy = Permissions.Connectors.View)]
        public async Task<IActionResult> GetConnectors()
        {
            IResult<List<ConnectorResponse>> connectors = await _eventService.GetAllConnectorsAsync();

            return Ok(connectors);
        }

        [HttpGet("GetConnector")]
        [Authorize(Policy = Permissions.Connectors.View)]
        public async Task<IActionResult> GetConnector(Guid connectorId)
        {
            IResult<ConnectorResponse> connector = await _eventService.GetConnectorAsync(connectorId);

            return Ok(connector);
        }
    }
}
