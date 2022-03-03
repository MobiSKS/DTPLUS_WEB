using HPCL.Common.Helper;
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
        public LocationsController(ILocationServices locationServices)
        {
            _locationServices = locationServices;
        }

        public async Task<IActionResult> HeadOfficeDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> HeadOfficeDetails(HeadOfficeDetails headOfficeDetails)
        {
            var response = await _locationServices.HeadOfficeDetails(headOfficeDetails);

            ModelState.Clear();
            return Json(response);
        }
    }
}

