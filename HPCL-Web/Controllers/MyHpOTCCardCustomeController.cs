using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class MyHpOTCCardCustomeController : Controller
    {
        public async Task<IActionResult> CustomerCardCreation()
        {
            return View();
        }
    }
}
