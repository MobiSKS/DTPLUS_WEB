﻿using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.RequestModel.AshokLeyLand;
using HPCL.Common.Models.RequestModel.Merchant;
using HPCL.Common.Models.RequestModel.VolvoEicher;
using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ResponseModel.VolvoEicher;
using HPCL.Common.Models.ViewModel.AshokLeyLand;
using HPCL.Common.Models.ViewModel.VolvoEicher;
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
    public class VolvoEicherService:IVolvoEicherService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public VolvoEicherService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }
        public async Task<VEDealerOTCCardStatusViewModel> GetVEDealerCardStatus(string DealerCode)
        {
          
            var reqForm = new VEDealerOTCRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                DealerCode = string.IsNullOrEmpty(DealerCode) ? "" : DealerCode,
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reqForm), Encoding.UTF8, "application/json");

            var responseContent = await _requestService.CommonRequestService(stringContent, WebApiUrl.viewvolvoeicherdealerotccardstatus);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
            VEDealerOTCCardStatusViewModel getVEDealerOTCCardStatusViewModel = obj.ToObject<VEDealerOTCCardStatusViewModel>();

            return getVEDealerOTCCardStatusViewModel;

        }
        public async Task<List<VECustomerProfileResponse>> BindCustomerDetailsForSearch(string CustomerId, string NameOnCard)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {

                var searchBody = new CustomerProfileModel
                    {
                        UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                        UserAgent = CommonBase.useragent,
                        UserIp = CommonBase.userip,
                        CustomerId = string.IsNullOrEmpty(CustomerId) ? "" : CustomerId,
                        NameOnCard = string.IsNullOrEmpty(NameOnCard) ? "" : NameOnCard
                    };
                


                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.getvolvoeichercustomerdetail);

                JObject customerResponse = JObject.Parse(JsonConvert.DeserializeObject(contentString).ToString());

                var jarr = customerResponse["Data"].Value<JObject>();

                var customerResult = jarr["GetCustomerDetails"].Value<JArray>();
                List<VECustomerProfileResponse> customerProfileResponse = customerResult.ToObject<List<VECustomerProfileResponse>>();

                if (customerProfileResponse != null && customerProfileResponse.Count > 0)
                {
                    foreach (VECustomerProfileResponse response in customerProfileResponse)
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
                        //}

                        //if (!string.IsNullOrEmpty(response.KeyOfficialDOB))
                        //{
                        //    if (response.KeyOfficialDOB.Contains("1900"))
                        //    {
                        //        response.KeyOfficialDOB = "";
                        //    }
                        //}
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
                    }
                }

                return customerProfileResponse;
            }
        }

        public async Task<InsertResponse> InsertVEDealerEnrollment(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<AlEnrollment> arrs = objs.ToObject<List<AlEnrollment>>();
            var email = "";

            email = arrs[0].EmailId.ToLower();

            var insertServiceBody = new AlEnrollment
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
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
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(insertServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertVolvoEicherDealerEnrollment);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<SearchAlResult> SearchVEDealer(string dealerCode, string dtpCode)
        {
            var searchBody = new SearchDealer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                DealerCode = dealerCode,
                DTPDealerCode = dtpCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getVolvoEicherDealerDetail);

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
                }
            }

            return searchList;
        }
        public async Task<InsertResponse> VEDealerEnrollmentUpdate(string getAllData)
        {

            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(getAllData).ToString());
            List<AlEnrollment> arrs = objs.ToObject<List<AlEnrollment>>();

            var email = "";

            email = arrs[0].EmailId.ToLower();

            var insertServiceBody = new AlEnrollment
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateVolvoEicherDealerEnrollment);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }

        public async Task<CommonResponseData> CheckVEDealerCodeExists(string DealerCode)
        {
            CommonResponseData responseData = new CommonResponseData();

            VerifyDealerRequestModel requestinfo = new VerifyDealerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.checkvedealercode);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CommonResponseData> searchList = jarr.ToObject<List<CommonResponseData>>();
            responseData = searchList[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            return responseData;
        }
        public async Task<VEOTCCardRequestModel> VEDealerOTCCardRequest()
        {
            var model = new VEOTCCardRequestModel();
            model.Remarks = "";
            return model;
        }
        public async Task<VEOTCCardRequestModel> VEDealerOTCCardRequest(VEOTCCardRequestModel model)
        {
            model.UserAgent = CommonBase.useragent;
            model.UserIp = CommonBase.userip;
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDealerWiseVolvoEicherOtcCardRequest);


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
        public async Task<InsertResponse> ResetVEDealerPassword(string UserName)
        {
            var requestBody = new UpdateAlDealePasswordReset
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserName = UserName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateVECommunicationEmailResetPassword);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<ViewVEDealerOTCCardDetailsModel> ViewVEDealerUnmappedOTCCardDetails()
        {
            ViewVEDealerOTCCardDetailsModel model = new ViewVEDealerOTCCardDetailsModel();
            model.Remarks = "";
            return model;
        }
        public async Task<VEOTCCardDealerAllocationResponse> GetViewVEOTCCardDealerAllocation(string DealerCode, string CardNo)
        {
            var searchBody = new GetALOTCCardDealerAllocationRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode,
                CardNo = string.IsNullOrEmpty(CardNo) ? "" : CardNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.viewVolvoEicherDealerOtcCardDetail);

            VEOTCCardDealerAllocationResponse response = new VEOTCCardDealerAllocationResponse();

            response = JsonConvert.DeserializeObject<VEOTCCardDealerAllocationResponse>(ResponseContent);

            return response;
        }
        public async Task<VECardCreationModel> CreateMultipleOTCCard()
        {
            VECardCreationModel model = new VECardCreationModel();
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
        public async Task<GetSalesExecutiveEmployeeIDResponse> GetVESalesExecutiveEmpId(string dealerCode)
        {
            GetSalesExecutiveEmployeeIDResponse customerCardInfo = new GetSalesExecutiveEmployeeIDResponse();

            var requestInfo = new GetAvailityALOTCCardRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = dealerCode
            };

            StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(requestInfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getVolvoEicherSalesExeEmpidAddonOtcCardMapping);

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
        public async Task<VECardCreationModel> GetMultipleOTCCardPartialView([FromBody] List<VECardEntryDetails> arrs)
        {
            VECardCreationModel addAddOnCard = new VECardCreationModel();

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            addAddOnCard.NoOfCards = arrs[0].NoOfCards;
            addAddOnCard.DealerCode = arrs[0].DealerCode;

            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].CardNo))))
                addAddOnCard.ObjVECardEntryDetail = arrs;

            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();

            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            return addAddOnCard;
        }
        public async Task<VECardCreationModel> CreateMultipleOTCCard(VECardCreationModel model)
        {
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.UserAgent = CommonBase.useragent;
            model.UserIp = CommonBase.userip;
            model.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CommunicationPhoneNo = (String.IsNullOrEmpty(model.CommunicationDialCode) ? "" : model.CommunicationDialCode) + "-" + (String.IsNullOrEmpty(model.CommunicationPhonePart2) ? "" : model.CommunicationPhonePart2);
            model.CommunicationFax = (String.IsNullOrEmpty(model.CommunicationFaxCode) ? "" : model.CommunicationFaxCode) + "-" + (String.IsNullOrEmpty(model.CommunicationFaxPart2) ? "" : model.CommunicationFaxPart2);
            if (!string.IsNullOrEmpty(model.CommunicationEmailid))
            {
                model.CommunicationEmailid = model.CommunicationEmailid.ToLower();
            }
            foreach (VECardEntryDetails cardDetails in model.ObjVECardEntryDetail)
            {
                if (!string.IsNullOrEmpty(cardDetails.VechileNo))
                    cardDetails.VechileNo = cardDetails.VechileNo.ToUpper();
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertVolvoEicherCustomer);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            model.Internel_Status_Code = customerResponse.Internel_Status_Code;
            model.Remarks = customerResponse.Message;

            foreach (VECardEntryDetails cardDetails in model.ObjVECardEntryDetail)
            {
                cardDetails.VehicleNoMsg = "";
                cardDetails.MobileNoMsg = "";
                cardDetails.CardNoMsg = "";
                cardDetails.VINNoMsg = "";
            }
            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                    model.Remarks = customerResponse.Data[0].Reason;
                else
                    model.Remarks = customerResponse.Message;
                model.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                model.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(model.CommunicationStateId));
                model.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());
            }
            else
            {
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                    model.Remarks = customerResponse.Data[0].Reason;
            }

            return model;
        }
        public async Task<List<VEOTCCardResponse>> GetAvailableVEOTCCardForDealer(string DealerCode)
        {

            var requestinfo = new GetAvailityALOTCCardRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAvailityVolvoEicherOtcCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<VEOTCCardResponse> searchList = jarr.ToObject<List<VEOTCCardResponse>>();

            return searchList;
        }
        public async Task<VEManageProfile> ManageProfile()
        {
            VEManageProfile custMdl = new VEManageProfile();

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
        public async Task<List<SearchGridResponse>> CardDetailsForSearch(string CustomerId, string CustomerTypeId)
        {
            var searchBody = new GetCardDetailsRequest()
            {
                UserId = CommonBase.userid,
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = CustomerId,
                CardNo = "",
                MobileNumber = "",
                VehicleNumber = "",
                StatusFlag = "-1"
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.searchVolvoEicherManageCard);

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

    }
}
