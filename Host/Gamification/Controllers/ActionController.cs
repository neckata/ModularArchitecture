using Gamification.DTOs.Actions;
using Gamification.Shared.Core;
using Gamification.Shared.Core.Constants;
using Gamification.Shared.Core.Enums;
using Gamification.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gamification.Controllers
{
    [ApiVersion("1")]
    public class ActionController : CommonBaseController
    {
        private readonly IConnectorFactory _connectorFactory;

        public ActionController(IConnectorFactory connectorFactory)
        {
            _connectorFactory = connectorFactory;
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Actions.View)]
        public async Task<IActionResult> CreateActionAsync(ConnectorTypeEnum connector, CreateActionRequest request)
        {
            var connectorObject = _connectorFactory.CreateFor(connector);

            await connectorObject.CreateActionAsync(request);

            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Actions.Edit)]
        public async Task<IActionResult> UpdateActionAsync(ConnectorTypeEnum connector, UpdateActionRequest request)
        {
            var connectorObject = _connectorFactory.CreateFor(connector);

            await connectorObject.UpdateActionAsync(request);

            return Ok();
        }
    }
}
