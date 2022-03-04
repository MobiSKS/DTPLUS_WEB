using HPCL.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
