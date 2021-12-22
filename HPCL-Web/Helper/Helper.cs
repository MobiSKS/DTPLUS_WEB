using HPCL.Common;
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
        private readonly IConfiguration _configuration;
        internal HttpClient _client;

        public HelperAPI(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        private string apiBaseurl = "apiBaseurl";
        private string AuthConnectionKey = "onionAuthConnection";
        public HttpClient GetApiBaseUrlString()
        {
            // return GetAPIBaseUrl().GetConnectionString(apiBaseurl);
            TokenManager objTokenManager  =new TokenManager(_configuration);
            var ApiUrl = objTokenManager.TokenBaseUrl;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ApiUrl);

            _client.DefaultRequestHeaders.Add("Secret_Key", objTokenManager.StrSecretKey);
            _client.DefaultRequestHeaders.Add("API_Key", objTokenManager.StrAPI_Key);
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
