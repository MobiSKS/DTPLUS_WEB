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


            /* using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
              {
                  var CustomerTypeForms = new Dictionary<string, string>
                  {
                      {"Useragent", Common.useragent},
                      {"Userip", Common.userip},
                      {"UserId", Common.userid},
                      { "ObjCardDetail", "" },
                      {"CustomerType", cust.CustomerTypeID.ToString()},
                      {"CustomerSubtype", cust.CustomerSubTypeID.ToString()},
                      {"ZonalOffice", cust.CustomerZonalOfficeID.ToString()},
                      {"RegionalOffice", cust.CustomerRegionID.ToString()},
                      {"DateOfApplication", cust.CustomerDateOfApplication},
                      {"SalesArea", cust.SalesArea},
                      {"IndividualOrgNameTitle", cust.IndividualOrgNameTitle},
                      {"IndividualOrgName", cust.IndividualOrgName},
                      {"NameOnCard", cust.NoOfCards.ToString()},
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
                      {"CommunicationDistrictId", cust.CommunicationDistrict.ToString()},
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
                      {"PermanentDistrictId", cust.CommunicationDistrict},
                      {"PermanentPhoneNo", cust.PerOrRegAddressPhoneNumber},
                      {"PermanentFax", cust.PermanentFax},
                      {"KeyOfficialTitle", cust.KeyOffTitle},
                      {"KeyOfficialIndividualInitials", cust.KeyOffIndividualInitials},
                      {"KeyOfficialFirstName", cust.KeyOffFirstName},
                      {"KeyOfficialMiddleName", cust.KeyOffMiddleName},
                      {"KeyOfficialLastName", cust.KeyOffLastName},
                      {"KeyOfficialFax", cust.KeyOffFax.ToString()},
                      {"KeyOfficialDesignation", cust.KeyOffDesignation},
                      {"KeyOfficialEmail", cust.KeyOffEmail},
                      {"KeyOfficialPhoneNo", cust.KeyOffPhoneNumber},
                      {"KeyOfficialDOA", cust.KeyOffDateOfAnniversary},
                      {"KeyOfficialMobile", cust.KeyOffMobileNumber},
                      {"KeyOfficialDOB", cust.KeyOfficialDOB},
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
                      {"FeePaymentsChequeNo", cust.FeePaymentsChequeNo.ToString()},
                      {"FeePaymentsChequeDate", cust.FeePaymentsChequeDate},                   
                      {"Createdby", "0"}   



                  };



                  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                  StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeForms), Encoding.UTF8, "application/json");

                  //using (var Response = await client.PostAsync(WebApiUrl.insertCustomer, content))
                  //{
                  //    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                  //    {
                  //        var ResponseContent = Response.Content.ReadAsStringAsync().Result;                      

                  //        ViewBag.Message = "Customer details saved Successfully";
                  //    }
                  //    else
                  //    {
                  //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                  //    }
                  //}

              }
                  */
            return View(cust);
        }

      
        [HttpPost]
        public async Task<JsonResult> GetCustomerType()
        {            
            CustomerModel custMdl = new CustomerModel();
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var CustType= new Dictionary<string, string>
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
        public async Task<JsonResult> GetCustomerSubType( int CustomerTypeID)
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
                            CustomerSubTypeName  = "--Select --"
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

    }
}

