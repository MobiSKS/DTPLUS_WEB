using HPCL_Web.Helper;
using HPCL_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class OfficerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            char flag = 'N';
            OfficerModel ofcrMdl = new OfficerModel();
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerTypeForms), Encoding.UTF8, "application/json");
                
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.getOfficerType, content))    
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        var jobject = (JObject)JsonConvert.DeserializeObject(ResponseContent);
                        var jvalue = (JArray)jobject["Data"];

                        foreach(var item in jvalue)
                        {
                            OfficerTypeModel ofcrTypMdl = new OfficerTypeModel();
                            ofcrTypMdl.OfficerID = Convert.ToInt32((JValue)item["OfficerTypeID"]);
                            ofcrTypMdl.OfficerTypeName = (string)(JValue)item["OfficerTypeName"];
                            ofcrTypMdl.OfficerTypeShortName = (string)(JValue)item["OfficerTypeShortName"];
                            ofcrMdl.OfficerTypeMdl.Add(ofcrTypMdl);
                        }
                    }
                }

                var OfficerStateForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"Country", "0"}
                };
                StringContent Statecontent = new StringContent(JsonConvert.SerializeObject(OfficerStateForms), Encoding.UTF8, "application/json");

                //Fetching State
                using (var Response = await client.PostAsync(WebApiUrl.getState, Statecontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        var jobject = (JObject)JsonConvert.DeserializeObject(ResponseContent);
                        var jvalue = (JArray)jobject["Data"];

                        foreach (var item in jvalue)
                        {
                            OfficerStateModel ofcrStateMdl = new OfficerStateModel();
                            ofcrStateMdl.CountryID = Convert.ToInt32((JValue)item["CountryID"]);
                            ofcrStateMdl.StateID = Convert.ToInt32((JValue)item["StateID"]);
                            ofcrStateMdl.StateName = (string)(JValue)item["StateName"];
                            ofcrMdl.OfficerStateMdl.Add(ofcrStateMdl);
                        }                        
                    }
                }

                if (flag == 'Y')
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Error Loading Officer Type");
                    ViewBag.Login = "1";
                    return View("Index");
                }
                else
                {
                    return View(ofcrMdl);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfficerModel ofcrMdl)
        {

            return View();
        }
        public async Task<IActionResult> EditOfficer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditOfficer(OfficerModel ofcrMdl)
        {
            return View();
        }

        public async Task<IActionResult> EditLocation()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> EditLocation(OfficerLocatModel OfcrLocation)
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<JsonResult> GetLocationDetails(string OfcrType)
        {
            string getLocation = "";
            if (OfcrType.Contains("1") || OfcrType.Contains("4") || OfcrType.Contains("6"))
            {
                getLocation = WebApiUrl.getLocationRegion;
            }
            else if (OfcrType.Contains("3") || OfcrType.Contains("5"))
            {
                getLocation = WebApiUrl.getLocationZone;
            }
            else if (OfcrType.Contains("2"))
            {
                getLocation = WebApiUrl.getLocationHq;
            }
            
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

                using (var Response = await client.PostAsync(getLocation, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        var jobject = (JObject)JsonConvert.DeserializeObject(ResponseContent);
                        var jvalue = (JArray)jobject["Data"];
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<option value='0'>Select Location</option>");
                        foreach (var item in jvalue)
                        {
                            if (OfcrType.Contains("1") || OfcrType.Contains("4") || OfcrType.Contains("6"))
                            {
                                sb.Append("<option value=" + Convert.ToInt32((JValue)item["ZoneID"]) + ">" + (string)(JValue)item["RegionName"] + "</option>");
                            }
                            else if (OfcrType.Contains("3") || OfcrType.Contains("5"))
                            {
                                sb.Append("<option value=" + Convert.ToInt32((JValue)item["ZoneID"]) + ">" + (string)(JValue)item["ZoneName"] + "</option>");
                            }
                            else if (OfcrType.Contains("2"))
                            {
                                sb.Append("<option value=" + Convert.ToInt32((JValue)item["HQID"]) + ">" + (string)(JValue)item["HQName"] + "</option>");
                            }
                        }
                        return Json(sb.ToString());
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Location Details");
                        return Json("1");
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
                    {"Userid", Common.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getDistrict, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        var jobject = (JObject)JsonConvert.DeserializeObject(ResponseContent);
                        var jvalue = (JArray)jobject["Data"];
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<option value='0'>Select District</option>");
                        foreach (var item in jvalue)
                        {
                            sb.Append("<option value=" + Convert.ToInt32((JValue)item["districtID"]) + ">" + (string)(JValue)item["districtName"] + "</option>");
                        }
                        return Json(sb.ToString());
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading District Details");
                        return Json("1");
                    }
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> ValidateUserName(string userName)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"UserName", userName}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.validateUserName, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        var jobject = (JObject)JsonConvert.DeserializeObject(ResponseContent);
                        var jvalue = (JArray)jobject["Data"];
                        char ifExsist = 'N';
                        foreach (var item in jvalue)
                        {
                            if (Convert.ToInt32((JValue)item["Status"]) == 0)
                            {
                                ifExsist = 'Y';
                            }
                        }

                        return Json(ifExsist);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading District Details");
                        return Json("1");
                    }
                }
            }
        }
    }
}
