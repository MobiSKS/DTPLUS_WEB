using HPCL_Web.Helper;
using HPCL_Web.Models;
using HPCL_Web.Models.CustomerFeeWaiver;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CustomerFeeWaiverController : Controller
    {
        public async Task<IActionResult> FeeWaiver()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> FeeWaiver(PendingCustomer entity)
        {
            var custDetails = new PendingCustomer
            {
                UserId = HttpContext.Session.GetString("UserName"),
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                FormNumber = entity.FormNumber
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(custDetails), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.bindPendingCustomerUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<PendingCustResponse> pendingList = jarr.ToObject<List<PendingCustResponse>>();
                        ModelState.Clear();
                        return Json(new { pendingList = pendingList });
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
        //public async Task<JsonResult> getAllPendingCustomer()
        //{
        //    var custDetails = new PendingCustomer
        //    {
        //        UserId = HttpContext.Session.GetString("UserName"),
        //        UserAgent = Common.useragent,
        //        UserIp = Common.userip
        //    };

        //    using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

        //        StringContent content = new StringContent(JsonConvert.SerializeObject(custDetails), Encoding.UTF8, "application/json");

        //        using (var Response = await client.PostAsync(WebApiUrl.bindPendingCustomerUrl, content))
        //        {
        //            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                var ResponseContent = Response.Content.ReadAsStringAsync().Result;

        //                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
        //                var jarr = obj["Data"].Value<JArray>();
        //                List<PendingCustResponse> allPendingList = jarr.ToObject<List<PendingCustResponse>>();
        //                ModelState.Clear();
        //                return Json(new { allPendingList = allPendingList });
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

        [HttpPost]
        public async Task<JsonResult> BindPendingCustomer(string customerReferenceNo)
        {
            var forms = new Dictionary<string, string>
            {
                {"useragent", Common.useragent},
                {"userip", Common.userip},
                {"userid", HttpContext.Session.GetString("UserName")},
                {"CustomerReferenceNo", customerReferenceNo}
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getFeeWaiverDetailsUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JObject>();

                        var basicJson = jarr["GetApproveFeeWaiverBasicDetail"].Value<JArray>();
                        var cardJson = jarr["GetApproveFeeWaiverCardDetail"].Value<JArray>();

                        List<GetApproveFeeWaiverBasicDetail> basicDetails = basicJson.ToObject<List<GetApproveFeeWaiverBasicDetail>>();
                        List<GetApproveFeeWaiverCardDetail> cardDetails = cardJson.ToObject<List<GetApproveFeeWaiverCardDetail>>();

                        ModelState.Clear();
                        return Json(new { basicDetails = basicDetails, cardDetails = cardDetails });
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
        public async Task<JsonResult> ApproveCustomer(string CustomerReferenceNo, string comments)
        {
            var forms = new ApproveRejectWaiver
            {
                UserAgent = Common.useragent,
                UserIp= Common.userip,
                UserId= HttpContext.Session.GetString("UserName"),
                CustomerReferenceNo = CustomerReferenceNo,
                Approvalstatus = 4,
                Comments = comments,
                ApprovedBy = HttpContext.Session.GetString("UserName")
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.approveRejectFeeWaiverUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();

                        List<ApproveRejectResponse> responseMsg = jarr.ToObject<List<ApproveRejectResponse>>();

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
        public async Task<JsonResult> RejectCustomer(string CustomerReferenceNo, string comments)
        {
            var forms = new ApproveRejectWaiver
            {
                UserAgent = Common.useragent,
                UserIp= Common.userip,
                UserId= HttpContext.Session.GetString("UserName"),
                CustomerReferenceNo = CustomerReferenceNo,
                Approvalstatus = 13,
                Comments = comments,
                ApprovedBy = HttpContext.Session.GetString("UserName")
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.approveRejectFeeWaiverUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<ApproveRejectResponse> responseMsg = jarr.ToObject<List<ApproveRejectResponse>>();

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


        public async Task<IActionResult> ViewCustomer(string formNumber)
        {
            HttpContext.Session.SetString("formNumber", formNumber);

            return PartialView();
        }


        [HttpPost]
        public async Task<JsonResult> ViewCustomerDetails(string formNumber)
        {
            //var bigNumber = BigInteger.Parse(formNumber);

            var customerBody = new BindPendingCustomer
            {
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                UserId = HttpContext.Session.GetString("UserName"),
                FormNumber = BigInteger.Parse(formNumber)
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(customerBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getPendingCustUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

                        var searchRes = obj["Data"].Value<JObject>();

                        var cardResult = searchRes["GetCustomerDetails"].Value<JArray>();
                        var customerKYCDetailsResult = searchRes["CustomerKYCDetails"].Value<JArray>();

                        List<CustomerFullDetails> customerList = cardResult.ToObject<List<CustomerFullDetails>>();

                        List<UploadDocResponseBody> UploadDocList = customerKYCDetailsResult.ToObject<List<UploadDocResponseBody>>();

                        CustomerFullDetails Customer = customerList.Where(t => t.FormNumber == formNumber).FirstOrDefault();

                        ModelState.Clear();
                        return Json(new { customer = Customer, kycDetailsResult = UploadDocList });
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
