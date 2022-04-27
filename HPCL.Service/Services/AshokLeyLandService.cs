using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.AshokLeyLand;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.RequestModel.AshokLeyLand;
using System;
using HPCL.Common.Models.ViewModel.ApplicationFormDataEntry;
using Microsoft.Extensions.Configuration;

namespace HPCL.Service.Services
{
    public class AshokLeyLandService : IAshokLeyLandService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public AshokLeyLandService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }

        public async Task<SearchAlResult> SearchDealer(string dealerCode, string dtpCode)
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDelerNameUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            SearchAlResult searchList = obj.ToObject<SearchAlResult>();

            return searchList;
        }

        public async Task<InsertResponse> InsertAlEnroll(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<AlEnrollment> arrs = objs.ToObject<List<AlEnrollment>>();

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
                EmailId = arrs[0].EmailId,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(insertServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.InsertAlDetailsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }

        public async Task<InsertResponse> AlEnrollUpdate(string getAllData)
        {

            JArray objs= JArray.Parse(JsonConvert.DeserializeObject(getAllData).ToString());
            List<AlEnrollment> arrs = objs.ToObject<List<AlEnrollment>>();

            var insertServiceBody = new AlEnrollment
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                DealerCode = arrs[0].DealerCode,
                ZonalOfficeId = arrs[0].ZonalOfficeId,
                RegionalOfficeId = arrs[0].RegionalOfficeId,
                Address1 =  arrs[0].Address1,
                Address2 =  arrs[0].Address2,
                Address3 =  arrs[0].Address3,
                StateId =  arrs[0].StateId,
                CityName =  arrs[0].CityName,
                DistrictId =  arrs[0].DistrictId,
                Pin =  arrs[0].Pin,
                MobileNo =  arrs[0].MobileNo,
                EmailId =  arrs[0].EmailId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(insertServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateAlDetailsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }

        public async Task<ALOTCCardRequestModel> DealerOTCCardRequest()
        {
            ALOTCCardRequestModel alOTCCardRequestModel = new ALOTCCardRequestModel();
            alOTCCardRequestModel.Remarks = "";
            return alOTCCardRequestModel;
        }
        public async Task<ALOTCCardRequestModel> DealerOTCCardRequest(ALOTCCardRequestModel alOTCCardRequestModel)
        {
            alOTCCardRequestModel.UserAgent = CommonBase.useragent;
            alOTCCardRequestModel.UserIp = CommonBase.userip;
            alOTCCardRequestModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            alOTCCardRequestModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent content = new StringContent(JsonConvert.SerializeObject(alOTCCardRequestModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDealerWiseAlOtcCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            alOTCCardRequestModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            alOTCCardRequestModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    alOTCCardRequestModel.Remarks = customerResponse.Data[0].Reason;
                else
                    alOTCCardRequestModel.Remarks = customerResponse.Message;
            }

            return alOTCCardRequestModel;
        }

        public async Task<AshokLeylandCardCreationModel> CreateMultipleOTCCard()
        {
            AshokLeylandCardCreationModel ashokLeylandCardCreationModel = new AshokLeylandCardCreationModel();
            ashokLeylandCardCreationModel.Remarks = "";
            ashokLeylandCardCreationModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
            ashokLeylandCardCreationModel.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());
            return ashokLeylandCardCreationModel;
        }
        public async Task<List<CardDetails>> GetAvailableAlOTCCardForDealer(string DealerCode)
        {

            var requestinfo = new GetAvailityALOTCCardRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = DealerCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAvailityAlOTCCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CardDetails> searchList = jarr.ToObject<List<CardDetails>>();

            return searchList;
        }

        public async Task<AshokLeylandCardCreationModel> CreateMultipleOTCCard(AshokLeylandCardCreationModel ashokLeylandCardCreationModel)
        {
            ashokLeylandCardCreationModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            ashokLeylandCardCreationModel.UserAgent = CommonBase.useragent;
            ashokLeylandCardCreationModel.UserIp = CommonBase.userip;
            ashokLeylandCardCreationModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            ashokLeylandCardCreationModel.CommunicationPhoneNo = (String.IsNullOrEmpty(ashokLeylandCardCreationModel.CommunicationDialCode) ? "" : ashokLeylandCardCreationModel.CommunicationDialCode) + "-" + (String.IsNullOrEmpty(ashokLeylandCardCreationModel.CommunicationPhonePart2) ? "" : ashokLeylandCardCreationModel.CommunicationPhonePart2);
            ashokLeylandCardCreationModel.CommunicationFax = (String.IsNullOrEmpty(ashokLeylandCardCreationModel.CommunicationFaxCode) ? "" : ashokLeylandCardCreationModel.CommunicationFaxCode) + "-" + (String.IsNullOrEmpty(ashokLeylandCardCreationModel.CommunicationFaxPart2) ? "" : ashokLeylandCardCreationModel.CommunicationFaxPart2);
            if (!string.IsNullOrEmpty(ashokLeylandCardCreationModel.CommunicationEmailid))
            {
                ashokLeylandCardCreationModel.CommunicationEmailid = ashokLeylandCardCreationModel.CommunicationEmailid.ToLower();
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(ashokLeylandCardCreationModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertAlCustomer);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            ashokLeylandCardCreationModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            ashokLeylandCardCreationModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    ashokLeylandCardCreationModel.Remarks = customerResponse.Data[0].Reason;
                else
                    ashokLeylandCardCreationModel.Remarks = customerResponse.Message;
                ashokLeylandCardCreationModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                ashokLeylandCardCreationModel.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(ashokLeylandCardCreationModel.CommunicationStateId));
                ashokLeylandCardCreationModel.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());
            }

            return ashokLeylandCardCreationModel;
        }

        public async Task<ViewALOTCCardDealerMappingModel> ViewALOTCCardsDealerMapping()
        {
            ViewALOTCCardDealerMappingModel model = new ViewALOTCCardDealerMappingModel();
            model.Remarks = "";
            return model;
        }
        public async Task<ALOTCCardDealerAllocationResponse> GetViewALOTCCardDealerAllocation(string DealerCode, string CardNo)
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

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.viewAlOTCCardDealerAllocation);

            ALOTCCardDealerAllocationResponse response = new ALOTCCardDealerAllocationResponse();

            response = JsonConvert.DeserializeObject<ALOTCCardDealerAllocationResponse>(ResponseContent);

            return response;
        }
        public async Task<AddonOTCCardMapping> AddonOTCCardMapping()
        {
            AddonOTCCardMapping addAddOnCard = new AddonOTCCardMapping();
            addAddOnCard.Remarks = "";
            addAddOnCard.Message = "";
            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();

            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            return addAddOnCard;
        }
        public async Task<GetCustomerDetails> GetAlAddonOTCCardMappingCustomerDetails(string customerId)
        {
            var searchBody = new GetCustomerName
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAladdonOTCCardMappingCustomerDetails);

            GetAlAddonCustomerResponse customerResponse = JsonConvert.DeserializeObject<GetAlAddonCustomerResponse>(response);
            GetCustomerDetails GetCustomerDetails = new GetCustomerDetails();

            if (customerResponse != null && customerResponse.Data != null)
            {
                GetCustomerDetails = customerResponse.Data.GetCustomerNameAndNameOnCard[0];
                if (String.IsNullOrEmpty(GetCustomerDetails.CustomerOrgName))
                {
                    GetCustomerDetails.CustomerOrgName = "";
                    GetCustomerDetails.Reason = customerResponse.Data.GetStatus[0].Reason;
                }
                if (String.IsNullOrEmpty(GetCustomerDetails.NameOnCard))
                {
                    GetCustomerDetails.NameOnCard = "";
                }
            }

            return GetCustomerDetails;
        }

        public async Task<AddonOTCCardMapping> GetAlAddonOTCCardCustomerDetailsPartialView(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<AddonOTCCardDetails> arrs = objs.ToObject<List<AddonOTCCardDetails>>();

            AddonOTCCardMapping addAddOnCard = new AddonOTCCardMapping();
            addAddOnCard.Message = "";

            if (arrs != null && arrs.Count > 0)
            {
                if (!string.IsNullOrEmpty(arrs[0].Message))
                    addAddOnCard.Message = arrs[0].Message;
                if (!string.IsNullOrEmpty(arrs[0].NoOfCards))
                    addAddOnCard.NoOfCards = arrs[0].NoOfCards;
                else
                    addAddOnCard.NoOfCards = "";
                if (!string.IsNullOrEmpty(arrs[0].CustomerOrgName))
                    addAddOnCard.CustomerOrgName = arrs[0].CustomerOrgName;
                if (!string.IsNullOrEmpty(arrs[0].NameOnCard))
                    addAddOnCard.NameOnCard = arrs[0].NameOnCard;
                if (!string.IsNullOrEmpty(arrs[0].DealerCode))
                    addAddOnCard.DealerCode = arrs[0].DealerCode;
                if (!string.IsNullOrEmpty(arrs[0].SalesExecutiveEmployeeID))
                    addAddOnCard.SalesExecutiveEmployeeID = arrs[0].SalesExecutiveEmployeeID;
                addAddOnCard.VehicleVerifiedManually = arrs[0].VehicleVerifiedManually;
            }
            addAddOnCard.TableStringyfiedData = str;
            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].VechileNo))))
                addAddOnCard.ObjCardDetail = arrs;

            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();

            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            return addAddOnCard;
        }
        public async Task<AddonOTCCardMapping> GetAlAddonOTCCardAddCardsPartialView(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<AddonOTCCardDetails> arrs = objs.ToObject<List<AddonOTCCardDetails>>();
            AddonOTCCardMapping addAddOnCard = new AddonOTCCardMapping();

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            addAddOnCard.CustomerId = arrs[0].CustomerId;
            addAddOnCard.NoOfCards = arrs[0].NoOfCards;
            addAddOnCard.TableStringyfiedData = str;
            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].VechileNo))))
                addAddOnCard.ObjCardDetail = arrs;

            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();

            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            return addAddOnCard;
        }

        public async Task<AddonOTCCardMapping> AddonOTCCardMapping(AddonOTCCardMapping addAddOnCard)
        {

            foreach (AddonOTCCardDetails cardDetails in addAddOnCard.ObjCardDetail)
            {
                if (!string.IsNullOrEmpty(cardDetails.VechileNo))
                {
                    cardDetails.VechileNo = cardDetails.VechileNo.ToUpper();
                }
            }

            addAddOnCard.Message = "";
            addAddOnCard.Reason = "";
            addAddOnCard.UserAgent = CommonBase.useragent;
            addAddOnCard.UserIp = CommonBase.userip;
            addAddOnCard.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            addAddOnCard.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            if (Convert.ToInt32(string.IsNullOrEmpty(addAddOnCard.NoOfCards) ? "0" : addAddOnCard.NoOfCards) > 0)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(addAddOnCard), Encoding.UTF8, "application/json");

                CustomerInserCardResponse customerInserCardResponse;

                var responseCustomer = await _requestService.CommonRequestService(content, WebApiUrl.alAddonOTCCard);

                customerInserCardResponse = JsonConvert.DeserializeObject<CustomerInserCardResponse>(responseCustomer);

                if (customerInserCardResponse.Internel_Status_Code != 1000)
                {
                    foreach (AddonOTCCardDetails cardDetails in addAddOnCard.ObjCardDetail)
                    {
                        cardDetails.DuplicateVehicleNo = "";
                        cardDetails.DuplicateMobileNo = "";
                    }

                    if (customerInserCardResponse.Message.Contains("Vehicle No."))
                    {
                        foreach (CustomerInserCardResponseData responseData in customerInserCardResponse.Data)
                        {
                            foreach (AddonOTCCardDetails cardDetails in addAddOnCard.ObjCardDetail)
                            {
                                if (cardDetails.VechileNo.ToUpper() == responseData.Reason.ToUpper())
                                {
                                    cardDetails.DuplicateVehicleNo = "Vehicle No. already exists";
                                }
                            }
                        }
                    }

                    if (customerInserCardResponse.Message.Contains("Mobile No."))
                    {
                        foreach (CustomerInserCardResponseData responseData in customerInserCardResponse.Data)
                        {
                            foreach (AddonOTCCardDetails cardDetails in addAddOnCard.ObjCardDetail)
                            {
                                if (cardDetails.MobileNo == responseData.Reason)
                                {
                                    cardDetails.DuplicateMobileNo = "Mobile No. already exists";
                                }
                            }
                        }
                    }
                }

                if (customerInserCardResponse.Internel_Status_Code == 1000)
                {
                    addAddOnCard.Status = customerInserCardResponse.Data[0].Status;
                    addAddOnCard.StatusCode = customerInserCardResponse.Internel_Status_Code;
                    addAddOnCard.Message = customerInserCardResponse.Message;
                    addAddOnCard.Reason = customerInserCardResponse.Data[0].Reason;
                }
                else
                {
                    addAddOnCard.Message = customerInserCardResponse.Message;
                    if (customerInserCardResponse.Message.Contains("No Record Found"))
                    {
                        addAddOnCard.Message = customerInserCardResponse.Data[0].Reason;
                    }
                    addAddOnCard.StatusCode = customerInserCardResponse.Internel_Status_Code;

                    foreach (AddonOTCCardDetails cardDetails in addAddOnCard.ObjCardDetail)
                    {
                        if (addAddOnCard.VehicleVerifiedManually)
                        {
                            cardDetails.Verified = "0";
                        }
                        else
                        {
                            cardDetails.Verified = "1";
                        }
                    }

                }
            }

            return addAddOnCard;
        }

        public async Task<AddonOTCCardMapping> GetAlSalesExeEmpIdAddOnOTCCardMapping(string dealerCode)
        {
            AddonOTCCardMapping customerCardInfo = new AddonOTCCardMapping();

            var requestInfo = new GetAvailityALOTCCardRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DealerCode = dealerCode
            };
            
            StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(requestInfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getAlSalesExeEmpidAddonOtcCardMapping);

            SalesExecutiveEmployeeIDResponse salesExecutiveEmployeeIDResponse = JsonConvert.DeserializeObject<SalesExecutiveEmployeeIDResponse>(response);


            if (salesExecutiveEmployeeIDResponse.Internel_Status_Code == 1000)
            {
                customerCardInfo.SalesExecutiveEmployeeID = salesExecutiveEmployeeIDResponse.Data[0].SalesExecutiveEmployeeID;
                customerCardInfo.StatusCode = salesExecutiveEmployeeIDResponse.Internel_Status_Code;
            }
            else
            {
                if (salesExecutiveEmployeeIDResponse.Data.Count > 0)
                    customerCardInfo.Reason = salesExecutiveEmployeeIDResponse.Data[0].Reason;
                else
                    customerCardInfo.Reason = "Invalid Dealer Code";
                customerCardInfo.StatusCode = salesExecutiveEmployeeIDResponse.Internel_Status_Code;
            }
            return customerCardInfo;
        }

    }
}
