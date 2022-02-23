using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.Security;
using HPCL.Common.Models.ViewModel.Security;
using HPCL.Service.Interfaces;
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
        private readonly ISecurityService _securityService;
        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public async Task<IActionResult> UserCreationApproval()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UserCreationApproval(BindGrid entity)
        {
            var rbeDetails = await _securityService.UserCreationApproval(entity);

            ModelState.Clear();
            return Json(new { rbeDetails = rbeDetails });
        }

        [HttpPost]
        public async Task<JsonResult> UserCreationApprovalList()
        {
            var bindDetails = new BindGrid
            {
                UserId = HttpContext.Session.GetString("UserName"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
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
            var viewRbeDetailsList = _securityService.ViewRbeDetails(userName);

            ModelState.Clear();
            return Json(new { viewRbeDetailsList = viewRbeDetailsList });
        }

        [HttpPost]
        public async Task<JsonResult> ApproveRbeUser(string userName, string comments)
        {
            var reason = await _securityService.ApproveRbeUser(userName, comments);

            ModelState.Clear();
            return Json(reason);
        }

        [HttpPost]
        public async Task<JsonResult> RejectRbeUser(string userName, string comments)
        {
            var reason = await _securityService.ApproveRbeUser(userName, comments);

            ModelState.Clear();
            return Json(reason);
        }
    }
}
