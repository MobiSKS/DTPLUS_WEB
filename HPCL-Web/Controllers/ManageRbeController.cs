using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.ManageRbe;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class ManageRbeController : Controller
    {
        private readonly IManageRbeService _manageRbeService;

        public ManageRbeController(IManageRbeService manageRbeService)
        {
            _manageRbeService = manageRbeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeRbeMapping()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ChangeRbeMapping(RbeMapping entity)
        {
            var searchList = await _manageRbeService.ChangeRbeMapping(entity);
            return Json(new { searchList = searchList });
        }

        public IActionResult RbeUserList()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RbeUserList(RbeUserList entity)
        {
            var searchList = await _manageRbeService.RbeUserList(entity);
            return Json(new { searchList = searchList });
        }

        public IActionResult RbeDeviceIdResetRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RbeDeviceIdResetRequest(RbeDeviceIdResetRequest entity)
        {
            var searchList = await _manageRbeService.RbeDeviceIdResetRequestService(entity);
            return Json(new { searchList = searchList });
        }
    }
}
