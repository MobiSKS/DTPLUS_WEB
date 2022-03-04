using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Service.Interfaces;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.Customer;

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
            lstDistrict = await _customerService.GetDistrictDetails(Stateid);
            return Json(lstDistrict);

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    var forms = new Dictionary<string, string>
            //    {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"Userid", HttpContext.Session.GetString("UserName")},
            //        {"StateID", Stateid.ToString()}
            //    };

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getDistrict, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<OfficerDistrictModel> lst = jarr.ToObject<List<OfficerDistrictModel>>();
            //            lst.Add(new OfficerDistrictModel
            //            {
            //                stateID = 0,
            //                districtID = 0,
            //                districtName = "Select District"
            //            });
            //            var SortedtList = lst.OrderBy(x => x.districtID).ToList();
            //            return Json(SortedtList);
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading District Details");
            //            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //        }
            //    }
            //}
        }

        [HttpPost]
        public async Task<JsonResult> GetVehicleTypeDetails()
        {
            List<VehicleTypeModel> sortedtList = new List<VehicleTypeModel>();
            sortedtList = await _customerService.GetVehicleTypeDetails();
            return Json(new { SortedtList = sortedtList });

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    var forms = new Dictionary<string, string>
            //    {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"Userid", HttpContext.Session.GetString("UserName")}

            //    };

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getVehicleTpe, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<VehicleTypeModel> lst = jarr.ToObject<List<VehicleTypeModel>>();

            //            var SortedtList = lst.OrderBy(x => x.VehicleTypeId).ToList();
            //            return Json(new { SortedtList = SortedtList });
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading Vehicle Type Details");
            //            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //        }
            //    }
            //}
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
                //return RedirectToAction("SuccessRedirect", new { customerReferenceNo = customerResponse.Data[0].CustomerReferenceNo });
                customerCardInfo.Status = cardInfo.Status;
                customerCardInfo.StatusCode = cardInfo.StatusCode;
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = cardInfo.Message;
            }


            return Json(customerCardInfo);

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{

            //    string feePaymentDate = "";

            //    if (!string.IsNullOrEmpty(customerCardInfo.FeePaymentDate))
            //    {
            //        string[] arrFeePaymentDate = customerCardInfo.FeePaymentDate.Split("-");
            //        feePaymentDate = arrFeePaymentDate[2] + "-" + arrFeePaymentDate[1] + "-" + arrFeePaymentDate[0];
            //    }

            //    feePaymentDate = (string.IsNullOrEmpty(feePaymentDate) ? "1900-01-01" : feePaymentDate);


            //    #region Create Request Info

            //    CustomerCardInsertInfo insertInfo = new CustomerCardInsertInfo();

            //    insertInfo.CustomerReferenceNo = customerCardInfo.CustomerReferenceNo;
            //    insertInfo.CustomerName = customerCardInfo.CustomerName;
            //    insertInfo.FormNumber = customerCardInfo.FormNumber;
            //    insertInfo.NoOfCards = customerCardInfo.NoOfCards;
            //    insertInfo.RBEId = customerCardInfo.RBEId;
            //    insertInfo.FeePaymentsCollectFeeWaiver = customerCardInfo.FeePaymentsCollectFeeWaiver;
            //    insertInfo.FeePaymentNo = customerCardInfo.FeePaymentNo;
            //    insertInfo.FeePaymentDate = feePaymentDate;
            //    insertInfo.CardPreference = customerCardInfo.CardPreference;
            //    insertInfo.RBEName = customerCardInfo.RBEName;
            //    insertInfo.RBEName = customerCardInfo.RBEName;
            //    insertInfo.Useragent = CommonBase.useragent;
            //    insertInfo.Userip = CommonBase.userip;
            //    insertInfo.UserId = HttpContext.Session.GetString("UserName");
            //    insertInfo.Createdby = HttpContext.Session.GetString("UserName");
            //    insertInfo.ObjCardDetail = customerCardInfo.ObjCardDetail;

            //    if (insertInfo.CardPreference.ToUpper() == "CARDLESS")
            //    {
            //        insertInfo.FeePaymentsCollectFeeWaiver = 0;
            //    }

            //    #endregion


            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    //string jsonString = JsonConvert.SerializeObject(CustomerCardData);





            //    #region Commented

            //    //// jsonString = jsonString.Replace("\\", "");

            //    ////StringContent content = new StringContent(CustomerCardData, Encoding.UTF8, "application/json");

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(insertInfo), Encoding.UTF8, "application/json");

            //    #endregion

            //    CustomerInserCardResponse customerInserCardResponse;


            //    using (var Response = await client.PostAsync(WebApiUrl.insertCustomerCard, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {

            //            using (HttpContent contentUrl = Response.Content)
            //            {
            //                var contentString = contentUrl.ReadAsStringAsync().Result;
            //                customerInserCardResponse = JsonConvert.DeserializeObject<CustomerInserCardResponse>(contentString);
            //                if (customerInserCardResponse.Internel_Status_Code == 1000)
            //                {
            //                    ViewBag.Message = "Customer card details saved Successfully";
            //                    //return RedirectToAction("SuccessRedirect", new { customerReferenceNo = customerResponse.Data[0].CustomerReferenceNo });
            //                    customerCardInfo.Status = customerInserCardResponse.Data[0].Status;
            //                    customerCardInfo.StatusCode = customerInserCardResponse.Internel_Status_Code;
            //                    ModelState.Clear();
            //                }
            //                else
            //                {
            //                    ViewBag.Message = customerInserCardResponse.Message;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            ViewBag.Message = "Status Code: " + Response.StatusCode.ToString() + " Error Message: " + Response.RequestMessage.ToString();
            //        }
            //    }

            //}

            //return View(customerCardInfo);
        }


        public async Task<IActionResult> UploadDoc(string CustomerReferenceNo)
        {
            var modals = await _customerService.UploadDoc(CustomerReferenceNo);
            return View(modals);
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

            //var searchBody = new Dictionary<string, string>
            //{
            //    {"Useragent", CommonBase.useragent},
            //    {"Userip", CommonBase.userip},
            //    {"Userid", HttpContext.Session.GetString("UserName")},
            //    {"Createdby" , entity.OfficerTypeID>0? entity.OfficerTypeID.ToString(): null },
            //    {"Createdon" , entity.CustomerDateOfApplication},
            //    {"FormNumber" , entity.FormNumber},
            //    {"StateId" , null},
            //    {"CustomerName" , null}
            //};

            //HttpContext.Session.SetString("viewUpdatedCustGrid", JsonConvert.SerializeObject(searchBody));

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getCustomerPendingForApproval, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            var jsonSerializerOptions = new JsonSerializerOptions()
            //            {
            //                IgnoreNullValues = true
            //            };
            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<SearchCustomerResponseGrid> searchList = jarr.ToObject<List<SearchCustomerResponseGrid>>();
            //            ModelState.Clear();
            //            return Json(new { searchList = searchList });
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

            FormNumber = string.Empty;


            HttpContext.Session.SetString("FormNumberSession", FormNumber);

            ModelState.Clear();

            return Json(new { customer = Customer, kycDetailsResult = UploadDocList });

            //HttpContext.Session.SetString("FormNumberSession", FormNumber);

            //var customerBody = new Dictionary<string, string>
            //{
            //    {"Useragent", CommonBase.useragent},
            //    {"Userip", CommonBase.userip},
            //    {"Userid", HttpContext.Session.GetString("UserName")},
            //    {"FormNumber" , FormNumber}
            //};

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent content = new StringContent(JsonConvert.SerializeObject(customerBody), Encoding.UTF8, "application/json");

            //    using (var Response = await client.PostAsync(WebApiUrl.getcustomerdetailsByFormNumber, content))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());

            //            var searchRes = obj["Data"].Value<JObject>();
            //            var cardResult = searchRes["GetCustomerDetails"].Value<JArray>();

            //            var customerKYCDetailsResult = searchRes["CustomerKYCDetails"].Value<JArray>();

            //            List<CustomerFullDetails> customerList = cardResult.ToObject<List<CustomerFullDetails>>();

            //            List<UploadDocResponseBody> UploadDocList = customerKYCDetailsResult.ToObject<List<UploadDocResponseBody>>();


            //            CustomerFullDetails Customer = customerList.Where(t => t.FormNumber == FormNumber).FirstOrDefault();

            //            FormNumber = string.Empty;


            //            HttpContext.Session.SetString("FormNumberSession", FormNumber);

            //            ModelState.Clear();

            //            return Json(new { customer = Customer, kycDetailsResult = UploadDocList });
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading Location Details");
            //            return Json("Status Code: " + Response.StatusCode.ToString() + " Message: " + Response.RequestMessage);
            //        }
            //    }
            //}
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
            CustomerInserCardResponseData customerInserCardResponseData = await _customerService.CheckformnumberDuplication(FormNumber);

            return Json(customerInserCardResponseData);

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    //fetching Customer info
            //    var CustomerFormNumber = new Dictionary<string, string>
            //        {
            //            {"Useragent", CommonBase.useragent},
            //            {"Userip", CommonBase.userip},
            //            {"Userid", HttpContext.Session.GetString("UserName")},
            //            {"FormNumber", FormNumber }
            //        };

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            //    StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(CustomerFormNumber), Encoding.UTF8, "application/json");
            //    using (var Response = await client.PostAsync(WebApiUrl.checkformnumberDuplication, cusFormcontent))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            //            return Json(lst[0]);
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
            //            var Response_Content = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(Response_Content).ToString());
            //            var Message = obj["errorMessage"].ToString();
            //            return Json("Failed to load Form Details");
            //        }
            //    }
            //}
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
            CustomerInserCardResponseData customerInserCardResponseData = await _customerService.CheckMobilNoDuplication(MobileNo);

            return Json(customerInserCardResponseData);

            //using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            //{
            //    //fetching Customer info
            //    var CustomerFormNumber = new Dictionary<string, string>
            //        {
            //            {"Useragent", CommonBase.useragent},
            //            {"Userip", CommonBase.userip},
            //            {"Userid", HttpContext.Session.GetString("UserName")},
            //            {"CommunicationMobileNo", MobileNo }
            //        };

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));


            //    StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(CustomerFormNumber), Encoding.UTF8, "application/json");
            //    using (var Response = await client.PostAsync(WebApiUrl.checkmobileNoDuplication, cusFormcontent))
            //    {
            //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var ResponseContent = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            //            var jarr = obj["Data"].Value<JArray>();
            //            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            //            return Json(lst[0]);
            //        }
            //        else
            //        {
            //            ModelState.Clear();
            //            ModelState.AddModelError(string.Empty, "Error Loading Customer Details");
            //            var Response_Content = Response.Content.ReadAsStringAsync().Result;

            //            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(Response_Content).ToString());
            //            var Message = obj["errorMessage"].ToString();
            //            return Json("Failed to load Mobile Details");
            //        }
            //    }
            //}

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


    }
}