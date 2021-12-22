using HPCL_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HPCL_Web.Helper;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Formatting;
using System.Text;
using HPCL.Common;

//using System.Net.Http.Formatting;

namespace HPCL_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HelperAPI _api;
        HttpClient _client;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            TokenManager objTokenManager = new TokenManager(_configuration);

            HelperAPI objHelperAPI = new HelperAPI(_configuration);
            _client = objHelperAPI.GetApiBaseUrlString();
            Token token = new Token();
            var forms = new Dictionary<string, string>
               {
                   {"useragent", objTokenManager.UserAgent},
                   {"userip", objTokenManager.Userip},
                   {"userid", objTokenManager.Userid},
               };
            var jsonformdata = JsonConvert.SerializeObject(forms);
            var tokenResponse = _client.PostAsync(_client.BaseAddress, new StringContent(jsonformdata, Encoding.UTF8, "application/json")).Result;
            if(tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Token>(JsonContent);
            }

            return View();
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

    public class Token
    {
        public string success { get; set; }
        public string message { get; set; }
        public int status_Code { get; set; }

        public string method_Name { get; set; }

        [JsonProperty("token")]
        public string token { get; set; }

        //[JsonProperty("model_State")]
        //public string model_State { get; set; }
    }
}
