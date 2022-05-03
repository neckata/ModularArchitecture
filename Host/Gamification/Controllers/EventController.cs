using Gamification.Shared.Core;
using Gamification.Shared.Core.Constants;
using Gamification.Shared.Core.Enums;
using Gamification.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.Controllers
{
    [ApiVersion("1")]
    public class EventController : CommonBaseController
    {
        private readonly IConnectorFactory _connectorFactory;

        public EventController(IConnectorFactory connectorFactory)
        {
            _connectorFactory = connectorFactory;
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Events.View)]
        public IActionResult GetConnector(ConnectorTypeEnum connector)
        {
            var connectorObject = _connectorFactory.CreateFor(connector);

            return Ok();
        }
    }
}
