using HPCL.Common.Helper;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class TMSController : Controller
    {
        private readonly ITMSService _tmsService;
        private readonly ICommonActionService _commonActionService;
        public TMSController(ITMSService tmsService, ICommonActionService commonActionService)
        {
            _tmsService = tmsService;
            _commonActionService = commonActionService;
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
    }
}
