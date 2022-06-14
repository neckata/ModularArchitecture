using System.Threading.Tasks;
using Slack.Core.Interfaces;
using ModularArchitecture.Shared.Core.Constants;
using ModularArchitecture.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Slack.Infrastructure.Controllers.Slack
{
    [ApiVersion("1")]
    public class SlackController : CommonBaseController
    {
        private ISlackClient _slackService;

        public SlackController(ISlackClient slackService)
        {
            _slackService = slackService;
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Slack.View)]
        public async Task<IActionResult> GetChannels()
        {
            return Ok(await _slackService.GetChannels());
        }
    }
}
