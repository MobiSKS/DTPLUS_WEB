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
                try
                {
                    //Fetching Officer Type
                    using (var Response = await client.PostAsync(WebApiUrl.getOfficerType, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            List<OfficerTypeModel> lst = jarr.ToObject<List<OfficerTypeModel>>();
                            ofcrMdl.OfficerTypeMdl.AddRange(lst);
                        }
                    }
                }
                catch
                {

                }


                var OfficerStateForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"Country", "0"}
                };
                StringContent Statecontent = new StringContent(JsonConvert.SerializeObject(OfficerStateForms), Encoding.UTF8, "application/json");

                try
                {
                    //Fetching State
                    using (var Response = await client.PostAsync(WebApiUrl.getState, Statecontent))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            List<OfficerStateModel> lst = jarr.ToObject<List<OfficerStateModel>>();
                            ofcrMdl.OfficerStateMdl.AddRange(lst);
                        }
                    }
                }
                catch
                {

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

                        if (OfcrType.Contains("1") || OfcrType.Contains("4") || OfcrType.Contains("6"))
                        {
                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            List<OfficerRegionModel> lst = jarr.ToObject<List<OfficerRegionModel>>();
                            lst.Add(new OfficerRegionModel
                            {
                                RegionID = 0,
                                RegionName = "Select Location",
                                RegionShortName = "Select Location"
                            });
                            var SortedtList = lst.OrderBy(x => x.RegionID).ToList();
                            return Json(SortedtList);
                        }
                        else if (OfcrType.Contains("3") || OfcrType.Contains("5"))
                        {
                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            List<OfficerZoneModel> lst = jarr.ToObject<List<OfficerZoneModel>>();
                            lst.Add(new OfficerZoneModel
                            {
                                HQID = 0,
                                ZoneID = 0,
                                ZoneName = "Select Location"
                            });
                            var SortedtList = lst.OrderBy(x => x.ZoneID).ToList();
                            return Json(SortedtList);
                        }
                        else if (OfcrType.Contains("2"))
                        {
                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            List<OfficerHqModel> lst = jarr.ToObject<List<OfficerHqModel>>();
                            lst.Add(new OfficerHqModel
                            {
                                HQID = 0,
                                HQName = "Select Location",
                                HQShortName = "Select Location"
                            });
                            var SortedtList = lst.OrderBy(x => x.HQID).ToList();
                            return Json(SortedtList);
                        }
                        return Json("Error");
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
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }
    }
}
