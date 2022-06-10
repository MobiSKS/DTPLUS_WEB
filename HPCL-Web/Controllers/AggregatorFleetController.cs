using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.Aggregator;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using HPCL.Common.Models.ResponseModel.Aggregator;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class AggregatorFleetController : Controller
    {
        private readonly IFleetService _fleetService;
        private readonly ICommonActionService _commonActionService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AggregatorFleetController(IFleetService fleetService, ICommonActionService commonActionService, IWebHostEnvironment hostingEnvironment)
        {
            _fleetService = fleetService;
            _commonActionService = commonActionService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ManageFleetCustomer(string FromDate, string ToDate)
        {
            var modals = await _fleetService.ManageFleetCustomer(FromDate, ToDate);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> ManageFleetCustomer(ManageAggregatorViewModel cust)
        {

            var modals = await _fleetService.ManageFleetCustomer(cust);

            if (cust.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirect", new { customerReferenceNo = modals.CustomerReferenceNo, Message = cust.Remarks });
            }

            return View(modals);
        }
        public async Task<IActionResult> SuccessRedirect(string customerReferenceNo, string Message)
        {
            ViewBag.CustomerReferenceNo = customerReferenceNo;
            ViewBag.Message = Message;
            return View();
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

        public async Task<IActionResult> UploadFleetDoc(string customerReferenceNo, string FormNumber)
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
                uploadDoc.Type = "1";
                if (!string.IsNullOrEmpty(FormNumber))
                {
                    var response = await _fleetService.UploadDoc(FormNumber);

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
        public async Task<JsonResult> UploadFleetDoc(UploadDoc entity)
        {
            var searchCustomer = _fleetService.UploadDoc(entity);

            ModelState.Clear();
            return Json(new { searchCustomer = searchCustomer });
        }

        [HttpPost]
        public async Task<JsonResult> SaveUploadDoc(UploadDoc entity)
        {
            var reason = await _fleetService.SaveUploadDoc(entity);

            ModelState.Clear();
            return Json(reason);
        }
        public async Task<IActionResult> SuccessUploadRedirect(string customerReferenceNo)
        {
            ViewBag.CustomerReferenceNo = customerReferenceNo;
            return View();
        }
        public async Task<IActionResult> GetCustomerAddCardsPartialView([FromBody] List<CardDetails> objCardDetails)
        {
            var modals = await _fleetService.GetCustomerAddCardsPartialView(objCardDetails);
            return PartialView("~/Views/AggregatorFleet/_AddFleetCustomerCardDetailsView.cshtml", modals);
        }
        public async Task<IActionResult> AddCardDetails(string customerReferenceNo)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();
            customerCardInfo = await _fleetService.AddCardDetails(customerReferenceNo);
            customerCardInfo.Message = "";

            return View(customerCardInfo);
        }

        [HttpPost]
        public async Task<IActionResult> AddCardDetails(CustomerCardInfo customerCardInfo)
        {
            var Model = await _fleetService.AddCardDetails(customerCardInfo);

            if (Model.StatusCode == 1000)
            {
                customerCardInfo.Status = Model.Status;
                customerCardInfo.StatusCode = Model.StatusCode;
                return RedirectToAction("SuccessAddCardRedirect", new { customerReferenceNo = Model.CustomerReferenceNo, Message = Model.Reason });
            }

            return View(Model);
        }
        [HttpPost]
        public async Task<JsonResult> GetCustomerDetailsForAddCard(string customerReferenceNo)
        {
            CustomerCardInfo customerCardInfo = new CustomerCardInfo();

            customerCardInfo = await _fleetService.GetCustomerDetailsForAddCard(customerReferenceNo);

            return Json(customerCardInfo);
        }
        public async Task<IActionResult> SuccessAddCardRedirect(string customerReferenceNo, string Message)
        {
            ViewBag.CustomerReferenceNo = customerReferenceNo;
            ViewBag.Message = Message;
            return View();
        }
        public async Task<IActionResult> VerfiyFleetCustomer(ValidateAggregatorCustomerModel entity, string reset, string success, string error)
        {
            var modals = await _fleetService.VerfiyFleetCustomer(entity);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;
            return View(modals);
        }


        [HttpPost]
        public async Task<JsonResult> ViewCustomerDetails(string FormNumber)
        {
            if (!string.IsNullOrEmpty(FormNumber))
                FormNumber = FormNumber.Trim();

            JObject obj = await _fleetService.ViewCustomerDetails(FormNumber);

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


        public async Task<IActionResult> UpdateFleetCustomer(string FormNumber)
        {
            var modals = await _fleetService.UpdateFleetCustomer(FormNumber);
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFleetCustomer(ManageAggregatorViewModel cust)
        {

            var modals = await _fleetService.UpdateFleetCustomer(cust);

            if (cust.Internel_Status_Code == 1000)
            {
                return RedirectToAction("VerfiyFleetCustomer");
            }

            return View(modals);
        }

        public async Task<ActionResult> VerifyorRejectFleetCustomer(string CustomerId, string FormNumber, string CustomerStatus, string VerifyRemark)
        {
            var searchResult = await _fleetService.VerifyorRejectFleetCustomer(CustomerId, FormNumber, CustomerStatus, VerifyRemark);
            string succesMsg = "", errorMsg = "";
            if (searchResult.Count() > 0)
            {
                if (searchResult[0].Status == 1)
                    succesMsg = searchResult[0].Reason;
                else if (searchResult[0].Status == 0)
                    errorMsg = searchResult[0].Reason;
            }
            return RedirectToAction("VerfiyFleetCustomer", "AggregatorFleet", new { success = succesMsg, error = errorMsg });
        }

        public async Task<JsonResult> RejectFleetCustomer(string CustomerId, string FormNumber, string CustomerStatus, string VerifyRemark)
        {
            var searchResult = await _fleetService.VerifyorRejectFleetCustomer(CustomerId, FormNumber, CustomerStatus, VerifyRemark);
            return Json(searchResult);
        }
        public async Task<JsonResult> RejectApprovalFleetCustomer(string CustomerId, string FormNumber, string CustomerStatus, string ApprovedRemark)
        {
            var searchResult = await _fleetService.ApproveorRejectFleetCustomer(CustomerId, FormNumber, CustomerStatus, ApprovedRemark);
            return Json(searchResult);
        }
        public async Task<IActionResult> ApproveFleetCustomer(ValidateAggregatorCustomerModel entity, string reset, string success, string error)
        {
            var modals = await _fleetService.ApproveFleetCustomer(entity);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;
            return View(modals);
        }
        public async Task<ActionResult> ApproveorRejectFleetCustomer(string CustomerId, string FormNumber, string CustomerStatus, string ApprovedRemark)
        {
            var searchResult = await _fleetService.ApproveorRejectFleetCustomer(CustomerId, FormNumber, CustomerStatus, ApprovedRemark);
            string succesMsg = "", errorMsg = "";
            if (searchResult.Count() > 0)
            {
                if (searchResult[0].Status == 1)
                    succesMsg = searchResult[0].Reason;
                else if (searchResult[0].Status == 0)
                    errorMsg = searchResult[0].Reason;
            }
            return RedirectToAction("ApproveFleetCustomer", "AggregatorFleet", new { success = succesMsg, error = errorMsg });
        }
        public async Task<IActionResult> GetAggregatorFilesinVerify(string FormNumber)
        {
            var modals = await _fleetService.GetAggregatorFiles(FormNumber);

            List<FileModel> files = new List<FileModel>();
            List<string> downloads = new List<string>();
            if (modals != null)
            {
                if (modals.Data.Count() > 0)
                {
                    downloads.Add(modals.Data[0].CustomerAddressProof);
                    downloads.Add(modals.Data[0].IDProofofOwnerPartner);
                    downloads.Add((modals.Data[0].PANCarddetails));
                    downloads.Add(modals.Data[0].VehicleDetails);
                    downloads.Add(modals.Data[0].SignedCustomerForm);
                    using (var ms = new MemoryStream())
                    {
                        using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                        {
                            using (var client = new HttpClient())
                            {

                                foreach (string file in downloads)
                                {
                                    using (var result = await client.GetAsync(file))
                                    {
                                        if (result.IsSuccessStatusCode)
                                        {
                                            byte[] bytes = await result.Content.ReadAsByteArrayAsync();
                                            var zipEntry = archive.CreateEntry(file,
                                            CompressionLevel.Fastest);
                                            using (var zipStream = zipEntry.Open())
                                            {
                                                zipStream.Write(bytes, 0, bytes.Length);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        return File(ms.ToArray(), "application/zip", "Aggregator.zip");
                    }
                }
            }
            return RedirectToAction("VerfiyFleetCustomer", "AggregatorFleet", new { success = "", error = "" });
        }
        public async Task<IActionResult> GetAggregatorFilesinApprove(string FormNumber)
        {
            var modals = await _fleetService.GetAggregatorFiles(FormNumber);

            List<FileModel> files = new List<FileModel>();
            List<string> downloads = new List<string>();
            if (modals != null)
            {
                if (modals.Data.Count() > 0)
                {
                    downloads.Add(modals.Data[0].CustomerAddressProof);
                    downloads.Add(modals.Data[0].IDProofofOwnerPartner);
                    downloads.Add((modals.Data[0].PANCarddetails));
                    downloads.Add(modals.Data[0].VehicleDetails);
                    downloads.Add(modals.Data[0].SignedCustomerForm);
                    using (var ms = new MemoryStream())
                    {
                        using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                        {
                            using (var client = new HttpClient())
                            {

                                foreach (string file in downloads)
                                {
                                    var fileName = Path.GetFileName(file);
                                    using (var result = await client.GetAsync(file))
                                    {
                                        if (result.IsSuccessStatusCode)
                                        {
                                            byte[] bytes = await result.Content.ReadAsByteArrayAsync();
                                            var zipEntry = archive.CreateEntry(fileName,
                                            CompressionLevel.Fastest);
                                            using (var zipStream = zipEntry.Open())
                                            {
                                                zipStream.Write(bytes, 0, bytes.Length);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        return File(ms.ToArray(), "application/zip", "Aggregator.zip");
                    }
                }
            }
            return RedirectToAction("ApproveFleetCustomer", "AggregatorFleet", new { success = "", error = "" });
        }
    }
}
