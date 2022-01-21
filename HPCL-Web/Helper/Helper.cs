using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL_Web.Helper
{
    public class HelperAPI : ConfigurationBase
    {
        private readonly IConfiguration Configuration;
        private HttpClient _client;
        //Common _common = new Common();
        Token _token = new Token();


        WebApiUrl _apiUrl = new WebApiUrl();

        //private string apiBaseurl = "apiBaseurl";
        private string apiBaseurl = "TokenSettings:appBaseurl";
        private string AuthConnectionKey = "onionAuthConnection";
        public HttpClient GetApiBaseUrlString()
        {
            var ApiUrl = GetAPIBaseUrl().GetValue<string>(apiBaseurl); 
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ApiUrl);
            _client.DefaultRequestHeaders.Add("Secret_Key", Common.Secret_Key);
            _client.DefaultRequestHeaders.Add("API_Key", Common.Api_Key);
            return _client;
        }

        public string GetAuthConnectionString()
        {
            return GetAPIBaseUrl().GetConnectionString(AuthConnectionKey);
        }

        // Generate Token
        public async Task<string> GetToken()
        {
            using (HttpClient _customclient = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
               {
                   {"useragent", Common.useragent},
                   {"userip", Common.userip},
                   {"userid", Common.userid},
               };

                _customclient.DefaultRequestHeaders.Add("Secret_Key", Common.Secret_Key);
                _customclient.DefaultRequestHeaders.Add("API_Key", Common.Api_Key);

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var tokenResponse = await _customclient.PostAsync(_apiUrl.generatetoken, content))
                {
                    if (tokenResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                        _token = JsonConvert.DeserializeObject<Token>(JsonContent);
                    }
                }
            }
            return _token.token;
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

    }
}
