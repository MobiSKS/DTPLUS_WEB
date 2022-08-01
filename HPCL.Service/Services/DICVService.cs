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
        public async Task<List<string>> ApplyHotlistorReactivate([FromBody] DICVHotlistorReactivateViewModel hotlistorReactivateViewModel)
        {
            string customerId = "";
            string cardNo = "";
            if (hotlistorReactivateViewModel.EntityTypeId == "1")
                customerId = hotlistorReactivateViewModel.EntityIdVal;
            if (hotlistorReactivateViewModel.EntityTypeId == "3")
                cardNo = hotlistorReactivateViewModel.EntityIdVal;

            var hotlistRequest = new DICVHotlistingRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                EntityTypeId = hotlistorReactivateViewModel.EntityTypeId != "" ? Convert.ToInt32(hotlistorReactivateViewModel.EntityTypeId) : 0,
                CustomerId = customerId,
                CardNo = cardNo,
                ReasonId = hotlistorReactivateViewModel.ReasonId != "" ? Convert.ToInt32(hotlistorReactivateViewModel.ReasonId) : 0,
                Remarks = hotlistorReactivateViewModel.Remarks,
                RemarksOthers = hotlistorReactivateViewModel.ReasonDetails,
                ActionId = hotlistorReactivateViewModel.ActionId != "" ? Convert.ToInt32(hotlistorReactivateViewModel.ActionId) : 0,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(hotlistRequest), Encoding.UTF8, "application/json");
            var Response = await _requestService.CommonRequestService(requestContent, WebApiUrl.dicvUpdateHotlistReactivate);
            JObject ResponseObj = JObject.Parse(JsonConvert.DeserializeObject(Response).ToString());
            List<string> messageList = new List<string>();
            if (ResponseObj["Status_Code"].ToString() == "200")
            {
                var responseJarr = ResponseObj["Data"].Value<JArray>();
                List<HotlistSuccessResponse> responseList = responseJarr.ToObject<List<HotlistSuccessResponse>>();
                messageList.Add(responseList[0].Status.ToString());
                messageList.Add(responseList[0].Reason.ToString());
            }
            else
            {
                messageList.Add(ResponseObj["Message"].ToString());
            }
            return messageList;
        }
        public async Task<GetDICVCommunicationEmailResetPasswordResponse> GetDICVCommunicationEmailResetPassword(string CustomerId)
        {
            var responseData = new GetDICVCommunicationEmailResetPasswordResponse();

            var requestinfo = new GetDICVCommunicationEmailResetPasswordRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDicvCommunicationEmailResetPassword);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<GetDICVCommunicationEmailResetPasswordResponse> searchList = jarr.ToObject<List<GetDICVCommunicationEmailResetPasswordResponse>>();
            responseData = searchList[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            return responseData;
        }
        public async Task<InsertResponse> UpdateDICVCommunicationEmailResetPassword(string CustomerId, string AlternateEmailId)
        {
            var email = "";

            email = AlternateEmailId.ToLower();

            var request = new DICVCustomerResetPassword
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
                AlternateEmailId = AlternateEmailId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateDicvCommunicationEmailResetPassword);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<DICVViewCardSearch> SearchCardMapping(DICVViewCardDetails viewCardDetails)
        {
            var searchBody = new DICVViewCardDetails();
            DICVViewCardSearch viewCardSearch = new DICVViewCardSearch();
            if (viewCardDetails.Customerid != null)
            {
                searchBody = new DICVViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    Customerid = viewCardDetails.Customerid,
                    Cardno = viewCardDetails.CardNo,
                    Vehiclenumber = viewCardDetails.VechileNo,
                    Mobileno = viewCardDetails.MobileNo,
                    IsNewMapping = false

                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new DICVViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    Customerid = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    Cardno = viewCardDetails.CardNo,
                    Vehiclenumber = viewCardDetails.VechileNo,
                    Mobileno = viewCardDetails.MobileNo,
                    IsNewMapping = false
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDicvMobileAndFastagno);

            viewCardSearch = JsonConvert.DeserializeObject<DICVViewCardSearch>(response);
            return viewCardSearch;
        }
        public async Task<List<string>> UpdateCards(DICVUpdateMobileandFastagNoInCard[] limitArray)
        {
            var updateServiceBody = new DICVUpdateMobileRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                ObjUpdateMobileandFastagNoInCard = limitArray,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.dicvUpdateMobileAndFastagNoInCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<string> messageList = new List<string>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            if (updateResponse.Count > 0)
            {
                messageList.Add(updateResponse[0].Status.ToString());
                foreach (var item in updateResponse)
                    messageList.Add(item.Reason);
            }

            return messageList;
        }
        public async Task<DICVViewCardSearch> AddCardMappingDetails(DICVViewCardDetails viewCardDetails)
        {
            var searchBody = new DICVViewCardDetails();
            DICVViewCardSearch viewCardSearch = new DICVViewCardSearch();
            if (viewCardDetails.Customerid != null)
            {
                searchBody = new DICVViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    Customerid = viewCardDetails.Customerid,
                    Cardno = viewCardDetails.CardNo,
                    Vehiclenumber = viewCardDetails.VechileNo,
                    Mobileno = viewCardDetails.MobileNo,
                    IsNewMapping = true
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDicvMobileAndFastagno);

            viewCardSearch = JsonConvert.DeserializeObject<DICVViewCardSearch>(response);
            return viewCardSearch;
        }
        public async Task<InsertResponse> ResetDICVDealerPassword(string UserName)
        {
            var requestBody = new UpdateDICVDealerPasswordReset
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserName = UserName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateDicvDealerEmailResetPassword);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<InsertResponse> EnableDisableDICVDealer(string DealerCode, string OfficerType, string EnableDisableFlag)
        {
            bool flag = false;

            if(EnableDisableFlag.ToUpper()== "ENABLED")
            {
                flag = true;
            }

            var requestBody = new EnableDisableDICVDealerRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                DealerCode = DealerCode,
                OfficerType = OfficerType,
                IsDisable = flag
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.enableDisableDicvDealer);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<DICVSearchManageCards> DICVManageCards(DICVCustomerCards entity, string editFlag)
        {
            var searchBody = new DICVCustomerCards();

            var UserName = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            if (entity.CustomerId != null || entity.CardNo != null)
            {
                searchBody = new DICVCustomerCards
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerId = entity.CustomerId,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo,
                    VehicleNumber = entity.VehicleNumber,
                    StatusFlag = entity.StatusFlag
                };
                _httpContextAccessor.HttpContext.Session.SetString("viewUpdatedGrid", JsonConvert.SerializeObject(searchBody));
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new DICVCustomerCards
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    StatusFlag = -1,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo,
                    VehicleNumber = entity.VehicleNumber,
                };
            }
            else if (editFlag == "edit" && _httpContextAccessor.HttpContext.Session.GetString("LoginType") != "Customer")
            {
                var str = _httpContextAccessor.HttpContext.Session.GetString("viewUpdatedGrid");

                DICVCustomerCards vGrid = JsonConvert.DeserializeObject<DICVCustomerCards>(str);

                searchBody = new DICVCustomerCards
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerId = vGrid.CustomerId,
                    CardNo = vGrid.CardNo,
                    MobileNo = vGrid.MobileNo,
                    VehicleNumber = vGrid.VehicleNumber,
                    StatusFlag = vGrid.StatusFlag
                };
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.searchDicvManageCard);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            DICVSearchManageCards searchList = obj.ToObject<DICVSearchManageCards>();
            return searchList;
        }
        public async Task<DICVSearchDetailsByCardId> DICVViewCardDetails(string CardId)
        {
            _httpContextAccessor.HttpContext.Session.SetString("CardIdSession", CardId);

            var cardDetailsBody = new DICVCardsSearch
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CardNo = CardId,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(cardDetailsBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.dicvGetCardLimitFeatures);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            DICVSearchDetailsByCardId searchRes = obj.ToObject<DICVSearchDetailsByCardId>();

            string cusId = string.Empty;
            foreach (var item in searchRes.Data.GetCardsDetailsModelOutput)
            {
                cusId = item.CustomerID;
            }
            _httpContextAccessor.HttpContext.Session.SetString("CustomerIdSession", cusId);

            return searchRes;
        }
        public async Task<DICVUpdateMobileModal> DICVCardlessMapping(string cardNumber, string mobileNumber, string LimitTypeName, string CCMSReloadSaleLimitValue)
        {
            DICVUpdateMobileModal editMobBody = new DICVUpdateMobileModal();
            editMobBody.CardNumber = cardNumber;
            editMobBody.MobileNumber = mobileNumber;
            editMobBody.LimitTypeName = LimitTypeName;
            editMobBody.CCMSReloadSaleLimitValue = CCMSReloadSaleLimitValue;

            _httpContextAccessor.HttpContext.Session.SetString("lmtType", editMobBody.LimitTypeName);
            return editMobBody;
        }

        public async Task<List<SuccessResponse>> DICVCardlessMappingUpdate(string mobNoNew, string crdNo)
        {
            var cardDetailsBody = new DICVUpdateMobile
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CardNo = crdNo,
                MobileNo = mobNoNew,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(cardDetailsBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.dicvUpdateMobileInCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse;
        }
        public async Task<DICVDealerOTCCardStatusModel> ViewDICVDealerOTCCardStatus()
        {
            DICVDealerOTCCardStatusModel model = new DICVDealerOTCCardStatusModel();
            model.Remarks = "";
            return model;
        }
        public async Task<GetDICVDealerOTCCardStatusResponse> GetDICVDealerOTCCardStatus(string DealerCode, string CardNo)
        {
            var searchBody = new GetDICVDealerOTCCardStatusRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode,
                CardNo = string.IsNullOrEmpty(CardNo) ? "" : CardNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.viewDicvDealerOtcCardStatus);

            GetDICVDealerOTCCardStatusResponse response = new GetDICVDealerOTCCardStatusResponse();

            response = JsonConvert.DeserializeObject<GetDICVDealerOTCCardStatusResponse>(ResponseContent);

            return response;
        }
        public async Task<DICVManageProfile> DICVManageProfile()
        {
            DICVManageProfile custMdl = new DICVManageProfile();

            custMdl.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            custMdl.SBUTypeID = 1;

            custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType(custMdl.SBUTypeID.ToString()));
            custMdl.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());
            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

            custMdl.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
            if (string.IsNullOrEmpty(custMdl.ExternalPANAPIStatus))
            {
                custMdl.ExternalPANAPIStatus = "Y";
            }
            custMdl.Remarks = "";

            return custMdl;
        }
        public async Task<List<DICVCustomerProfileResponse>> BindCustomerDetailsForSearch(string CardNo, string Email, string CustomerId, string MobileNo)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var searchBody = new DICVCustomerProfileSearchRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CardNo = string.IsNullOrEmpty(CardNo) ? "" : CardNo,
                    Email = string.IsNullOrEmpty(Email) ? "" : Email,
                    CustomerID = string.IsNullOrEmpty(CustomerId) ? "" : CustomerId,
                    MobileNo = string.IsNullOrEmpty(MobileNo) ? "" : MobileNo
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.getDicvCustomerDetails);

                JObject customerResponse = JObject.Parse(JsonConvert.DeserializeObject(contentString).ToString());

                var jarr = customerResponse["Data"].Value<JObject>();

                var customerResult = jarr["GetCustomerDetails"].Value<JArray>();
                List<DICVCustomerProfileResponse> customerProfileResponse = customerResult.ToObject<List<DICVCustomerProfileResponse>>();

                if (customerProfileResponse != null && customerProfileResponse.Count > 0)
                {
                    foreach (DICVCustomerProfileResponse response in customerProfileResponse)
                    {

                        #region Commented
                        //if (string.IsNullOrEmpty(response.AreaOfOperation))
                        //{
                        //    response.AreaOfOperation = "";
                        //}
                        //if (!string.IsNullOrEmpty(response.CommunicationPhoneNo))
                        //{
                        //    string[] subs = response.CommunicationPhoneNo.Split("-");

                        //    if (subs.Count() > 1)
                        //    {
                        //        response.CommunicationDialCode = subs[0].ToString();
                        //        response.CommunicationPhoneNo = subs[1].ToString();
                        //    }
                        //    else
                        //    {
                        //        response.CommunicationDialCode = "";
                        //        response.CommunicationPhoneNo = "";
                        //    }
                        //}

                        //if (!string.IsNullOrEmpty(response.CommunicationFax))
                        //{
                        //    string[] subs = response.CommunicationFax.Split("-");

                        //    if (subs.Count() > 1)
                        //    {
                        //        response.CommunicationFaxCode = subs[0].ToString();
                        //        response.CommunicationFax = subs[1].ToString();
                        //    }
                        //    else
                        //    {
                        //        response.CommunicationFaxCode = "";
                        //        response.CommunicationFax = "";
                        //    }
                        //}

                        //if (!string.IsNullOrEmpty(response.PermanentPhoneNo))
                        //{
                        //    string[] subs = response.PermanentPhoneNo.Split("-");

                        //    if (subs.Count() > 1)
                        //    {
                        //        response.PerOrRegAddressDialCode = subs[0].ToString();
                        //        response.PermanentPhoneNo = subs[1].ToString();
                        //    }
                        //    else
                        //    {
                        //        response.PerOrRegAddressDialCode = "";
                        //        response.PermanentPhoneNo = "";
                        //    }
                        //}

                        //if (!string.IsNullOrEmpty(response.PermanentFax))
                        //{
                        //    string[] subs = response.PermanentFax.Split("-");

                        //    if (subs.Count() > 1)
                        //    {
                        //        response.PermanentFaxCode = subs[0].ToString();
                        //        response.PermanentFax = subs[1].ToString();
                        //    }
                        //    else
                        //    {
                        //        response.PermanentFaxCode = "";
                        //        response.PermanentFax = "";
                        //    }
                        //}

                        //if (!string.IsNullOrEmpty(response.KeyOfficialFax))
                        //{
                        //    string[] subs = response.KeyOfficialFax.Split("-");

                        //    if (subs.Count() > 1)
                        //    {
                        //        response.KeyOffFaxCode = subs[0].ToString();
                        //        response.KeyOffFax = subs[1].ToString();
                        //    }
                        //    else
                        //    {
                        //        response.KeyOffFaxCode = "";
                        //        response.KeyOffFax = "";
                        //    }
                        //}

                        //if (!string.IsNullOrEmpty(response.KeyOfficialPhoneNo))
                        //{
                        //    string[] subs = response.KeyOfficialPhoneNo.Split("-");

                        //    if (subs.Count() > 1)
                        //    {
                        //        response.KeyOffDialCode = subs[0].ToString();
                        //        response.KeyOfficialPhoneNo = subs[1].ToString();
                        //    }
                        //    else
                        //    {
                        //        response.KeyOffDialCode = "";
                        //        response.KeyOfficialPhoneNo = "";
                        //    }
                        //}

                        //if (response.FleetSizeNoOfVechileOwnedHCV == "0")
                        //    response.FleetSizeNoOfVechileOwnedHCV = "";
                        //response.FleetSizeNoOfVechileOwnedLCV = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedLCV) ? "" : response.FleetSizeNoOfVechileOwnedLCV);
                        //if (response.FleetSizeNoOfVechileOwnedLCV == "0")
                        //    response.FleetSizeNoOfVechileOwnedLCV = "";
                        //response.FleetSizeNoOfVechileOwnedMUVSUV = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedMUVSUV) ? "" : response.FleetSizeNoOfVechileOwnedMUVSUV);
                        //if (response.FleetSizeNoOfVechileOwnedMUVSUV == "0")
                        //    response.FleetSizeNoOfVechileOwnedMUVSUV = "";
                        //response.FleetSizeNoOfVechileOwnedCarJeep = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedCarJeep) ? "" : response.FleetSizeNoOfVechileOwnedCarJeep);
                        //if (response.FleetSizeNoOfVechileOwnedCarJeep == "0")
                        //    response.FleetSizeNoOfVechileOwnedCarJeep = "";

                        //if (!string.IsNullOrEmpty(response.KeyOfficialDOA))
                        //{
                        //    if (response.KeyOfficialDOA.Contains("1900"))
                        //    {
                        //        response.KeyOfficialDOA = "";
                        //    }
                        //    if (response.KeyOfficialDOA.Contains("0001"))
                        //    {
                        //        response.KeyOfficialDOA = "";
                        //    }
                        //}

                        //if (!string.IsNullOrEmpty(response.KeyOfficialDOB))
                        //{
                        //    if (response.KeyOfficialDOB.Contains("1900"))
                        //    {
                        //        response.KeyOfficialDOB = "";
                        //    }
                        //    if (response.KeyOfficialDOB.Contains("0001"))
                        //    {
                        //        response.KeyOfficialDOB = "";
                        //    }
                        //}
                        #endregion

                        if (string.IsNullOrEmpty(response.NameOnCard))
                        {
                            response.NameOnCard = "";
                        }
                        if (!string.IsNullOrEmpty(response.DateOfApplication))
                        {
                            response.CustomerApplicationDate = response.DateOfApplication;
                        }
                        if (string.IsNullOrEmpty(response.RegionalOfficeName))
                        {
                            response.RegionalOfficeName = "";
                        }
                        if (response.FormNumber == "0")
                        {
                            response.FormNumber = "";
                        }
                        response.strSBU = response.SBUId.ToString();
                    }
                }

                return customerProfileResponse;
            }
        }
        public async Task<InsertResponse> UpdateDICVCustomerProfile(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<UpdateDICVCustomerProfileRequest> arrs = objs.ToObject<List<UpdateDICVCustomerProfileRequest>>();

            var insertServiceBody = new UpdateDICVCustomerProfileRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = arrs[0].CustomerID,
                IndividualOrgNameTitle = arrs[0].IndividualOrgNameTitle,
                IndividualOrgName = arrs[0].IndividualOrgName,
                NameOnCard = arrs[0].NameOnCard,
                CommunicationAddress1 = arrs[0].CommunicationAddress1,
                CommunicationAddress2 = arrs[0].CommunicationAddress2,
                CommunicationCityName = arrs[0].CommunicationCityName,
                CommunicationPincode = arrs[0].CommunicationPincode,
                CommunicationStateId = arrs[0].CommunicationStateId,
                CommunicationDistrictId = arrs[0].CommunicationDistrictId,
                CommunicationPhoneNo = arrs[0].CommunicationPhoneNo,
                CommunicationFax = arrs[0].CommunicationFax,
                CommunicationMobileNo = arrs[0].CommunicationMobileNo,
                CommunicationEmailid = arrs[0].CommunicationEmailid
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(insertServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.requestUpdateDicvCustomer);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<DICVCustomerBalanceInfoModel> DICVBalanceInfo()
        {
            DICVCustomerBalanceInfoModel custMdl = new DICVCustomerBalanceInfoModel();
            custMdl.Remarks = "";
            return custMdl;
        }

        public async Task<GetDICVCustomerBalanceInfoResponse> GetCustomerBalanceInfo(string CustomerID)
        {
            GetDICVCustomerBalanceInfoResponse customerBalanceInfo = new GetDICVCustomerBalanceInfoResponse();

            var Request = new DICVCustomerProfileSearchRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDicvCustomerBalanceInfo);

            customerBalanceInfo = JsonConvert.DeserializeObject<GetDICVCustomerBalanceInfoResponse>(response);

            return customerBalanceInfo;
        }

    }
}