using HPCL.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class ApplicationFormDataEntryController : Controller
    {
        [TypeFilter(typeof(SessionExpireActionFilter))]
        public IActionResult Index()
        {
            return View();
        }
    }
}
