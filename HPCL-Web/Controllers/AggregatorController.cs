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
using System;
using HPCL.Common.Models.ViewModel.Aggregator;
using HPCL.Common.Models.ViewModel.ParentCustomer;
using Newtonsoft.Json;

namespace HPCL_Web.Controllers
{

    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class AggregatorController : Controller
    {
        private readonly IAggregatorService _aggregatorService;
        private readonly ICommonActionService _commonActionService;
        private readonly IParentCustomerService _ParentCustomerService;
        public AggregatorController(IAggregatorService aggregatorService, ICommonActionService commonActionService,IParentCustomerService parentCustomerService)
        {
            _aggregatorService = aggregatorService;
            _commonActionService = commonActionService;
            _ParentCustomerService = parentCustomerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            return View();
        }
        public async Task<IActionResult> ManageAggregator(string FromDate, string ToDate)
        {
            var modals = await _aggregatorService.ManageAggregator(FromDate, ToDate);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> ManageAggregator(ManageAggregatorViewModel cust)
        {

            var modals = await _aggregatorService.ManageAggregator(cust);

            if (cust.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirect", new { FormNumber = modals.FormNumber, Message = cust.Remarks });
            }
            else
            {
                ViewBag.Success = "false";
            }
            return View(modals);
        }
        public async Task<IActionResult> SuccessRedirect(string FormNumber, string Message)
        {
            ViewBag.FormNumber = FormNumber;
            ViewBag.Message = Message;
            return View();
        }

        public async Task<IActionResult> ValidateAggregatorCustomer(ValidateAggregatorCustomerModel entity,string reset)
        {
            var modals = await _aggregatorService.ValidateAggregatorCustomer(entity);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            return View(modals);
        }


        [HttpPost]
        public async Task<JsonResult> ViewCustomerDetails(string FormNumber)
        {
            if (!string.IsNullOrEmpty(FormNumber))
                FormNumber = FormNumber.Trim();

            JObject obj = await _aggregatorService.ViewCustomerDetails(FormNumber);

            var searchRes = obj["Data"].Value<JObject>();
            var customerResult = searchRes["GetAggregatorCustomerDetails"].Value<JArray>();

            var customerKYCDetailsResult = searchRes["AggregatorCustomerKYCDetails"].Value<JArray>();

            List<CustomerFullDetails> customerList = customerResult.ToObject<List<CustomerFullDetails>>();

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
            UpdateKycResponse updateKycResponse = await _aggregatorService.AproveCustomer(CustomerReferenceNo, Comments, Approvalstatus);
            return Json(updateKycResponse);
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

        public async Task<IActionResult> UpdateAggregatorCustomer(string FormNumber,string customerId,string requestId)
        {
            var modals = new ManageAggregatorViewModel();
            if (FormNumber!=null && FormNumber!="")
                modals = await _aggregatorService.UpdateAggregatorCustomer(FormNumber);
            if (customerId!=null && customerId!="")
            {
                var parentModal = await _ParentCustomerService.UpdateParentCustomer(customerId, requestId);
                if (parentModal != null)
                {
                    var reqEntity= JsonConvert.SerializeObject(parentModal);
                    modals = JsonConvert.DeserializeObject<ManageAggregatorViewModel>(reqEntity);
                }
            }
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAggregatorCustomer(ManageAggregatorViewModel cust)
        {

            var modals = await _aggregatorService.UpdateAggregatorCustomer(cust);

            if (cust.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectUpdateCustomer", new { FormNumber = modals.FormNumber, Message = cust.Remarks });
            }

            return View(modals);
        }
        public async Task<IActionResult> UploadAggregatorDoc(string customerReferenceNo, string FormNumber)
        {
            UploadDoc uploadDoc = new UploadDoc();
            uploadDoc.CustomerReferenceNo = "";
            uploadDoc.FormNumber = "";
            uploadDoc.IdProofType = 0;
            uploadDoc.AddressProofType = 0;
            uploadDoc.IdProofDocumentNo = "";
            uploadDoc.AddressProofDocumentNo = "";

            if (!string.IsNullOrEmpty(FormNumber))
            {
                uploadDoc.CustomerReferenceNo = customerReferenceNo;
                uploadDoc.Type = "1";
                if (!string.IsNullOrEmpty(FormNumber))
                {
                    var response = await _aggregatorService.UploadDoc(FormNumber);

                    if (response != null)
                    {
                        uploadDoc.FormNumber = FormNumber;
                        uploadDoc.IdProofType = Convert.ToInt32(response.IdProofTypeId);
                        uploadDoc.IdProofDocumentNo = response.IdProofDocumentNo;
                        uploadDoc.IdProofFrontSRC = response.IdProofFront;
                        uploadDoc.IdProofFrontimg = response.IdProofFront;
                        uploadDoc.IdProofBackimg = response.IdProofBack;
                        uploadDoc.AddressProofType = Convert.ToInt32(response.AddressProofTypeId);
                        uploadDoc.AddressProofDocumentNo = response.AddressProofDocumentNo;
                        uploadDoc.AddProofFrontimg = response.AddressProofFront;
                        uploadDoc.AddProofBackimg = response.AddressProofBack;
                    }
                }
            }
            return View(uploadDoc);
        }

        [HttpPost]
        public async Task<JsonResult> UploadAggregatorDoc(UploadDoc entity)
        {
            var searchCustomer = _aggregatorService.UploadDoc(entity);

            ModelState.Clear();
            return Json(new { searchCustomer = searchCustomer });
        }

        [HttpPost]
        public async Task<JsonResult> SaveUploadDoc(UploadDoc entity)
        {
            var reason = await _aggregatorService.SaveUploadDoc(entity);

            ModelState.Clear();
            return Json(reason);
        }
        public async Task<IActionResult> SuccessUploadRedirect(string FormNumber)
        {
            ViewBag.FormNumber = FormNumber;
            return View();
        }
        public async Task<IActionResult> SuccessRedirectUpdateCustomer(string FormNumber, string Message)
        {
            ViewBag.FormNumber = FormNumber;
            ViewBag.Message = Message;
            return View();
        }
        public async Task<IActionResult> SuccessUploadDocumentRedirect(string FormNumber)
        {
            ViewBag.FormNumber = FormNumber;
            return View();
        }
        
    }
}