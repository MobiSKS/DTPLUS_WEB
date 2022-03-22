using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.Locations;
using HPCL.Common.Models.ViewModel.Locations;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class LocationsController : Controller
    {
        private readonly ILocationServices _locationServices;
        private readonly ICommonActionService _commonActionService;
        public LocationsController(ILocationServices locationServices, ICommonActionService commonActionService)
        {
            _locationServices = locationServices;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> HeadOfficeDetails()
        {
            var response = await _locationServices.HeadOfficeDetails();
            return View(response);
        }

        [HttpPost]
        public async Task<JsonResult> HeadOfficeUpdate(HeadOfficeDetailsResponse entity)
        {
            var response = await _locationServices.UpdateHod(entity);
            ViewBag.HodUpdateRes = response;
            return Json(response);
        }

        public async Task<IActionResult> ZonalOfficersList()
        {
            var response = await _commonActionService.GetZonalOfficeList();
            return View(response);
        }
    }
}

