using HPCL_Web.Helper;
using HPCL_Web.Models.Locations;
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
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class LocationsController : Controller
    {
        HelperAPI _api = new HelperAPI();

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> HeadOfficeDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> HeadOfficeDetails(HeadOfficeDetails headOfficeDetails)
        {
            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token", access_token.Result);
            }

            var forms = new Dictionary<string, string>
            {
                {"useragent", Common.useragent},
                {"userip", Common.userip},
                {"userid", HttpContext.Session.GetString("UserName")},
            };

            List<HeadOfficeDetailsResponse> lst = new List<HeadOfficeDetailsResponse>();

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getLocationHq, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        lst = jarr.ToObject<List<HeadOfficeDetailsResponse>>();                       
                    }
                }

                var checkHqCode = lst.Where(x => x.HQCode == headOfficeDetails.HQCode);

                var checkHqName = lst.Where(x => x.HQCode == headOfficeDetails.HQCode);

                if (checkHqCode.Count() == 0 || checkHqName.Count() == 0)
                {
                    var createResponseBody = new HeadOfficeDetails
                    {
                        HQCode = headOfficeDetails.HQCode,
                        HQName = headOfficeDetails.HQName,
                        HQShortName = headOfficeDetails.HQShortName,
                        CreatedBy = HttpContext.Session.GetString("UserName"),
                        UserId = HttpContext.Session.GetString("UserName"),
                        UserAgent = Common.useragent,
                        UserIp = Common.userip
                    };

                    StringContent updateContent = new StringContent(JsonConvert.SerializeObject(createResponseBody), Encoding.UTF8, "application/json");

                    using (var Response = await client.PostAsync(WebApiUrl.createHeadOffice, updateContent))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

                            if (obj["Message"].Value<string>() == "Success")
                            {
                                var jarr = obj["Data"].Value<JArray>();
                                List<InsertHeadOfficeDetailsResponse> insertResponse = jarr.ToObject<List<InsertHeadOfficeDetailsResponse>>();
                                ModelState.Clear();
                                return Json(insertResponse[0].Reason);
                            }
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Error Loading Location Details");
                            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                        }
                    }
                }
                else
                {
                    int hqId = lst.Select(x => x.HQID).FirstOrDefault();

                    var requestBody = new UpdateHeadOfficeDetails
                    {
                        HQID= hqId,
                        HQCode= headOfficeDetails.HQCode,
                        HQName=headOfficeDetails.HQName,
                        HQShortName=headOfficeDetails.HQShortName,
                        ModifiedBy= HttpContext.Session.GetString("UserName"),
                        UserId=HttpContext.Session.GetString("UserName"),
                        UserAgent=Common.useragent,
                        UserIp=Common.userip
                    };

                    StringContent updateContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                    using (var Response = await client.PostAsync(WebApiUrl.updateHeadOffice, updateContent))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

                            if (obj["Message"].Value<string>() == "Success")
                            {
                                var jarr = obj["Data"].Value<JArray>();
                                List<UpdateHeadOfficeDetailsResponse> updateResponse = jarr.ToObject<List<UpdateHeadOfficeDetailsResponse>>();
                                ModelState.Clear();
                                return Json(updateResponse[0].Reason);
                            }
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Error Loading Location Details");
                            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                        }
                    }
                }
                ModelState.Clear();
                return Json("Isuue with the request");
            }
        }
    }
}
