using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Aggregator;
using HPCL.Common.Models.RequestModel.Customer;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.Aggregator;
using HPCL.Common.Models.ViewModel.Customer;
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
    public class FleetService : IFleetService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;
        public FleetService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }
        public async Task<ManageAggregatorViewModel> ManageFleetCustomer(string FromDate, string ToDate)
        {
            ManageAggregatorViewModel custMdl = new ManageAggregatorViewModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                custMdl.FromDate = await _commonActionService.changeDateFormat(FromDate);
                custMdl.ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                custMdl.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                custMdl.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var searchBody = new ManageAggregatorRequestModel();

            searchBody = new ManageAggregatorRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = custMdl.FromDate,
                ToDate = custMdl.ToDate

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getaggregatorfleetcustomer);

            custMdl = JsonConvert.DeserializeObject<ManageAggregatorViewModel>(response);
            custMdl.Remarks = "";
            int CustomerTypeID = 901;
            custMdl.CustomerSubTypeMdl.AddRange(await _commonActionService.GetCustomerSubTypeDropdown(CustomerTypeID));
            custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());

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
        public async Task<ManageAggregatorViewModel> ManageFleetCustomer(ManageAggregatorViewModel cust)
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
            string CustomerTypeID = "901";//For Fleet Customer


            var CustomerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                    {"CustomerType", CustomerTypeID.ToString()},
                    {"CustomerSubtype",cust.CustomerSubTypeID.ToString()},
                    {"ZonalOffice", cust.CustomerZonalOfficeID.ToString()},
                    {"RegionalOffice", cust.CustomerRegionID.ToString()},
                    {"DateOfApplication", customerDateOfApplication},
                    {"SalesArea", cust.CustomerSalesAreaID.ToString()},
                    {"IndividualOrgNameTitle", cust.IndividualOrgNameTitle},
                    {"IndividualOrgName", cust.IndividualOrgName},
                    {"NameOnCard", (String.IsNullOrEmpty(cust.CustomerNameOnCard)?"":cust.CustomerNameOnCard)},
                    {"TypeOfBusinessEntity", cust.CustomerTbentityID.ToString()},
                    {"ResidenceStatus", cust.CustomerResidenceStatus},
                    {"IncomeTaxPan", cust.CustomerIncomeTaxPan},
                    {"CommunicationAddress1", cust.CommunicationAddress1},
                    {"CommunicationAddress2", cust.CommunicationAddress2},
                    {"CommunicationAddress3", (String.IsNullOrEmpty(cust.CommunicationAddress3)?"":cust.CommunicationAddress3)},
                    {"CommunicationLocation", (String.IsNullOrEmpty(cust.CommunicationLocation)?"":cust.CommunicationLocation)},
                    {"CommunicationCityName", (String.IsNullOrEmpty(cust.CommunicationCity)?"":cust.CommunicationCity)},
                    {"CommunicationPincode", cust.CommunicationPinCode},
                    {"CommunicationStateId", cust.CommunicationStateID.ToString()},
                    {"CommunicationDistrictId", cust.CommunicationDistrictId.ToString()},
                    {"CommunicationPhoneNo", (String.IsNullOrEmpty(cust.CommunicationDialCode)?"":cust.CommunicationDialCode) + "-" + (String.IsNullOrEmpty(cust.CommunicationPhoneNo)?"":cust.CommunicationPhoneNo)},
                    {"CommunicationFax", (String.IsNullOrEmpty(cust.CommunicationFaxCode)?"":cust.CommunicationFaxCode) + "-" + (String.IsNullOrEmpty(cust.CommunicationFax)?"":cust.CommunicationFax)},
                    {"CommunicationMobileNo", cust.CommunicationMobileNumber},
                    {"CommunicationEmailid", cust.CommunicationEmail},
                    {"PermanentAddress1", cust.PerOrRegAddress1},
                    {"PermanentAddress2", cust.PerOrRegAddress2},
                    {"PermanentAddress3", (String.IsNullOrEmpty(cust.PerOrRegAddress3)?"":cust.PerOrRegAddress3)},
                    {"PermanentLocation", (String.IsNullOrEmpty(cust.PerOrRegAddressLocation)?"":cust.PerOrRegAddressLocation)},
                    {"PermanentCityName", cust.PerOrRegAddressCity},
                    {"PermanentPincode", cust.PerOrRegAddressPinCode},
                    {"PermanentStateId", cust.PerOrRegAddressStateID.ToString()},
                    {"PermanentDistrictId", (cust.PermanentDistrictId==0?cust.CommunicationDistrictId.ToString():cust.PermanentDistrictId.ToString())},
                    {"PermanentPhoneNo", (String.IsNullOrEmpty(cust.PerOrRegAddressDialCode)?"":cust.PerOrRegAddressDialCode) + "-" + (String.IsNullOrEmpty(cust.PerOrRegAddressPhoneNumber)?"":cust.PerOrRegAddressPhoneNumber)},
                    {"PermanentFax", (String.IsNullOrEmpty(cust.PermanentFaxCode)?"":cust.PermanentFaxCode) + "-" + (String.IsNullOrEmpty(cust.PermanentFax)?"":cust.PermanentFax)},
                    {"KeyOfficialTitle", cust.KeyOffTitle},
                    {"KeyOfficialIndividualInitials", cust.KeyOffIndividualInitials},
                    {"KeyOfficialFirstName", (String.IsNullOrEmpty(cust.KeyOffFirstName)?"":cust.KeyOffFirstName)},
                    {"KeyOfficialMiddleName", (String.IsNullOrEmpty(cust.KeyOffMiddleName)?"":cust.KeyOffMiddleName)},
                    {"KeyOfficialLastName", (String.IsNullOrEmpty(cust.KeyOffLastName)?"":cust.KeyOffLastName)},
                    {"KeyOfficialFax", (String.IsNullOrEmpty(cust.KeyOffFaxCode)?"":cust.KeyOffFaxCode) + "-" + (String.IsNullOrEmpty(cust.KeyOffFax)?"":cust.KeyOffFax)},
                    {"KeyOfficialDesignation", cust.KeyOffDesignation},
                    {"KeyOfficialEmail", cust.KeyOffEmail},
                    {"KeyOfficialPhoneNo", (String.IsNullOrEmpty(cust.KeyOffPhoneCode)?"":cust.KeyOffPhoneCode) + "-" + (String.IsNullOrEmpty(cust.KeyOffPhoneNumber)?"":cust.KeyOffPhoneNumber)},
                    {"KeyOfficialDOA", (string.IsNullOrEmpty(KeyOffDateOfAnniversary)?"1900-01-01":KeyOffDateOfAnniversary)},
                    {"KeyOfficialMobile", cust.KeyOffMobileNumber},
                    {"KeyOfficialDOB", (string.IsNullOrEmpty(KeyOfficialDOB)?"1900-01-01":KeyOfficialDOB)},
                    {"KeyOfficialSecretQuestion", cust.KeyOfficialSecretQuestion},
                    {"KeyOfficialSecretAnswer", (String.IsNullOrEmpty(cust.KeyOfficialSecretAnswer)?"":cust.KeyOfficialSecretAnswer)},
                    {"KeyOfficialTypeOfFleet", cust.CustomerTypeOfFleetID},
                    {"KeyOfficialCardAppliedFor", (String.IsNullOrEmpty(cust.KeyOfficialCardAppliedFor)?"":cust.KeyOfficialCardAppliedFor)},
                    {"KeyOfficialApproxMonthlySpendsonVechile1", (String.IsNullOrEmpty(cust.KeyOfficialApproxMonthlySpendsonVechile1)?"0":cust.KeyOfficialApproxMonthlySpendsonVechile1)},
                    {"KeyOfficialApproxMonthlySpendsonVechile2", (String.IsNullOrEmpty(cust.KeyOfficialApproxMonthlySpendsonVechile2)?"0":cust.KeyOfficialApproxMonthlySpendsonVechile2)},
                    {"AreaOfOperation", cust.AreaOfOperation},
                    {"FleetSizeNoOfVechileOwnedHCV", (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedHCV)?"0":cust.FleetSizeNoOfVechileOwnedHCV)},
                    {"FleetSizeNoOfVechileOwnedLCV", (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedLCV)?"0":cust.FleetSizeNoOfVechileOwnedLCV)},
                    {"FleetSizeNoOfVechileOwnedMUVSUV", (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedMUVSUV)?"0":cust.FleetSizeNoOfVechileOwnedMUVSUV)},
                    {"FleetSizeNoOfVechileOwnedCarJeep", (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedCarJeep)?"0":cust.FleetSizeNoOfVechileOwnedCarJeep)},
                    {"NoOfCards", cust.NoOfCards.ToString()},
                    {"FeePaymentsCollectFeeWaiver", cust.FeePaymentsCollectFeeWaiver.ToString()},
                    {"Createdby", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                    //{"TierOfCustomer", cust.TierOfCustomerID.ToString()},
                    //{"TypeOfCustomer", cust.TypeOfCustomerID.ToString()},
                    {"FormNumber", cust.FormNumber.ToString()},
                    {"PanCardRemarks", (String.IsNullOrEmpty(cust.PanCardRemarks)?"":cust.PanCardRemarks)},
                    {"RBEId", ""}

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

            var contentString = await _requestService.CommonRequestService(content, WebApiUrl.insertaggregatornormalfleetcustomer);
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
                cust.CustomerSubTypeMdl.AddRange(await _commonActionService.GetCustomerSubTypeDropdown(901));

                cust.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());

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
        public async Task<UploadDocResponse> UploadDoc(UploadDoc entity)
        {
            var searchBody = new UploadDoc
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerReferenceNo = entity.CustomerReferenceNo,
                Type = String.IsNullOrEmpty(entity.Type) ? "0" : "1",
            };

            _httpContextAccessor.HttpContext.Session.SetString("CustomerReferenceNoVal", entity.CustomerReferenceNo);

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getfleetcustomernameandformnumberbyreferenceno);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            UploadDocResponse searchCustomer = obj.ToObject<UploadDocResponse>();
            return searchCustomer;
        }

        public async Task<string> SaveUploadDoc(UploadDoc entity)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("CustomerReferenceNoVal")), "CustomerReferenceNo");

            form.Add(new StringContent("4"), "IdProofType");
            form.Add(new StreamContent(entity.IdProofFront.OpenReadStream()), "IdProofFront", entity.IdProofFront.FileName);

            form.Add(new StringContent("6"), "AddressProofType");
            form.Add(new StreamContent(entity.AddressProofFront.OpenReadStream()), "AddressProofFront", entity.AddressProofFront.FileName);

            form.Add(new StringContent("3"), "PanCardType");
            form.Add(new StreamContent(entity.PanCard.OpenReadStream()), "PanCard", entity.PanCard.FileName);

            form.Add(new StringContent("7"), "VehicleDetailsType");
            form.Add(new StreamContent(entity.VehicleDetails.OpenReadStream()), "VehicleDetails", entity.VehicleDetails.FileName);

            form.Add(new StringContent("8"), "CustomerFormType");
            form.Add(new StreamContent(entity.CustomerForm.OpenReadStream()), "CustomerForm", entity.CustomerForm.FileName);

            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("UserId")), "CreatedBy");
            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("UserId")), "Userid");
            form.Add(new StringContent(CommonBase.useragent), "Useragent");
            form.Add(new StringContent(CommonBase.userip), "Userip");

            var response = await _requestService.FormDataRequestService(form, WebApiUrl.uploadaggregatornormalfleetcustomerkyc);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<UpdateKycResponse> insertKyc = jarr.ToObject<List<UpdateKycResponse>>();

            return (insertKyc[0].Reason + "," + _httpContextAccessor.HttpContext.Session.GetString("CustomerReferenceNoVal"));
        }
        public async Task<UploadDocResponseBody> UploadDoc(string FormNumber)
        {
            UploadDocResponseBody UploadDocResponseBody = new UploadDocResponseBody();

            if (!string.IsNullOrEmpty(FormNumber))
            {
                JObject obj = await ViewCustomerDetails(FormNumber);

                var searchRes = obj["Data"].Value<JObject>();
                var custResult = searchRes["GetAggregatorCustomerDetails"].Value<JArray>();

                var customerKYCDetailsResult = searchRes["AggregatorCustomerKYCDetails"].Value<JArray>();

                List<CustomerFullDetails> customerList = custResult.ToObject<List<CustomerFullDetails>>();

                List<UploadDocResponseBody> UploadDocList = customerKYCDetailsResult.ToObject<List<UploadDocResponseBody>>();

                UploadDocResponseBody = UploadDocList.Where(t => t.FormNumber == FormNumber).FirstOrDefault();

            }

            return UploadDocResponseBody;
        }
        public async Task<JObject> ViewCustomerDetails(string FormNumber)
        {
            _httpContextAccessor.HttpContext.Session.SetString("FormNumberSession", FormNumber);

            var customerBody = new CheckformNumberDuplicationRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = FormNumber
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(customerBody), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getunverfiedfleetcustomerbyformnumber);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

            return obj;
        }
        public async Task<CustomerCardInfo> AddCardDetails(CustomerCardInfo customerCardInfo)
        {

            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(customerCardInfo.CustomerReferenceNo.ToString()), "CustomerReferenceNo");
            form.Add(new StringContent(customerCardInfo.NoOfCards.ToString()), "NoOfCards");
            form.Add(new StringContent(customerCardInfo.CardPreference.ToString()), "CardPreference");
            foreach (CardDetails item in customerCardInfo.ObjCardDetail)
            {
                if(customerCardInfo.CardPreference=="Cardless")
                    form.Add(new StringContent(item.MobileNo.ToString()), "MobileNo");
                form.Add(new StringContent(item.VechileNo.ToUpper().ToString()), "VechileNo");
                form.Add(new StringContent(item.VehicleType.ToString()), "VehicleType");
                form.Add(new StringContent(item.VehicleMake.ToString()), "VehicleMake");
                form.Add(new StringContent(item.YearOfRegistration.ToString()), "YearOfRegistration");
                form.Add(new StreamContent(item.RCCopy.OpenReadStream()), "RcCopy", item.RCCopy.FileName);
            }
            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("UserId")), "CreatedBy");
            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("UserId")), "Userid");
            form.Add(new StringContent(CommonBase.useragent), "Useragent");
            form.Add(new StringContent(CommonBase.userip), "Userip");

            var response = await _requestService.FormDataRequestService(form, WebApiUrl.aggregatornormalfleetcustomeraddcard);
            //StringContent content = new StringContent(JsonConvert.SerializeObject(insertInfo), Encoding.UTF8, "application/json");

            CustomerInserCardResponse customerInserCardResponse;

            //var responseCustomer = await _requestService.CommonRequestService(content, WebApiUrl.aggregatornormalfleetcustomeraddcard);


            customerInserCardResponse = JsonConvert.DeserializeObject<CustomerInserCardResponse>(response);

            if (customerInserCardResponse.Internel_Status_Code != 1000)
            {
                foreach (CardDetails cardDetails in customerCardInfo.ObjCardDetail)
                {
                    cardDetails.VehicleNoMsg = "";
                    cardDetails.MobileNoMsg = "";
                }

                if (customerInserCardResponse.Message.Contains("Vehicle No."))
                {
                    foreach (CustomerInserCardResponseData responseData in customerInserCardResponse.Data)
                    {
                        foreach (CardDetails cardDetails in customerCardInfo.ObjCardDetail)
                        {
                            if (cardDetails.VechileNo.ToUpper() == responseData.Reason.ToUpper())
                            {
                                cardDetails.VehicleNoMsg = "Vehicle No. already exists";
                            }
                        }
                    }
                }

                if (customerInserCardResponse.Message.Contains("Mobile No."))
                {
                    foreach (CustomerInserCardResponseData responseData in customerInserCardResponse.Data)
                    {
                        foreach (CardDetails cardDetails in customerCardInfo.ObjCardDetail)
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
                customerCardInfo.StatusCode = customerInserCardResponse.Internel_Status_Code;
                customerCardInfo.Message = customerInserCardResponse.Message;

                if (customerInserCardResponse != null && customerInserCardResponse.Data != null
                    && customerInserCardResponse.Data.Count > 0 && customerInserCardResponse.Data[0].Status != 1)
                {
                    customerCardInfo.Status = customerInserCardResponse.Data[0].Status;
                    customerCardInfo.StatusCode = customerInserCardResponse.Internel_Status_Code + 1;
                    customerCardInfo.Reason = customerInserCardResponse.Data[0].Reason;
                }
                else if (customerInserCardResponse != null && customerInserCardResponse.Data != null && customerInserCardResponse.Data.Count > 0)
                {
                    customerCardInfo.Status = customerInserCardResponse.Data[0].Status;
                    customerCardInfo.StatusCode = customerInserCardResponse.Internel_Status_Code;
                    customerCardInfo.Reason = customerInserCardResponse.Data[0].Reason;
                }
            }
            else
            {
                customerCardInfo.Message = customerInserCardResponse.Message;
                customerCardInfo.StatusCode = customerInserCardResponse.Internel_Status_Code;
                if (customerInserCardResponse != null && customerInserCardResponse.Data != null && customerInserCardResponse.Data.Count > 0)
                {
                    customerCardInfo.Message = customerInserCardResponse.Data[0].Reason;
                }
                foreach (CardDetails cardDetails in customerCardInfo.ObjCardDetail)
                {
                    if (customerCardInfo.VehicleNoVerifiedManually)
                    {
                        cardDetails.Verified = "0";
                    }
                    else
                    {
                        cardDetails.Verified = "1";
                    }
                }
            }

            return customerCardInfo;
        }
        public async Task<CustomerCardInfo> AddCardDetails(string customerReferenceNo)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();
            customerCardInfo.Remarks = "";
            customerCardInfo.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());
            customerCardInfo.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();
            if (string.IsNullOrEmpty(customerCardInfo.ExternalVehicleAPIStatus))
            {
                customerCardInfo.ExternalVehicleAPIStatus = "Y";
            }

            if (!string.IsNullOrEmpty(customerReferenceNo))
            {
                var requestData = new CheckPancardbyDistrictIdRequestModel()
                {
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CustomerReferenceNo = customerReferenceNo
                };

                CustomerResponseByReferenceNo customerResponseByReferenceNo;
                StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                var responseCustomer = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getfleetcustomernameandformnumberbyreferenceno);

                customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(responseCustomer);
                if (customerResponseByReferenceNo.Internel_Status_Code == 1000)
                {
                    customerCardInfo.CustomerReferenceNo = customerReferenceNo;

                    if (customerResponseByReferenceNo.Data != null)
                    {
                        customerCardInfo.FormNumber = customerResponseByReferenceNo.Data[0].FormNumber;

                        customerCardInfo.CustomerName = customerResponseByReferenceNo.Data[0].CustomerName;

                        customerCardInfo.CustomerTypeName = customerResponseByReferenceNo.Data[0].CustomerTypeName;
                        customerCardInfo.CustomerTypeId = customerResponseByReferenceNo.Data[0].CustomerTypeId;
                        customerCardInfo.Status = customerResponseByReferenceNo.Data[0].Status;

                        customerCardInfo.PaymentType = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentType) ? "" : customerResponseByReferenceNo.Data[0].PaymentType;
                        customerCardInfo.PaymentReceivedDate = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentReceivedDate) ? "" : customerResponseByReferenceNo.Data[0].PaymentReceivedDate;
                        customerCardInfo.NoOfCards = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].NoOfCards) ? "" : customerResponseByReferenceNo.Data[0].NoOfCards;
                        customerCardInfo.ReceivedAmount = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].ReceivedAmount) ? "0" : customerResponseByReferenceNo.Data[0].ReceivedAmount;
                        customerCardInfo.RBEId = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEId) ? "0" : customerResponseByReferenceNo.Data[0].RBEId;
                        customerCardInfo.RBEName = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEName) ? "" : customerResponseByReferenceNo.Data[0].RBEName;
                        if (customerCardInfo.RBEId == "0")
                            customerCardInfo.RBEId = "";
                        if (customerCardInfo.NoOfCards == "0")
                            customerCardInfo.NoOfCards = "";
                        //if (customerCardInfo.CustomerTypeId == "905" || customerCardInfo.CustomerTypeId == "909")
                        //{
                        //    customerCardInfo.NoOfVehiclesAllCards = customerCardInfo.NoOfCards;
                        //}
                    }
                }

            }

            return customerCardInfo;
        }
        public async Task<CustomerCardInfo> GetCustomerDetailsForAddCard(string customerReferenceNo)
        {
            //fetching Customer info
            var CustomerRefinfo = new CustomerModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerReferenceNo = customerReferenceNo,
                Type = "1",
            };

            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            CustomerResponseByReferenceNo customerResponseByReferenceNo;
            StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");

            var responseCustomer = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getfleetcustomernameandformnumberbyreferenceno);

            customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(responseCustomer);

            if (string.IsNullOrEmpty(customerCardInfo.FormNumber))
            {
                customerCardInfo.FormNumber = "";
            }

            if (customerResponseByReferenceNo.Internel_Status_Code == 1000)
            {
                customerCardInfo.CustomerReferenceNo = customerReferenceNo;
                customerCardInfo.FormNumber = customerResponseByReferenceNo.Data[0].FormNumber;

                customerCardInfo.CustomerName = customerResponseByReferenceNo.Data[0].CustomerName;

                customerCardInfo.CustomerTypeName = customerResponseByReferenceNo.Data[0].CustomerTypeName;
                customerCardInfo.CustomerTypeId = customerResponseByReferenceNo.Data[0].CustomerTypeId;
                if (customerResponseByReferenceNo.Data != null)
                {
                    customerCardInfo.PaymentType = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentType) ? "" : customerResponseByReferenceNo.Data[0].PaymentType;
                    customerCardInfo.PaymentReceivedDate = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentReceivedDate) ? "" : customerResponseByReferenceNo.Data[0].PaymentReceivedDate;
                    customerCardInfo.NoOfCards = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].NoOfCards) ? "" : customerResponseByReferenceNo.Data[0].NoOfCards;
                    customerCardInfo.ReceivedAmount = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].ReceivedAmount) ? "0" : customerResponseByReferenceNo.Data[0].ReceivedAmount;
                    customerCardInfo.RBEId = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEId) ? "0" : customerResponseByReferenceNo.Data[0].RBEId;
                    customerCardInfo.Status = customerResponseByReferenceNo.Data[0].Status;
                    customerCardInfo.Reason = customerResponseByReferenceNo.Data[0].Reason;

                    if (customerCardInfo.RBEId == "null")
                        customerCardInfo.RBEId = "";
                    else if (customerCardInfo.RBEId == null)
                        customerCardInfo.RBEId = "";
                    else if (customerCardInfo.RBEId == "0")
                        customerCardInfo.RBEId = "";

                    if (customerCardInfo.NoOfCards == "0")
                        customerCardInfo.NoOfCards = "";
                    customerCardInfo.RBEName = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEName) ? "" : customerResponseByReferenceNo.Data[0].RBEName;


                }
            }
            else
            {
                customerCardInfo.Remarks = customerResponseByReferenceNo.Message;
            }
            return customerCardInfo;
        }
        public async Task<CustomerCardInfo> GetCustomerAddCardsPartialView([FromBody] List<CardDetails> arrs)
        {
            CustomerCardInfo addAddOnCard = new CustomerCardInfo();

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            if (!string.IsNullOrEmpty(arrs[0].CardPreference))
                addAddOnCard.CardPreference = arrs[0].CardPreference;

            addAddOnCard.CustomerTypeId = "901";
            addAddOnCard.NoOfCards = arrs[0].NoOfCards;
            addAddOnCard.VehicleNoVerifiedManually = Convert.ToBoolean(arrs[0].VehicleNoVerifiedManually);
            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].CardIdentifier)) || (!string.IsNullOrEmpty(arrs[0].VechileNo))))
                addAddOnCard.ObjCardDetail = arrs;


            addAddOnCard.NoOfGridRows = arrs[0].NoOfCards;

            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();
            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            return addAddOnCard;
        }
        public async Task<ValidateAggregatorCustomerModel> VerfiyFleetCustomer(ValidateAggregatorCustomerModel entity)
        {

            entity.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
            entity.CustomerStatusMdl.AddRange(await _commonActionService.GetNormalFleetCustomerStatus());

            var request = new GetValidateNewCustomerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = String.IsNullOrEmpty(entity.FormNumber) ? "" : entity.FormNumber,
                StateId = String.IsNullOrEmpty(entity.StateId) ? "" : entity.StateId,
                CustomerName = String.IsNullOrEmpty(entity.CustomerName) ? "" : entity.CustomerName,
                Status = String.IsNullOrEmpty(entity.StatusId) ? "19" : entity.StatusId
            };

            _httpContextAccessor.HttpContext.Session.SetString("viewUpdatedCustGrid", JsonConvert.SerializeObject(request));

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getaggregatornormalfleetcustomer);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SearchCustomerResponseGrid> searchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();

            entity.SearchCustomerResponseGridLst = searchList;
            entity.Message = obj["Message"].ToString();
            return entity;
        }
        public async Task<ManageAggregatorViewModel> UpdateFleetCustomer(string FormNumber)
        {
            ManageAggregatorViewModel custMdl = new ManageAggregatorViewModel();
            custMdl.Remarks = "";

            int CustomerTypeID = 901;
            custMdl.CustomerSubTypeMdl.AddRange(await _commonActionService.GetCustomerSubTypeDropdown(CustomerTypeID));

            custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());

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

            if (!string.IsNullOrEmpty(FormNumber))
            {
                JObject obj = await ViewCustomerDetails(FormNumber);

                var searchRes = obj["Data"].Value<JObject>();
                var custResult = searchRes["GetAggregatorCustomerDetails"].Value<JArray>();



                List<CustomerFullDetails> customerList = custResult.ToObject<List<CustomerFullDetails>>();

                CustomerFullDetails Customer = customerList.Where(t => t.FormNumber == FormNumber).FirstOrDefault();

                if (Customer != null)
                {
                    custMdl.FormNumber = Customer.FormNumber;
                    custMdl.CustomerReferenceNo = Customer.CustomerReferenceNo;
                    custMdl.CustomerTypeID = Convert.ToInt32(string.IsNullOrEmpty(Customer.CustomerTypeId) ? "0" : Customer.CustomerTypeId);

                    custMdl.CustomerSubTypeMdl.AddRange(await _commonActionService.GetCustomerSubTypeDropdown(custMdl.CustomerTypeID));

                    custMdl.CustomerSubTypeID = Convert.ToInt32(string.IsNullOrEmpty(Customer.CustomerSubtypeId) ? "0" : Customer.CustomerSubtypeId);
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
                        string[] subs = Customer.DateOfApplication.Split(' ');
                        string[] date = subs[0].Split('/');
                        custMdl.CustomerDateOfApplication = date[1] + "-" + date[0] + "-" + date[2];
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

                            string[] subs = Customer.KeyOfficialDOA.Split(' ');
                            string[] date = subs[0].Split('/');
                            custMdl.KeyOffDateOfAnniversary = date[1] + "-" + date[0] + "-" + date[2];
                        }
                    }

                    if (!string.IsNullOrEmpty(Customer.KeyOfficialDOB))
                    {
                        if (!Customer.KeyOfficialDOB.Contains("1900"))
                        {
                            string[] subs = Customer.KeyOfficialDOB.Split(' ');
                            string[] date = subs[0].Split('/');
                            custMdl.KeyOfficialDOB = date[1] + "-" + date[0] + "-" + date[2];
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

        public async Task<ManageAggregatorViewModel> UpdateFleetCustomer(ManageAggregatorViewModel cust)
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

            string CustomerTypeID = "901";//For Fleet Customer

            var CustomerTypeForms = new Dictionary<string, string>
                {
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"CustomerReferenceNo", cust.CustomerReferenceNo},
                    {"ZonalOffice", cust.CustomerZonalOfficeID.ToString()},
                    {"RegionalOffice", cust.CustomerRegionID.ToString()},
                    {"DateOfApplication", customerDateOfApplication},
                    {"SalesArea", cust.CustomerSalesAreaID.ToString()},
                    {"ModifiedBy", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                    {"IndividualOrgNameTitle", cust.IndividualOrgNameTitle},
                    {"IndividualOrgName", cust.IndividualOrgName},
                    {"NameOnCard", (String.IsNullOrEmpty(cust.CustomerNameOnCard)?"":cust.CustomerNameOnCard)},
                    {"TypeOfBusinessEntity", cust.CustomerTbentityID.ToString()},
                    {"ResidenceStatus", cust.CustomerResidenceStatus},
                    {"IncomeTaxPan", cust.CustomerIncomeTaxPan},
                    {"CommunicationAddress1", cust.CommunicationAddress1},
                    {"CommunicationAddress2", cust.CommunicationAddress2},
                    {"CommunicationAddress3", (String.IsNullOrEmpty(cust.CommunicationAddress3)?"":cust.CommunicationAddress3)},
                    {"CommunicationLocation", (String.IsNullOrEmpty(cust.CommunicationLocation)?"":cust.CommunicationLocation)},
                    {"CommunicationCityName", (String.IsNullOrEmpty(cust.CommunicationCity)?"":cust.CommunicationCity)},
                    {"CommunicationPincode", cust.CommunicationPinCode},
                    {"CommunicationStateId", cust.CommunicationStateID.ToString()},
                    {"CommunicationDistrictId", cust.CommunicationDistrictId.ToString()},
                    {"CommunicationPhoneNo", (String.IsNullOrEmpty(cust.CommunicationDialCode)?"":cust.CommunicationDialCode) + "-" + (String.IsNullOrEmpty(cust.CommunicationPhoneNo)?"":cust.CommunicationPhoneNo)},
                    {"CommunicationFax", (String.IsNullOrEmpty(cust.CommunicationFaxCode)?"":cust.CommunicationFaxCode) + "-" + (String.IsNullOrEmpty(cust.CommunicationFax)?"":cust.CommunicationFax)},
                    {"CommunicationMobileNo", cust.CommunicationMobileNumber},
                    {"CommunicationEmailid", cust.CommunicationEmail},
                    {"PermanentAddress1", cust.PerOrRegAddress1},
                    {"PermanentAddress2", cust.PerOrRegAddress2},
                    {"PermanentAddress3", (String.IsNullOrEmpty(cust.PerOrRegAddress3)?"":cust.PerOrRegAddress3)},
                    {"PermanentLocation", (String.IsNullOrEmpty(cust.PerOrRegAddressLocation)?"":cust.PerOrRegAddressLocation)},
                    {"PermanentCityName", cust.PerOrRegAddressCity},
                    {"PermanentPincode", cust.PerOrRegAddressPinCode},
                    {"PermanentStateId", cust.PerOrRegAddressStateID.ToString()},
                    {"PermanentDistrictId", (cust.PermanentDistrictId==0?cust.CommunicationDistrictId.ToString():cust.PermanentDistrictId.ToString())},
                    {"PermanentPhoneNo", (String.IsNullOrEmpty(cust.PerOrRegAddressDialCode)?"":cust.PerOrRegAddressDialCode) + "-" + (String.IsNullOrEmpty(cust.PerOrRegAddressPhoneNumber)?"":cust.PerOrRegAddressPhoneNumber)},
                    {"PermanentFax", (String.IsNullOrEmpty(cust.PermanentFaxCode)?"":cust.PermanentFaxCode) + "-" + (String.IsNullOrEmpty(cust.PermanentFax)?"":cust.PermanentFax)},
                    {"KeyOfficialTitle", cust.KeyOffTitle},
                    {"KeyOfficialIndividualInitials", cust.KeyOffIndividualInitials},
                    {"KeyOfficialFirstName", (String.IsNullOrEmpty(cust.KeyOffFirstName)?"":cust.KeyOffFirstName)},
                    {"KeyOfficialMiddleName", (String.IsNullOrEmpty(cust.KeyOffMiddleName)?"":cust.KeyOffMiddleName)},
                    {"KeyOfficialLastName", (String.IsNullOrEmpty(cust.KeyOffLastName)?"":cust.KeyOffLastName)},
                    {"KeyOfficialFax", (String.IsNullOrEmpty(cust.KeyOffFaxCode)?"":cust.KeyOffFaxCode) + "-" + (String.IsNullOrEmpty(cust.KeyOffFax)?"":cust.KeyOffFax)},
                    {"KeyOfficialDesignation", cust.KeyOffDesignation},
                    {"KeyOfficialEmail", cust.KeyOffEmail},
                    {"KeyOfficialPhoneNo", (String.IsNullOrEmpty(cust.KeyOffPhoneCode)?"":cust.KeyOffPhoneCode) + "-" + (String.IsNullOrEmpty(cust.KeyOffPhoneNumber)?"":cust.KeyOffPhoneNumber)},
                    {"KeyOfficialDOA", (string.IsNullOrEmpty(KeyOffDateOfAnniversary)?"1900-01-01":KeyOffDateOfAnniversary)},
                    {"KeyOfficialMobile", cust.KeyOffMobileNumber},
                    {"KeyOfficialDOB", (string.IsNullOrEmpty(KeyOfficialDOB)?"1900-01-01":KeyOfficialDOB)},
                    {"KeyOfficialSecretQuestion", cust.KeyOfficialSecretQuestion},
                    {"KeyOfficialSecretAnswer", (String.IsNullOrEmpty(cust.KeyOfficialSecretAnswer)?"":cust.KeyOfficialSecretAnswer)},
                    {"KeyOfficialTypeOfFleet", cust.CustomerTypeOfFleetID},
                    {"KeyOfficialCardAppliedFor", (String.IsNullOrEmpty(cust.KeyOfficialCardAppliedFor)?"":cust.KeyOfficialCardAppliedFor)},
                    {"KeyOfficialApproxMonthlySpendsonVechile1", (String.IsNullOrEmpty(cust.KeyOfficialApproxMonthlySpendsonVechile1)?"0":cust.KeyOfficialApproxMonthlySpendsonVechile1)},
                    {"KeyOfficialApproxMonthlySpendsonVechile2", (String.IsNullOrEmpty(cust.KeyOfficialApproxMonthlySpendsonVechile2)?"0":cust.KeyOfficialApproxMonthlySpendsonVechile2)},
                    {"AreaOfOperation", cust.AreaOfOperation},
                    {"FleetSizeNoOfVechileOwnedHCV", (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedHCV)?"0":cust.FleetSizeNoOfVechileOwnedHCV)},
                    {"FleetSizeNoOfVechileOwnedLCV", (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedLCV)?"0":cust.FleetSizeNoOfVechileOwnedLCV)},
                    {"FleetSizeNoOfVechileOwnedMUVSUV", (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedMUVSUV)?"0":cust.FleetSizeNoOfVechileOwnedMUVSUV)},
                    {"FleetSizeNoOfVechileOwnedCarJeep", (String.IsNullOrEmpty(cust.FleetSizeNoOfVechileOwnedCarJeep)?"0":cust.FleetSizeNoOfVechileOwnedCarJeep)},
                    {"CustomerType", CustomerTypeID.ToString()},
                    {"CustomerSubtype", cust.CustomerSubTypeID.ToString()},
                    {"TierOfCustomer", cust.TierOfCustomerID.ToString()},
                    {"TypeOfCustomer", cust.TypeOfCustomerID.ToString()},
                    {"PanCardRemarks", (String.IsNullOrEmpty(cust.PanCardRemarks)?"":cust.PanCardRemarks)}

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

            var contentString = await _requestService.CommonRequestService(content, WebApiUrl.updateaggregatornormalfleetcustomer);
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

                cust.CustomerSubTypeMdl.AddRange(await _commonActionService.GetCustomerSubTypeDropdown(901));

                cust.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());

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
    }
}
