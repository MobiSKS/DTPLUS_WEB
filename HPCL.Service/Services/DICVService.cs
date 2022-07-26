using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.RequestModel.DICV;
using HPCL.Common.Models.RequestModel.Merchant;
using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.DICV;
using HPCL.Common.Models.ResponseModel.VolvoEicher;
using HPCL.Common.Models.ViewModel.DICV;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class DICVService: IDICVService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public DICVService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }
        public async Task<List<OfficerTypeResponseModal>> GetDICVOfficerTypeList()
        {
            var officerTypeForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress")
            };

            StringContent officerTypeContent = new StringContent(JsonConvert.SerializeObject(officerTypeForms), Encoding.UTF8, "application/json");

            var officerTypeResponse = await _requestService.CommonRequestService(officerTypeContent, WebApiUrl.getDicvOfficerType);

            JObject officerTypeObj = JObject.Parse(JsonConvert.DeserializeObject(officerTypeResponse).ToString());
            var officerTypeJarr = officerTypeObj["Data"].Value<JArray>();
            List<OfficerTypeResponseModal> officerTypeLst = officerTypeJarr.ToObject<List<OfficerTypeResponseModal>>();

            return officerTypeLst;
        }
        public async Task<DICVDealerEnrollmentModel> DICVDealerEnrollment()
        {
            DICVDealerEnrollmentModel model = new DICVDealerEnrollmentModel();
            List<OfficerTypeResponseModal> officerTypeLst = new List<OfficerTypeResponseModal>();
            officerTypeLst = await GetDICVOfficerTypeList();
            officerTypeLst.Insert(0, new OfficerTypeResponseModal
            {
                OfficerTypeID = 0,
                OfficerTypeName = "--Select--"
            });

            model.OfficerTypes.AddRange(officerTypeLst);

            return model;
        }
        public async Task<InsertResponse> InsertDICVDealerEnrollment(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<DICVDealerEnrollmentModel> arrs = objs.ToObject<List<DICVDealerEnrollmentModel>>();
            var email = "";

            email = arrs[0].EmailId.ToLower();

            var insertServiceBody = new DICVDealerEnrollmentModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                DealerCode = arrs[0].DealerCode,
                DealerName = arrs[0].DealerName,
                ZonalOfficeId = arrs[0].ZonalOfficeId,
                RegionalOfficeId = arrs[0].RegionalOfficeId,
                Address1 = arrs[0].Address1,
                Address2 = arrs[0].Address2,
                Address3 = arrs[0].Address3,
                StateId = arrs[0].StateId,
                CityName = arrs[0].CityName,
                DistrictId = arrs[0].DistrictId,
                Pin = arrs[0].Pin,
                MobileNo = arrs[0].MobileNo,
                EmailId = email,
                OfficerType = arrs[0].OfficerType,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(insertServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDicvDealerEnrollment);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<SearchAlResult> SearchDICVDealer(string dealerCode, string dtpCode, string OfficerType)
        {
            var searchBody = new SearchDealerRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                DealerCode = dealerCode,
                DTPDealerCode = dtpCode,
                OfficerType = OfficerType
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDicvDealerDetail);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            SearchAlResult searchList = obj.ToObject<SearchAlResult>();

            if (searchList != null && searchList.data != null && searchList.data.Count > 0)
            {
                foreach (ALList item in searchList.data)
                {
                    item.ZOfficeID = item.ZonalOfficeID.ToString();
                    item.ROfficeID = item.RegionalOfficeID.ToString();
                    item.SId = item.StateId.ToString();
                    item.DId = item.DistrictId.ToString();
                    item.OTypeId = item.officerType.ToString();
                }
            }

            return searchList;
        }
        public async Task<InsertResponse> DICVDealerEnrollmentUpdate(string getAllData)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(getAllData).ToString());
            List<DICVDealerEnrollmentModel> arrs = objs.ToObject<List<DICVDealerEnrollmentModel>>();

            var email = "";

            email = arrs[0].EmailId.ToLower();

            var insertServiceBody = new DICVDealerEnrollmentModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                DealerCode = arrs[0].DealerCode,
                ZonalOfficeId = arrs[0].ZonalOfficeId,
                RegionalOfficeId = arrs[0].RegionalOfficeId,
                Address1 = arrs[0].Address1,
                Address2 = arrs[0].Address2,
                Address3 = arrs[0].Address3,
                StateId = arrs[0].StateId,
                CityName = arrs[0].CityName,
                DistrictId = arrs[0].DistrictId,
                Pin = arrs[0].Pin,
                MobileNo = arrs[0].MobileNo,
                EmailId = email,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(insertServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateDicvDealerEnrollment);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<CommonResponseData> CheckDICVDealerCodeExists(string DealerCode)
        {
            CommonResponseData responseData = new CommonResponseData();

            VerifyDealerRequestModel requestinfo = new VerifyDealerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.checkDicvDealerCode);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CommonResponseData> searchList = jarr.ToObject<List<CommonResponseData>>();
            responseData = searchList[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            return responseData;
        }
        public async Task<DICVOTCCardRequestModel> DICVDealerOTCCardRequest()
        {
            var model = new DICVOTCCardRequestModel();
            model.Remarks = "";
            return model;
        }
        public async Task<GetDICVBalanceOTCCardResponse> CheckDICVDealerBalanceQty(string DealerCode)
        {
            GetDICVBalanceOTCCardResponse responseData = new GetDICVBalanceOTCCardResponse();

            VerifyDealerRequestModel requestinfo = new VerifyDealerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDicvBalanceOtcCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<GetDICVBalanceOTCCardResponse> searchList = jarr.ToObject<List<GetDICVBalanceOTCCardResponse>>();
            responseData = searchList[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            if (responseData != null && responseData.Status == 1)
            {
                Int32 totalCard = Convert.ToInt32(string.IsNullOrEmpty(responseData.TotalCard) ? "0" : responseData.TotalCard);
                //Int32 balanceCard = Convert.ToInt32(string.IsNullOrEmpty(responseData.BalanceCard) ? "0" : responseData.BalanceCard);
                //if ((totalCard - balanceCard) >= 0)
                //    responseData.BalanceRequestCard = (totalCard - balanceCard).ToString();
                //else
                //    responseData.BalanceRequestCard = "0";
                responseData.BalanceRequestCard = responseData.BalanceCard;
            }
            else
            {
                responseData.BalanceRequestCard = "0";
            }

            return responseData;
        }
        public async Task<DICVOTCCardRequestModel> DICVDealerOTCCardRequest(DICVOTCCardRequestModel model)
        {
            model.UserAgent = CommonBase.useragent;
            model.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDealerWiseDicvOtcCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            model.Internel_Status_Code = customerResponse.Internel_Status_Code;
            model.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                    model.Remarks = customerResponse.Data[0].Reason;
                else
                    model.Remarks = customerResponse.Message;
            }
            else
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                    model.Remarks = customerResponse.Data[0].Reason;
            }

            return model;
        }
        public async Task<DICVCustomerEnrollmentModel> DICVCustomerEnrollment()
        {
            DICVCustomerEnrollmentModel model = new DICVCustomerEnrollmentModel();
            model.Remarks = "";
            model.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
            if (string.IsNullOrEmpty(model.ExternalPANAPIStatus))
            {
                model.ExternalPANAPIStatus = "Y";
            }
            model.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
            model.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());
            return model;
        }
        public async Task<DICVCustomerEnrollmentModel> DICVCustomerEnrollment(DICVCustomerEnrollmentModel customerModel)
        {
            customerModel.UserAgent = CommonBase.useragent;
            customerModel.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            customerModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            customerModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            customerModel.CommunicationPhoneNo = (string.IsNullOrEmpty(customerModel.CommunicationDialCode) ? "" : customerModel.CommunicationDialCode) + "-" + (string.IsNullOrEmpty(customerModel.CommunicationPhonePart2) ? "" : customerModel.CommunicationPhonePart2);
            if (!string.IsNullOrEmpty(customerModel.CommunicationEmailid))
            {
                customerModel.CommunicationEmailid = customerModel.CommunicationEmailid.ToLower();
            }
            foreach (DICVCardEntryDetails cardDetails in customerModel.ObjDICVCardEntryDetail)
            {
                if (!string.IsNullOrEmpty(cardDetails.VechileNo))
                {
                    cardDetails.VechileNo = cardDetails.VechileNo.ToUpper();
                }
                else
                {
                    cardDetails.VechileNo = "";
                }
                if (string.IsNullOrEmpty(cardDetails.MobileNo))
                {
                    cardDetails.MobileNo = "";
                }
            }

            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(customerModel.CreatedBy), "CreatedBy");
            form.Add(new StringContent(customerModel.IndividualOrgName), "IndividualOrgName");
            form.Add(new StringContent(customerModel.IndividualOrgNameTitle), "IndividualOrgNameTitle");
            form.Add(new StringContent(customerModel.NameOnCard), "NameOnCard");
            form.Add(new StringContent(customerModel.CommunicationAddress1), "CommunicationAddress1");
            form.Add(new StringContent(customerModel.CommunicationAddress2), "CommunicationAddress2");
            form.Add(new StringContent(customerModel.CommunicationCityName), "CommunicationCityName");
            form.Add(new StringContent(customerModel.CommunicationPincode), "CommunicationPincode");
            form.Add(new StringContent(customerModel.CommunicationStateId.ToString()), "CommunicationStateId");
            form.Add(new StringContent(customerModel.CommunicationDistrictId.ToString()), "CommunicationDistrictId");
            form.Add(new StringContent(string.IsNullOrEmpty(customerModel.CommunicationPhoneNo) ? "" : customerModel.CommunicationPhoneNo), "CommunicationPhoneNo");
            form.Add(new StringContent(string.IsNullOrEmpty(customerModel.CommunicationPhoneNo) ? "" : customerModel.CommunicationPhoneNo), "CommunicationFax");
            form.Add(new StringContent(customerModel.CommunicationMobileNo), "CommunicationMobileNo");
            form.Add(new StringContent(customerModel.CommunicationEmailid), "CommunicationEmailid");
            form.Add(new StringContent("Y"), "CopyofDriverLicense");
            form.Add(new StringContent("Y"), "CopyofVehicleRegistrationCertificate");
            form.Add(new StringContent(customerModel.DealerCode), "DealerCode");
            form.Add(new StringContent(string.IsNullOrEmpty(customerModel.SalesExecutiveEmployeeID) ? "" : customerModel.SalesExecutiveEmployeeID), "SalesExecutiveEmployeeID");
            form.Add(new StreamContent(customerModel.AddressProof.OpenReadStream()), "AddressProof", customerModel.AddressProof.FileName);
            form.Add(new StreamContent(customerModel.IDProof.OpenReadStream()), "IDProof", customerModel.IDProof.FileName);
            form.Add(new StreamContent(customerModel.PanCardProof.OpenReadStream()), "PanCardProof", customerModel.PanCardProof.FileName);
            form.Add(new StringContent(string.IsNullOrEmpty(customerModel.PanCardNumber) ? "" : customerModel.PanCardNumber), "PanCardNumber");

            foreach (DICVCardEntryDetails item in customerModel.ObjDICVCardEntryDetail)
            {
                form.Add(new StringContent(item.VechileNo.ToString()), "VechileNo");
                form.Add(new StringContent(item.CardNo.ToString()), "CardNo");
                form.Add(new StringContent(item.MobileNo.ToString()), "MobileNo");
                form.Add(new StringContent(item.VehicleType.ToString()), "VehicleType");
                form.Add(new StringContent(item.VINNumber.ToString()), "VINNumber");
                form.Add(new StreamContent(item.RCProof.OpenReadStream()), "RcCopyProof", item.RCProof.FileName);
            }

            form.Add(new StringContent(customerModel.UserId), "Userid");
            form.Add(new StringContent(customerModel.UserAgent), "Useragent");
            form.Add(new StringContent(customerModel.UserIp), "Userip");

            var response = await _requestService.FormDataRequestService(form, WebApiUrl.insertDicvCustomer);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            customerModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            customerModel.Remarks = customerResponse.Message;

            foreach (DICVCardEntryDetails cardDetails in customerModel.ObjDICVCardEntryDetail)
            {
                cardDetails.VehicleNoMsg = "";
                cardDetails.MobileNoMsg = "";
                cardDetails.CardNoMsg = "";
                cardDetails.VINNoMsg = "";
            }
            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                    customerModel.Remarks = customerResponse.Data[0].Reason;
                else
                    customerModel.Remarks = customerResponse.Message;
                customerModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                customerModel.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(customerModel.CommunicationStateId));
                customerModel.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());
            }
            else
            {
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                    customerModel.Remarks = customerResponse.Data[0].Reason;
            }

            return customerModel;
        }
        public async Task<DICVCustomerEnrollmentModel> GetDICVCustomerOTCCardPartialView([FromBody] List<DICVCardEntryDetails> arrs)
        {
            DICVCustomerEnrollmentModel addAddOnCard = new DICVCustomerEnrollmentModel();

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            addAddOnCard.NoOfCards = arrs[0].NoOfCards;
            addAddOnCard.DealerCode = arrs[0].DealerCode;

            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].CardNo))))
                addAddOnCard.ObjDICVCardEntryDetail = arrs;

            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();

            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            return addAddOnCard;
        }
        public async Task<List<DICVOTCCardDetails>> GetAvailableDICVOTCCardForDealer(string DealerCode)
        {
            var requestinfo = new VerifyDealerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAvailityDicvOtcCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<DICVOTCCardDetails> searchList = jarr.ToObject<List<DICVOTCCardDetails>>();

            return searchList;
        }
        public async Task<GetSalesExecutiveEmployeeIDResponse> GetDICVSalesExeEmpId(string dealerCode)
        {
            GetSalesExecutiveEmployeeIDResponse customerCardInfo = new GetSalesExecutiveEmployeeIDResponse();

            var requestInfo = new VerifyDealerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = dealerCode
            };

            StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(requestInfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getDicvSalesExeEmpidAddonOtcCardMapping);

            SalesExecutiveEmployeeIDResponse salesExecutiveEmployeeIDResponse = JsonConvert.DeserializeObject<SalesExecutiveEmployeeIDResponse>(response);


            if (salesExecutiveEmployeeIDResponse.Internel_Status_Code == 1000)
            {
                customerCardInfo.SalesExecutiveEmployeeID = salesExecutiveEmployeeIDResponse.Data[0].SalesExecutiveEmployeeID;
                customerCardInfo.StatusCode = salesExecutiveEmployeeIDResponse.Internel_Status_Code;
            }
            else
            {
                if (salesExecutiveEmployeeIDResponse.Data.Count > 0)
                {
                    customerCardInfo.Reason = salesExecutiveEmployeeIDResponse.Data[0].Reason;
                    customerCardInfo.SalesExecutiveEmployeeID = "";
                }
                else
                {
                    customerCardInfo.Reason = "";
                    customerCardInfo.SalesExecutiveEmployeeID = "";
                }
                customerCardInfo.StatusCode = salesExecutiveEmployeeIDResponse.Internel_Status_Code;
            }
            return customerCardInfo;
        }
        public async Task<DICVHotlistorReactivateViewModel> DICVHotlistAndReactivate()
        {
            DICVHotlistorReactivateViewModel model = new DICVHotlistorReactivateViewModel();

            var entitytype = await _commonActionService.GetEntityTypeList();

            List<HotlistEntity> newlist = new List<HotlistEntity>();

            foreach (HotlistEntity entity in entitytype)
            {
                if (entity.EntityId == 1 || entity.EntityId == 3)
                {
                    newlist.Add(entity);
                }
            }

            model.HotlistEntity.AddRange(newlist);

            return model;
        }
        public async Task<List<DICVHotlistReason>> GetReasonListForEntities(string EntityTypeId)
        {
            var forms = new DICVHotlistRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                EntityTypeId = EntityTypeId != "" ? Convert.ToInt32(EntityTypeId) : 0
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.dicvHotlistReason);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<DICVHotlistReason> HotlistReason = jarr.ToObject<List<DICVHotlistReason>>();
            var sortedtList = HotlistReason.OrderBy(x => x.ReasonId).ToList();
            return sortedtList;
        }

    }
}