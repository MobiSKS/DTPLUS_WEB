using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Customer;
using HPCL.Common.Models.ViewModel.ParentCustomer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class ParentCustomerController : Controller
    {
        private readonly IParentCustomerService _customerService;
        private readonly ICommonActionService _commonActionService;
        public ParentCustomerController(IParentCustomerService customerService, ICommonActionService commonActionService)
        {
            _customerService = customerService;
            _commonActionService = commonActionService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Approval()
        {
            return View();
        }
        public async Task<IActionResult> ManageProfile(string CustomerId,string NameOnCard)
        {
            var modals = await _customerService.ManageProfile(CustomerId, NameOnCard);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> ManageProfile(ManageProfileViewModel cust)
        {

            var modals = await _customerService.ManageProfile(cust);

            if (cust.Internel_Status_Code == 1000)
            {
                //return RedirectToAction("SuccessRedirect", new { customerReferenceNo = modals.CustomerReferenceNo, Message = cust.Remarks });
                ViewBag.Success = cust.Remarks;
            }
            else
            {
                ViewBag.Success = "false";
            }
            return View(modals);
        }
        public async Task<IActionResult> ApproveParentCustomer(ParentCustomerApprovalModel ApprovalMdl, string reset)
        {
            var modals = await _customerService.ApproveParentCustomer(ApprovalMdl);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> ActionParentCustomerApproval([FromBody] ApproveParentCustomer approveParentCustomer)
        {
            var reason = await _customerService.ActionParentCustomerApproval(approveParentCustomer);
            return Json(reason);
        }
        public async Task<IActionResult> AuthorizeParentCustomer(ParentCustomerApprovalModel ApprovalMdl, string reset)
        {
            var modals = await _customerService.AuthorizeParentCustomer(ApprovalMdl);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> ActionParentCustomerAuthorize([FromBody] ApproveParentCustomer approveParentCustomer)
        {
            var reason = await _customerService.ActionParentCustomerAuthorize(approveParentCustomer);
            return Json(reason);
        }
        public async Task<IActionResult> GetCardDetails(string CustomerId,string RequestId)
        {
            var modals = await _customerService.GetCardDetails(CustomerId, RequestId);
            return PartialView("~/Views/ParentCustomer/_GetCardandDispatchDetailsTbl.cshtml", modals);
        }
        public async Task<IActionResult> GetDispatchDetails(string CustomerId, string RequestId)
        {
            var modals = await _customerService.GetDispatchDetails(CustomerId, RequestId);
            return PartialView("~/Views/ParentCustomer/_GetCardandDispatchDetailsTbl.cshtml", modals);
        }

        public async Task<IActionResult> UpdateParentCustomer(string CustomerId,string RequestId)
        {
            var modals = await _customerService.UpdateParentCustomer(CustomerId,RequestId);
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateParentCustomer(ManageProfileViewModel cust)
        {

            var modals = await _customerService.UpdateParentCustomer(cust);

            if (cust.Internel_Status_Code == 1000)
            {
                ViewBag.Success = cust.Remarks;
            }
            else
            {
                ViewBag.Success = "false";
            }
            return View(modals);
        }
        public async Task<IActionResult> ParentCustomerRequestStatus()
        {
            var modals = new ParentCustomerStatusReport();
            return View(modals);
        }
        
        public async Task<IActionResult> SearchParentCustomerRequestStatus(string ZonalOfficeId, string RegionalOfficeId,string FromDate, string ToDate, string FormNumber)
        {
            var modals = await _customerService.SearchParentCustomerRequestStatus(ZonalOfficeId, RegionalOfficeId, FromDate, ToDate, FormNumber);
            return PartialView("~/Views/ParentCustomer/_ParentCustomerRequestStatusTbl.cshtml", modals);
        }
        
    }
}
