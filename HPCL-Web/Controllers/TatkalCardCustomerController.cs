using HPCL.Common.Models.ViewModel.TatkalCardCustomer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class TatkalCardCustomerController : Controller
    {
        private readonly ITatkalCardCustomerService _tatkalCardCustomerService;
        public TatkalCardCustomerController(ITatkalCardCustomerService tatkalCardCustomerService)
        {
            _tatkalCardCustomerService = tatkalCardCustomerService;
        }

        public async Task<IActionResult> RequestForTatkalCard()
        {
            TatkalCustomerCardRequestInfo custMdl = new TatkalCustomerCardRequestInfo();
            custMdl = await _tatkalCardCustomerService.RequestForTatkalCard();

            return View(custMdl);
        }

        [HttpPost]
        public async Task<IActionResult> RequestForTatkalCard(TatkalCustomerCardRequestInfo tatkalCustomerCardRequestInfo)
        {

            TatkalCustomerCardRequestInfo request = await _tatkalCardCustomerService.RequestForTatkalCard(tatkalCustomerCardRequestInfo);

            if (request.Internel_Status_Code == 1000)
            {
                tatkalCustomerCardRequestInfo.Remarks = "";
                ViewBag.Message = "Tatkal Card add request saved successfully";
                return RedirectToAction("SuccessRedirectForTatkalCard");
            }

            return View(tatkalCustomerCardRequestInfo);
        }


        public async Task<IActionResult> SuccessRedirectForTatkalCard()
        {
            return View();
        }


    }
}
