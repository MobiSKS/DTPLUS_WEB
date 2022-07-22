using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ICICIBankCreditPouch;
using HPCL.Common.Models.ViewModel.ICICIBankCreditPouch;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class ICICIBankCreditPouchController : Controller
    {
        private readonly IICICIBankCreditPouchService _iCICIBankCreditPouchService;
        private readonly ICommonActionService _commonActionService;

        public ICICIBankCreditPouchController(IICICIBankCreditPouchService iCICIBankCreditPouchService, ICommonActionService commonActionService)
        {
            _iCICIBankCreditPouchService = iCICIBankCreditPouchService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View(SessionMenuModel.menuList);
        }

        public IActionResult ExceptionRequestToAddCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ExceptionRequestToAddCustomer(CustomerDetailsReq entity)
        {
            var searchList = await _iCICIBankCreditPouchService.GetCustomerDetails(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> CheckPlan(string amount)
        {
            var searchList = await _iCICIBankCreditPouchService.GetPlan(amount);
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
            var reasonList = await _iCICIBankCreditPouchService.InsertExceptionRequest(entity);
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
            var searchList = await _iCICIBankCreditPouchService.SearchRequestApproval(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> SubmitRequestApproval(string bankEntryDetail)
        {
            var reasonList = await _iCICIBankCreditPouchService.SubmitRequestApproval(bankEntryDetail);
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
            var searchList = await _iCICIBankCreditPouchService.GetEnrollStatus(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> GetEnrollStatusReport(string customerId, int requestId)
        {
            var searchList = await _iCICIBankCreditPouchService.GetEnrollStatusReport(customerId, requestId);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        public IActionResult CCMSRechargeThroughCreditPouch()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CCMSInitiateRechargeICICI(string customerId, string amount)
        {
            var checkList = await _iCICIBankCreditPouchService.CCMSInitiateRechargeICICI(customerId, amount);
            return Json(new { checkList = checkList });
        }

        [HttpPost]
        public async Task<JsonResult> CCMSRechargeThroughCreditPouch(string customerId, string amount)
        {
            var searchList = await _iCICIBankCreditPouchService.CCMSRechargeICICI(customerId, amount);
            return Json(new { searchList = searchList });
        }

        public async Task<IActionResult> IciciRedirectToPaymentGateway(string inputTxtValues)
        {
            //CcmsRechargeHdfcResData arrs = JsonConvert.DeserializeObject<CcmsRechargeHdfcResData>(inputTxtValues);

            //HttpContext.Session.SetString("hdfcpgurl", arrs.response.apiurl);
            //HttpContext.Session.SetString("hdfcreqhash", arrs.response.request_Hash);
            //HttpContext.Session.SetString("hdfcaccesscode", arrs.response.accessCode);

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
            var searchList = await _iCICIBankCreditPouchService.GetRequestAuthorizationDetails(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> AuthorizationAction(string authReq)
        {
            var reasonList = await _iCICIBankCreditPouchService.AuthorizationAction(authReq);
            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }

        public IActionResult RequestToAvail()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CheckEligibility(CheckEligibleReq entity)
        {
            var searchList = await _iCICIBankCreditPouchService.CheckEligibility(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> ReqAvailEnroll(string customerId, string planTypeId)
        {
            var searchList = await _iCICIBankCreditPouchService.ReqAvailEnroll(customerId, planTypeId);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }
    }
}
