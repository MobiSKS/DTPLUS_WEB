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
                HttpContext.Session.SetString("Token", access_token.Result);
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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

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
                            HttpContext.Session.Remove("Token");
                            ViewBag.Message = loginRes[0].Reason;
                            return View(user);
                        }
                        else if (loginRes[0].Status == 1)
                        {
                            Random rnd = new Random();
                            int num = rnd.Next();
                            HttpContext.Session.SetString("LocalStorage", num.ToString());
                            HttpContext.Session.SetString("UserName", loginRes[0].UserName);
                            HttpContext.Session.SetString("LoginType", loginRes[0].LoginType);
                            HttpContext.Session.SetString("RegionalId", string.IsNullOrEmpty(loginRes[0].RegionalOfficeID) ? "": loginRes[0].RegionalOfficeID);
                            if (loginRes[0].LoginType == "Merchant")
                            {
                                HttpContext.Session.SetString("MerchantID", loginRes[0].UserId);
                            }
                            HttpContext.Session.SetString("UserId", loginRes[0].UserId);
                            HttpContext.Session.SetString("Today", DateTime.Now.ToString("yyyy-MM-dd"));
                            return RedirectToAction("Profile");
                        }

                        ModelState.Clear();
                        return Json(new { loginRes = loginRes });
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
