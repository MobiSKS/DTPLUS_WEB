using HPCL_Web.Helper;
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
        public async Task<IActionResult> ViewCardSearch()
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
                Customerid = entity.Customerid
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

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<ViewCardSearchResult> searchList = jarr.ToObject<List<ViewCardSearchResult>>();
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


        //  public async Task<IActionResult> EditMobile(string Customerid)
        // {
        //     HttpContext.Session.SetString("Customerids", Customerid);
        //     return View();
        //   }

        [HttpPost]
        public async Task<JsonResult> EditMobile(string Customerid)
        {
            HttpContext.Session.SetString("Customerids", Customerid);
            var forms = new Dictionary<string, string>
            {
                {"useragent", Common.useragent},
                {"userip", Common.userip},
                {"userid", Common.userid},
                {"customerId", HttpContext.Session.GetString("Customerids")}
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.ViewCardLimitsUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<ViewCardSearchResult> searchList = jarr.ToObject<List<ViewCardSearchResult>>();

                        ModelState.Clear();

                        return Json(new { searchList = searchList });
                        //return Json(new
                        //{
                        //    redirectUrl = Url.Action("EditMobile", "ViewCard", new { searchList = searchList }),
                        //    isRedirect = true
                        //});
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

        [HttpPost]
        public async Task<JsonResult> UpdateCards(ObjCardLimits[] limitArray)
        {

            var updateServiceBody = new UpdateMobile
            {

                UserId = HttpContext.Session.GetString("UserName"),
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                objCardLimits = limitArray,
                ModifiedBy = HttpContext.Session.GetString("UserName"),
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.MobileAddOrEditUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

                        var updateRes = obj["Data"].Value<JArray>();
                        List<UpdateMobileResponse> updateResponse = updateRes.ToObject<List<UpdateMobileResponse>>();

                        ModelState.Clear();
                        return Json(updateResponse[0].Reason);
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


        //[HttpPost]
        //public async Task<JsonResult> Addmobilenumber(ViewCardDetails entity)
        //{
        //    var searchBody = new ViewCardDetails
        //    {
        //        UserId = Common.userid,
        //        UserAgent = Common.useragent,
        //        UserIp = Common.userip,
        //        Customerid = entity.Customerid,
        //        CardNo = entity.CardNo,
        //        MobileNo = entity.MobileNo,
        //        FastagNo = entity.FastagNo,
        //        Statusflag = entity.Statusflag
        //    };

        //    using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

        //        StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

        //        using (var Response = await client.PostAsync(WebApiUrl.MobileAddOrEdit, content))
        //        {
        //            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                var ResponseContent = Response.Content.ReadAsStringAsync().Result;

        //                var jsonSerializerOptions = new JsonSerializerOptions()
        //                {
        //                    IgnoreNullValues = true
        //                };
        //                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
        //                var jarr = obj["Data"].Value<JArray>();
        //                List<ViewCardSearchResult> searchList = jarr.ToObject<List<ViewCardSearchResult>>();
        //                ModelState.Clear();
        //                return Json(new { searchList = searchList });
        //            }
        //            else
        //            {
        //                ModelState.Clear();
        //                ModelState.AddModelError(string.Empty, "Error Loading Location Details");
        //                return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
        //            }
        //        }
        //    }
        //}
    }
}
