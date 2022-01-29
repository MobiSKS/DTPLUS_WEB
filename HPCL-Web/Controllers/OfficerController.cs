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
        public async Task<IActionResult> Details(string officerType, string location, int pg = 1)
        {
            List<OfficerListModel> ofcLstMdl = new List<OfficerListModel>();

            if (!String.IsNullOrEmpty(officerType))
            {
                HttpContext.Session.SetString("OfcrType", officerType);
            }
            else
            {
                officerType = HttpContext.Session.GetString("OfcrType");
            }

            if (!String.IsNullOrEmpty(location))
            {
                HttpContext.Session.SetString("Location", location);
            }
            else
            {
                location = HttpContext.Session.GetString("Location");
            }

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var form = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"OfficerType", String.IsNullOrEmpty(officerType) ? "0" : officerType },
                    {"Location", String.IsNullOrEmpty(location) ? "0" : location }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.getOfficerDetails, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerListModel> lst = jarr.ToObject<List<OfficerListModel>>();
                        ofcLstMdl.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }
            }

            ViewBag.OfficerType = HttpContext.Session.GetString("OfcrType");
            ViewBag.Location = HttpContext.Session.GetString("Location");

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int resCount = ofcLstMdl.Count();
            var pager = new PagerModel(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            var data = ofcLstMdl.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager; 

            return View(data);
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

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerTypeModel> lst = jarr.ToObject<List<OfficerTypeModel>>();
                        ofcrMdl.OfficerTypeMdl.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to load officer types";
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

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerStateModel> lst = jarr.ToObject<List<OfficerStateModel>>();
                        ofcrMdl.OfficerStateMdl.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to load states";
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
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"UserId", Common.userid},
                    {"FirstName", ofcrMdl.FirstName},
                    {"LastName", ofcrMdl.LastName},
                    {"UserName", ofcrMdl.UserName},
                    {"OfficerType", ofcrMdl.OfficerTypeID.ToString()},
                    {"LocationId", ofcrMdl.LocationID.ToString()},
                    {"Address1", ofcrMdl.Address1},
                    {"Address2", ofcrMdl.Address2},
                    {"Address3", ofcrMdl.Address3},
                    {"StateId", ofcrMdl.State.ToString()},
                    {"CityName", ofcrMdl.City},
                    {"DistrictId", ofcrMdl.DistrictID.ToString()},
                    {"Pin", ofcrMdl.Pin.ToString()},
                    {"MobileNo", ofcrMdl.Mobile},
                    {"PhoneNo", ofcrMdl.Phone},
                    {"EmailId", ofcrMdl.Email},
                    {"Fax", ofcrMdl.Fax},
                    {"Createdby", "0"}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerForms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.insertOfficer, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Message"].ToString();

                        ViewBag.Message = jarr;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to create Officer";
                    }
                }

                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent officerTypecontent = new StringContent(JsonConvert.SerializeObject(OfficerTypeForms), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.getOfficerType, officerTypecontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerTypeModel> lst = jarr.ToObject<List<OfficerTypeModel>>();
                        ofcrMdl.OfficerTypeMdl.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to load officer types";
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

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerStateModel> lst = jarr.ToObject<List<OfficerStateModel>>();
                        ofcrMdl.OfficerStateMdl.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to load states";
                    }
                }

            }
            return View(ofcrMdl);
        }
        public async Task<IActionResult> EditOfficer(string OfficerID)
        {
            char flag = 'N';
            OfficerModel ofcrMdl = new OfficerModel();
            //ofcrMdl.OfficerTypeID = 1;
            //ofcrMdl.State = 75;
            //ofcrMdl.DistrictID = 101;
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerBindForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"OfficerID", OfficerID}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent Bindcontent = new StringContent(JsonConvert.SerializeObject(OfficerBindForms), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.bindOfficer, Bindcontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerModel> lst = jarr.ToObject<List<OfficerModel>>();
                        ofcrMdl = lst.First();
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }

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

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerTypeModel> lst = jarr.ToObject<List<OfficerTypeModel>>();
                        ofcrMdl.OfficerTypeMdl.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to load officer types";
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

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerStateModel> lst = jarr.ToObject<List<OfficerStateModel>>();
                        ofcrMdl.OfficerStateMdl.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to load states";
                    }
                }

                var forms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent Districtcontent = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getDistrict, Districtcontent))
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
                        ofcrMdl.OfficerDistrictMdl.AddRange(SortedtList);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to load districts";
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
        public async Task<IActionResult> EditOfficer(OfficerModel ofcrMdl)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"UserId", Common.userid},
                    {"FirstName", ofcrMdl.FirstName},
                    {"LastName", ofcrMdl.LastName},
                    {"Address1", ofcrMdl.Address1},
                    {"Address2", ofcrMdl.Address2},
                    {"Address3", ofcrMdl.Address3},
                    {"StateId", ofcrMdl.State.ToString()},
                    {"CityName", ofcrMdl.City},
                    {"DistrictId", ofcrMdl.DistrictID.ToString()},
                    {"Pin", ofcrMdl.Pin.ToString()},
                    {"MobileNo", ofcrMdl.Mobile},
                    {"PhoneNo", ofcrMdl.Phone},
                    {"EmailId", ofcrMdl.Email},
                    {"Fax", ofcrMdl.Fax},
                    {"ModifiedBy", "0" },
                    {"OfficerId", "0"}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerTypeForms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.insertOfficer, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Message"].ToString();

                        ViewBag.Message = jarr;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to update Officer";
                    }
                }
            }
            ModelState.Clear();
            return View(ofcrMdl);
        }

        public async Task<IActionResult> EditLocation()
        {
            return View();
        }
        public async Task<IActionResult> Delete(string OfficerID)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerDeleteForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"UserId", Common.userid},
                    {"OfficerID", OfficerID},
                    {"ModifiedBy", "0"},

                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerDeleteForms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.deleteOfficer, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Message"].ToString();

                        ViewBag.Message = jarr;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        ViewBag.Message = "Failed to delete Officer";
                    }
                }
            }
            return RedirectToAction("Details", "Officer", new { pg = 1 });
        }
        //[HttpPost]
        //public async Task<IActionResult> EditLocation(OfficerLocatModel OfcrLocation)
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<JsonResult> GetOfficerTypeDetails()
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

                using (var Response = await client.PostAsync(WebApiUrl.getOfficerType, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerTypeModel> lst = jarr.ToObject<List<OfficerTypeModel>>();
                        //lst.Add(new OfficerTypeModel
                        //{
                        //    OfficerTypeID = 0,
                        //    OfficerTypeName = "All",
                        //    OfficerTypeShortName = "All"
                        //});
                        var SortedtList = lst.OrderBy(x => x.OfficerTypeID).ToList();
                        return Json(SortedtList);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Officer Type Details");
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        return Json("Failed to Load Officer Type Details");
                    }
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetLocationDetails(string OfcrType)
        {
            if (OfcrType == "0")
            {
                return Json("0");
            }
            if (String.IsNullOrEmpty(OfcrType))
            {
                OfcrType = HttpContext.Session.GetString("OfcrType");
            }

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
                            //lst.Add(new OfficerRegionModel
                            //{
                            //    RegionID = 0,
                            //    RegionName = "Select Location",
                            //    RegionShortName = "Select Location"
                            //});
                            var SortedtList = lst.OrderBy(x => x.RegionID).ToList();
                            return Json(SortedtList);
                        }
                        else if (OfcrType.Contains("3") || OfcrType.Contains("5"))
                        {
                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            List<OfficerZoneModel> lst = jarr.ToObject<List<OfficerZoneModel>>();
                            //lst.Add(new OfficerZoneModel
                            //{
                            //    HQID = 0,
                            //    ZoneID = 0,
                            //    ZoneName = "Select Location"
                            //});
                            var SortedtList = lst.OrderBy(x => x.ZoneID).ToList();
                            return Json(SortedtList);
                        }
                        else if (OfcrType.Contains("2"))
                        {
                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            List<OfficerHqModel> lst = jarr.ToObject<List<OfficerHqModel>>();
                            //lst.Add(new OfficerHqModel
                            //{
                            //    HQID = 0,
                            //    HQName = "Select Location",
                            //    HQShortName = "Select Location"
                            //});
                            var SortedtList = lst.OrderBy(x => x.HQID).ToList();
                            return Json(SortedtList);
                        }
                        return Json("Error");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Location Details");
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        return Json("Failed to load Location");
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
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        return Json("Failed to load District");
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
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        //var Message = obj["Message"].ToString();
                        return Json("Failed to validate user");
                    }
                }
            }
        }
    }
}
