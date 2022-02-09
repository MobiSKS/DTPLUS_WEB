using HPCL_Web.Helper;
using HPCL_Web.Models.Cards.ActivateReActivate;
using HPCL_Web.Models.Cards.ManageCards;
using HPCL_Web.Models.Cards.SetCardLimit;
using HPCL_Web.Models.Cards.SetCcmsLimit;
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
    public class CardsController : Controller
    {
        public async Task<IActionResult> ManageCards()
        {
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

                //using (var Response = await client.PostAsync(WebApiUrl.GetLimitTypeUrl, content))
                //{
                //    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                //        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                //        var jarr = obj["Data"].Value<JArray>();
                //        List<LimitTypeModal> limitList = jarr.ToObject<List<LimitTypeModal>>();

                //        modals.LimitTypeModals.AddRange(limitList);

                //    }
                //    else
                //    {
                //        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                //    }
                //}
            }
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> ManageCards(CustomerCards entity)
        {
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

                        //var cardRemainingResult = searchRes["CardReminingLimt"].Value<JArray>();
                        //var ccmsRemainingResult = searchRes["CardReminingCCMSLimt"].Value<JArray>();

                        List<SearchCardResult> cardDetailsList = cardResult.ToObject<List<SearchCardResult>>();
                        List<LimitResponse> limitDetailsList = limitResult.ToObject<List<LimitResponse>>();
                        List<ServicesResponse> servicesDetailsList = serviceResult.ToObject<List<ServicesResponse>>();

                        //List<CardReminingLimt> cardRemaining = cardRemainingResult.ToObject<List<CardReminingLimt>>();
                        //List<CardReminingCCMSLimt> ccmsRemaining = ccmsRemainingResult.ToObject<List<CardReminingCCMSLimt>>();

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
                            //cardRemaining = cardRemaining,
                            //ccmsRemaining = ccmsRemaining
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

        public async Task<IActionResult> AcDcCardSearch()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AcDcCardSearch(SearchCards entity)
        {
            HttpContext.Session.SetString("AcDcCustomerId", entity.CustomerId);

            var searchBody = new SearchCards
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CustomerId = entity.CustomerId,
                CardNo = entity.CardNo,
                MobileNo = entity.MobileNo
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.GetAllCardStatusUrl, content))
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
                        List<SearchCardsResponse> searchList = jarr.ToObject<List<SearchCardsResponse>>();
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
        public async Task<JsonResult> UpdateStatus(string cardNo, int Statusflag)
        {
            var updateServiceBody = new UpdateStatus
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CardNo = cardNo,
                Statusflag = Statusflag,
                ModifiedBy = Common.userid
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.UpdateCardStatusUrl, content))
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

        [HttpPost]
        public async Task<JsonResult> RefreshGrid()
        {
            var searchBody = new SearchCards
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CustomerId = HttpContext.Session.GetString("AcDcCustomerId"),
                CardNo = "",
                MobileNo = ""
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.GetAllCardStatusUrl, content))
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
                        List<SearchCardsResponse> searchList = jarr.ToObject<List<SearchCardsResponse>>();
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
    
        public async Task<IActionResult> SetSaleLimit()
        {
            GetCardLimit modals = new GetCardLimit();

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
                            if (item.StatusId == 4 || item.StatusId == 1)
                            {
                                lsts.Add(item);
                            }
                        }
                        modals.CardStatusList.AddRange(lsts);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                    return View(modals);
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> SetSaleLimit(GetCardLimit entity)
        {
            var searchBody = new GetCardLimit
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CustomerId = entity.CustomerId,
                CardNo = entity.CardNo,
                MobileNo = entity.MobileNo,
                Statusflag = entity.Statusflag
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.GetCardLimitUrl, content))
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
                        List<GetCardLimitResponse> searchList = jarr.ToObject<List<GetCardLimitResponse>>();
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
        public async Task<JsonResult> UpdateCards(ObjCardLimits[] limitArray)
        {
            var updateServiceBody = new UpdateCardLimit
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                objCardLimits = limitArray,
                ModifiedBy =Common.userid
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.UpdateCardLimitUrl, content))
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
    
        public async Task<IActionResult> SetCcmsLimitForAllCards()
        {
            GetCcmsLimitAll modals = new GetCcmsLimitAll();

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
                            if (item.StatusId == 4)
                            {
                                lsts.Add(item);
                            }
                        }
                        modals.CardStatusList.AddRange(lsts);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }

                var forms = new Dictionary<string, string>
                {
                    {"useragent", Common.useragent},
                    {"userip", Common.userip},
                    {"userid", Common.userid},
                };

                StringContent contentLimit = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.GetLimitTypeUrl, contentLimit))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<LimitTypeModal> limitType = jarr.ToObject<List<LimitTypeModal>>();

                        modals.LimitTypeModals.AddRange(limitType);
                    }
                    else
                    {
                        ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
                    }
                }
            }
            ModelState.Clear();

            return View(modals);
        }
    
        [HttpPost]
        public async Task<JsonResult> SearchCcmsLimitForAllCards(GetCcmsLimitAll entity)
        {
            HttpContext.Session.SetString("CCMSCustomerId", entity.CustomerId);

            var reqBody = new GetCcmsLimitAll
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CustomerId = entity.CustomerId,
                StatusFlag = entity.StatusFlag
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.SearchCcmsAllCardLimitUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<SearchCcmsLimitAllResponse> searchCcmsCard = jarr.ToObject<List<SearchCcmsLimitAllResponse>>();
                        ModelState.Clear();
                        return Json(new { searchCcmsCard = searchCcmsCard });
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
        public async Task<JsonResult> UpdateCcmsLimitAllCards(GetCcmsLimitAll entity)
        {
            var reqBody = new UpdateCcmsLimitAll
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CustomerId = HttpContext.Session.GetString("CCMSCustomerId"),
                LimitType=entity.TypeOfLimit,
                Amount = entity.CcmsLimit,
                ModifiedBy=Common.userid
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.UpdateCcmsAllCardLimitUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<UpdateMobileResponse> searchCcmsCard = jarr.ToObject<List<UpdateMobileResponse>>();
                        ModelState.Clear();
                        return Json(searchCcmsCard[0].Reason);
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
        public async Task<JsonResult> GetLimitType()
        {
            var forms = new Dictionary<string, string>
            {
                {"useragent", Common.useragent},
                {"userip", Common.userip},
                {"userid", Common.userid},
            };


            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
  
                StringContent contentLimit = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.GetLimitTypeUrl, contentLimit))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<LimitTypeModal> limitType = jarr.ToObject<List<LimitTypeModal>>();
                        var sortedtList = limitType.OrderBy(x => x.LimitId).ToList();
                        return Json(new { sortedtList = sortedtList });
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading Region Details");
                        return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
                    }
                }
            }
        }

        public async Task<IActionResult> SetCcmsForIndCards()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SetCcmsForIndCards(SetIndividualLimit entity)
        {
            var searchBody = new SetIndividualLimit
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                CustomerId = entity.CustomerId,
                CardNo = entity.CardNo ?? "",
                MobileNo = entity.MobileNo ?? "",
                VehicleNo = entity.VehicleNo ?? ""
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.SearchCcmsIndividualCardLimitUrl, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        var jsonSerializerOptions = new JsonSerializerOptions()
                        {
                            IgnoreNullValues = true
                        };
                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JObject>();

                        var gridResponse = jarr["CCMSBasicDetail"].Value<JArray>();
                        var balanceAmuntResponse = jarr["CCMSBalanceDetail"].Value<JArray>();


                        List<SearchIndividualCardsResponse> searchList = gridResponse.ToObject<List<SearchIndividualCardsResponse>>();
                        List<CCMSBalanceDetail> amounts = balanceAmuntResponse.ToObject<List<CCMSBalanceDetail>>();

                        ModelState.Clear();
                        return Json(new { searchList = searchList, amounts = amounts });
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
        public async Task<JsonResult> UpdateCcmsIndividualCard(string objCCMSLimits, string viewGirds)
        {
            HttpContext.Session.SetString("viewGrid", viewGirds);

            ObjCCMSLimits[] arrs = JsonConvert.DeserializeObject<ObjCCMSLimits[]>(objCCMSLimits);

            var searchBody = new UpdateCcmsIndLimit
            {
                UserId = Common.userid,
                UserAgent = Common.useragent,
                UserIp = Common.userip,
                ObjCCMSLimits = arrs,
                ModifiedBy = Common.userid
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.UpdateCcmsIndividualCardLimitUrl, content))
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
                        List<UpdateMobileResponse> searchList = jarr.ToObject<List<UpdateMobileResponse>>();
                        ModelState.Clear();
                        return Json(searchList[0].Reason);
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
    
        public async Task<IActionResult> ViewUpdatedGrid()
        {
            var str = HttpContext.Session.GetString("viewGrid");
            //var obj = JsonConvert.DeserializeObject<ObjCCMSLimits>(str);


            JArray obj = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<ViewGird> vGrid = obj.ToObject<List<ViewGird>>();



            ViewGird grids = new ViewGird
            {
                Cardno = vGrid[0].Cardno,
                vehicleNo = vGrid[0].vehicleNo,
                issueDate = vGrid[0].issueDate,
                expiryDate = vGrid[0].expiryDate,
                status = vGrid[0].status,
                Mobileno = vGrid[0].Mobileno,
                limitTypeText = vGrid[0].limitTypeText,
                Amount = vGrid[0].Amount
            };

            return View(grids);
        }
    }
}
