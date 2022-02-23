using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Service.Interfaces;

namespace HPCL_Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> OnlineForm()
        {
            // return View();

            char flag = 'N';

            CustomerModel custMdl = new CustomerModel();
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                //Fetching CustomerType
                var CustType = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")},
                    {"CTFlag",  "1" }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(CustType), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getCustomerType, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerTypeModel> lst = jarr.ToObject<List<CustomerTypeModel>>();
                        custMdl.CustomerTypeMdl.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

                //fetching Zonal Office
                var ZonalOfficeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")}

                };
                StringContent Zonecontent = new StringContent(JsonConvert.SerializeObject(ZonalOfficeForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getZonalOffice, Zonecontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerZonalOfficeModel> lst = jarr.ToObject<List<CustomerZonalOfficeModel>>();
                        custMdl.CustomerZonalOfficeMdl.AddRange(lst);

                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

                //sales Area Dropdown
                //var saleAreaReqData = new Dictionary<string, string>
                //{
                //    {"Useragent", Common.useragent},
                //    {"Userip", Common.userip},
                //    {"Userid", HttpContext.Session.GetString("UserName")},
                //    {"RegionID", "0" }
                //};


                //StringContent contentRegionData = new StringContent(JsonConvert.SerializeObject(saleAreaReqData), Encoding.UTF8, "application/json");

                //using (var Response = await client.PostAsync(WebApiUrl.getSalesArea, contentRegionData))
                //{
                //    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                //        var jarr = obj["Data"].Value<JArray>();
                //        List<SalesAreaModel> lst = jarr.ToObject<List<SalesAreaModel>>();
                //        custMdl.SalesAreaMdl.AddRange(lst);
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
                    {"Userid", HttpContext.Session.GetString("UserName")}

                };
                StringContent TBEntitycontent = new StringContent(JsonConvert.SerializeObject(TBEntityForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getTbentityName, TBEntitycontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerTbentityModel> lst = jarr.ToObject<List<CustomerTbentityModel>>();
                        custMdl.CustomerTbentityMdl.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

                //fetching State
                var CustomerStateForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")},
                    {"Country", "0"}
                };
                StringContent Statecontent = new StringContent(JsonConvert.SerializeObject(CustomerStateForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getState, Statecontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerStateModel> lst = jarr.ToObject<List<CustomerStateModel>>();
                        custMdl.CustomerStateMdl.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

                //Fetching Secret question
                var CustomerSecretQueForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")}

                };
                StringContent SecretQuecontent = new StringContent(JsonConvert.SerializeObject(CustomerSecretQueForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getSecretQuestion, SecretQuecontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerSecretQueModel> lst = jarr.ToObject<List<CustomerSecretQueModel>>();
                        custMdl.CustomerSecretQueMdl.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }
                //Fetching Type of Fleet
                var CustomerTypeOfFleetForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")}

                };
                StringContent TypeOfFleetcontent = new StringContent(JsonConvert.SerializeObject(CustomerTypeOfFleetForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getTypeOfFleet, TypeOfFleetcontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerTypeOfFleetModel> lst = jarr.ToObject<List<CustomerTypeOfFleetModel>>();
                        custMdl.CustomerTypeOfFleetMdl.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")}

                };

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                //StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getVehicleTpe, TypeOfFleetcontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<VehicleTypeModel> lst = jarr.ToObject<List<VehicleTypeModel>>();

                        //var SortedtList = lst.OrderBy(x => x.VehicleTypeId).ToList();
                        //custMdl.VehicleTypeMdl.AddRange(SortedtList);
                        custMdl.VehicleTypeMdl.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

                //CardDetails cardDetails = new CardDetails()
                //{
                //    CardIdentifier = 1,
                //    VechileNo = 2,
                //    VehicleMake = 3,
                //    VehicleType = 4,
                //    YearOfRegistration = 5
                //};
                //custMdl.CardDetailsMdl.Add(cardDetails);

                //CardDetails cardDetails1 = new CardDetails()
                //{
                //    CardIdentifier = 1,
                //    VechileNo = 2,
                //    VehicleMake = 3,
                //    VehicleType = 4,
                //    YearOfRegistration = 5
                //};
                //custMdl.CardDetailsMdl.Add(cardDetails1);


                if (flag == 'Y')
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Error Loading Customer Type");
                    ViewBag.Login = "1";
                    return View("Index");
                }
                else
                {
                    return View(custMdl);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnlineForm(CustomerModel cust)
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


            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var CustomerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", HttpContext.Session.GetString("UserName")},
                    {"CustomerType", cust.CustomerTypeID.ToString()},
                    {"CustomerSubtype", cust.CustomerSubTypeID.ToString()},
                    {"ZonalOffice", cust.CustomerZonalOfficeID.ToString()},
                    {"RegionalOffice", cust.CustomerRegionID.ToString()},
                    {"DateOfApplication", cust.CustomerDateOfApplication},
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
                    {"PermanentDistrictId", cust.PermanentDistrictId.ToString()},
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
                    {"KeyOfficialDOA", (string.IsNullOrEmpty(cust.KeyOffDateOfAnniversary)?"1900-01-01":cust.KeyOffDateOfAnniversary)},
                    {"KeyOfficialMobile", cust.KeyOffMobileNumber},
                    {"KeyOfficialDOB", (string.IsNullOrEmpty(cust.KeyOfficialDOB)?"1900-01-01":cust.KeyOfficialDOB)},
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
                    //{"FeePaymentsChequeNo", cust.FeePaymentsChequeNo.ToString()},
                    //{"FeePaymentsChequeDate", cust.FeePaymentsChequeDate},
                    {"Createdby", HttpContext.Session.GetString("UserName")},
                    //{ "ObjCardDetail", json }
                     {"TierOfCustomer", cust.TierOfCustomerID.ToString()},
                     {"TypeOfCustomer", cust.TypeOfCustomerID.ToString()}

                };
                //for (int i = 1; i <= cust.NoOfCards; i++)
                //{
                //    ObjCardDetails obj = new ObjCardDetails();
                //    obj.CardIdentifier = Request.Form["CardIdentifier[" + i + "]"];
                //    obj.VechileNo = Request.Form["CardIdentifier[" + i + "]"];
                //    obj.VehicleType = Convert.ToInt32(Request.Form["VehicleType[" + i + "]"]);
                //    obj.VehicleMake = Request.Form["VehicleMake[" + i + "]"];
                //    obj.YearOfRegistration = Convert.ToInt32(Request.Form["YearOfRegistration[" + i + "]"]);
                //    cust.ObjCardDetail.Add(obj);
                //}

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

                CustomerResponse customerResponse;

                using (var Response = await client.PostAsync(WebApiUrl.insertCustomer, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        using (HttpContent contentUrl = Response.Content)
                        {
                            var contentString = contentUrl.ReadAsStringAsync().Result;
                            customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(contentString);

                            if (customerResponse.Internel_Status_Code == 1000)
                            {
                                cust.Remarks = "";
                                ViewBag.Message = "Customer details saved Successfully";
                                return RedirectToAction("SuccessRedirect", new { customerReferenceNo = customerResponse.Data[0].CustomerReferenceNo });
                            }
                            else
                            {
                                cust.Remarks = customerResponse.Data[0].Reason;

                                /////////////
                                //Fetching CustomerType
                                var CustType = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")},
                                    {"CTFlag",  "1" }
                                };

                                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                                StringContent contentCustomerType = new StringContent(JsonConvert.SerializeObject(CustType), Encoding.UTF8, "application/json");

                                using (var CustomerTypeResponse = await client.PostAsync(WebApiUrl.getCustomerType, contentCustomerType))
                                {
                                    if (CustomerTypeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = CustomerTypeResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<CustomerTypeModel> lst = jarr.ToObject<List<CustomerTypeModel>>();
                                        cust.CustomerTypeMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }

                                //Customer SubType
                                var customerSubType = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")},
                                    { "CustomerTypeId", cust.CustomerTypeID.ToString() }
                                };

                                StringContent contentCustomerSubType = new StringContent(JsonConvert.SerializeObject(customerSubType), Encoding.UTF8, "application/json");

                                using (var CustomerSubTypeResponse = await client.PostAsync(WebApiUrl.getCustomerSubType, contentCustomerSubType))
                                {
                                    if (CustomerSubTypeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = CustomerSubTypeResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<CustomerSubTypeModel> lst = jarr.ToObject<List<CustomerSubTypeModel>>();
                                        lst.Add(new CustomerSubTypeModel
                                        {
                                            CustomerSubTypeID = 0,
                                            CustomerSubTypeName = "Select Customer Subtype"
                                        });
                                        var SortedtList = lst.OrderBy(x => x.CustomerSubTypeID).ToList();
                                        cust.CustomerSubTypeMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }

                                //fetching Zonal Office
                                var ZonalOfficeForms = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")}

                                };
                                StringContent Zonecontent = new StringContent(JsonConvert.SerializeObject(ZonalOfficeForms), Encoding.UTF8, "application/json");
                                using (var zonalOfficeResponse = await client.PostAsync(WebApiUrl.getZonalOffice, Zonecontent))
                                {
                                    if (zonalOfficeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = zonalOfficeResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<CustomerZonalOfficeModel> lst = jarr.ToObject<List<CustomerZonalOfficeModel>>();
                                        cust.CustomerZonalOfficeMdl.AddRange(lst);

                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }

                                //Regional Office
                                var customerRegionalOffice = new Dictionary<string, string>
                                    {
                                        {"Useragent", CommonBase.useragent},
                                        {"Userip", CommonBase.userip},
                                        {"Userid", HttpContext.Session.GetString("UserName")},
                                        {"ZonalId", cust.CustomerZonalOfficeID.ToString() }

                                };

                                StringContent customerRegionalOfficecontent = new StringContent(JsonConvert.SerializeObject(customerRegionalOffice), Encoding.UTF8, "application/json");
                                using (var customerRegionalOfficeResponse = await client.PostAsync(WebApiUrl.getRegionalOffice, customerRegionalOfficecontent))
                                {
                                    if (customerRegionalOfficeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = customerRegionalOfficeResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<CustomerRegionModel> lst = jarr.ToObject<List<CustomerRegionModel>>();

                                        //lst.Add(new CustomerRegionModel
                                        //{
                                        //    RegionalOfficeID = 0,
                                        //    RegionalOfficeName = "Select Region",

                                        //});
                                        //var SortedtList = lst.OrderBy(x => x.RegionalOfficeID).ToList();
                                        //cust.CustomerRegionMdl.AddRange(SortedtList);
                                        cust.CustomerRegionMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }

                                //sales Area Dropdown
                                var saleAreaReqData = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")},
                                    {"RegionID", cust.CustomerRegionID.ToString() }
                                };


                                StringContent contentRegionData = new StringContent(JsonConvert.SerializeObject(saleAreaReqData), Encoding.UTF8, "application/json");

                                using (var SalesAreaResponse = await client.PostAsync(WebApiUrl.getSalesArea, contentRegionData))
                                {
                                    if (SalesAreaResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = SalesAreaResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<SalesAreaModel> lst = jarr.ToObject<List<SalesAreaModel>>();
                                        cust.SalesAreaMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }


                                //fetching Type of Business Entity
                                var TBEntityForms = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")}

                                };
                                StringContent TBEntitycontent = new StringContent(JsonConvert.SerializeObject(TBEntityForms), Encoding.UTF8, "application/json");
                                using (var TbentityNameResponse = await client.PostAsync(WebApiUrl.getTbentityName, TBEntitycontent))
                                {
                                    if (TbentityNameResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = TbentityNameResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<CustomerTbentityModel> lst = jarr.ToObject<List<CustomerTbentityModel>>();
                                        cust.CustomerTbentityMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }

                                //fetching State
                                var CustomerStateForms = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")},
                                    {"Country", "0"}
                                };
                                StringContent Statecontent = new StringContent(JsonConvert.SerializeObject(CustomerStateForms), Encoding.UTF8, "application/json");
                                using (var StateResponse = await client.PostAsync(WebApiUrl.getState, Statecontent))
                                {
                                    if (StateResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = StateResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<CustomerStateModel> lst = jarr.ToObject<List<CustomerStateModel>>();
                                        cust.CustomerStateMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }

                                var districtModel = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")},
                                    {"StateID", cust.CommunicationStateID.ToString()}
                                };

                                StringContent districtModelContent = new StringContent(JsonConvert.SerializeObject(districtModel), Encoding.UTF8, "application/json");

                                using (var districtModelResponse = await client.PostAsync(WebApiUrl.getDistrict, districtModelContent))
                                {
                                    if (districtModelResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = districtModelResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<OfficerDistrictModel> lst = jarr.ToObject<List<OfficerDistrictModel>>();

                                        cust.CommunicationDistrictMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }


                                //Fetching Secret question
                                var CustomerSecretQueForms = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")}

                                };
                                StringContent SecretQuecontent = new StringContent(JsonConvert.SerializeObject(CustomerSecretQueForms), Encoding.UTF8, "application/json");
                                using (var SecretQuestionResponse = await client.PostAsync(WebApiUrl.getSecretQuestion, SecretQuecontent))
                                {
                                    if (SecretQuestionResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = SecretQuestionResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<CustomerSecretQueModel> lst = jarr.ToObject<List<CustomerSecretQueModel>>();
                                        cust.CustomerSecretQueMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }

                                //Fetching Type of Fleet
                                var CustomerTypeOfFleetForms = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")}

                                };
                                StringContent TypeOfFleetcontent = new StringContent(JsonConvert.SerializeObject(CustomerTypeOfFleetForms), Encoding.UTF8, "application/json");
                                using (var TypeOfFleetResponse = await client.PostAsync(WebApiUrl.getTypeOfFleet, TypeOfFleetcontent))
                                {
                                    if (TypeOfFleetResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = TypeOfFleetResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<CustomerTypeOfFleetModel> lst = jarr.ToObject<List<CustomerTypeOfFleetModel>>();
                                        cust.CustomerTypeOfFleetMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }

                                var forms = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")}

                                };

                                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                                //StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                                using (var VehicleTpeResponse = await client.PostAsync(WebApiUrl.getVehicleTpe, TypeOfFleetcontent))
                                {
                                    if (VehicleTpeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = VehicleTpeResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<VehicleTypeModel> lst = jarr.ToObject<List<VehicleTypeModel>>();

                                        //var SortedtList = lst.OrderBy(x => x.VehicleTypeId).ToList();
                                        //custMdl.VehicleTypeMdl.AddRange(SortedtList);
                                        cust.VehicleTypeMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }

                                //District
                                var permanentdistrictModel = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")},
                                    {"StateID", cust.PerOrRegAddressStateID.ToString()}
                                };

                                StringContent perDistrictModelContent = new StringContent(JsonConvert.SerializeObject(permanentdistrictModel), Encoding.UTF8, "application/json");

                                using (var perDistrictModelResponse = await client.PostAsync(WebApiUrl.getDistrict, perDistrictModelContent))
                                {
                                    if (perDistrictModelResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = perDistrictModelResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<OfficerDistrictModel> lst = jarr.ToObject<List<OfficerDistrictModel>>();

                                        cust.PerOrRegAddressDistrictMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }
                                //////////////


                                ViewBag.Message = customerResponse.Message;
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

            }

            return View(cust);
        }


        [HttpPost]
        public async Task<JsonResult> GetCustomerType()
        {
            CustomerModel custMdl = new CustomerModel();
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var CustType = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(CustType), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getOfficerType, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerTypeModel> lst = jarr.ToObject<List<CustomerTypeModel>>();
                        custMdl.CustomerTypeMdl.AddRange(lst);
                        return Json(lst);
                    }
                    else
                    {
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                        // ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

            }

        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerSubType(int CustomerTypeID)
        {
            CustomerModel custMdl = new CustomerModel();
            var customerType = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")},
                    { "CustomerTypeId", CustomerTypeID.ToString() }
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(customerType), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getCustomerSubType, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerSubTypeModel> lst = jarr.ToObject<List<CustomerSubTypeModel>>();
                        lst.Add(new CustomerSubTypeModel
                        {
                            CustomerSubTypeID = 0,
                            CustomerSubTypeName = "--Select --"
                        });
                        var SortedtList = lst.OrderBy(x => x.CustomerSubTypeID).ToList();
                        return Json(SortedtList);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading customer sub type Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetRegionalDetails(int ZonalOfficeID)
        {
            CustomerModel custMdl = new CustomerModel();
            var customerZone = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")},
                    {"ZonalId", ZonalOfficeID.ToString() }

            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(customerZone), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getRegionalOffice, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;
                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerRegionModel> lst = jarr.ToObject<List<CustomerRegionModel>>();
                        lst.Add(new CustomerRegionModel
                        {
                            RegionalOfficeID = 0,
                            RegionalOfficeName = "--Select Region--",

                        });
                        var SortedtList = lst.OrderBy(x => x.RegionalOfficeID).ToList();
                        return Json(SortedtList);

                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Region Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetDistrictDetails(string Stateid)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")},
                    {"StateID", Stateid.ToString()}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getDistrict, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerDistrictModel> lst = jarr.ToObject<List<OfficerDistrictModel>>();
                        lst.Add(new OfficerDistrictModel
                        {
                            stateID = 0,
                            districtID = 0,
                            districtName = "Select District"
                        });
                        var SortedtList = lst.OrderBy(x => x.districtID).ToList();
                        return Json(SortedtList);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading District Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetVehicleTypeDetails()
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")}

                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getVehicleTpe, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<VehicleTypeModel> lst = jarr.ToObject<List<VehicleTypeModel>>();

                        var SortedtList = lst.OrderBy(x => x.VehicleTypeId).ToList();
                        return Json(new { SortedtList = SortedtList });
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Vehicle Type Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> PANValidation(string PANNumber)
        {
            string apiUrl = "v2/pan";
            var data = "";
            var input = new
            {
                consent = "Y",
                pan = PANNumber
            };

            using (HttpClient client = new HelperAPI().GetApiPANUrlString())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, input);
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsStringAsync();
                }
            }

            return new JsonResult(data);

        }

        public async Task<IActionResult> SuccessRedirect(int customerReferenceNo)
        {
            ViewBag.CustomerReferenceNo = customerReferenceNo;
            return View();
        }

        public async Task<IActionResult> AddCardDetails(string customerReferenceNo)
        {
            // return View();

            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            char flag = 'N';


            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                //Fetching CustomerType
                var CustType = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")},
                    {"CTFlag",  "1" }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                //Fetching Type of Fleet
                var CustomerTypeOfFleetForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")}

                };
                StringContent TypeOfFleetcontent = new StringContent(JsonConvert.SerializeObject(CustomerTypeOfFleetForms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getVehicleTpe, TypeOfFleetcontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<VehicleTypeModel> lst = jarr.ToObject<List<VehicleTypeModel>>();

                        //var SortedtList = lst.OrderBy(x => x.VehicleTypeId).ToList();
                        //custMdl.VehicleTypeMdl.AddRange(SortedtList);
                        customerCardInfo.VehicleTypeMdl.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }


                if (!string.IsNullOrEmpty(customerReferenceNo))
                {

                    //fetching Customer info
                    var CustomerRefinfo = new Dictionary<string, string>
                    {
                        {"Useragent", CommonBase.useragent},
                        {"Userip", CommonBase.userip},
                        {"Userid", HttpContext.Session.GetString("UserName")},
                        {"CustomerReferenceNo", customerReferenceNo}
                    };

                    CustomerResponseByReferenceNo customerResponseByReferenceNo;
                    StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");
                    using (var Response = await client.PostAsync(WebApiUrl.getCustomerByReferenceno, custRefcontent))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;


                            customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(ResponseContent);
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
                            }
                            else
                            {
                                ViewBag.Message = customerResponseByReferenceNo.Message;
                            }

                        }
                        else
                        {
                            ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                        }
                    }
                }

                if (flag == 'Y')
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Error Loading Customer Type");
                    ViewBag.Login = "1";
                    return View("Index");
                }
                else
                {
                    return View(customerCardInfo);
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerDetails(string customerReferenceNo)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")},
                    {"ZonalID", "0" }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");



                //fetching Customer info
                var CustomerRefinfo = new Dictionary<string, string>
                    {
                        {"Useragent", CommonBase.useragent},
                        {"Userip", CommonBase.userip},
                        {"Userid", HttpContext.Session.GetString("UserName")},
                        {"CustomerReferenceNo", customerReferenceNo}
                    };

                CustomerCardInfo customerCardInfo = new CustomerCardInfo();

                CustomerResponseByReferenceNo customerResponseByReferenceNo;
                StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getCustomerByReferenceno, custRefcontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(ResponseContent);
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

                            return Json(customerCardInfo);
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
                            var Response_Content = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(Response_Content).ToString());
                            var Message = "Failed to load Customer Details";
                            return Json("Failed to load Customer Details");
                        }

                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
                        var Response_Content = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(Response_Content).ToString());
                        var Message = obj["errorMessage"].ToString();
                        return Json("Failed to load Customer Details");
                    }
                }


            }
        }


        [HttpPost]
        public async Task<JsonResult> GetCustomerRBEName(string RBEId)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")},
                    {"ZonalID", "0" }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");



                //fetching Customer info
                var CustomerRefinfo = new Dictionary<string, string>
                    {
                        {"Useragent", CommonBase.useragent},
                        {"Userip", CommonBase.userip},
                        {"Userid", HttpContext.Session.GetString("UserName")},
                        {"RBEId", RBEId}
                    };

                CustomerCardInfo customerCardInfo = new CustomerCardInfo();

                CustomerRBE customerRBE;
                StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getCustomerRbeId, custRefcontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        customerRBE = JsonConvert.DeserializeObject<CustomerRBE>(ResponseContent);
                        if (customerRBE.Internel_Status_Code == 1000)
                        {
                            customerCardInfo.RBEName = customerRBE.Data[0].RBEName;
                            customerCardInfo.RBEId = customerRBE.Data[0].RBEId.ToString();

                            return Json(customerCardInfo);
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
                            var Response_Content = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(Response_Content).ToString());
                            var Message = "Failed to load Customer Details";
                            return Json("Failed to load Customer Details");
                        }

                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
                        var Response_Content = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(Response_Content).ToString());
                        var Message = obj["errorMessage"].ToString();
                        return Json("Failed to load Customer Details");
                    }
                }


            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCardDetails(CustomerCardInfo customerCardInfo)
        {
            //var json = "";
            //if (customerCardInfo.ObjCardDetail.Count > 0)
            //{
            //    int i = 0;
            //    foreach (CardDetails CardDetails in customerCardInfo.ObjCardDetail)
            //    {
            //        CardDetails.VechileNo = "";
            //    }
            //    //json = JsonConvert.SerializeObject(customerCardInfo.ObjCardDetail);
            //}

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                //var CustomerCardData = new Dictionary<string, string>
                //{
                //    {"Useragent", Common.useragent},
                //    {"Userip", Common.userip},
                //    {"UserId", Common.userid},
                //    {"CustomerReferenceNo", customerCardInfo.CustomerReferenceNo},
                //    {"NoOfCards", customerCardInfo.NoOfCards.ToString()},
                //    {"RBEId", customerCardInfo.RBEId.ToString()},
                //    {"FeePaymentsCollectFeeWaiver", customerCardInfo.FeePaymentsCollectFeeWaiver.ToString()},
                //    {"FeePaymentNo", customerCardInfo.FeePaymentNo},
                //    {"FeePaymentDate", customerCardInfo.FeePaymentDate},
                //    {"CardPreference", customerCardInfo.CardPreference},
                //    {"Createdby", Common.userid.ToString()},
                //    { "ObjCardDetail", json }
                //};

                #region Create Request Info

                CustomerCardInsertInfo insertInfo = new CustomerCardInsertInfo();

                insertInfo.CustomerReferenceNo = customerCardInfo.CustomerReferenceNo;
                insertInfo.CustomerName = customerCardInfo.CustomerName;
                insertInfo.FormNumber = customerCardInfo.FormNumber;
                insertInfo.NoOfCards = customerCardInfo.NoOfCards;
                insertInfo.RBEId = customerCardInfo.RBEId;
                insertInfo.FeePaymentsCollectFeeWaiver = customerCardInfo.FeePaymentsCollectFeeWaiver;
                insertInfo.FeePaymentNo = customerCardInfo.FeePaymentNo;
                insertInfo.FeePaymentDate = customerCardInfo.FeePaymentDate;
                insertInfo.CardPreference = customerCardInfo.CardPreference;
                insertInfo.RBEName = customerCardInfo.RBEName;
                insertInfo.RBEName = customerCardInfo.RBEName;
                insertInfo.Useragent = CommonBase.useragent;
                insertInfo.Userip = CommonBase.userip;
                insertInfo.UserId = HttpContext.Session.GetString("UserName");
                insertInfo.Createdby = HttpContext.Session.GetString("UserName");
                insertInfo.ObjCardDetail = customerCardInfo.ObjCardDetail;

                #endregion


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                //string jsonString = JsonConvert.SerializeObject(CustomerCardData);





                #region Commented

                //// jsonString = jsonString.Replace("\\", "");

                ////StringContent content = new StringContent(CustomerCardData, Encoding.UTF8, "application/json");

                StringContent content = new StringContent(JsonConvert.SerializeObject(insertInfo), Encoding.UTF8, "application/json");

                #endregion

                CustomerInserCardResponse customerInserCardResponse;


                using (var Response = await client.PostAsync(WebApiUrl.insertCustomerCard, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        using (HttpContent contentUrl = Response.Content)
                        {
                            var contentString = contentUrl.ReadAsStringAsync().Result;
                            customerInserCardResponse = JsonConvert.DeserializeObject<CustomerInserCardResponse>(contentString);
                            if (customerInserCardResponse.Internel_Status_Code == 1000)
                            {
                                ViewBag.Message = "Customer card details saved Successfully";
                                //return RedirectToAction("SuccessRedirect", new { customerReferenceNo = customerResponse.Data[0].CustomerReferenceNo });
                                customerCardInfo.Status = customerInserCardResponse.Data[0].Status;
                                customerCardInfo.StatusCode = customerInserCardResponse.Internel_Status_Code;
                            }
                            else
                            {
                                ViewBag.Message = customerInserCardResponse.Message;
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

            }

            return View(customerCardInfo);
        }


        public async Task<IActionResult> UploadDoc(string CustomerReferenceNo)
        {
            var modals = await _customerService.UploadDoc(CustomerReferenceNo);
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> UploadDoc(UploadDoc entity)
        {
            var searchCustomer = _customerService.UploadDoc(entity);

            ModelState.Clear();
            return Json(new { searchCustomer = searchCustomer });
        }

        [HttpPost]
        public async Task<JsonResult> SaveUploadDoc(UploadDoc entity)
        {
            var reason = await _customerService.SaveUploadDoc(entity);

            ModelState.Clear();
            return Json(reason);
        }

        public async Task<IActionResult> ValidateNewCustomer()
        {
            CustomerModel customerModel = new CustomerModel();

            var OfficerType = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")}
                };


            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerType), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getOfficerType, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();

                        List<OfficerTypeModel> lst = jarr.ToObject<List<OfficerTypeModel>>();

                        customerModel.OfficerTypeMdl.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

            }

            return View(customerModel);
        }

        [HttpPost]
        public async Task<JsonResult> ValidateNewCustomer(CustomerModel entity)
        {
            var searchBody = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", HttpContext.Session.GetString("UserName")},
                {"Createdby" , entity.OfficerTypeID>0? entity.OfficerTypeID.ToString(): null },
                {"Createdon" , entity.CustomerDateOfApplication},
                {"FormNumber" , entity.FormNumber},
                {"StateId" , null},
                {"CustomerName" , null}
            };

            HttpContext.Session.SetString("viewUpdatedCustGrid", JsonConvert.SerializeObject(searchBody));

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getCustomerPendingForApproval, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        var jsonSerializerOptions = new JsonSerializerOptions()
                        {
                            IgnoreNullValues = true
                        };
                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<SearchCustomerResponseGrid> searchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();
                        ModelState.Clear();
                        return Json(new { searchList = searchList });
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> ReloadUpdatedGrid()
        {
            var str = HttpContext.Session.GetString("viewUpdatedCustGrid");

            CustomerModel vGrid = JsonConvert.DeserializeObject<CustomerModel>(str);

            var searchBody = new Dictionary<string, string>
            {
               {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", HttpContext.Session.GetString("UserName")},
                {"OfficerTypeID", vGrid.OfficerTypeID > 0 ? vGrid.OfficerTypeID.ToString() : null},
                {"CustomerDateOfApplication", vGrid.CustomerDateOfApplication},
                {"FormNumber", vGrid.FormNumber},
                {"StateId" , null},
                {"CustomerName" , null}
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getCustomerPendingForApproval, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        var jsonSerializerOptions = new JsonSerializerOptions()
                        {
                            IgnoreNullValues = true
                        };
                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<SearchCustomerResponseGrid> updatedSearchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();
                        ModelState.Clear();
                        return Json(new { updatedSearchList = updatedSearchList });
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> ViewCustomerDetails(string FormNumber)
        {
            HttpContext.Session.SetString("FormNumberSession", FormNumber);

            var customerBody = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", HttpContext.Session.GetString("UserName")},
                {"FormNumber" , FormNumber}
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(customerBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getcustomerdetailsByFormNumber, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

                        var searchRes = obj["Data"].Value<JObject>();
                        var cardResult = searchRes["GetCustomerDetails"].Value<JArray>();

                        var customerKYCDetailsResult = searchRes["CustomerKYCDetails"].Value<JArray>();
                        //var ccmsRemainingResult = searchRes["CardReminingCCMSLimt"].Value<JArray>();

                        List<CustomerFullDetails> customerList = cardResult.ToObject<List<CustomerFullDetails>>();

                        List<UploadDocResponseBody> UploadDocList = customerKYCDetailsResult.ToObject<List<UploadDocResponseBody>>();

                        //List<LimitResponse> limitDetailsList = limitResult.ToObject<List<LimitResponse>>();
                        //List<ServicesResponse> servicesDetailsList = serviceResult.ToObject<List<ServicesResponse>>();

                        //List<CardReminingLimt> cardRemaining = cardRemainingResult.ToObject<List<CardReminingLimt>>();
                        //List<CardReminingCCMSLimt> ccmsRemaining = ccmsRemainingResult.ToObject<List<CardReminingCCMSLimt>>();

                        CustomerFullDetails Customer = customerList.Where(t => t.FormNumber == FormNumber).FirstOrDefault();

                        FormNumber = string.Empty;

                        //foreach (var item in cardDetailsList)
                        //{
                        //    cusId = item.CustomerID;
                        //}

                        HttpContext.Session.SetString("FormNumberSession", FormNumber);

                        ModelState.Clear();
                        //return Json(new
                        //{
                        //    cardDetailsList = cardDetailsList
                        //    //limitDetailsList = limitDetailsList,
                        //    //servicesDetailsList = servicesDetailsList,
                        //    //cardRemaining = cardRemaining,
                        //    //ccmsRemaining = ccmsRemaining
                        //});
                        return Json(new { customer = Customer, kycDetailsResult = UploadDocList });
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Location Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> AproveCustomer(string CustomerReferenceNo, string Comments, string Approvalstatus)
        {
            var approvalBody = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", HttpContext.Session.GetString("UserName")},
                {"CustomerReferenceNo" , CustomerReferenceNo},
                {"Comments" , Comments},
                {"Approvalstatus" , Approvalstatus},
                {"ApprovedBy" , HttpContext.Session.GetString("UserName")}
            };

            //HttpContext.Session.SetString("viewUpdatedCustGrid", JsonConvert.SerializeObject(searchBody));

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(approvalBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.approveOrrejectcustomer, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        //var jsonSerializerOptions = new JsonSerializerOptions()
                        //{
                        //    IgnoreNullValues = true
                        //};
                        //JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var jarr = obj["Data"].Value<JArray>();
                        //List<SearchCustomerResponseGrid> searchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();
                        //ModelState.Clear();
                        //return Json(new { searchList = searchList });

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<UpdateKycResponse> insertKyc = jarr.ToObject<List<UpdateKycResponse>>();
                        ModelState.Clear();


                        return Json(insertKyc[0].Reason);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetsalesAreaDetails(int RegionID)
        {
            CustomerModel custMdl = new CustomerModel();
            var customerRegion = new Dictionary<string, string>
             {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")},
                    {"RegionID", RegionID.ToString() }

            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(customerRegion), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getSalesArea, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;
                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<SalesAreaModel> lst = jarr.ToObject<List<SalesAreaModel>>();
                        lst.Add(new SalesAreaModel
                        {
                            SalesAreaID = 0,
                            SalesAreaName = "Select Sales Area",

                        });
                        var SortedtList = lst.OrderBy(x => x.SalesAreaID).ToList();
                        return Json(SortedtList);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading sales Area Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        public async Task<IActionResult> SuccessUploadRedirect(int customerReferenceNo)
        {
            ViewBag.CustomerReferenceNo = customerReferenceNo;
            return View();
        }

    }
}