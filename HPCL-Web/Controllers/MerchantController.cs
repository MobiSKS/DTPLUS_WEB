using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class MerchantController : Controller
    {
        public IActionResult ViewPorfile()
        {
            return View();
        }
        public IActionResult CreateMerchant()
        {
            return View();
        }
    }
}
