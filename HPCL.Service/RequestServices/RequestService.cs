using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using HPCL.Common.Helper;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            Start:
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

                        if (respMessage != "Success")
                        {
                            var access_token = _api.GetToken();
                            if (access_token.Result != null)
                            {
                                HttpContextAccessor.HttpContext.Session.SetString("Token", access_token.Result);
                                goto Start;
                            }
                        }
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
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = await Response.Content.ReadAsStringAsync();

                        return ResponseContent;
                    }
                    else
                    {
                        throw new ArgumentException("Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString());
                    }
                }
            }
        }

        public async Task<string> VehicleRegistrationValidCheckService(StringContent content, string requestUrl)
        {
            using (HttpClient client = new HelperAPI().GetApiVehicleRegistrationUrlString())
            {
                using (var Response = await client.PostAsync(requestUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = await Response.Content.ReadAsStringAsync();

                        return ResponseContent;
                    }
                    else
                    {
                        throw new ArgumentException("Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString());
                    }
                }
            }
        }

        public async Task<string> FormDataRequestService(MultipartFormDataContent content, string requestUrl)
        {
        Start:
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
                        //    var access_token = _api.GetToken();
                        //    if (access_token.Result != null)
                        //    {
                        //        HttpContextAccessor.HttpContext.Session.SetString("Token", access_token.Result);
                        //        goto Start;
                        //    }
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
    }
}
