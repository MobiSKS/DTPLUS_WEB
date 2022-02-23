using HPCL.Common.Models.ViewModel.CustomerFeeWaiver;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CustomerFeeWaiverController : Controller
    {
        private readonly ICustomerFeeWaiverServices _customerFeeWaiverServices;
        public CustomerFeeWaiverController(ICustomerFeeWaiverServices customerFeeWaiverServices)
        {
            _customerFeeWaiverServices = customerFeeWaiverServices;
        }

        public async Task<IActionResult> FeeWaiver()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> FeeWaiver(PendingCustomer entity)
        {
            var pendingList = await _customerFeeWaiverServices.FeeWaiver(entity);

            ModelState.Clear();
            return Json(new { pendingList = pendingList });
        }

        [HttpPost]
        public async Task<JsonResult> BindPendingCustomer(string customerReferenceNo)
        {
            var tuple = await _customerFeeWaiverServices.BindPendingCustomer(customerReferenceNo);
            var basicDetails = tuple.Item1;
            var cardDetails = tuple.Item2;

            ModelState.Clear();
            return Json(new { basicDetails = basicDetails, cardDetails = cardDetails });
        }

        [HttpPost]
        public async Task<JsonResult> ApproveCustomer(string CustomerReferenceNo, string comments)
        {
            var reason = await _customerFeeWaiverServices.ApproveCustomer(CustomerReferenceNo, comments);
            ModelState.Clear();
            return Json(reason);
        }

        [HttpPost]
        public async Task<JsonResult> RejectCustomer(string CustomerReferenceNo, string comments)
        {
            var reason = await _customerFeeWaiverServices.RejectCustomer(CustomerReferenceNo, comments);
            ModelState.Clear();
            return Json(reason);
        }
    }
}
