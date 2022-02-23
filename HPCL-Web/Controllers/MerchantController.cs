using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.Common;
using HPCL.Common.Models.ViewModel.Merchant;
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
        public async Task<IActionResult> CreateMerchant(string MerchantIDValue, string fromDate, string toDate, string category)
        {
            char flag = 'N';
            MerchantModel merchantModel = new MerchantModel();
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("MerchantID")))
            {
                merchantModel.SearchMerchantID = HttpContext.Session.GetString("MerchantID");
            }

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var MerchantTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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

                if (!String.IsNullOrEmpty(MerchantIDValue))
                {
                    var MerchantFormDetails = new Dictionary<string, string>
                    {
                        {"Useragent", CommonBase.useragent},
                        {"Userip", CommonBase.userip},
                        {"Userid", CommonBase.userid},
                        {"MerchantId", MerchantIDValue}
                    };

                    MerchantGetDetailsModel merchantDetailsModel = new MerchantGetDetailsModel();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    StringContent MerchantFormDetailsContent = new StringContent(JsonConvert.SerializeObject(MerchantFormDetails), Encoding.UTF8, "application/json");
                    using (var Response = await client.PostAsync(WebApiUrl.getMerchantByMerchantID, MerchantFormDetailsContent))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            try
                            {
                                List<MerchantGetDetailsModel> dtls = jarr.ToObject<List<MerchantGetDetailsModel>>();
                                merchantDetailsModel = dtls.First();
                            }
                            catch (Exception ex)
                            {
                                if (!String.IsNullOrEmpty(fromDate))
                                {
                                    ViewBag.NotFoundError = "Not Found";
                                    MerchantApprovalModel merchAppMdl = new MerchantApprovalModel();
                                    merchAppMdl.FromDate = fromDate;
                                    merchAppMdl.ToDate = toDate;
                                    merchAppMdl.CategoryID = category;
                                    merchAppMdl.ShowTable = "Error";

                                    return RedirectToAction("VerifyMerchant", merchAppMdl);
                                }    
                                ViewBag.NotFoundError = "Not Found";
                                return View(merchantModel);
                            }
                            merchantModel.SearchMerchantID = merchantDetailsModel.MerchantId;
                            merchantModel.MerchantID = merchantDetailsModel.MerchantId;
                            merchantModel.ERPCode = merchantDetailsModel.ErpCode;
                            merchantModel.RetailOutletName = merchantDetailsModel.RetailOutletName;
                            merchantModel.MerchantType = merchantDetailsModel.MerchantTypeId;
                            merchantModel.DealerName = merchantDetailsModel.DealerName;
                            merchantModel.MappedMerchantID = merchantDetailsModel.MappedMerchantId;
                            merchantModel.DealerMobileNumber = merchantDetailsModel.DealerMobileNo;
                            merchantModel.OutletCategory = merchantDetailsModel.OutletCategoryId;
                            merchantModel.PreHighwayNumber = merchantDetailsModel.HighwayNo1;
                            merchantModel.HighwayNumber = merchantDetailsModel.HighwayNo2;
                            merchantModel.SBUType = merchantDetailsModel.SBUTypeId;
                            merchantModel.LPG_CNGSale = merchantDetailsModel.LPGCNGSale;
                            merchantModel.PANCardNumber = merchantDetailsModel.PancardNumber;
                            merchantModel.GSTNumber = merchantDetailsModel.GSTNumber;
                            merchantModel.Retail_Outlet_Address1 = merchantDetailsModel.RetailOutletAddress1;
                            merchantModel.Retail_Outlet_Address2 = merchantDetailsModel.RetailOutletAddress2;
                            merchantModel.Retail_Outlet_Address3 = merchantDetailsModel.RetailOutletAddress3;
                            merchantModel.Retail_Outlet_Location = merchantDetailsModel.RetailOutletLocation;
                            merchantModel.Retail_Outlet_City = merchantDetailsModel.RetailOutletCity;
                            merchantModel.Retail_Outlet_State = merchantDetailsModel.RetailOutletStateId;
                            merchantModel.Retail_Outlet_District = merchantDetailsModel.RetailOutletDistrictId;
                            merchantModel.Retail_DistictID = merchantDetailsModel.RetailOutletDistrictId;
                            merchantModel.Retail_Outlet_Pin = merchantDetailsModel.RetailOutletPinNumber;
                            merchantModel.ZonalOffice = merchantDetailsModel.ZonalOfficeId;
                            merchantModel.ZonalOfficeID = merchantDetailsModel.ZonalOfficeId;
                            merchantModel.RegionalOffice = merchantDetailsModel.RegionalOfficeId;
                            merchantModel.RegionalOfcID = merchantDetailsModel.RegionalOfficeId;
                            merchantModel.SalesArea = merchantDetailsModel.SalesAreaId;
                            merchantModel.SalesAreaID = merchantDetailsModel.SalesAreaId;
                            merchantModel.FName = merchantDetailsModel.ContactPersonNameFirstName;
                            merchantModel.MName = merchantDetailsModel.ContactPersonNameMiddleName;
                            merchantModel.LName = merchantDetailsModel.ContactPersonNameLastName;
                            merchantModel.Mobile = merchantDetailsModel.MobileNo;
                            merchantModel.Email = merchantDetailsModel.EmailId;
                            merchantModel.Misc = merchantDetailsModel.Mics;
                            merchantModel.Comm_Address1 = merchantDetailsModel.CommunicationAddress1;
                            merchantModel.Comm_Address2 = merchantDetailsModel.CommunicationAddress2;
                            merchantModel.Comm_Address3 = merchantDetailsModel.CommunicationAddress3;
                            merchantModel.Comm_City = merchantDetailsModel.CommunicationCity;
                            merchantModel.Comm_State = merchantDetailsModel.CommunicationStateId;
                            merchantModel.Comm_District = merchantDetailsModel.CommunicationDistrictId;
                            merchantModel.Comm_DistictID = merchantDetailsModel.CommunicationDistrictId;
                            merchantModel.Comm_Pin = merchantDetailsModel.CommunicationPinNumber;
                            merchantModel.NumOfLiveTerminals = merchantDetailsModel.NoofLiveTerminals;
                            merchantModel.TerminalTypeRequested = merchantDetailsModel.TerminalTypeRequested;
                            merchantModel.Retail_Outlet_PhoneOffice = merchantDetailsModel.RetailOutletPhoneNumber;
                            merchantModel.Retail_Outlet_Fax = merchantDetailsModel.RetailOutletFax;
                            merchantModel.Comm_PhoneNumber = merchantDetailsModel.CommunicationPhoneNumber;
                            merchantModel.Comm_Fax = merchantDetailsModel.CommunicationFax;

                            ViewBag.Message = "LoadData";
                        }
                        else
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var Message = obj["errorMessage"].ToString();
                            ViewBag.Message = "Failed to load Merchant Data";
                        }
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
            char updateFlag = 'N';
            MerchantGetDetailsModel merchantModel = new MerchantGetDetailsModel();
            MerchantModel mrchntMdl = new MerchantModel();
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                if (!String.IsNullOrEmpty(merchantMdl.SearchMerchantID) && String.IsNullOrEmpty(merchantMdl.RetailOutletName))
                {
                    return RedirectToAction("CreateMerchant", new { MerchantIDValue = merchantMdl.SearchMerchantID });
                }
                else if (!String.IsNullOrEmpty(merchantMdl.SearchMerchantID) && !String.IsNullOrEmpty(merchantMdl.RetailOutletName))
                {
                    updateFlag = 'Y';
                }

                var MerchantTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                        merchantMdl.MerchantTypes.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load Merchant types";
                    }
                }

                var OutletCategoryForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        merchantMdl.OutletCategories.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to Outlet Categories";
                    }
                }

                var SBUForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        merchantMdl.SBUTypes.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to SBUs";
                    }
                }

                var MerchantStateForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                        merchantMdl.RetailOutletStates.AddRange(lst);
                        merchantMdl.CommStates.AddRange(lst1);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load Merchant States";
                    }
                }

                var MerchantZoneForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
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
                        merchantMdl.ZonalOffices.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load Merchant Zones";
                    }
                }

                if (updateFlag == 'Y')
                {
                    var MerchantUpdateForms = new Dictionary<string, string>
                    {
                        {"Useragent", CommonBase.useragent},
                        {"Userip", CommonBase.userip},
                        {"Userid", CommonBase.userid},
                        {"MerchantId", "30" + merchantMdl.ERPCode},
                        {"RetailOutletName", merchantMdl.RetailOutletName},
                        {"MerchantTypeId", merchantMdl.MerchantType},
                        {"DealerName", merchantMdl.DealerName},
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
                        {"RetailOutletPhoneNumber", merchantMdl.Retail_Outlet_PhoneOffice},
                        {"RetailOutletFax", merchantMdl.Retail_Outlet_Fax},
                        {"ZonalOfficeId", merchantMdl.ZonalOfficeID},
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
                        {"CommunicationPhoneNumber", merchantMdl.Comm_PhoneNumber},
                        {"CommunicationFax", merchantMdl.Comm_Fax},
                        {"ModifiedBy", HttpContext.Session.GetString("UserName")}
                    };

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    StringContent MerchantUpdateContent = new StringContent(JsonConvert.SerializeObject(MerchantUpdateForms), Encoding.UTF8, "application/json");
                    using (var Response = await client.PostAsync(WebApiUrl.updateMerchant, MerchantUpdateContent))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            string message = "";

                            if (obj["Status_Code"].ToString() == "200")
                            {
                                var jarr = obj["Data"].Value<JArray>();
                                List<ApiResponseModel> lst = jarr.ToObject<List<ApiResponseModel>>();
                                message = lst.First().Reason.ToString();
                            }
                            else
                            {
                                message = obj["Message"].ToString();
                            }

                            TempData["Response"] = message;

                            ViewBag.Message = message;
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
                else
                {
                    var MerchantCreateForms = new Dictionary<string, string>
                    {
                        {"Useragent", CommonBase.useragent},
                        {"Userip", CommonBase.userip},
                        {"Userid", CommonBase.userid},
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
                        {"RetailOutletPhoneNumber", merchantMdl.Retail_Outlet_PhoneOffice},
                        {"RetailOutletFax",merchantMdl.Retail_Outlet_Fax},
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
                        {"CommunicationPhoneNumber", merchantMdl.Comm_PhoneNumber},
                        {"CommunicationFax", merchantMdl.Comm_Fax},
                        {"NoofLiveTerminals", merchantMdl.NumOfLiveTerminals.ToString() },
                        {"TerminalTypeRequested", merchantMdl.TerminalTypeRequested },
                        {"CreatedBy", HttpContext.Session.GetString("UserName")}
                    };

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    StringContent MerchantCreateContent = new StringContent(JsonConvert.SerializeObject(MerchantCreateForms), Encoding.UTF8, "application/json");
                    using (var Response = await client.PostAsync(WebApiUrl.insertMerchant, MerchantCreateContent))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            string message = "";

                            if (obj["Status_Code"].ToString() == "200")
                            {
                                var jarr = obj["Data"].Value<JArray>();
                                List<ApiResponseModel> lst = jarr.ToObject<List<ApiResponseModel>>();
                                message = lst.First().Reason.ToString();
                            }
                            else
                            {
                                message = obj["Message"].ToString();
                            }

                            TempData["Response"] = message;
                            ViewBag.Message = message;

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
            }
            return RedirectToAction("CreateMerchant");
        }
        public async Task<IActionResult> VerifyMerchant(MerchantApprovalModel merchaApprovalMdl, int pg = 1)
        {
            MerchantApprovalModel merchantApprovalMdl = new MerchantApprovalModel();
            if (!String.IsNullOrEmpty(merchaApprovalMdl.FromDate))
            {
                merchantApprovalMdl.FromDate = merchaApprovalMdl.FromDate;
                merchantApprovalMdl.ToDate = merchaApprovalMdl.ToDate;
                merchantApprovalMdl.CategoryID = merchaApprovalMdl.CategoryID;

                using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
                {
                    string[] fromDateArr = merchantApprovalMdl.FromDate.Split("-");
                    string[] toDateArr = merchantApprovalMdl.ToDate.Split("-");

                    string fromDate = fromDateArr[2] + "-" + fromDateArr[1] + "-" + fromDateArr[0];
                    string toDate = toDateArr[2] + "-" + toDateArr[1] + "-" + toDateArr[0];

                    var MerchantApprovalForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
                    {"Category", merchantApprovalMdl.CategoryID },
                    {"FromDate", fromDate },
                    {"ToDate", toDate }
                };

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    StringContent MerchantApprovalContent = new StringContent(JsonConvert.SerializeObject(MerchantApprovalForms), Encoding.UTF8, "application/json");
                    using (var Response = await client.PostAsync(WebApiUrl.getMerchantApprovalList, MerchantApprovalContent))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            List<MerchantApprovalTableModel> lst = jarr.ToObject<List<MerchantApprovalTableModel>>();
                            merchantApprovalMdl.MerchantApprovalTableDetails.AddRange(lst);
                        }
                        else
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var Message = obj["errorMessage"].ToString();
                            ViewBag.Message = "Failed to load Merchant types";
                        }
                    }
                }

                const int pageSize = 5;
                if (pg < 1)
                    pg = 1;

                int resCount = merchantApprovalMdl.MerchantApprovalTableDetails.Count();
                var pager = new PagerModel(resCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;

                var data = merchantApprovalMdl.MerchantApprovalTableDetails.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;

                merchantApprovalMdl.MerchantApprovalTableDetails.Clear();
                merchantApprovalMdl.MerchantApprovalTableDetails.AddRange(data);
            }

            if (merchaApprovalMdl.ShowTable == "Error")
            {
                ViewBag.NotFoundError = "Not Found";
            }
            return View(merchantApprovalMdl);
        }
        [HttpPost]
        public async Task<IActionResult> ActionOnMerchantID([FromBody] ApprovalRejectionModel erpnmodel)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {

                var forms = new ApprovalRejectionModel
                {
                    UserId = CommonBase.userid,
                    Useragent = CommonBase.useragent,
                    Userip = CommonBase.userip,
                    ObjApprovalRejectDetail = erpnmodel.ObjApprovalRejectDetail,
                    StatusId = erpnmodel.StatusId == "Approve" ? "4":"13",
                    ApprovedBy = "0"
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
                try
                {
                    using (var Response = await client.PostAsync(WebApiUrl.approveRejectMerchant, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            string message = "";

                            if (obj["Status_Code"].ToString() == "200")
                            {
                                var jarr = obj["Data"].Value<JArray>();
                                List<ApiResponseModel> lst = jarr.ToObject<List<ApiResponseModel>>();
                                message = lst.First().Reason.ToString();
                            }
                            else
                            {
                                message = obj["Message"].ToString();
                            }

                            return Json(new { success = true, message = message });
                            //return Json(jarr);
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Error Loading District Details");
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var Message = obj["errorMessage"].ToString();
                            return Json(new { success = false, message = "Error" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json("Failed to Approve/Reject");
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
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
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
                        List<MerchantSalesAreaModel> lst = jarr.ToObject<List<MerchantSalesAreaModel>>();
                        lst.Add(new MerchantSalesAreaModel
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
