using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class MyHpOTCCardCustomerController : Controller
    {
        
        private readonly IMyHpOTCCardCustomerService _myHpOTCCardCustomerService;
        public MyHpOTCCardCustomerController(IMyHpOTCCardCustomerService myHpOTCCardCustomerService)
        {
            _myHpOTCCardCustomerService = myHpOTCCardCustomerService;
        }
        public async Task<IActionResult> CustomerCardCreation()
        {
            return View();
        }

        public async Task<IActionResult> RequestForOTCCard()
        {
            RequestForOTCCardModel custMdl = new RequestForOTCCardModel();
            custMdl = await _myHpOTCCardCustomerService.RequestForOTCCard();

            return View(custMdl);
        }

        [HttpPost]
        public async Task<IActionResult> RequestForOTCCard(RequestForOTCCardModel requestForOTCCardModel)
        {

            RequestForOTCCardModel request = await _myHpOTCCardCustomerService.RequestForOTCCard(requestForOTCCardModel);

            if (request.Internel_Status_Code == 1000)
            {
                requestForOTCCardModel.Remarks = "";
                ViewBag.Message = "OTC Card add request saved successfully";
                return RedirectToAction("SuccessRedirectForOTCCard");
            }

            return View(requestForOTCCardModel);
        }


        public async Task<IActionResult> SuccessRedirectForOTCCard()
        {
            return View();
        }


    }
}
