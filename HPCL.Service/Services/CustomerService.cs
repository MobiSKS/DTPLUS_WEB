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

namespace HPCL.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public CustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<UploadDocResponse> UploadDoc(UploadDoc entity)
        {
            var searchBody = new UploadDoc
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerReferenceNo = entity.CustomerReferenceNo
            };

            _httpContextAccessor.HttpContext.Session.SetString("CustomerReferenceNoVal", entity.CustomerReferenceNo);

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.searchCustRefUrl);

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
            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("UserName")), "CreatedBy");
            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("UserName")), "Userid");
            form.Add(new StringContent(CommonBase.useragent), "Useragent");
            form.Add(new StringContent(CommonBase.userip), "Userip");

            var response = await _requestService.FormDataRequestService(form, WebApiUrl.UploadKycUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<UpdateKycResponse> insertKyc = jarr.ToObject<List<UpdateKycResponse>>();

            return (insertKyc[0].Reason + "," + _httpContextAccessor.HttpContext.Session.GetString("CustomerReferenceNoVal"));
        }

        public async Task<CustomerModel> OnlineForm()
        {
            CustomerModel custMdl = new CustomerModel();
            custMdl.Remarks = "";
            custMdl.CustomerTypeMdl.AddRange(await _commonActionService.GetCustomerTypeListDropdown());
            custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());

            custMdl.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());

            custMdl.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

            custMdl.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

            custMdl.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

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

            string[] custDateOfApplication = cust.CustomerDateOfApplication.Split("-");

            customerDateOfApplication = custDateOfApplication[2] + "-" + custDateOfApplication[1] + "-" + custDateOfApplication[0];

            if (!string.IsNullOrEmpty(cust.KeyOffDateOfAnniversary))
            {
                string[] dateOfAnniversary = cust.KeyOffDateOfAnniversary.Split("-");
                KeyOffDateOfAnniversary = dateOfAnniversary[2] + "-" + dateOfAnniversary[1] + "-" + dateOfAnniversary[0];
            }

            if (!string.IsNullOrEmpty(cust.KeyOfficialDOB))
            {
                string[] officialDOB = cust.KeyOfficialDOB.Split("-");
                KeyOfficialDOB = officialDOB[2] + "-" + officialDOB[1] + "-" + officialDOB[0];
            }


            var CustomerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
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
                    {"KeyOfficialSecretQuestion", cust.KeyOfficialSecretQuestion},
                    {"KeyOfficialSecretAnswer", (String.IsNullOrEmpty(cust.KeyOfficialSecretAnswer)?"":cust.KeyOfficialSecretAnswer)},
                    {"KeyOfficialTypeOfFleet", cust.CustomerTypeOfFleetID},
                    {"KeyOfficialCardAppliedFor", (String.IsNullOrEmpty(cust.KeyOfficialCardAppliedFor)?"":cust.KeyOfficialCardAppliedFor)},
                    {"KeyOfficialApproxMonthlySpendsonVechile1", cust.KeyOfficialApproxMonthlySpendsonVechile1.ToString()},
                    {"KeyOfficialApproxMonthlySpendsonVechile2", cust.KeyOfficialApproxMonthlySpendsonVechile2.ToString()},
                    {"AreaOfOperation", cust.AreaOfOperation},
                    {"FleetSizeNoOfVechileOwnedHCV", cust.FleetSizeNoOfVechileOwnedHCV.ToString()},
                    {"FleetSizeNoOfVechileOwnedLCV", cust.FleetSizeNoOfVechileOwnedLCV.ToString()},
                    {"FleetSizeNoOfVechileOwnedMUVSUV", cust.FleetSizeNoOfVechileOwnedMUVSUV.ToString()},
                    {"FleetSizeNoOfVechileOwnedCarJeep", cust.FleetSizeNoOfVechileOwnedCarJeep.ToString()},
                    {"NoOfCards", cust.NoOfCards.ToString()},
                    {"FeePaymentsCollectFeeWaiver", cust.FeePaymentsCollectFeeWaiver.ToString()},
                    {"Createdby", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"TierOfCustomer", cust.TierOfCustomerID.ToString()},
                    {"TypeOfCustomer", cust.TypeOfCustomerID.ToString()},
                    {"FormNumber", cust.FormNumber.ToString()},
                    {"PanCardRemarks", (String.IsNullOrEmpty(cust.PanCardRemarks)?"":cust.PanCardRemarks)}

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

            //
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

                cust.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());

                cust.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(cust.CustomerZonalOfficeID));

                cust.SalesAreaMdl.AddRange(await _commonActionService.GetSalesAreaDropdown(cust.CustomerRegionID.ToString()));

                cust.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

                cust.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());

                cust.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.CommunicationStateID.ToString()));

                cust.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

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
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
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
            var request = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getVehicleTpe);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<VehicleTypeModel> lstVehicleTypeModel = jarr.ToObject<List<VehicleTypeModel>>();

            var SortedtList = lstVehicleTypeModel.OrderBy(x => x.VehicleTypeId).ToList();
            return SortedtList;
        }

        public async Task<CustomerCardInfo> AddCardDetails(string customerReferenceNo)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();
            customerCardInfo.Remarks = "";
            customerCardInfo.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());


            if (!string.IsNullOrEmpty(customerReferenceNo))
            {

                //fetching Customer info
                var CustomerRefinfo = new Dictionary<string, string>
                    {
                        {"Useragent", CommonBase.useragent},
                        {"Userip", CommonBase.userip},
                        {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                        {"CustomerReferenceNo", customerReferenceNo}
                    };

                CustomerResponseByReferenceNo customerResponseByReferenceNo;
                StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");

                var responseCustomer = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getCustomerByReferenceno);

                customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(responseCustomer);
                if (customerResponseByReferenceNo.Internel_Status_Code == 1000)
                {
                    customerCardInfo.CustomerReferenceNo = customerReferenceNo;
                    customerCardInfo.FormNumber = customerResponseByReferenceNo.Data[0].FormNumber;

                    StringBuilder sb = new StringBuilder();

                    if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].Title.ToString()))
                        sb.Append(customerResponseByReferenceNo.Data[0].Title.ToString());

                    if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].KeyInitials.ToString()))
                        sb.Append(customerResponseByReferenceNo.Data[0].KeyInitials.ToString());

                    if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].FirstName.ToString()))
                        sb.Append(customerResponseByReferenceNo.Data[0].FirstName.ToString());

                    if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].FirstName.ToString()))
                        sb.Append(customerResponseByReferenceNo.Data[0].FirstName.ToString());

                    if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].MiddleName))
                        sb.Append(" " + customerResponseByReferenceNo.Data[0].MiddleName);

                    if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].LastName))
                        sb.Append(" " + customerResponseByReferenceNo.Data[0].LastName);

                    customerCardInfo.CustomerName = sb.ToString();

                    customerCardInfo.CustomerTypeName = customerResponseByReferenceNo.Data[0].CustomerTypeName;
                    customerCardInfo.CustomerTypeId = customerResponseByReferenceNo.Data[0].CustomerTypeId;

                    if (customerResponseByReferenceNo.Data != null)
                    {
                        customerCardInfo.PaymentType = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentType) ? "" : customerResponseByReferenceNo.Data[0].PaymentType;
                        customerCardInfo.PaymentReceivedDate = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentReceivedDate) ? "" : customerResponseByReferenceNo.Data[0].PaymentReceivedDate;
                        customerCardInfo.NoOfCards = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].NoOfCards) ? "" : customerResponseByReferenceNo.Data[0].NoOfCards;
                        customerCardInfo.ReceivedAmount = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].ReceivedAmount) ? "0" : customerResponseByReferenceNo.Data[0].ReceivedAmount;
                        customerCardInfo.RBEId = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEId) ? "0" : customerResponseByReferenceNo.Data[0].RBEId;
                        customerCardInfo.RBEName = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEName) ? "0" : customerResponseByReferenceNo.Data[0].RBEName;
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
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                CustomerReferenceNo = customerReferenceNo
            };

            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            CustomerResponseByReferenceNo customerResponseByReferenceNo;
            StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");

            var responseCustomer = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getCustomerByReferenceno);

            customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(responseCustomer);


            if (customerResponseByReferenceNo.Internel_Status_Code == 1000)
            {
                customerCardInfo.CustomerReferenceNo = customerReferenceNo;
                customerCardInfo.FormNumber = customerResponseByReferenceNo.Data[0].FormNumber;

                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].FirstName.ToString()))
                    sb.Append(customerResponseByReferenceNo.Data[0].FirstName.ToString());

                if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].MiddleName))
                    sb.Append(" " + customerResponseByReferenceNo.Data[0].MiddleName);

                if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].LastName))
                    sb.Append(" " + customerResponseByReferenceNo.Data[0].LastName);


                customerCardInfo.CustomerName = sb.ToString();

                customerCardInfo.CustomerTypeName = customerResponseByReferenceNo.Data[0].CustomerTypeName;
                customerCardInfo.CustomerTypeId = customerResponseByReferenceNo.Data[0].CustomerTypeId;
                if (customerResponseByReferenceNo.Data != null)
                {
                    customerCardInfo.PaymentType = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentType) ? "" : customerResponseByReferenceNo.Data[0].PaymentType;
                    customerCardInfo.PaymentReceivedDate = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentReceivedDate) ? "" : customerResponseByReferenceNo.Data[0].PaymentReceivedDate;
                    customerCardInfo.NoOfCards = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].NoOfCards) ? "" : customerResponseByReferenceNo.Data[0].NoOfCards;
                    customerCardInfo.ReceivedAmount = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].ReceivedAmount) ? "0" : customerResponseByReferenceNo.Data[0].ReceivedAmount;
                    customerCardInfo.RBEId = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEId) ? "0" : customerResponseByReferenceNo.Data[0].RBEId;
                    if (customerCardInfo.RBEId == "0")
                        customerCardInfo.RBEId = "";
                    if (customerCardInfo.NoOfCards == "0")
                        customerCardInfo.NoOfCards = "";
                    customerCardInfo.RBEName = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEName) ? "0" : customerResponseByReferenceNo.Data[0].RBEName;

                    if (customerCardInfo.CustomerTypeId == "905" || customerCardInfo.CustomerTypeId == "909")
                    {
                        customerCardInfo.NoOfVehiclesAllCards = customerCardInfo.NoOfCards;
                    }
                }
            }
            return customerCardInfo;
        }

        public async Task<CustomerCardInfo> GetCustomerRBEName(string RBEId)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            //fetching Customer info
            var CustomerRefinfo = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"RBEId", RBEId}
            };

            CustomerRBE customerRBE;
            StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");

            var responseCustomer = await _requestService.CommonRequestService(custRefcontent, WebApiUrl.getCustomerRbeId);

            customerRBE = JsonConvert.DeserializeObject<CustomerRBE>(responseCustomer);


            if (customerRBE.Internel_Status_Code == 1000)
            {
                customerCardInfo.RBEName = customerRBE.Data[0].RBEName;
                customerCardInfo.RBEId = customerRBE.Data[0].RBEId.ToString();
            }

            return customerCardInfo;
        }

        public async Task<CustomerCardInfo> AddCardDetails(CustomerCardInfo customerCardInfo)
        {

            string feePaymentDate = "";

            if (!string.IsNullOrEmpty(customerCardInfo.FeePaymentDate))
            {
                string[] arrFeePaymentDate = customerCardInfo.FeePaymentDate.Split("-");
                feePaymentDate = arrFeePaymentDate[2] + "-" + arrFeePaymentDate[1] + "-" + arrFeePaymentDate[0];
            }

            feePaymentDate = (string.IsNullOrEmpty(feePaymentDate) ? "1900-01-01" : feePaymentDate);


            #region Create Request Info

            CustomerCardInsertInfo insertInfo = new CustomerCardInsertInfo();

            insertInfo.CustomerReferenceNo = customerCardInfo.CustomerReferenceNo;
            insertInfo.CustomerName = customerCardInfo.CustomerName;
            insertInfo.FormNumber = customerCardInfo.FormNumber;
            insertInfo.NoOfCards = customerCardInfo.NoOfCards;
            insertInfo.RBEId = customerCardInfo.RBEId;
            insertInfo.FeePaymentsCollectFeeWaiver = customerCardInfo.FeePaymentsCollectFeeWaiver;
            insertInfo.FeePaymentNo = customerCardInfo.FeePaymentNo;
            insertInfo.FeePaymentDate = feePaymentDate;
            insertInfo.CardPreference = customerCardInfo.CardPreference;
            insertInfo.RBEName = customerCardInfo.RBEName;
            insertInfo.RBEName = customerCardInfo.RBEName;
            insertInfo.Useragent = CommonBase.useragent;
            insertInfo.Userip = CommonBase.userip;
            insertInfo.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            insertInfo.Createdby = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            insertInfo.ObjCardDetail = customerCardInfo.ObjCardDetail;

            if (insertInfo.CardPreference.ToUpper() == "CARDLESS")
            {
                insertInfo.FeePaymentsCollectFeeWaiver = 0;
            }

            #endregion


            StringContent content = new StringContent(JsonConvert.SerializeObject(insertInfo), Encoding.UTF8, "application/json");

            CustomerInserCardResponse customerInserCardResponse;

            var responseCustomer = await _requestService.CommonRequestService(content, WebApiUrl.insertCustomerCard);


            customerInserCardResponse = JsonConvert.DeserializeObject<CustomerInserCardResponse>(responseCustomer);

            if (customerInserCardResponse.Internel_Status_Code == 1000)
            {
                customerCardInfo.Status = customerInserCardResponse.Data[0].Status;
                customerCardInfo.StatusCode = customerInserCardResponse.Internel_Status_Code;
                customerCardInfo.Message = customerInserCardResponse.Message;
            }
            else
            {
                customerCardInfo.Message = customerInserCardResponse.Message;
            }

            //if (customerInserCardResponse.Internel_Status_Code == 1000)
            //{
            //    ViewBag.Message = "Customer card details saved Successfully";
            //    //return RedirectToAction("SuccessRedirect", new { customerReferenceNo = customerResponse.Data[0].CustomerReferenceNo });
            //    customerCardInfo.Status = customerInserCardResponse.Data[0].Status;
            //    customerCardInfo.StatusCode = customerInserCardResponse.Internel_Status_Code;
            //    ModelState.Clear();
            //}
            //else
            //{
            //    ViewBag.Message = customerInserCardResponse.Message;
            //}


            return customerCardInfo;
        }


        public async Task<CustomerModel> ValidateNewCustomer()
        {

            CustomerModel customerModel = new CustomerModel();

            var request = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getOfficerType);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<OfficerTypeModel> lst = jarr.ToObject<List<OfficerTypeModel>>();

            customerModel.OfficerTypeMdl.AddRange(lst);

            return customerModel;
        }

        public async Task<List<SearchCustomerResponseGrid>> ValidateNewCustomer(CustomerModel entity)
        {
            var searchBody = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"Createdby" , entity.OfficerTypeID>0? entity.OfficerTypeID.ToString(): null },
                {"Createdon" , entity.CustomerDateOfApplication},
                {"FormNumber" , entity.FormNumber},
                {"StateId" , null},
                {"CustomerName" , null}
            };

            _httpContextAccessor.HttpContext.Session.SetString("viewUpdatedCustGrid", JsonConvert.SerializeObject(searchBody));

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerPendingForApproval);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SearchCustomerResponseGrid> searchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();

            return searchList;
        }
        public async Task<List<SearchCustomerResponseGrid>> ReloadUpdatedGrid()
        {
            var str = _httpContextAccessor.HttpContext.Session.GetString("viewUpdatedCustGrid");

            CustomerModel vGrid = JsonConvert.DeserializeObject<CustomerModel>(str);

            var searchBody = new Dictionary<string, string>
            {
               {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"OfficerTypeID", vGrid.OfficerTypeID > 0 ? vGrid.OfficerTypeID.ToString() : null},
                {"CustomerDateOfApplication", vGrid.CustomerDateOfApplication},
                {"FormNumber", vGrid.FormNumber},
                {"StateId" , null},
                {"CustomerName" , null}
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
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                FormNumber = FormNumber
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(customerBody), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.getcustomerdetailsByFormNumber);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

            return obj;
        }
        public async Task<UpdateKycResponse> AproveCustomer(string CustomerReferenceNo, string Comments, string Approvalstatus)
        {
            var approvalBody = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"CustomerReferenceNo" , CustomerReferenceNo},
                {"Comments" , Comments},
                {"Approvalstatus" , Approvalstatus},
                {"ApprovedBy" , _httpContextAccessor.HttpContext.Session.GetString("UserName")}
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(approvalBody), Encoding.UTF8, "application/json");
            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.approveOrrejectcustomer);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<UpdateKycResponse> insertKyc = jarr.ToObject<List<UpdateKycResponse>>();

            return insertKyc[0];
        }
        public async Task<List<SalesAreaModel>> GetsalesAreaDetails(int RegionID)
        {
            var customerRegion = new Dictionary<string, string>
             {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"RegionID", RegionID.ToString() }

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(customerRegion), Encoding.UTF8, "application/json");
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
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
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
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
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
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
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
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
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
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
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

            custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());

            custMdl.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());

            custMdl.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

            custMdl.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

            custMdl.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

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
                    custMdl.FormNumber = Customer.FormNumber;
                    custMdl.CustomerReferenceNo = Customer.CustomerReferenceNo;
                    custMdl.CustomerTypeID = Convert.ToInt32(string.IsNullOrEmpty(Customer.CustomerTypeId) ? "0" : Customer.CustomerTypeId);

                    custMdl.CustomerSubTypeMdl.AddRange(await _commonActionService.GetCustomerSubTypeDropdown(custMdl.CustomerTypeID));

                    custMdl.CustomerSubTypeID = Convert.ToInt32(string.IsNullOrEmpty(Customer.CustomerSubtypeId) ? "0" : Customer.CustomerSubtypeId);
                    custMdl.CustomerZonalOfficeID = Convert.ToInt32(string.IsNullOrEmpty(Customer.ZonalOfficeID) ? "0" : Customer.ZonalOfficeID);

                    custMdl.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(custMdl.CustomerZonalOfficeID));

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
                            custMdl.PermanentFax = Customer.CommunicationFax;
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
                        //string[] subs = Customer.KeyOfficialDOA.Split('T');
                        //string[] date = subs[0].Split('-');
                        //custMdl.KeyOffDateOfAnniversary = date[2] + "-" + date[1] + "-" + date[0];

                        string[] subs = Customer.KeyOfficialDOA.Split(' ');
                        string[] date = subs[0].Split('/');
                        custMdl.KeyOffDateOfAnniversary = date[1] + "-" + date[0] + "-" + date[2];
                    }

                    if (!string.IsNullOrEmpty(Customer.KeyOfficialDOB))
                    {
                        //string[] subs = Customer.KeyOfficialDOB.Split('T');
                        //string[] date = subs[0].Split('-');
                        //custMdl.KeyOfficialDOB = date[2] + "-" + date[1] + "-" + date[0];

                        string[] subs = Customer.KeyOfficialDOB.Split(' ');
                        string[] date = subs[0].Split('/');
                        custMdl.KeyOfficialDOB = date[1] + "-" + date[0] + "-" + date[2];
                    }

                    custMdl.CustomerTypeOfFleetID = (string.IsNullOrEmpty(Customer.KeyOfficialTypeOfFleetId) ? "0" : Customer.KeyOfficialTypeOfFleetId);
                    custMdl.KeyOfficialCardAppliedFor = Customer.KeyOfficialCardAppliedFor;
                    custMdl.KeyOfficialApproxMonthlySpendsonVechile1 = Convert.ToDecimal(string.IsNullOrEmpty(Customer.KeyOfficialApproxMonthlySpendsonVechile1) ? "0" : Customer.KeyOfficialApproxMonthlySpendsonVechile1);
                    custMdl.KeyOfficialApproxMonthlySpendsonVechile2 = Convert.ToDecimal(string.IsNullOrEmpty(Customer.KeyOfficialApproxMonthlySpendsonVechile2) ? "0" : Customer.KeyOfficialApproxMonthlySpendsonVechile2);
                    custMdl.FleetSizeNoOfVechileOwnedHCV = Convert.ToInt32(string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedHCV) ? "0" : Customer.FleetSizeNoOfVechileOwnedHCV);
                    custMdl.FleetSizeNoOfVechileOwnedLCV = Convert.ToInt32(string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedLCV) ? "0" : Customer.FleetSizeNoOfVechileOwnedLCV);
                    custMdl.FleetSizeNoOfVechileOwnedMUVSUV = Convert.ToInt32(string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedMUVSUV) ? "0" : Customer.FleetSizeNoOfVechileOwnedMUVSUV);
                    custMdl.FleetSizeNoOfVechileOwnedCarJeep = Convert.ToInt32(string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedCarJeep) ? "0" : Customer.FleetSizeNoOfVechileOwnedCarJeep);
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

            string[] custDateOfApplication = cust.CustomerDateOfApplication.Split("-");

            customerDateOfApplication = custDateOfApplication[2] + "-" + custDateOfApplication[1] + "-" + custDateOfApplication[0];

            if (!string.IsNullOrEmpty(cust.KeyOffDateOfAnniversary))
            {
                string[] dateOfAnniversary = cust.KeyOffDateOfAnniversary.Split("-");
                KeyOffDateOfAnniversary = dateOfAnniversary[2] + "-" + dateOfAnniversary[1] + "-" + dateOfAnniversary[0];
            }

            if (!string.IsNullOrEmpty(cust.KeyOfficialDOB))
            {
                string[] officialDOB = cust.KeyOfficialDOB.Split("-");
                KeyOfficialDOB = officialDOB[2] + "-" + officialDOB[1] + "-" + officialDOB[0];
            }


            var CustomerTypeForms = new Dictionary<string, string>
                {
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"CustomerReferenceNo", cust.CustomerReferenceNo},
                    {"ZonalOffice", cust.CustomerZonalOfficeID.ToString()},
                    {"RegionalOffice", cust.CustomerRegionID.ToString()},
                    {"DateOfApplication", customerDateOfApplication},
                    {"SalesArea", cust.CustomerSalesAreaID.ToString()},
                    {"ModifiedBy", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
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
                    {"KeyOfficialApproxMonthlySpendsonVechile1", cust.KeyOfficialApproxMonthlySpendsonVechile1.ToString()},
                    {"KeyOfficialApproxMonthlySpendsonVechile2", cust.KeyOfficialApproxMonthlySpendsonVechile2.ToString()},
                    {"AreaOfOperation", cust.AreaOfOperation},
                    {"FleetSizeNoOfVechileOwnedHCV", cust.FleetSizeNoOfVechileOwnedHCV.ToString()},
                    {"FleetSizeNoOfVechileOwnedLCV", cust.FleetSizeNoOfVechileOwnedLCV.ToString()},
                    {"FleetSizeNoOfVechileOwnedMUVSUV", cust.FleetSizeNoOfVechileOwnedMUVSUV.ToString()},
                    {"FleetSizeNoOfVechileOwnedCarJeep", cust.FleetSizeNoOfVechileOwnedCarJeep.ToString()},
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

                cust.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());

                cust.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(cust.CustomerZonalOfficeID));

                cust.SalesAreaMdl.AddRange(await _commonActionService.GetSalesAreaDropdown(cust.CustomerRegionID.ToString()));

                cust.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

                cust.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());

                cust.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(cust.CommunicationStateID.ToString()));

                cust.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

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

    }
}