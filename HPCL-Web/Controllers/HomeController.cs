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

        public async Task<IActionResult> Index()
        {
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
                var access_token = _api.GetToken();

                if (access_token.Result != null)
                {
                    HttpContext.Session.SetString("Token", access_token.Result);
                }
                else
                {
                    HttpContext.Session.SetString("Token", "");
                }

                var forms = new Dictionary<string, string>
                {
                    { "mobileno", user.MobileNo},
                    { "username", user.Username},
                    { "password", user.Password},
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid},
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getuserlogin, content))
                {
                    //if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    //{ 
                        TempData["Profile"] = JsonConvert.SerializeObject(user);
                        return RedirectToAction("Profile");
                    //}
                    //else
                    //{
                    //    ModelState.Clear();
                    //    ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                    //    ViewBag.Login = "1";
                    //    return View("Index");
                    //}
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
