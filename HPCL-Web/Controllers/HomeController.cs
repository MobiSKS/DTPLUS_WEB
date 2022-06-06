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
            SessionMenuModel.menuList.Clear();
            SessionMenuModel.sessionList.Clear();
            return View();
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

            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token_" + user.UserId, access_token.Result);
            }

            var loginBody = new UserInfoModel
            {
                UserId = user.UserId,
                Useragent = CommonBase.useragent,
                Userip = CommonBase.userip,
                Password = user.Password
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token_" + user.UserId));

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
                            HttpContext.Session.Remove("Token_" + user.UserId);
                            ViewBag.Message = loginRes[0].Reason;
                            return View(user);
                        }
                        else if (loginRes[0].Status == 1)
                        {
                            Random rnd = new Random();
                            int num = rnd.Next();

                            if (!SessionMenuModel.sessionList.Any(a => a.UserID == user.UserId))
                            {
                                List<SessionDataModelDetails> sessionData = new List<SessionDataModelDetails>
                                {
                                    new SessionDataModelDetails { UserID = user.UserId, LocalStorage = num.ToString(),
                                        UserName = loginRes[0].UserName, LoginType = loginRes[0].LoginType,
                                        RegionalId = string.IsNullOrEmpty(loginRes[0].RegionalOfficeID) ? "" : loginRes[0].RegionalOfficeID,
                                        ZonalId = string.IsNullOrEmpty(loginRes[0].ZonalOfficeID) ? "" : loginRes[0].ZonalOfficeID,
                                        MerchantID = loginRes[0].LoginType == "Merchant" ? loginRes[0].UserId : "",
                                        UserId = loginRes[0].UserId,
                                        Today = DateTime.Now.ToString("yyyy-MM-dd"),
                                        Token = access_token.Result,
                                        UserRole = loginRes[0].UserRole
                                    }
                                };

                                SessionMenuModel.sessionList.AddRange(sessionData);
                            }

                            HttpContext.Session.SetString("LocalStorage", num.ToString());
                            HttpContext.Session.SetString("UserName", loginRes[0].UserName);
                            //HttpContext.Session.SetString("LoginType", loginRes[0].LoginType);
                            //HttpContext.Session.SetString("RegionalId", string.IsNullOrEmpty(loginRes[0].RegionalOfficeID) ? "" : loginRes[0].RegionalOfficeID);
                            //if (loginRes[0].LoginType == "Merchant")
                            //{
                            //    HttpContext.Session.SetString("MerchantID", loginRes[0].UserId);
                            //}
                            HttpContext.Session.SetString("UserId", loginRes[0].UserId);
                            //HttpContext.Session.SetString("Today", DateTime.Now.ToString("yyyy-MM-dd"));

                            return RedirectToAction("Profile");
                        }
                        else
                        {
                            ModelState.Clear();
                            HttpContext.Session.Remove("Token_" + user.UserId);
                            ViewBag.Message = "Details not found for this User!";
                            return View(user);
                        }

                        //ModelState.Clear();
                        //return Json(new { loginRes = loginRes });
                    }
                    else
                    {
                        ModelState.Clear();
                        HttpContext.Session.Remove("Token_" + user.UserId);
                        ViewBag.Message = "Details not found for this User!";
                        return View(user);
                        //ModelState.Clear();
                        //ModelState.AddModelError(string.Empty, "Error Loading Location Details");
                        //return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(UserInfoModel user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Validate Captcha Code
        //        if (!Captcha.ValidateCaptchaCode(user.CaptchaCode, HttpContext))
        //        {
        //            ViewBag.CaptchaCodeStatus = "Error";
        //            return View(user);
        //        }
        //    }

        //    var access_token = _api.GetToken();

        //    if (access_token.Result != null)
        //    {
        //        HttpContext.Session.SetString("Token", access_token.Result);
        //    }

        //    var loginBody = new UserInfoModel
        //    {
        //        UserId = user.UserId,
        //        Useragent = CommonBase.useragent,
        //        Userip = CommonBase.userip,
        //        Password = user.Password
        //    };

        //    using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

        //        StringContent content = new StringContent(JsonConvert.SerializeObject(loginBody), Encoding.UTF8, "application/json");

        //        using (var Response = await client.PostAsync(WebApiUrl.getLoginUrl, content))
        //        {
        //            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                var ResponseContent = Response.Content.ReadAsStringAsync().Result;

        //                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
        //                var jarr = obj["Data"].Value<JArray>();
        //                List<LoginResponse> loginRes = jarr.ToObject<List<LoginResponse>>();

        //                if (loginRes[0].Status == 0)
        //                {
        //                    HttpContext.Session.Remove("Token");
        //                    ViewBag.Message = loginRes[0].Reason;
        //                    return View(user);
        //                }
        //                else if (loginRes[0].Status == 1)
        //                {
        //                    Random rnd = new Random();
        //                    int num = rnd.Next();
        //                    HttpContext.Session.SetString("LocalStorage", num.ToString());
        //                    HttpContext.Session.SetString("UserName", loginRes[0].UserName);
        //                    HttpContext.Session.SetString("LoginType", loginRes[0].LoginType);
        //                    HttpContext.Session.SetString("RegionalId", string.IsNullOrEmpty(loginRes[0].RegionalOfficeID) ? "": loginRes[0].RegionalOfficeID);
        //                    if (loginRes[0].LoginType == "Merchant")
        //                    {
        //                        HttpContext.Session.SetString("MerchantID", loginRes[0].UserId);
        //                    }
        //                    HttpContext.Session.SetString("UserId", loginRes[0].UserId);
        //                    HttpContext.Session.SetString("Today", DateTime.Now.ToString("yyyy-MM-dd"));
        //                    return RedirectToAction("Profile");
        //                }

        //                ModelState.Clear();
        //                return Json(new { loginRes = loginRes });
        //            }
        //            else
        //            {
        //                ModelState.Clear();
        //                ModelState.AddModelError(string.Empty, "Error Loading Location Details");
        //                return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
        //            }
        //        }
        //    }
        //}

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
                HttpContext.Session.SetString("UserId", SessionMenuModel.sessionList[0].UserId);
                HttpContext.Session.SetString("Today", SessionMenuModel.sessionList[0].Today);
                HttpContext.Session.SetString("UserRole", SessionMenuModel.sessionList[0].UserRole);
                HttpContext.Session.SetString("BreadCrumbsController", SessionMenuModel.sessionList[0].BreadCrumbsController == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsController);
                HttpContext.Session.SetString("BreadCrumbsAction", SessionMenuModel.sessionList[0].BreadCrumbsAction == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsAction);
                HttpContext.Session.SetString("CurrentAction", SessionMenuModel.sessionList[0].CurrentAction == null ? "" : SessionMenuModel.sessionList[0].CurrentAction);
                HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName);
            }
            else
            {
                foreach (var item in SessionMenuModel.sessionList.Where(x => x.UserID == userId))
                {
                    HttpContext.Session.SetString("Token", item.Token);
                    HttpContext.Session.SetString("LocalStorage", item.LocalStorage);
                    HttpContext.Session.SetString("UserName", item.UserName);
                    HttpContext.Session.SetString("LoginType", item.LoginType);
                    HttpContext.Session.SetString("RegionalId", item.RegionalId);
                    HttpContext.Session.SetString("ZonalId", item.ZonalId);
                    HttpContext.Session.SetString("MerchantID", item.MerchantID);
                    HttpContext.Session.SetString("UserId", item.UserId);
                    HttpContext.Session.SetString("Today", item.Today);
                    HttpContext.Session.SetString("UserRole", item.UserRole);
                    HttpContext.Session.SetString("BreadCrumbsController", item.BreadCrumbsController == null ? "" : item.BreadCrumbsController);
                    HttpContext.Session.SetString("BreadCrumbsAction", item.BreadCrumbsAction == null ? "" : item.BreadCrumbsAction);
                    HttpContext.Session.SetString("CurrentAction", item.CurrentAction == null ? "" : item.CurrentAction);
                    HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", item.BreadCrumbsPerviousMenuName == null ? "" : item.BreadCrumbsPerviousMenuName);
                }
            }

            if (SessionMenuModel.menuList.Count == 0 || !SessionMenuModel.menuList.Any(a => a.UserID == userId))
            {
                var menuDetails = new MenuRequestModel
                {
                    UserId = userId,
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
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
                HttpContext.Session.SetString("UserId", SessionMenuModel.sessionList[0].UserId);
                HttpContext.Session.SetString("Today", SessionMenuModel.sessionList[0].Today);
                HttpContext.Session.SetString("UserRole", SessionMenuModel.sessionList[0].UserRole);
                HttpContext.Session.SetString("BreadCrumbsController", SessionMenuModel.sessionList[0].BreadCrumbsController == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsController);
                HttpContext.Session.SetString("BreadCrumbsAction", SessionMenuModel.sessionList[0].BreadCrumbsAction == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsAction);
                HttpContext.Session.SetString("CurrentAction", SessionMenuModel.sessionList[0].CurrentAction == null ? "" : SessionMenuModel.sessionList[0].CurrentAction);
                HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName == null ? "" : SessionMenuModel.sessionList[0].BreadCrumbsPerviousMenuName);

                return Json("Success");
            }
            else
            {
                foreach (var item in SessionMenuModel.sessionList.Where(x => x.UserID == userId))
                {
                    HttpContext.Session.SetString("Token", item.Token);
                    HttpContext.Session.SetString("LocalStorage", item.LocalStorage);
                    HttpContext.Session.SetString("UserName", item.UserName);
                    HttpContext.Session.SetString("LoginType", item.LoginType);
                    HttpContext.Session.SetString("RegionalId", item.RegionalId);
                    HttpContext.Session.SetString("ZonalId", item.ZonalId);
                    HttpContext.Session.SetString("MerchantID", item.MerchantID);
                    HttpContext.Session.SetString("UserId", item.UserId);
                    HttpContext.Session.SetString("Today", item.Today);
                    HttpContext.Session.SetString("UserRole", item.UserRole);
                    HttpContext.Session.SetString("BreadCrumbsController", item.BreadCrumbsController == null ? "" : item.BreadCrumbsController);
                    HttpContext.Session.SetString("BreadCrumbsAction", item.BreadCrumbsAction == null ? "" : item.BreadCrumbsAction);
                    HttpContext.Session.SetString("CurrentAction", item.CurrentAction == null ? "" : item.CurrentAction);
                    HttpContext.Session.SetString("BreadCrumbsPerviousMenuName", item.BreadCrumbsPerviousMenuName == null ? "" : item.BreadCrumbsPerviousMenuName);
                }
                return Json("Success");
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
