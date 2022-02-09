using HPCL_Web.Helper;
using HPCL_Web.Models.Merchant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class MerchantController : Controller
    {
        public IActionResult ViewPorfile()
        {
            return View();
        }
        public async Task<IActionResult> CreateMerchant()
        {
            char flag = 'N';
            MerchantModel merchantModel = new MerchantModel();
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var MerchantTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent MerchantTypeContent = new StringContent(JsonConvert.SerializeObject(MerchantTypeForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getMerchantType, MerchantTypeContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<MerchantTypeModel> lst = jarr.ToObject<List<MerchantTypeModel>>();
                        merchantModel.MerchantTypes.AddRange(lst);
                    }
                    else
                    {
                        flag = 'Y';
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load Merchant types";
                    }
                }

                var OutletCategoryForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                };

                StringContent OutletCategoryContent = new StringContent(JsonConvert.SerializeObject(OutletCategoryForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getOutletCategory, OutletCategoryContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OutletCategoryModel> lst = jarr.ToObject<List<OutletCategoryModel>>();
                        merchantModel.OutletCategories.AddRange(lst);
                    }
                    else
                    {
                        flag = 'Y';
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to Outlet Categories";
                    }
                }

                var SBUForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                };

                StringContent SBUContent = new StringContent(JsonConvert.SerializeObject(SBUForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getSbu, SBUContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<SBUTypeModel> lst = jarr.ToObject<List<SBUTypeModel>>();
                        merchantModel.SBUTypes.AddRange(lst);
                    }
                    else
                    {
                        flag = 'Y';
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to SBUs";
                    }
                }

                var MerchantStateForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent MerchantStateContent = new StringContent(JsonConvert.SerializeObject(MerchantStateForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getState, MerchantStateContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<RetailOutletStateModel> lst = jarr.ToObject<List<RetailOutletStateModel>>();
                        List<CommStateModel> lst1 = jarr.ToObject<List<CommStateModel>>();
                        merchantModel.RetailOutletStates.AddRange(lst);
                        merchantModel.CommStates.AddRange(lst1);
                    }
                    else
                    {
                        flag = 'Y';
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load Merchant States";
                    }
                }

                var MerchantZoneForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent MerchantZoneContent = new StringContent(JsonConvert.SerializeObject(MerchantZoneForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.zonalOffice, MerchantZoneContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<ZonalOfficeModel> lst = jarr.ToObject<List<ZonalOfficeModel>>();
                        merchantModel.ZonalOffices.AddRange(lst);
                    }
                    else
                    {
                        flag = 'Y';
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load Merchant Zones";
                    }
                }

                if (flag == 'Y')
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Error Loading Merchant Data");
                    ViewBag.Login = "1";
                    return View("Index");
                }
                else
                {
                    return View(merchantModel);
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateMerchant(MerchantModel merchantMdl)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var MerchantCreateForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"ErpCode", merchantMdl.ERPCode},
                    {"RetailOutletName", merchantMdl.RetailOutletName},
                    {"MerchantTypeId", merchantMdl.MerchantType},
                    {"DealerName", merchantMdl.DealerName},
                    {"MappedMerchantId", merchantMdl.MappedMerchantID},
                    {"DealerMobileNo", merchantMdl.DealerMobileNumber},
                    {"OutletCategoryId", merchantMdl.OutletCategory},
                    {"HighwayNo1", merchantMdl.PreHighwayNumber},
                    {"HighwayNo2", merchantMdl.HighwayNumber},
                    {"HighwayName", merchantMdl.HighwayName},
                    {"SBUTypeId", merchantMdl.SBUType},
                    {"LPGCNGSale", merchantMdl.LPG_CNGSale},
                    {"PancardNumber", merchantMdl.PANCardNumber},
                    {"GSTNumber", merchantMdl.GSTNumber},
                    {"RetailOutletAddress1", merchantMdl.Retail_Outlet_Address1},
                    {"RetailOutletAddress2", merchantMdl.Retail_Outlet_Address2},
                    {"RetailOutletAddress3", merchantMdl.Retail_Outlet_Address3},
                    {"RetailOutletLocation", merchantMdl.Retail_Outlet_Location},
                    {"RetailOutletCity", merchantMdl.Retail_Outlet_City},
                    {"RetailOutletStateId", merchantMdl.Retail_Outlet_State},
                    {"RetailOutletDistrictId", merchantMdl.Retail_DistictID},
                    {"RetailOutletPinNumber", merchantMdl.Retail_Outlet_Pin},
                    {"RetailOutletPhoneNumber", merchantMdl.Retail_Outlet_PhoneOfficeCode + " - " + merchantMdl.Retail_Outlet_PhoneOffice},
                    {"RetailOutletFax", merchantMdl.Retail_Outlet_FaxCode + " - " + merchantMdl.Retail_Outlet_Fax},
                    {"ZonalOfficeId", merchantMdl.ZonalOffice},
                    {"RegionalOfficeId", merchantMdl.RegionalOfcID},
                    {"SalesAreaId", merchantMdl.SalesAreaID},
                    {"ContactPersonNameFirstName", merchantMdl.FName},
                    {"ContactPersonNameMiddleName", merchantMdl.MName},
                    {"ContactPersonNameLastName", merchantMdl.LName},
                    {"MobileNo", merchantMdl.Mobile},
                    {"EmailId", merchantMdl.Email},
                    {"Mics", merchantMdl.Misc},
                    {"CommunicationAddress1", merchantMdl.Comm_Address1},
                    {"CommunicationAddress2", merchantMdl.Comm_Address2},
                    {"CommunicationAddress3", merchantMdl.Comm_Address3},
                    {"CommunicationLocation", ""},
                    {"CommunicationCity", merchantMdl.Comm_City},
                    {"CommunicationStateId", merchantMdl.Comm_State},
                    {"CommunicationDistrictId", merchantMdl.Comm_DistictID == null ? "0" : merchantMdl.Comm_DistictID},
                    {"CommunicationPinNumber", merchantMdl.Comm_Pin},
                    {"CommunicationPhoneNumber", merchantMdl.Comm_Pre_PhoneNumber + " - " + merchantMdl.Comm_PhoneNumber},
                    {"CommunicationFax", merchantMdl.Comm_Pre_Fax + " - " + merchantMdl.Comm_Fax},
                    {"NoofLiveTerminals", merchantMdl.NumOfLiveTerminals.ToString() },
                    {"TerminalTypeRequested", merchantMdl.TerminalTypeRequested },
                    {"CreatedBy", "0"}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent MerchantCreateContent = new StringContent(JsonConvert.SerializeObject(MerchantCreateForms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.insertMerchant, MerchantCreateContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Message"].ToString();

                        ViewBag.Message = jarr;
                        return View(merchantMdl);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to create Merchant";
                    }
                }
            }
            return RedirectToAction("CreateMerchant");
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
                    {"StateID", Stateid.ToString() }
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
                        List<RetailOutletDistrictModel> lst = jarr.ToObject<List<RetailOutletDistrictModel>>();
                        lst.Add(new RetailOutletDistrictModel
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
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        return Json("Failed to load District");
                    }
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetRegionalOfcDetails(string ZonalOfcID)
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

                using (var Response = await client.PostAsync(WebApiUrl.regionalOffice, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<RegionalOfficeModel> lst = jarr.ToObject<List<RegionalOfficeModel>>();
                        lst.Add(new RegionalOfficeModel
                        {
                            RegionalOfficeID = 0,
                            RegionalOfficeName = "--Select--"
                        });
                        var SortedtList = lst.OrderBy(x => x.RegionalOfficeID).ToList();
                        return Json(SortedtList);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Regional Office Details");
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        return Json("Failed to load Regional Office Details");
                    }
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetSalesAreaDetails(string RegionalID)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"RegionID", "0" }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

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
                            RegionID = 0,
                            SalesAreaID = 0,
                            SalesAreaName = "--Select--"
                        });
                        var SortedtList = lst.OrderBy(x => x.SalesAreaID).ToList();
                        return Json(SortedtList);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading District Details");
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        return Json("Failed to load District");
                    }
                }
            }
        }
    }
}
