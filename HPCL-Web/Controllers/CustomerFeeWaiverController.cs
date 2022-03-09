using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.CustomerFeeWaiver;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            var searchList = await _customerFeeWaiverServices.BindPendingCustomer(customerReferenceNo);

            ModelState.Clear();
            return Json(new { searchList = searchList });
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


        public IActionResult ViewCustomer(string formNumber)
        {
            _customerFeeWaiverServices.ViewCustomer(formNumber);
            return PartialView();
        }


        [HttpPost]
        public async Task<JsonResult> ViewCustomerDetails(string formNumber)
        {
            var Tuple = await _customerFeeWaiverServices.ViewCustomerDetails(formNumber);

            var customerList = Tuple.Item1;
            var UploadDocList = Tuple.Item2;

            CustomerFullDetails Customer = customerList.Where(t => t.FormNumber == formNumber).FirstOrDefault();
            return Json(new { customer = Customer, kycDetailsResult = UploadDocList });
        }
    }
}
