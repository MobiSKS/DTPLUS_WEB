using HPCL_Web.Helper;
using HPCL_Web.Models;
using HPCL_Web.Models.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CustomerController : Controller
    {
        HelperAPI _api = new HelperAPI();

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> OnlineForm()
        {
            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token", access_token.Result);
            }


            CustomerModel modals = new CustomerModel();

            var customerType = new CustomerType
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CTFlag = 1
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(customerType), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getCustomerType, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerTypeResponse> lst = jarr.ToObject<List<CustomerTypeResponse>>();
                        modals.CustomerList.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }
            }
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> OnlineForm(CustomerModel cust)
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerType()
        {
            var customerType = new CustomerType
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CTFlag = 1
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(customerType), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getLocationHq, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerTypeResponse> lst = jarr.ToObject<List<CustomerTypeResponse>>();
                        return Json(lst);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading District Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerSubType()
        {
            var customerType = new CustomerType
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CTFlag = 1
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(customerType), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getLocationHq, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerTypeResponse> lst = jarr.ToObject<List<CustomerTypeResponse>>();
                        return Json(lst);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading District Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }
    }
}
