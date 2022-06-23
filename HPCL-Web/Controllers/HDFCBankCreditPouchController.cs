using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.HDFCBankCreditPouch;
using HPCL.Common.Models.ViewModel.HDFCBankCreditPouch;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class HDFCBankCreditPouchController : Controller
    {
        private readonly IHDFCBankCreditPouchService _hDFCBankCreditPouchService;

        public HDFCBankCreditPouchController(IHDFCBankCreditPouchService hDFCBankCreditPouchService)
        {
            _hDFCBankCreditPouchService = hDFCBankCreditPouchService;
        }
        public IActionResult Index()
        {
            return View();  
        }

        public IActionResult ExceptionRequestToAddCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ExceptionRequestToAddCustomer(CustomerDetailsReq entity)
        {
            var searchList = await _hDFCBankCreditPouchService.GetCustomerDetails(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> CheckPlan(string amount)
        {
            var searchList = await _hDFCBankCreditPouchService.GetPlan(amount);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> EnrollExceptionRequest(string enrollList)
        {
            EnrollExceptionRequest[] arrs = JsonConvert.DeserializeObject<EnrollExceptionRequest[]>(enrollList);

            var entity = new EnrollExceptionRequest
            {
                CustomerId = arrs[0].CustomerId,
                FuleConsumptionCapacity = arrs[0].FuleConsumptionCapacity,
                PlanTypeId = arrs[0].PlanTypeId,
                ReferenceNo = arrs[0].ReferenceNo,
                MoComment = arrs[0].MoComment,
                RequestedBy = arrs[0].RequestedBy
            };
            var reasonList = await _hDFCBankCreditPouchService.InsertExceptionRequest(entity);
            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }

        public IActionResult RequestApproval()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RequestApproval(SearchRequestApprovalClone entity)
        {
            var searchList = await _hDFCBankCreditPouchService.SearchRequestApproval(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> SubmitRequestApproval(string bankEntryDetail)
        {
            var reasonList = await _hDFCBankCreditPouchService.SubmitRequestApproval(bankEntryDetail);
            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }

        public IActionResult EnrollmentStatus()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> EnrollmentStatus(SearchEnrollStatusClone entity)
        {
            var searchList = await _hDFCBankCreditPouchService.GetEnrollStatus(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> GetEnrollStatusReport(string customerId, int requestId)
        {
            var searchList = await _hDFCBankCreditPouchService.GetEnrollStatusReport(customerId, requestId);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        public IActionResult CCMSRechargeThroughCreditPouch()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CCMSRechargeThroughCreditPouch(string customerId, string amount)
        {
            var searchList = await _hDFCBankCreditPouchService.CCMSRechargeHDFC(customerId, amount);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        public async Task<IActionResult> HdfcRedirectToPaymentGateway(string inputTxtValues)
        {
            CcmsRechargeHdfcResData arrs = JsonConvert.DeserializeObject<CcmsRechargeHdfcResData>(inputTxtValues);

            HttpContext.Session.SetString("hdfcpgurl", arrs.response.apiurl);
            HttpContext.Session.SetString("hdfcreqhash", arrs.response.request_Hash);
            HttpContext.Session.SetString("hdfcaccesscode", arrs.response.accessCode);

            return View();
        }

        public IActionResult RequestAuthorization()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RequestAuthorization(GetRequestAuthorizationReq entity)
        {
            var searchList = await _hDFCBankCreditPouchService.GetRequestAuthorizationDetails(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> AuthorizationAction(string authReq)
        {
            var reasonList = await _hDFCBankCreditPouchService.AuthorizationAction(authReq);
            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }
    }
}
