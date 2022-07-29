using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.RequestModel.AshokLeyLand;
using HPCL.Common.Models.RequestModel.JCB;
using HPCL.Common.Models.RequestModel.Merchant;
using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ResponseModel.JCB;
using HPCL.Common.Models.ResponseModel.VolvoEicher;
using HPCL.Common.Models.ViewModel.AshokLeyLand;
using HPCL.Common.Models.ViewModel.JCB;
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
    public class JCBService : IJCBService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public JCBService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }
        public async Task<CommonResponseData> CheckJCBDealerCodeExists(string DealerCode)
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

            var response = await _requestService.CommonRequestService(content, WebApiUrl.checkJcbDealerCode);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CommonResponseData> searchList = jarr.ToObject<List<CommonResponseData>>();
            responseData = searchList[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            return responseData;
        }
        public async Task<InsertResponse> InsertJCBDealerEnrollment(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<AlEnrollment> arrs = objs.ToObject<List<AlEnrollment>>();
            var email = "";

            email = arrs[0].EmailId.ToLower();

            var insertServiceBody = new AlEnrollment
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertJcbDealerEnrollment);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }

        public async Task<InsertResponse> JCBDealerEnrollmentUpdate(string getAllData)
        {

            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(getAllData).ToString());
            List<AlEnrollment> arrs = objs.ToObject<List<AlEnrollment>>();

            var email = "";

            email = arrs[0].EmailId.ToLower();

            var insertServiceBody = new AlEnrollment
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateJcbDealerEnrollment);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<SearchAlResult> SearchJCBDealer(string dealerCode, string dtpCode, string OfficerType)
        {
            var searchBody = new SearchDealer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                DealerCode = dealerCode,
                DTPDealerCode = dtpCode,
                OfficerType = OfficerType
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getJcbDealerDetail);

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
        public async Task<JCBOTCCardRequestModel> JCBDealerOTCCardRequest()
        {
            var model = new JCBOTCCardRequestModel();
            model.Remarks = "";
            return model;
        }
        public async Task<GetJCBBalanceOTCCardResponse> CheckJCBDealerBalanceQty(string DealerCode)
        {
            GetJCBBalanceOTCCardResponse responseData = new GetJCBBalanceOTCCardResponse();

            VerifyDealerRequestModel requestinfo = new VerifyDealerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getJcbBalanceOtcCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<GetJCBBalanceOTCCardResponse> searchList = jarr.ToObject<List<GetJCBBalanceOTCCardResponse>>();
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
        public async Task<JCBOTCCardRequestModel> JCBDealerOTCCardRequest(JCBOTCCardRequestModel model)
        {
            model.UserAgent = CommonBase.useragent;
            model.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDealerWiseJcbOtcCardRequest);


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
        public async Task<ViewJCBUnmappedOTCCardsModel> ViewJCBUnmappedOTCCards()
        {
            ViewJCBUnmappedOTCCardsModel model = new ViewJCBUnmappedOTCCardsModel();
            model.Remarks = "";
            return model;
        }

        public async Task<JCBOTCCardDealerAllocationResponse> GetViewJCBOTCCardDealerAllocation(string DealerCode, string CardNo, bool ShowUnmappedCard)
        {
            var searchBody = new GetJCBOTCCardDealerAllocationRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode,
                CardNo = string.IsNullOrEmpty(CardNo) ? "" : CardNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.viewJcbDealerOtcCardDetail);

            JCBOTCCardDealerAllocationResponse response = new JCBOTCCardDealerAllocationResponse();

            response = JsonConvert.DeserializeObject<JCBOTCCardDealerAllocationResponse>(ResponseContent);

            if (response != null)
                response.ShowUnmappedCard = ShowUnmappedCard;

            return response;
        }
        public async Task<JCBCustomerEnrollmentModel> JCBCustomerEnrollment()
        {
            JCBCustomerEnrollmentModel model = new JCBCustomerEnrollmentModel();
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
        public async Task<GetSalesExecutiveEmployeeIDResponse> GetJCBSalesExeEmpId(string dealerCode)
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

            var response = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getJcbSalesExeEmpidAddonOtcCardMapping);

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
        public async Task<JCBCustomerEnrollmentModel> GetJCBCustomerOTCCardPartialView([FromBody] List<JCBCardEntryDetails> arrs)
        {
            JCBCustomerEnrollmentModel addAddOnCard = new JCBCustomerEnrollmentModel();

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            addAddOnCard.NoOfCards = arrs[0].NoOfCards;
            addAddOnCard.DealerCode = arrs[0].DealerCode;

            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].CardNo))))
                addAddOnCard.ObjJCBCardEntryDetail = arrs;

            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();

            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            return addAddOnCard;
        }
        public async Task<List<JCBOTCCardDetails>> GetAvailableJCBOTCCardForDealer(string DealerCode)
        {
            var requestinfo = new VerifyDealerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAvailityJCBOtcCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<JCBOTCCardDetails> searchList = jarr.ToObject<List<JCBOTCCardDetails>>();

            return searchList;
        }
        public async Task<JCBCustomerEnrollmentModel> JCBCustomerEnrollment(JCBCustomerEnrollmentModel customerModel)
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
            foreach (JCBCardEntryDetails cardDetails in customerModel.ObjJCBCardEntryDetail)
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

            foreach (JCBCardEntryDetails item in customerModel.ObjJCBCardEntryDetail)
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

            var response = await _requestService.FormDataRequestService(form, WebApiUrl.insertJcbCustomer);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            customerModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            customerModel.Remarks = customerResponse.Message;

            foreach (JCBCardEntryDetails cardDetails in customerModel.ObjJCBCardEntryDetail)
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

        public async Task<JCBManageProfile> JCBManageProfile()
        {
            JCBManageProfile custMdl = new JCBManageProfile();

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

            return custMdl;
        }
        public async Task<List<JCBCustomerProfileResponse>> BindCustomerDetailsForSearch(string CustomerId, string NameOnCard)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {

                var searchBody = new CustomerProfileModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerId = string.IsNullOrEmpty(CustomerId) ? "" : CustomerId,
                    NameOnCard = string.IsNullOrEmpty(NameOnCard) ? "" : NameOnCard
                };



                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.getJcbCustomerDetail);

                JObject customerResponse = JObject.Parse(JsonConvert.DeserializeObject(contentString).ToString());

                var jarr = customerResponse["Data"].Value<JObject>();

                var customerResult = jarr["GetCustomerDetails"].Value<JArray>();
                List<JCBCustomerProfileResponse> customerProfileResponse = customerResult.ToObject<List<JCBCustomerProfileResponse>>();

                if (customerProfileResponse != null && customerProfileResponse.Count > 0)
                {
                    foreach (JCBCustomerProfileResponse response in customerProfileResponse)
                    {
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

                        if (response.FleetSizeNoOfVechileOwnedHCV == "0")
                            response.FleetSizeNoOfVechileOwnedHCV = "";
                        response.FleetSizeNoOfVechileOwnedLCV = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedLCV) ? "" : response.FleetSizeNoOfVechileOwnedLCV);
                        if (response.FleetSizeNoOfVechileOwnedLCV == "0")
                            response.FleetSizeNoOfVechileOwnedLCV = "";
                        response.FleetSizeNoOfVechileOwnedMUVSUV = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedMUVSUV) ? "" : response.FleetSizeNoOfVechileOwnedMUVSUV);
                        if (response.FleetSizeNoOfVechileOwnedMUVSUV == "0")
                            response.FleetSizeNoOfVechileOwnedMUVSUV = "";
                        response.FleetSizeNoOfVechileOwnedCarJeep = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedCarJeep) ? "" : response.FleetSizeNoOfVechileOwnedCarJeep);
                        if (response.FleetSizeNoOfVechileOwnedCarJeep == "0")
                            response.FleetSizeNoOfVechileOwnedCarJeep = "";

                        if (!string.IsNullOrEmpty(response.KeyOfficialDOA))
                        {
                            if (response.KeyOfficialDOA.Contains("1900"))
                            {
                                response.KeyOfficialDOA = "";
                            }
                            if (response.KeyOfficialDOA.Contains("0001"))
                            {
                                response.KeyOfficialDOA = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.KeyOfficialDOB))
                        {
                            if (response.KeyOfficialDOB.Contains("1900"))
                            {
                                response.KeyOfficialDOB = "";
                            }
                            if (response.KeyOfficialDOB.Contains("0001"))
                            {
                                response.KeyOfficialDOB = "";
                            }
                        }
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
        public async Task<List<SearchGridResponse>> CardDetailsForSearch(string CustomerId, string CustomerTypeId)
        {
            var searchBody = new GetCardDetailsRequest()
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
                CardNo = "",
                MobileNumber = "",
                VehicleNumber = "",
                StatusFlag = "-1"
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.searchJcbManageCard);

                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(contentString).ToString());
                var jarr = obj["Data"].Value<JArray>();
                List<SearchGridResponse> searchList = jarr.ToObject<List<SearchGridResponse>>();

                if (searchList != null && searchList.Count > 0)
                {
                    foreach (SearchGridResponse item in searchList)
                    {
                        if (string.IsNullOrEmpty(item.ExpiryDate))
                            item.ExpiryDate = "";
                        if (string.IsNullOrEmpty(item.IssueDate))
                            item.IssueDate = "";
                        if (string.IsNullOrEmpty(item.VechileNo))
                            item.VechileNo = "";
                    }
                }

                if (CustomerTypeId == "927")//DriverCard
                {
                    if (searchList != null && searchList.Count > 0)
                    {
                        foreach (SearchGridResponse item in searchList)
                        {
                            item.VechileNo = "DRIVER CARD";
                        }
                    }
                }

                if (CustomerTypeId == "918")//OTC
                {
                    if (searchList != null && searchList.Count > 0)
                    {
                        foreach (SearchGridResponse item in searchList)
                        {
                            if (string.IsNullOrEmpty(item.VechileNo))
                            {
                                item.VechileNo = "";
                            }
                        }
                    }
                }

                return searchList;
            }
        }
        public async Task<GetJCBCommunicationEmailResetPasswordResponse> GetJCBCommunicationEmailResetPassword(string CustomerId)
        {
            var responseData = new GetJCBCommunicationEmailResetPasswordResponse();

            var requestinfo = new GetJCBCommunicationEmailResetPasswordRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getJCBCommunicationEmailResetPassword);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<GetJCBCommunicationEmailResetPasswordResponse> searchList = jarr.ToObject<List<GetJCBCommunicationEmailResetPasswordResponse>>();
            responseData = searchList[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            return responseData;
        }
        public async Task<InsertResponse> UpdateJCBCommunicationEmailResetPassword(string CustomerId, string AlternateEmailId)
        {
            var email = "";

            email = AlternateEmailId.ToLower();

            var request = new JCBCustomerResetPassword
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
                AlternateEmailId = AlternateEmailId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateJcbCommunicationEmailResetPassword);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<JCBSearchManageCards> JCBManageCards(JCBCustomerCards entity, string editFlag)
        {
            var searchBody = new JCBCustomerCards();

            var UserName = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            if (entity.CustomerId != null || entity.CardNo != null)
            {
                searchBody = new JCBCustomerCards
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
                searchBody = new JCBCustomerCards
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

                JCBCustomerCards vGrid = JsonConvert.DeserializeObject<JCBCustomerCards>(str);

                searchBody = new JCBCustomerCards
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.searchJcbManageCard);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            JCBSearchManageCards searchList = obj.ToObject<JCBSearchManageCards>();
            return searchList;
        }
        public async Task<JCBSearchDetailsByCardId> JCBViewCardDetails(string CardId)
        {
            _httpContextAccessor.HttpContext.Session.SetString("CardIdSession", CardId);

            var cardDetailsBody = new JCBCardsSearch
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CardNo = CardId,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(cardDetailsBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.jcbGetCardLimitFeatures);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            JCBSearchDetailsByCardId searchRes = obj.ToObject<JCBSearchDetailsByCardId>();

            string cusId = string.Empty;
            foreach (var item in searchRes.Data.GetCardsDetailsModelOutput)
            {
                cusId = item.CustomerID;
            }
            _httpContextAccessor.HttpContext.Session.SetString("CustomerIdSession", cusId);

            return searchRes;
        }
        public async Task<JCBUpdateMobileModal> JCBCardlessMapping(string cardNumber, string mobileNumber, string LimitTypeName, string CCMSReloadSaleLimitValue)
        {
            JCBUpdateMobileModal editMobBody = new JCBUpdateMobileModal();
            editMobBody.CardNumber = cardNumber;
            editMobBody.MobileNumber = mobileNumber;
            editMobBody.LimitTypeName = LimitTypeName;
            editMobBody.CCMSReloadSaleLimitValue = CCMSReloadSaleLimitValue;

            _httpContextAccessor.HttpContext.Session.SetString("lmtType", editMobBody.LimitTypeName);
            return editMobBody;
        }

        public async Task<List<SuccessResponse>> JCBCardlessMappingUpdate(string mobNoNew, string crdNo)
        {
            var cardDetailsBody = new JCBUpdateMobile
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CardNo = crdNo,
                MobileNo = mobNoNew,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(cardDetailsBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateMobileUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse;
        }
        public async Task<JCBViewCardSearch> SearchCardMapping(JCBViewCardDetails viewCardDetails)
        {
            var searchBody = new JCBViewCardDetails();
            JCBViewCardSearch viewCardSearch = new JCBViewCardSearch();
            if (viewCardDetails.Customerid != null)
            {
                searchBody = new JCBViewCardDetails
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
                searchBody = new JCBViewCardDetails
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
            //var response = await _requestService.CommonRequestService(content, WebApiUrl.SearchCardMappingUrl);
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getJcbMobileAndFastagno);

            viewCardSearch = JsonConvert.DeserializeObject<JCBViewCardSearch>(response);
            return viewCardSearch;
        }
        public async Task<List<string>> UpdateCards(JCBUpdateMobileandFastagNoInCard[] limitArray)
        {
            var updateServiceBody = new JCBUpdateMobileRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                ObjUpdateMobileandFastagNoInCard = limitArray,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.JCBUpdateMobileAndFastagNoInCard);

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
        public async Task<JCBViewCardSearch> AddCardMappingDetails(JCBViewCardDetails viewCardDetails)
        {
            var searchBody = new JCBViewCardDetails();
            JCBViewCardSearch viewCardSearch = new JCBViewCardSearch();
            if (viewCardDetails.Customerid != null)
            {
                searchBody = new JCBViewCardDetails
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
            //var response = await _requestService.CommonRequestService(content, WebApiUrl.searchcardmappingdetailswithblankmobile);
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getJcbMobileAndFastagno);

            viewCardSearch = JsonConvert.DeserializeObject<JCBViewCardSearch>(response);
            return viewCardSearch;
        }
        public async Task<GetJCBDealerCardDispatchResponse> GetJCBDealerCardDispatchDetails(string CustomerID)
        {
            var request = new GetALCardDispatchDetailsRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getJcbDispatchDetail);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetJCBDealerCardDispatchResponse roleLocationResponse = obj.ToObject<GetJCBDealerCardDispatchResponse>();

            return roleLocationResponse;
        }
        public async Task<InsertResponse> ResetJCBDealerPassword(string UserName)
        {
            var requestBody = new UpdateAlDealePasswordReset
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserName = UserName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateJcbDealerCommunicationEmailResetPassword);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<JCBHotlistorReactivateViewModel> JCBHotlistAndReactivate()
        {
            JCBHotlistorReactivateViewModel model = new JCBHotlistorReactivateViewModel();

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
        public async Task<List<JCBHotlistReason>> GetReasonListForEntities(string EntityTypeId)
        {
            var forms = new JCBHotlistRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                EntityTypeId = EntityTypeId != "" ? Convert.ToInt32(EntityTypeId) : 0
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.jcbHotlistReactive);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<JCBHotlistReason> HotlistReason = jarr.ToObject<List<JCBHotlistReason>>();
            var sortedtList = HotlistReason.OrderBy(x => x.ReasonId).ToList();
            return sortedtList;
        }
        public async Task<List<string>> ApplyHotlistorReactivate([FromBody] JCBHotlistorReactivateViewModel hotlistorReactivateViewModel)
        {
            string customerId = "";
            string cardNo = "";
            if (hotlistorReactivateViewModel.EntityTypeId == "1")
                customerId = hotlistorReactivateViewModel.EntityIdVal;
            if (hotlistorReactivateViewModel.EntityTypeId == "3")
                cardNo = hotlistorReactivateViewModel.EntityIdVal;

            var hotlistRequest = new JCBHotlistingRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                EntityTypeId = hotlistorReactivateViewModel.EntityTypeId != "" ? Convert.ToInt32(hotlistorReactivateViewModel.EntityTypeId) : 0,
                CustomerId = customerId,
                CardNo = cardNo,
                ActionId = hotlistorReactivateViewModel.ActionId != "" ? Convert.ToInt32(hotlistorReactivateViewModel.ActionId) : 0,
                ReasonId = hotlistorReactivateViewModel.ReasonId != "" ? Convert.ToInt32(hotlistorReactivateViewModel.ReasonId) : 0,
                RemarksOthers = hotlistorReactivateViewModel.ReasonDetails,
                Remarks = hotlistorReactivateViewModel.Remarks,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(hotlistRequest), Encoding.UTF8, "application/json");
            var Response = await _requestService.CommonRequestService(requestContent, WebApiUrl.jcbUpdateHotlistReactivate);
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
        public async Task<InsertResponse> EnableDisableJCBDealer(string DealerCode, string OfficerType, string EnableDisableFlag)
        {
            bool flag = false;

            if (EnableDisableFlag.ToUpper() == "ENABLED")
            {
                flag = true;
            }

            var requestBody = new EnableDisableJCBDealerRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                DealerCode = DealerCode,
                OfficerType = OfficerType,
                IsDisable = flag
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.enableDisableJcbDealer);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }

        public async Task<JCBViewDealerOTCCardStatusModel> ViewJCBDealerOTCCardStatus()
        {
            JCBViewDealerOTCCardStatusModel model = new JCBViewDealerOTCCardStatusModel();
            model.Remarks = "";
            return model;
        }
        public async Task<GetJCBDealerOTCCardStatusResponse> GetViewJCBDealerOTCCardStatus(string DealerCode, string CardNo)
        {
            var searchBody = new GetJCBOTCCardDealerAllocationRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode,
                CardNo = string.IsNullOrEmpty(CardNo) ? "" : CardNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.viewJcbDealerOtcCardStatus);

            GetJCBDealerOTCCardStatusResponse response = new GetJCBDealerOTCCardStatusResponse();

            response = JsonConvert.DeserializeObject<GetJCBDealerOTCCardStatusResponse>(ResponseContent);

            return response;
        }

    }
}