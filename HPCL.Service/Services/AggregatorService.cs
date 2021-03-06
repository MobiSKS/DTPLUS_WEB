using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class AggregatorService:IAggregatorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;
        public AggregatorService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }
        public async Task<ManageAggregatorViewModel> ManageAggregator(string FromDate, string ToDate)
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
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = custMdl.FromDate,
                ToDate = custMdl.ToDate

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getaggregatorcustomer);

            custMdl = JsonConvert.DeserializeObject<ManageAggregatorViewModel>(response);
            custMdl.Remarks = "";
            custMdl.CustomerTypeMdl.AddRange(await _commonActionService.GetCustomerTypeListDropdown());
            custMdl.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
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
        public async Task<ManageAggregatorViewModel> ManageAggregator(ManageAggregatorViewModel cust)
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


            var CustomerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", _httpContextAccessor.HttpContext.Session.GetString("IpAddress")},
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                    {"CustomerType", CustomerTypeID.ToString()},
                    {"CustomerSubtype", CustomerSubTypeID.ToString()},
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
                    {"KeyOfficialSecretQuestion", "0"},
                    {"KeyOfficialSecretAnswer", ""},
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
                    {"TierOfCustomer", "0"},
                    {"TypeOfCustomer", "0"},
                    {"FormNumber", cust.FormNumber.ToString()},
                    {"PanCardRemarks", (String.IsNullOrEmpty(cust.PanCardRemarks)?"":cust.PanCardRemarks)},
                    {"RBEId", ""}

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

            var contentString = await _requestService.CommonRequestService(content, WebApiUrl.insertaggregatorcustomer);
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
       

        public async Task<ValidateAggregatorCustomerModel> ValidateAggregatorCustomer(ValidateAggregatorCustomerModel entity)
        {
            string fromDateOfApplication = "";
            string toDateOfApplication = "";

            entity.CustomerRegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            if (!string.IsNullOrEmpty(entity.FromDate))
            {
                string[] frmDate = entity.FromDate.Split("-");
                fromDateOfApplication = frmDate[2] + "-" + frmDate[1] + "-" + frmDate[0];
            }
            if (!string.IsNullOrEmpty(entity.ToDate))
            {
                string[] toDate = entity.ToDate.Split("-");
                toDateOfApplication = toDate[2] + "-" + toDate[1] + "-" + toDate[0];
            }
            var request = new GetValidateNewCustomerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RegionalOfficeId = entity.CustomerRegionID > 0 ? entity.CustomerRegionID.ToString() : null,
                FromDate = fromDateOfApplication,
                ToDate = toDateOfApplication,
                FormNumber = entity.FormNumber,
                StateId = null,
                CustomerName = null
            };

            _httpContextAccessor.HttpContext.Session.SetString("viewUpdatedCustGrid", JsonConvert.SerializeObject(request));

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.bindunverfiedaggregatorcustomer);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SearchCustomerResponseGrid> searchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();

            entity.SearchCustomerResponseGridLst = searchList;
            entity.Message = obj["Message"].ToString();
            return entity;
        }
        public async Task<JObject> ViewCustomerDetails(string FormNumber)
        {
            _httpContextAccessor.HttpContext.Session.SetString("FormNumberSession", FormNumber);

            var customerBody = new CheckformNumberDuplicationRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = FormNumber
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(customerBody), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getunverfiedaggregatorcustomerbyformnumber);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

            return obj;
        }
        public async Task<UpdateKycResponse> AproveCustomer(string CustomerReferenceNo, string Comments, string Approvalstatus, string formNumber)
        {
            var approvalBody = new AproveAggregatorCustomerRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerReferenceNo = CustomerReferenceNo,
                Comments = Comments,
                Approvalstatus = Approvalstatus,
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = BigInteger.Parse(formNumber)
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(approvalBody), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.approverejectaggregatorcustomer);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<UpdateKycResponse> insertKyc = jarr.ToObject<List<UpdateKycResponse>>();

            return insertKyc[0];
        }
        public async Task<ManageAggregatorViewModel> UpdateAggregatorCustomer(string FormNumber)
        {
            ManageAggregatorViewModel custMdl = new ManageAggregatorViewModel();
            custMdl.Remarks = "";

            custMdl.CustomerTypeMdl.AddRange(await _commonActionService.GetCustomerTypeListDropdown());
            custMdl.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());

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
                    custMdl.SBUTypeId = string.IsNullOrEmpty(Customer.SBUTypeId) ? "0" : Customer.SBUTypeId;
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

        public async Task<ManageAggregatorViewModel> UpdateAggregatorCustomer(ManageAggregatorViewModel cust)
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

            var CustomerTypeForms = new Dictionary<string, string>
                {
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                    {"Useragent", CommonBase.useragent},
                    {"Userip", _httpContextAccessor.HttpContext.Session.GetString("IpAddress")},
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
                    {"KeyOfficialSecretQuestion", "0"},
                    {"KeyOfficialSecretAnswer", ""},
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
                    {"CustomerSubtype", CustomerSubTypeID.ToString()},
                    {"TierOfCustomer", "0"},
                    {"TypeOfCustomer", "0"},
                    {"PanCardRemarks", (String.IsNullOrEmpty(cust.PanCardRemarks)?"":cust.PanCardRemarks)}

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

            var contentString = await _requestService.CommonRequestService(content, WebApiUrl.updateaggregatorcustomer);
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
        public async Task<UploadDocResponse> UploadDoc(UploadDoc entity)
        {
            var searchBody = new UploadDoc
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
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

        public async Task<string> SaveUploadDoc(UploadDoc entity)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("FormNoVal")), "FormNumber");
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
            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("IpAddress")), "Userip");

            var response = await _requestService.FormDataRequestService(form, WebApiUrl.uploadaggregatorcustomerkyc);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<UpdateKycResponse> insertKyc = jarr.ToObject<List<UpdateKycResponse>>();

            return (insertKyc[0].Reason + "," + _httpContextAccessor.HttpContext.Session.GetString("FormNoVal"));
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
    }
}
