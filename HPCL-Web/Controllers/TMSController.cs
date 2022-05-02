using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.TMS;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            string path = "";
            path = Path.Combine(contentRootPath, "Views\\TMS");
            path = Path.Combine(path, fileName);

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

    }
}
