using HPCL_Web.Helper;
using HPCL_Web.Models.Security;
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
    public class SecurityController : Controller
    {
        public async Task<IActionResult> UserCreationApproval()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UserCreationApproval(BindGrid entity)
        {
            var bindDetails = new BindGrid
            {
                UserId = HttpContext.Session.GetString("UserName"),
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                FirstName = entity.FirstName,
                UserName=entity.UserName,
                StatusId=entity.StatusId
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(bindDetails), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.BindRbeDetailsUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<BindGridResponse> rbeDetails = jarr.ToObject<List<BindGridResponse>>();
                        ModelState.Clear();
                        return Json(new { rbeDetails = rbeDetails });
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
        public async Task<JsonResult> UserCreationApprovalList()
        {
            var bindDetails = new BindGrid
            {
                UserId = HttpContext.Session.GetString("UserName"),
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                FirstName = "",
                UserName="",
                StatusId=""
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(bindDetails), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.BindRbeDetailsUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<BindGridResponse> rbeDetailsAll = jarr.ToObject<List<BindGridResponse>>();
                        ModelState.Clear();
                        return Json(new { rbeDetailsAll = rbeDetailsAll });
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
        public async Task<JsonResult> ViewRbeDetails(string userName)
        {
            var bindDetails = new ViewRbeDetails
            {
                UserId = HttpContext.Session.GetString("UserName"),
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                UserName = userName
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(bindDetails), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.ViewRbeDataUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<ViewRbeDetailsResponse> viewRbeDetailsList = jarr.ToObject<List<ViewRbeDetailsResponse>>();
                        ModelState.Clear();
                        return Json(new { viewRbeDetailsList = viewRbeDetailsList });
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
        public async Task<JsonResult> ApproveRbeUser(string userName, string comments)
        {
            var forms = new ApproveRejectRbeUser
            {
                UserAgent = Common.useragent,
                UserIp= Common.userip,
                UserId= HttpContext.Session.GetString("UserName"),
                UserName = userName,
                Approvalstatus = 4,
                Comments = comments,
                ApprovedBy = HttpContext.Session.GetString("UserName")
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.ApproveRejectRbeUserUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();

                        List<ApproveRejectRbeUserResponse> responseMsg = jarr.ToObject<List<ApproveRejectRbeUserResponse>>();

                        ModelState.Clear();
                        return Json(responseMsg[0].Reason);
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
        public async Task<JsonResult> RejectRbeUser(string userName, string comments)
        {
            var forms = new ApproveRejectRbeUser
            {
                UserAgent = Common.useragent,
                UserIp= Common.userip,
                UserId= HttpContext.Session.GetString("UserName"),
                UserName = userName,
                Approvalstatus = 13,
                Comments = comments,
                ApprovedBy = HttpContext.Session.GetString("UserName")
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.ApproveRejectRbeUserUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<ApproveRejectRbeUserResponse> responseMsg = jarr.ToObject<List<ApproveRejectRbeUserResponse>>();

                        ModelState.Clear();
                        return Json(responseMsg[0].Reason);
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
