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

            //Fetching CustomerType
            var CustType = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"CTFlag",  "1" }
                };


            StringContent custTypeContent = new StringContent(JsonConvert.SerializeObject(CustType), Encoding.UTF8, "application/json");

            var responseCustType = await _requestService.CommonRequestService(custTypeContent, WebApiUrl.getCustomerType);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseCustType).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTypeModel> lst = jarr.ToObject<List<CustomerTypeModel>>();
            custMdl.CustomerTypeMdl.AddRange(lst);


            custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());


            //fetching Type of Business Entity
            var TBEntityForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}

                };
            StringContent TBEntitycontent = new StringContent(JsonConvert.SerializeObject(TBEntityForms), Encoding.UTF8, "application/json");

            var responseTBEntity = await _requestService.CommonRequestService(TBEntitycontent, WebApiUrl.getTbentityName);

            obj = JObject.Parse(JsonConvert.DeserializeObject(responseTBEntity).ToString());
            jarr = obj["Data"].Value<JArray>();
            List<CustomerTbentityModel> lstTBEntity = jarr.ToObject<List<CustomerTbentityModel>>();
            custMdl.CustomerTbentityMdl.AddRange(lstTBEntity);


            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());


            custMdl.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());


            //Fetching Type of Fleet
            var CustomerTypeOfFleetForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}

                };
            StringContent TypeOfFleetcontent = new StringContent(JsonConvert.SerializeObject(CustomerTypeOfFleetForms), Encoding.UTF8, "application/json");

            var responseTypeOfFleet = await _requestService.CommonRequestService(TypeOfFleetcontent, WebApiUrl.getTypeOfFleet);

            obj = JObject.Parse(JsonConvert.DeserializeObject(responseTypeOfFleet).ToString());
            jarr = obj["Data"].Value<JArray>();
            List<CustomerTypeOfFleetModel> lstTypeOfFleet = jarr.ToObject<List<CustomerTypeOfFleetModel>>();
            custMdl.CustomerTypeOfFleetMdl.AddRange(lstTypeOfFleet);


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
                     {"FormNumber", cust.FormNumber.ToString()}

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
                //Fetching CustomerType
                var CustType = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                                    {"CTFlag",  "1" }
                                };


                StringContent contentCustomerType = new StringContent(JsonConvert.SerializeObject(CustType), Encoding.UTF8, "application/json");

                var responseCustomerType = await _requestService.CommonRequestService(contentCustomerType, WebApiUrl.getCustomerType);

                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseCustomerType).ToString());
                var jarr = obj["Data"].Value<JArray>();
                List<CustomerTypeModel> lstCustomerType = jarr.ToObject<List<CustomerTypeModel>>();
                cust.CustomerTypeMdl.AddRange(lstCustomerType);


                //Customer SubType
                var customerSubType = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                                    { "CustomerTypeId", cust.CustomerTypeID.ToString() }
                                };

                StringContent contentCustomerSubType = new StringContent(JsonConvert.SerializeObject(customerSubType), Encoding.UTF8, "application/json");

                var responseCustomerSubType = await _requestService.CommonRequestService(contentCustomerSubType, WebApiUrl.getCustomerSubType);

                obj = JObject.Parse(JsonConvert.DeserializeObject(responseCustomerSubType).ToString());
                jarr = obj["Data"].Value<JArray>();
                List<CustomerSubTypeModel> lstCustomerSubType = jarr.ToObject<List<CustomerSubTypeModel>>();

                lstCustomerSubType.Add(new CustomerSubTypeModel
                {
                    CustomerSubTypeID = 0,
                    CustomerSubTypeName = "Select Customer Sub type"
                });
                var SortedtList = lstCustomerSubType.OrderBy(x => x.CustomerSubTypeID).ToList();
                cust.CustomerSubTypeMdl.AddRange(SortedtList);


                //using (var CustomerSubTypeResponse = await client.PostAsync(WebApiUrl.getCustomerSubType, contentCustomerSubType))
                //{
                //    if (CustomerSubTypeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = CustomerSubTypeResponse.Content.ReadAsStringAsync().Result;

                //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                //        var jarr = obj["Data"].Value<JArray>();
                //        List<CustomerSubTypeModel> lst = jarr.ToObject<List<CustomerSubTypeModel>>();
                //        lst.Add(new CustomerSubTypeModel
                //        {
                //            CustomerSubTypeID = 0,
                //            CustomerSubTypeName = "Select Customer Subtype"
                //        });
                //        var SortedtList = lst.OrderBy(x => x.CustomerSubTypeID).ToList();
                //        cust.CustomerSubTypeMdl.AddRange(lst);
                //    }
                //    else
                //    {
                //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                //    }
                //}

                cust.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());


                //using (var zonalOfficeResponse = await client.PostAsync(WebApiUrl.getZonalOffice, Zonecontent))
                //{
                //    if (zonalOfficeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = zonalOfficeResponse.Content.ReadAsStringAsync().Result;

                //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                //        var jarr = obj["Data"].Value<JArray>();
                //        List<CustomerZonalOfficeModel> lst = jarr.ToObject<List<CustomerZonalOfficeModel>>();
                //        cust.CustomerZonalOfficeMdl.AddRange(lst);

                //    }
                //    else
                //    {
                //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                //    }
                //}

                //Regional Office
                var customerRegionalOffice = new Dictionary<string, string>
                                    {
                                        {"Useragent", CommonBase.useragent},
                                        {"Userip", CommonBase.userip},
                                        {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                                        {"ZonalId", cust.CustomerZonalOfficeID.ToString() }

                                };

                StringContent customerRegionalOfficecontent = new StringContent(JsonConvert.SerializeObject(customerRegionalOffice), Encoding.UTF8, "application/json");

                var responseRegionalOffice = await _requestService.CommonRequestService(customerRegionalOfficecontent, WebApiUrl.getRegionalOffice);

                obj = JObject.Parse(JsonConvert.DeserializeObject(responseRegionalOffice).ToString());
                jarr = obj["Data"].Value<JArray>();
                List<CustomerRegionModel> lstRegionModel = jarr.ToObject<List<CustomerRegionModel>>();

                cust.CustomerRegionMdl.AddRange(lstRegionModel);

                //using (var customerRegionalOfficeResponse = await client.PostAsync(WebApiUrl.getRegionalOffice, customerRegionalOfficecontent))
                //{
                //    if (customerRegionalOfficeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = customerRegionalOfficeResponse.Content.ReadAsStringAsync().Result;

                //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                //        var jarr = obj["Data"].Value<JArray>();
                //        List<CustomerRegionModel> lst = jarr.ToObject<List<CustomerRegionModel>>();

                //        //lst.Add(new CustomerRegionModel
                //        //{
                //        //    RegionalOfficeID = 0,
                //        //    RegionalOfficeName = "Select Region",

                //        //});
                //        //var SortedtList = lst.OrderBy(x => x.RegionalOfficeID).ToList();
                //        //cust.CustomerRegionMdl.AddRange(SortedtList);
                //        cust.CustomerRegionMdl.AddRange(lst);
                //    }
                //    else
                //    {
                //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                //    }
                //}

                //sales Area Dropdown
                var saleAreaReqData = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                                    {"RegionID", cust.CustomerRegionID.ToString() }
                                };


                StringContent contentRegionData = new StringContent(JsonConvert.SerializeObject(saleAreaReqData), Encoding.UTF8, "application/json");

                var responseSalesArea = await _requestService.CommonRequestService(contentRegionData, WebApiUrl.getSalesArea);

                obj = JObject.Parse(JsonConvert.DeserializeObject(responseSalesArea).ToString());
                jarr = obj["Data"].Value<JArray>();
                List<SalesAreaModel> lstSalesArea = jarr.ToObject<List<SalesAreaModel>>();

                cust.SalesAreaMdl.AddRange(lstSalesArea);

                //using (var SalesAreaResponse = await client.PostAsync(WebApiUrl.getSalesArea, contentRegionData))
                //{
                //    if (SalesAreaResponse.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = SalesAreaResponse.Content.ReadAsStringAsync().Result;

                //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                //        var jarr = obj["Data"].Value<JArray>();
                //        List<SalesAreaModel> lst = jarr.ToObject<List<SalesAreaModel>>();
                //        cust.SalesAreaMdl.AddRange(lst);
                //    }
                //    else
                //    {
                //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                //    }
                //}


                //fetching Type of Business Entity
                var TBEntityForms = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}

                                };
                StringContent TBEntitycontent = new StringContent(JsonConvert.SerializeObject(TBEntityForms), Encoding.UTF8, "application/json");

                var responseTBEntity = await _requestService.CommonRequestService(TBEntitycontent, WebApiUrl.getTbentityName);

                obj = JObject.Parse(JsonConvert.DeserializeObject(responseTBEntity).ToString());
                jarr = obj["Data"].Value<JArray>();
                List<CustomerTbentityModel> lstTBEntity = jarr.ToObject<List<CustomerTbentityModel>>();

                cust.CustomerTbentityMdl.AddRange(lstTBEntity);

                //using (var TbentityNameResponse = await client.PostAsync(WebApiUrl.getTbentityName, TBEntitycontent))
                //{
                //    if (TbentityNameResponse.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = TbentityNameResponse.Content.ReadAsStringAsync().Result;

                //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                //        var jarr = obj["Data"].Value<JArray>();
                //        List<CustomerTbentityModel> lst = jarr.ToObject<List<CustomerTbentityModel>>();
                //        cust.CustomerTbentityMdl.AddRange(lst);
                //    }
                //    else
                //    {
                //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                //    }
                //}


                cust.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());


                var districtModel = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                                    {"StateID", cust.CommunicationStateID.ToString()}
                                };

                StringContent districtModelContent = new StringContent(JsonConvert.SerializeObject(districtModel), Encoding.UTF8, "application/json");

                var responseDistrict = await _requestService.CommonRequestService(districtModelContent, WebApiUrl.getDistrict);

                obj = JObject.Parse(JsonConvert.DeserializeObject(responseDistrict).ToString());
                jarr = obj["Data"].Value<JArray>();
                List<OfficerDistrictModel> lstistrict = jarr.ToObject<List<OfficerDistrictModel>>();

                cust.CommunicationDistrictMdl.AddRange(lstistrict);

                //using (var districtModelResponse = await client.PostAsync(WebApiUrl.getDistrict, districtModelContent))
                //{
                //    if (districtModelResponse.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = districtModelResponse.Content.ReadAsStringAsync().Result;

                //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                //        var jarr = obj["Data"].Value<JArray>();
                //        List<OfficerDistrictModel> lst = jarr.ToObject<List<OfficerDistrictModel>>();

                //        cust.CommunicationDistrictMdl.AddRange(lst);
                //    }
                //    else
                //    {
                //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                //    }
                //}



                cust.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());


                //Fetching Type of Fleet
                var CustomerTypeOfFleetForms = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}

                                };
                StringContent TypeOfFleetcontent = new StringContent(JsonConvert.SerializeObject(CustomerTypeOfFleetForms), Encoding.UTF8, "application/json");

                var responseTypeOfFleet = await _requestService.CommonRequestService(TypeOfFleetcontent, WebApiUrl.getTypeOfFleet);

                obj = JObject.Parse(JsonConvert.DeserializeObject(responseTypeOfFleet).ToString());
                jarr = obj["Data"].Value<JArray>();
                List<CustomerTypeOfFleetModel> lstTypeOfFleet = jarr.ToObject<List<CustomerTypeOfFleetModel>>();

                cust.CustomerTypeOfFleetMdl.AddRange(lstTypeOfFleet);

                //using (var TypeOfFleetResponse = await client.PostAsync(WebApiUrl.getTypeOfFleet, TypeOfFleetcontent))
                //{
                //    if (TypeOfFleetResponse.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = TypeOfFleetResponse.Content.ReadAsStringAsync().Result;

                //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                //        var jarr = obj["Data"].Value<JArray>();
                //        List<CustomerTypeOfFleetModel> lst = jarr.ToObject<List<CustomerTypeOfFleetModel>>();
                //        cust.CustomerTypeOfFleetMdl.AddRange(lst);
                //    }
                //    else
                //    {
                //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                //    }
                //}
                                
                cust.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());

               
                //District
                var permanentdistrictModel = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                                    {"StateID", cust.PerOrRegAddressStateID.ToString()}
                                };

                StringContent perDistrictModelContent = new StringContent(JsonConvert.SerializeObject(permanentdistrictModel), Encoding.UTF8, "application/json");

                var responseDistrictModel = await _requestService.CommonRequestService(perDistrictModelContent, WebApiUrl.getDistrict);

                obj = JObject.Parse(JsonConvert.DeserializeObject(responseDistrictModel).ToString());
                jarr = obj["Data"].Value<JArray>();
                List<OfficerDistrictModel> lstDistrict = jarr.ToObject<List<OfficerDistrictModel>>();

                cust.PerOrRegAddressDistrictMdl.AddRange(lstDistrict);

                //using (var perDistrictModelResponse = await client.PostAsync(WebApiUrl.getDistrict, perDistrictModelContent))
                //{
                //    if (perDistrictModelResponse.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = perDistrictModelResponse.Content.ReadAsStringAsync().Result;

                //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                //        var jarr = obj["Data"].Value<JArray>();
                //        List<OfficerDistrictModel> lst = jarr.ToObject<List<OfficerDistrictModel>>();

                //        cust.PerOrRegAddressDistrictMdl.AddRange(lst);
                //    }
                //    else
                //    {
                //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                //    }
                //}


            }

            return cust;
        }

        public async Task<List<CustomerTypeModel>> GetCustomerType()
        {

            var CustType = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };


            StringContent content = new StringContent(JsonConvert.SerializeObject(CustType), Encoding.UTF8, "application/json");

            var responseCustomerType = await _requestService.CommonRequestService(content, WebApiUrl.getOfficerType);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseCustomerType).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTypeModel> lstCustomerType = jarr.ToObject<List<CustomerTypeModel>>();

            return lstCustomerType;

            //using (var Response = await client.PostAsync(WebApiUrl.getOfficerType, content))
            //{
            //    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //        var jarr = obj["Data"].Value<JArray>();
            //        List<CustomerTypeModel> lst = jarr.ToObject<List<CustomerTypeModel>>();
            //        custMdl.CustomerTypeMdl.AddRange(lst);
            //        return Json(lst);
            //    }
            //    else
            //    {
            //        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //        // ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
            //    }
            //}

        }

        public async Task<List<CustomerSubTypeModel>> GetCustomerSubType(int CustomerTypeID)
        {
            var customerType = new Dictionary<string, string>
             {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    { "CustomerTypeId", CustomerTypeID.ToString() }
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(customerType), Encoding.UTF8, "application/json");


            var responseCustomerSubType = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerSubType);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseCustomerSubType).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerSubTypeModel> lstCustomerSubType = jarr.ToObject<List<CustomerSubTypeModel>>();
            lstCustomerSubType.Add(new CustomerSubTypeModel
            {
                CustomerSubTypeID = 0,
                CustomerSubTypeName = "Select Customer Sub Type"
            });
            var SortedtList = lstCustomerSubType.OrderBy(x => x.CustomerSubTypeID).ToList();

            return SortedtList;

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(customerType), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getCustomerSubType, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<CustomerSubTypeModel> lst = jarr.ToObject<List<CustomerSubTypeModel>>();
            //            lst.Add(new CustomerSubTypeModel
            //            {
            //                CustomerSubTypeID = 0,
            //                CustomerSubTypeName = "--Select --"
            //            });
            //            var SortedtList = lst.OrderBy(x => x.CustomerSubTypeID).ToList();
            //            return Json(SortedtList);
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading customer sub type Details");
            //            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //        }
            //    }
            //}


        }

        public async Task<List<OfficerDistrictModel>> GetDistrictDetails(string Stateid)
        {

            var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"StateID", Stateid.ToString()}
                };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDistrict);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<OfficerDistrictModel> lstOfficerDistrictModel = jarr.ToObject<List<OfficerDistrictModel>>();
            lstOfficerDistrictModel.Add(new OfficerDistrictModel
            {
                stateID = 0,
                districtID = 0,
                districtName = "Select District"
            });
            var SortedtList = lstOfficerDistrictModel.OrderBy(x => x.districtID).ToList();
            return SortedtList;

            //using (var Response = await client.PostAsync(WebApiUrl.getDistrict, content))
            //{
            //    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //        var jarr = obj["Data"].Value<JArray>();
            //        List<OfficerDistrictModel> lst = jarr.ToObject<List<OfficerDistrictModel>>();
            //        lst.Add(new OfficerDistrictModel
            //        {
            //            stateID = 0,
            //            districtID = 0,
            //            districtName = "Select District"
            //        });
            //        var SortedtList = lst.OrderBy(x => x.districtID).ToList();
            //        return Json(SortedtList);
            //    }
            //    else
            //    {
            //        ModelState.Clear();
            //        ModelState.AddModelError(string.Empty, "Error Loading District Details");
            //        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //    }
            //}
        }

        public async Task<List<VehicleTypeModel>> GetVehicleTypeDetails()
        {

            var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}

                };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getVehicleTpe);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<VehicleTypeModel> lstVehicleTypeModel = jarr.ToObject<List<VehicleTypeModel>>();

            var SortedtList = lstVehicleTypeModel.OrderBy(x => x.VehicleTypeId).ToList();
            return SortedtList;

            //using (var Response = await client.PostAsync(WebApiUrl.getVehicleTpe, content))
            //{
            //    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //        var jarr = obj["Data"].Value<JArray>();
            //        List<VehicleTypeModel> lst = jarr.ToObject<List<VehicleTypeModel>>();

            //        var SortedtList = lst.OrderBy(x => x.VehicleTypeId).ToList();
            //        return Json(new { SortedtList = SortedtList });
            //    }
            //    else
            //    {
            //        ModelState.Clear();
            //        ModelState.AddModelError(string.Empty, "Error Loading Vehicle Type Details");
            //        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //    }
            //}

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

                //using (var Response = await client.PostAsync(WebApiUrl.getCustomerByReferenceno, custRefcontent))
                //{
                //    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = Response.Content.ReadAsStringAsync().Result;


                //        customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(ResponseContent);
                //        if (customerResponseByReferenceNo.Internel_Status_Code == 1000)
                //        {
                //            customerCardInfo.CustomerReferenceNo = customerReferenceNo;
                //            customerCardInfo.FormNumber = customerResponseByReferenceNo.Data[0].FormNumber;

                //            StringBuilder sb = new StringBuilder();
                //            if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].FirstName.ToString()))
                //                sb.Append(customerResponseByReferenceNo.Data[0].FirstName.ToString());

                //            if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].MiddleName))
                //                sb.Append(" " + customerResponseByReferenceNo.Data[0].MiddleName);

                //            if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].LastName))
                //                sb.Append(" " + customerResponseByReferenceNo.Data[0].LastName);

                //            customerCardInfo.CustomerName = sb.ToString();

                //            customerCardInfo.CustomerTypeName = customerResponseByReferenceNo.Data[0].CustomerTypeName;
                //            customerCardInfo.CustomerTypeId = customerResponseByReferenceNo.Data[0].CustomerTypeId;

                //            if (customerResponseByReferenceNo.Data != null)
                //            {
                //                customerCardInfo.PaymentType = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentType) ? "" : customerResponseByReferenceNo.Data[0].PaymentType;
                //                customerCardInfo.PaymentReceivedDate = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentReceivedDate) ? "" : customerResponseByReferenceNo.Data[0].PaymentReceivedDate;
                //                customerCardInfo.NoOfCards = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].NoOfCards) ? "" : customerResponseByReferenceNo.Data[0].NoOfCards;
                //                customerCardInfo.ReceivedAmount = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].ReceivedAmount) ? "0" : customerResponseByReferenceNo.Data[0].ReceivedAmount;
                //                customerCardInfo.RBEId = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEId) ? "0" : customerResponseByReferenceNo.Data[0].RBEId;
                //                customerCardInfo.RBEName = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEName) ? "0" : customerResponseByReferenceNo.Data[0].RBEName;
                //                if (customerCardInfo.RBEId == "0")
                //                    customerCardInfo.RBEId = "";
                //                if (customerCardInfo.NoOfCards == "0")
                //                    customerCardInfo.NoOfCards = "";
                //                if (customerCardInfo.CustomerTypeId == "905" || customerCardInfo.CustomerTypeId == "909")
                //                {
                //                    customerCardInfo.NoOfVehiclesAllCards = customerCardInfo.NoOfCards;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            ViewBag.Message = customerResponseByReferenceNo.Message;
                //        }

                //    }
                //    else
                //    {
                //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                //    }
                //}

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

            var OfficerType = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerType), Encoding.UTF8, "application/json");

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

            var customerBody = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"FormNumber" , FormNumber}
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

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(customerRegion), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getSalesArea, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;
            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<SalesAreaModel> lst = jarr.ToObject<List<SalesAreaModel>>();
            //            lst.Add(new SalesAreaModel
            //            {
            //                SalesAreaID = 0,
            //                SalesAreaName = "Select Sales Area",

            //            });
            //            var SortedtList = lst.OrderBy(x => x.SalesAreaID).ToList();
            //            return Json(SortedtList);
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading sales Area Details");
            //            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //        }
            //    }
            //}

        }
        public async Task<CustomerInserCardResponseData> CheckformnumberDuplication(string FormNumber)
        {
            //fetching Customer info
            var CustomerFormNumber = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"FormNumber", FormNumber }
            };


            StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(CustomerFormNumber), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(cusFormcontent, WebApiUrl.checkformnumberDuplication);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            return lst[0];
        }
        public async Task<CustomerInserCardResponseData> CheckMobilNoDuplication(string MobileNo)
        {
            //fetching Customer info
            var CustomerFormNumber = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"CommunicationMobileNo", MobileNo }
            };


            StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(CustomerFormNumber), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(cusFormcontent, WebApiUrl.checkmobileNoDuplication);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            return lst[0];
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
    }
}