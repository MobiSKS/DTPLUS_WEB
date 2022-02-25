using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Merchant;
using HPCL.Common.Models.ViewModel.Merchant;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class MerchantServices : IMerchantServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public MerchantServices(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }
        public async Task<string> ActionOnMerchantID([FromBody] ApproveRejectListRequestModal approvalRejectionMdl)
        {
            var approvalRejectionMerchantForms = new ApproveRejectListRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                StatusId = approvalRejectionMdl.StatusId == "Approve" ? "4" : "13",
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                ObjApprovalRejectDetail = approvalRejectionMdl.ObjApprovalRejectDetail
            };

            StringContent approvalRejectionMerchantContent = new StringContent(JsonConvert.SerializeObject(approvalRejectionMerchantForms), Encoding.UTF8, "application/json");
            var approvalRejectionMerchantResponse = await _requestService.CommonRequestService(approvalRejectionMerchantContent, WebApiUrl.approveRejectMerchant);
            JObject approvalRejectionMerchantObj = JObject.Parse(JsonConvert.DeserializeObject(approvalRejectionMerchantResponse).ToString());

            if (approvalRejectionMerchantObj["Status_Code"].ToString() == "200")
            {
                var approvalRejectionMerchantJarr = approvalRejectionMerchantObj["Data"].Value<JArray>();
                List<SuccessResponse> approvalRejectionMerchantLst = approvalRejectionMerchantJarr.ToObject<List<SuccessResponse>>();
                return approvalRejectionMerchantLst.First().Reason.ToString();
            }
            else
            {
                return approvalRejectionMerchantObj["Message"].ToString();
            }
        }

        public async Task<MerchantGetDetailsModel> CreateMerchant(string merchantIdValue, string fromDate, string toDate, string category)
        {
            MerchantGetDetailsModel merchantMdl = new MerchantGetDetailsModel();

            if (!string.IsNullOrEmpty(merchantIdValue))
            {
                var merchantDetailsForms = new GetMerchantDetailsFromMerchantIDRequestModal
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = merchantIdValue
                };

                StringContent merchantDetailsContent = new StringContent(JsonConvert.SerializeObject(merchantDetailsForms), Encoding.UTF8, "application/json");

                var merchantDetailsResponse = await _requestService.CommonRequestService(merchantDetailsContent, WebApiUrl.GetStatusTypeUrl);

                JObject merchantDetailsObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDetailsResponse).ToString());
                var merchantDetailsJarr = merchantDetailsObj["Data"].Value<JArray>();
                List<MerchantGetDetailsModel> merchantDetailsLst = merchantDetailsJarr.ToObject<List<MerchantGetDetailsModel>>();
                merchantMdl = merchantDetailsLst.First();
            }
            merchantMdl.MerchantTypes.AddRange(await _commonActionService.GetMerchantTypeList());
            merchantMdl.OutletCategories.AddRange(await _commonActionService.GetOutletCategoryList());
            merchantMdl.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            merchantMdl.RetailOutletStates.AddRange(await _commonActionService.GetStateList());
            merchantMdl.CommStates.AddRange(await _commonActionService.GetStateList());
            merchantMdl.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());

            return merchantMdl;
        }
        public async Task<string> CreateMerchant(MerchantGetDetailsModel merchantMdl)
        {
            string url;
            var merchantCreateUpdateForms = new MerchantCreateUpdateRequestModal();

            if (!String.IsNullOrEmpty(merchantMdl.MerchantId) && !String.IsNullOrEmpty(merchantMdl.RetailOutletName))
            {
                url = WebApiUrl.updateMerchant;
                merchantCreateUpdateForms = new MerchantCreateUpdateRequestModal
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = "30" + merchantMdl.ErpCode,
                    RetailOutletName = merchantMdl.RetailOutletName,
                    MerchantTypeId = merchantMdl.MerchantTypeId,
                    DealerName = merchantMdl.DealerName,
                    DealerMobileNo = merchantMdl.DealerMobileNo,
                    OutletCategoryId = merchantMdl.OutletCategoryId,
                    HighwayNo1 = merchantMdl.HighwayNo1,
                    HighwayNo2 = merchantMdl.HighwayNo2,
                    HighwayName = merchantMdl.HighwayName,
                    SBUTypeId = merchantMdl.SBUTypeId,
                    LPGCNGSale = merchantMdl.LPGCNGSale,
                    PancardNumber = merchantMdl.PancardNumber,
                    GSTNumber = merchantMdl.GSTNumber,
                    RetailOutletAddress1 = merchantMdl.RetailOutletAddress1,
                    RetailOutletAddress2 = merchantMdl.RetailOutletAddress2,
                    RetailOutletAddress3 = merchantMdl.RetailOutletAddress3,
                    RetailOutletLocation = merchantMdl.RetailOutletLocation,
                    RetailOutletCity = merchantMdl.RetailOutletCity,
                    RetailOutletStateId = merchantMdl.RetailOutletStateId,
                    RetailOutletDistrictId = merchantMdl.RetailOutletDistrictId,
                    RetailOutletPinNumber = merchantMdl.RetailOutletPinNumber,
                    RetailOutletPhoneNumber = merchantMdl.RetailOutletPhoneNumber,
                    RetailOutletFax = merchantMdl.RetailOutletFax,
                    ZonalOfficeId = merchantMdl.ZonalOfficeId,
                    RegionalOfficeId = merchantMdl.RegionalOfficeId,
                    SalesAreaId = merchantMdl.SalesAreaId,
                    ContactPersonNameFirstName = merchantMdl.ContactPersonNameFirstName,
                    ContactPersonNameMiddleName = merchantMdl.ContactPersonNameMiddleName,
                    ContactPersonNameLastName = merchantMdl.ContactPersonNameLastName,
                    MobileNo = merchantMdl.MobileNo,
                    EmailId = merchantMdl.EmailId,
                    Mics = merchantMdl.Mics,
                    CommunicationAddress1 = merchantMdl.CommunicationAddress1,
                    CommunicationAddress2 = merchantMdl.CommunicationAddress2,
                    CommunicationAddress3 = merchantMdl.CommunicationAddress3,
                    CommunicationLocation = "",
                    CommunicationCity = merchantMdl.CommunicationCity,
                    CommunicationStateId = merchantMdl.CommunicationStateId,
                    CommunicationDistrictId = merchantMdl.CommunicationDistrictId == null ? "0" : merchantMdl.CommunicationDistrictId,
                    CommunicationPinNumber = merchantMdl.CommunicationPinNumber,
                    CommunicationPhoneNumber = merchantMdl.CommunicationPhoneNumber,
                    CommunicationFax = merchantMdl.CommunicationFax,
                    ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName")
                };
            }
            else
            {
                url = WebApiUrl.insertMerchant;
                merchantCreateUpdateForms = new MerchantCreateUpdateRequestModal
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = "30" + merchantMdl.ErpCode,
                    RetailOutletName = merchantMdl.RetailOutletName,
                    MerchantTypeId = merchantMdl.MerchantTypeId,
                    DealerName = merchantMdl.DealerName,
                    DealerMobileNo = merchantMdl.DealerMobileNo,
                    OutletCategoryId = merchantMdl.OutletCategoryId,
                    HighwayNo1 = merchantMdl.HighwayNo1,
                    HighwayNo2 = merchantMdl.HighwayNo2,
                    HighwayName = merchantMdl.HighwayName,
                    SBUTypeId = merchantMdl.SBUTypeId,
                    LPGCNGSale = merchantMdl.LPGCNGSale,
                    PancardNumber = merchantMdl.PancardNumber,
                    GSTNumber = merchantMdl.GSTNumber,
                    RetailOutletAddress1 = merchantMdl.RetailOutletAddress1,
                    RetailOutletAddress2 = merchantMdl.RetailOutletAddress2,
                    RetailOutletAddress3 = merchantMdl.RetailOutletAddress3,
                    RetailOutletLocation = merchantMdl.RetailOutletLocation,
                    RetailOutletCity = merchantMdl.RetailOutletCity,
                    RetailOutletStateId = merchantMdl.RetailOutletStateId,
                    RetailOutletDistrictId = merchantMdl.RetailOutletDistrictId,
                    RetailOutletPinNumber = merchantMdl.RetailOutletPinNumber,
                    RetailOutletPhoneNumber = merchantMdl.RetailOutletPhoneNumber,
                    RetailOutletFax = merchantMdl.RetailOutletFax,
                    ZonalOfficeId = merchantMdl.ZonalOfficeId,
                    RegionalOfficeId = merchantMdl.RegionalOfficeId,
                    SalesAreaId = merchantMdl.SalesAreaId,
                    ContactPersonNameFirstName = merchantMdl.ContactPersonNameFirstName,
                    ContactPersonNameMiddleName = merchantMdl.ContactPersonNameMiddleName,
                    ContactPersonNameLastName = merchantMdl.ContactPersonNameLastName,
                    MobileNo = merchantMdl.MobileNo,
                    EmailId = merchantMdl.EmailId,
                    Mics = merchantMdl.Mics,
                    CommunicationAddress1 = merchantMdl.CommunicationAddress1,
                    CommunicationAddress2 = merchantMdl.CommunicationAddress2,
                    CommunicationAddress3 = merchantMdl.CommunicationAddress3,
                    CommunicationLocation = "",
                    CommunicationCity = merchantMdl.CommunicationCity,
                    CommunicationStateId = merchantMdl.CommunicationStateId,
                    CommunicationDistrictId = merchantMdl.CommunicationDistrictId == null ? "0" : merchantMdl.CommunicationDistrictId,
                    CommunicationPinNumber = merchantMdl.CommunicationPinNumber,
                    CommunicationPhoneNumber = merchantMdl.CommunicationPhoneNumber,
                    CommunicationFax = merchantMdl.CommunicationFax,
                    CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName")
                };
            }

            StringContent merchantCreateUpdateContent = new StringContent(JsonConvert.SerializeObject(merchantCreateUpdateForms), Encoding.UTF8, "application/json");
            var merchantCreateUpdateResponse = await _requestService.CommonRequestService(merchantCreateUpdateContent, url);
            JObject merchantCreateUpdateObj = JObject.Parse(JsonConvert.DeserializeObject(merchantCreateUpdateResponse).ToString());

            if (merchantCreateUpdateObj["Status_Code"].ToString() == "200")
            {
                var merchantCreateUpdateJarr = merchantCreateUpdateObj["Data"].Value<JArray>();
                List<SuccessResponse> merchantCreateUpdateLst = merchantCreateUpdateJarr.ToObject<List<SuccessResponse>>();
                return merchantCreateUpdateLst.First().Reason.ToString();
            }
            else
            {
                return merchantCreateUpdateObj["Message"].ToString();
            }
        }

        public async Task<MerchantApprovalModel> VerifyMerchant(MerchantApprovalModel merchaApprovalMdl)
        {
            string[] fromDateArr = merchaApprovalMdl.FromDate.Split("-");
            string[] toDateArr = merchaApprovalMdl.ToDate.Split("-");

            string fromDate = fromDateArr[2] + "-" + fromDateArr[1] + "-" + fromDateArr[0];
            string toDate = toDateArr[2] + "-" + toDateArr[1] + "-" + toDateArr[0];

            var merchantApprovalForms = new GetVerifyMerchantListRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Category = merchaApprovalMdl.CategoryID,
                FromDate = fromDate,
                ToDate = toDate
            };

            StringContent merchantApprovalContent = new StringContent(JsonConvert.SerializeObject(merchantApprovalForms), Encoding.UTF8, "application/json");

            var merchantApprovalResponse = await _requestService.CommonRequestService(merchantApprovalContent, WebApiUrl.getMerchantApprovalList);

            JObject merchantApprovalObj = JObject.Parse(JsonConvert.DeserializeObject(merchantApprovalResponse).ToString());
            var merchantApprovalJarr = merchantApprovalObj["Data"].Value<JArray>();
            List<MerchantApprovalModel> merchantApprovalLst = merchantApprovalJarr.ToObject<List<MerchantApprovalModel>>();
            merchaApprovalMdl.MerchantApprovalTableDetails.AddRange(merchantApprovalLst);
            return merchaApprovalMdl;
        }
    }
}
