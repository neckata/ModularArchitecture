using System.Threading.Tasks;
using ModularArchitecture.Shared.Core.Constants;
using ModularArchitecture.Shared.Infrastructure.Controllers;
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
        public async Task<IActionResult> GetEmails()
        {
            return Ok();
        }
    }
}
