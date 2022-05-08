﻿using System.Threading.Tasks;
using ExcelUpload.Core.Interfaces;
using Gamification.DTOs.Actions;
using Gamification.Shared.Core.Constants;
using Gamification.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExcelUpload.Infrastructure.Controllers.ExcelUpload
{
    [ApiVersion("1")]
    public class ExcelUploadController : CommonBaseController
    {
        private IExcelUploadClient _excelUploadService;

        public ExcelUploadController(IExcelUploadClient excelUploadService)
        {
            _excelUploadService = excelUploadService;
        }

        [HttpPut]
        [Authorize(Policy = Permissions.ExcelUpload.Edit)]
        public async Task<IActionResult> UploadFile()
        {
            var file = new CreateActionRequest();
            return Ok(await _excelUploadService.UploadFile(file));
        }
    }
}
