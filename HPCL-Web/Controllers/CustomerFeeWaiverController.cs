using HPCL.Common.Models.ViewModel.CustomerFeeWaiver;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CustomerFeeWaiverController : Controller
    {
        //private readonly ICustomerFeeWaiverServices _customerFeeWaiverServices;
        //public CustomerFeeWaiverController(ICustomerFeeWaiverServices customerFeeWaiverServices)
        //{
        //    _customerFeeWaiverServices = customerFeeWaiverServices;
        //}

        //public async Task<IActionResult> FeeWaiver()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<JsonResult> FeeWaiver(PendingCustomer entity)
        //{
        //    var pendingList = await _customerFeeWaiverServices.FeeWaiver(entity);

        //    ModelState.Clear();
        //    return Json(new { pendingList = pendingList });
        //}

        //[HttpPost]
        //public async Task<JsonResult> BindPendingCustomer(string customerReferenceNo)
        //{
        //    var tuple = await _customerFeeWaiverServices.BindPendingCustomer(customerReferenceNo);
        //    var basicDetails = tuple.Item1;
        //    var cardDetails = tuple.Item2;

        //    ModelState.Clear();
        //    return Json(new { basicDetails = basicDetails, cardDetails = cardDetails });
        //}

        //[HttpPost]
        //public async Task<JsonResult> ApproveCustomer(string CustomerReferenceNo, string comments)
        //{
        //    var reason = await _customerFeeWaiverServices.ApproveCustomer(CustomerReferenceNo, comments);
        //    ModelState.Clear();
        //    return Json(reason);
        //}

        //[HttpPost]
        //public async Task<JsonResult> RejectCustomer(string CustomerReferenceNo, string comments)
        //{
        //    var reason = await _customerFeeWaiverServices.RejectCustomer(CustomerReferenceNo, comments);
        //    ModelState.Clear();
        //    return Json(reason);
        //}


        //public async Task<IActionResult> ViewCustomer(string formNumber)
        //{
        //    HttpContext.Session.SetString("formNumber", formNumber);

        //    return PartialView();
        //}


        //[HttpPost]
        //public async Task<JsonResult> ViewCustomerDetails(string formNumber)
        //{
        //    //var bigNumber = BigInteger.Parse(formNumber);

        //    var customerBody = new BindPendingCustomer
        //    {
        //        UserAgent = Common.useragent,
        //        UserIp = Common.userip,
        //        UserId = HttpContext.Session.GetString("UserName"),
        //        FormNumber = BigInteger.Parse(formNumber)
        //    };

        //    using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

        //        StringContent content = new StringContent(JsonConvert.SerializeObject(customerBody), Encoding.UTF8, "application/json");

        //        using (var Response = await client.PostAsync(WebApiUrl.getPendingCustUrl, content))
        //        {
        //            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                var ResponseContent = Response.Content.ReadAsStringAsync().Result;

        //                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

        //                var searchRes = obj["Data"].Value<JObject>();

        //                var cardResult = searchRes["GetCustomerDetails"].Value<JArray>();
        //                var customerKYCDetailsResult = searchRes["CustomerKYCDetails"].Value<JArray>();

        //                List<CustomerFullDetails> customerList = cardResult.ToObject<List<CustomerFullDetails>>();

        //                List<UploadDocResponseBody> UploadDocList = customerKYCDetailsResult.ToObject<List<UploadDocResponseBody>>();

        //                CustomerFullDetails Customer = customerList.Where(t => t.FormNumber == formNumber).FirstOrDefault();

        //                ModelState.Clear();
        //                return Json(new { customer = Customer, kycDetailsResult = UploadDocList });
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
