using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.ResponseModel.Login;
using System.IO;
using System;
using HPCL.Common.Models.CommonEntity;
using System.Linq;
using System.Net;

namespace HPCL_Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        HelperAPI _api = new HelperAPI();
        //Common _common = new Common();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Profile()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            SessionMenuModel.menuList.RemoveAll(x => x.UserID.ToLower() == HttpContext.Session.GetString("UserId"));
            SessionMenuModel.sessionList.RemoveAll(x => x.UserID.ToLower() == HttpContext.Session.GetString("UserId"));
            return RedirectToAction("Index");
        }

        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }


        [HttpPost]
        public async Task<IActionResult> Index(UserInfoModel user)
        {
            if (ModelState.IsValid)
            {
                // Validate Captcha Code
                if (!Captcha.ValidateCaptchaCode(user.CaptchaCode, HttpContext))
                {
                    ViewBag.CaptchaCodeStatus = "Error";
                    return View(user);
                }
            }
            //Setting User IP from Front End
            //_httpContextAccessor.HttpContext.Session.GetString("IpAddress") = user.Userip;

            //var access_token = _api.GetToken();

            //if (access_token.Result != null)
            //{
            //    HttpContext.Session.SetString("Token_" + user.UserId, access_token.Result);
            //}

            var loginBody = new UserInfoModel
            {
                UserId = user.UserId.ToLower(),
                Useragent = CommonBase.useragent,
                Userip = user.Userip,
                Password = user.Password
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                //client.DefaultRequestHeaders.Add("Secret_Key", "PVmMSclp834KBIUa9O-XxpBsDJhsi1dsds74CiGaoo5");

                StringContent content = new StringContent(JsonConvert.SerializeObject(loginBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getLoginUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<LoginResponse> loginRes = jarr.ToObject<List<LoginResponse>>();

                        if (loginRes[0].Status == 0)
                        {
                            ModelState.Clear();
                            //HttpContext.Session.Remove("Token_" + user.UserId);
                            ViewBag.Message = loginRes[0].Reason;
                            return View(user);
                        }
                        else if (loginRes[0].Status == 1)
                        {
                            Random rnd = new Random();
                            int num = rnd.Next();

                            //if (!SessionMenuModel.sessionList.Any(a => a.UserID == user.UserId))
                            //{
                            SessionMenuModel.sessionList.RemoveAll(x => x.UserID.ToLower() == user.UserId.ToLower());

                            List<SessionDataModelDetails> sessionData = new List<SessionDataModelDetails>
                            {
                                new SessionDataModelDetails { UserID = user.UserId.ToLower(), LocalStorage = num.ToString(),
                                    UserName = loginRes[0].UserName, LoginType = loginRes[0].LoginType,
                                    RegionalId = string.IsNullOrEmpty(loginRes[0].RegionalOfficeID) ? "" : loginRes[0].RegionalOfficeID,
                                    ZonalId = string.IsNullOrEmpty(loginRes[0].ZonalOfficeID) ? "" : loginRes[0].ZonalOfficeID,
                                    MerchantID = loginRes[0].LoginType == "Merchant" ? loginRes[0].UserId.ToLower() : "",
                                    UserId = loginRes[0].UserId.ToLower(),
                                    Today = DateTime.Now.ToString("yyyy-MM-dd"),
                                    Token = loginRes[0].Token,
                                    UserRole = loginRes[0].UserRole,
                                    IpAddress = user.Userip
                                }
                            };

                            SessionMenuModel.sessionList.AddRange(sessionData);

                            HttpContext.Session.SetString("LocalStorage", num.ToString());
                            HttpContext.Session.SetString("UserName", loginRes[0].UserName);

                            HttpContext.Session.SetString("UserId", loginRes[0].UserId.ToLower());

                            if (loginRes[0].LoginType == "Customer")
                            {
                                if (SessionMenuModel.sessionList.Count == 1)
                                {
                                    HttpContext.Session.SetString("Token", SessionMenuModel.sessionList[0].Token);
                                    HttpContext.Session.SetString("LocalStorage", SessionMenuModel.sessionList[0].LocalStorage);
                                    HttpContext.Session.SetString("UserName", SessionMenuModel.sessionList[0].UserName);
                                    HttpContext.Session.SetString("LoginType", SessionMenuModel.sessionList[0].LoginType);
                                    HttpContext.Session.SetString("RegionalId", SessionMenuModel.sessionList[0].RegionalId);
                                    HttpContext.Session.SetString("ZonalId", SessionMenuModel.sessionList[0].ZonalId);
                                    HttpContext.Session.SetString("MerchantID", SessionMenuModel.sessionList[0].MerchantID);
                                    HttpContext.Session.SetString("UserId", SessionMenuModel.sessionList[0].UserId.ToLower());
                                    HttpContext.Session.SetString("Today", SessionMenuModel.sessionList[0].Today);
                                    HttpContext.Session.SetString("UserRole", SessionMenuModel.sessionList[0].UserRole);
                                    HttpContext.Session.SetString("BreadCrumbsController", SessionMenuModel.sessionList[0].BreadCrumbsController == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsController);
                                    HttpContext.Session.SetString("BreadCrumbsAction", SessionMenuModel.sessionList[0].BreadCrumbsAction == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsAction);
                                    HttpContext.Session.SetString("CurrentAction", SessionMenuModel.sessionList[0].CurrentAction == null ? "" : SessionMenuModel.sessionList[0].CurrentAction);
                                    HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName);
                                    HttpContext.Session.SetString("IpAddress", SessionMenuModel.sessionList[0].IpAddress);
                                }
                                else
                                {
                                    foreach (var item in SessionMenuModel.sessionList.Where(x => x.UserId.ToLower() == loginRes[0].UserId.ToLower()))
                                    {
                                        HttpContext.Session.SetString("Token", item.Token);
                                        HttpContext.Session.SetString("LocalStorage", item.LocalStorage);
                                        HttpContext.Session.SetString("UserName", item.UserName);
                                        HttpContext.Session.SetString("LoginType", item.LoginType);
                                        HttpContext.Session.SetString("RegionalId", item.RegionalId);
                                        HttpContext.Session.SetString("ZonalId", item.ZonalId);
                                        HttpContext.Session.SetString("MerchantID", item.MerchantID);
                                        HttpContext.Session.SetString("UserId", item.UserId.ToLower());
                                        HttpContext.Session.SetString("Today", item.Today);
                                        HttpContext.Session.SetString("UserRole", item.UserRole);
                                        HttpContext.Session.SetString("BreadCrumbsController", item.BreadCrumbsController == null ? "" : item.BreadCrumbsController);
                                        HttpContext.Session.SetString("BreadCrumbsAction", item.BreadCrumbsAction == null ? "" : item.BreadCrumbsAction);
                                        HttpContext.Session.SetString("CurrentAction", item.CurrentAction == null ? "" : item.CurrentAction);
                                        HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", item.BreadCrumbsPerviousMenuName == null ? "" : item.BreadCrumbsPerviousMenuName);
                                        HttpContext.Session.SetString("IpAddress", item.IpAddress);
                                    }
                                }

                                HttpContext.Session.SetString("CustomerZonalOfcId", loginRes[0].ZonalOfficeID ?? "");
                                HttpContext.Session.SetString("CustomerZonalOfcName", loginRes[0].ZonalOfficeName ?? "");
                                HttpContext.Session.SetString("CustomerRegionalOfcName", loginRes[0].RegionalOfficeName ?? "");
                                HttpContext.Session.SetString("RegionalOfcId", loginRes[0].RegionalOfficeID ?? "");
                                HttpContext.Session.SetInt32("CustomerSbuTypeId", loginRes[0].SBUTypeId);
                                HttpContext.Session.SetString("CustomerSbuTypeName", loginRes[0].SBUName ?? "");

                                return RedirectToAction("CustomerDashboard", "CustomerDashboard", new { CustomerId = HttpContext.Session.GetString("UserId").ToString() });

                            }
                            else if (loginRes[0].LoginType == "Merchant")
                            {
                                return RedirectToAction("Dashboard", "MerchantDashboard", new { CustomerId = HttpContext.Session.GetString("UserId").ToString() });
                            }
                            else
                            {
                                return RedirectToAction("Profile");
                            }

                        }
                        else
                        {
                            ModelState.Clear();
                            //HttpContext.Session.Remove("Token_" + user.UserId);
                            ViewBag.Message = "Details not found for this User!";
                            return View(user);
                        }

                        //ModelState.Clear();
                        //return Json(new { loginRes = loginRes });
                    }
                    else
                    {
                        ModelState.Clear();
                        //HttpContext.Session.Remove("Token_" + user.UserId);
                        ViewBag.Message = "Details not found for this User!";
                        return View(user);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> IndexPopup(UserInfoModel user)
        {
            if (ModelState.IsValid)
            {
                // Validate Captcha Code
                if (!Captcha.ValidateCaptchaCode(user.CaptchaCode, HttpContext))
                {
                    ViewBag.CaptchaCodeStatus = "Error";
                    return PartialView("~/Views/Home/IndexPopup.cshtml", user);
                }
            }
            //Setting User IP from Front End
            //_httpContextAccessor.HttpContext.Session.GetString("IpAddress") = user.Userip;

            //var access_token = _api.GetToken();

            //if (access_token.Result != null)
            //{
            //    HttpContext.Session.SetString("Token_" + user.UserId, access_token.Result);
            //}

            var loginBody = new UserInfoModel
            {
                UserId = user.UserId.ToLower(),
                Useragent = CommonBase.useragent,
                Userip = user.Userip,
                Password = user.Password
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                //client.DefaultRequestHeaders.Add("Secret_Key", "PVmMSclp834KBIUa9O-XxpBsDJhsi1dsds74CiGaoo5");

                StringContent content = new StringContent(JsonConvert.SerializeObject(loginBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getLoginUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<LoginResponse> loginRes = jarr.ToObject<List<LoginResponse>>();

                        if (loginRes[0].Status == 0)
                        {
                            ModelState.Clear();
                            //HttpContext.Session.Remove("Token_" + user.UserId);
                            ViewBag.Message = loginRes[0].Reason;
                            return PartialView("~/Views/Home/IndexPopup.cshtml", user);
                        }
                        else if (loginRes[0].Status == 1)
                        {
                            Random rnd = new Random();
                            int num = rnd.Next();

                            //if (!SessionMenuModel.sessionList.Any(a => a.UserID == user.UserId))
                            //{
                            SessionMenuModel.sessionList.RemoveAll(x => x.UserID.ToLower() == user.UserId.ToLower());

                            List<SessionDataModelDetails> sessionData = new List<SessionDataModelDetails>
                            {
                                new SessionDataModelDetails { UserID = user.UserId.ToLower(), LocalStorage = num.ToString(),
                                    UserName = loginRes[0].UserName, LoginType = loginRes[0].LoginType,
                                    RegionalId = string.IsNullOrEmpty(loginRes[0].RegionalOfficeID) ? "" : loginRes[0].RegionalOfficeID,
                                    ZonalId = string.IsNullOrEmpty(loginRes[0].ZonalOfficeID) ? "" : loginRes[0].ZonalOfficeID,
                                    MerchantID = loginRes[0].LoginType == "Merchant" ? loginRes[0].UserId.ToLower() : "",
                                    UserId = loginRes[0].UserId.ToLower(),
                                    Today = DateTime.Now.ToString("yyyy-MM-dd"),
                                    Token = loginRes[0].Token,
                                    UserRole = loginRes[0].UserRole,
                                    IpAddress = user.Userip
                                }
                            };

                            SessionMenuModel.sessionList.AddRange(sessionData);

                            HttpContext.Session.SetString("LocalStorage", num.ToString());
                            HttpContext.Session.SetString("UserName", loginRes[0].UserName);

                            HttpContext.Session.SetString("UserId", loginRes[0].UserId.ToLower());

                            if (loginRes[0].LoginType == "Customer")
                            {
                                if (SessionMenuModel.sessionList.Count == 1)
                                {
                                    HttpContext.Session.SetString("Token", SessionMenuModel.sessionList[0].Token);
                                    HttpContext.Session.SetString("LocalStorage", SessionMenuModel.sessionList[0].LocalStorage);
                                    HttpContext.Session.SetString("UserName", SessionMenuModel.sessionList[0].UserName);
                                    HttpContext.Session.SetString("LoginType", SessionMenuModel.sessionList[0].LoginType);
                                    HttpContext.Session.SetString("RegionalId", SessionMenuModel.sessionList[0].RegionalId);
                                    HttpContext.Session.SetString("ZonalId", SessionMenuModel.sessionList[0].ZonalId);
                                    HttpContext.Session.SetString("MerchantID", SessionMenuModel.sessionList[0].MerchantID);
                                    HttpContext.Session.SetString("UserId", SessionMenuModel.sessionList[0].UserId.ToLower());
                                    HttpContext.Session.SetString("Today", SessionMenuModel.sessionList[0].Today);
                                    HttpContext.Session.SetString("UserRole", SessionMenuModel.sessionList[0].UserRole);
                                    HttpContext.Session.SetString("BreadCrumbsController", SessionMenuModel.sessionList[0].BreadCrumbsController == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsController);
                                    HttpContext.Session.SetString("BreadCrumbsAction", SessionMenuModel.sessionList[0].BreadCrumbsAction == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsAction);
                                    HttpContext.Session.SetString("CurrentAction", SessionMenuModel.sessionList[0].CurrentAction == null ? "" : SessionMenuModel.sessionList[0].CurrentAction);
                                    HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName);
                                    HttpContext.Session.SetString("IpAddress", SessionMenuModel.sessionList[0].IpAddress);
                                }
                                else
                                {
                                    foreach (var item in SessionMenuModel.sessionList.Where(x => x.UserId.ToLower() == loginRes[0].UserId.ToLower()))
                                    {
                                        HttpContext.Session.SetString("Token", item.Token);
                                        HttpContext.Session.SetString("LocalStorage", item.LocalStorage);
                                        HttpContext.Session.SetString("UserName", item.UserName);
                                        HttpContext.Session.SetString("LoginType", item.LoginType);
                                        HttpContext.Session.SetString("RegionalId", item.RegionalId);
                                        HttpContext.Session.SetString("ZonalId", item.ZonalId);
                                        HttpContext.Session.SetString("MerchantID", item.MerchantID);
                                        HttpContext.Session.SetString("UserId", item.UserId.ToLower());
                                        HttpContext.Session.SetString("Today", item.Today);
                                        HttpContext.Session.SetString("UserRole", item.UserRole);
                                        HttpContext.Session.SetString("BreadCrumbsController", item.BreadCrumbsController == null ? "" : item.BreadCrumbsController);
                                        HttpContext.Session.SetString("BreadCrumbsAction", item.BreadCrumbsAction == null ? "" : item.BreadCrumbsAction);
                                        HttpContext.Session.SetString("CurrentAction", item.CurrentAction == null ? "" : item.CurrentAction);
                                        HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", item.BreadCrumbsPerviousMenuName == null ? "" : item.BreadCrumbsPerviousMenuName);
                                        HttpContext.Session.SetString("IpAddress", item.IpAddress);
                                    }
                                }

                                HttpContext.Session.SetString("CustomerZonalOfcId", loginRes[0].ZonalOfficeID ?? "");
                                HttpContext.Session.SetString("CustomerZonalOfcName", loginRes[0].ZonalOfficeName ?? "");
                                HttpContext.Session.SetString("CustomerRegionalOfcName", loginRes[0].RegionalOfficeName ?? "");
                                HttpContext.Session.SetString("RegionalOfcId", loginRes[0].RegionalOfficeID ?? "");
                                HttpContext.Session.SetInt32("CustomerSbuTypeId", loginRes[0].SBUTypeId);
                                HttpContext.Session.SetString("CustomerSbuTypeName", loginRes[0].SBUName ?? "");

                                return RedirectToAction("CustomerDashboard", "CustomerDashboard", new { CustomerId = HttpContext.Session.GetString("UserId").ToString() });

                            }
                            else if (loginRes[0].LoginType == "Merchant")
                            {
                                return RedirectToAction("Dashboard", "MerchantDashboard", new { CustomerId = HttpContext.Session.GetString("UserId").ToString() });
                            }
                            else
                            {
                                return RedirectToAction("Profile");
                            }

                        }
                        else
                        {
                            ModelState.Clear();
                            //HttpContext.Session.Remove("Token_" + user.UserId);
                            ViewBag.Message = "Details not found for this User!";
                            return PartialView("~/Views/Home/IndexPopup.cshtml", user);
                        }

                        //ModelState.Clear();
                        //return Json(new { loginRes = loginRes });
                    }
                    else
                    {
                        ModelState.Clear();
                        //HttpContext.Session.Remove("Token_" + user.UserId);
                        ViewBag.Message = "Details not found for this User!";
                        return PartialView("~/Views/Home/IndexPopup.cshtml", user);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> TopMenu([FromBody] string userId)
        {
            if (SessionMenuModel.sessionList.Count == 1)
            {
                HttpContext.Session.SetString("Token", SessionMenuModel.sessionList[0].Token);
                HttpContext.Session.SetString("LocalStorage", SessionMenuModel.sessionList[0].LocalStorage);
                HttpContext.Session.SetString("UserName", SessionMenuModel.sessionList[0].UserName);
                HttpContext.Session.SetString("LoginType", SessionMenuModel.sessionList[0].LoginType);
                HttpContext.Session.SetString("RegionalId", SessionMenuModel.sessionList[0].RegionalId);
                HttpContext.Session.SetString("ZonalId", SessionMenuModel.sessionList[0].ZonalId);
                HttpContext.Session.SetString("MerchantID", SessionMenuModel.sessionList[0].MerchantID);
                HttpContext.Session.SetString("UserId", SessionMenuModel.sessionList[0].UserId.ToLower());
                HttpContext.Session.SetString("Today", SessionMenuModel.sessionList[0].Today);
                HttpContext.Session.SetString("UserRole", SessionMenuModel.sessionList[0].UserRole);
                HttpContext.Session.SetString("BreadCrumbsController", SessionMenuModel.sessionList[0].BreadCrumbsController == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsController);
                HttpContext.Session.SetString("BreadCrumbsAction", SessionMenuModel.sessionList[0].BreadCrumbsAction == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsAction);
                HttpContext.Session.SetString("CurrentAction", SessionMenuModel.sessionList[0].CurrentAction == null ? "" : SessionMenuModel.sessionList[0].CurrentAction);
                HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName);
                HttpContext.Session.SetString("IpAddress", SessionMenuModel.sessionList[0].IpAddress);
            }
            else
            {
                foreach (var item in SessionMenuModel.sessionList.Where(x => x.UserId.ToLower() == userId.ToLower()))
                {
                    HttpContext.Session.SetString("Token", item.Token);
                    HttpContext.Session.SetString("LocalStorage", item.LocalStorage);
                    HttpContext.Session.SetString("UserName", item.UserName);
                    HttpContext.Session.SetString("LoginType", item.LoginType);
                    HttpContext.Session.SetString("RegionalId", item.RegionalId);
                    HttpContext.Session.SetString("ZonalId", item.ZonalId);
                    HttpContext.Session.SetString("MerchantID", item.MerchantID);
                    HttpContext.Session.SetString("UserId", item.UserId.ToLower());
                    HttpContext.Session.SetString("Today", item.Today);
                    HttpContext.Session.SetString("UserRole", item.UserRole);
                    HttpContext.Session.SetString("BreadCrumbsController", item.BreadCrumbsController == null ? "" : item.BreadCrumbsController);
                    HttpContext.Session.SetString("BreadCrumbsAction", item.BreadCrumbsAction == null ? "" : item.BreadCrumbsAction);
                    HttpContext.Session.SetString("CurrentAction", item.CurrentAction == null ? "" : item.CurrentAction);
                    HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", item.BreadCrumbsPerviousMenuName == null ? "" : item.BreadCrumbsPerviousMenuName);
                    HttpContext.Session.SetString("IpAddress", item.IpAddress);
                }
            }

            if (SessionMenuModel.menuList.Count == 0 || !SessionMenuModel.menuList.Any(a => a.UserID.ToLower() == userId.ToLower()))
            {
                var menuDetails = new MenuRequestModel
                {
                    UserId = userId.ToLower(),
                    UserAgent = CommonBase.useragent,
                    UserIp = HttpContext.Session.GetString("IpAddress"),
                    UserType = HttpContext.Session.GetString("UserRole")
                };

                using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    StringContent content = new StringContent(JsonConvert.SerializeObject(menuDetails), Encoding.UTF8, "application/json");

                    using (var Response = await client.PostAsync(WebApiUrl.getMenu, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var jarr = obj["Data"].Value<JArray>();
                            List<MenuDetailModel> menu = jarr.ToObject<List<MenuDetailModel>>();
                            SessionMenuModel.menuList.AddRange(menu);
                            return PartialView("~/Views/Shared/TopMenu.cshtml", SessionMenuModel.menuList.Where(x => x.UserID == HttpContext.Session.GetString("UserId")));
                        }
                        else
                        {
                            return PartialView("~/Views/Shared/TopMenu.cshtml", null);
                        }
                    }
                }
            }
            else
            {
                return PartialView("~/Views/Shared/TopMenu.cshtml", SessionMenuModel.menuList.Where(x => x.UserID == HttpContext.Session.GetString("UserId")));
            }
        }

        [HttpPost]
        public async Task<JsonResult> SetSessionItems([FromBody] string userId)
        {

            if (SessionMenuModel.sessionList.Count == 1)
            {
                HttpContext.Session.SetString("Token", SessionMenuModel.sessionList[0].Token);
                HttpContext.Session.SetString("LocalStorage", SessionMenuModel.sessionList[0].LocalStorage);
                HttpContext.Session.SetString("UserName", SessionMenuModel.sessionList[0].UserName);
                HttpContext.Session.SetString("LoginType", SessionMenuModel.sessionList[0].LoginType);
                HttpContext.Session.SetString("RegionalId", SessionMenuModel.sessionList[0].RegionalId);
                HttpContext.Session.SetString("ZonalId", SessionMenuModel.sessionList[0].ZonalId);
                HttpContext.Session.SetString("MerchantID", SessionMenuModel.sessionList[0].MerchantID);
                HttpContext.Session.SetString("UserId", SessionMenuModel.sessionList[0].UserId.ToLower());
                HttpContext.Session.SetString("Today", SessionMenuModel.sessionList[0].Today);
                HttpContext.Session.SetString("UserRole", SessionMenuModel.sessionList[0].UserRole);
                HttpContext.Session.SetString("BreadCrumbsController", SessionMenuModel.sessionList[0].BreadCrumbsController == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsController);
                HttpContext.Session.SetString("BreadCrumbsAction", SessionMenuModel.sessionList[0].BreadCrumbsAction == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsAction);
                HttpContext.Session.SetString("CurrentAction", SessionMenuModel.sessionList[0].CurrentAction == null ? "" : SessionMenuModel.sessionList[0].CurrentAction);
                HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName);
                HttpContext.Session.SetString("IpAddress", SessionMenuModel.sessionList[0].IpAddress);

                return Json("Success");
            }
            else
            {
                foreach (var item in SessionMenuModel.sessionList.Where(x => x.UserId.ToLower() == userId.ToLower()))
                {
                    HttpContext.Session.SetString("Token", item.Token);
                    HttpContext.Session.SetString("LocalStorage", item.LocalStorage);
                    HttpContext.Session.SetString("UserName", item.UserName);
                    HttpContext.Session.SetString("LoginType", item.LoginType);
                    HttpContext.Session.SetString("RegionalId", item.RegionalId);
                    HttpContext.Session.SetString("ZonalId", item.ZonalId);
                    HttpContext.Session.SetString("MerchantID", item.MerchantID);
                    HttpContext.Session.SetString("UserId", item.UserId.ToLower());
                    HttpContext.Session.SetString("Today", item.Today);
                    HttpContext.Session.SetString("UserRole", item.UserRole);
                    HttpContext.Session.SetString("BreadCrumbsController", item.BreadCrumbsController == null ? "" : item.BreadCrumbsController);
                    HttpContext.Session.SetString("BreadCrumbsAction", item.BreadCrumbsAction == null ? "" : item.BreadCrumbsAction);
                    HttpContext.Session.SetString("CurrentAction", item.CurrentAction == null ? "" : item.CurrentAction);
                    HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", item.BreadCrumbsPerviousMenuName == null ? "" : item.BreadCrumbsPerviousMenuName);
                    HttpContext.Session.SetString("IpAddress", item.IpAddress);
                }
                return Json("Success");
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetIpAddress()
        {
            using (WebClient client = new WebClient())
            {
                string jsonData = client.DownloadString("https://api.ipify.org/?format=json");
                IpAddressResponseModel results = JsonConvert.DeserializeObject<IpAddressResponseModel>(jsonData);
                string externalIp = results.ip;
                return Json(externalIp);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
