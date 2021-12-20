using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HPCL_Web.Helper
{
    public class HelperAPI : ConfigurationBase
    {
        private readonly IConfiguration Configuration;
        internal HttpClient _client;

        private string apiBaseurl = "apiBaseurl";
        private string AuthConnectionKey = "onionAuthConnection";
        public HttpClient GetApiBaseUrlString()
        {
            // return GetAPIBaseUrl().GetConnectionString(apiBaseurl);

            var ApiUrl = GetAPIBaseUrl().GetConnectionString(apiBaseurl); 
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ApiUrl);
            return _client;

        }

        public string GetAuthConnectionString()
        {
            return GetAPIBaseUrl().GetConnectionString(AuthConnectionKey);
        }

        //public HelperAPI(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public HttpClient Initial()
        //{
        //    var ApiUrl = Configuration["apiBaseurl"];
        //    _client = new HttpClient();
        //    _client.BaseAddress = new Uri(ApiUrl);
        //    return _client;
        //}
    }
}
