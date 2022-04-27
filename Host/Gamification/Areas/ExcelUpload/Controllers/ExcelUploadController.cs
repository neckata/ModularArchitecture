using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.Areas.ExcelUpload.Controllers
{
    public class ExcelUploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
