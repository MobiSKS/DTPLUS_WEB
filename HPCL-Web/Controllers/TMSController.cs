using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.TMS;
using HPCL.Common.Models.ViewModel.TMS;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class TMSController : Controller
    {
        private readonly ITMSService _tmsService;
        private readonly ICommonActionService _commonActionService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public TMSController(ITMSService tmsService, ICommonActionService commonActionService, IWebHostEnvironment hostingEnvironment)
        {
            _tmsService = tmsService;
            _commonActionService = commonActionService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EnrollToTransportManagementSystem()
        {
            var modals = await _tmsService.EnrollToTransportManagementSystem();
            return View(modals);
        }

        public async Task<ActionResult> ViewCustomerDetails(string CustomerId)
        {
            var model = await _tmsService.ViewCustomerDetails(CustomerId);

            return PartialView("~/Views/TMS/_ViewCustomerTblView.cshtml", model);
        }
        public FileResult DownloadFile(string fileName)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
   
            string path = "";
            path = Path.Combine(webRootPath, "MyStaticFiles", fileName);

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollToTransportManagementSystem(EnrollToTransportManagementSystemModel model)
        {
            var Model = await _tmsService.EnrollToTransportManagementSystem(model);

            if (Model.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessEnrollToTMS", new { Message = Model.Message });
            }

            return View(Model);
        }
        public async Task<IActionResult> SuccessEnrollToTMS(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        public async Task<IActionResult> EnrollVehicle()
        {
            var modals = await _tmsService.EnrollVehicle();
            return View(modals);
        }
        public async Task<IActionResult> GetEnrollVehicleManagementDetail(string customerId, int enrollmentStatus, string vehicleNo, string cardNo)
        {
            var modals = await _tmsService.GetEnrollVehicleManagementDetail(customerId, enrollmentStatus, vehicleNo, cardNo);
            return PartialView("~/Views/TMS/_EnrollVehicleTbl.cshtml", modals);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerDetailsForView(string CustomerId)
        {
            var model = await _tmsService.ViewCustomerDetails(CustomerId);

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitVehicleEnrollment([FromBody] EnrollVehicleViewModel enrollVehicleViewModel)
        {
            var result = await _tmsService.SubmitVehicleEnrollment(enrollVehicleViewModel);
            return Json(result);
        }
        public async Task<IActionResult> ManageEnrollments()
        {
            var modals = await _tmsService.ManageEnrollments();
            return View(modals);
        }
        public async Task<ActionResult> ViewCustomerDetailsForManageEnrollments(string CustomerId)
        {
            var model = await _tmsService.ViewCustomerDetailsForManageEnrollments(CustomerId);

            return PartialView("~/Views/TMS/_ViewCustomerTblEnrollments.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTMSEnrollmentStatus([FromBody] ManageEnrollmentsModel manageEnrollmentsModel)
        {
            var result = await _tmsService.UpdateTMSEnrollmentStatus(manageEnrollmentsModel);
            return Json(result);
        }
        public async Task<IActionResult> SwitchToCargoFL()
        {
            var modals = await _tmsService.SwitchToCargoFL();
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> SwitchToCargoFL(NavigateToTransportManagementSystemModel model)
        {
            var Model = await _tmsService.SwitchToCargoFL(model);

            if (Model.StatusCode == 1000)
            {
                if (!string.IsNullOrEmpty(Model.url))
                {
                    string url = Model.url;

                    if (!(url.Contains("http:")))
                    {
                        if (!url.Contains("https:"))
                        {
                            url = "http://" + url;
                        }
                    }
                    if (!(url.Contains("https:")))
                    {
                        if (!url.Contains("http:"))
                        {
                            url = "http://" + url;
                        }
                    }

                    return Redirect(url);
                }
                else
                {
                    return View(Model);
                }
            }

            return View(Model);
        }

        public async Task<IActionResult> ApproveEnrollments(EnrollmentsApprovalModel model, string reset, string success, string error, string CustomerID, string TMSUserId, string FromDate, string ToDate, string TMSStatus)
        {
            var searchResult = await _tmsService.ApproveEnrollments(model);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;
            if (!String.IsNullOrEmpty(reset))
            {
                model.TMSStatus = 1;
                model.FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
                model.ToDate = DateTime.Now.ToString("dd-MM-yyyy");
            }
            return View(searchResult);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCustomerDetailForEnrollmentApproval([FromBody] UpdateCustomerDetailForEnrollmentApprovalRequest model)
        {
            var updateKycResponse = await _tmsService.UpdateCustomerDetailForEnrollmentApproval(model);

            return Json(new { customer = updateKycResponse });
        }

    }
}
