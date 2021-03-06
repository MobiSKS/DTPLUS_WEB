using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Common.Resources;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class CommonController : Controller
    {
        private readonly ICommonActionService _commonActionService;
        public CommonController(ICommonActionService commonActionService)
        {
            _commonActionService = commonActionService;
        }

        [HttpPost]
        public async Task<JsonResult> GetLimitType()
        {
            var sortedtList = await _commonActionService.GetLimitType();

            ModelState.Clear();
            return Json(new { sortedtList = sortedtList });
        }

        [HttpPost]
        public async Task<JsonResult> GetOfficerTypeDetails()
        {
            var sortedtList = await _commonActionService.GetOfficerTypeList();

            ModelState.Clear();
            return Json(sortedtList);
        }

        [HttpPost]
        public async Task<JsonResult> GetLocationDetails(string OfcrType)
        {
            if (OfcrType.Contains("1") || OfcrType.Contains("4") || OfcrType.Contains("6"))
            {
                var sortedtList = await _commonActionService.GetRegionalOfficeList("0");

                ModelState.Clear();
                return Json(sortedtList);
            }
            else if (OfcrType.Contains("3") || OfcrType.Contains("5"))
            {
                var sortedtList = await _commonActionService.GetZonalOfficeList();

                ModelState.Clear();
                return Json(sortedtList);
            }
            else
            {
                var sortedtList = await _commonActionService.GetHqList();

                ModelState.Clear();
                return Json(sortedtList);
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetDistrictDetails(string stateId)
        {
            var sortedtList = await _commonActionService.GetDistrictList(stateId);

            ModelState.Clear();
            return Json(sortedtList);
        }

        [HttpPost]
        public async Task<JsonResult> GetRegionalOfcDetails(string zonalOfcId)
        {
            var sortedtList = await _commonActionService.GetRegionalOfficeList(zonalOfcId);

            ModelState.Clear();
            return Json(sortedtList);
        }
        public async Task<JsonResult> GetSalesAreaList(string regionalOfcId)
        {
            var sortedtList = await _commonActionService.GetSalesAreaList(regionalOfcId);

            ModelState.Clear();
            return Json(sortedtList);
        }
        [HttpPost]
        public async Task<JsonResult> ValidateUserName(string userName)
        {
            var check = await _commonActionService.ValidateUserName(userName);

            ModelState.Clear();
            return Json(check);
        }

        [HttpPost]
        public async Task<JsonResult> GetStatusType(string status)
        {
            var statusTypeList = await _commonActionService.GetStatusType(status);
            return Json(new { statusTypeList = statusTypeList });
        }

        [HttpPost]
        public async Task<JsonResult> GetStateList()
        {
            var stateLst = await _commonActionService.GetStateList();
            return Json(new { stateLst = stateLst });
        }

        public async Task<JsonResult> GetzonalOfficeList()
        {
            var zonalOfficeLst = await _commonActionService.GetZonalOfficeList();

            return Json(new { zonalOfficeLst = zonalOfficeLst });
        }

        public async Task<JsonResult> ProofType()
        {
            var proofTypeList = await _commonActionService.ProofType();
            return Json(new { proofTypeList = proofTypeList });
        }

        [HttpPost]
        public async Task<JsonResult> CheckDealerCodeIsValid(string DealerCode)
        {
            var responseData = await _commonActionService.CheckDealerCodeIsValid(DealerCode);

            ModelState.Clear();

            if (responseData.Internel_Status_Code.ToString() == Constants.SuccessInternelStatusCode)
            {
                return Json(responseData);
            }
            else
            {
                return Json("Failed to load Dealer Details");
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetDistrictDetailsByState(string Stateid)
        {
            List<OfficerDistrictModel> lstDistrict = new List<OfficerDistrictModel>();
            lstDistrict = await _commonActionService.GetDistrictDetails(Stateid);
            return Json(lstDistrict);
        }
        [HttpPost]
        public async Task<JsonResult> CheckMobilNoDuplication(string MobileNo)
        {
            CustomerInserCardResponseData customerInserCardResponseData = await _commonActionService.CheckMobilNoDuplication(MobileNo);

            return Json(customerInserCardResponseData);
        }

        [HttpPost]
        public async Task<JsonResult> CheckEmailDuplication(string Emailid)
        {
            CustomerInserCardResponseData customerInserCardResponseData = await _commonActionService.CheckEmailDuplication(Emailid);

            return Json(customerInserCardResponseData);
        }
        [HttpPost]
        public async Task<JsonResult> GetVehicleTypeDetails()
        {
            List<VehicleTypeModel> sortedtList = new List<VehicleTypeModel>();
            sortedtList = await _commonActionService.GetVehicleTypeDropdown();
            return Json(new { SortedtList = sortedtList });
        }

        [HttpPost]
        public async Task<JsonResult> CheckPanNoDuplication(string PanNo)
        {
            CustomerInserCardResponseData customerInserCardResponseData = new CustomerInserCardResponseData();
            customerInserCardResponseData = await _commonActionService.CheckPanNoDuplication(PanNo);

            return Json(customerInserCardResponseData);
        }

        [HttpPost]
        public async Task<JsonResult> GetMerchantStatus()
        {
            var merchantStatusList = await _commonActionService.GetMerchantStatus();

            return Json(new { merchantStatusList = merchantStatusList });
        }

        [HttpPost]
        public async Task<JsonResult> GetTransactionType()
        {
            var SortedtList = await _commonActionService.GetTransactionTypeDropdown();

            return Json(new { SortedtList = SortedtList });
        }
        [HttpPost]
        public async Task<JsonResult> GetActionList(string EntityTypeId)
        {
            var sortedtList = await _commonActionService.GetActionList(EntityTypeId);

            ModelState.Clear();
            return Json(sortedtList);
        }
        [HttpPost]
        public async Task<JsonResult> GetReasonListForEntities(string EntityTypeId,string ActionId)
        {
            var sortedtList = await _commonActionService.GetReasonListForEntities(EntityTypeId,ActionId);

            ModelState.Clear();
            return Json(sortedtList);
        }
        [HttpPost]
        public async Task<JsonResult> CheckVehicleRegistrationValid(string RegistrationNumber)
        {
            var data = await _commonActionService.CheckVehicleRegistrationValid(RegistrationNumber);
            return new JsonResult(data);
        }

        public async Task<JsonResult> ValidateErpCode(string erpCode)
        {
            var check = await _commonActionService.ValidateErpCode(erpCode);

            ModelState.Clear();
            return Json(check);
        }

        public async Task<JsonResult> ValidateMappedMerchantId(string mappedMerchantId)
        {
            var check = await _commonActionService.ValidateMappedMerchantId(mappedMerchantId);

            ModelState.Clear();
            return Json(check);
        }

        public async Task<JsonResult> GetRbeMappingStatus()
        {
            var successRes = await _commonActionService.RbeMappingStatusService();
            return Json(new { successRes = successRes });
        }

        public async Task<JsonResult> GetCreditCloseLimitType()
        {
            var successRes = await _commonActionService.GetCreditCloseLimitType();
            return Json(successRes);
        }

        public async Task<JsonResult> GetHotlistReason()
        {
            var hotlistReasonList = await _commonActionService.GetHotlistReason();
            return Json(new { hotlistReasonList = hotlistReasonList });
        }

        public async Task<JsonResult> GetCreditPouchStatus(string pagename)
        {
            var getStatus = await _commonActionService.GetCreditPouchStatus(pagename);
            return Json(new { getStatus = getStatus });
        }
        [HttpPost]
        public async Task<JsonResult> GetZonalOfficebySBUType(string SBUTypeId)
        {
            var sortedtList = await _commonActionService.GetZonalOfficebySBUType(SBUTypeId);

            ModelState.Clear();
            return Json(sortedtList);
        }
        public async Task<JsonResult> GetSbuTypeList()
        {
            var sbuTypeResponseLst = await _commonActionService.GetSbuTypeList();

            return Json(new { sbuTypeResponseList = sbuTypeResponseLst });
        }

        [HttpPost]
        public async Task<JsonResult> GetZonalOfficeListbySBUtype(string SBUTypeId)
        {
            var sortedtList = await _commonActionService.GetZonalOfficeListbySBUtype(SBUTypeId);

            ModelState.Clear();
            return Json(sortedtList);
        }

        [HttpPost]
        public async Task<JsonResult> PostAuthForCreditPouch(string postAuthCust, string CreditPouchType)
        {
            var authStatus = await _commonActionService.PostAuthForCreditPouch(postAuthCust, CreditPouchType);

            ModelState.Clear();
            return Json(authStatus);
        }

        [HttpPost]
        public async Task<JsonResult> PostAuthForCreditPouchForParentCustomer(string postAuthCust, string CreditPouchType)
        {
            var authStatus = await _commonActionService.PostAuthForCreditPouchForParentCustomer(postAuthCust, CreditPouchType);

            ModelState.Clear();
            return Json(authStatus);
        }
    }
}
