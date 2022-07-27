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
        public async Task<List<string>> ActionOnMerchantID([FromBody] ApproveRejectListRequestModal approvalRejectionMdl)
        {
            string url = "";
            if (approvalRejectionMdl.CategoryId == "1")
            {
                url = WebApiUrl.approveRejectMerchant;
            }
            else
            {
                url = WebApiUrl.approveRejectMerchantUpdate;
            }
            var approvalRejectionMerchantForms = new ApproveRejectListRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                StatusId = approvalRejectionMdl.StatusId == "Approve" ? "4" : "13",
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjApprovalRejectDetail = approvalRejectionMdl.ObjApprovalRejectDetail
            };

            StringContent approvalRejectionMerchantContent = new StringContent(JsonConvert.SerializeObject(approvalRejectionMerchantForms), Encoding.UTF8, "application/json");
            var approvalRejectionMerchantResponse = await _requestService.CommonRequestService(approvalRejectionMerchantContent, url);
            JObject approvalRejectionMerchantObj = JObject.Parse(JsonConvert.DeserializeObject(approvalRejectionMerchantResponse).ToString());
            List<string> messageList = new List<string>();

            if (approvalRejectionMerchantObj["Status_Code"].ToString() == "200")
            {
                var approvalRejectionMerchantJarr = approvalRejectionMerchantObj["Data"].Value<JArray>();
                List<SuccessResponse> approvalRejectionMerchantLst = approvalRejectionMerchantJarr.ToObject<List<SuccessResponse>>();

                if (approvalRejectionMerchantLst.Count > 0)
                {
                    foreach (var item in approvalRejectionMerchantLst)
                        messageList.Add(item.Reason);
                }
                return messageList;
            }
            else
            {
                 messageList.Add(approvalRejectionMerchantObj["Message"].ToString());
                return messageList;
            }
        }

        public async Task<MerchantGetDetailsModel> CreateMerchant(string merchantIdValue, string fromDate, string toDate, string category, string ERPCode, string actionFlow)
        {
            MerchantGetDetailsModel merchantMdl = new MerchantGetDetailsModel();
            if (!string.IsNullOrEmpty(merchantIdValue))
            {
                var merchantDetailsForms = new GetMerchantDetailsFromMerchantIDRequestModal
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
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
                merchantMdl.CreatedMerchant = "Y";
                merchantMdl.UpdateFlag = "Y";
            }
            else if (!string.IsNullOrEmpty(ERPCode))
            {
                var merchantDetailsForms = new GetMerchantDetailsFromMerchantIDRequestModal
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    ERPCode = ERPCode
                };

                StringContent merchantDetailsContent = new StringContent(JsonConvert.SerializeObject(merchantDetailsForms), Encoding.UTF8, "application/json");

                var merchantDetailsResponse = await _requestService.CommonRequestService(merchantDetailsContent, WebApiUrl.getMerchantbyERPCode);

                JObject merchantDetailsObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDetailsResponse).ToString());
                var merchantDetailsJarr = merchantDetailsObj["Data"].Value<JArray>();
                List<MerchantGetDetailsModel> merchantDetailsLst = merchantDetailsJarr.ToObject<List<MerchantGetDetailsModel>>();
                merchantMdl = merchantDetailsLst.First();
                merchantMdl.CreatedMerchant = "Y";
                merchantMdl.UpdateFlag = "Y";
            }

            merchantMdl.MerchantTypes.AddRange(await _commonActionService.GetMerchantTypeList());
            merchantMdl.OutletCategories.AddRange(await _commonActionService.GetOutletCategoryList());
            merchantMdl.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            if (string.IsNullOrEmpty(merchantMdl.SBUTypeId))
                merchantMdl.SBUTypeId = "1";
            merchantMdl.RetailOutletStates.AddRange(await _commonActionService.GetStateList());
            merchantMdl.CommStates.AddRange(await _commonActionService.GetStateList());
            merchantMdl.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficebySBUType(merchantMdl.SBUTypeId));

            merchantMdl.RegionalOfcIdVal = merchantMdl.RegionalOfficeId;
            merchantMdl.RetailDistrictIdVal = merchantMdl.RetailOutletDistrictId;
            merchantMdl.SalesAreaIdVal = merchantMdl.SalesAreaId;
            merchantMdl.CommDistrictIdVal = merchantMdl.CommunicationDistrictId;
            merchantMdl.OutletCategoryVal = merchantMdl.OutletCategoryId;
            merchantMdl.MerchantTypeIdVal = merchantMdl.MerchantTypeId;

            merchantMdl.Action = actionFlow;

            return merchantMdl;
        }
        public async Task<Tuple<string,string>> CreateMerchant(MerchantGetDetailsModel merchantMdl)
        {
            string url;
            if (!string.IsNullOrEmpty(merchantMdl.OutletCategoryVal))
            {
                merchantMdl.OutletCategoryId = merchantMdl.OutletCategoryVal;
            }

            var merchantCreateUpdateForms = new MerchantCreateUpdateRequestModal();

            if (merchantMdl.UpdateFlag == "Y")
            {
                url = WebApiUrl.updateMerchant;
                merchantCreateUpdateForms = new MerchantCreateUpdateRequestModal
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    ErpCode = merchantMdl.ErpCode,
                    RetailOutletName = merchantMdl.RetailOutletName,
                    MerchantTypeId = string.IsNullOrEmpty(merchantMdl.MerchantTypeIdVal) ? merchantMdl.MerchantTypeId : merchantMdl.MerchantTypeIdVal,
                    DealerName = merchantMdl.DealerName,
                    DealerMobileNo = merchantMdl.DealerMobileNo,
                    OutletCategoryId = merchantMdl.OutletCategoryId,
                    HighwayNo1 = merchantMdl.OutletCategoryId == "1" ? merchantMdl.HighwayNo1 : "",
                    HighwayNo2 = merchantMdl.OutletCategoryId == "1" ? merchantMdl.HighwayNo2 : "",
                    //HighwayNo1 = merchantMdl.HighwayNo1,
                    //HighwayNo2 =  merchantMdl.HighwayNo2,
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
                    ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    MappedMerchantId=merchantMdl.MappedMerchantId
                };
            }
            else
            {
                url = WebApiUrl.insertMerchant;
                merchantCreateUpdateForms = new MerchantCreateUpdateRequestModal
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    ErpCode = merchantMdl.ErpCode,
                    RetailOutletName = merchantMdl.RetailOutletName,
                    MerchantTypeId = string.IsNullOrEmpty(merchantMdl.MerchantTypeIdVal) ? merchantMdl.MerchantTypeId : merchantMdl.MerchantTypeIdVal,
                    DealerName = merchantMdl.DealerName,
                    MappedMerchantId = String.IsNullOrEmpty(merchantMdl.MappedMerchantId) ? "" : merchantMdl.MappedMerchantId,
                    DealerMobileNo = merchantMdl.DealerMobileNo,
                    OutletCategoryId = merchantMdl.OutletCategoryId,
                    HighwayNo1 = merchantMdl.OutletCategoryId == "1" ? merchantMdl.HighwayNo1 : "",
                    HighwayNo2 = merchantMdl.OutletCategoryId == "1" ? merchantMdl.HighwayNo2 : "",
                    //HighwayNo1 = merchantMdl.HighwayNo1,
                    //HighwayNo2 = merchantMdl.HighwayNo2,
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
                    CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    
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
            string fDate = "", tDate = "";
            if (!string.IsNullOrEmpty(merchaApprovalMdl.FromDate) && !string.IsNullOrEmpty(merchaApprovalMdl.FromDate))
            {
                fDate = await _commonActionService.changeDateFormat(merchaApprovalMdl.FromDate);
                tDate = await _commonActionService.changeDateFormat(merchaApprovalMdl.ToDate);
            }
            else
            {
                merchaApprovalMdl.FromDate =DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                merchaApprovalMdl.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var merchantApprovalForms = new GetVerifyMerchantListRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                Category = string.IsNullOrEmpty(merchaApprovalMdl.CategoryID) ? "1": merchaApprovalMdl.CategoryID,
                FromDate = fDate,
                ToDate = tDate
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
        public async Task<MerchantRejectedModel> RejectedMerchant(string FromDate, string ToDate)
        {
            string fromDate = "", toDate = "";
            MerchantRejectedModel merchantRejectedMdl = new MerchantRejectedModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                merchantRejectedMdl.FromDate = await _commonActionService.changeDateFormat(FromDate);
                merchantRejectedMdl.ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                merchantRejectedMdl.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                merchantRejectedMdl.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            var merchantApprovalForms = new GetVerifyMerchantListRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = merchantRejectedMdl.FromDate,
                ToDate = merchantRejectedMdl.ToDate
            };
            StringContent MerchantRejectedContent = new StringContent(JsonConvert.SerializeObject(merchantApprovalForms), Encoding.UTF8, "application/json");

            var merchantRejectedResponse = await _requestService.CommonRequestService(MerchantRejectedContent, WebApiUrl.getRejectedMerchant);

            JObject merchantRejectedObj = JObject.Parse(JsonConvert.DeserializeObject(merchantRejectedResponse).ToString());
            merchantRejectedMdl = JsonConvert.DeserializeObject<MerchantRejectedModel>(merchantRejectedResponse);
            var merchantRejectedJarr = merchantRejectedObj["Data"].Value<JArray>();
            List<MerchantApprovalTableModel> merchantApprovalLst = merchantRejectedJarr.ToObject<List<MerchantApprovalTableModel>>();
            merchantRejectedMdl.MerchantApprovalTableDetails.AddRange(merchantApprovalLst);
            return merchantRejectedMdl;

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
                        { "Userip", _httpContextAccessor.HttpContext.Session.GetString("IpAddress")},
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
            if (!(String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress3)) && !(String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress2)))
                merchantModel.Retail_Outlet_Address1 = merchantDetailsModel.RetailOutletAddress1 + "," + merchantDetailsModel.RetailOutletAddress2 + "," + merchantDetailsModel.RetailOutletAddress3;
            else if (String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress3) && !(String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress2)))
                merchantModel.Retail_Outlet_Address1 = merchantDetailsModel.RetailOutletAddress1 + "," + merchantDetailsModel.RetailOutletAddress2;
            else if (String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress3) && String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress2))
                merchantModel.Retail_Outlet_Address1 = merchantDetailsModel.RetailOutletAddress1;
            merchantModel.Retail_Outlet_Location = merchantDetailsModel.RetailOutletLocation;
            merchantModel.Retail_Outlet_City = merchantDetailsModel.RetailOutletCity;
            merchantModel.Retail_Outlet_State = merchantDetailsModel.RetailOutletStateName;
            merchantModel.Retail_Outlet_District = merchantDetailsModel.RetailOutletDistrictName;
            if (!(String.IsNullOrEmpty(merchantDetailsModel.RetailOutletStateName)))
                merchantModel.Retail_Outlet_District = merchantDetailsModel.RetailOutletDistrictName + "," + merchantDetailsModel.RetailOutletStateName;
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
            if (!(String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress3)) && !(String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress2)))
                merchantModel.Comm_Address1 = merchantDetailsModel.CommunicationAddress1 + "," + merchantDetailsModel.CommunicationAddress2 + "," + merchantDetailsModel.CommunicationAddress3;
            else if (String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress3) && !(String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress2)))
                merchantModel.Comm_Address1 = merchantDetailsModel.CommunicationAddress1 + "," + merchantDetailsModel.CommunicationAddress2;
            else if (String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress3) && String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress2))
                merchantModel.Comm_Address1 = merchantDetailsModel.CommunicationAddress1;
            merchantModel.Comm_City = merchantDetailsModel.CommunicationCity;
            merchantModel.Comm_State = merchantDetailsModel.CommunicationStateName;
            merchantModel.Comm_District = merchantDetailsModel.CommunicationDistrictName;
            if (!(String.IsNullOrEmpty(merchantDetailsModel.CommunicationStateName)) && !(String.IsNullOrEmpty(merchantDetailsModel.CommunicationDistrictName)))
                merchantModel.Comm_District = merchantDetailsModel.CommunicationDistrictName + "," + merchantDetailsModel.CommunicationStateName;
            else if (!(String.IsNullOrEmpty(merchantDetailsModel.CommunicationStateName)) && (String.IsNullOrEmpty(merchantDetailsModel.CommunicationDistrictName)))
                merchantModel.Comm_District = merchantDetailsModel.CommunicationStateName;
            else if (String.IsNullOrEmpty(merchantDetailsModel.CommunicationStateName) && !(String.IsNullOrEmpty(merchantDetailsModel.CommunicationDistrictName)))
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
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
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

        public async Task<RequestforReactivationViewModel> MerchantRequestForReactivation(RequestforReactivationViewModel reqEntity)
        {
            RequestforReactivationViewModel requestforReactivationModel = new RequestforReactivationViewModel();

            var Request = new MerchantReactivationRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                MerchantID = reqEntity.MerchantId==null?"":reqEntity.MerchantId,
                MerchantRO=reqEntity.RegionalOfficeId == null ? "0" : reqEntity.RegionalOfficeId,
                MerchantZO =reqEntity.ZonalOfficeId == null ? "0" : reqEntity.ZonalOfficeId,
                MerchantStatus =reqEntity.StatusId == null ? "0" : reqEntity.StatusId,
                HotlistDate =reqEntity.HotlistDate == null ? "" : reqEntity.HotlistDate,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetRequestForApprovalReactivateMerchant);

            requestforReactivationModel = JsonConvert.DeserializeObject<RequestforReactivationViewModel>(response);

            return requestforReactivationModel;
        }
    }
}
