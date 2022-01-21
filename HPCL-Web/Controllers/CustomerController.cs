using HPCL_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> OnlineForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OnlineForm(CustomerModel cust)
        {
            return View();
        }
    }
}
