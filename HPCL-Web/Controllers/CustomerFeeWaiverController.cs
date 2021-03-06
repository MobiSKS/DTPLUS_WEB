using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.CustomerFeeWaiver;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class CustomerFeeWaiverController : Controller
    {
        private readonly ICustomerFeeWaiverServices _customerFeeWaiverServices;
        public CustomerFeeWaiverController(ICustomerFeeWaiverServices customerFeeWaiverServices)
        {
            _customerFeeWaiverServices = customerFeeWaiverServices;
        }

        //public async Task<IActionResult> FeeWaiver()
        //{
        //    PendingCustomer pendingCustomer = new PendingCustomer();
        //    return View(pendingCustomer);
        //}

        //[HttpPost]
        //public async Task<JsonResult> FeeWaiver(PendingCustomer entity)
        //{
        //    var pendingList = await _customerFeeWaiverServices.FeeWaiver(entity);

        //    ModelState.Clear();
        //    return Json(new { pendingList = pendingList });
        //}
        public async Task<IActionResult> FeeWaiver(PendingCustomer entity, string reset)
        {
            var modals = await _customerFeeWaiverServices.FeeWaiver(entity);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> BindPendingCustomer(string formNumber)
        {
            var searchList = await _customerFeeWaiverServices.BindPendingCustomer(formNumber);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> ApproveCustomer(string formNumber, string comments)
        {
            var responseMsg = await _customerFeeWaiverServices.ApproveCustomer(formNumber, comments);
            ModelState.Clear();
            return Json(new { responseMsg = responseMsg });
        }

        [HttpPost]
        public async Task<JsonResult> RejectCustomer(string formNumber, string comments)
        {
            var responseMsg = await _customerFeeWaiverServices.RejectCustomer(formNumber, comments);
            ModelState.Clear();
            return Json(new { responseMsg = responseMsg });
        }


        public IActionResult ViewCustomer(string formNumber)
        {
            _customerFeeWaiverServices.ViewCustomer(formNumber);
            return PartialView();
        }


        [HttpPost]
        public async Task<JsonResult> ViewCustomerDetails(string formNumber)
        {
            var vewCustomerAllList = await _customerFeeWaiverServices.ViewCustomerDetails(formNumber);

            return Json(new { vewCustomerAllList = vewCustomerAllList });
        }
    }
}
