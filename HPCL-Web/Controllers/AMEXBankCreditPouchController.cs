using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.AMEXBankCreditPouch;
using HPCL.Common.Models.ViewModel.AMEXBankCreditPouch;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class AMEXBankCreditPouchController : Controller
    {
        private readonly IAMEXBankCreditPouchService _amexXBankCreditPouchService;
        private readonly ICommonActionService _commonActionService;

        public AMEXBankCreditPouchController(IAMEXBankCreditPouchService amexXBankCreditPouchService, ICommonActionService commonActionService)
        {
            _amexXBankCreditPouchService = amexXBankCreditPouchService;
            _commonActionService = commonActionService;
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
            var searchList = await _amexXBankCreditPouchService.GetCustomerDetails(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> CheckPlan(string amount)
        {
            var searchList = await _amexXBankCreditPouchService.GetPlan(amount);
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
            var reasonList = await _amexXBankCreditPouchService.InsertExceptionRequest(entity);
            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }

        public async Task<IActionResult> RequestApproval()
        {
            SearchRequestApproval searchRequestApproval = new SearchRequestApproval();
            searchRequestApproval.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            searchRequestApproval.ZoneMdl.AddRange(await _commonActionService.GetZonalOfficeListbySBUtype("1"));

            return View(searchRequestApproval);
        }

        [HttpPost]
        public async Task<JsonResult> RequestApproval(SearchRequestApproval entity)
        {
            var searchList = await _amexXBankCreditPouchService.SearchRequestApproval(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> SubmitRequestApproval(string bankEntryDetail)
        {
            var reasonList = await _amexXBankCreditPouchService.SubmitRequestApproval(bankEntryDetail);
            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }

        public async Task<IActionResult> EnrollmentStatus()
        {
            SearchEnrollStatus entity = new SearchEnrollStatus();
            entity.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            entity.ZoneMdl.AddRange(await _commonActionService.GetZonalOfficeListbySBUtype("1"));
            return View(entity);
        }

        [HttpPost]
        public async Task<JsonResult> EnrollmentStatus(SearchEnrollStatus entity)
        {
            var searchList = await _amexXBankCreditPouchService.GetEnrollStatus(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> GetEnrollStatusReport(string customerId, int requestId)
        {
            var searchList = await _amexXBankCreditPouchService.GetEnrollStatusReport(customerId, requestId);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        public IActionResult CCMSRechargeThroughCreditPouch()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CCMSInitiateRechargeAMEX(string customerId, string amount)
        {
            var checkList = await _amexXBankCreditPouchService.CCMSInitiateRechargeAMEX(customerId, amount);
            return Json(new { checkList = checkList });
        }

        [HttpPost]
        public async Task<JsonResult> CCMSRechargeThroughCreditPouch(string customerId, string amount)
        {
            var searchList = await _amexXBankCreditPouchService.CCMSRechargeAMEX(customerId, amount);
            return Json(new { searchList = searchList });
        }

        public async Task<IActionResult> AmexRedirectToPaymentGateway(string inputTxtValues)
        {
            CcmsRechargeHdfcResData arrs = JsonConvert.DeserializeObject<CcmsRechargeHdfcResData>(inputTxtValues);

            HttpContext.Session.SetString("hdfcpgurl", arrs.response.apiurl);
            HttpContext.Session.SetString("hdfcreqhash", arrs.response.request_Hash);
            HttpContext.Session.SetString("hdfcaccesscode", arrs.response.accessCode);

            return View();
        }

        public IActionResult RequestAuthorization()
        {
            GetRequestAuthorizationReq entity = new GetRequestAuthorizationReq();
            return View(entity);
        }

        [HttpPost]
        public async Task<JsonResult> RequestAuthorization(GetRequestAuthorizationReq entity)
        {
            var searchList = await _amexXBankCreditPouchService.GetRequestAuthorizationDetails(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> AuthorizationAction(string authReq)
        {
            var reasonList = await _amexXBankCreditPouchService.AuthorizationAction(authReq);
            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }

        public IActionResult RequestAvailEnroll()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CheckEligibility(CheckEligibleReq entity)
        {
            var searchList = await _amexXBankCreditPouchService.CheckEligibility(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> ReqAvailEnroll(string customerId, string planTypeId)
        {
            var searchList = await _amexXBankCreditPouchService.ReqAvailEnroll(customerId, planTypeId);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }
    }
}
