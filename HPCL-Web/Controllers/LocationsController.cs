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

        #region "Head Office Details"
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
        #endregion

        #region "Zonal Office"
        public async Task<IActionResult> ZonalOfficersList()
        {
            var response = await _commonActionService.GetZonalOfficeList();
            return View(response);
        }

        public async Task<IActionResult> DeleteZonalOffice(int zonalOfficeID)
        {
            var cityLists = await _locationServices.DeleteZonalOffice(zonalOfficeID);
            return RedirectToAction("ZonalOfficersList");
        }
        #endregion

        #region "Regional Office"
        public IActionResult RegionalOfficersList()
        {
            return View();
        }

        public async Task<IActionResult> DeleteRegionalOffice(int regionalOfficeID)
        {
            var cityLists = await _locationServices.DeleteRegionalOffice(regionalOfficeID);
            return RedirectToAction("RegionalOfficersList");
        }
        #endregion

        #region "Country Region"
        public async Task<IActionResult> CountryRegionList()
        {
            var countryRegionList = await _commonActionService.GetCountryRegion();
            return View(countryRegionList);
        }

        public async Task<IActionResult> DeleteCountryRegion(int regionID)
        {
            var cityLists = await _locationServices.DeleteCountryRegion(regionID);
            return RedirectToAction("CountryRegionList");
        }
        #endregion

        #region "State"
        public async Task<IActionResult> StateList()
        {
            var stateList = await _commonActionService.GetStateList();
            return View(stateList);
        }

        public async Task<IActionResult> DeleteState(int stateID)
        {
            var cityLists = await _locationServices.DeleteState(stateID);
            return RedirectToAction("StateList");
        }
        #endregion

        #region "District"
        public async Task<IActionResult> DistrictList()
        {
            string stateId = "0";
            var districtList = await _commonActionService.GetDistrictList(stateId);
            return View(districtList);
        }

        public async Task<IActionResult> DeleteDistrict(int districtID)
        {
            var cityLists = await _locationServices.DeleteDistrict(districtID);
            return RedirectToAction("DistrictList");
        }
        #endregion

        #region "City"
        public async Task<IActionResult> CityList()
        {
            var cityList = await _commonActionService.GetCity();
            return View(cityList);
        }

        public async Task<IActionResult> DeleteCity(int cityId)
        {
            var cityLists = await _locationServices.DeleteCity(cityId);
            return RedirectToAction("CityList");
        }
        #endregion 
    }
}

