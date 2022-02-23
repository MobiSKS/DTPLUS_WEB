using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using HPCL.Common.Helper;

namespace HPCL.Service
{
    public class RequestService : IRequestService
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        public RequestService(IHttpContextAccessor HttpContextAccessors)
        {
            HttpContextAccessor = HttpContextAccessors;
        }
        public async Task<string> CommonRequestService(StringContent content, string requestUrl)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContextAccessor.HttpContext.Session.GetString("Token"));

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
    }
}
