using HPCL_Web.Helper;
using HPCL_Web.Models.Cards.ActivateReActivate;
using HPCL_Web.Models.Cards.ManageCards;
using HPCL_Web.Models.ViewCard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class ViewCardController : Controller
    {
        public IActionResult ViewCardSearch()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> ViewCardSearch(ViewCardDetails entity)
        {
            var searchBody = new ViewCardDetails
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CustomerId = entity.CustomerId,

            };





            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.ViewCardLimitsUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)

                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        var jsonSerializerOptions = new JsonSerializerOptions()
                        {
                            IgnoreNullValues = true
                        };
                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<SearchGridResponse> searchList = jarr.ToObject<List<SearchGridResponse>>();
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
        public IActionResult AddOrEditMobile()
        {
            return View();
        }


        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Mapping()
        {
            return View();
        }
    }
}
