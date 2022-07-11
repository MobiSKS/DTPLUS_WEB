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
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models.RequestModel.TMS;
using System.Linq;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Common.Models.RequestModel.Configure;

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

        public async Task<InsertResponse> InsertAlEnroll(string str)
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.InsertAlDetailsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }

        public async Task<InsertResponse> AlEnrollUpdate(string getAllData)
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
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                    alOTCCardRequestModel.Remarks = customerResponse.Data[0].Reason;
                else
                    alOTCCardRequestModel.Remarks = customerResponse.Message;
            }
            else
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                    alOTCCardRequestModel.Remarks = customerResponse.Data[0].Reason;
            }

            return alOTCCardRequestModel;
        }

        public async Task<AshokLeylandCardCreationModel> CreateMultipleOTCCard()
        {
            AshokLeylandCardCreationModel ashokLeylandCardCreationModel = new AshokLeylandCardCreationModel();
            ashokLeylandCardCreationModel.Remarks = "";
            ashokLeylandCardCreationModel.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
            if (string.IsNullOrEmpty(ashokLeylandCardCreationModel.ExternalPANAPIStatus))
            {
                ashokLeylandCardCreationModel.ExternalPANAPIStatus = "Y";
            }
            ashokLeylandCardCreationModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
            ashokLeylandCardCreationModel.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());
            return ashokLeylandCardCreationModel;
        }
        public async Task<List<OTCCardDetails>> GetAvailableAlOTCCardForDealer(string DealerCode)
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
            List<OTCCardDetails> searchList = jarr.ToObject<List<OTCCardDetails>>();

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
            foreach (ALCardEntryDetails cardDetails in ashokLeylandCardCreationModel.ObjALCardEntryDetail)
            {
                if (!string.IsNullOrEmpty(cardDetails.VechileNo))
                    cardDetails.VechileNo = cardDetails.VechileNo.ToUpper();
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(ashokLeylandCardCreationModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertAlCustomer);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            ashokLeylandCardCreationModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            ashokLeylandCardCreationModel.Remarks = customerResponse.Message;

            foreach (ALCardEntryDetails cardDetails in ashokLeylandCardCreationModel.ObjALCardEntryDetail)
            {
                cardDetails.VehicleNoMsg = "";
                cardDetails.MobileNoMsg = "";
                cardDetails.CardNoMsg = "";
                cardDetails.VINNoMsg = "";
            }
            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                    ashokLeylandCardCreationModel.Remarks = customerResponse.Data[0].Reason;
                else
                    ashokLeylandCardCreationModel.Remarks = customerResponse.Message;
                ashokLeylandCardCreationModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                ashokLeylandCardCreationModel.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(ashokLeylandCardCreationModel.CommunicationStateId));
                ashokLeylandCardCreationModel.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());
            }
            else
            {
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                    ashokLeylandCardCreationModel.Remarks = customerResponse.Data[0].Reason;
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
                if (customerResponse.Data.GetCustomerNameAndNameOnCard != null && customerResponse.Data.GetCustomerNameAndNameOnCard.Count > 0)
                {
                    GetCustomerDetails = customerResponse.Data.GetCustomerNameAndNameOnCard[0];
                }
                if (String.IsNullOrEmpty(GetCustomerDetails.CustomerOrgName))
                {
                    GetCustomerDetails.CustomerOrgName = "";
                    if (String.IsNullOrEmpty(customerResponse.Data.GetStatus[0].Reason))
                    {
                        GetCustomerDetails.Reason = "Customer ID Not Found";
                    }
                    else
                    {
                        GetCustomerDetails.Reason = customerResponse.Data.GetStatus[0].Reason;
                    }
                }
                else if (GetCustomerDetails.CustomerOrgName.Trim() == "")
                {
                    GetCustomerDetails.CustomerOrgName = "";
                    if (String.IsNullOrEmpty(customerResponse.Data.GetStatus[0].Reason))
                    {
                        GetCustomerDetails.Reason = "Customer ID Not Found";
                    }
                    else
                    {
                        GetCustomerDetails.Reason = customerResponse.Data.GetStatus[0].Reason;
                    }
                }
                if (String.IsNullOrEmpty(GetCustomerDetails.NameOnCard))
                {
                    GetCustomerDetails.NameOnCard = "";
                }
            }

            return GetCustomerDetails;
        }

        public async Task<AddonOTCCardMapping> GetAlAddonOTCCardCustomerDetailsPartialView([FromBody] List<AddonOTCCardDetails> arrs)
        {
            //JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            //List<AddonOTCCardDetails> arrs = objs.ToObject<List<AddonOTCCardDetails>>();

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
                addAddOnCard.VehicleVerifiedManually = Convert.ToBoolean(arrs[0].VehicleVerifiedManually);
            }
            //addAddOnCard.TableStringyfiedData = str;
            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].CardNo))))
                addAddOnCard.ObjCardDetail = arrs;

            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();

            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            return addAddOnCard;
        }
        public async Task<AddonOTCCardMapping> GetAlAddonOTCCardAddCardsPartialView([FromBody] List<AddonOTCCardDetails> arrs)
        {
            //JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            //List<AddonOTCCardDetails> arrs = objs.ToObject<List<AddonOTCCardDetails>>();
            AddonOTCCardMapping addAddOnCard = new AddonOTCCardMapping();

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            addAddOnCard.CustomerId = arrs[0].CustomerId;
            addAddOnCard.NoOfCards = arrs[0].NoOfCards;
            //addAddOnCard.TableStringyfiedData = str;
            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].CardNo))))
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
                        cardDetails.VehicleNoMsg = "";
                        cardDetails.MobileNoMsg = "";
                        cardDetails.CardNoMsg = "";
                        cardDetails.VINNoMsg = "";
                        if (string.IsNullOrEmpty(cardDetails.VechileNo))
                        {
                            cardDetails.VechileNo = "";
                        }
                        if (string.IsNullOrEmpty(cardDetails.MobileNo))
                        {
                            cardDetails.MobileNo = "";
                        }
                    }

                    addAddOnCard.Message = customerInserCardResponse.Message;
                    if (customerInserCardResponse.Message.Contains("No Record Found"))
                    {
                        addAddOnCard.Message = customerInserCardResponse.Data[0].Reason;
                    }

                    if (addAddOnCard.Message.Contains("Card No."))
                    {
                        foreach (CustomerInserCardResponseData responseData in customerInserCardResponse.Data)
                        {
                            foreach (AddonOTCCardDetails cardDetails in addAddOnCard.ObjCardDetail)
                            {
                                if (cardDetails.CardNo.ToUpper() == responseData.Reason.ToUpper())
                                {
                                    cardDetails.CardNoMsg = "Card No. already allocated";
                                }
                            }
                        }
                    }

                    if (addAddOnCard.Message.Contains("VIN No."))
                    {
                        foreach (CustomerInserCardResponseData responseData in customerInserCardResponse.Data)
                        {
                            foreach (AddonOTCCardDetails cardDetails in addAddOnCard.ObjCardDetail)
                            {
                                if (cardDetails.VINNumber.ToUpper() == responseData.Reason.ToUpper())
                                {
                                    cardDetails.VINNoMsg = "VIN No. already exists.";
                                }
                            }
                        }
                    }

                    if (addAddOnCard.Message.Contains("Vehicle No."))
                    {
                        foreach (CustomerInserCardResponseData responseData in customerInserCardResponse.Data)
                        {
                            foreach (AddonOTCCardDetails cardDetails in addAddOnCard.ObjCardDetail)
                            {
                                if (cardDetails.VechileNo.ToUpper() == responseData.Reason.ToUpper())
                                {
                                    cardDetails.VehicleNoMsg = "Vehicle No. already exists";
                                }
                            }
                        }
                    }

                    if (addAddOnCard.Message.Contains("Mobile No."))
                    {
                        foreach (CustomerInserCardResponseData responseData in customerInserCardResponse.Data)
                        {
                            foreach (AddonOTCCardDetails cardDetails in addAddOnCard.ObjCardDetail)
                            {
                                if (cardDetails.MobileNo == responseData.Reason)
                                {
                                    cardDetails.MobileNoMsg = "Mobile No. already exists";
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

        public async Task<AshokLeylandCustomerUpdateModel> UpdateALCustomer()
        {
            AshokLeylandCustomerUpdateModel model = new AshokLeylandCustomerUpdateModel();
            model.Message = "";
            model.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
            return model;
        }
        public async Task<AshokLeylandCustomerUpdateModel> GetCustomerAddress(string CustomerId)
        {
            AshokLeylandCustomerUpdateModel custMdl = new AshokLeylandCustomerUpdateModel();
            custMdl.Message = "";

            var request = new GetEnrollVehicleManagementDetailRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getAlCustomerDetail);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<AshokLeylandCustomerUpdateModel> lst = jarr.ToObject<List<AshokLeylandCustomerUpdateModel>>();

            if (lst != null && lst.Count > 0)
            {
                custMdl = lst[0];

                custMdl.CASID = custMdl.CommunicationStateId.ToString();
                custMdl.CADID = custMdl.CommunicationDistrictId.ToString();

                if (!string.IsNullOrEmpty(custMdl.CommunicationPhoneNo))
                {
                    string[] subs = custMdl.CommunicationPhoneNo.Split('-');

                    if (subs.Count() > 1)
                    {
                        custMdl.CommunicationDialCode = subs[0].ToString();
                        custMdl.CommunicationPhonePart2 = subs[1].ToString();
                    }
                    else
                    {
                        custMdl.CommunicationPhonePart2 = custMdl.CommunicationPhoneNo;
                    }
                }

                if (!string.IsNullOrEmpty(custMdl.CommunicationFax))
                {
                    string[] subs = custMdl.CommunicationFax.Split('-');

                    if (subs.Count() > 1)
                    {
                        custMdl.CommunicationFaxCode = subs[0].ToString();
                        custMdl.CommunicationFaxPart2 = subs[1].ToString();
                    }
                    else
                    {
                        custMdl.CommunicationFaxPart2 = custMdl.CommunicationFax;
                    }
                }
            }
            else
            {
                custMdl.Message = obj["Message"].ToString();
                custMdl.CommunicationAddress1 = "";
            }

            return custMdl;
        }

        public async Task<AshokLeylandCustomerUpdateModel> UpdateALCustomer(AshokLeylandCustomerUpdateModel model)
        {
            model.UserAgent = CommonBase.useragent;
            model.UserIp = CommonBase.userip;
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CommunicationPhoneNo = (string.IsNullOrEmpty(model.CommunicationDialCode) ? "" : model.CommunicationDialCode) + "-" + (string.IsNullOrEmpty(model.CommunicationPhonePart2) ? "" : model.CommunicationPhonePart2);
            model.CommunicationFax = (string.IsNullOrEmpty(model.CommunicationFaxCode) ? "" : model.CommunicationFaxCode) + "-" + (string.IsNullOrEmpty(model.CommunicationFaxPart2) ? "" : model.CommunicationFaxPart2);

            if (!string.IsNullOrEmpty(model.CommunicationEmailid))
            {
                model.CommunicationEmailid = model.CommunicationEmailid.ToLower();
            }


            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.alUpdateCustomer);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            CustomerAddressUpdateResponse customerResponse = JsonConvert.DeserializeObject<CustomerAddressUpdateResponse>(response, settings);

            model.Internel_Status_Code = customerResponse.Internel_Status_Code;
            model.Message = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    model.Message = customerResponse.Data[0].Reason;
                else
                    model.Message = customerResponse.Message;

                model.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                model.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(model.CommunicationStateId.ToString()));
            }
            else
            {
                model.Message = customerResponse.Data[0].Reason;
            }

            return model;
        }

        public async Task<AshokLeylandCardCreationModel> GetMultipleOTCCardPartialView([FromBody] List<ALCardEntryDetails> arrs)
        {
            AshokLeylandCardCreationModel addAddOnCard = new AshokLeylandCardCreationModel();

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            addAddOnCard.NoOfCards = arrs[0].NoOfCards;
            addAddOnCard.DealerCode = arrs[0].DealerCode;

            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].CardNo))))
                addAddOnCard.ObjALCardEntryDetail = arrs;

            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();

            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            return addAddOnCard;
        }

        public async Task<GetAlCustomerDetailForVerificationModel> VerifyCustomerDocuments(GetAlCustomerDetailForVerificationModel model)
        {
            model.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

            model.StatusResponseMdl.AddRange(await GetAlStatusType());
            if (model.Status == 0)
                model.Status = 9;//Pending

            var reqBody = new GetAlCustomerDetailForVerificationRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = string.IsNullOrEmpty(model.FormNumber) ? "" : model.FormNumber,
                CustomerName = string.IsNullOrEmpty(model.CustomerName) ? "" : model.CustomerName,
                StateID = model.StateID,
                Status = model.Status
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAlCustomerDetailForVerification);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetAlCustomerDetailForVerificationModel res = obj.ToObject<GetAlCustomerDetailForVerificationModel>();

            if (res != null && res.Data != null && res.Data.Count > 0)
            {
                model.Data = res.Data;
            }
            else
            {
                model.Data = new List<GetAlCustomerDetailForVerificationDetails>();
            }
            model.Message = res.Message;
            model.Internel_Status_Code = res.Internel_Status_Code;

            return model;
        }
        public async Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> AproveRejectCustomer(string CustomerID, string Comments, string Approvalstatus)
        {
            var approvalBody = new VerifyCustomerDocumentsRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                customerID = CustomerID,
                customerStatus = Approvalstatus,
                remarks = String.IsNullOrEmpty(Comments) ? "" : Comments
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(approvalBody), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.updateAlCustomerStatus);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> insertKyc = jarr.ToObject<List<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse>>();

            return insertKyc[0];
        }
        public async Task<List<StatusResponseModal>> GetAlStatusType()
        {
            var statusType = new StatusRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(statusType), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAlCustomerStatus);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<StatusResponseModal> lst = jarr.ToObject<List<StatusResponseModal>>();

            return lst;
        }

        public async Task<ALCustomerProfileModel> ManageProfile()
        {
            ALCustomerProfileModel custMdl = new ALCustomerProfileModel();

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

        public async Task<List<CustomerProfileResponse>> BindCustomerDetailsForSearch(string CustomerId, string NameOnCard)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var searchBody = new CustomerProfileModel();
                if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
                {
                    searchBody = new CustomerProfileModel
                    {
                        UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                        UserAgent = CommonBase.useragent,
                        UserIp = CommonBase.userip,
                        CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                    };
                }
                else
                {
                    searchBody = new CustomerProfileModel
                    {
                        UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                        UserAgent = CommonBase.useragent,
                        UserIp = CommonBase.userip,
                        CustomerId = string.IsNullOrEmpty(CustomerId) ? "" : CustomerId,
                        NameOnCard = string.IsNullOrEmpty(NameOnCard) ? "" : NameOnCard
                    };
                }

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.getAlCustomerDetails);

                JObject customerResponse = JObject.Parse(JsonConvert.DeserializeObject(contentString).ToString());

                var jarr = customerResponse["Data"].Value<JObject>();

                var customerResult = jarr["GetCustomerDetails"].Value<JArray>();
                List<CustomerProfileResponse> customerProfileResponse = customerResult.ToObject<List<CustomerProfileResponse>>();

                if (customerProfileResponse != null && customerProfileResponse.Count > 0)
                {
                    foreach (CustomerProfileResponse response in customerProfileResponse)
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
                        //if (response.KeyOfficialApproxMonthlySpendsonVechile1 == "0")
                        //    response.KeyOfficialApproxMonthlySpendsonVechile1 = "";
                        //if (response.KeyOfficialApproxMonthlySpendsonVechile2 == "0")
                        //    response.KeyOfficialApproxMonthlySpendsonVechile2 = "";

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
                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.searchAlManageCard);

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

        public async Task<InsertResponse> UpdateAlCostomerProfile(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<UpdateAlCustomerProfileRequest> arrs = objs.ToObject<List<UpdateAlCustomerProfileRequest>>();

            string dateOfApplication = await _commonActionService.changeDateFormat(arrs[0].DateOfApplication);
            string signedOnDate = await _commonActionService.changeDateFormat(arrs[0].SignedOnDate);

            var insertServiceBody = new UpdateAlCustomerProfileRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID = arrs[0].CustomerID,
                ZonalOffice = arrs[0].ZonalOffice,
                RegionalOffice = arrs[0].RegionalOffice,
                formNumber = "0",
                DateOfApplication = dateOfApplication,
                salesArea = arrs[0].salesArea,
                IndividualOrgNameTitle = arrs[0].IndividualOrgNameTitle,
                IndividualOrgName = arrs[0].IndividualOrgName,
                NameOnCard = arrs[0].NameOnCard,
                TypeOfBusinessEntity = arrs[0].TypeOfBusinessEntity,
                ResidenceStatus = arrs[0].ResidenceStatus,
                IncomeTaxPan = arrs[0].IncomeTaxPan,
                CommunicationAddress1 = arrs[0].CommunicationAddress1,
                CommunicationAddress2 = arrs[0].CommunicationAddress2,
                CommunicationAddress3 = arrs[0].CommunicationAddress3,
                CommunicationLocation = arrs[0].CommunicationLocation,
                CommunicationCityName = arrs[0].CommunicationCityName,
                CommunicationPincode = arrs[0].CommunicationPincode,
                CommunicationStateId = arrs[0].CommunicationStateId,
                CommunicationDistrictId = arrs[0].CommunicationDistrictId,
                CommunicationPhoneNo = arrs[0].CommunicationPhoneNo,
                CommunicationFax = arrs[0].CommunicationFax,
                CommunicationMobileNo = arrs[0].CommunicationMobileNo,
                CommunicationEmailid = arrs[0].CommunicationEmailid,
                SignedOnDate = signedOnDate
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(insertServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateAlCustomerDetail);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }
        public async Task<UploadDocResponseBody> UploadALDoc(string CustomerID)
        {
            UploadDocResponseBody UploadDocResponseBody = new UploadDocResponseBody();

            //if (!string.IsNullOrEmpty(FormNumber))
            //{
            //    JObject obj = await ViewCustomerDetails(FormNumber);

            //    var searchRes = obj["Data"].Value<JObject>();
            //    var custResult = searchRes["GetAggregatorCustomerDetails"].Value<JArray>();

            //    var customerKYCDetailsResult = searchRes["AggregatorCustomerKYCDetails"].Value<JArray>();

            //    List<CustomerFullDetails> customerList = custResult.ToObject<List<CustomerFullDetails>>();

            //    List<UploadDocResponseBody> UploadDocList = customerKYCDetailsResult.ToObject<List<UploadDocResponseBody>>();

            //    UploadDocResponseBody = UploadDocList.Where(t => t.FormNumber == FormNumber).FirstOrDefault();

            //}

            return UploadDocResponseBody;
        }
        public async Task<UploadDocResponse> UploadALDoc(UploadALDoc entity)
        {
            var searchBody = new UploadDoc
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FormNumber = entity.FormNumber,
                Type = String.IsNullOrEmpty(entity.Type) ? "0" : "1",
            };

            _httpContextAccessor.HttpContext.Session.SetString("FormNoVal", entity.FormNumber);

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getaggregatornameandformnumberbyreferenceno);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            UploadDocResponse searchCustomer = obj.ToObject<UploadDocResponse>();
            return searchCustomer;
        }

        public async Task<PendingKYCCustomerDetailsModel> ALPendingKYCCustomerDetail(PendingKYCCustomerDetailsModel Model)
        {
            var reqBody = new GetSMSAlertstoIndividualCardUsersRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = string.IsNullOrEmpty(Model.CustomerID) ? "" : Model.CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAlPendingKycCustomer);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            PendingKYCCustomerDetailsModel res = obj.ToObject<PendingKYCCustomerDetailsModel>();

            Model.Message = res.Message;
            Model.Internel_Status_Code = res.Internel_Status_Code;
            if (res != null && res.Data != null && res.Data.Count > 0 && res.Data[0].Status == 1)
            {
                Model.Data = res.Data;
            }
            else if (res != null && res.Data != null && res.Data.Count > 0 && res.Data[0].Status == 0)
            {
                Model.Message = res.Data[0].Reason;
                Model.Data = new List<PendingKYCCustomerData>();
            }
            else
            {
                Model.Data = new List<PendingKYCCustomerData>();
            }

            return Model;
        }
        public async Task<InsertResponse> ResetAlDealerPassword(string UserName)
        {
            var requestBody = new UpdateAlDealePasswordReset
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserName = UserName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateAlCommunicationEmailResetPassword);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }

    }
}