using HPCL_Web.Helper;
using HPCL_Web.Models.ManageCards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CardsController : Controller
    {
        HelperAPI _api = new HelperAPI();

        public async Task<IActionResult> ManageCards()
        {
            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token", access_token.Result);
            }

            CustomerCards modals = new CustomerCards();

            var statusType = new StatusType
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                EntityTypeId = 3
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(statusType), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.GetStatusTypeUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<StatusModal> lst = jarr.ToObject<List<StatusModal>>();

                        List<StatusModal> lsts = new List<StatusModal>();
                        lsts.Add(new StatusModal { StatusId = -1, StatusName = "All" });
                        foreach (var item in lst)
                        {
                            if (item.StatusId == 4 || item.StatusId == 6 || item.StatusId == 7)
                            {
                                lsts.Add(item);
                            }
                        }
                        modals.StatusModals.AddRange(lsts);
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
        public async Task<JsonResult> ManageCards(CustomerCards entity)
        {
            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token", access_token.Result);
            }

            var searchBody = new CustomerCards
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CustomerId = entity.CustomerId,
                CardNo = entity.CardNo,
                MobileNumber = entity.MobileNumber,
                VehicleNumber = entity.VehicleNumber,
                StatusFlag = -1
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.SearchCardUrl, content))
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

        [HttpPost]
        public async Task<JsonResult> ViewCardDetails(string CardId)
        {
            HttpContext.Session.SetString("CardIdSession", CardId);

            var cardDetailsBody = new CardsSearch
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CardNo = CardId,
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(cardDetailsBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.GetCardDetailsUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

                        var searchRes = obj["Data"].Value<JObject>();
                        var cardResult = searchRes["GetCardsDetailsModelOutput"].Value<JArray>();
                        var limitResult = searchRes["GetCardLimtModel"].Value<JArray>();
                        var serviceResult = searchRes["CardServices"].Value<JArray>();

                        var cardRemainingResult = searchRes["CardReminingLimt"].Value<JArray>();
                        var ccmsRemainingResult = searchRes["CardReminingCCMSLimt"].Value<JArray>();

                        List<SearchCardResult> cardDetailsList = cardResult.ToObject<List<SearchCardResult>>();
                        List<LimitSearchResponse> limitDetailsList = limitResult.ToObject<List<LimitSearchResponse>>();
                        List<ServicesResponse> servicesDetailsList = serviceResult.ToObject<List<ServicesResponse>>();

                        List<CardReminingLimt> cardRemaining = cardRemainingResult.ToObject<List<CardReminingLimt>>();
                        List<CardReminingCCMSLimt> ccmsRemaining = ccmsRemainingResult.ToObject<List<CardReminingCCMSLimt>>();

                        string cusId = string.Empty;

                        foreach (var item in cardDetailsList)
                        {
                            cusId = item.CustomerID;
                        }

                        HttpContext.Session.SetString("CustomerIdSession", cusId);

                        ModelState.Clear();
                        return Json(new
                        {
                            cardDetailsList = cardDetailsList,
                            limitDetailsList = limitDetailsList,
                            servicesDetailsList = servicesDetailsList,
                            cardRemaining = cardRemaining,
                            ccmsRemaining = ccmsRemaining
                        });
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
        public async Task<JsonResult> UpdateService(string serviceId, bool flag)
        {
            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token", access_token.Result);
            }

            var updateServiceBody = new UpdateService
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CustomerId = HttpContext.Session.GetString("CustomerIdSession"),
                CardNo = HttpContext.Session.GetString("CardIdSession"),
                ServiceId = Convert.ToInt32(serviceId),
                Flag = Convert.ToInt32(flag),
                CreatedBy = "1"
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.UpdateServiceUrl, content))
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

        public async Task<IActionResult> CardlessMapping(string cardNumber, string mobileNumber)
        {
            UpdateMobileModal editMobBody = new UpdateMobileModal();
            editMobBody.CardNumber = cardNumber;
            editMobBody.MobileNumber = mobileNumber;

            return View(editMobBody);
        }

        [HttpPost]
        public async Task<JsonResult> CardlessMapping(UpdateMobileModal entity)
        {
            var access_token = _api.GetToken();

            if (access_token.Result != null)
            {
                HttpContext.Session.SetString("Token", access_token.Result);
            }

            var cardDetailsBody = new UpdateMobile
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CardNo = entity.CardNumber,
                MobileNo = entity.MobileNumber,
                ModifiedBy = "1"
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(cardDetailsBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.UpdateMobileUrl, content))
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
    }
}
