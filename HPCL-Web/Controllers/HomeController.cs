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


namespace HPCL_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HelperAPI _api = new HelperAPI();
        Common _common = new Common();

        WebApiUrl _apiUrl = new WebApiUrl();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //var access_token = _api.GetToken();
            //string result = access_token.Result;
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserInfoModel user)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
               var forms = new Dictionary<string, string>
               {
                    { "mobileno", user.MobileNo},
                    { "username", user.Username},
                    { "password", user.Password},
                    { "useragent", _common.useragent},
                    { "userip", _common.userip},
                    { "userid", _common.userid},
               };
                client.DefaultRequestHeaders.Add("Secret_Key", _common.Secret_Key);
                client.DefaultRequestHeaders.Add("API_Key", _common.Api_Key);
                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(_apiUrl.getuserlogin, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["Profile"] = JsonConvert.SerializeObject(user);
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                        return View();
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
