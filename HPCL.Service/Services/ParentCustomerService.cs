using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Customer;
using HPCL.Common.Models.RequestModel.ParentCustomer;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.ParentCustomer;
using HPCL.Common.Models.ViewModel.Aggregator;
using HPCL.Common.Models.ViewModel.CustomerRequest;
using HPCL.Common.Models.ViewModel.ParentCustomer;
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
    public class ParentCustomerService : IParentCustomerService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;
        public ParentCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }

        public async Task<ManageProfileViewModel> ManageProfile(string CustomerId, string NameOnCard)
        {
            ManageProfileViewModel custMdl = new ManageProfileViewModel();
            if (CustomerId != null || NameOnCard != null && (CustomerId != "" || NameOnCard != ""))
            {
                var reqForms = new ManageProfilerequestModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerId = CustomerId,
                    NameOnCard = NameOnCard,
                };

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reqForms), Encoding.UTF8, "application/json");

                var responseContent = await _requestService.CommonRequestService(stringContent, WebApiUrl.getparentcustomerforUpdate);

                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
                custMdl = obj.ToObject<ManageProfileViewModel>();
            }
            custMdl.Remarks = "";
            custMdl.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            custMdl.CustomerTypeMdl.AddRange(await _commonActionService.GetCustomerTypeListDropdown());
            custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType("1"));

            custMdl.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

            custMdl.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

            custMdl.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

            custMdl.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

            custMdl.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
            if (string.IsNullOrEmpty(custMdl.ExternalPANAPIStatus))
            {
                custMdl.ExternalPANAPIStatus = "Y";
            }

            return custMdl;
        }
        public async Task<ManageProfileViewModel> ManageProfile(ManageProfileViewModel cust)
        {
            if (cust.InterState)
            {
                cust.AreaOfOperation = "Inter State";
            }
            if (cust.InterCity)
            {
                if (string.IsNullOrEmpty(cust.AreaOfOperation))
                    cust.AreaOfOperation = "Inter City";
                else
                    cust.AreaOfOperation = cust.AreaOfOperation + ",Inter City";
            }
            if (cust.IntraCity)
            {
                if (string.IsNullOrEmpty(cust.AreaOfOperation))
                    cust.AreaOfOperation = "Intra City";
                else
                    cust.AreaOfOperation = cust.AreaOfOperation + ",Intra City";
            }

            if (!string.IsNullOrEmpty(cust.CommunicationEmail))
            {
                cust.CommunicationEmail = cust.CommunicationEmail.ToLower();
            }
            if (!string.IsNullOrEmpty(cust.KeyOffEmail))
            {
                cust.KeyOffEmail = cust.KeyOffEmail.ToLower();
            }

            string customerDateOfApplication = "";
            string KeyOffDateOfAnniversary = "";
            string KeyOfficialDOB = "";
            customerDateOfApplication = await _commonActionService.changeDateFormat(cust.CustomerDateOfApplication);

            if (!string.IsNullOrEmpty(cust.KeyOffDateOfAnniversary))
            {
                KeyOffDateOfAnniversary = await _commonActionService.changeDateFormat(cust.KeyOffDateOfAnniversary);
            }

            if (!string.IsNullOrEmpty(cust.KeyOfficialDOB))
            {
                KeyOfficialDOB = await _commonActionService.changeDateFormat(cust.KeyOfficialDOB);
            }
            string CustomerTypeID = "981", CustomerSubTypeID = "913";//For Parent Customer

            var CustomerTypeForms = new CustomerCreationRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerType = CustomerTypeID.ToString(),
                CustomerSubtype = CustomerSubTypeID.ToString(),
                ZonalOffice = cust.CustomerZonalOfficeID.ToString(),
                RegionalOffice = cust.CustomerRegionID.ToString(),
                DateOfApplication = customerDateOfApplication,
                SalesArea = cust.CustomerSalesAreaID.ToString(),
                IndividualOrgNameTitle = cust.IndividualOrgNameTitle,
                IndividualOrgName = cust.IndividualOrgName,
                NameOnCard = (String.IsNullOrEmpty(cust.CustomerNameOnCard) ? "" : cust.CustomerNameOnCard),
                TypeOfBusinessEntity = cust.CustomerTbentityID.ToString(),
                ResidenceStatus = cust.CustomerResidenceStatus,
                IncomeTaxPan = cust.CustomerIncomeTaxPan,
                CommunicationAddress1 = cust.CommunicationAddress1,
                CommunicationAddress2 = cust.CommunicationAddress2,
                CommunicationAddress3 = (String.IsNullOrEmpty(cust.CommunicationAddress3) ? "" : cust.CommunicationAddress3),
                CommunicationLocation = (String.IsNullOrEmpty(cust.CommunicationLocation) ? "" : cust.CommunicationLocation),
                CommunicationCityName = (String.IsNullOrEmpty(cust.CommunicationCity) ? "" : cust.CommunicationCity),
                CommunicationPincode = cust.CommunicationPinCode,
                CommunicationStateId = cust.CommunicationStateID.ToString(),
                CommunicationDistrictId = cust.CommunicationDistrictId.ToString(),
                CommunicationPhoneNo = (String.IsNullOrEmpty(cust.CommunicationDialCode) ? "" : cust.CommunicationDialCode) + "-" + (String.IsNullOrEmpty(cust.CommunicationPhoneNo) ? "" : cust.CommunicationPhoneNo),
                CommunicationFax = (String.IsNullOrEmpty(cust.CommunicationFaxCode) ? "" : cust.CommunicationFaxCode) + "-" + (String.IsNullOrEmpty(cust.CommunicationFax) ? "" : cust.CommunicationFax),
                CommunicationMobileNo = cust.CommunicationMobileNumber,
                CommunicationEmailid = cust.CommunicationEmail,
                PermanentAddress1 = cust.PerOrRegAddress1,
                PermanentAddress2 = cust.PerOrRegAddress2,
                PermanentAddress3 = (String.IsNullOrEmpty(cust.PerOrRegAddress3) ? "" : cust.PerOrRegAddress3),
                PermanentLocation = (String.IsNullOrEmpty(cust.PerOrRegAddressLocation) ? "" : cust.PerOrRegAddressLocation),
                PermanentCityName = cust.PerOrRegAddressCity,
                PermanentPincode = cust.PerOrRegAddressPinCode,
                PermanentStateId = cust.PerOrRegAddressStateID.ToString(),
                PermanentDistrictId = (cust.PermanentDistrictId == 0 ? cust.CommunicationDistrictId.ToString() : cust.PermanentDistrictId.ToString()),
                PermanentPhoneNo = (String.IsNullOrEmpty(cust.PerOrRegAddressDialCode) ? "" : cust.PerOrRegAddressDialCode) + "-" + (String.IsNullOrEmpty(cust.PerOrRegAddressPhoneNumber) ? "" : cust.PerOrRegAddressPhoneNumber),
                PermanentFax = (String.IsNullOrEmpty(cust.PermanentFaxCode) ? "" : cust.PermanentFaxCode) + "-" + (String.IsNullOrEmpty(cust.PermanentFax) ? "" : cust.PermanentFax),
                KeyOfficialTitle = cust.KeyOffTitle,
                KeyOfficialIndividualInitials = cust.KeyOffIndividualInitials,
                KeyOfficialFirstName = (String.IsNullOrEmpty(cust.KeyOffFirstName) ? "" : cust.KeyOffFirstName),
                KeyOfficialMiddleName = (String.IsNullOrEmpty(cust.KeyOffMiddleName) ? "" : cust.KeyOffMiddleName),
                KeyOfficialLastName = (String.IsNullOrEmpty(cust.KeyOffLastName) ? "" : cust.KeyOffLastName),
                KeyOfficialFax = (String.IsNullOrEmpty(cust.KeyOffFaxCode) ? "" : cust.KeyOffFaxCode) + "-" + (String.IsNullOrEmpty(cust.KeyOffFax) ? "" : cust.KeyOffFax),
                KeyOfficialDesignation = cust.KeyOffDesignation,
                KeyOfficialEmail = cust.KeyOffEmail,
                KeyOfficialPhoneNo = (String.IsNullOrEmpty(cust.KeyOffPhoneCode) ? "" : cust.KeyOffPhoneCode) + "-" + (String.IsNullOrEmpty(cust.KeyOffPhoneNumber) ? "" : cust.KeyOffPhoneNumber),
                KeyOfficialDOA = (string.IsNullOrEmpty(KeyOffDateOfAnniversary) ? "1900-01-01" : KeyOffDateOfAnniversary),
                KeyOfficialMobile = cust.KeyOffMobileNumber,
                KeyOfficialDOB = (string.IsNullOrEmpty(KeyOfficialDOB) ? "1900-01-01" : KeyOfficialDOB),
                KeyOfficialSecretQuestion = "0",
                KeyOfficialSecretAnswer = null,
                KeyOfficialTypeOfFleet = cust.CustomerTypeOfFleetID,
                KeyOfficialCardAppliedFor = (String.IsNullOrEmpty(cust.KeyOfficialCardAppliedFor) ? "" : cust.KeyOfficialCardAppliedFor),
                KeyOfficialApproxMonthlySpendsonVechile1 = (String.IsNullOrEmpty(cust.KeyOfficialApproxMonthlySpendsonVechile1) ? "0" : cust.KeyOfficialApproxMonthlySpendsonVechile1),
                KeyOfficialApproxMonthlySpendsonVechile2 = (String.IsNullOrEmpty(cust.KeyOfficialApproxMonthlySpendsonVechile2) ? "0" : cust.KeyOfficialApproxMonthlySpendsonVechile2),
                AreaOfOperation = cust.AreaOfOperation,
                FleetSizeNoOfVechileOwnedHCV = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedHCV) ? "0" : cust.FleetSizeNoOfVechileOwnedHCV),
                FleetSizeNoOfVechileOwnedLCV = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedLCV) ? "0" : cust.FleetSizeNoOfVechileOwnedLCV),
                FleetSizeNoOfVechileOwnedMUVSUV = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedMUVSUV) ? "0" : cust.FleetSizeNoOfVechileOwnedMUVSUV),
                FleetSizeNoOfVechileOwnedCarJeep = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedCarJeep) ? "0" : cust.FleetSizeNoOfVechileOwnedCarJeep),
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = cust.FormNumber.ToString(),
                PanCardRemarks = (String.IsNullOrEmpty(cust.PanCardRemarks) ? "" : cust.PanCardRemarks),
                RBEId = ""
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

            var contentString = await _requestService.CommonRequestService(content, WebApiUrl.insertparentcustomer);
            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(contentString);
            cust.Internel_Status_Code = customerResponse.Internel_Status_Code;

            if (customerResponse.Data != null)
            {
                cust.Remarks = customerResponse.Data[0].Reason;
                cust.CustomerReferenceNo = customerResponse.Data[0].CustomerReferenceNo;
            }
            else
                cust.Remarks = customerResponse.Message;

            if (cust.Internel_Status_Code != 1000)
            {
                //cust.CustomerTypeMdl.AddRange(await _commonActionService.GetCustomerTypeListDropdown());

                //cust.CustomerSubTypeMdl.AddRange(await _commonActionService.GetCustomerSubTypeDropdown(cust.CustomerTypeID));
                cust.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());

                cust.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType("1"));

                cust.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(cust.CustomerZonalOfficeID));

                cust.SalesAreaMdl.AddRange(await _commonActionService.GetSalesAreaDropdown(cust.CustomerRegionID.ToString()));

                cust.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

                cust.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

                cust.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.CommunicationStateID.ToString()));

                cust.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

                cust.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

                cust.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

                cust.PerOrRegAddressDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.PerOrRegAddressStateID.ToString()));
            }

            return cust;
        }
        public async Task<ParentCustomerApprovalModel> ApproveParentCustomer(ParentCustomerApprovalModel ApprovalMdl)
        {
            string fDate = "", tDate = "";
            if (!string.IsNullOrEmpty(ApprovalMdl.FromDate) && !string.IsNullOrEmpty(ApprovalMdl.FromDate))
            {
                fDate = await _commonActionService.changeDateFormat(ApprovalMdl.FromDate);
                tDate = await _commonActionService.changeDateFormat(ApprovalMdl.ToDate);
            }
            else
            {
                ApprovalMdl.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                ApprovalMdl.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var reqForms = new ApproveParentCustomer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = fDate,
                ToDate = tDate
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reqForms), Encoding.UTF8, "application/json");

            var responseContent = await _requestService.CommonRequestService(stringContent, WebApiUrl.getparentcustomerapproval);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
            ParentCustomerApprovalModel parentCustomerApprovalModel = obj.ToObject<ParentCustomerApprovalModel>();
            return parentCustomerApprovalModel;
        }
        public async Task<List<string>> ActionParentCustomerApproval([FromBody] ApproveParentCustomer approveParentCustomer)
        {

            var reqForms = new ApproveParentCustomer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                ActionType = approveParentCustomer.Action == "Approve" ? 1 : 13,
                Approvedby = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjParentCustomerDtl = approveParentCustomer.ObjParentCustomerDtl
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reqForms), Encoding.UTF8, "application/json");
            var responseContent = await _requestService.CommonRequestService(stringContent, WebApiUrl.actionparentcustomerapproval);
            JObject respObj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
            List<string> messageList = new List<string>();

            if (respObj["Status_Code"].ToString() == "200")
            {
                var respJarr = respObj["Data"].Value<JArray>();
                List<SuccessResponse> successResponseList = respJarr.ToObject<List<SuccessResponse>>();

                if (successResponseList.Count > 0)
                {
                    foreach (var item in successResponseList)
                        messageList.Add(item.Reason);
                }
                return messageList;
            }
            else
            {
                messageList.Add(respObj["Message"].ToString());
                return messageList;
            }
        }
        public async Task<ParentCustomerApprovalModel> AuthorizeParentCustomer(ParentCustomerApprovalModel ApprovalMdl)
        {
            string fDate = "", tDate = "";
            if (!string.IsNullOrEmpty(ApprovalMdl.FromDate) && !string.IsNullOrEmpty(ApprovalMdl.FromDate))
            {
                fDate = await _commonActionService.changeDateFormat(ApprovalMdl.FromDate);
                tDate = await _commonActionService.changeDateFormat(ApprovalMdl.ToDate);
            }
            else
            {
                ApprovalMdl.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                ApprovalMdl.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var reqForms = new ApproveParentCustomer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = fDate,
                ToDate = tDate
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reqForms), Encoding.UTF8, "application/json");

            var responseContent = await _requestService.CommonRequestService(stringContent, WebApiUrl.getparentcustomerauth);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
            ParentCustomerApprovalModel parentCustomerApprovalModel = obj.ToObject<ParentCustomerApprovalModel>();
            return parentCustomerApprovalModel;
        }
        public async Task<List<string>> ActionParentCustomerAuthorize([FromBody] ApproveParentCustomer approveParentCustomer)
        {

            var reqForms = new ApproveParentCustomer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                ActionType = approveParentCustomer.Action == "Approve" ? 22 : 21,
                Approvedby = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjParentCustomerDtl = approveParentCustomer.ObjParentCustomerDtl,

            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reqForms), Encoding.UTF8, "application/json");
            var responseContent = await _requestService.CommonRequestService(stringContent, WebApiUrl.actionparentcustomerauth);
            JObject respObj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
            List<string> messageList = new List<string>();

            if (respObj["Status_Code"].ToString() == "200")
            {
                var respJarr = respObj["Data"].Value<JArray>();
                List<ParentCustomerSuccessResponse> successResponseList = respJarr.ToObject<List<ParentCustomerSuccessResponse>>();

                if (successResponseList.Count > 0)
                {
                    messageList.Add(successResponseList[0].Reason);
                    foreach (var item in successResponseList)
                        messageList.Add(item.customerId);
                }
                return messageList;
            }
            else
            {
                messageList.Add(respObj["Message"].ToString());
                return messageList;
            }
        }
        public async Task<ViewCustomerCardorDispatchDetails> GetCardDetails(string CustomerId, string RequestId)
        {

            var reqForms = new ManageProfilerequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
                RequestId = RequestId
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reqForms), Encoding.UTF8, "application/json");

            var responseContent = await _requestService.CommonRequestService(stringContent, WebApiUrl.getparentcustomercarddetails);

            JObject resObj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
            ViewCustomerCardorDispatchDetails responseData = JsonConvert.DeserializeObject<ViewCustomerCardorDispatchDetails>(responseContent);
            var jObjJarr = resObj["Data"].Value<JArray>();
            List<ViewCardDetails> viewCardDetailsList = jObjJarr.ToObject<List<ViewCardDetails>>();
            responseData.CardData.AddRange(viewCardDetailsList);
            responseData.Search = "Card";
            return responseData;
        }
        public async Task<ViewCustomerCardorDispatchDetails> GetDispatchDetails(string CustomerId, string RequestId)
        {

            var reqForms = new ManageProfilerequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
                RequestId = RequestId
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reqForms), Encoding.UTF8, "application/json");

            var responseContent = await _requestService.CommonRequestService(stringContent, WebApiUrl.getparentcustomerdispatchdetails);
            JObject resObj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
            ViewCustomerCardorDispatchDetails responseData = JsonConvert.DeserializeObject<ViewCustomerCardorDispatchDetails>(responseContent);
            var jObjJarr = resObj["Data"].Value<JArray>();
            List<ViewDispatchDetails> viewDispatchDetailsList = jObjJarr.ToObject<List<ViewDispatchDetails>>();
            responseData.DispatchData.AddRange(viewDispatchDetailsList);
            responseData.Search = "Dispatch";
            return responseData;
        }
        public async Task<ManageProfileViewModel> UpdateParentCustomer(string customerId, string RequestId)
        {
            ManageProfileViewModel custMdl = new ManageProfileViewModel();
            custMdl.Remarks = "";

            int CustomerTypeID = 981, CustomerSubTypeID = 913;
            custMdl.CustomerSubTypeMdl.AddRange(await _commonActionService.GetCustomerSubTypeDropdown(CustomerTypeID));
            custMdl.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            //custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());

            custMdl.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

            custMdl.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

            custMdl.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

            custMdl.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

            custMdl.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
            if (string.IsNullOrEmpty(custMdl.ExternalPANAPIStatus))
            {
                custMdl.ExternalPANAPIStatus = "Y";
            }

            if (!string.IsNullOrEmpty(customerId))
            {
                var customerBody = new ManageProfilerequestModel()
                {
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CustomerId = customerId,
                    RequestId = RequestId
                };


                StringContent content = new StringContent(JsonConvert.SerializeObject(customerBody), Encoding.UTF8, "application/json");
                var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getparentcustomertoupdate);
                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());


                var searchRes = obj["Data"].Value<JObject>();
                var custResult = searchRes["GetParentCustomerDetails"].Value<JArray>();



                List<CustomerFullDetails> customerList = custResult.ToObject<List<CustomerFullDetails>>();

                CustomerFullDetails Customer = customerList.Where(t => t.CustomerID == customerId).FirstOrDefault();

                if (Customer != null)
                {
                    custMdl.FormNumber = Customer.FormNumber;
                    custMdl.CustomerId = customerId;
                    custMdl.RequestId = RequestId;
                    custMdl.CustomerTypeID = Convert.ToInt32(string.IsNullOrEmpty(Customer.CustomerTypeId) ? "0" : Customer.CustomerTypeId);
                    custMdl.SBUTypeId = string.IsNullOrEmpty(Customer.SBUTypeId) ? "0" : Customer.SBUTypeId;

                    custMdl.CustomerSubTypeID = Convert.ToInt32(string.IsNullOrEmpty(Customer.CustomerSubtypeId) ? "0" : Customer.CustomerSubtypeId);
                    List<CustomerZonalOfficeModel> lstZonalList = new List<CustomerZonalOfficeModel>();
                    lstZonalList = await _commonActionService.GetZonalOfficebySBUType(custMdl.SBUTypeId);
                    if (lstZonalList.Count > 0)
                    {
                        if (lstZonalList[0].ZonalOfficeName == "--Select--")
                            lstZonalList[0].ZonalOfficeName = "Select Zonal Office";
                    }

                    custMdl.CustomerZonalOfficeMdl.Clear();
                    custMdl.CustomerZonalOfficeMdl.AddRange(lstZonalList);

                    custMdl.CustomerZonalOfficeID = Convert.ToInt32(string.IsNullOrEmpty(Customer.ZonalOfficeID) ? "0" : Customer.ZonalOfficeID);

                    List<CustomerRegionModel> lstCustomerRegion = new List<CustomerRegionModel>();
                    lstCustomerRegion = await _commonActionService.GetRegionalDetailsDropdown(custMdl.CustomerZonalOfficeID);
                    if (lstCustomerRegion.Count > 0)
                    {
                        if (lstCustomerRegion[0].RegionalOfficeName == "--Select--")
                            lstCustomerRegion[0].RegionalOfficeName = "Select Regional Office";
                    }

                    custMdl.CustomerRegionMdl.Clear();
                    custMdl.CustomerRegionMdl.AddRange(lstCustomerRegion);

                    custMdl.CustomerRegionID = Convert.ToInt32(string.IsNullOrEmpty(Customer.RegionalOfficeID) ? "0" : Customer.RegionalOfficeID);

                    custMdl.SalesAreaMdl.AddRange(await _commonActionService.GetSalesAreaDropdown(custMdl.CustomerRegionID.ToString()));

                    if (!string.IsNullOrEmpty(Customer.DateOfApplication))
                    {
                        //string[] subs = Customer.DateOfApplication.Split(' ');
                        //string[] date = subs[0].Split('/');
                        custMdl.CustomerDateOfApplication = Customer.DateOfApplication;
                    }
                    custMdl.CustomerSalesAreaID = Convert.ToInt32(string.IsNullOrEmpty(Customer.SalesareaID) ? "0" : Customer.SalesareaID);
                    custMdl.IndividualOrgNameTitle = Customer.IndividualOrgNameTitle;
                    custMdl.IndividualOrgName = Customer.IndividualOrgName;
                    custMdl.CustomerNameOnCard = Customer.NameOnCard;
                    custMdl.CustomerTbentityID = Convert.ToInt32(string.IsNullOrEmpty(Customer.TypeOfBusinessEntityId) ? "0" : Customer.TypeOfBusinessEntityId);
                    custMdl.CustomerResidenceStatus = Customer.ResidenceStatus;
                    custMdl.CustomerIncomeTaxPan = Customer.IncomeTaxPan;
                    custMdl.CommunicationAddress1 = Customer.CommunicationAddress1;
                    custMdl.CommunicationAddress2 = Customer.CommunicationAddress2;
                    custMdl.CommunicationAddress3 = Customer.CommunicationAddress3;
                    custMdl.CommunicationLocation = Customer.CommunicationLocation;
                    custMdl.CommunicationCity = Customer.CommunicationCityName;
                    custMdl.CommunicationPinCode = Customer.CommunicationPincode;
                    custMdl.CommunicationStateID = Convert.ToInt32(string.IsNullOrEmpty(Customer.CommunicationStateId) ? "0" : Customer.CommunicationStateId);
                    if (custMdl.CommunicationDistrictMdl.Count() > 0)
                        custMdl.CommunicationDistrictMdl.RemoveAt(0);
                    custMdl.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(custMdl.CommunicationStateID.ToString()));

                    custMdl.CommunicationDistrictId = (string.IsNullOrEmpty(Customer.CommunicationDistrictId) ? "0" : Customer.CommunicationDistrictId);
                    custMdl.CommunicationEmail = Customer.CommunicationEmailid;
                    custMdl.CommunicationMobileNumber = Customer.CommunicationMobileNo;

                    if (!string.IsNullOrEmpty(Customer.CommunicationPhoneNo))
                    {
                        string[] subs = Customer.CommunicationPhoneNo.Split('-');

                        if (subs.Count() > 1)
                        {
                            custMdl.CommunicationDialCode = subs[0].ToString();
                            custMdl.CommunicationPhoneNo = subs[1].ToString();
                        }
                        else
                        {
                            custMdl.CommunicationPhoneNo = Customer.CommunicationPhoneNo;
                        }
                    }

                    if (!string.IsNullOrEmpty(Customer.CommunicationFax))
                    {
                        string[] subs = Customer.CommunicationFax.Split('-');

                        if (subs.Count() > 1)
                        {
                            custMdl.CommunicationFaxCode = subs[0].ToString();
                            custMdl.CommunicationFax = subs[1].ToString();
                        }
                        else
                        {
                            custMdl.CommunicationFax = Customer.CommunicationFax;
                        }
                    }

                    custMdl.PerOrRegAddress1 = Customer.PermanentAddress1;
                    custMdl.PerOrRegAddress2 = Customer.PermanentAddress2;
                    custMdl.PerOrRegAddress3 = Customer.PermanentAddress3;
                    custMdl.PerOrRegAddressLocation = Customer.PermanentLocation;
                    custMdl.PerOrRegAddressCity = Customer.PermanentCityName;
                    custMdl.PerOrRegAddressPinCode = Customer.PermanentPincode;
                    custMdl.PerOrRegAddressStateID = Convert.ToInt32(string.IsNullOrEmpty(Customer.PermanentStateId) ? "0" : Customer.PermanentStateId);
                    if (custMdl.PerOrRegAddressDistrictMdl.Count() > 0)
                        custMdl.PerOrRegAddressDistrictMdl.RemoveAt(0);
                    custMdl.PerOrRegAddressDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(custMdl.PerOrRegAddressStateID.ToString()));

                    custMdl.PermanentDistrictId = Convert.ToInt32(string.IsNullOrEmpty(Customer.PermanentDistrictId) ? "0" : Customer.PermanentDistrictId);

                    if (!string.IsNullOrEmpty(Customer.PermanentFax))
                    {
                        string[] subs = Customer.PermanentFax.Split('-');

                        if (subs.Count() > 1)
                        {
                            custMdl.PermanentFaxCode = subs[0].ToString();
                            custMdl.PermanentFax = subs[1].ToString();
                        }
                        else
                        {
                            custMdl.PermanentFax = Customer.PermanentFax;
                        }
                    }

                    if (!string.IsNullOrEmpty(Customer.PermanentPhoneNo))
                    {
                        string[] subs = Customer.PermanentPhoneNo.Split('-');

                        if (subs.Count() > 1)
                        {
                            custMdl.PerOrRegAddressDialCode = subs[0].ToString();
                            custMdl.PerOrRegAddressPhoneNumber = subs[1].ToString();
                        }
                        else
                        {
                            custMdl.PerOrRegAddressPhoneNumber = Customer.PermanentPhoneNo;
                        }
                    }

                    custMdl.KeyOffTitle = Customer.KeyOfficialTitle;
                    custMdl.KeyOffIndividualInitials = Customer.KeyOfficialIndividualInitials;
                    custMdl.KeyOffFirstName = Customer.KeyOfficialFirstName;
                    custMdl.KeyOffMiddleName = Customer.KeyOfficialMiddleName;
                    custMdl.KeyOffLastName = Customer.KeyOfficialLastName;

                    if (!string.IsNullOrEmpty(Customer.KeyOfficialFax))
                    {
                        string[] subs = Customer.KeyOfficialFax.Split('-');

                        if (subs.Count() > 1)
                        {
                            custMdl.KeyOffFaxCode = subs[0].ToString();
                            custMdl.KeyOffFax = subs[1].ToString();
                        }
                        else
                        {
                            custMdl.KeyOffFax = Customer.KeyOfficialFax;
                        }
                    }
                    custMdl.KeyOffDesignation = Customer.KeyOfficialDesignation;
                    custMdl.KeyOffEmail = Customer.KeyOfficialEmail;

                    if (!string.IsNullOrEmpty(Customer.KeyOfficialPhoneNo))
                    {
                        string[] subs = Customer.KeyOfficialPhoneNo.Split('-');

                        if (subs.Count() > 1)
                        {
                            custMdl.KeyOffPhoneCode = subs[0].ToString();
                            custMdl.KeyOffPhoneNumber = subs[1].ToString();
                        }
                        else
                        {
                            custMdl.KeyOffPhoneNumber = Customer.KeyOfficialPhoneNo;
                        }
                    }
                    custMdl.KeyOffMobileNumber = Customer.KeyOfficialMobile;
                    custMdl.KeyOfficialSecretQuestion = Customer.KeyOfficialSecretQuestionId;
                    custMdl.KeyOfficialSecretAnswer = Customer.KeyOfficialSecretAnswer;


                    if (!string.IsNullOrEmpty(Customer.KeyOfficialDOA))
                    {
                        if (!Customer.KeyOfficialDOA.Contains("1900"))
                        {
                            if (!Customer.KeyOfficialDOA.Contains('-'))
                            {
                                string[] subs = Customer.KeyOfficialDOA.Split(' ');
                                string[] date = subs[0].Split('/');
                                custMdl.KeyOffDateOfAnniversary = date[1] + "-" + date[0] + "-" + date[2];
                            }
                            else
                            {
                                custMdl.KeyOffDateOfAnniversary = Customer.KeyOfficialDOA;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(Customer.KeyOfficialDOB))
                    {
                        if (!Customer.KeyOfficialDOB.Contains("1900"))
                        {
                            if (!Customer.KeyOfficialDOB.Contains('-'))
                            {
                                string[] subs = Customer.KeyOfficialDOB.Split(' ');
                                string[] date = subs[0].Split('/');
                                custMdl.KeyOfficialDOB = date[1] + "-" + date[0] + "-" + date[2];
                            }
                            else
                            {
                                custMdl.KeyOfficialDOB = Customer.KeyOfficialDOB;
                            }
                        }
                    }

                    custMdl.CustomerTypeOfFleetID = (string.IsNullOrEmpty(Customer.KeyOfficialTypeOfFleetId) ? "0" : Customer.KeyOfficialTypeOfFleetId);
                    custMdl.KeyOfficialCardAppliedFor = Customer.KeyOfficialCardAppliedFor;
                    custMdl.KeyOfficialApproxMonthlySpendsonVechile1 = (string.IsNullOrEmpty(Customer.KeyOfficialApproxMonthlySpendsonVechile1) ? "" : Customer.KeyOfficialApproxMonthlySpendsonVechile1);
                    if (custMdl.KeyOfficialApproxMonthlySpendsonVechile1 == "0")
                        custMdl.KeyOfficialApproxMonthlySpendsonVechile1 = "";
                    custMdl.KeyOfficialApproxMonthlySpendsonVechile2 = (string.IsNullOrEmpty(Customer.KeyOfficialApproxMonthlySpendsonVechile2) ? "" : Customer.KeyOfficialApproxMonthlySpendsonVechile2);
                    if (custMdl.KeyOfficialApproxMonthlySpendsonVechile2 == "0")
                        custMdl.KeyOfficialApproxMonthlySpendsonVechile2 = "";
                    custMdl.FleetSizeNoOfVechileOwnedHCV = (string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedHCV) ? "" : Customer.FleetSizeNoOfVechileOwnedHCV);
                    if (custMdl.FleetSizeNoOfVechileOwnedHCV == "0")
                        custMdl.FleetSizeNoOfVechileOwnedHCV = "";
                    custMdl.FleetSizeNoOfVechileOwnedLCV = (string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedLCV) ? "" : Customer.FleetSizeNoOfVechileOwnedLCV);
                    if (custMdl.FleetSizeNoOfVechileOwnedLCV == "0")
                        custMdl.FleetSizeNoOfVechileOwnedLCV = "";
                    custMdl.FleetSizeNoOfVechileOwnedMUVSUV = (string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedMUVSUV) ? "" : Customer.FleetSizeNoOfVechileOwnedMUVSUV);
                    if (custMdl.FleetSizeNoOfVechileOwnedMUVSUV == "0")
                        custMdl.FleetSizeNoOfVechileOwnedMUVSUV = "";
                    custMdl.FleetSizeNoOfVechileOwnedCarJeep = (string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedCarJeep) ? "" : Customer.FleetSizeNoOfVechileOwnedCarJeep);
                    if (custMdl.FleetSizeNoOfVechileOwnedCarJeep == "0")
                        custMdl.FleetSizeNoOfVechileOwnedCarJeep = "";
                    custMdl.CustomerReferenceNo = Customer.CustomerReferenceNo;
                    custMdl.TierOfCustomerID = Convert.ToInt32(string.IsNullOrEmpty(Customer.TierOfCustomerId) ? "0" : Customer.TierOfCustomerId);
                    custMdl.TypeOfCustomerID = Convert.ToInt32(string.IsNullOrEmpty(Customer.TypeOfCustomerId) ? "0" : Customer.TypeOfCustomerId);
                    custMdl.PanCardRemarks = Customer.PanCardRemarks;

                    custMdl.IsDuplicatePanNo = "0";
                    custMdl.AllowPanDuplication = "N";
                    if (!string.IsNullOrEmpty(custMdl.PanCardRemarks))
                    {
                        custMdl.IsDuplicatePanNo = "1";
                        custMdl.AllowPanDuplication = "Y";
                    }

                    if (Customer.AreaOfOperation != null)
                    {
                        if (Customer.AreaOfOperation.Contains("Inter State"))
                            custMdl.InterState = true;
                        else
                            custMdl.InterState = false;

                        if (Customer.AreaOfOperation.Contains("Inter City"))
                            custMdl.InterCity = true;
                        else
                            custMdl.InterCity = false;

                        if (Customer.AreaOfOperation.Contains("Intra City"))
                            custMdl.IntraCity = true;
                        else
                            custMdl.IntraCity = false;
                    }
                }

            }

            return custMdl;
        }

        public async Task<ManageProfileViewModel> UpdateParentCustomer(ManageProfileViewModel cust)
        {
            if (cust.InterState)
            {
                cust.AreaOfOperation = "Inter State";
            }
            if (cust.InterCity)
            {
                if (string.IsNullOrEmpty(cust.AreaOfOperation))
                    cust.AreaOfOperation = "Inter City";
                else
                    cust.AreaOfOperation = cust.AreaOfOperation + ",Inter City";
            }
            if (cust.IntraCity)
            {
                if (string.IsNullOrEmpty(cust.AreaOfOperation))
                    cust.AreaOfOperation = "Intra City";
                else
                    cust.AreaOfOperation = cust.AreaOfOperation + ",Intra City";
            }

            if (!string.IsNullOrEmpty(cust.CommunicationEmail))
            {
                cust.CommunicationEmail = cust.CommunicationEmail.ToLower();
            }
            if (!string.IsNullOrEmpty(cust.KeyOffEmail))
            {
                cust.KeyOffEmail = cust.KeyOffEmail.ToLower();
            }

            string customerDateOfApplication = "";
            string KeyOffDateOfAnniversary = "";
            string KeyOfficialDOB = "";


            customerDateOfApplication = await _commonActionService.changeDateFormat(cust.CustomerDateOfApplication);

            if (!string.IsNullOrEmpty(cust.KeyOffDateOfAnniversary))
            {
                KeyOffDateOfAnniversary = await _commonActionService.changeDateFormat(cust.KeyOffDateOfAnniversary);
            }

            if (!string.IsNullOrEmpty(cust.KeyOfficialDOB))
            {
                KeyOfficialDOB = await _commonActionService.changeDateFormat(cust.KeyOfficialDOB);
            }

            string CustomerTypeID = "981", CustomerSubTypeID = "913";//For Parent Customer

            var CustomerTypeForms = new CustomerCreationRequestModel
            {
                RequestId = cust.RequestId,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerReferenceNo = cust.CustomerReferenceNo,
                CustomerId = cust.CustomerId,
                ZonalOffice = cust.CustomerZonalOfficeID.ToString(),
                RegionalOffice = cust.CustomerRegionID.ToString(),
                DateOfApplication = customerDateOfApplication,
                SalesArea = cust.CustomerSalesAreaID.ToString(),
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                IndividualOrgNameTitle = cust.IndividualOrgNameTitle,
                IndividualOrgName = cust.IndividualOrgName,
                NameOnCard = (String.IsNullOrEmpty(cust.CustomerNameOnCard) ? "" : cust.CustomerNameOnCard),
                TypeOfBusinessEntity = cust.CustomerTbentityID.ToString(),
                ResidenceStatus = cust.CustomerResidenceStatus,
                IncomeTaxPan = cust.CustomerIncomeTaxPan,
                CommunicationAddress1 = cust.CommunicationAddress1,
                CommunicationAddress2 = cust.CommunicationAddress2,
                CommunicationAddress3 = (String.IsNullOrEmpty(cust.CommunicationAddress3) ? "" : cust.CommunicationAddress3),
                CommunicationLocation = (String.IsNullOrEmpty(cust.CommunicationLocation) ? "" : cust.CommunicationLocation),
                CommunicationCityName = (String.IsNullOrEmpty(cust.CommunicationCity) ? "" : cust.CommunicationCity),
                CommunicationPincode = cust.CommunicationPinCode,
                CommunicationStateId = cust.CommunicationStateID.ToString(),
                CommunicationDistrictId = cust.CommunicationDistrictId.ToString(),
                CommunicationPhoneNo = (String.IsNullOrEmpty(cust.CommunicationDialCode) ? "" : cust.CommunicationDialCode) + "-" + (String.IsNullOrEmpty(cust.CommunicationPhoneNo) ? "" : cust.CommunicationPhoneNo),
                CommunicationFax = (String.IsNullOrEmpty(cust.CommunicationFaxCode) ? "" : cust.CommunicationFaxCode) + "-" + (String.IsNullOrEmpty(cust.CommunicationFax) ? "" : cust.CommunicationFax),
                CommunicationMobileNo = cust.CommunicationMobileNumber,
                CommunicationEmailid = cust.CommunicationEmail,
                PermanentAddress1 = cust.PerOrRegAddress1,
                PermanentAddress2 = cust.PerOrRegAddress2,
                PermanentAddress3 = (String.IsNullOrEmpty(cust.PerOrRegAddress3) ? "" : cust.PerOrRegAddress3),
                PermanentLocation = (String.IsNullOrEmpty(cust.PerOrRegAddressLocation) ? "" : cust.PerOrRegAddressLocation),
                PermanentCityName = cust.PerOrRegAddressCity,
                PermanentPincode = cust.PerOrRegAddressPinCode,
                PermanentStateId = cust.PerOrRegAddressStateID.ToString(),
                PermanentDistrictId = (cust.PermanentDistrictId == 0 ? cust.CommunicationDistrictId.ToString() : cust.PermanentDistrictId.ToString()),
                PermanentPhoneNo = (String.IsNullOrEmpty(cust.PerOrRegAddressDialCode) ? "" : cust.PerOrRegAddressDialCode) + "-" + (String.IsNullOrEmpty(cust.PerOrRegAddressPhoneNumber) ? "" : cust.PerOrRegAddressPhoneNumber),
                PermanentFax = (String.IsNullOrEmpty(cust.PermanentFaxCode) ? "" : cust.PermanentFaxCode) + "-" + (String.IsNullOrEmpty(cust.PermanentFax) ? "" : cust.PermanentFax),
                KeyOfficialTitle = cust.KeyOffTitle,
                KeyOfficialIndividualInitials = cust.KeyOffIndividualInitials,
                KeyOfficialFirstName = (String.IsNullOrEmpty(cust.KeyOffFirstName) ? "" : cust.KeyOffFirstName),
                KeyOfficialMiddleName = (String.IsNullOrEmpty(cust.KeyOffMiddleName) ? "" : cust.KeyOffMiddleName),
                KeyOfficialLastName = (String.IsNullOrEmpty(cust.KeyOffLastName) ? "" : cust.KeyOffLastName),
                KeyOfficialFax = (String.IsNullOrEmpty(cust.KeyOffFaxCode) ? "" : cust.KeyOffFaxCode) + "-" + (String.IsNullOrEmpty(cust.KeyOffFax) ? "" : cust.KeyOffFax),
                KeyOfficialDesignation = cust.KeyOffDesignation,
                KeyOfficialEmail = cust.KeyOffEmail,
                KeyOfficialPhoneNo = (String.IsNullOrEmpty(cust.KeyOffPhoneCode) ? "" : cust.KeyOffPhoneCode) + "-" + (String.IsNullOrEmpty(cust.KeyOffPhoneNumber) ? "" : cust.KeyOffPhoneNumber),
                KeyOfficialDOA = (string.IsNullOrEmpty(KeyOffDateOfAnniversary) ? "1900-01-01" : KeyOffDateOfAnniversary),
                KeyOfficialMobile = cust.KeyOffMobileNumber,
                KeyOfficialDOB = (string.IsNullOrEmpty(KeyOfficialDOB) ? "1900-01-01" : KeyOfficialDOB),
                KeyOfficialSecretQuestion = "0",
                KeyOfficialSecretAnswer = null,
                KeyOfficialTypeOfFleet = cust.CustomerTypeOfFleetID,
                KeyOfficialCardAppliedFor = (String.IsNullOrEmpty(cust.KeyOfficialCardAppliedFor) ? "" : cust.KeyOfficialCardAppliedFor),
                KeyOfficialApproxMonthlySpendsonVechile1 = (String.IsNullOrEmpty(cust.KeyOfficialApproxMonthlySpendsonVechile1) ? "0" : cust.KeyOfficialApproxMonthlySpendsonVechile1),
                KeyOfficialApproxMonthlySpendsonVechile2 = (String.IsNullOrEmpty(cust.KeyOfficialApproxMonthlySpendsonVechile2) ? "0" : cust.KeyOfficialApproxMonthlySpendsonVechile2),
                AreaOfOperation = cust.AreaOfOperation,
                FleetSizeNoOfVechileOwnedHCV = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedHCV) ? "0" : cust.FleetSizeNoOfVechileOwnedHCV),
                FleetSizeNoOfVechileOwnedLCV = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedLCV) ? "0" : cust.FleetSizeNoOfVechileOwnedLCV),
                FleetSizeNoOfVechileOwnedMUVSUV = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedMUVSUV) ? "0" : cust.FleetSizeNoOfVechileOwnedMUVSUV),
                FleetSizeNoOfVechileOwnedCarJeep = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedCarJeep) ? "0" : cust.FleetSizeNoOfVechileOwnedCarJeep),
                CustomerType = CustomerTypeID.ToString(),
                CustomerSubtype = CustomerSubTypeID.ToString(),
                TierOfCustomer = "0",
                TypeOfCustomer = "0",
                PanCardRemarks = (String.IsNullOrEmpty(cust.PanCardRemarks) ? "" : cust.PanCardRemarks)

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

            var contentString = await _requestService.CommonRequestService(content, WebApiUrl.updateparentcustomer);
            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(contentString);
            cust.Internel_Status_Code = customerResponse.Internel_Status_Code;

            if (customerResponse.Data != null)
            {
                cust.Remarks = customerResponse.Data[0].Reason;
                cust.CustomerReferenceNo = customerResponse.Data[0].CustomerReferenceNo;
            }
            else
                cust.Remarks = customerResponse.Message;

            if (cust.Internel_Status_Code != 1000)
            {
                //cust.CustomerTypeMdl.AddRange(await _commonActionService.GetCustomerTypeListDropdown());

                // cust.CustomerSubTypeMdl.AddRange(await _commonActionService.GetCustomerSubTypeDropdown(901));
                cust.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
                cust.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType("1"));

                cust.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(cust.CustomerZonalOfficeID));

                cust.SalesAreaMdl.AddRange(await _commonActionService.GetSalesAreaDropdown(cust.CustomerRegionID.ToString()));

                cust.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

                cust.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

                cust.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.CommunicationStateID.ToString()));

                cust.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

                cust.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

                cust.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

                cust.PerOrRegAddressDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.PerOrRegAddressStateID.ToString()));
            }

            return cust;
        }
        public async Task<ParentCustomerReportModel> SearchParentCustomerRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string FormNumber, string SBUtypeId)
        {
            ParentCustomerReportModel parentCustomerReportModel = new ParentCustomerReportModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                FromDate = await _commonActionService.changeDateFormat(FromDate);
                ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var searchBody = new ParentCustomerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FormId = FormNumber == null ? "0" : FormNumber,
                ZO = ZonalOfficeId == null ? "0" : ZonalOfficeId,
                RO = RegionalOfficeId == null ? "0" : RegionalOfficeId,
                FromDate = FromDate,
                ToDate = ToDate,
                SBUTypeId = SBUtypeId,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getparentcustomerstatus);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            parentCustomerReportModel = obj.ToObject<ParentCustomerReportModel>();
            return parentCustomerReportModel;
        }
        public async Task<ParentCustomerReportModel> SearchParentCustomerRequestStatusReport(string FormNumber, string RequestId)
        {
            ParentCustomerReportModel parentCustomerReportModel = new ParentCustomerReportModel();

            var searchBody = new ParentCustomerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FormNumber = FormNumber == null ? "0" : FormNumber,
                RequestId = RequestId

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getparentcustomerstatusreport);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            parentCustomerReportModel = obj.ToObject<ParentCustomerReportModel>();
            return parentCustomerReportModel;
        }

        public async Task<ParentCustomerBalanceInfoModel> GetCustomerBalanceInfo(ParentCustomerBalanceInfoModel requestInfo)
        {
            ParentCustomerBalanceInfoModel customerBalanceResponse = new ParentCustomerBalanceInfoModel();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ParentCustomerID = requestInfo.ParentCustomerID,
                ChildCustomerId = requestInfo.ChildCustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getparentcustomerbalanceinfo);

            customerBalanceResponse = JsonConvert.DeserializeObject<ParentCustomerBalanceInfoModel>(response);

            return customerBalanceResponse;
        }

        public async Task<GetCustomerCardWiseBalanceResponse> GetCustomerCardWiseBalance(string CustomerID)
        {
            GetCustomerCardWiseBalanceResponse customerBalanceResponse = new GetCustomerCardWiseBalanceResponse();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getparentcustomercardwisebalance);

            customerBalanceResponse = JsonConvert.DeserializeObject<GetCustomerCardWiseBalanceResponse>(response);

            return customerBalanceResponse;
        }
        public async Task<CustomerCCMSBalanceModel> GetCCMSBalanceDetails(string CustomerID)
        {
            CustomerCCMSBalanceModel customerCCMSBalance = new CustomerCCMSBalanceModel();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ChildCustomerId = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getparentccmsbalanceinfoforcustomerId);

            customerCCMSBalance = JsonConvert.DeserializeObject<CustomerCCMSBalanceModel>(response);

            return customerCCMSBalance;
        }
        public async Task<ParentCustomerBalanceInfoModel> GetCustomerDetailsByCustomerID(string CustomerID)
        {
            var request = new GetCustomerByCustomerIdRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getparentcustomerdetailbycustomerId);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var searchRes = obj["Data"].Value<JObject>();
            var custResult = searchRes["GetParentCustomerDetails"].Value<JArray>();

            List<ParentCustomerBalanceInfoModel> customerList = custResult.ToObject<List<ParentCustomerBalanceInfoModel>>();

            ParentCustomerBalanceInfoModel Customer = customerList.Where(t => t.CustomerId == CustomerID).FirstOrDefault();
            return Customer;
        }
        public async Task<ParentCustomerTransactionViewModel> ParentCustomerTransactionDetails(ParentCustomerTransactionViewModel requestInfo)
        {
            ParentCustomerTransactionViewModel customerBalanceResponse = new ParentCustomerTransactionViewModel();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ParentCustomerID = requestInfo.ParentCustomerID,
                ChildCustomerId = requestInfo.ChildCustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getparenttransactionssummary);

            customerBalanceResponse = JsonConvert.DeserializeObject<ParentCustomerTransactionViewModel>(response);

            return customerBalanceResponse;
        }
        public async Task<ParentChildCustomerMappingViewModel> ParentChildCustomerMapping(ParentChildCustomerMappingRequest requestInfo)
        {
            ParentChildCustomerMappingViewModel parentChildCustomerMapping = new ParentChildCustomerMappingViewModel();

            var Request = new ParentChildCustomerMappingRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ParentCustomerId = requestInfo.ParentCustomerId,
                ObjChildDtl = requestInfo.ObjChildDtl,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getchildmappingdetails);

            parentChildCustomerMapping = JsonConvert.DeserializeObject<ParentChildCustomerMappingViewModel>(response);

            return parentChildCustomerMapping;
        }
        public async Task<ViewParentChildTransactionDetailsModel> ViewParentChildTransactionDetails(String ParentCustomerID)
        {
            ViewParentChildTransactionDetailsModel customerTransactionResponse = new ViewParentChildTransactionDetailsModel();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ParentCustomerID = ParentCustomerID,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getchildbyparent);

            //customerBalanceResponse = JsonConvert.DeserializeObject<ParentCustomerTransactionViewModel>(response);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());


            var searchRes = obj["Data"].Value<JArray>();
            List<ChildCustomerDetails> searchList = searchRes.ToObject<List<ChildCustomerDetails>>();
            customerTransactionResponse.ParentCustomerID = ParentCustomerID;
            customerTransactionResponse.ChildCustomerIds.AddRange(searchList);
            return customerTransactionResponse;
        }
        public async Task<List<SuccessResponse>> ValidateChildCustomerId(string CustomerId)
        {
            var validateUserNameForms = new ParentChildCustomerMappingRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(validateUserNameForms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(stringContent, WebApiUrl.parentcustomerchildmappingeligibility);

            JObject jObj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jArr = jObj["Data"].Value<JArray>();
            List<SuccessResponse> responseList = jArr.ToObject<List<SuccessResponse>>();
            return responseList;
        }
        public async Task<ParentChildCustomerMappingViewModel> ConfirmParentChildCustomerMapping(ParentChildCustomerMappingRequest requestInfo)
        {
            ParentChildCustomerMappingViewModel parentChildCustomerMapping = new ParentChildCustomerMappingViewModel();

            var Request = new ParentChildCustomerMappingRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ParentCustomerId = requestInfo.ParentCustomerId,
                ObjParentCustomerDtl = requestInfo.ObjParentCustomerDtl,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.parentcustomerchildmapping);

            parentChildCustomerMapping = JsonConvert.DeserializeObject<ParentChildCustomerMappingViewModel>(response);

            return parentChildCustomerMapping;
        }
        public async Task<CustomerDriveStarsDetailsModel> GetDriveStarsDetails(string CustomerID)
        {
            CustomerDriveStarsDetailsModel customerDriveStarDetails = new CustomerDriveStarsDetailsModel();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.pcdrivestarsbalanceinfo);

            customerDriveStarDetails = JsonConvert.DeserializeObject<CustomerDriveStarsDetailsModel>(response);

            return customerDriveStarDetails;
        }
        public async Task<List<SuccessResponse>> ValidateParentCustomerId(string CustomerId)
        {
            var validateUserNameForms = new ParentChildCustomerMappingRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(validateUserNameForms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(stringContent, WebApiUrl.customerparentmappingeligibility);

            JObject jObj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jArr = jObj["Data"].Value<JArray>();
            List<SuccessResponse> responseList = jArr.ToObject<List<SuccessResponse>>();
            return responseList;
        }
        public async Task<ControlCardSearchModel> GetCustomerControlCard(string CustomerID)
        {
            ControlCardSearchModel controlCardDetails = new ControlCardSearchModel();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getparentcustomercontrolcardsearch);

            controlCardDetails = JsonConvert.DeserializeObject<ControlCardSearchModel>(response);

            return controlCardDetails;
        }
        public async Task<List<SuccessResponse>> SubmitRestPinforParentCustomer([FromBody] ControlCardPinRestRequestModel reqModel)
        {
            var reqBody = new ParentCustomerSearchRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = reqModel.CustomerID == "" ? null : reqModel.CustomerID,
                ControlCardNo = reqModel.ControlCardNo == "" ? null : reqModel.ControlCardNo,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.parentcustomercontrolcardpinreset);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res;
        }
        public async Task<List<SuccessResponse>> ConfigureSMSAlerts([FromBody] ParentCustomerSearchRequestModel reqModel)
        {
            var reqBody = new ParentCustomerSearchRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = reqModel.CustomerID,
                TypePCConfigureSMSAlerts = reqModel.TypePCConfigureSMSAlerts,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.pcupdateconfiguresmsalerts);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res;
        }
        public async Task<PCConfigureSMSAlertModel> GetPCAvailableSMSAlerts(string CustomerID)
        {
            PCConfigureSMSAlertModel configureSMS = new PCConfigureSMSAlertModel();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.pcconfiguresmsalerts);

            configureSMS = JsonConvert.DeserializeObject<PCConfigureSMSAlertModel>(response);

            return configureSMS;
        }
        public async Task<List<SuccessResponse>> UpdateDndSmsAlertsConfigure(string CustomerId)
        {
            var reqBody = new UpdateDndSmsAlertsConfigureReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.pcdndconfiguresmsalerts);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }
        public async Task<AccountStatementRequestViewModel> GetAccountStatementRequest(string CustomerID)
        {
            AccountStatementRequestViewModel accountStatementResponse = new AccountStatementRequestViewModel();

            var Request = new UpdateDndSmsAlertsConfigureReq()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getaccountstatmentrequestdetails);

            accountStatementResponse = JsonConvert.DeserializeObject<AccountStatementRequestViewModel>(response);

            return accountStatementResponse;
        }
        public async Task<List<SuccessResponse>> InsertAccountStatementRequest([FromBody] AccountStatementRequestModel reqEntity)
        {
            var reqBody = new AccountStatementRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = reqEntity.CustomerId,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                StatementType = reqEntity.StatementType,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertaccountstatmentrequest);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }
        public async Task<List<SuccessResponse>> UpdateAccountStatementRequest([FromBody] AccountStatementRequestModel reqEntity)
        {
            var reqBody = new AccountStatementRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = reqEntity.CustomerId,
                RequestId = reqEntity.RequestId,
                IsActivate = "0",
                StatementType = reqEntity.StatementType,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateaccountstatmentrequeststatus);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }
        public async Task<ViewParentChildTransactionDetailsModel> GetParentChildTransactionDetails(ParentChildransactionRequestModel requestInfo)
        {
            ViewParentChildTransactionDetailsModel transactionResponse = new ViewParentChildTransactionDetailsModel();
            if (!string.IsNullOrEmpty(requestInfo.FromDate) && !string.IsNullOrEmpty(requestInfo.FromDate))
            {
                requestInfo.FromDate = await _commonActionService.changeDateFormat(requestInfo.FromDate);
                requestInfo.ToDate = await _commonActionService.changeDateFormat(requestInfo.ToDate);
            }
            else
            {
                requestInfo.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                requestInfo.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var Request = new ParentChildransactionRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ParentCustomerID = requestInfo.ParentCustomerID,
                FromDate = requestInfo.FromDate,
                ToDate = requestInfo.ToDate,
                ObjChildCustomerIdDtl = requestInfo.ObjChildCustomerIdDtl
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getparenttransactionssummarydetails);

            transactionResponse = JsonConvert.DeserializeObject<ViewParentChildTransactionDetailsModel>(response);

            return transactionResponse;
        }
        public async Task<BasicSearchViewModel> CustomerBasicSearch(BasicSearchViewModel reqEntity)
        {
            BasicSearchViewModel searchResponse = new BasicSearchViewModel();

            var Request = new BasicSearchRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = reqEntity.CustomerId == "" ? null : reqEntity.CustomerId,
                FormNumber = reqEntity.FormNumber == "" ? null : reqEntity.FormNumber,
                MobileNo = reqEntity.MobileNumber == "" ? null : reqEntity.MobileNumber,
                NameonCard = reqEntity.NameOnCard == "" ? null : reqEntity.NameOnCard,
                CustomerName = reqEntity.CustomerName == "" ? null : reqEntity.CustomerName,
                CommunicationCityName = reqEntity.City == "" ? null : reqEntity.City,
                CommunicationStateId = reqEntity.StateId == null ? "0" : reqEntity.StateId

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getparentcustomerbasicsearch);

            searchResponse = JsonConvert.DeserializeObject<BasicSearchViewModel>(response);

            return searchResponse;
        }
        public async Task<ViewParentChildTransactionDetailsModel> GetTransactionLocationDetails(PCTransactionLocationrequest requestInfo)
        {
            ViewParentChildTransactionDetailsModel transactionResponse = new ViewParentChildTransactionDetailsModel();

            var Request = new PCTransactionLocationrequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = requestInfo.CustomerId,
                TransactionId = requestInfo.TransactionId,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getparenttransactionlocationdetails);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<TransactionLocationDetails> locationDetails = jarr.ToObject<List<TransactionLocationDetails>>();
            transactionResponse.TransactionLocationDetails.AddRange(locationDetails);
            return transactionResponse;
        }
        public async Task<ConvertParenttoAggregatorViewModel> ConvertParentToAggregator(string CustomerId, string NameOnCard)
        {
            ConvertParenttoAggregatorViewModel custMdl = new ConvertParenttoAggregatorViewModel();
            if (CustomerId != null || NameOnCard != null && (CustomerId != "" || NameOnCard != ""))
            {
                var reqForms = new ManageProfilerequestModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerId = CustomerId,
                    NameOnCard = NameOnCard,
                };

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reqForms), Encoding.UTF8, "application/json");

                var responseContent = await _requestService.CommonRequestService(stringContent, WebApiUrl.Convertparentcustomertoaggregator);

                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
                custMdl = obj.ToObject<ConvertParenttoAggregatorViewModel>();
            }

            return custMdl;
        }
        public async Task<ManageAggregatorViewModel> UpdateParentasAggregatorCustomer(ManageAggregatorViewModel cust)
        {
            if (cust.InterState)
            {
                cust.AreaOfOperation = "Inter State";
            }
            if (cust.InterCity)
            {
                if (string.IsNullOrEmpty(cust.AreaOfOperation))
                    cust.AreaOfOperation = "Inter City";
                else
                    cust.AreaOfOperation = cust.AreaOfOperation + ",Inter City";
            }
            if (cust.IntraCity)
            {
                if (string.IsNullOrEmpty(cust.AreaOfOperation))
                    cust.AreaOfOperation = "Intra City";
                else
                    cust.AreaOfOperation = cust.AreaOfOperation + ",Intra City";
            }

            if (!string.IsNullOrEmpty(cust.CommunicationEmail))
            {
                cust.CommunicationEmail = cust.CommunicationEmail.ToLower();
            }
            if (!string.IsNullOrEmpty(cust.KeyOffEmail))
            {
                cust.KeyOffEmail = cust.KeyOffEmail.ToLower();
            }

            string customerDateOfApplication = "";
            string KeyOffDateOfAnniversary = "";
            string KeyOfficialDOB = "";


            customerDateOfApplication = await _commonActionService.changeDateFormat(cust.CustomerDateOfApplication);

            if (!string.IsNullOrEmpty(cust.KeyOffDateOfAnniversary))
            {
                KeyOffDateOfAnniversary = await _commonActionService.changeDateFormat(cust.KeyOffDateOfAnniversary);
            }

            if (!string.IsNullOrEmpty(cust.KeyOfficialDOB))
            {
                KeyOfficialDOB = await _commonActionService.changeDateFormat(cust.KeyOfficialDOB);
            }

            string CustomerTypeID = "945", CustomerSubTypeID = "946";//For Aggregator Customer
            var CustomerTypeForms = new CustomerCreationRequestModel
            {
                RequestId = cust.RequestId,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerReferenceNo = cust.CustomerReferenceNo,
                CustomerId = cust.CustomerId,
                ZonalOffice = cust.CustomerZonalOfficeID.ToString(),
                RegionalOffice = cust.CustomerRegionID.ToString(),
                DateOfApplication = customerDateOfApplication,
                SalesArea = cust.CustomerSalesAreaID.ToString(),
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                IndividualOrgNameTitle = cust.IndividualOrgNameTitle,
                IndividualOrgName = cust.IndividualOrgName,
                NameOnCard = (String.IsNullOrEmpty(cust.CustomerNameOnCard) ? "" : cust.CustomerNameOnCard),
                TypeOfBusinessEntity = cust.CustomerTbentityID.ToString(),
                ResidenceStatus = cust.CustomerResidenceStatus,
                IncomeTaxPan = cust.CustomerIncomeTaxPan,
                CommunicationAddress1 = cust.CommunicationAddress1,
                CommunicationAddress2 = cust.CommunicationAddress2,
                CommunicationAddress3 = (String.IsNullOrEmpty(cust.CommunicationAddress3) ? "" : cust.CommunicationAddress3),
                CommunicationLocation = (String.IsNullOrEmpty(cust.CommunicationLocation) ? "" : cust.CommunicationLocation),
                CommunicationCityName = (String.IsNullOrEmpty(cust.CommunicationCity) ? "" : cust.CommunicationCity),
                CommunicationPincode = cust.CommunicationPinCode,
                CommunicationStateId = cust.CommunicationStateID.ToString(),
                CommunicationDistrictId = cust.CommunicationDistrictId.ToString(),
                CommunicationPhoneNo = (String.IsNullOrEmpty(cust.CommunicationDialCode) ? "" : cust.CommunicationDialCode) + "-" + (String.IsNullOrEmpty(cust.CommunicationPhoneNo) ? "" : cust.CommunicationPhoneNo),
                CommunicationFax = (String.IsNullOrEmpty(cust.CommunicationFaxCode) ? "" : cust.CommunicationFaxCode) + "-" + (String.IsNullOrEmpty(cust.CommunicationFax) ? "" : cust.CommunicationFax),
                CommunicationMobileNo = cust.CommunicationMobileNumber,
                CommunicationEmailid = cust.CommunicationEmail,
                PermanentAddress1 = cust.PerOrRegAddress1,
                PermanentAddress2 = cust.PerOrRegAddress2,
                PermanentAddress3 = (String.IsNullOrEmpty(cust.PerOrRegAddress3) ? "" : cust.PerOrRegAddress3),
                PermanentLocation = (String.IsNullOrEmpty(cust.PerOrRegAddressLocation) ? "" : cust.PerOrRegAddressLocation),
                PermanentCityName = cust.PerOrRegAddressCity,
                PermanentPincode = cust.PerOrRegAddressPinCode,
                PermanentStateId = cust.PerOrRegAddressStateID.ToString(),
                PermanentDistrictId = (cust.PermanentDistrictId == 0 ? cust.CommunicationDistrictId.ToString() : cust.PermanentDistrictId.ToString()),
                PermanentPhoneNo = (String.IsNullOrEmpty(cust.PerOrRegAddressDialCode) ? "" : cust.PerOrRegAddressDialCode) + "-" + (String.IsNullOrEmpty(cust.PerOrRegAddressPhoneNumber) ? "" : cust.PerOrRegAddressPhoneNumber),
                PermanentFax = (String.IsNullOrEmpty(cust.PermanentFaxCode) ? "" : cust.PermanentFaxCode) + "-" + (String.IsNullOrEmpty(cust.PermanentFax) ? "" : cust.PermanentFax),
                KeyOfficialTitle = cust.KeyOffTitle,
                KeyOfficialIndividualInitials = cust.KeyOffIndividualInitials,
                KeyOfficialFirstName = (String.IsNullOrEmpty(cust.KeyOffFirstName) ? "" : cust.KeyOffFirstName),
                KeyOfficialMiddleName = (String.IsNullOrEmpty(cust.KeyOffMiddleName) ? "" : cust.KeyOffMiddleName),
                KeyOfficialLastName = (String.IsNullOrEmpty(cust.KeyOffLastName) ? "" : cust.KeyOffLastName),
                KeyOfficialFax = (String.IsNullOrEmpty(cust.KeyOffFaxCode) ? "" : cust.KeyOffFaxCode) + "-" + (String.IsNullOrEmpty(cust.KeyOffFax) ? "" : cust.KeyOffFax),
                KeyOfficialDesignation = cust.KeyOffDesignation,
                KeyOfficialEmail = cust.KeyOffEmail,
                KeyOfficialPhoneNo = (String.IsNullOrEmpty(cust.KeyOffPhoneCode) ? "" : cust.KeyOffPhoneCode) + "-" + (String.IsNullOrEmpty(cust.KeyOffPhoneNumber) ? "" : cust.KeyOffPhoneNumber),
                KeyOfficialDOA = (string.IsNullOrEmpty(KeyOffDateOfAnniversary) ? "1900-01-01" : KeyOffDateOfAnniversary),
                KeyOfficialMobile = cust.KeyOffMobileNumber,
                KeyOfficialDOB = (string.IsNullOrEmpty(KeyOfficialDOB) ? "1900-01-01" : KeyOfficialDOB),
                KeyOfficialSecretQuestion = "0",
                KeyOfficialSecretAnswer = null,
                KeyOfficialTypeOfFleet = cust.CustomerTypeOfFleetID,
                KeyOfficialCardAppliedFor = (String.IsNullOrEmpty(cust.KeyOfficialCardAppliedFor) ? "" : cust.KeyOfficialCardAppliedFor),
                KeyOfficialApproxMonthlySpendsonVechile1 = (String.IsNullOrEmpty(cust.KeyOfficialApproxMonthlySpendsonVechile1) ? "0" : cust.KeyOfficialApproxMonthlySpendsonVechile1),
                KeyOfficialApproxMonthlySpendsonVechile2 = (String.IsNullOrEmpty(cust.KeyOfficialApproxMonthlySpendsonVechile2) ? "0" : cust.KeyOfficialApproxMonthlySpendsonVechile2),
                AreaOfOperation = cust.AreaOfOperation,
                FleetSizeNoOfVechileOwnedHCV = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedHCV) ? "0" : cust.FleetSizeNoOfVechileOwnedHCV),
                FleetSizeNoOfVechileOwnedLCV = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedLCV) ? "0" : cust.FleetSizeNoOfVechileOwnedLCV),
                FleetSizeNoOfVechileOwnedMUVSUV = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedMUVSUV) ? "0" : cust.FleetSizeNoOfVechileOwnedMUVSUV),
                FleetSizeNoOfVechileOwnedCarJeep = (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedCarJeep) ? "0" : cust.FleetSizeNoOfVechileOwnedCarJeep),
                CustomerType = CustomerTypeID.ToString(),
                CustomerSubtype = CustomerSubTypeID.ToString(),
                TierOfCustomer = "0",
                TypeOfCustomer = "0",
                PanCardRemarks = (String.IsNullOrEmpty(cust.PanCardRemarks) ? "" : cust.PanCardRemarks)

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

            var contentString = await _requestService.CommonRequestService(content, WebApiUrl.convertparenttoaggregator);
            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(contentString);
            cust.Internel_Status_Code = customerResponse.Internel_Status_Code;

            if (customerResponse.Data != null)
            {
                cust.Remarks = customerResponse.Data[0].Reason;
                cust.CustomerReferenceNo = customerResponse.Data[0].CustomerReferenceNo;
            }
            else
                cust.Remarks = customerResponse.Message;

            if (cust.Internel_Status_Code != 1000)
            {
                cust.CustomerTypeMdl.AddRange(await _commonActionService.GetCustomerTypeListDropdown());

                cust.CustomerSubTypeMdl.AddRange(await _commonActionService.GetCustomerSubTypeDropdown(cust.CustomerTypeID));
                cust.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
                cust.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType("1"));

                cust.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(cust.CustomerZonalOfficeID));

                cust.SalesAreaMdl.AddRange(await _commonActionService.GetSalesAreaDropdown(cust.CustomerRegionID.ToString()));

                cust.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

                cust.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

                cust.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.CommunicationStateID.ToString()));

                cust.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

                cust.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

                cust.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

                cust.PerOrRegAddressDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.PerOrRegAddressStateID.ToString()));
            }

            return cust;
        }
        public async Task<ParentChildBalanceTransferViewModel> ParentChildBalanceFundTransfer(ParentChildBalanceTransferViewModel reqEntity)
        {
            ParentChildBalanceTransferViewModel searchResponse = new ParentChildBalanceTransferViewModel();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ParentCustomerID = reqEntity.ParentCustomerID,
                ChildCustomerId = reqEntity.ChildCustomerId==""?null:reqEntity.ChildCustomerId,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.parentchildbalancetransfer);

            searchResponse = JsonConvert.DeserializeObject<ParentChildBalanceTransferViewModel>(response);

            return searchResponse;
        }
    }
}