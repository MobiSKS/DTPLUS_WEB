using HPCL_Web.Helper;
using HPCL_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using HPCL_Web.Models.Officer;


namespace HPCL_Web.Controllers
{
    public class CustomerController : Controller
    {
        HelperAPI _api = new HelperAPI();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> OnlineForm()
        {
            // return View();

            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token", access_token.Result);
            }



            char flag = 'N';

            CustomerModel custMdl = new CustomerModel();
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                //Fetching CustomerType
                var CustType = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}

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
                var saleAreaReqData = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"RegionID", "0" }
                };


                StringContent contentRegionData = new StringContent(JsonConvert.SerializeObject(saleAreaReqData), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getSalesArea, contentRegionData))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<SalesAreaModel> lst = jarr.ToObject<List<SalesAreaModel>>();
                        custMdl.SalesAreaMdl.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }
                

                //fetching Type of Business Entity
                var TBEntityForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}

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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}

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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}

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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}

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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"UserId", Common.userid},
                    {"CustomerType", cust.CustomerTypeID.ToString()},
                    {"CustomerSubtype", cust.CustomerSubTypeID.ToString()},
                    {"ZonalOffice", cust.CustomerZonalOfficeID.ToString()},
                    {"RegionalOffice", cust.CustomerRegionID.ToString()},
                    {"DateOfApplication", cust.CustomerDateOfApplication},
                    {"SalesArea", cust.CustomerSalesAreaID.ToString()},
                    {"IndividualOrgNameTitle", cust.IndividualOrgNameTitle},
                    {"IndividualOrgName", cust.IndividualOrgName},
                    {"NameOnCard", cust.CustomerNameOnCard.ToString()},
                    {"TypeOfBusinessEntity", cust.CustomerTbentityID.ToString()},
                    {"ResidenceStatus", cust.CustomerResidenceStatus},
                    {"IncomeTaxPan", cust.CustomerIncomeTaxPan},
                    {"CommunicationAddress1", cust.CommunicationAddress1},
                    {"CommunicationAddress2", cust.CommunicationAddress2},
                    {"CommunicationAddress3", cust.CommunicationAddress3},
                    {"CommunicationLocation", cust.CommunicationLocation},
                    {"CommunicationCityName", cust.CommunicationCity},
                    {"CommunicationPincode", cust.CommunicationPinCode},
                    {"CommunicationStateId", cust.CommunicationStateID.ToString()},
                    {"CommunicationDistrictId", cust.CommunicationDistrictId.ToString()},
                    {"CommunicationPhoneNo", cust.CommunicationPhoneNo},
                    {"CommunicationFax", cust.CommunicationFax},
                    {"CommunicationMobileNo", cust.CommunicationMobileNumber},
                    {"CommunicationEmailid", cust.CommunicationEmail},
                    {"PermanentAddress1", cust.PerOrRegAddress1},
                    {"PermanentAddress2", cust.PerOrRegAddress2},
                    {"PermanentAddress3", cust.PerOrRegAddress3},
                    {"PermanentLocation", cust.PerOrRegAddressLocation},
                    {"PermanentCityName", cust.PerOrRegAddressCity},
                    {"PermanentPincode", cust.PerOrRegAddressPinCode},
                    {"PermanentStateId", cust.PerOrRegAddressStateID.ToString()},
                    {"PermanentDistrictId", cust.PermanentDistrictId.ToString()},
                    {"PermanentPhoneNo", cust.PerOrRegAddressPhoneNumber},
                    {"PermanentFax", cust.PermanentFax},
                    {"KeyOfficialTitle", cust.KeyOffTitle},
                    {"KeyOfficialIndividualInitials", cust.KeyOffIndividualInitials},
                    {"KeyOfficialFirstName", cust.KeyOffFirstName},
                    {"KeyOfficialMiddleName", cust.KeyOffMiddleName},
                    {"KeyOfficialLastName", cust.KeyOffLastName},
                    {"KeyOfficialFax", cust.KeyOffFax == null?"":cust.KeyOffFax.ToString()},
                    {"KeyOfficialDesignation", cust.KeyOffDesignation},
                    {"KeyOfficialEmail", cust.KeyOffEmail},
                    //{"KeyOfficialPhoneNo", cust.KeyOffPhoneNumber},
                    {"KeyOfficialPhoneNo", cust.CommunicationPhoneNo},
                    {"KeyOfficialDOA", (string.IsNullOrEmpty(cust.KeyOffDateOfAnniversary)?"1900-01-01":cust.KeyOffDateOfAnniversary)},
                    {"KeyOfficialMobile", cust.KeyOffMobileNumber},
                    {"KeyOfficialDOB", (string.IsNullOrEmpty(cust.KeyOfficialDOB)?"1900-01-01":cust.KeyOfficialDOB)},
                    {"KeyOfficialSecretQuestion", cust.KeyOfficialSecretQuestion},
                    {"KeyOfficialSecretAnswer", cust.KeyOfficialSecretAnswer},
                    {"KeyOfficialTypeOfFleet", cust.CustomerTypeOfFleetID},
                    {"KeyOfficialCardAppliedFor", cust.KeyOfficialCardAppliedFor},
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
                    {"Createdby", Common.userid.ToString()},
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
                                ViewBag.Message = "Customer details saved Successfully";
                                return RedirectToAction("SuccessRedirect", new { customerReferenceNo = customerResponse.Data[0].CustomerReferenceNo });
                            }
                            else
                            {
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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}
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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}

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

            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token", access_token.Result);
            }

            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            char flag = 'N';


            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                //Fetching CustomerType
                var CustType = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"CTFlag",  "1" }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                //Fetching Type of Fleet
                var CustomerTypeOfFleetForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}

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
                        {"Useragent", Common.useragent},
                        {"Userip", Common.userip},
                        {"Userid", Common.userid},
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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"ZonalID", "0" }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");



                //fetching Customer info
                var CustomerRefinfo = new Dictionary<string, string>
                    {
                        {"Useragent", Common.useragent},
                        {"Userip", Common.userip},
                        {"Userid", Common.userid},
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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"ZonalID", "0" }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");



                //fetching Customer info
                var CustomerRefinfo = new Dictionary<string, string>
                    {
                        {"Useragent", Common.useragent},
                        {"Userip", Common.userip},
                        {"Userid", Common.userid},
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
            if (customerCardInfo.ObjCardDetail.Count > 0)
            {
                int i = 0;
                foreach (CardDetails CardDetails in customerCardInfo.ObjCardDetail)
                {
                    CardDetails.VechileNo = "";
                }
                //json = JsonConvert.SerializeObject(customerCardInfo.ObjCardDetail);
            }

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
                insertInfo.Useragent = Common.useragent;
                insertInfo.Userip = Common.userip;
                insertInfo.UserId = Common.userid;
                insertInfo.Createdby = Common.userid;
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

    }
}