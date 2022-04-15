﻿using Microsoft.AspNetCore.Mvc;
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
using System;

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
                return RedirectToAction("SuccessRedirect", new { customerReferenceNo = modals.CustomerReferenceNo, Message = cust.Remarks });
            }

            return View(modals);
        }


        [HttpPost]
        public async Task<JsonResult> GetCustomerType()
        {

            List<CustomerTypeModel> lstCustomerType = new List<CustomerTypeModel>();
            lstCustomerType = await _customerService.GetCustomerType();
            return Json(lstCustomerType);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerSubType(int CustomerTypeID)
        {
            List<CustomerSubTypeModel> lstCustomerSubType = new List<CustomerSubTypeModel>();
            lstCustomerSubType = await _commonActionService.GetCustomerSubTypeDropdown(CustomerTypeID);
            return Json(lstCustomerSubType);
        }

        [HttpPost]
        public async Task<JsonResult> GetRegionalDetails(int ZonalOfficeID)
        {
            List<CustomerRegionModel> lstCustomerRegion = new List<CustomerRegionModel>();
            if (ZonalOfficeID > 0)
            {
                lstCustomerRegion = await _commonActionService.GetRegionalDetailsDropdown(ZonalOfficeID);
                if (lstCustomerRegion.Count > 0)
                {
                    if (lstCustomerRegion[0].RegionalOfficeName == "--Select--")
                        lstCustomerRegion[0].RegionalOfficeName = "Select Regional Office";
                }
            }
            else
            {
                lstCustomerRegion.Add(new CustomerRegionModel
                {
                    RegionalOfficeID = 0,
                    RegionalOfficeName = "Select Regional Office",

                });
            }
            return Json(lstCustomerRegion);
        }

        [HttpPost]
        public async Task<JsonResult> GetDistrictDetails(string Stateid)
        {
            List<OfficerDistrictModel> lstDistrict = new List<OfficerDistrictModel>();
            if (Convert.ToInt32(string.IsNullOrEmpty(Stateid) ? "0" : Stateid) > 0)
            {
                lstDistrict = await _commonActionService.GetDistrictDetails(Stateid);
            }
            else
            {
                lstDistrict.Add(new OfficerDistrictModel
                {
                    districtID = 0,
                    districtName = "Select District"
                });
            }
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

        public async Task<IActionResult> SuccessRedirect(string customerReferenceNo, string Message)
        {
            ViewBag.CustomerReferenceNo = customerReferenceNo;
            ViewBag.Message = Message;
            return View();
        }

        public async Task<IActionResult> AddCardDetails(string customerReferenceNo)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();
            customerCardInfo = await _customerService.AddCardDetails(customerReferenceNo);

            return View(customerCardInfo);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerDetails(string customerReferenceNo)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            customerCardInfo = await _customerService.GetCustomerDetails(customerReferenceNo);

            return Json(customerCardInfo);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerDetailsForAddCard(string customerReferenceNo)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            customerCardInfo = await _customerService.GetCustomerDetailsForAddCard(customerReferenceNo);

            return Json(customerCardInfo);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerRBEName(string RBEId)
        {

            CustomerCardInfo customerCardInfo = new CustomerCardInfo();
            customerCardInfo = await _customerService.GetCustomerRBEName(RBEId);

            return Json(customerCardInfo);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddCardDetails([FromBody]CustomerCardInfo customerCardInfo)
        {
            var result = await _customerService.AddCardDetails(customerCardInfo);

            return Json(result);
        }

        public async Task<IActionResult> UploadDoc(string customerReferenceNo, string FormNumber)
        {
            UploadDoc uploadDoc = new UploadDoc();
            uploadDoc.CustomerReferenceNo = "";
            uploadDoc.FormNumber = "";
            uploadDoc.IdProofType = 0;
            uploadDoc.AddressProofType = 0;
            uploadDoc.IdProofDocumentNo = "";
            uploadDoc.AddressProofDocumentNo = "";

            if (!string.IsNullOrEmpty(customerReferenceNo))
            {
                uploadDoc.CustomerReferenceNo = customerReferenceNo;

                if (!string.IsNullOrEmpty(FormNumber))
                {
                    var response = await _customerService.UploadDoc(FormNumber);

                    if (response != null)
                    {
                        uploadDoc.FormNumber = FormNumber;
                        uploadDoc.IdProofType = Convert.ToInt32(response.IdProofTypeId);
                        uploadDoc.IdProofDocumentNo = response.IdProofDocumentNo;
                        uploadDoc.IdProofFrontSRC = response.IdProofFront;
                        //uploadDoc.IdProofFront = response.IdProofFront;
                        //uploadDoc.IdProofBack = response.IdProofBack;
                        uploadDoc.AddressProofType = Convert.ToInt32(response.AddressProofTypeId);
                        uploadDoc.AddressProofDocumentNo = response.AddressProofDocumentNo;
                        //uploadDoc.AddressProofFront = response.AddressProofFront;
                        //uploadDoc.AddressProofBack = response.AddressProofBack;
                    }
                }
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

        public async Task<IActionResult> ValidateNewCustomer(CustomerValidate entity)
        {
            var modals = await _customerService.ValidateNewCustomer(entity);

            return View(modals);
        }
        
        [HttpPost]
        public async Task<JsonResult> ReloadUpdatedGrid()
        {
            List<SearchCustomerResponseGrid> updatedSearchList = await _customerService.ReloadUpdatedGrid();
            return Json(new { updatedSearchList = updatedSearchList });
        }

        [HttpPost]
        public async Task<JsonResult> ViewCustomerDetails(string FormNumber)
        {
            if (!string.IsNullOrEmpty(FormNumber))
                FormNumber = FormNumber.Trim();

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

                string applicationDate = "";
                if (!string.IsNullOrEmpty(Customer.DateOfApplication))
                {
                    string[] dateAndTime = Customer.DateOfApplication.Split(" ");
                    string[] dateOfApplication = dateAndTime[0].Split("/");
                    applicationDate = dateOfApplication[1] + "-" + dateOfApplication[0] + "-" + dateOfApplication[2];
                    Customer.DateOfApplication = applicationDate;
                }
                else
                {
                    Customer.DateOfApplication = "";
                }

                applicationDate = "";
                if (!string.IsNullOrEmpty(Customer.KeyOfficialDOA))
                {
                    if (!Customer.KeyOfficialDOA.Contains("1900"))
                    {
                        string[] dateAndTime = Customer.KeyOfficialDOA.Split(" ");
                        string[] DOA = dateAndTime[0].Split("/");
                        applicationDate = DOA[1] + "-" + DOA[0] + "-" + DOA[2];
                        Customer.KeyOfficialDOA = applicationDate;
                    }
                    else
                    {
                        Customer.KeyOfficialDOA = "";
                    }
                }
                else
                {
                    Customer.KeyOfficialDOA = "";
                }

                applicationDate = "";
                if (!string.IsNullOrEmpty(Customer.KeyOfficialDOB))
                {
                    if (!Customer.KeyOfficialDOB.Contains("1900"))
                    {
                        string[] dateAndTime = Customer.KeyOfficialDOB.Split(" ");
                        string[] DOB = dateAndTime[0].Split("/");
                        applicationDate = DOB[1] + "-" + DOB[0] + "-" + DOB[2];
                        Customer.KeyOfficialDOB = applicationDate;
                    }
                    else
                    {
                        Customer.KeyOfficialDOB = "";
                    }
                }
                else
                {
                    Customer.KeyOfficialDOB = "";
                }
                if (!string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedCarJeep) && Convert.ToInt32(Customer.FleetSizeNoOfVechileOwnedCarJeep) == 0)
                {
                    Customer.FleetSizeNoOfVechileOwnedCarJeep = "";
                }
                if (!string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedHCV) && Convert.ToInt32(Customer.FleetSizeNoOfVechileOwnedHCV) == 0)
                {
                    Customer.FleetSizeNoOfVechileOwnedHCV = "";
                }
                if (!string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedLCV) && Convert.ToInt32(Customer.FleetSizeNoOfVechileOwnedLCV) == 0)
                {
                    Customer.FleetSizeNoOfVechileOwnedLCV = "";
                }
                if (!string.IsNullOrEmpty(Customer.FleetSizeNoOfVechileOwnedMUVSUV) && Convert.ToInt32(Customer.FleetSizeNoOfVechileOwnedMUVSUV) == 0)
                {
                    Customer.FleetSizeNoOfVechileOwnedMUVSUV = "";
                }
                if (!string.IsNullOrEmpty(Customer.KeyOfficialApproxMonthlySpendsonVechile1) && Convert.ToDecimal(Customer.KeyOfficialApproxMonthlySpendsonVechile1) == 0)
                {
                    Customer.KeyOfficialApproxMonthlySpendsonVechile1 = "";
                }
                if (!string.IsNullOrEmpty(Customer.KeyOfficialApproxMonthlySpendsonVechile2) && Convert.ToDecimal(Customer.KeyOfficialApproxMonthlySpendsonVechile2) == 0)
                {
                    Customer.KeyOfficialApproxMonthlySpendsonVechile2 = "";
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
        }

        [HttpPost]
        public async Task<JsonResult> GetsalesAreaDetails(int RegionID)
        {
            List<SalesAreaModel> lst = new List<SalesAreaModel>();
            if (RegionID > 0)
            {
                lst = await _customerService.GetsalesAreaDetails(RegionID);
            }
            else
            {
                lst.Add(new SalesAreaModel
                {
                    SalesAreaID = 0,
                    SalesAreaName = "Select Sales Area"
                });
            }
            return Json(lst);
        }

        public async Task<IActionResult> SuccessUploadRedirect(string customerReferenceNo)
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

        public async Task<IActionResult> SuccessAddCardRedirect(string customerReferenceNo)
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

        public async Task<IActionResult> UpdateCustomer(string FormNumber)
        {
            var modals = await _customerService.UpdateCustomer(FormNumber);
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(CustomerModel cust)
        {

            var modals = await _customerService.UpdateCustomer(cust);

            if (cust.Internel_Status_Code == 1000)
            {
                return RedirectToAction("ValidateNewCustomer");
            }

            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> CheckPanCardDuplicationByDistrictidForCustomerUpdate(string DistrictId, string IncomeTaxPan, string CustomerReferenceNo)
        {
            var result = await _commonActionService.CheckPanCardDuplicationByDistrictidForCustomerUpdate(DistrictId, IncomeTaxPan, CustomerReferenceNo);

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> CheckVechileNoUsed(string VechileNo)
        {
            var Model = await _commonActionService.CheckVechileNoUsed(VechileNo);

            return Json(Model);
        }

        public async Task<IActionResult> GetCustomerCardsPartialView(string CustomerTypeId,string NoofCards, string NoofVehicles,string CardPreference)
        {
            var modals = await _customerService.GetCustomerCardsPartialView(CustomerTypeId, NoofCards, NoofVehicles, CardPreference);
            return PartialView("~/Views/Customer/_CustomerCardsTbl.cshtml", modals);
        }
    }
}