﻿using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Aggregator;
using HPCL.Common.Models.RequestModel.Customer;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.Aggregator;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
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
    public class FleetService:IFleetService
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
            form.Add(new StringContent(entity.IdProofType.ToString()), "IdProofType");
            form.Add(new StringContent(entity.IdProofDocumentNo), "IdProofDocumentNo");
            form.Add(new StreamContent(entity.IdProofFront.OpenReadStream()), "IdProofFront", entity.IdProofFront.FileName);
            form.Add(new StreamContent(entity.IdProofBack.OpenReadStream()), "IdProofBack", entity.IdProofBack.FileName);
            form.Add(new StringContent(entity.AddressProofType.ToString()), "AddressProofType");
            form.Add(new StringContent(entity.AddressProofDocumentNo), "AddressProofDocumentNo");
            form.Add(new StreamContent(entity.AddressProofFront.OpenReadStream()), "AddressProofFront", entity.AddressProofFront.FileName);
            form.Add(new StreamContent(entity.AddressProofBack.OpenReadStream()), "AddressProofBack", entity.AddressProofBack.FileName);
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
    }
}
