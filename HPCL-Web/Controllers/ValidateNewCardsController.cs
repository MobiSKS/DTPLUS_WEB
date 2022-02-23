using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.Common;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Common.Models.ViewModel.ValidateNewCards;
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
    public class ValidateNewCardsController : Controller
    {
        public async Task<IActionResult> Details(ValidateNewCardsModel validateNewCardsMdl, int pg = 1)
        {
            ValidateNewCardsModel validateNewCardsModel = new ValidateNewCardsModel();

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var OfficerTypeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid}
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerTypeForms), Encoding.UTF8, "application/json");
                //Fetching Officer Type
                using (var Response = await client.PostAsync(WebApiUrl.getOfficerType, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<OfficerTypeModel> lst = jarr.ToObject<List<OfficerTypeModel>>();
                        validateNewCardsModel.OfficerTypeMdl.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load officer types";
                    }
                }

                validateNewCardsModel.FormNumber = validateNewCardsMdl.FormNumber;
                validateNewCardsModel.Date = validateNewCardsMdl.Date;
                validateNewCardsModel.CreatedBy = validateNewCardsMdl.CreatedBy;
                string Date = "";

                if (!String.IsNullOrEmpty(validateNewCardsMdl.Date))
                {
                    string[] dateArr = validateNewCardsModel.Date.Split("-");
                    Date = dateArr[2] + "-" + dateArr[1] + "-" + dateArr[0];
                }

                var ValidateNewCardListForm = new Dictionary<string, string>
                    {
                        {"Useragent", CommonBase.useragent},
                        {"Userip", CommonBase.userip},
                        {"Userid", CommonBase.userid},
                        {"StateId", "" },
                        {"FormNumber", "" },
                        {"CustomerName", "" },
                        {"Createdon", "" },
                        {"Createdby", "" }
                    };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent ValidateNewCardListContent = new StringContent(JsonConvert.SerializeObject(ValidateNewCardListForm), Encoding.UTF8, "application/json");
                using (var Response = await client.PostAsync(WebApiUrl.getValidateNewCardLists, ValidateNewCardListContent))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<NewCardsList> lst = jarr.ToObject<List<NewCardsList>>();
                        validateNewCardsModel.NewCardsLists.AddRange(lst);
                    }
                    else
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        ViewBag.Message = "Failed to load Merchant types";
                    }
                }

                const int pageSize = 5;
                if (pg < 1)
                    pg = 1;

                int resCount = validateNewCardsModel.NewCardsLists.Count();
                var pager = new PagerModel(resCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;

                var data = validateNewCardsModel.NewCardsLists.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;

                validateNewCardsModel.NewCardsLists.Clear();
                validateNewCardsModel.NewCardsLists.AddRange(data);
            }

            //if (validateNewCardsMdl.ShowTable == "Error")
            //{
            //    ViewBag.NotFoundError = "Not Found";
            //}
            return View(validateNewCardsModel);
        }
        public async Task<IActionResult> GetCardDetailsForApproval(string CustomerRefNo)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var forms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", CommonBase.userid},
                    {"CustomerReferenceNo", CustomerRefNo }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

                using (var Response = await client.PostAsync(WebApiUrl.getCardDetailsForCardApproval, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var jarr = obj["Data"].Value<JArray>();
                        List<VehicleDetailsModel> lst = jarr.ToObject<List<VehicleDetailsModel>>();
                        //lst.Add(new MerchantSalesAreaModel
                        //{
                        //    RegionID = 0,
                        //    SalesAreaID = 0,
                        //    SalesAreaName = "--Select--"
                        //});
                        //var SortedtList = lst.OrderBy(x => x.SalesAreaID).ToList();
                        return PartialView("~/Views/ValidateNewCards/_GetValidateFormDetails.cshtml", lst);
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Error Loading District Details");
                        var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                        JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                        var Message = obj["errorMessage"].ToString();
                        return Json("Failed to load District");
                    }
                }
            }
        }
        public async Task<IActionResult> ActionOnCards([FromBody] ApproveCardDetailsModel approveRejectModel)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {

                var forms = new ApproveCardDetailsModel
                {
                    UserId = CommonBase.userid,
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerReferenceNo = approveRejectModel.CustomerReferenceNo,
                    Comments = approveRejectModel.Comments,
                    Approvalstatus = approveRejectModel.Approvalstatus,
                    ApprovedBy = HttpContext.Session.GetString("UserName")
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
                try
                {
                    using (var Response = await client.PostAsync(WebApiUrl.approveRejectCard, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            string message = "";

                            if (obj["Status_Code"].ToString() == "200")
                            {
                                var jarr = obj["Data"].Value<JArray>();
                                List<ApiResponseModel> lst = jarr.ToObject<List<ApiResponseModel>>();
                                message = lst.First().Reason.ToString();
                            }
                            else
                            {
                                message = obj["Message"].ToString();
                            }

                            return Json(new { success = true, message = message });
                            //return Json(jarr);
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Error Loading District Details");
                            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

                            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
                            var Message = obj["errorMessage"].ToString();
                            return Json(new { success = false, message = "Error" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json("Failed to Approve/Reject");
                }
            }
        }
    }
}
