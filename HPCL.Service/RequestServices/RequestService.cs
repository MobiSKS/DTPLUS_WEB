using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using HPCL.Common.Helper;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HPCL.Service
{
    public class RequestService : IRequestService
    {
        HelperAPI _api = new HelperAPI();
        private readonly IHttpContextAccessor HttpContextAccessor;
        public RequestService(IHttpContextAccessor HttpContextAccessors)
        {
            HttpContextAccessor = HttpContextAccessors;
        }
        public async Task<string> CommonRequestService(StringContent content, string requestUrl)
        {
            //Start:
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContextAccessor.HttpContext.Session.GetString("Token"));

                using (var Response = await client.PostAsync(requestUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = await Response.Content.ReadAsStringAsync();
                        JObject respObj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        string respMessage = respObj["Message"].ToString();

                        //if (respMessage != "Success")
                        //{
                        //    ResponseContent = null;
                        //    //var access_token = _api.GetToken();
                        //    //if (access_token.Result != null)
                        //    //{
                        //    //    HttpContextAccessor.HttpContext.Session.SetString("Token", access_token.Result);
                        //    //}
                        //    //else
                        //    //{
                        //    //    goto Start;
                        //    //}
                        //}
                        return ResponseContent;
                    }
                    else
                    {
                        throw new ArgumentException("Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString());
                    }
                }
            }
        }

        public async Task<string> PANValidationService(StringContent content, string requestUrl)
        {
            using (HttpClient client = new HelperAPI().GetApiPANUrlString())
            {
                using (var Response = await client.PostAsync(requestUrl, content))
                {
                    //if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = await Response.Content.ReadAsStringAsync();

                        return ResponseContent;
                    }
                    //else
                    //{
                    //    throw new ArgumentException("Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString());
                    //}
                }
            }
        }

        public async Task<string> VehicleRegistrationValidCheckService(StringContent content, string requestUrl)
        {
            using (HttpClient client = new HelperAPI().GetApiVehicleRegistrationUrlString())
            {
                using (var Response = await client.PostAsync(requestUrl, content))
                {
                    //if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = await Response.Content.ReadAsStringAsync();

                        return ResponseContent;
                    }
                    //else
                    //{
                    //    throw new ArgumentException("Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString());
                    //}
                }
            }
        }

        public async Task<string> FormDataRequestService(MultipartFormDataContent content, string requestUrl)
        {
        //Start:
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContextAccessor.HttpContext.Session.GetString("Token"));

                using (var Response = await client.PostAsync(requestUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = await Response.Content.ReadAsStringAsync();
                        JObject respObj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        string respMessage = respObj["Message"].ToString();
                        //if (respMessage != "Success")
                        //{
                        //    ResponseContent = null;
                        //    //var access_token = _api.GetToken();
                        //    //if (access_token.Result != null)
                        //    //{
                        //    //    HttpContextAccessor.HttpContext.Session.SetString("Token", access_token.Result);
                        //    //}
                        //    //else
                        //    //{
                        //    //    goto Start;
                        //    //}
                        //}
                        return ResponseContent;
                    }
                    else
                    {
                        throw new ArgumentException("Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString());
                    }
                }
            }
        }


        public async Task<string> RechargeRequestService(StringContent content, string requestUrl, string useriprecharge)
        {
            var access_token = GetTokenRec(useriprecharge);

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.Result);

                using (var Response = await client.PostAsync(requestUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = await Response.Content.ReadAsStringAsync();
                        JObject respObj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        string respMessage = respObj["Message"].ToString();

                        return ResponseContent;
                    }
                    else
                    {
                        throw new ArgumentException("Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString());
                    }
                }
            }
        }

        Token _token = new Token();
        public async Task<string> GetTokenRec(string useriprecharge)
        {
            using (HttpClient _customclient = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                   {"useragent", CommonBase.useragent},
                   {"userip", useriprecharge},
                   {"userid", "demo"},
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var tokenResponse = await _customclient.PostAsync(WebApiUrl.generatetoken, content))
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
}
