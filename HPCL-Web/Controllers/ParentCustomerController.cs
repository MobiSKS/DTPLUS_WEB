using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Customer;
using HPCL.Common.Models.RequestModel.ParentCustomer;
using HPCL.Common.Models.ViewModel.ParentCustomer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public IActionResult Financial()
        {
            return View();
        }
        public IActionResult Approval()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
        public IActionResult Requests()
        {
            return View();
        }
        public async Task<IActionResult> ManageProfile(string CustomerId, string NameOnCard)
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
        public async Task<IActionResult> GetCardDetails(string CustomerId, string RequestId)
        {
            var modals = await _customerService.GetCardDetails(CustomerId, RequestId);
            return PartialView("~/Views/ParentCustomer/_GetCardandDispatchDetailsTbl.cshtml", modals);
        }
        public async Task<IActionResult> GetDispatchDetails(string CustomerId, string RequestId)
        {
            var modals = await _customerService.GetDispatchDetails(CustomerId, RequestId);
            return PartialView("~/Views/ParentCustomer/_GetCardandDispatchDetailsTbl.cshtml", modals);
        }

        public async Task<IActionResult> UpdateParentCustomer(string CustomerId, string RequestId)
        {
            var modals = await _customerService.UpdateParentCustomer(CustomerId, RequestId);
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
            modals.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            modals.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficebySBUType("1"));
            return View(modals);
        }

        public async Task<IActionResult> SearchParentCustomerRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string FormNumber, string SBUtypeId)
        {
            var modals = await _customerService.SearchParentCustomerRequestStatus(ZonalOfficeId, RegionalOfficeId, FromDate, ToDate, FormNumber, SBUtypeId);
            return PartialView("~/Views/ParentCustomer/_ParentCustomerRequestStatusTbl.cshtml", modals);
        }
        public async Task<IActionResult> SearchParentCustomerRequestStatusReport(string FormNumber, string RequestId)
        {
            var modals = await _customerService.SearchParentCustomerRequestStatusReport(FormNumber, RequestId);
            return PartialView("~/Views/ParentCustomer/_ParentCustomerStatusReportTbl.cshtml", modals);
        }

        public async Task<IActionResult> BalanceInfo(ParentCustomerBalanceInfoModel requestInfo)
        {

            ParentCustomerBalanceInfoModel customerBalanceResponse = new ParentCustomerBalanceInfoModel();
            if (requestInfo.ParentCustomerID != null && requestInfo.ParentCustomerID != "")
            {
                customerBalanceResponse = await _customerService.GetCustomerBalanceInfo(requestInfo);
                customerBalanceResponse.ParentCustomerID = requestInfo.ParentCustomerID;
                customerBalanceResponse.ChildCustomerId = requestInfo.ChildCustomerId;
                ViewBag.ParentCustomerID = requestInfo.ParentCustomerID;
            }

            return View(customerBalanceResponse);
        }
        public async Task<IActionResult> GetCustomerCardWiseBalance(string CustomerID)
        {
            var modals = await _customerService.GetCustomerCardWiseBalance(CustomerID);
            ViewBag.CustomerID = CustomerID;
            return PartialView("~/Views/ParentCustomer/_ParentCustomerCardWiseBalancesTbl.cshtml", modals);
        }
        public async Task<IActionResult> GetCCMSBalanceDetails(string CustomerID)
        {

            var modals = await _customerService.GetCCMSBalanceDetails(CustomerID);
            ViewBag.CustomerID = CustomerID;
            return PartialView("~/Views/ParentCustomer/_ParentCustomerCCMSBalanceDetails.cshtml", modals);
        }
        public async Task<IActionResult> GetCustomerDetailsByCustomerID(string CustomerID)
        {
            ParentCustomerBalanceInfoModel result = await _customerService.GetCustomerDetailsByCustomerID(CustomerID);
            return PartialView("~/Views/ParentCustomer/_CustomerSummaryView.cshtml", result);

        }
        public async Task<IActionResult> ParentCustomerTransactionDetails(ParentCustomerTransactionViewModel requestInfo)
        {

            ParentCustomerTransactionViewModel customerBalanceResponse = new ParentCustomerTransactionViewModel();
            if (requestInfo.ParentCustomerID != null && requestInfo.ParentCustomerID != "")
                customerBalanceResponse = await _customerService.ParentCustomerTransactionDetails(requestInfo);

            return View(customerBalanceResponse);
        }
        public async Task<IActionResult> ParentChildCustomerMapping(string ChildCustomerIds)
        {
            ParentChildCustomerMappingViewModel parentChildCustomerMapping = new ParentChildCustomerMappingViewModel();
            if (ChildCustomerIds != null && ChildCustomerIds != "")
            {
                parentChildCustomerMapping = JsonConvert.DeserializeObject<ParentChildCustomerMappingViewModel>(ChildCustomerIds);
            }
            return View(parentChildCustomerMapping);
        }

        public async Task<IActionResult> MapParenttoChildCustomer(string ChildCustomerIds)
        {


            ParentChildCustomerMappingRequest requestInfo = JsonConvert.DeserializeObject<ParentChildCustomerMappingRequest>(ChildCustomerIds);
            ParentChildCustomerMappingViewModel parentChildCustomerMapping = new ParentChildCustomerMappingViewModel();
            if (requestInfo.ParentCustomerId != null && requestInfo.ParentCustomerId != "")
            {
                parentChildCustomerMapping = await _customerService.ParentChildCustomerMapping(requestInfo);
                parentChildCustomerMapping.ParentCustomerId = requestInfo.ParentCustomerId;

            }
            return View(parentChildCustomerMapping);
        }

        public async Task<IActionResult> ViewParentChildTransactionDetails(string ParentCustomerID, string ChildCustomerIds)
        {

            ParentCustomerTransactionViewModel parentCustomerTransactionViewModel = new ParentCustomerTransactionViewModel();

            parentCustomerTransactionViewModel = await _customerService.ViewParentChildTransactionDetails(ParentCustomerID);
            ViewBag.SelectedChildCustomerIds = ChildCustomerIds;

            return View(parentCustomerTransactionViewModel);

        }

        [HttpPost]
        public async Task<JsonResult> ValidateChildCustomerId(string CustomerId)
        {
            var reason = await _customerService.ValidateChildCustomerId(CustomerId);
            return Json(reason);
        }
        [HttpPost]
        public async Task<JsonResult> ConfirmParentChildCustomerMapping([FromBody] ParentChildCustomerMappingRequest requestInfo)
        {
            var reason = await _customerService.ConfirmParentChildCustomerMapping(requestInfo);
            return Json(reason);
        }
        public async Task<IActionResult> GetDriveStarsDetails(string CustomerID)
        {

            var modals = await _customerService.GetDriveStarsDetails(CustomerID);
            ViewBag.CustomerID = CustomerID;
            return PartialView("~/Views/ParentCustomer/_ParentCustomerDriveStarsDetails.cshtml", modals);
        }
        [HttpPost]
        public async Task<JsonResult> ValidateParentCustomerId(string CustomerId)
        {
            var reason = await _customerService.ValidateParentCustomerId(CustomerId);
            return Json(reason);
        }
        public async Task<IActionResult> ControlCardSearch()
        {
            return View();
        }

        public async Task<IActionResult> GetCustomerControlCard(string CustomerID)
        {
            var modals = await _customerService.GetCustomerControlCard(CustomerID);
            return PartialView("~/Views/ParentCustomer/_ControlCardDetailsTbl.cshtml", modals);
        }
        public async Task<IActionResult> ControlCardPinReset()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SubmitRestPinforParentCustomer([FromBody] ParentCustomerSearchRequestModel reqModel)
        {
            var modals = await _customerService.SubmitRestPinforParentCustomer(reqModel);
            return Json(modals);
        }
        public async Task<IActionResult> PCConfigureSMSAlerts(PCConfigureSMSAlertModel reqEntity)
        {
            PCConfigureSMSAlertModel modals = new PCConfigureSMSAlertModel();
            if (reqEntity.CustomerID != null && reqEntity.CustomerID != "")
            {
                modals = await _customerService.GetPCAvailableSMSAlerts(reqEntity.CustomerID);
            }
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> ConfigureSMSAlerts([FromBody] ParentCustomerSearchRequestModel reqModel)
        {
            var modals = await _customerService.ConfigureSMSAlerts(reqModel);
            return Json(modals);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateDndSmsAlertsConfigure(string CustomerId)
        {
            var reasonList = await _customerService.UpdateDndSmsAlertsConfigure(CustomerId);
            return Json(new { reasonList = reasonList });
        }
    }
}
