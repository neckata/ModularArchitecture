using System.Threading.Tasks;
using Gamification.Shared.Core.Constants;
using Gamification.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Outlook.Core.Services;

namespace Outlook.Infrastructure.Controllers.ExcelUpload
{
    [ApiVersion("1")]
    public class OutlookController : CommonBaseController
    {
        private IOutlookClient _outlookService;

        public OutlookController(IOutlookClient outlookService)
        {
            _outlookService = outlookService;
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Outlook.View)]
        public async Task<IActionResult> GetActions()
        {
            return Ok(await _outlookService.GetActions());
        }
    }
}
