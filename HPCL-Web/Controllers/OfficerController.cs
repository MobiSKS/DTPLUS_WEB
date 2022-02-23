using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.Common;
using HPCL.Common.Models.ViewModel.Officers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class OfficerController : Controller
    {
        public async Task<IActionResult> Details(string officerType, string location)
        {
            List<OfficerListModel> ofcLstMdl = new List<OfficerListModel>();

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var form = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }
            }

            ViewBag.OfficerType = officerType;
            ViewBag.Location = location;
            ViewBag.AddUpdateMessage = TempData["Message"];

            return View(ofcLstMdl);
        }
        public async Task<IActionResult> Create()
        {
            char flag = 'N';
            OfficerModel ofcrMdl = new OfficerModel();
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }

                var OfficerStateForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        var Message = obj["errorMessage"].ToString();
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
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", CommonBase.userid},
                    {"FirstName", ofcrMdl.FirstName},
                    {"LastName", string.IsNullOrEmpty(ofcrMdl.LastName) ? "":ofcrMdl.LastName},
                    {"UserName", ofcrMdl.UserName},
                    {"OfficerType", ofcrMdl.OfficerTypeID},
                    {"LocationId", ofcrMdl.LocationID},
                    {"Address1", ofcrMdl.Address1},
                    {"Address2", string.IsNullOrEmpty(ofcrMdl.Address2) ? "":ofcrMdl.Address2},
                    {"Address3", string.IsNullOrEmpty(ofcrMdl.Address3) ? "":ofcrMdl.Address3},
                    {"StateId", ofcrMdl.State.ToString()},
                    {"CityName", string.IsNullOrEmpty(ofcrMdl.City) ? "":ofcrMdl.City},
                    {"DistrictId", string.IsNullOrEmpty(ofcrMdl.DistrictID) ? "0":ofcrMdl.DistrictID},
                    {"Pin", string.IsNullOrEmpty(ofcrMdl.Pin) ? "":ofcrMdl.Pin},
                    {"MobileNo", string.IsNullOrEmpty(ofcrMdl.Mobile) ? "":ofcrMdl.Mobile},
                    {"PhoneNo", string.IsNullOrEmpty(ofcrMdl.Phone) ? "":ofcrMdl.Phone},
                    {"EmailId", string.IsNullOrEmpty(ofcrMdl.Email) ? "":ofcrMdl.Email},
                    {"Fax", string.IsNullOrEmpty(ofcrMdl.Fax) ? "":ofcrMdl.Fax},
                    {"Createdby", HttpContext.Session.GetString("UserName")}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerForms), Encoding.UTF8, "application/json");
                if (ofcrMdl.OfficerTypeName == "RBE")
                {
                    using (var Response = await client.PostAsync(WebApiUrl.insertRbeOfficer, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            string message = "";
                            string status = "";

                            if (obj["Status_Code"].ToString() == "200")
                            {
                                var jarr = obj["Data"].Value<JArray>();
                                List<ApiResponseModel> lst = jarr.ToObject<List<ApiResponseModel>>();
                                message = lst.First().Reason.ToString();
                                status = lst.First().Status.ToString();
                                if (status == "1")
                                {
                                    TempData["Message"] = message;
                                    return RedirectToAction("Details", "Officer", new { pg = 1 });
                                }
                            }
                            else
                            {
                                message = obj["Message"].ToString();
                            }

                            ViewBag.Message = message;
                        }
                        else
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var Message = obj["errorMessage"].ToString();
                            ViewBag.Message = "Failed to create Officer";
                        }
                    }
                }
                else
                {
                    using (var Response = await client.PostAsync(WebApiUrl.insertOfficer, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            string message = "";
                            string status = "";

                            if (obj["Status_Code"].ToString() == "200")
                            {
                                var jarr = obj["Data"].Value<JArray>();
                                List<ApiResponseModel> lst = jarr.ToObject<List<ApiResponseModel>>();
                                message = lst.First().Reason.ToString();
                                status = lst.First().Status.ToString();
                                if (status == "1")
                                {
                                    TempData["Message"] = message;
                                    return RedirectToAction("Details", "Officer", new { pg = 1 });
                                }
                            }
                            else
                            {
                                message = obj["Message"].ToString();
                            }

                            ViewBag.Message = message;
                        }
                        else
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var Message = obj["errorMessage"].ToString();
                            ViewBag.Message = "Failed to create Officer";
                        }
                    }
                }

                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }

                var OfficerStateForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        var Message = obj["errorMessage"].ToString();
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
            ofcrMdl.OfficerID = OfficerID;

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerBindForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        ofcrMdl.State = ofcrMdl.StateId;
                        ofcrMdl.Mobile = ofcrMdl.MobileNo;
                        ofcrMdl.Email = ofcrMdl.EmailId;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }

                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }

                var OfficerStateForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load states";
                    }
                }

                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                        var Message = obj["errorMessage"].ToString();
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
                var EditOfficerForm = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", CommonBase.userid},
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
                    {"OfficerId", ofcrMdl.OfficerID},
                    {"CreatedBy",HttpContext.Session.GetString("UserName") }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent editOfficerContent = new StringContent(JsonConvert.SerializeObject(EditOfficerForm), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.updateOfficer, editOfficerContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        string message = "";
                        string status = "";

                        if (obj["Status_Code"].ToString() == "200")
                        {
                            var jarr = obj["Data"].Value<JArray>();
                            List<ApiResponseModel> lst = jarr.ToObject<List<ApiResponseModel>>();
                            message = lst.First().Reason.ToString();
                            status = lst.First().Status.ToString();
                            if (status == "1")
                            {
                                TempData["Message"] = message;
                                return RedirectToAction("Details", "Officer", new { pg = 1 });
                            }
                        }
                        else
                        {
                            message = obj["Message"].ToString();
                        }

                        ViewBag.Message = message;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to update Officer";
                    }
                }

                //Fetching other details

                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                        ofcrMdl.OfficerTypeID = ofcrMdl.OfficerID;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }

                var OfficerStateForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load states";
                    }
                }

                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load districts";
                    }
                }
            }
            //ModelState.Clear();
            return View(ofcrMdl);
        }
        public async Task<IActionResult> EditLocation(string OfficerID)
        {
            char flag = 'N';
            OfficerLocationModel OfcrLocMdl = new OfficerLocationModel();
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerTypeForms), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.zonalOffice, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<ZoneOffice> lst = jarr.ToObject<List<ZoneOffice>>();
                        OfcrLocMdl.ZoneOffices.AddRange(lst);
                        OfcrLocMdl.UserName = CommonBase.userid;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }

                var OfficerLocationForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
                    {"OfficerID", OfficerID}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                StringContent OfficerLocationMappingContent = new StringContent(JsonConvert.SerializeObject(OfficerLocationForms), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.getLocationMapping, OfficerLocationMappingContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<LocationMapping> lst = jarr.ToObject<List<LocationMapping>>();
                        OfcrLocMdl.LocationMappings.AddRange(lst);
                        OfcrLocMdl.UserName = CommonBase.userid;
                        OfcrLocMdl.OfficerID = OfficerID;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
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
                    return View(OfcrLocMdl);
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditLocation(OfficerLocationModel ofcrLocationMdl)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                string message = "";
                string status = "";

                var OfficerlocationForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
                    {"OfficerId", ofcrLocationMdl.OfficerID},
                    {"UserName", ofcrLocationMdl.UserName},
                    {"ZO", ofcrLocationMdl.ZoneOfcID},
                    {"RO", ofcrLocationMdl.RegionalOfcID},
                    {"Createdby", HttpContext.Session.GetString("UserName")},
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerlocationForms), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.insertOfficerLocationMapping, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

                        if (obj["Status_Code"].ToString() == "200")
                        {
                            var jarr = obj["Data"].Value<JArray>();
                            List<ApiResponseModel> lst = jarr.ToObject<List<ApiResponseModel>>();
                            message = lst.First().Reason.ToString();
                            status = lst.First().Status.ToString();
                        }
                        else
                        {
                            message = obj["Message"].ToString();
                        }
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }
            }

            OfficerLocationModel OfcrLocMdl = new OfficerLocationModel();

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerTypeForms), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.zonalOffice, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<ZoneOffice> lst = jarr.ToObject<List<ZoneOffice>>();
                        OfcrLocMdl.ZoneOffices.AddRange(lst);
                        OfcrLocMdl.UserName = CommonBase.userid;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }

                var OfficerLocationForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
                    {"OfficerID", ofcrLocationMdl.OfficerID}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                StringContent OfficerLocationMappingContent = new StringContent(JsonConvert.SerializeObject(OfficerLocationForms), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.getLocationMapping, OfficerLocationMappingContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<LocationMapping> lst = jarr.ToObject<List<LocationMapping>>();
                        OfcrLocMdl.LocationMappings.AddRange(lst);
                        OfcrLocMdl.UserName = CommonBase.userid;
                        OfcrLocMdl.OfficerID = ofcrLocationMdl.OfficerID;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }
            }

            return View(OfcrLocMdl);
        }
        public async Task<IActionResult> Delete(string OfficerID)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerDeleteForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", CommonBase.userid},
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
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to delete Officer";
                    }
                }
            }
            return RedirectToAction("Details", "Officer", new { pg = 1 });
        }
        public async Task<IActionResult> DeleteLocation(string userName, string zonalID, string regionalID, string officerID)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                string message = "";
                string status = "";

                var DeleteLocationForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", CommonBase.userid},
                    {"UserName", userName},
                    {"ZO", zonalID},
                    {"RO", regionalID},
                    {"ModifiedBy", HttpContext.Session.GetString("UserName")}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(DeleteLocationForms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.deleteOfficerLocationMapping, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

                        if (obj["Status_Code"].ToString() == "200")
                        {
                            var jarr = obj["Data"].Value<JArray>();
                            List<ApiResponseModel> lst = jarr.ToObject<List<ApiResponseModel>>();
                            message = lst.First().Reason.ToString();
                            status = lst.First().Status.ToString();
                        }
                        else
                        {
                            message = obj["Message"].ToString();
                        }
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to delete Officer";
                    }
                }
            }

            OfficerLocationModel OfcrLocMdl = new OfficerLocationModel();

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerTypeForms), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.zonalOffice, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<ZoneOffice> lst = jarr.ToObject<List<ZoneOffice>>();
                        OfcrLocMdl.ZoneOffices.AddRange(lst);
                        OfcrLocMdl.UserName = CommonBase.userid;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }

                var OfficerLocationForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
                    {"OfficerID", officerID}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                StringContent OfficerLocationMappingContent = new StringContent(JsonConvert.SerializeObject(OfficerLocationForms), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.getLocationMapping, OfficerLocationMappingContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<LocationMapping> lst = jarr.ToObject<List<LocationMapping>>();
                        OfcrLocMdl.LocationMappings.AddRange(lst);
                        OfcrLocMdl.UserName = CommonBase.userid;
                        OfcrLocMdl.OfficerID = officerID;
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }
            }

            return RedirectToAction("EditLocation", "Officer", new { OfficerID = officerID });
        }
        public async Task<IActionResult> OfficerDetails()
        {
            OfficerDetailsModel ofcLstMdl = new OfficerDetailsModel();

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerStateForms = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"ZonalId", ""},
                    {"RegionalId", ""},
                    {"Country", "0"}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
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
                        ofcLstMdl.OfficerStates.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load states";
                    }
                }

                var OfficerZonalDetails = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                StringContent Zonalcontent = new StringContent(JsonConvert.SerializeObject(OfficerZonalDetails), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.zonalOffice, Zonalcontent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerZoneModel> lst = jarr.ToObject<List<OfficerZoneModel>>();
                        ofcLstMdl.OfficerZones.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }
            }

            ViewBag.OfficerType = "";
            ViewBag.Location = "";
            ViewBag.AddUpdateMessage = TempData["Message"];

            return View(ofcLstMdl);
        }

        public async Task<IActionResult> GetOfficerDetailsTable(string ZonalOfcID, string RegionalOfcID, string StateID, string DistrictID)
        {

            List<OfficerDetailsTable> lst = await OfficerDetailsTableDataAsync(ZonalOfcID, RegionalOfcID, StateID, DistrictID);
            return PartialView("~/Views/Officer/_OfficerDetailsTable.cshtml", lst);
            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    var form = new Dictionary<string, string>
            //    {
            //        {"Useragent", Common.useragent},
            //        {"Userip", Common.userip},
            //        {"Userid", Common.userid},
            //        {"ZonalId", "" },
            //        {"RegionalId", "" },
            //        {"ZO", ZonalOfcID == "0" ? "":ZonalOfcID },
            //        {"RO", (string.IsNullOrEmpty(RegionalOfcID) || RegionalOfcID == "0")  ? "":RegionalOfcID },
            //        {"StateId", StateID == "0" ? "":StateID },
            //        {"DistrictId", (string.IsNullOrEmpty(DistrictID) || DistrictID == "0") ? "":DistrictID }
            //    };

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            //    StringContent content = new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getOfficerDetailByLocation, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<OfficerDetailsTable> lst = jarr.ToObject<List<OfficerDetailsTable>>();
            //            return PartialView("~/Views/Officer/_OfficerDetailsTable.cshtml", lst);
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading Officer Details Table");
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var Message = obj["errorMessage"].ToString();
            //            return Json("Failed to load Officer Details Table");
            //        }
            //    }
            //}
        }
        public async Task<List<OfficerDetailsTable>> OfficerDetailsTableDataAsync(string ZonalOfcID, string RegionalOfcID, string StateID, string DistrictID)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var form = new Dictionary<string, string>
                {
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                    {"ZonalId", "" },
                    {"RegionalId", "" },
                    {"ZO", ZonalOfcID == "0" ? "":ZonalOfcID },
                    {"RO", (string.IsNullOrEmpty(RegionalOfcID) || RegionalOfcID == "0")  ? "":RegionalOfcID },
                    {"StateId", StateID == "0" ? "":StateID },
                    {"DistrictId", (string.IsNullOrEmpty(DistrictID) || DistrictID == "0") ? "":DistrictID }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getOfficerDetailByLocation, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerDetailsTable> lst = jarr.ToObject<List<OfficerDetailsTable>>();
                        return lst;
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Officer Details Table");
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        List<OfficerDetailsTable> lst = new List<OfficerDetailsTable>();
                        return lst;
                    }
                }
            }
        }
        //public async Task<IActionResult> DownloadExcel(string ZonalOfcID, string RegionalOfcID, string StateID, string DistrictID)
        //{
        //    List<OfficerDetailsTable> lst = await OfficerDetailsTableDataAsync(ZonalOfcID, RegionalOfcID, StateID, DistrictID);

        //    DataTable dt = new DataTable("Grid");

        //    dt.Columns.AddRange(new DataColumn[9] { new DataColumn("S.No."),
        //                                new DataColumn("ZO"),
        //                                new DataColumn("RO"),
        //                                new DataColumn("STATE"),
        //                                new DataColumn("DISTRICT"),
        //                                new DataColumn("MARKETING OFFICER NAME"),
        //                                new DataColumn("MARKETING OFFICER EMAIL"),
        //                                new DataColumn("ZONAL OFFICER NAME"),
        //                                new DataColumn("ZONAL OFFICER EMAIL")
        //    });

        //    int i = 1;
        //    foreach (var item in lst)
        //    {
        //        dt.Rows.Add(i, item.ZonalOfficeName, item.RegionalOfficeName, item.StateName, item.DistrictName, item.MarketingOfficerName, item.MarketingOfficerEmail, item.ZonalOfficerName, item.ZonalOfficerEmail);
        //        i++;
        //    }

        //    Microsoft.Office.Interop.Excel.Application excel;
        //    Microsoft.Office.Interop.Excel.Workbook worKbooK;
        //    Microsoft.Office.Interop.Excel.Worksheet worKsheeT;
        //    Microsoft.Office.Interop.Excel.Range celLrangE;

        //    try
        //    {
        //        excel = new Microsoft.Office.Interop.Excel.Application();
        //        excel.Visible = false;
        //        excel.DisplayAlerts = false;
        //        worKbooK = excel.Workbooks.Add(Type.Missing);


        //        worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
        //        worKsheeT.Name = "StudentRepoertCard";

        //        worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, 8]].Merge();
        //        worKsheeT.Cells[1, 1] = "Student Report Card";
        //        worKsheeT.Cells.Font.Size = 15;


        //        int rowcount = 2;

        //        foreach (DataRow datarow in ExportToExcel().Rows)
        //        {
        //            rowcount += 1;
        //            for (int i = 1; i <= ExportToExcel().Columns.Count; i++)
        //            {

        //                if (rowcount == 3)
        //                {
        //                    worKsheeT.Cells[2, i] = ExportToExcel().Columns[i - 1].ColumnName;
        //                    worKsheeT.Cells.Font.Color = System.Drawing.Color.Black;

        //                }

        //                worKsheeT.Cells[rowcount, i] = datarow[i - 1].ToString();

        //                if (rowcount > 3)
        //                {
        //                    if (i == ExportToExcel().Columns.Count)
        //                    {
        //                        if (rowcount % 2 == 0)
        //                        {
        //                            celLrangE = worKsheeT.Range[worKsheeT.Cells[rowcount, 1], worKsheeT.Cells[rowcount, ExportToExcel().Columns.Count]];
        //                        }

        //                    }
        //                }

        //            }

        //        }

        //        celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[rowcount, ExportToExcel().Columns.Count]];
        //        celLrangE.EntireColumn.AutoFit();
        //        Microsoft.Office.Interop.Excel.Borders border = celLrangE.Borders;
        //        border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        //        border.Weight = 2d;

        //        celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[2, ExportToExcel().Columns.Count]];

        //        worKbooK.SaveAs(textBox1.Text); ;
        //        worKbooK.Close();
        //        excel.Quit();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);

        //    }
        //    finally
        //    {
        //        worKsheeT = null;
        //        celLrangE = null;
        //        worKbooK = null;
        //    }

        //    //DataTable dt = new DataTable("Grid");

        //    //dt.Columns.AddRange(new DataColumn[9] { new DataColumn("S.No."),
        //    //                            new DataColumn("ZO"),
        //    //                            new DataColumn("RO"),
        //    //                            new DataColumn("STATE"),
        //    //                            new DataColumn("DISTRICT"),
        //    //                            new DataColumn("MARKETING OFFICER NAME"),
        //    //                            new DataColumn("MARKETING OFFICER EMAIL"),
        //    //                            new DataColumn("ZONAL OFFICER NAME"),
        //    //                            new DataColumn("ZONAL OFFICER EMAIL")
        //    //});

        //    //int i = 1;
        //    //foreach (var item in lst)
        //    //{
        //    //    dt.Rows.Add(i, item.ZonalOfficeName, item.RegionalOfficeName, item.StateName, item.DistrictName, item.MarketingOfficerName, item.MarketingOfficerEmail, item.ZonalOfficerName, item.ZonalOfficerEmail);
        //    //    i++;
        //    //}

        //    //using (XLWorkbook wb = new XLWorkbook())
        //    //{
        //    //    wb.Worksheets.Add(dt);
        //    //    using (MemoryStream stream = new MemoryStream())
        //    //    {
        //    //        wb.SaveAs(stream);
        //    //        return File(stream.ToArray(), "application/ms-excel", "OfficerInformation.xlsx");
        //    //    }
        //    //}
        //}
        [HttpPost]
        public async Task<JsonResult> GetOfficerTypeDetails()
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                        var SortedtList = lst.OrderBy(x => x.OfficerTypeName).ToList();
                        return Json(SortedtList);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Officer Type Details");
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        return Json("Failed to Load Officer Type Details");
                    }
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetLocationDetails(string OfcrType)
        {
            if (OfcrType == "All" || string.IsNullOrEmpty(OfcrType))
            {
                OfcrType = "0";
            }

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
                getLocation = WebApiUrl.regionalOffice;
            }
            else if (OfcrType.Contains("3") || OfcrType.Contains("5"))
            {
                getLocation = WebApiUrl.zonalOffice;
            }
            else if (OfcrType.Contains("2"))
            {
                getLocation = WebApiUrl.getLocationHq;
            }

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                            //    RegionalOfficeID = 0,
                            //    RegionalOfficeName = "Select Location"
                            //});
                            var SortedtList = lst.OrderBy(x => x.RegionalOfficeName).ToList();
                            return Json(SortedtList);
                        }
                        else if (OfcrType.Contains("3") || OfcrType.Contains("5"))
                        {
                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            List<OfficerZoneModel> lst = jarr.ToObject<List<OfficerZoneModel>>();
                            //lst.Add(new OfficerZoneModel
                            //{
                            //    ZonalOfficeID = 0,
                            //    ZonalOfficeName = "Select Location"
                            //});
                            var SortedtList = lst.OrderBy(x => x.ZonalOfficeName).ToList();
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
                            //    HQName = "Select Location"
                            //});
                            var SortedtList = lst.OrderBy(x => x.HQName).ToList();
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
                        var Message = obj["errorMessage"].ToString();
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
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        //lst.Add(new OfficerDistrictModel
                        //{
                        //    stateID = 0,
                        //    districtID = 0,
                        //    districtName = "Select District"
                        //});
                        var SortedtList = lst.OrderBy(x => x.districtName).ToList();
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
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        List<RegionalOffice> lst = jarr.ToObject<List<RegionalOffice>>();
                        //lst.Add(new RegionalOffice
                        //{
                        //    RegionalOfficeID = 0,
                        //    RegionalOfficeName = "--Select--"
                        //});
                        var SortedtList = lst.OrderBy(x => x.RegionalOfficeName).ToList();
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
        public async Task<JsonResult> ValidateUserName(string userName)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        var Message = obj["errorMessage"].ToString();
                        return Json("Failed to validate user");
                    }
                }
            }
        }
    }
}
