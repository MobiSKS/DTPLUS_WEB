using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.Customer;
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
    public class ViewCustomerController : Controller
    {
        public async Task<IActionResult> ViewCustomerSearch()
        {
            ViewCustomerSearch ObjViewCustomer = new ViewCustomerSearch();

            var searchBody = new ViewCustomerSearch
            {
                UserId = CommonBase.userid,
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID = "",
                CustomerReferenceNo = 0,
                FormNumber = 0,
                FromDate = "",
                StatusId = 0,
                ToDate = "",
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.Viewonlineformstatus, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerViewModalOutput> searchList = jarr.ToObject<List<CustomerViewModalOutput>>();
                        ObjViewCustomer.CustomerViewModalOutputs.AddRange(searchList);
                        //return Json(new { searchList = searchList });
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Location Details");
                        //return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
            return View(ObjViewCustomer);
        }
        [HttpPost]
        public async Task<JsonResult> ViewCustomerSearch(ViewCustomerSearch entity)
        {

            var searchBody = new ViewCustomerSearch
            {
                UserId = CommonBase.userid,
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID = "",
                CustomerReferenceNo=0,
                FormNumber=0,
                FromDate="",
                StatusId=0,
                ToDate="",
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.Viewonlineformstatus, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<CustomerViewModalOutput> searchList = jarr.ToObject<List<CustomerViewModalOutput>>();
                        ModelState.Clear();
                        return Json(new { searchList = searchList });
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Location Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }
        
    }
}
