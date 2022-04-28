using System.Threading.Tasks;
using ExcelUpload.Infrastructure.Services;
using Gamification.Shared.Core.Constants;
using Gamification.Shared.DTOs.Connector;
using Gamification.Shared.DTOs.Filters;
using Gamification.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.Controllers.ExcelUpload
{
    [ApiVersion("1")]
    public class ExcelUploadController : CommonBaseController
    {
        private IExcelUploadService _excelUploadService;

        public ExcelUploadController(IExcelUploadService excelUploadService)
        {
            _excelUploadService = excelUploadService;
        }

        [HttpPut]
        [Authorize(Policy = Permissions.ExcelUpload.Edit)]
        public async Task<IActionResult> UploadFile()
        {
            return Ok(await _excelUploadService.UploadFile());
        }
    }
}
