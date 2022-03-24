using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.Locations;
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

        public IActionResult RegionalOfficersList()
        {
            return View();
        }

        public async Task<IActionResult> CountryRegionList()
        {
            var countryRegionList = await _commonActionService.GetCountryRegion();
            return View(countryRegionList);
        }

        public async Task<IActionResult> StateList()
        {
            var stateList = await _commonActionService.GetStateList();
            return View(stateList);
        }

        public async Task<IActionResult> DistrictList()
        {
            string stateId = "0";
            var districtList = await _commonActionService.GetDistrictList(stateId);
            return View(districtList);
        }

        public async Task<IActionResult> CityList()
        {
            var cityList = await _commonActionService.GetCity();
            return View(cityList);
        }

        public async Task<IActionResult> DeleteCity(int cityId)
        {
            var cityList = await _locationServices.DeleteCity(cityId);
            return RedirectToAction("CityList");
        }
    }
}

