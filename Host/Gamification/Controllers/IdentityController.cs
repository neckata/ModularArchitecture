using Gamification.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gamification.Controllers
{
    [ApiVersion("1")]
    public class IdentityController : CommonBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Authorize()
        {
            return Ok();
        }
    }
}
