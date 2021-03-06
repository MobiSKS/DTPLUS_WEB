using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models;
using HPCL.Common.Models.ViewModel.Officers;
using System;
using System.Linq;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.RequestModel.Customer;
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using Microsoft.Extensions.Configuration;
using HPCL.Common.Models.ViewModel.TMS;

namespace HPCL.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public CustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }

        public async Task<UploadDocResponse> UploadDoc(UploadDoc entity)
        {
            var searchBody = new UploadDoc
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FormNumber = entity.FormNumber,
                Type = String.IsNullOrEmpty(entity.Type)?"0":"1",
            };

            _httpContextAccessor.HttpContext.Session.SetString("FormNoVal", entity.FormNumber);

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.searchCustRefUrl);

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

            var response = await _requestService.FormDataRequestService(form, WebApiUrl.UploadKycUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<UpdateKycResponse> insertKyc = jarr.ToObject<List<UpdateKycResponse>>();

            return (insertKyc[0].Reason + "," + _httpContextAccessor.HttpContext.Session.GetString("FormNoVal"));
        }

        public async Task<CustomerModel> OnlineForm()
        {
            CustomerModel custMdl = new CustomerModel();
            custMdl.Remarks = "";
            custMdl.CustomerTypeMdl.AddRange(await _commonActionService.GetCustomerTypeListDropdown());
            custMdl.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            custMdl.SBUTypeID = 1;
            custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType(custMdl.SBUTypeID.ToString()));

            custMdl.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

            //custMdl.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

            custMdl.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

            custMdl.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

            custMdl.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
            if (string.IsNullOrEmpty(custMdl.ExternalPANAPIStatus))
            {
                custMdl.ExternalPANAPIStatus = "Y";
            }

            return custMdl;
        }

        public async Task<CustomerModel> OnlineForm(CustomerModel cust)
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

            //string[] custDateOfApplication = cust.CustomerDateOfApplication.Split("-");

            //customerDateOfApplication = custDateOfApplication[2] + "-" + custDateOfApplication[1] + "-" + custDateOfApplication[0];
            customerDateOfApplication = await _commonActionService.changeDateFormat(cust.CustomerDateOfApplication);

            if (!string.IsNullOrEmpty(cust.KeyOffDateOfAnniversary))
            {
                //string[] dateOfAnniversary = cust.KeyOffDateOfAnniversary.Split("-");
                //KeyOffDateOfAnniversary = dateOfAnniversary[2] + "-" + dateOfAnniversary[1] + "-" + dateOfAnniversary[0];
                KeyOffDateOfAnniversary = await _commonActionService.changeDateFormat(cust.KeyOffDateOfAnniversary);
            }

            if (!string.IsNullOrEmpty(cust.KeyOfficialDOB))
            {
                //string[] officialDOB = cust.KeyOfficialDOB.Split("-");
                //KeyOfficialDOB = officialDOB[2] + "-" + officialDOB[1] + "-" + officialDOB[0];
                KeyOfficialDOB = await _commonActionService.changeDateFormat(cust.KeyOfficialDOB);
            }


            var CustomerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", _httpContextAccessor.HttpContext.Session.GetString("IpAddress")},
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                    {"CustomerType", cust.CustomerTypeID.ToString()},
                    {"CustomerSubtype", cust.CustomerSubTypeID.ToString()},
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
                    {"TierOfCustomer", cust.TierOfCustomerID.ToString()},
                    {"TypeOfCustomer", cust.TypeOfCustomerID.ToString()},
                    {"FormNumber", cust.FormNumber.ToString()},
                    {"PanCardRemarks", (String.IsNullOrEmpty(cust.PanCardRemarks)?"":cust.PanCardRemarks)},
                    {"RBEId", ""}

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

            var contentString = await _requestService.CommonRequestService(content, WebApiUrl.insertCustomer);
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

                cust.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType(cust.SBUTypeID.ToString()));

                cust.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(cust.CustomerZonalOfficeID));

                cust.SalesAreaMdl.AddRange(await _commonActionService.GetSalesAreaDropdown(cust.CustomerRegionID.ToString()));

                cust.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

                cust.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

                cust.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.CommunicationStateID.ToString()));

                //cust.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

                cust.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

                cust.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

                cust.PerOrRegAddressDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.PerOrRegAddressStateID.ToString()));
            }

            return cust;
        }

        public async Task<List<CustomerTypeModel>> GetCustomerType()
        {

            var CustType = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(CustType), Encoding.UTF8, "application/json");

            var responseCustomerType = await _requestService.CommonRequestService(content, WebApiUrl.getOfficerType);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseCustomerType).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTypeModel> lstCustomerType = jarr.ToObject<List<CustomerTypeModel>>();

            return lstCustomerType;
        }

        public async Task<List<VehicleTypeModel>> GetVehicleTypeDetails()
        {
            List<VehicleTypeModel> lstVehicleTypeModel = new List<VehicleTypeModel>();

            lstVehicleTypeModel = await _commonActionService.GetVehicleTypeDropdown();

            var SortedtList = lstVehicleTypeModel.OrderBy(x => x.VehicleTypeId).ToList();
            SortedtList.Insert(0, new VehicleTypeModel() { VehicleTypeId = 0, VehicleTypeName = "--Select--" });

            return SortedtList;
        }

        public async Task<CustomerCardInfo> AddCardDetails(string FormNumber)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();
            customerCardInfo.Remarks = "";
            customerCardInfo.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());
            customerCardInfo.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();
            if (string.IsNullOrEmpty(customerCardInfo.ExternalVehicleAPIStatus))
            {
                customerCardInfo.ExternalVehicleAPIStatus = "Y";
            }

            if (!string.IsNullOrEmpty(FormNumber))
            {
                var requestData = new CheckPancardbyDistrictIdRequestModel()
                {
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    FormNumber = FormNumber,
                    Type = "0"
                };

                CustomerResponseByReferenceNo customerResponseByReferenceNo;
                StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                var responseCustomer = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getNameAndFormNumberByReferenceNoForAddCard);

                customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(responseCustomer);
                if (customerResponseByReferenceNo.Internel_Status_Code == 1000)
                {
                    customerCardInfo.FormNumber = FormNumber;

                    if (customerResponseByReferenceNo.Data != null)
                    {
                        customerCardInfo.FormNumber = customerResponseByReferenceNo.Data[0].FormNumber;

                        customerCardInfo.CustomerName = customerResponseByReferenceNo.Data[0].CustomerName;

                        customerCardInfo.CustomerTypeName = customerResponseByReferenceNo.Data[0].CustomerTypeName;
                        customerCardInfo.CustomerTypeId = customerResponseByReferenceNo.Data[0].CustomerTypeId;
                        customerCardInfo.Status= customerResponseByReferenceNo.Data[0].Status;

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
                        if (customerCardInfo.CustomerTypeId == "905" || customerCardInfo.CustomerTypeId == "909")
                        {
                            customerCardInfo.NoOfVehiclesAllCards = customerCardInfo.NoOfCards;
                        }
                    }
                }

            }

            return customerCardInfo;
        }

        public async Task<CustomerCardInfo> GetCustomerDetails(string customerReferenceNo)
        {
            //fetching Customer info
            var CustomerRefinfo = new CustomerModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerReferenceNo = customerReferenceNo
            };

            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            CustomerResponseByReferenceNo customerResponseByReferenceNo;
            StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");

            var responseCustomer = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getCustomerByReferenceno);

            customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(responseCustomer);

            if(string.IsNullOrEmpty(customerCardInfo.FormNumber))
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

                    if (customerCardInfo.RBEId == "null")
                        customerCardInfo.RBEId = "";
                    else if (customerCardInfo.RBEId == null)
                        customerCardInfo.RBEId = "";
                    else if (customerCardInfo.RBEId == "0")
                        customerCardInfo.RBEId = "";

                    if (customerCardInfo.NoOfCards == "0")
                        customerCardInfo.NoOfCards = "";
                    customerCardInfo.RBEName = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEName) ? "" : customerResponseByReferenceNo.Data[0].RBEName;

                    if (customerCardInfo.CustomerTypeId == "905" || customerCardInfo.CustomerTypeId == "909")
                    {
                        customerCardInfo.NoOfVehiclesAllCards = customerCardInfo.NoOfCards;
                    }
                }
            }
            else
            {
                customerCardInfo.Remarks = customerResponseByReferenceNo.Message;
            }
            return customerCardInfo;
        }

        public async Task<CustomerCardInfo> GetCustomerRBEName(string RBEId)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            var requestInfo = new GetCustomerRBENameRquest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RBEId = RBEId
            };

            CustomerRBE customerRBE;
            StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(requestInfo), Encoding.UTF8, "application/json");

            var responseCustomer = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getCustomerRbeId);

            customerRBE = JsonConvert.DeserializeObject<CustomerRBE>(responseCustomer);


            if (customerRBE.Internel_Status_Code == 1000)
            {
                customerCardInfo.RBEName = customerRBE.Data[0].RBEName;
                customerCardInfo.RBEId = customerRBE.Data[0].RBEId.ToString();
                customerCardInfo.StatusCode = customerRBE.Internel_Status_Code;
            }
            else
            {
                customerCardInfo.Reason = customerRBE.Data[0].Reason;
                customerCardInfo.StatusCode = customerRBE.Internel_Status_Code;
            }
            return customerCardInfo;
        }

        public async Task<CustomerCardInfo> AddCardDetails(CustomerCardInfo customerCardInfo)
        {

            string feePaymentDate = "";

            if (!string.IsNullOrEmpty(customerCardInfo.FeePaymentDate))
            {
                feePaymentDate = await _commonActionService.changeDateFormat(customerCardInfo.FeePaymentDate);
            }

            feePaymentDate = (string.IsNullOrEmpty(feePaymentDate) ? "1900-01-01" : feePaymentDate);

            foreach (HPCL.Common.Models.ViewModel.Customer.CardDetails cardDetails in customerCardInfo.ObjCardDetail)
            {
                if (!string.IsNullOrEmpty(cardDetails.VechileNo))
                {
                    cardDetails.VechileNo = cardDetails.VechileNo.ToUpper();
                }

                if (customerCardInfo.CustomerTypeId == "902" || customerCardInfo.CustomerTypeId == "917")
                {
                    cardDetails.YearOfRegistration = "0";
                }
            }

            #region Create Request Info

            CustomerCardInsertInfo insertInfo = new CustomerCardInsertInfo();

            insertInfo.CustomerReferenceNo = customerCardInfo.CustomerReferenceNo;
            insertInfo.CustomerName = customerCardInfo.CustomerName;
            insertInfo.FormNumber = customerCardInfo.FormNumber;
            insertInfo.NoOfCards = customerCardInfo.NoOfCards;
            insertInfo.NoofVechileforAllCards = (String.IsNullOrEmpty(customerCardInfo.NoOfVehiclesAllCards) ? "0" : customerCardInfo.NoOfVehiclesAllCards);
            insertInfo.RBEId = customerCardInfo.RBEId;
            insertInfo.FeePaymentsCollectFeeWaiver = customerCardInfo.FeePaymentsCollectFeeWaiver;
            insertInfo.FeePaymentNo = customerCardInfo.FeePaymentNo;
            insertInfo.FeePaymentDate = feePaymentDate;
            insertInfo.CardPreference = customerCardInfo.CardPreference;
            insertInfo.RBEName = customerCardInfo.RBEName;
            insertInfo.Useragent = CommonBase.useragent;
            insertInfo.Userip = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            insertInfo.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            insertInfo.Createdby = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            insertInfo.ObjCardDetail = customerCardInfo.ObjCardDetail;
            insertInfo.VehicleNoVerifiedManually = (customerCardInfo.VehicleNoVerifiedManually ? "1" : "0");

            if (insertInfo.CardPreference.ToUpper() == "CARDLESS")
            {
                insertInfo.FeePaymentsCollectFeeWaiver = 0;
            }

            #endregion


            StringContent content = new StringContent(JsonConvert.SerializeObject(insertInfo), Encoding.UTF8, "application/json");

            CustomerInserCardResponse customerInserCardResponse;

            var responseCustomer = await _requestService.CommonRequestService(content, WebApiUrl.insertCustomerCard);


            customerInserCardResponse = JsonConvert.DeserializeObject<CustomerInserCardResponse>(responseCustomer);

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


        public async Task<CustomerModel> ValidateNewCustomer()
        {

            CustomerModel customerModel = new CustomerModel();
            customerModel.Remarks = "";

            customerModel.CustomerRegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());
            return customerModel;
        }

        public async Task<CustomerValidate> ValidateNewCustomer(CustomerValidate entity)
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

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerPendingForApproval);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SearchCustomerResponseGrid> searchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();

            entity.SearchCustomerResponseGridLst = searchList;
            entity.Message = obj["Message"].ToString();
            return entity;
        }
        public async Task<List<SearchCustomerResponseGrid>> ReloadUpdatedGrid()
        {
            var str = _httpContextAccessor.HttpContext.Session.GetString("viewUpdatedCustGrid");

            CustomerModel vGrid = JsonConvert.DeserializeObject<CustomerModel>(str);

            string fromDateOfApplication = "";
            string toDateOfApplication = "";

            if (!string.IsNullOrEmpty(vGrid.FromDate))
            {
                //string[] frmDate = vGrid.FromDate.Split("-");
                //fromDateOfApplication = frmDate[2] + "-" + frmDate[1] + "-" + frmDate[0];
                fromDateOfApplication = vGrid.FromDate;
            }
            if (!string.IsNullOrEmpty(vGrid.ToDate))
            {
                //string[] toDate = vGrid.ToDate.Split("-");
                //toDateOfApplication = toDate[2] + "-" + toDate[1] + "-" + toDate[0];
                toDateOfApplication = vGrid.ToDate;
            }

            var searchBody = new GetValidateNewCustomerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RegionalOfficeId = vGrid.CustomerRegionID > 0 ? vGrid.CustomerRegionID.ToString() : null,
                FromDate = fromDateOfApplication,
                ToDate = toDateOfApplication,
                FormNumber = vGrid.FormNumber,
                StateId = null,
                CustomerName = null
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerPendingForApproval);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SearchCustomerResponseGrid> updatedSearchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();

            return updatedSearchList;
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
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getcustomerdetailsByFormNumber);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

            return obj;
        }
        public async Task<CommonResponseData> AproveCustomer(string FormNumber, string Comments, string Approvalstatus)
        {
            var approvalBody = new AproveCustomerRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = FormNumber,
                Comments = Comments,
                Approvalstatus = Approvalstatus,
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(approvalBody), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.approveOrrejectcustomer);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CommonResponseData> insertKyc = jarr.ToObject<List<CommonResponseData>>();

            foreach (CommonResponseData item in insertKyc)
            {
                item.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());
            }

            return insertKyc[0];
        }
        public async Task<List<SalesAreaModel>> GetsalesAreaDetails(int RegionID)
        {
            var requestInfo = new SalesAreaRequestModal()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RegionID = RegionID.ToString()
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestInfo), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getSalesArea);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SalesAreaModel> lst = jarr.ToObject<List<SalesAreaModel>>();
            lst.Add(new SalesAreaModel
            {
                SalesAreaID = 0,
                SalesAreaName = "Select Sales Area"
            });
            var SortedtList = lst.OrderBy(x => x.SalesAreaID).ToList();
            return SortedtList;
        }

        public async Task<CustomerBalanceInfoModel> BalanceInfo()
        {
            CustomerBalanceInfoModel custMdl = new CustomerBalanceInfoModel();
            custMdl.Remarks = "";
            return custMdl;
        }

        public async Task<GetCustomerBalanceResponse> GetCustomerBalanceInfo(string CustomerID)
        {
            GetCustomerBalanceResponse customerBalanceResponse = new GetCustomerBalanceResponse();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerBalanceInfo);

            customerBalanceResponse = JsonConvert.DeserializeObject<GetCustomerBalanceResponse>(response);

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

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerCardWiseBalances);

            customerBalanceResponse = JsonConvert.DeserializeObject<GetCustomerCardWiseBalanceResponse>(response);

            return customerBalanceResponse;
        }
        public async Task<JObject> GetCustomerDetailsByCustomerID(string CustomerID)
        {
            var request = new GetCustomerByCustomerIdRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerByCustomerId);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

            return obj;
        }
        public async Task<CustomerCCMSBalanceModel> GetCCMSBalanceDetails(string CustomerID)
        {
            CustomerCCMSBalanceModel customerCCMSBalance = new CustomerCCMSBalanceModel();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getccmsbalanceinfoforcustomerid);

            customerCCMSBalance = JsonConvert.DeserializeObject<CustomerCCMSBalanceModel>(response);

            return customerCCMSBalance;
        }
        public async Task<string> GenerateFormNumber()
        {
            var request = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getFormNumber);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<FormNumberResponseModel> lstFormNumber = jarr.ToObject<List<FormNumberResponseModel>>();
            string FormNumber = lstFormNumber[0].FormNumber;

            return FormNumber;
        }
        public async Task<CustomerModel> UpdateCustomer(string FormNumber)
        {
            CustomerModel custMdl = new CustomerModel();
            custMdl.Remarks = "";

            custMdl.CustomerTypeMdl.AddRange(await _commonActionService.GetCustomerTypeListDropdown());

            custMdl.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());

            //custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType("1"));

            custMdl.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

            //custMdl.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

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
                var custResult = searchRes["GetCustomerDetails"].Value<JArray>();

                //var customerKYCDetailsResult = searchRes["CustomerKYCDetails"].Value<JArray>();

                List<CustomerFullDetails> customerList = custResult.ToObject<List<CustomerFullDetails>>();

                //List<UploadDocResponseBody> UploadDocList = customerKYCDetailsResult.ToObject<List<UploadDocResponseBody>>();

                CustomerFullDetails Customer = customerList.Where(t => t.FormNumber == FormNumber).FirstOrDefault();

                if (Customer != null)
                {
                    custMdl.SBUTypeID = Convert.ToInt32(String.IsNullOrEmpty(Customer.SBUTypeId) ? "0" : Customer.SBUTypeId);
                    custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType(custMdl.SBUTypeID.ToString()));
                    custMdl.FormNumber = Customer.FormNumber;
                    custMdl.CustomerReferenceNo = Customer.CustomerReferenceNo;
                    custMdl.CustomerTypeID = Convert.ToInt32(string.IsNullOrEmpty(Customer.CustomerTypeId) ? "0" : Customer.CustomerTypeId);

                    custMdl.CustomerSubTypeMdl.Clear();
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
                        //DateTime dateTime = new DateTime();
                        //dateTime = DateTime.ParseExact(Customer.DateOfApplication, "MM/dd/yyyy", null);
                        //custMdl.CustomerDateOfApplication = dateTime.Day + "-" + dateTime.Month + "-" + dateTime.Year;
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
                            //string[] subs = Customer.KeyOfficialDOA.Split('T');
                            //string[] date = subs[0].Split('-');
                            //custMdl.KeyOffDateOfAnniversary = date[2] + "-" + date[1] + "-" + date[0];

                            string[] subs = Customer.KeyOfficialDOA.Split(' ');
                            string[] date = subs[0].Split('/');
                            custMdl.KeyOffDateOfAnniversary = date[1] + "-" + date[0] + "-" + date[2];
                        }
                    }

                    if (!string.IsNullOrEmpty(Customer.KeyOfficialDOB))
                    {
                        if (!Customer.KeyOfficialDOB.Contains("1900"))
                        {
                            //string[] subs = Customer.KeyOfficialDOB.Split('T');
                            //string[] date = subs[0].Split('-');
                            //custMdl.KeyOfficialDOB = date[2] + "-" + date[1] + "-" + date[0];

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

                    custMdl.IsDuplicatePanNo = "1";
                    custMdl.AllowPanDuplication = "N";
                    if (!string.IsNullOrEmpty(custMdl.PanCardRemarks))
                    {
                        custMdl.IsDuplicatePanNo = "0";
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

        public async Task<CustomerModel> UpdateCustomer(CustomerModel cust)
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

            //string[] custDateOfApplication = cust.CustomerDateOfApplication.Split("-");

            //customerDateOfApplication = custDateOfApplication[2] + "-" + custDateOfApplication[1] + "-" + custDateOfApplication[0];

            //if (!string.IsNullOrEmpty(cust.KeyOffDateOfAnniversary))
            //{
            //    string[] dateOfAnniversary = cust.KeyOffDateOfAnniversary.Split("-");
            //    KeyOffDateOfAnniversary = dateOfAnniversary[2] + "-" + dateOfAnniversary[1] + "-" + dateOfAnniversary[0];
            //}

            //if (!string.IsNullOrEmpty(cust.KeyOfficialDOB))
            //{
            //    string[] officialDOB = cust.KeyOfficialDOB.Split("-");
            //    KeyOfficialDOB = officialDOB[2] + "-" + officialDOB[1] + "-" + officialDOB[0];
            //}

            customerDateOfApplication = await _commonActionService.changeDateFormat(cust.CustomerDateOfApplication);

            if (!string.IsNullOrEmpty(cust.KeyOffDateOfAnniversary))
            {
                //string[] dateOfAnniversary = cust.KeyOffDateOfAnniversary.Split("-");
                //KeyOffDateOfAnniversary = dateOfAnniversary[2] + "-" + dateOfAnniversary[1] + "-" + dateOfAnniversary[0];
                KeyOffDateOfAnniversary = await _commonActionService.changeDateFormat(cust.KeyOffDateOfAnniversary);
            }

            if (!string.IsNullOrEmpty(cust.KeyOfficialDOB))
            {
                //string[] officialDOB = cust.KeyOfficialDOB.Split("-");
                //KeyOfficialDOB = officialDOB[2] + "-" + officialDOB[1] + "-" + officialDOB[0];
                KeyOfficialDOB = await _commonActionService.changeDateFormat(cust.KeyOfficialDOB);
            }

            cust.KeyOfficialSecretQuestion = "0";
            cust.KeyOfficialSecretAnswer = "";
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
                    {"CustomerType", cust.CustomerTypeID.ToString()},
                    {"CustomerSubtype", cust.CustomerSubTypeID.ToString()},
                    {"TierOfCustomer", cust.TierOfCustomerID.ToString()},
                    {"TypeOfCustomer", cust.TypeOfCustomerID.ToString()},
                    {"PanCardRemarks", (String.IsNullOrEmpty(cust.PanCardRemarks)?"":cust.PanCardRemarks)}

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

            var contentString = await _requestService.CommonRequestService(content, WebApiUrl.updateCustomer);
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

                cust.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType("1"));

                cust.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(cust.CustomerZonalOfficeID));

                cust.SalesAreaMdl.AddRange(await _commonActionService.GetSalesAreaDropdown(cust.CustomerRegionID.ToString()));

                cust.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

                cust.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

                cust.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.CommunicationStateID.ToString()));

                //cust.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

                cust.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

                cust.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

                cust.PerOrRegAddressDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.PerOrRegAddressStateID.ToString()));
            }

            return cust;
        }

        public async Task<UploadDocResponseBody> UploadDoc(string FormNumber)
        {
            UploadDocResponseBody UploadDocResponseBody = new UploadDocResponseBody();

            if (!string.IsNullOrEmpty(FormNumber))
            {
                JObject obj = await ViewCustomerDetails(FormNumber);

                var searchRes = obj["Data"].Value<JObject>();
                var custResult = searchRes["GetCustomerDetails"].Value<JArray>();

                var customerKYCDetailsResult = searchRes["CustomerKYCDetails"].Value<JArray>();

                List<CustomerFullDetails> customerList = custResult.ToObject<List<CustomerFullDetails>>();

                List<UploadDocResponseBody> UploadDocList = customerKYCDetailsResult.ToObject<List<UploadDocResponseBody>>();

                UploadDocResponseBody = UploadDocList.Where(t => t.FormNumber == FormNumber).FirstOrDefault();

            }

            return UploadDocResponseBody;
        }

        public async Task<CustomerCardInfo> GetCustomerDetailsForAddCard(string customerReferenceNo)
        {
            //fetching Customer info
            var CustomerRefinfo = new CustomerModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = customerReferenceNo,
                Type = "0"
            };

            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            CustomerResponseByReferenceNo customerResponseByReferenceNo;
            StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");

            var responseCustomer = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getNameAndFormNumberByReferenceNoForAddCard);

            customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(responseCustomer);

            customerCardInfo.FormNumber = customerReferenceNo;


            if (customerResponseByReferenceNo.Internel_Status_Code == 1000)
            {
                customerCardInfo.CustomerReferenceNo = customerReferenceNo;
                customerCardInfo.FormNumber = customerReferenceNo;

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

                    if (customerCardInfo.CustomerTypeId == "905" || customerCardInfo.CustomerTypeId == "909")
                    {
                        customerCardInfo.NoOfVehiclesAllCards = customerCardInfo.NoOfCards;
                    }
                }
            }
            else
            {
                customerCardInfo.Remarks = customerResponseByReferenceNo.Message;
            }
            return customerCardInfo;
        }
        
        public async Task<CustomerCardInfo> GetAddCardPaymentDetailsPartialView([FromBody] List<CardDetails> arrs)
        {
            //JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            //List<CardDetails> arrs = objs.ToObject<List<CardDetails>>();

            CustomerCardInfo addAddOnCard = new CustomerCardInfo();
            addAddOnCard.Message = "";
            addAddOnCard.CardPreference = "";
            addAddOnCard.FeePaymentDate = "";
            addAddOnCard.FeePaymentNo = "";
            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();
            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            if (!string.IsNullOrEmpty(arrs[0].CardPreference))
                addAddOnCard.CardPreference = arrs[0].CardPreference;
            if (!string.IsNullOrEmpty(arrs[0].FeePaymentDate))
                addAddOnCard.FeePaymentDate = arrs[0].FeePaymentDate;
            if (!string.IsNullOrEmpty(arrs[0].FeePaymentNo) && arrs[0].FeePaymentNo != "0")
                addAddOnCard.FeePaymentNo = arrs[0].FeePaymentNo;
            if (Convert.ToInt32(arrs[0].FeePaymentsCollectFeeWaiver) > 0)
                addAddOnCard.FeePaymentsCollectFeeWaiver = Convert.ToInt32(arrs[0].FeePaymentsCollectFeeWaiver);
            addAddOnCard.CustomerTypeId = arrs[0].CustomerTypeId;
            addAddOnCard.VehicleNoVerifiedManually = Convert.ToBoolean(arrs[0].VehicleNoVerifiedManually);
            //addAddOnCard.TableStringyfiedData = str;
            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].CardIdentifier)) || (!string.IsNullOrEmpty(arrs[0].VechileNo))))
                addAddOnCard.ObjCardDetail = arrs;

            return addAddOnCard;
        }

        public async Task<CustomerCardInfo> GetCustomerAddCardsPartialView([FromBody] List<CardDetails> arrs)
        {
            //JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            //List<CardDetails> arrs = objs.ToObject<List<CardDetails>>();
            CustomerCardInfo addAddOnCard = new CustomerCardInfo();

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            if (!string.IsNullOrEmpty(arrs[0].CardPreference))
                addAddOnCard.CardPreference = arrs[0].CardPreference;
            if (!string.IsNullOrEmpty(arrs[0].FeePaymentDate))
                addAddOnCard.FeePaymentDate = arrs[0].FeePaymentDate;
            if (!string.IsNullOrEmpty(arrs[0].FeePaymentNo) && arrs[0].FeePaymentNo != "0")
                addAddOnCard.FeePaymentNo = arrs[0].FeePaymentNo;
            if (Convert.ToInt32(arrs[0].FeePaymentsCollectFeeWaiver) > 0)
                addAddOnCard.FeePaymentsCollectFeeWaiver = Convert.ToInt32(arrs[0].FeePaymentsCollectFeeWaiver);
            addAddOnCard.CustomerTypeId = arrs[0].CustomerTypeId;
            addAddOnCard.NoOfCards = arrs[0].NoOfCards;
            addAddOnCard.NoOfVehiclesAllCards = arrs[0].NoofVehicles;
            //addAddOnCard.TableStringyfiedData = str;
            addAddOnCard.VehicleNoVerifiedManually = Convert.ToBoolean(arrs[0].VehicleNoVerifiedManually);
            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].CardIdentifier)) || (!string.IsNullOrEmpty(arrs[0].VechileNo))))
                addAddOnCard.ObjCardDetail = arrs;

            if (addAddOnCard.CustomerTypeId == "905" || addAddOnCard.CustomerTypeId == "909")
            {
                addAddOnCard.NoOfGridRows = arrs[0].NoofVehicles;
            }
            else
            {
                addAddOnCard.NoOfGridRows = arrs[0].NoOfCards;
            }
            addAddOnCard.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();
            if (string.IsNullOrEmpty(addAddOnCard.ExternalVehicleAPIStatus))
            {
                addAddOnCard.ExternalVehicleAPIStatus = "Y";
            }

            return addAddOnCard;
        }

        public async Task<UpdateCustomerAddress> GetCustomerAddress(string CustomerId)
        {
            UpdateCustomerAddress custMdl = new UpdateCustomerAddress();
            custMdl.Remarks = "";

            var request = new EnrollToTransportManagementSystemModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.GetDetailsForCustomerUpdate);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<UpdateCustomerAddress> lst = jarr.ToObject<List<UpdateCustomerAddress>>();

            if (lst != null && lst.Count > 0 && lst[0].Status == 1)
            {
                custMdl = lst[0];

                custMdl.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
                if (string.IsNullOrEmpty(custMdl.ExternalPANAPIStatus))
                {
                    custMdl.ExternalPANAPIStatus = "Y";
                }

                custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                custMdl.CASID = custMdl.CommunicationStateId.ToString();
                custMdl.CADID = custMdl.CommunicationDistrictId.ToString();
                custMdl.PASID = custMdl.PermanentStateId.ToString();
                custMdl.PADID = custMdl.PermanentDistrictId.ToString();

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

                if (!string.IsNullOrEmpty(custMdl.PermanentFax))
                {
                    string[] subs = custMdl.PermanentFax.Split('-');

                    if (subs.Count() > 1)
                    {
                        custMdl.PermanentFaxCode = subs[0].ToString();
                        custMdl.PermanentFaxPhoneNumber = subs[1].ToString();
                    }
                    else
                    {
                        custMdl.PermanentFaxPhoneNumber = custMdl.PermanentFax;
                    }
                }

                if (!string.IsNullOrEmpty(custMdl.PermanentPhoneNo))
                {
                    string[] subs = custMdl.PermanentPhoneNo.Split('-');

                    if (subs.Count() > 1)
                    {
                        custMdl.PerOrRegAddressDialCode = subs[0].ToString();
                        custMdl.PerOrRegAddressPhoneNumber = subs[1].ToString();
                    }
                    else
                    {
                        custMdl.PerOrRegAddressPhoneNumber = custMdl.PermanentPhoneNo;
                    }
                }
            }
            else
            {
                custMdl.Message = obj["Message"].ToString();
                custMdl.CommunicationAddress1 = "";
                custMdl.Status = 0;
                custMdl.Reason = "";
                if (lst != null && lst.Count > 0 && lst[0].Status == 0)
                {
                    custMdl.Status = lst[0].Status;
                    custMdl.Reason = lst[0].Reason;
                }
                if (lst != null && lst.Count == 0)
                {
                    custMdl.Reason = obj["Message"].ToString();
                }
            }

            return custMdl;
        }

        public async Task<UpdateCustomerAddress> UpdateCustomerAddress()
        {
            UpdateCustomerAddress custMdl = new UpdateCustomerAddress();
            custMdl.Remarks = "";

            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

            custMdl.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
            if (string.IsNullOrEmpty(custMdl.ExternalPANAPIStatus))
            {
                custMdl.ExternalPANAPIStatus = "Y";
            }

            return custMdl;
        }

        public async Task<UpdateCustomerAddress> UpdateCustomerAddress(UpdateCustomerAddress model)
        {
            model.UserAgent = CommonBase.useragent;
            model.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CommunicationPhoneNo = (string.IsNullOrEmpty(model.CommunicationDialCode) ? "" : model.CommunicationDialCode) + "-" + (string.IsNullOrEmpty(model.CommunicationPhonePart2) ? "" : model.CommunicationPhonePart2);
            model.CommunicationFax = (string.IsNullOrEmpty(model.CommunicationFaxCode) ? "" : model.CommunicationFaxCode) + "-" + (string.IsNullOrEmpty(model.CommunicationFaxPart2) ? "" : model.CommunicationFaxPart2);
            model.PermanentPhoneNo = (string.IsNullOrEmpty(model.PerOrRegAddressDialCode) ? "" : model.PerOrRegAddressDialCode) + "-" + (string.IsNullOrEmpty(model.PerOrRegAddressPhoneNumber) ? "" : model.PerOrRegAddressPhoneNumber);
            model.PermanentFax = (string.IsNullOrEmpty(model.PermanentFaxCode) ? "" : model.PermanentFaxCode) + "-" + (string.IsNullOrEmpty(model.PermanentFaxPhoneNumber) ? "" : model.PermanentFaxPhoneNumber);

            if (!string.IsNullOrEmpty(model.CommunicationEmailid))
            {
                model.CommunicationEmailid = model.CommunicationEmailid.ToLower();
            }


            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateCustomerAddress);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            CustomerAddressUpdateResponse customerResponse = JsonConvert.DeserializeObject<CustomerAddressUpdateResponse>(response, settings);

            model.Internel_Status_Code = customerResponse.Internel_Status_Code;
            model.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    model.Remarks = customerResponse.Data[0].Reason;
                else
                    model.Remarks = customerResponse.Message;

                model.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                model.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(model.CommunicationStateId.ToString()));
                model.PerOrRegAddressDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(model.PermanentStateId.ToString()));
            }
            else
            {
                model.Remarks = customerResponse.Data[0].Reason;
            }

            return model;
        }

        public async Task<UpdateContactPersonDetailsModel> UpdateContactPersonDetails()
        {
            UpdateContactPersonDetailsModel custMdl = new UpdateContactPersonDetailsModel();
            custMdl.Remarks = "";

            return custMdl;
        }
        public async Task<UpdateContactPersonResponseDetails> GetUpdateContactPersonDetails(string CustomerId)
        {
            UpdateContactPersonResponseDetails custMdl = new UpdateContactPersonResponseDetails();
            custMdl.Message = "";

            var request = new EnrollToTransportManagementSystemModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getUpdateContactPersonDetails);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<UpdateContactPersonResponseDetails> lst = jarr.ToObject<List<UpdateContactPersonResponseDetails>>();

            if (lst != null && lst.Count > 0)
            {
                if (!string.IsNullOrEmpty(lst[0].FirstName))
                {
                    custMdl = lst[0];

                    if (!string.IsNullOrEmpty(custMdl.Ph_Office))
                    {
                        string[] subs = custMdl.Ph_Office.Split('-');

                        if (subs.Count() > 1)
                        {
                            custMdl.KeyOffPhoneCode = subs[0].ToString();
                            custMdl.KeyOffPhonePart2 = subs[1].ToString();
                        }
                        else
                        {
                            custMdl.KeyOffPhonePart2 = custMdl.Ph_Office;
                        }
                    }

                    if (!string.IsNullOrEmpty(custMdl.Fax))
                    {
                        string[] subs = custMdl.Fax.Split('-');

                        if (subs.Count() > 1)
                        {
                            custMdl.KeyOffFaxCode = subs[0].ToString();
                            custMdl.KeyOffFaxPart2 = subs[1].ToString();
                        }
                        else
                        {
                            custMdl.KeyOffFaxPart2 = custMdl.Fax;
                        }
                    }
                    if (!string.IsNullOrEmpty(custMdl.DateOfBirth) && custMdl.DateOfBirth.Contains("1900"))
                    {
                        custMdl.DateOfBirth = "";
                    }
                    if (!string.IsNullOrEmpty(custMdl.DateofAnniversary) && custMdl.DateofAnniversary.Contains("1900"))
                    {
                        custMdl.DateofAnniversary = "";
                    }
                }
                else
                {
                    custMdl.Message = lst[0].Reason;
                    custMdl.FirstName = "";
                }
            }
            else
            {
                custMdl.Message = obj["Message"].ToString();
                custMdl.FirstName = "";
            }

            return custMdl;
        }
        public async Task<UpdateContactPersonDetailsModel> UpdateContactPersonDetails(UpdateContactPersonDetailsModel model)
        {
            model.UserAgent = CommonBase.useragent;
            model.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.KeyOfficialPhoneNo = (string.IsNullOrEmpty(model.KeyOffPhoneCode) ? "" : model.KeyOffPhoneCode) + "-" + (string.IsNullOrEmpty(model.KeyOffPhonePart2) ? "" : model.KeyOffPhonePart2);
            model.KeyOfficialFax = (string.IsNullOrEmpty(model.KeyOffFaxCode) ? "" : model.KeyOffFaxCode) + "-" + (string.IsNullOrEmpty(model.KeyOffFaxPart2) ? "" : model.KeyOffFaxPart2);

            if (!string.IsNullOrEmpty(model.KeyOfficialEmail))
            {
                model.KeyOfficialEmail = model.KeyOfficialEmail.ToLower();
            }

            if (!string.IsNullOrEmpty(model.KeyDOA))
            {
                model.KeyOfficialDOA = await _commonActionService.changeDateFormat(model.KeyDOA);
            }
            else
            {
                model.KeyOfficialDOA = "1900-01-01";
            }

            if (!string.IsNullOrEmpty(model.KeyDOB))
            {
                model.KeyOfficialDOB = await _commonActionService.changeDateFormat(model.KeyDOB);
            }
            else
            {
                model.KeyOfficialDOB = "1900-01-01";
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateContactPersonDetails);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            CustomerAddressUpdateResponse customerResponse = JsonConvert.DeserializeObject<CustomerAddressUpdateResponse>(response, settings);

            model.Internel_Status_Code = customerResponse.Internel_Status_Code;
            model.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                {
                    model.Remarks = customerResponse.Data[0].Reason;
                    model.Internel_Status_Code = customerResponse.Internel_Status_Code;
                }
            }
            else
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0 && customerResponse.Data[0].Status != 1)
                {
                    model.Remarks = customerResponse.Data[0].Reason;
                    model.Status = customerResponse.Data[0].Status;
                    model.Internel_Status_Code = customerResponse.Internel_Status_Code + 1;
                }
                else if (customerResponse.Data != null && customerResponse.Data.Count > 0 && customerResponse.Data[0].Status == 1)
                {
                    model.Remarks = customerResponse.Data[0].Reason;
                    model.Status = customerResponse.Data[0].Status;
                }
            }

            return model;
        }
        public async Task<LowCCMSBalanceAlertConfigurationModel> CCMSBalanceAlert()
        {
            LowCCMSBalanceAlertConfigurationModel model = new LowCCMSBalanceAlertConfigurationModel();
            model.Remarks = "";

            return model;
        }
        public async Task<LowCCMSBalanceAlertConfigurationModel> GetCCMSBalAlertConfiguration(string CustomerID)
        {
            GetCCMSBalAlertConfigurationResponse configurationResponse = new GetCCMSBalAlertConfigurationResponse();
            LowCCMSBalanceAlertConfigurationModel returnValue = new LowCCMSBalanceAlertConfigurationModel();

            var Request = new GetCustomerBalanceRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getCcmsBalAlertConfiguration);

            configurationResponse = JsonConvert.DeserializeObject<GetCCMSBalAlertConfigurationResponse>(response);

            if (configurationResponse != null && configurationResponse.Data != null)
            {
                returnValue.IndividualOrgName = "";
                returnValue.NameOnCard = "";
                if (configurationResponse.Data.CCMSCustomerDetail != null && configurationResponse.Data.CCMSCustomerDetail.Count > 0)
                {
                    if (!string.IsNullOrEmpty(configurationResponse.Data.CCMSCustomerDetail[0].IndividualOrgName))
                        returnValue.IndividualOrgName = configurationResponse.Data.CCMSCustomerDetail[0].IndividualOrgName;
                    if (!string.IsNullOrEmpty(configurationResponse.Data.CCMSCustomerDetail[0].NameOnCard))
                        returnValue.NameOnCard = configurationResponse.Data.CCMSCustomerDetail[0].NameOnCard;
                }

                if (configurationResponse.Data.CCMSAmountDetail != null && configurationResponse.Data.CCMSAmountDetail.Count > 0)
                {
                    returnValue.Reason = "";
                    returnValue.Amount = "";
                    returnValue.Status = configurationResponse.Data.CCMSAmountDetail[0].Status;
                    if (!string.IsNullOrEmpty(configurationResponse.Data.CCMSAmountDetail[0].Reason))
                        returnValue.Reason = configurationResponse.Data.CCMSAmountDetail[0].Reason;
                    if (!string.IsNullOrEmpty(configurationResponse.Data.CCMSAmountDetail[0].Amount))
                        returnValue.Amount = configurationResponse.Data.CCMSAmountDetail[0].Amount;

                    if (returnValue.Amount == "0")
                    {
                        returnValue.Amount = "";
                    }
                }
            }

            return returnValue;
        }

        public async Task<UpdateKycResponse> UpdateCCMSBalAlertConfiguration(string CustomerID, string Amount, string ActionType)
        {
            var approvalBody = new UpdateCCMSBalAlertConfigurationRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID,
                Amount = Amount,
                ActionType = ActionType
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(approvalBody), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.updateCcmsbalAlertConfiguration);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<UpdateKycResponse> insertKyc = jarr.ToObject<List<UpdateKycResponse>>();

            return insertKyc[0];
        }

        public async Task<CustomerAddressApproveRequestModel> ApprovalUpdateCustomerAddress(CustomerAddressApproveRequestModel model)
        {
            if (string.IsNullOrEmpty(model.FromDate))
            {
                model.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            string strFromDate = "";
            string strToDate = "";
            if (!string.IsNullOrEmpty(model.FromDate))
            {
                string[] frmDate = model.FromDate.Split("-");
                strFromDate = frmDate[2] + "-" + frmDate[1] + "-" + frmDate[0];
            }
            if (!string.IsNullOrEmpty(model.ToDate))
            {
                string[] toDate = model.ToDate.Split("-");
                strToDate = toDate[2] + "-" + toDate[1] + "-" + toDate[0];
            }

            var reqBody = new GetCustomerDetailForEnrollmentApprovalRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FromDate = strFromDate,
                ToDate = strToDate
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.approveCustomerAddressRequests);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CustomerAddressApproveRequestModel res = obj.ToObject<CustomerAddressApproveRequestModel>();

            if (res != null && res.Data != null && res.Data.Count > 0)
            {
                model.Data = res.Data;
            }
            else
            {
                model.Data = new List<CustomerAddressApproveDetails>();
            }
            model.Message = res.Message;
            model.Internel_Status_Code = res.Internel_Status_Code;

            return model;
        }
        public async Task<CommonResponseData> ApproveCustomerAddressRequests([FromBody] ApproveCustomerAddressRequest model)
        {
            CommonResponseData responseData = new CommonResponseData();

            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.UserAgent = CommonBase.useragent;
            model.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(requestContent, WebApiUrl.approvalApproveCustomerAddressRequests);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var Jarr = obj["Data"].Value<JArray>();
            List<CommonResponseData> updateResponse = Jarr.ToObject<List<CommonResponseData>>();
            responseData = updateResponse[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            if (obj["Internel_Status_Code"].ToString() == "1000")
            {
                string msg = "";
                foreach (CommonResponseData item in updateResponse)
                {
                    msg = msg + item.Reason + " ";
                }
                responseData.Reason = msg;
                if (responseData.Status != 1)
                {
                    responseData.Internel_Status_Code = responseData.Internel_Status_Code + 1;
                }
            }
            else
            {
                responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());
                responseData.Status = Convert.ToInt32(obj["Status_Code"].ToString());
                string msg = "";
                foreach (CommonResponseData item in updateResponse)
                {
                    msg = msg + item.Reason + " ";
                }
                responseData.Reason = msg;
            }
            return responseData;
        }

        public async Task<CustomerAddressApproveRequestModel> ApprovalUpdateCustomerContactPerson(CustomerAddressApproveRequestModel model)
        {
            if (string.IsNullOrEmpty(model.FromDate))
            {
                model.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            string strFromDate = "";
            string strToDate = "";
            if (!string.IsNullOrEmpty(model.FromDate))
            {
                string[] frmDate = model.FromDate.Split("-");
                strFromDate = frmDate[2] + "-" + frmDate[1] + "-" + frmDate[0];
            }
            if (!string.IsNullOrEmpty(model.ToDate))
            {
                string[] toDate = model.ToDate.Split("-");
                strToDate = toDate[2] + "-" + toDate[1] + "-" + toDate[0];
            }

            var reqBody = new GetCustomerDetailForEnrollmentApprovalRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FromDate = strFromDate,
                ToDate = strToDate
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.requestApproveCustomerContactPersonDetails);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CustomerAddressApproveRequestModel res = obj.ToObject<CustomerAddressApproveRequestModel>();

            if (res != null && res.Data != null && res.Data.Count > 0)
            {
                model.Data = res.Data;
            }
            else
            {
                model.Data = new List<CustomerAddressApproveDetails>();
            }
            model.Message = res.Message;
            model.Internel_Status_Code = res.Internel_Status_Code;

            return model;
        }
        public async Task<CommonResponseData> ApproveCustomerContactPersonRequests([FromBody] ApproveCustomerContactPersonRequest model)
        {
            CommonResponseData responseData = new CommonResponseData();

            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.UserAgent = CommonBase.useragent;
            model.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(requestContent, WebApiUrl.approveCustomerContactPersonDetails);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var Jarr = obj["Data"].Value<JArray>();
            List<CommonResponseData> updateResponse = Jarr.ToObject<List<CommonResponseData>>();
            responseData = updateResponse[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            if (obj["Internel_Status_Code"].ToString() == "1000")
            {
                string msg = "";
                foreach (CommonResponseData item in updateResponse)
                {
                    msg = msg + item.Reason + " ";
                }
                responseData.Reason = msg;
                if (responseData.Status != 1)
                {
                    responseData.Internel_Status_Code = responseData.Internel_Status_Code + 1;
                }
            }
            else
            {
                responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());
                responseData.Status = Convert.ToInt32(obj["Status_Code"].ToString());
                string msg = "";
                foreach (CommonResponseData item in updateResponse)
                {
                    msg = msg + item.Reason + " ";
                }
                responseData.Reason = msg;
            }
            return responseData;
        }

        public async Task<GetCustomerAddressRequestForApproval> GetCustomerOldAndNewAddressList(string CustomerId)
        {
            var request = new GetCustomerAddressDetailsRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getApproveCustomerAddressRequests);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetCustomerAddressRequestForApproval customerDetails = obj.ToObject<GetCustomerAddressRequestForApproval>();
            if (customerDetails != null)
            {
                customerDetails.CustomerId = CustomerId;
                foreach (CustomerAddressDetails item in customerDetails.Data.ObjOldCustomerAddressValue)
                {
                    if (!string.IsNullOrEmpty(item.CommunicationFax) && item.CommunicationFax == "-")
                        item.CommunicationFax = "";
                    if (!string.IsNullOrEmpty(item.PermanentFax) && item.PermanentFax == "-")
                        item.PermanentFax = "";
                    if (!string.IsNullOrEmpty(item.CommunicationPhoneNo) && item.CommunicationPhoneNo == "-")
                        item.CommunicationPhoneNo = "";
                    if (!string.IsNullOrEmpty(item.PermanentPhoneNo) && item.PermanentPhoneNo == "-")
                        item.PermanentPhoneNo = "";
                }
                foreach (CustomerAddressDetails item in customerDetails.Data.ObjNewCustomerAddressValue)
                {
                    if (!string.IsNullOrEmpty(item.CommunicationFax) && item.CommunicationFax == "-")
                        item.CommunicationFax = "";
                    if (!string.IsNullOrEmpty(item.PermanentFax) && item.PermanentFax == "-")
                        item.PermanentFax = "";
                    if (!string.IsNullOrEmpty(item.CommunicationPhoneNo) && item.CommunicationPhoneNo == "-")
                        item.CommunicationPhoneNo = "";
                    if (!string.IsNullOrEmpty(item.PermanentPhoneNo) && item.PermanentPhoneNo == "-")
                        item.PermanentPhoneNo = "";
                }
            }

            return customerDetails;
        }
        public async Task<GetCustomerContactPersonRequestForApproval> GetCustomerOldAndNewContactPersonList(string CustomerId)
        {
            var request = new GetCustomerAddressDetailsRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.requestGetApproveCustomerContactPersonDetails);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetCustomerContactPersonRequestForApproval customerDetails = obj.ToObject<GetCustomerContactPersonRequestForApproval>();
            if (customerDetails != null)
            {
                customerDetails.CustomerId = CustomerId;
                foreach (CustomerContactPersonDetails item in customerDetails.Data.ObjOldCustomerContactValue)
                {
                    if (!string.IsNullOrEmpty(item.KeyOfficialDOA) && item.KeyOfficialDOA.Contains("1900"))
                    {
                        item.KeyOfficialDOA = "";
                    }
                    if (!string.IsNullOrEmpty(item.KeyOfficialDOB) && item.KeyOfficialDOB.Contains("1900"))
                    {
                        item.KeyOfficialDOB = "";
                    }
                    if (!string.IsNullOrEmpty(item.KeyOfficialPhoneNo) && item.KeyOfficialPhoneNo == "-")
                        item.KeyOfficialPhoneNo = "";
                    if (!string.IsNullOrEmpty(item.KeyOfficialFax) && item.KeyOfficialFax == "-")
                        item.KeyOfficialFax = "";
                }
                foreach (CustomerContactPersonDetails item in customerDetails.Data.ObjNewCustomerContactValue)
                {
                    if (!string.IsNullOrEmpty(item.KeyOfficialDOA) && item.KeyOfficialDOA.Contains("1900"))
                    {
                        item.KeyOfficialDOA = "";
                    }
                    if (!string.IsNullOrEmpty(item.KeyOfficialDOB) && item.KeyOfficialDOB.Contains("1900"))
                    {
                        item.KeyOfficialDOB = "";
                    }
                    if (!string.IsNullOrEmpty(item.KeyOfficialPhoneNo) && item.KeyOfficialPhoneNo == "-")
                        item.KeyOfficialPhoneNo = "";
                    if (!string.IsNullOrEmpty(item.KeyOfficialFax) && item.KeyOfficialFax == "-")
                        item.KeyOfficialFax = "";
                }
            }
            return customerDetails;
        }
        public async Task<CustomerResetPasswordViewModel> CustomerResetPassword(string CustomerId)
        {
            CustomerResetPasswordViewModel customerResetPasswordViewModel = new CustomerResetPasswordViewModel();
            var reqBody = new CustomerResetPasswordRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getcommunicationemailresetpassword);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            customerResetPasswordViewModel = obj.ToObject<CustomerResetPasswordViewModel>();
            return customerResetPasswordViewModel;
        }
        public async Task<List<SuccessResponse>> UpdateEmailResetPassword([FromBody] CustomerResetPasswordViewModel reqModel)
        {
            var reqBody = new CustomerResetPasswordRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = reqModel.CustomerId,
                AlternateEmailId = reqModel.AlternateEmailId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updatecommunicationemailresetpassword);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res;
        }
    }
}