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

//using System.Net.Http.Formatting;

namespace HPCL_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HelperAPI _api;
        HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            //_client = new HelperAPI().GetApiBaseUrlString();

            _client = new HttpClient();

            Token token = new Token();

            var api_key = "3C25F265-F86D-419D-9A04-EA74A503C197";

            var forms = new Dictionary<string, string>
               {
                   {"useragent", "web"},
                   {"userip", "1"},
                   {"userid", "1"},
               };

            //HttpRequestMessage request = new HttpRequestMessage();
            //request.RequestUri = new Uri("");
            //request.Method = HttpMethod.Get;
            //request.Headers.Add("Secret_Key", "PVmMSclp834KBIUa9O-XxpBsDJhsi1dsds74CiGaoo5");
            //request.Headers.Add("API_Key", api_key);

            //HttpRequestMessage response = await _client.SendAsync(request,,);

            //var headers = new Dictionary<string, string>
            //   {
            //       {"Content-Type", "application/json"},
            //       {"Secret_Key", "PVmMSclp834KBIUa9O-XxpBsDJhsi1dsds74CiGaoo5"},
            //       {"API_Key", api_key},
            //   };

            _client.BaseAddress = new Uri("http://180.179.222.161/dtp/api/dtplus/generatetoken");

           // _client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _client.DefaultRequestHeaders.Add("Secret_Key", "PVmMSclp834KBIUa9O-XxpBsDJhsi1dsds74CiGaoo5");
            _client.DefaultRequestHeaders.Add("API_Key", api_key);


            var tokenResponse = _client.PostAsync(_client.BaseAddress, new FormUrlEncodedContent(forms)).Result;

            if(tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Token>(JsonContent);
            }

            //var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;

            //if (string.IsNullOrEmpty(token.Error))
            //{
            //    Console.WriteLine("Token issued is: {0}", token.AccessToken);
            //}
            //else
            //{
            //    Console.WriteLine("Error : {0}", token.Error);
            //}

            //HttpResponseMessage res = await _client.GetAsync("api/dtplus/login/get_user_login");

            //if (res.IsSuccessStatusCode)
            //{
            //    var result = res.Content.ReadAsStringAsync().Result;
            //    // var data = JsonConvert.DeserializeObject<List<>>(result);
            //}
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
