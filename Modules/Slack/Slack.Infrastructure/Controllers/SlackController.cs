using System.Threading.Tasks;
using Slack.Core.Interfaces;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Constants;
using ModularArchitecture.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Slack.Infrastructure.Controllers.Slack
{
    [ApiVersion("1")]
    public class SlackController : CommonBaseController
    {
        private ISlackClient _SlackService;

        public SlackController(ISlackClient SlackService)
        {
            _SlackService = SlackService;
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Slack.View)]
        public async Task<IActionResult> GetChannels()
        {
            return Ok(await _SlackService.GetChannels());
        }
    }
}
