using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Service.Interfaces;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICommonActionService _commonActionService;
        public CustomerController(ICustomerService customerService, ICommonActionService commonActionService)
        {
            _customerService = customerService;
            _commonActionService = commonActionService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> OnlineForm()
        {
            var modals = await _customerService.OnlineForm();
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> OnlineForm(CustomerModel cust)
        {

            var modals = await _customerService.OnlineForm(cust);

            if (cust.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirect", new { customerReferenceNo = modals.CustomerReferenceNo });
            }

            return View(modals);
        }


        [HttpPost]
        public async Task<JsonResult> GetCustomerType()
        {

            List<CustomerTypeModel> lstCustomerType = new List<CustomerTypeModel>();
            lstCustomerType = await _customerService.GetCustomerType();
            return Json(lstCustomerType);

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    var CustType = new Dictionary<string, string>
            //    {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"Userid", HttpContext.Session.GetString("UserName")}
            //    };

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(CustType), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getOfficerType, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<CustomerTypeModel> lst = jarr.ToObject<List<CustomerTypeModel>>();
            //            return Json(lst);
            //        }
            //        else
            //        {
            //            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //            // ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
            //        }
            //    }

            //}

        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerSubType(int CustomerTypeID)
        {

            List<CustomerSubTypeModel> lstCustomerSubType = new List<CustomerSubTypeModel>();
            lstCustomerSubType = await _customerService.GetCustomerSubType(CustomerTypeID);
            return Json(lstCustomerSubType);

            //var customerType = new Dictionary<string, string>
            //    {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"Userid", HttpContext.Session.GetString("UserName")},
            //        { "CustomerTypeId", CustomerTypeID.ToString() }
            //};

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(customerType), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getCustomerSubType, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<CustomerSubTypeModel> lst = jarr.ToObject<List<CustomerSubTypeModel>>();
            //            lst.Add(new CustomerSubTypeModel
            //            {
            //                CustomerSubTypeID = 0,
            //                CustomerSubTypeName = "--Select --"
            //            });
            //            var SortedtList = lst.OrderBy(x => x.CustomerSubTypeID).ToList();
            //            return Json(SortedtList);
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading customer sub type Details");
            //            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //        }
            //    }
            //}
        }

        [HttpPost]
        public async Task<JsonResult> GetRegionalDetails(int ZonalOfficeID)
        {
            List<CustomerRegionModel> lstCustomerRegion = new List<CustomerRegionModel>();
            lstCustomerRegion = await _commonActionService.GetRegionalDetailsDropdown(ZonalOfficeID);
            return Json(lstCustomerRegion);
        }

        [HttpPost]
        public async Task<JsonResult> GetDistrictDetails(string Stateid)
        {
            List<OfficerDistrictModel> lstDistrict = new List<OfficerDistrictModel>();
            lstDistrict = await _commonActionService.GetDistrictDetails(Stateid);
            return Json(lstDistrict);
        }

        [HttpPost]
        public async Task<JsonResult> GetVehicleTypeDetails()
        {
            List<VehicleTypeModel> sortedtList = new List<VehicleTypeModel>();
            sortedtList = await _customerService.GetVehicleTypeDetails();
            return Json(new { SortedtList = sortedtList });
        }

        [HttpPost]
        public async Task<JsonResult> PANValidation(string PANNumber)
        {
            string data = await _commonActionService.PANValidation(PANNumber);
            return new JsonResult(data);
        }

        public async Task<IActionResult> SuccessRedirect(int customerReferenceNo)
        {
            ViewBag.CustomerReferenceNo = customerReferenceNo;
            return View();
        }

        public async Task<IActionResult> AddCardDetails(string customerReferenceNo)
        {

            CustomerCardInfo customerCardInfo = new CustomerCardInfo();
            customerCardInfo = await _customerService.AddCardDetails(customerReferenceNo);

            //char flag = 'N';


            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    //Fetching CustomerType
            //    var CustType = new Dictionary<string, string>
            //    {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"Userid", HttpContext.Session.GetString("UserName")},
            //        {"CTFlag",  "1" }
            //    };

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    //Fetching Type of Fleet
            //    var CustomerTypeOfFleetForms = new Dictionary<string, string>
            //    {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"Userid", HttpContext.Session.GetString("UserName")}

            //    };
            //    StringContent TypeOfFleetcontent = new StringContent(JsonConvert.SerializeObject(CustomerTypeOfFleetForms), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getVehicleTpe, TypeOfFleetcontent))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<VehicleTypeModel> lst = jarr.ToObject<List<VehicleTypeModel>>();

            //            //var SortedtList = lst.OrderBy(x => x.VehicleTypeId).ToList();
            //            //custMdl.VehicleTypeMdl.AddRange(SortedtList);
            //            customerCardInfo.VehicleTypeMdl.AddRange(lst);
            //        }
            //        else
            //        {
            //            ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
            //        }
            //    }


            //    if (!string.IsNullOrEmpty(customerReferenceNo))
            //    {

            //        //fetching Customer info
            //        var CustomerRefinfo = new Dictionary<string, string>
            //        {
            //            {"Useragent", CommonBase.useragent},
            //            {"Userip", CommonBase.userip},
            //            {"Userid", HttpContext.Session.GetString("UserName")},
            //            {"CustomerReferenceNo", customerReferenceNo}
            //        };

            //        CustomerResponseByReferenceNo customerResponseByReferenceNo;
            //        StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");
            //        using (var Response = await client.PostAsync(WebApiUrl.getCustomerByReferenceno, custRefcontent))
            //        {
            //            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //            {
            //                var ResponseContent = Response.Content.ReadAsStringAsync().Result;


            //                customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(ResponseContent);
            //                if (customerResponseByReferenceNo.Internel_Status_Code == 1000)
            //                {
            //                    customerCardInfo.CustomerReferenceNo = customerReferenceNo;
            //                    customerCardInfo.FormNumber = customerResponseByReferenceNo.Data[0].FormNumber;

            //                    StringBuilder sb = new StringBuilder();
            //                    if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].FirstName.ToString()))
            //                        sb.Append(customerResponseByReferenceNo.Data[0].FirstName.ToString());

            //                    if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].MiddleName))
            //                        sb.Append(" " + customerResponseByReferenceNo.Data[0].MiddleName);

            //                    if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].LastName))
            //                        sb.Append(" " + customerResponseByReferenceNo.Data[0].LastName);

            //                    customerCardInfo.CustomerName = sb.ToString();

            //                    customerCardInfo.CustomerTypeName = customerResponseByReferenceNo.Data[0].CustomerTypeName;
            //                    customerCardInfo.CustomerTypeId = customerResponseByReferenceNo.Data[0].CustomerTypeId;

            //                    if (customerResponseByReferenceNo.Data != null)
            //                    {
            //                        customerCardInfo.PaymentType = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentType) ? "" : customerResponseByReferenceNo.Data[0].PaymentType;
            //                        customerCardInfo.PaymentReceivedDate = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentReceivedDate) ? "" : customerResponseByReferenceNo.Data[0].PaymentReceivedDate;
            //                        customerCardInfo.NoOfCards = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].NoOfCards) ? "" : customerResponseByReferenceNo.Data[0].NoOfCards;
            //                        customerCardInfo.ReceivedAmount= string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].ReceivedAmount) ? "0" : customerResponseByReferenceNo.Data[0].ReceivedAmount;
            //                        customerCardInfo.RBEId = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEId) ? "0" : customerResponseByReferenceNo.Data[0].RBEId;
            //                        customerCardInfo.RBEName = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEName) ? "0" : customerResponseByReferenceNo.Data[0].RBEName;
            //                        if (customerCardInfo.RBEId == "0")
            //                            customerCardInfo.RBEId = "";
            //                        if (customerCardInfo.NoOfCards == "0")
            //                            customerCardInfo.NoOfCards = "";
            //                        if (customerCardInfo.CustomerTypeId == "905" || customerCardInfo.CustomerTypeId == "909")
            //                        {
            //                            customerCardInfo.NoOfVehiclesAllCards = customerCardInfo.NoOfCards;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    ViewBag.Message = customerResponseByReferenceNo.Message;
            //                }

            //            }
            //            else
            //            {
            //                ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
            //            }
            //        }
            //    }

            //    if (flag == 'Y')
            //    {
            //        ModelState.Clear();
            //        ModelState.AddModelError(string.Empty, "Error Loading Customer Type");
            //        ViewBag.Login = "1";
            //        return View("Index");
            //    }
            //    else
            //    {
            //        return View(customerCardInfo);
            //    }
            //}
            return View(customerCardInfo);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerDetails(string customerReferenceNo)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            customerCardInfo = await _customerService.GetCustomerDetails(customerReferenceNo);

            return Json(customerCardInfo);

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    var forms = new Dictionary<string, string>
            //    {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"Userid", HttpContext.Session.GetString("UserName")},
            //        {"ZonalID", "0" }
            //    };

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");



            //    //fetching Customer info
            //    var CustomerRefinfo = new Dictionary<string, string>
            //        {
            //            {"Useragent", CommonBase.useragent},
            //            {"Userip", CommonBase.userip},
            //            {"Userid", HttpContext.Session.GetString("UserName")},
            //            {"CustomerReferenceNo", customerReferenceNo}
            //        };

            //    CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            //    CustomerResponseByReferenceNo customerResponseByReferenceNo;
            //    StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");
            //    using (var Response = await client.PostAsync(WebApiUrl.getCustomerByReferenceno, custRefcontent))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            customerResponseByReferenceNo = JsonConvert.DeserializeObject<CustomerResponseByReferenceNo>(ResponseContent);
            //            if (customerResponseByReferenceNo.Internel_Status_Code == 1000)
            //            {
            //                customerCardInfo.CustomerReferenceNo = customerReferenceNo;
            //                customerCardInfo.FormNumber = customerResponseByReferenceNo.Data[0].FormNumber;

            //                StringBuilder sb = new StringBuilder();
            //                if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].FirstName.ToString()))
            //                    sb.Append(customerResponseByReferenceNo.Data[0].FirstName.ToString());

            //                if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].MiddleName))
            //                    sb.Append(" " + customerResponseByReferenceNo.Data[0].MiddleName);

            //                if (!string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].LastName))
            //                    sb.Append(" " + customerResponseByReferenceNo.Data[0].LastName);


            //                customerCardInfo.CustomerName = sb.ToString();

            //                customerCardInfo.CustomerTypeName = customerResponseByReferenceNo.Data[0].CustomerTypeName;
            //                customerCardInfo.CustomerTypeId = customerResponseByReferenceNo.Data[0].CustomerTypeId;
            //                if (customerResponseByReferenceNo.Data != null)
            //                {
            //                    customerCardInfo.PaymentType = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentType) ? "" : customerResponseByReferenceNo.Data[0].PaymentType;
            //                    customerCardInfo.PaymentReceivedDate = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].PaymentReceivedDate) ? "" : customerResponseByReferenceNo.Data[0].PaymentReceivedDate;
            //                    customerCardInfo.NoOfCards = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].NoOfCards) ? "" : customerResponseByReferenceNo.Data[0].NoOfCards;
            //                    customerCardInfo.ReceivedAmount = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].ReceivedAmount) ? "0" : customerResponseByReferenceNo.Data[0].ReceivedAmount;
            //                    customerCardInfo.RBEId = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEId) ? "0" : customerResponseByReferenceNo.Data[0].RBEId;
            //                    if (customerCardInfo.RBEId == "0")
            //                        customerCardInfo.RBEId = "";
            //                    if (customerCardInfo.NoOfCards == "0")
            //                        customerCardInfo.NoOfCards = "";
            //                    customerCardInfo.RBEName = string.IsNullOrEmpty(customerResponseByReferenceNo.Data[0].RBEName) ? "0" : customerResponseByReferenceNo.Data[0].RBEName;

            //                    if (customerCardInfo.CustomerTypeId == "905" || customerCardInfo.CustomerTypeId == "909")
            //                    {
            //                        customerCardInfo.NoOfVehiclesAllCards = customerCardInfo.NoOfCards;
            //                    }
            //                }

            //                return Json(customerCardInfo);
            //            }
            //            else
            //            {
            //                ModelState.Clear();
            //                ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
            //                var Response_Content = Response.Content.ReadAsStringAsync().Result;

            //                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(Response_Content).ToString());
            //                return Json("Failed to load Customer Details");
            //            }
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
            //            var Response_Content = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(Response_Content).ToString());
            //            var Message = obj["errorMessage"].ToString();
            //            return Json("Failed to load Customer Details");
            //        }
            //    }


            //}
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerRBEName(string RBEId)
        {

            CustomerCardInfo customerCardInfo = new CustomerCardInfo();
            customerCardInfo = await _customerService.GetCustomerRBEName(RBEId);

            return Json(customerCardInfo);

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    var forms = new Dictionary<string, string>
            //    {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"Userid", HttpContext.Session.GetString("UserName")},
            //        {"ZonalID", "0" }
            //    };

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");



            //    //fetching Customer info
            //    var CustomerRefinfo = new Dictionary<string, string>
            //        {
            //            {"Useragent", CommonBase.useragent},
            //            {"Userip", CommonBase.userip},
            //            {"Userid", HttpContext.Session.GetString("UserName")},
            //            {"RBEId", RBEId}
            //        };

            //    CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            //    CustomerRBE customerRBE;
            //    StringContent custRefcontent = new StringContent(JsonConvert.SerializeObject(CustomerRefinfo), Encoding.UTF8, "application/json");
            //    using (var Response = await client.PostAsync(WebApiUrl.getCustomerRbeId, custRefcontent))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            customerRBE = JsonConvert.DeserializeObject<CustomerRBE>(ResponseContent);
            //            if (customerRBE.Internel_Status_Code == 1000)
            //            {
            //                customerCardInfo.RBEName = customerRBE.Data[0].RBEName;
            //                customerCardInfo.RBEId = customerRBE.Data[0].RBEId.ToString();

            //                return Json(customerCardInfo);
            //            }
            //            else
            //            {
            //                ModelState.Clear();
            //                ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
            //                var Response_Content = Response.Content.ReadAsStringAsync().Result;

            //                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(Response_Content).ToString());
            //                return Json("Failed to load Customer Details");
            //            }

            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
            //            var Response_Content = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(Response_Content).ToString());
            //            var Message = obj["errorMessage"].ToString();
            //            return Json("Failed to load Customer Details");
            //        }
            //    }
            //}

        }

        [HttpPost]
        public async Task<IActionResult> AddCardDetails(CustomerCardInfo customerCardInfo)
        {
            CustomerCardInfo cardInfo = new CustomerCardInfo();
            cardInfo = await _customerService.AddCardDetails(customerCardInfo);

            if (cardInfo.StatusCode == 1000)
            {
                ViewBag.Message = "Customer card details saved Successfully";
                customerCardInfo.Status = cardInfo.Status;
                customerCardInfo.StatusCode = cardInfo.StatusCode;
                return RedirectToAction("SuccessAddCardRedirect", new { customerReferenceNo = cardInfo.CustomerReferenceNo });
                //ModelState.Clear();
            }
            else
            {
                ViewBag.Message = cardInfo.Message;
            }

            return Json(customerCardInfo);
        }


        public IActionResult UploadDoc(string customerReferenceNo)
        {
            UploadDoc uploadDoc = new UploadDoc();
            if (!string.IsNullOrEmpty(customerReferenceNo))
            {
                uploadDoc.CustomerReferenceNo = customerReferenceNo;
            }
            return View(uploadDoc);
        }

        [HttpPost]
        public async Task<JsonResult> UploadDoc(UploadDoc entity)
        {
            var searchCustomer = _customerService.UploadDoc(entity);

            ModelState.Clear();
            return Json(new { searchCustomer = searchCustomer });
        }

        [HttpPost]
        public async Task<JsonResult> SaveUploadDoc(UploadDoc entity)
        {
            var reason = await _customerService.SaveUploadDoc(entity);

            ModelState.Clear();
            return Json(reason);
        }

        public async Task<IActionResult> ValidateNewCustomer()
        {
            CustomerModel customerModel = new CustomerModel();
            customerModel = await _customerService.ValidateNewCustomer();


            //var OfficerType = new Dictionary<string, string>
            //    {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"Userid", HttpContext.Session.GetString("UserName")}
            //    };


            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(OfficerType), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getOfficerType, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();

            //            List<OfficerTypeModel> lst = jarr.ToObject<List<OfficerTypeModel>>();

            //            customerModel.OfficerTypeMdl.AddRange(lst);
            //        }
            //        else
            //        {
            //            ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
            //        }
            //    }

            //}

            return View(customerModel);
        }

        [HttpPost]
        public async Task<JsonResult> ValidateNewCustomer(CustomerModel entity)
        {
            List<SearchCustomerResponseGrid> searchList = new List<SearchCustomerResponseGrid>();
            searchList = await _customerService.ValidateNewCustomer(entity);

            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> ReloadUpdatedGrid()
        {
            List<SearchCustomerResponseGrid> updatedSearchList = await _customerService.ReloadUpdatedGrid();
            return Json(new { updatedSearchList = updatedSearchList });

            //var str = HttpContext.Session.GetString("viewUpdatedCustGrid");

            //CustomerModel vGrid = JsonConvert.DeserializeObject<CustomerModel>(str);

            //var searchBody = new Dictionary<string, string>
            //{
            //   {"Useragent", CommonBase.useragent},
            //    {"Userip", CommonBase.userip},
            //    {"Userid", HttpContext.Session.GetString("UserName")},
            //    {"OfficerTypeID", vGrid.OfficerTypeID > 0 ? vGrid.OfficerTypeID.ToString() : null},
            //    {"CustomerDateOfApplication", vGrid.CustomerDateOfApplication},
            //    {"FormNumber", vGrid.FormNumber},
            //    {"StateId" , null},
            //    {"CustomerName" , null}
            //};

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getCustomerPendingForApproval, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<SearchCustomerResponseGrid> updatedSearchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();
            //            ModelState.Clear();
            //            return Json(new { updatedSearchList = updatedSearchList });
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
            //            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //        }
            //    }
            //}
        }

        [HttpPost]
        public async Task<JsonResult> ViewCustomerDetails(string FormNumber)
        {
            JObject obj = await _customerService.ViewCustomerDetails(FormNumber);

            var searchRes = obj["Data"].Value<JObject>();
            var cardResult = searchRes["GetCustomerDetails"].Value<JArray>();

            var customerKYCDetailsResult = searchRes["CustomerKYCDetails"].Value<JArray>();

            List<CustomerFullDetails> customerList = cardResult.ToObject<List<CustomerFullDetails>>();

            List<UploadDocResponseBody> UploadDocList = customerKYCDetailsResult.ToObject<List<UploadDocResponseBody>>();

            CustomerFullDetails Customer = customerList.Where(t => t.FormNumber == FormNumber).FirstOrDefault();

            if (Customer != null)
            {
                if (!string.IsNullOrEmpty(Customer.CommunicationPhoneNo))
                {
                    string[] subs = Customer.CommunicationPhoneNo.Split('-');

                    if (subs.Count() > 1)
                    {
                        Customer.CommunicationDialCode = subs[0].ToString();
                        Customer.CommunicationPh = subs[1].ToString();
                    }
                    else
                    {
                        Customer.CommunicationPh = Customer.CommunicationPhoneNo;
                    }
                }

                if (!string.IsNullOrEmpty(Customer.CommunicationFax))
                {
                    string[] subs = Customer.CommunicationFax.Split('-');

                    if (subs.Count() > 1)
                    {
                        Customer.CommunicationFaxCode = subs[0].ToString();
                        Customer.CommunicationFaxPh = subs[1].ToString();
                    }
                    else
                    {
                        Customer.CommunicationFaxPh = Customer.CommunicationFax;
                    }
                }

                if (!string.IsNullOrEmpty(Customer.PermanentFax))
                {
                    string[] subs = Customer.PermanentFax.Split('-');

                    if (subs.Count() > 1)
                    {
                        Customer.PermanentFaxCode = subs[0].ToString();
                        Customer.PermanentFaxPh = subs[1].ToString();
                    }
                    else
                    {
                        Customer.PermanentFaxPh = Customer.CommunicationFax;
                    }
                }

                if (!string.IsNullOrEmpty(Customer.PermanentPhoneNo))
                {
                    string[] subs = Customer.PermanentPhoneNo.Split('-');

                    if (subs.Count() > 1)
                    {
                        Customer.PerOrRegAddressDialCode = subs[0].ToString();
                        Customer.PerOrRegAddressPh = subs[1].ToString();
                    }
                    else
                    {
                        Customer.PerOrRegAddressPh = Customer.PermanentPhoneNo;
                    }
                }

                if (!string.IsNullOrEmpty(Customer.KeyOfficialPhoneNo))
                {
                    string[] subs = Customer.KeyOfficialPhoneNo.Split('-');

                    if (subs.Count() > 1)
                    {
                        Customer.KeyOffDialCode = subs[0].ToString();
                        Customer.KeyOffPh = subs[1].ToString();
                    }
                    else
                    {
                        Customer.KeyOffPh = Customer.KeyOfficialPhoneNo;
                    }
                }

                if (!string.IsNullOrEmpty(Customer.KeyOfficialFax))
                {
                    string[] subs = Customer.KeyOfficialFax.Split('-');

                    if (subs.Count() > 1)
                    {
                        Customer.KeyOffFaxCode = subs[0].ToString();
                        Customer.KeyOffFaxPh = subs[1].ToString();
                    }
                    else
                    {
                        Customer.KeyOffPh = Customer.KeyOfficialFax;
                    }
                }
            }

            FormNumber = string.Empty;

            HttpContext.Session.SetString("FormNumberSession", FormNumber);

            ModelState.Clear();

            return Json(new { customer = Customer, kycDetailsResult = UploadDocList });
        }

        [HttpPost]
        public async Task<JsonResult> AproveCustomer(string CustomerReferenceNo, string Comments, string Approvalstatus)
        {
            UpdateKycResponse updateKycResponse = await _customerService.AproveCustomer(CustomerReferenceNo, Comments, Approvalstatus);
            return Json(updateKycResponse.Reason);

            //var approvalBody = new Dictionary<string, string>
            //{
            //    {"Useragent", CommonBase.useragent},
            //    {"Userip", CommonBase.userip},
            //    {"Userid", HttpContext.Session.GetString("UserName")},
            //    {"CustomerReferenceNo" , CustomerReferenceNo},
            //    {"Comments" , Comments},
            //    {"Approvalstatus" , Approvalstatus},
            //    {"ApprovedBy" , HttpContext.Session.GetString("UserName")}
            //};

            ////HttpContext.Session.SetString("viewUpdatedCustGrid", JsonConvert.SerializeObject(searchBody));

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(approvalBody), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.approveOrrejectcustomer, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            //var jsonSerializerOptions = new JsonSerializerOptions()
            //            //{
            //            //    IgnoreNullValues = true
            //            //};
            //            //JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            //var jarr = obj["Data"].Value<JArray>();
            //            //List<SearchCustomerResponseGrid> searchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();
            //            //ModelState.Clear();
            //            //return Json(new { searchList = searchList });

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<UpdateKycResponse> insertKyc = jarr.ToObject<List<UpdateKycResponse>>();
            //            ModelState.Clear();


            //            return Json(insertKyc[0].Reason);
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
            //            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //        }
            //    }
            //}
        }

        [HttpPost]
        public async Task<JsonResult> GetsalesAreaDetails(int RegionID)
        {
            List<SalesAreaModel> lst = await _customerService.GetsalesAreaDetails(RegionID);
            return Json(lst);

            //var customerRegion = new Dictionary<string, string>
            // {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"Userid", HttpContext.Session.GetString("UserName")},
            //        {"RegionID", RegionID.ToString() }

            //};

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(customerRegion), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getSalesArea, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;
            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<SalesAreaModel> lst = jarr.ToObject<List<SalesAreaModel>>();
            //            lst.Add(new SalesAreaModel
            //            {
            //                SalesAreaID = 0,
            //                SalesAreaName = "Select Sales Area",

            //            });
            //            var SortedtList = lst.OrderBy(x => x.SalesAreaID).ToList();
            //            return Json(SortedtList);
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading sales Area Details");
            //            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //        }
            //    }
            //}
        }

        public async Task<IActionResult> SuccessUploadRedirect(int customerReferenceNo)
        {
            ViewBag.CustomerReferenceNo = customerReferenceNo;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CheckformnumberDuplication(string FormNumber)
        {
            CustomerInserCardResponseData customerInserCardResponseData = await _commonActionService.CheckformNumberDuplication(FormNumber);

            return Json(customerInserCardResponseData);
        }

        [HttpPost]
        public async Task<JsonResult> CheckEmailDuplication(string Emailid)
        {
            CustomerInserCardResponseData customerInserCardResponseData = await _commonActionService.CheckEmailDuplication(Emailid);

            return Json(customerInserCardResponseData);
        }

        [HttpPost]
        public async Task<JsonResult> CheckMobilNoDuplication(string MobileNo)
        {
            CustomerInserCardResponseData customerInserCardResponseData = await _commonActionService.CheckMobilNoDuplication(MobileNo);

            return Json(customerInserCardResponseData);
        }

        [HttpPost]
        public async Task<JsonResult> CheckVehicleRegistrationValid(string RegistrationNumber)
        {
            var data = await _commonActionService.CheckVehicleRegistrationValid(RegistrationNumber);
            return new JsonResult(data);
        }

        [HttpPost]
        public async Task<JsonResult> CheckPanNoDuplication(string PanNo)
        {
            CustomerInserCardResponseData customerInserCardResponseData = new CustomerInserCardResponseData();
            customerInserCardResponseData = await _commonActionService.CheckPanNoDuplication(PanNo);

            return Json(customerInserCardResponseData);
        }

        public async Task<IActionResult> BalanceInfo()
        {
            CustomerBalanceInfoModel customerBalanceInfo = new CustomerBalanceInfoModel();
            customerBalanceInfo = await _customerService.BalanceInfo();
            return View(customerBalanceInfo);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerBalanceInfo(string CustomerID)
        {

            GetCustomerBalanceResponse customerBalanceResponse = new GetCustomerBalanceResponse();
            customerBalanceResponse = await _customerService.GetCustomerBalanceInfo(CustomerID);

            return Json(new { customerBalanceResponse = customerBalanceResponse.Data });
        }
        public async Task<IActionResult> GetCustomerCardWiseBalance(string CustomerID)
        {
            var modals = await _customerService.GetCustomerCardWiseBalance(CustomerID);
            return PartialView("~/Views/Customer/_CustomerCardWiseBalancesTbl.cshtml", modals);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerDetailsByCustomerID(string CustomerID)
        {
            JObject obj = await _customerService.GetCustomerDetailsByCustomerID(CustomerID);

            var searchRes = obj["Data"].Value<JObject>();
            var custResult = searchRes["GetCustomerDetails"].Value<JArray>();

            List<GetCustomerDetailsResponse> customerList = custResult.ToObject<List<GetCustomerDetailsResponse>>();

            GetCustomerDetailsResponse Customer = customerList.Where(t => t.CustomerID == CustomerID).FirstOrDefault();

            ModelState.Clear();

            return Json(new { customer = Customer });
        }

        public async Task<IActionResult> SuccessAddCardRedirect(int customerReferenceNo)
        {
            ViewBag.CustomerReferenceNo = customerReferenceNo;
            return View();
        }
        public async Task<IActionResult> GetCCMSBalanceDetails(string CustomerID)
        {

            var modals = await _customerService.GetCCMSBalanceDetails(CustomerID);
            return PartialView("~/Views/Customer/_CustomerCCMSBalanceDetails.cshtml", modals);
        }
        public async Task<IActionResult> GetFormNumber()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GenerateFormNumber()
        {
            var FormNumber = await _customerService.GenerateFormNumber();

            return Json(FormNumber);
        }
        [HttpPost]
        public async Task<JsonResult> CheckPanCardDuplicationByDistrictid(string DistrictId, string IncomeTaxPan)
        {
            var result = await _commonActionService.CheckPanCardDuplicationByDistrictid(DistrictId, IncomeTaxPan);

            return Json(result);
        }

    }
}