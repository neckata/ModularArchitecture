using Host.ModularArchitecture.ModuleResolver;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Constants;
using ModularArchitecture.Shared.Core.Interfaces.Services.Module;
using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Module;
using ModularArchitecture.Shared.Infrastructure.Controllers;
using ModularArchitecture.Shared.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.ModularArchitecture.Controllers
{
    /// <summary>
    /// Controller used for accessing available Modules and using their features
    /// </summary>
    [ApiVersion("1")]
    public class ActionController : CommonBaseController
    {
        private readonly IModuleResolver _moduleResolver;
        private readonly IModuleService _eventService;
        private readonly IMediator _mediator;

        /// <summary>
        /// ActionController
        /// </summary>
        /// <param name="moduleResolver"></param>
        /// <param name="eventService"></param>
        /// <param name="mediator"></param>
        public ActionController(IModuleResolver moduleResolver, IModuleService eventService, IMediator mediator)
        {
            _moduleResolver = moduleResolver;
            _eventService = eventService;
            _mediator = mediator;
        }

        /// <summary>
        /// Create action from specific Module
        /// </summary>
        /// <param name="moduleType">Module</param>
        /// <param name="request">Action</param>
        /// <returns>Created action</returns>
        [HttpPut]
        [Authorize(Policy = Permissions.Actions.Create)]
        public async Task<IActionResult> CreateActionAsync(string moduleType, CreateActionRequest request)
        {
            object command = _moduleResolver.CreateCommand(moduleType, request, ActionsTypeEnum.Create);

            object response = await _mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Update action from specific Module
        /// </summary>
        /// <param name="moduleType">Module</param>
        /// <param name="request">Action</param>
        /// <returns>Updated action</returns>
        [HttpPost]
        [Authorize(Policy = Permissions.Actions.Edit)]
        public async Task<IActionResult> UpdateActionAsync(string moduleType, UpdateActionRequest request)
        {
            object command = _moduleResolver.CreateCommand(moduleType, request, ActionsTypeEnum.Update);

            object response = await _mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Get actions from specific Module
        /// </summary>
        /// <param name="moduleType">Module</param>
        /// <returns>List of actions</returns>
        [HttpGet]
        [Authorize(Policy = Permissions.Actions.View)]
        public async Task<IActionResult> GetActions(string moduleType)
        {
            object command = _moduleResolver.CreateCommand(moduleType, null, ActionsTypeEnum.View);

            object response = await _mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Get available Modules
        /// </summary>
        /// <returns>List of Modules</returns>
        [HttpGet("GetModules")]
        [Authorize(Policy = Permissions.Modules.View)]
        public async Task<IActionResult> GetModules()
        {
            IResult<List<ModuleResponse>> modules = await _eventService.GetAllModulesAsync();

            return Ok(modules);
        }

        /// <summary>
        /// Get detailed information about Module
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns>Module</returns>
        [HttpGet("GetModule")]
        [Authorize(Policy = Permissions.Modules.View)]
        public async Task<IActionResult> GetModule(Guid moduleId)
        {
            IResult<ModuleResponse> module = await _eventService.GetModuleAsync(moduleId);

            return Ok(module);
        }
    }
}
