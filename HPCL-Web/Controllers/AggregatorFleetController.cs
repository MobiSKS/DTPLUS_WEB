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

namespace HPCL_Web.Controllers
{
    public class AggregatorFleetController : Controller
    {
        private readonly IFleetService _fleetService;
        private readonly ICommonActionService _commonActionService;
        public AggregatorFleetController(IFleetService fleetService, ICommonActionService commonActionService)
        {
            _fleetService = fleetService;
            _commonActionService = commonActionService;
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
    }
}
