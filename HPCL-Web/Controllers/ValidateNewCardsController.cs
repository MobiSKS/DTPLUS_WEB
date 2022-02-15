using HPCL_Web.Helper;
using HPCL_Web.Models;
using HPCL_Web.Models.Officer;
using HPCL_Web.Models.ValidateNewCards;
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
                    {"Useragent", Common.useragent},
                    {"Userip", Common.userip},
                    {"Userid", Common.userid}
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

                if (!String.IsNullOrEmpty(validateNewCardsMdl.FormNumber))
                {
                    validateNewCardsModel.FormNumber = validateNewCardsMdl.FormNumber;
                    validateNewCardsModel.Date = validateNewCardsMdl.Date;
                    validateNewCardsModel.CreatedBy = validateNewCardsMdl.CreatedBy;


                    string[] dateArr = validateNewCardsModel.Date.Split("/");

                    string Date = dateArr[2] + "-" + dateArr[1] + "-" + dateArr[0];

                    var ValidateNewCardListForm = new Dictionary<string, string>
                    {
                        {"Useragent", Common.useragent},
                        {"Userip", Common.userip},
                        {"Userid", Common.userid},
                        {"StateId", "" },
                        {"FormNumber", validateNewCardsModel.FormNumber },
                        {"CustomerName", validateNewCardsModel.CreatedBy },
                        {"Createdon", validateNewCardsModel.Date },
                        {"Createdby", "0" }
                    };

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    StringContent ValidateNewCardListContent = new StringContent(JsonConvert.SerializeObject(ValidateNewCardListForm), Encoding.UTF8, "application/json");
                    using (var Response = await client.PostAsync(WebApiUrl.getMerchantApprovalList, ValidateNewCardListContent))
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
            }

            //if (validateNewCardsMdl.ShowTable == "Error")
            //{
            //    ViewBag.NotFoundError = "Not Found";
            //}
            return View(validateNewCardsModel);
        }
    }
}
