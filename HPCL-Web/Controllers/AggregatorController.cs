using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Service.Interfaces;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using System;
using HPCL.Common.Models.ViewModel.Aggregator;

namespace HPCL_Web.Controllers
{

    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class AggregatorController : Controller
    {
        private readonly IAggregatorService _aggregatorService;
        private readonly ICommonActionService _commonActionService;
        public AggregatorController(IAggregatorService aggregatorService, ICommonActionService commonActionService)
        {
            _aggregatorService = aggregatorService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            return View();
        }
        public async Task<IActionResult> ManageAggregator(string FromDate, string ToDate)
        {
            var modals = await _aggregatorService.ManageAggregator(FromDate, ToDate);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> ManageAggregator(ManageAggregatorViewModel cust)
        {

            var modals = await _aggregatorService.ManageAggregator(cust);

            if (cust.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirect", new { customerReferenceNo = modals.CustomerReferenceNo, Message = cust.Remarks });
            }

            return View(modals);
        }
    }
}