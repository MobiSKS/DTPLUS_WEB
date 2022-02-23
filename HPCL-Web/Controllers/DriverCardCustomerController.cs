using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPCL.Common.Models.DriverCardCustomer;
using HPCL.Common.Helper;
using HPCL.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
//using HPCL.Common.Models.ViewModel.Officer;
using System.Text.Json;

namespace HPCL_Web.Controllers
{
    public class DriverCardCustomerController : Controller
    {
        HelperAPI _api = new HelperAPI();
        public async Task<IActionResult> CreateDriverCardCustomer()
        {
            return View();
        }

        public async Task<IActionResult> RequestForDriverCard()
        {

            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token", access_token.Result);
            }


            char flag = 'N';

            RequestForDriverCardModel custMdl = new RequestForDriverCardModel();

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                //Fetching Customer Region
                var CustomerRegion = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", HttpContext.Session.GetString("UserName")},
                    {"ZoneID",  "0" }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerRegion), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getLocationRegion, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<RegionModel> lst = jarr.ToObject<List<RegionModel>>();
                        custMdl.RegionMdl.AddRange(lst);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }


                if (flag == 'Y')
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Error Loading Customer Type");
                    ViewBag.Login = "1";
                    return View("Index");
                }
                else
                {
                    return View(custMdl);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> RequestForDriverCard(RequestForDriverCardModel requestForDriverCardModel)
        {
           
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var driversRequestData = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", HttpContext.Session.GetString("UserName")},
                    {"RegionalId", requestForDriverCardModel.CustomerRegionID.ToString()},
                    {"NoofCards", requestForDriverCardModel.NoofCards.ToString()},
                    {"CreatedBy", HttpContext.Session.GetString("UserName")}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(driversRequestData), Encoding.UTF8, "application/json");

                CustomerResponse customerResponse;

                using (var Response = await client.PostAsync(WebApiUrl.insertDriverCardRequest, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        using (HttpContent contentUrl = Response.Content)
                        {
                            var contentString = contentUrl.ReadAsStringAsync().Result;
                            customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(contentString);

                            if (customerResponse.Internel_Status_Code == 1000)
                            {
                                requestForDriverCardModel.Remarks = "";
                                ViewBag.Message = "Driver Card add request saved successfully";
                                return RedirectToAction("SuccessRequestForDriverCard");
                            }
                            else
                            {
                                if (customerResponse.Data != null)
                                    requestForDriverCardModel.Remarks = customerResponse.Data[0].Reason;
                                else
                                    requestForDriverCardModel.Remarks = customerResponse.Message;


                                var CustRegion = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", HttpContext.Session.GetString("UserName")},
                                    {"ZoneID",  "0" }
                                };

                                StringContent contentCustomerRegion = new StringContent(JsonConvert.SerializeObject(CustRegion), Encoding.UTF8, "application/json");
                                                                
                                using (var CustomerRegionResponse = await client.PostAsync(WebApiUrl.getLocationRegion, contentCustomerRegion))
                                {
                                    if (CustomerRegionResponse.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var ResponseContent = CustomerRegionResponse.Content.ReadAsStringAsync().Result;

                                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                                        var jarr = obj["Data"].Value<JArray>();
                                        List<RegionModel> lst = jarr.ToObject<List<RegionModel>>();
                                        requestForDriverCardModel.RegionMdl.AddRange(lst);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                                    }
                                }

                                ViewBag.Message = customerResponse.Message;
                                ViewBag.NotFoundError = "Not Found";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

            }

            return View(requestForDriverCardModel);
        }


        public async Task<IActionResult> SuccessRequestForDriverCard()
        {
            return View();
        }
    }
}