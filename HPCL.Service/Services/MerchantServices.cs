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
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
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

        public async Task<MerchantGetDetailsModel> CreateMerchant(string merchantIdValue, string fromDate, string toDate, string category,string ERPCode, string actionFlow)
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

                var merchantDetailsResponse = await _requestService.CommonRequestService(merchantDetailsContent, WebApiUrl.getMerchantByMerchantID);

                JObject merchantDetailsObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDetailsResponse).ToString());
                if (merchantDetailsObj["Status_Code"].ToString() == "200" && merchantDetailsObj["Internel_Status_Code"].ToString() == "1001")
                {
                    merchantMdl.Message = merchantDetailsObj["Message"].ToString();
                    return merchantMdl;
                }
                else
                {
                    merchantMdl.Message = "";
                    var merchantDetailsJarr = merchantDetailsObj["Data"].Value<JArray>();
                    List<MerchantGetDetailsModel> merchantDetailsLst = merchantDetailsJarr.ToObject<List<MerchantGetDetailsModel>>();
                    merchantMdl = merchantDetailsLst.First();
                }

            }

            if (!string.IsNullOrEmpty(ERPCode))
            {
                var merchantDetailsForms = new GetMerchantDetailsFromMerchantIDRequestModal
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    ERPCode = ERPCode
                };

                StringContent merchantDetailsContent = new StringContent(JsonConvert.SerializeObject(merchantDetailsForms), Encoding.UTF8, "application/json");

                var merchantDetailsResponse = await _requestService.CommonRequestService(merchantDetailsContent, WebApiUrl.getMerchantbyERPCode);

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

            merchantMdl.RegionalOfcIdVal = merchantMdl.RegionalOfficeId;
            merchantMdl.RetailDistrictIdVal = merchantMdl.RetailOutletDistrictId;
            merchantMdl.SalesAreaIdVal = merchantMdl.SalesAreaId;

            merchantMdl.Action = actionFlow;

            return merchantMdl;
        }
        public async Task<Tuple<string,string>> CreateMerchant(MerchantGetDetailsModel merchantMdl)
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
                    ErpCode = merchantMdl.ErpCode,
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
                    RetailOutletDistrictId = merchantMdl.RetailDistrictIdVal,
                    RetailOutletPinNumber = merchantMdl.RetailOutletPinNumber,
                    RetailOutletPhoneNumber = merchantMdl.RetailOutletPhoneNumber,
                    RetailOutletFax = merchantMdl.RetailOutletFax,
                    ZonalOfficeId = merchantMdl.ZonalOfficeId,
                    RegionalOfficeId = merchantMdl.RegionalOfcIdVal,
                    SalesAreaId = merchantMdl.SalesAreaIdVal,
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
                    CommunicationDistrictId = merchantMdl.CommDistrictIdVal == null ? "0" : merchantMdl.CommDistrictIdVal,
                    CommunicationPinNumber = merchantMdl.CommunicationPinNumber,
                    CommunicationPhoneNumber = merchantMdl.CommunicationPhoneNumber,
                    CommunicationFax = merchantMdl.CommunicationFax,
                    ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
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
                    ErpCode = merchantMdl.ErpCode,
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
                    RetailOutletDistrictId = merchantMdl.RetailDistrictIdVal,
                    RetailOutletPinNumber = merchantMdl.RetailOutletPinNumber,
                    RetailOutletPhoneNumber = merchantMdl.RetailOutletPhoneNumber,
                    RetailOutletFax = merchantMdl.RetailOutletFax,
                    ZonalOfficeId = merchantMdl.ZonalOfficeId,
                    RegionalOfficeId = merchantMdl.RegionalOfcIdVal,
                    SalesAreaId = merchantMdl.SalesAreaIdVal,
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
                    CommunicationDistrictId = merchantMdl.CommDistrictIdVal == null ? "0" : merchantMdl.CommDistrictIdVal,
                    CommunicationPinNumber = merchantMdl.CommunicationPinNumber,
                    CommunicationPhoneNumber = merchantMdl.CommunicationPhoneNumber,
                    CommunicationFax = merchantMdl.CommunicationFax,
                    NoofLiveTerminals = merchantMdl.NoofLiveTerminals,
                    TerminalTypeRequested = merchantMdl.TerminalTypeRequested,
                    CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent merchantCreateUpdateContent = new StringContent(JsonConvert.SerializeObject(merchantCreateUpdateForms), Encoding.UTF8, "application/json");
            var merchantCreateUpdateResponse = await _requestService.CommonRequestService(merchantCreateUpdateContent, url);
            JObject merchantCreateUpdateObj = JObject.Parse(JsonConvert.DeserializeObject(merchantCreateUpdateResponse).ToString());

            if (merchantCreateUpdateObj["Status_Code"].ToString() == "200")
            {
                var merchantCreateUpdateJarr = merchantCreateUpdateObj["Data"].Value<JArray>();
                List<SuccessResponse> merchantCreateUpdateLst = merchantCreateUpdateJarr.ToObject<List<SuccessResponse>>();
                return Tuple.Create(merchantCreateUpdateLst.First().Status.ToString(), merchantCreateUpdateLst.First().Reason.ToString());
            }
            else
            {
                return Tuple.Create("0",merchantCreateUpdateObj["Message"].ToString());
            }
        }

        public async Task<MerchantApprovalModel> VerifyMerchant(MerchantApprovalModel merchaApprovalMdl)
        {
            //string fromDate = "", toDate = "";

            if (!string.IsNullOrEmpty(merchaApprovalMdl.FromDate) && !string.IsNullOrEmpty(merchaApprovalMdl.FromDate))
            {
                string[] fromDateArr = merchaApprovalMdl.FromDate.Split("-");
                string[] toDateArr = merchaApprovalMdl.ToDate.Split("-");

                //fromDate = fromDateArr[2] + "-" + fromDateArr[1] + "-" + fromDateArr[0];
                //toDate = toDateArr[2] + "-" + toDateArr[1] + "-" + toDateArr[0];
            }
            else
            {
                merchaApprovalMdl.FromDate = DateTime.Now.ToString();
                merchaApprovalMdl.ToDate = DateTime.Now.ToString();
                return merchaApprovalMdl;
            }

            var merchantApprovalForms = new GetVerifyMerchantListRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Category = merchaApprovalMdl.CategoryID,
                FromDate = merchaApprovalMdl.FromDate,
                ToDate = merchaApprovalMdl.ToDate
            };

            StringContent merchantApprovalContent = new StringContent(JsonConvert.SerializeObject(merchantApprovalForms), Encoding.UTF8, "application/json");

            var merchantApprovalResponse = await _requestService.CommonRequestService(merchantApprovalContent, WebApiUrl.getMerchantApprovalList);

            JObject merchantApprovalObj = JObject.Parse(JsonConvert.DeserializeObject(merchantApprovalResponse).ToString());
            var merchantApprovalJarr = merchantApprovalObj["Data"].Value<JArray>();
            List<MerchantApprovalTableModel> merchantApprovalLst = merchantApprovalJarr.ToObject<List<MerchantApprovalTableModel>>();
            merchaApprovalMdl.MerchantApprovalTableDetails.AddRange(merchantApprovalLst);
            return merchaApprovalMdl;
        }

        #region Rejected Merchants
        public async Task<MerchantApprovalModel> RejectedMerchant(MerchantApprovalModel merchaApprovalMdl)
        {
            string fromDate = "", toDate = "";

            if (!string.IsNullOrEmpty(merchaApprovalMdl.FromDate) && !string.IsNullOrEmpty(merchaApprovalMdl.FromDate))
            {
                string[] fromDateArr = merchaApprovalMdl.FromDate.Split("-");
                fromDate = fromDateArr[2] + "-" + fromDateArr[1] + "-" + fromDateArr[0];
                if (!string.IsNullOrEmpty(merchaApprovalMdl.ToDate) && !string.IsNullOrEmpty(merchaApprovalMdl.ToDate))
                {
                    string[] toDateArr = merchaApprovalMdl.ToDate.Split("-");
                    toDate = toDateArr[2] + "-" + toDateArr[1] + "-" + toDateArr[0];
                }
            }
            else
            {
                return merchaApprovalMdl;
            }

            var merchantApprovalForms = new GetVerifyMerchantListRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = fromDate,
                ToDate = toDate
            };
            StringContent MerchantRejectedContent = new StringContent(JsonConvert.SerializeObject(merchantApprovalForms), Encoding.UTF8, "application/json");

            var merchantRejectedResponse = await _requestService.CommonRequestService(MerchantRejectedContent, WebApiUrl.getRejectedMerchant);

            JObject merchantRejectedObj = JObject.Parse(JsonConvert.DeserializeObject(merchantRejectedResponse).ToString());
            merchaApprovalMdl = JsonConvert.DeserializeObject<MerchantApprovalModel>(merchantRejectedResponse);
            var merchantRejectedJarr = merchantRejectedObj["Data"].Value<JArray>();
            List<MerchantApprovalTableModel> merchantApprovalLst = merchantRejectedJarr.ToObject<List<MerchantApprovalTableModel>>();
            merchaApprovalMdl.MerchantApprovalTableDetails.AddRange(merchantApprovalLst);
            return merchaApprovalMdl;

        }
        public async Task<MerchantModel> MerchantSummary(string ERPCode, string fromDate, string toDate)
        {
            char flag = 'N';
            MerchantModel merchantModel = new MerchantModel();
            if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("ERPCode")))
            {
                merchantModel.ERPCode = _httpContextAccessor.HttpContext.Session.GetString("ERPCode");
            }


            var MerchantFormDetails = new Dictionary<string, string>
                    {
                        {"Useragent", CommonBase.useragent},
                        { "Userip", CommonBase.userip},
                        {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                        { "ErpCode", ERPCode}
                    };
            MerchantGetDetailsModel merchantDetailsModel = new MerchantGetDetailsModel();
            StringContent MerchantFormDetailsContent = new StringContent(JsonConvert.SerializeObject(MerchantFormDetails), Encoding.UTF8, "application/json");

            var merchantRejectedResponse = await _requestService.CommonRequestService(MerchantFormDetailsContent, WebApiUrl.getMerchantbyERPCode);

            JObject merchantRejectedObj = JObject.Parse(JsonConvert.DeserializeObject(merchantRejectedResponse).ToString());
            var merchantRejectedJarr = merchantRejectedObj["Data"].Value<JArray>();
            List<MerchantGetDetailsModel> dtls = merchantRejectedJarr.ToObject<List<MerchantGetDetailsModel>>();
            merchantDetailsModel = dtls.First();
            merchantModel.MerchantID = merchantDetailsModel.MerchantId;
            merchantModel.ERPCode = merchantDetailsModel.ErpCode;
            merchantModel.RetailOutletName = merchantDetailsModel.RetailOutletName;
            merchantModel.MerchantType = merchantDetailsModel.MerchantTypeName;
            merchantModel.DealerName = merchantDetailsModel.DealerName;
            merchantModel.MappedMerchantID = merchantDetailsModel.MappedMerchantId;
            merchantModel.DealerMobileNumber = merchantDetailsModel.DealerMobileNo;
            merchantModel.OutletCategory = merchantDetailsModel.OutletCategoryName;
            merchantModel.PreHighwayNumber = merchantDetailsModel.HighwayNo1;
            merchantModel.HighwayNumber = merchantDetailsModel.HighwayNo2;
            merchantModel.HighwayName = merchantDetailsModel.HighwayName;
            merchantModel.SBUType = merchantDetailsModel.SBUName;
            merchantModel.LPG_CNGSale = merchantDetailsModel.LPGCNGSale;
            merchantModel.PANCardNumber = merchantDetailsModel.PancardNumber;
            merchantModel.GSTNumber = merchantDetailsModel.GSTNumber;
            merchantModel.Retail_Outlet_Address1 = merchantDetailsModel.RetailOutletAddress1;
            merchantModel.Retail_Outlet_Address2 = merchantDetailsModel.RetailOutletAddress2;
            merchantModel.Retail_Outlet_Address3 = merchantDetailsModel.RetailOutletAddress3;
            merchantModel.Retail_Outlet_Location = merchantDetailsModel.RetailOutletLocation;
            merchantModel.Retail_Outlet_City = merchantDetailsModel.RetailOutletCity;
            merchantModel.Retail_Outlet_State = merchantDetailsModel.RetailOutletStateName;
            merchantModel.Retail_Outlet_District = merchantDetailsModel.RetailOutletDistrictName;
            merchantModel.Retail_DistictID = merchantDetailsModel.RetailOutletDistrictId;
            merchantModel.Retail_Outlet_Pin = merchantDetailsModel.RetailOutletPinNumber;
            merchantModel.ZonalOffice = merchantDetailsModel.ZonalOfficeName;
            merchantModel.ZonalOfficeID = merchantDetailsModel.ZonalOfficeId;
            merchantModel.RegionalOffice = merchantDetailsModel.RegionalOfficeName;
            merchantModel.RegionalOfcID = merchantDetailsModel.RegionalOfficeId;
            merchantModel.SalesArea = merchantDetailsModel.SalesAreaName;
            merchantModel.SalesAreaID = merchantDetailsModel.SalesAreaId;
            merchantModel.FName = merchantDetailsModel.ContactPersonNameFirstName;
            merchantModel.MName = merchantDetailsModel.ContactPersonNameMiddleName;
            merchantModel.LName = merchantDetailsModel.ContactPersonNameLastName;
            merchantModel.Mobile = merchantDetailsModel.MobileNo;
            merchantModel.Email = merchantDetailsModel.EmailId;
            merchantModel.Misc = merchantDetailsModel.Mics;
            merchantModel.Comm_Address1 = merchantDetailsModel.CommunicationAddress1;
            merchantModel.Comm_Address2 = merchantDetailsModel.CommunicationAddress2;
            merchantModel.Comm_Address3 = merchantDetailsModel.CommunicationAddress3;
            merchantModel.Comm_City = merchantDetailsModel.CommunicationCity;
            merchantModel.Comm_State = merchantDetailsModel.CommunicationStateName;
            merchantModel.Comm_District = merchantDetailsModel.CommunicationDistrictName;
            merchantModel.Comm_DistictID = merchantDetailsModel.CommunicationDistrictId;
            merchantModel.Comm_Pin = merchantDetailsModel.CommunicationPinNumber;
            merchantModel.NumOfLiveTerminals = merchantDetailsModel.NoofLiveTerminals;
            merchantModel.TerminalTypeRequested = merchantDetailsModel.TerminalTypeRequested;
            merchantModel.Retail_Outlet_PhoneOffice = merchantDetailsModel.RetailOutletPhoneNumber;
            merchantModel.Retail_Outlet_Fax = merchantDetailsModel.RetailOutletFax;
            merchantModel.Comm_PhoneNumber = merchantDetailsModel.CommunicationPhoneNumber;
            merchantModel.Comm_Fax = merchantDetailsModel.CommunicationFax;


            return merchantModel;

        }
        #endregion

        #region Search Merchant
        public async Task<SearchMerchantModel> SearchMerchant()
        {
            SearchMerchantModel searchMerchantModel = new SearchMerchantModel();
            searchMerchantModel.TerminalStatusResponseModals.AddRange(await _commonActionService.GetMerchantStatus());
            return searchMerchantModel;
        }

        public async Task<List<SearchDetailsTableModel>> SearchMerchantDetails(string merchantId, string erpCode, string retailOutletName, string city, string highwayNo, string status)
        {            
            var searchDetailsTableForms = new SearchMerchantModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                MerchantId = string.IsNullOrEmpty(merchantId) ? "" : merchantId,
                ErpCode = string.IsNullOrEmpty(erpCode) ? "" : erpCode,
                RetailOutletName = string.IsNullOrEmpty(retailOutletName) ? "" : retailOutletName,
                RetailOutletCity = string.IsNullOrEmpty(city) ? "" : city,
                HighwayNo = string.IsNullOrEmpty(highwayNo) ? "" : highwayNo,
                MerchantStatus = string.IsNullOrEmpty(status) || status == "0" ? "" : status
            };

            StringContent searchDetailsTableContent = new StringContent(JsonConvert.SerializeObject(searchDetailsTableForms), Encoding.UTF8, "application/json");

            var searchDetailsTableResponse = await _requestService.CommonRequestService(searchDetailsTableContent, WebApiUrl.searchMerchant);

            JObject searchDetailsTableObj = JObject.Parse(JsonConvert.DeserializeObject(searchDetailsTableResponse).ToString());
            var searchDetailsTableJarr = searchDetailsTableObj["Data"].Value<JArray>();
            List<SearchDetailsTableModel> searchDetailsTableModels = searchDetailsTableJarr.ToObject<List<SearchDetailsTableModel>>();
            return searchDetailsTableModels;
        }

        #endregion
    }
}
