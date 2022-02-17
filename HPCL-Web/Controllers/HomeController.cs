using HPCL_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using HPCL_Web.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using HPCL_Web.Models.Login;

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

        [HttpPost]
        public async Task<JsonResult> Index(UserInfoModel user)
        {
            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token", access_token.Result);
            }
            
            var loginBody = new UserInfoModel
            {
                UserId = user.UserId,
                Useragent = Common.useragent,
                Userip = Common.userip,
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
                        }
                        else if (loginRes[0].Status == 1)
                        {
                            HttpContext.Session.SetString("UserName", loginRes[0].UserName);
                            HttpContext.Session.SetString("LoginType", loginRes[0].LoginType);
                            HttpContext.Session.SetString("UserId", loginRes[0].UserId);
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
