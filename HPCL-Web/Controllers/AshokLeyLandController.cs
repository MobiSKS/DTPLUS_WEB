using HPCL.Common.Helper;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class AshokLeyLandController : Controller
    {
        private readonly IAshokLeyLandService _ashokLeyLandService;

        public AshokLeyLandController(IAshokLeyLandService ashokLeyLandService)
        {
            _ashokLeyLandService = ashokLeyLandService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AlEnroll()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AlEnroll(string str)
        {
            var result = await _ashokLeyLandService.InsertAlEnroll(str);
            return Json(new { result = result });
        }

        [HttpPost]
        public async Task<JsonResult> SearchDealer(string dealerCode, string dtpCode)
        {
            var searchList = await _ashokLeyLandService.SearchDealer(dealerCode, dtpCode);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> AlEnrollUpdate(string getAllData)
        {
            var result = await _ashokLeyLandService.AlEnrollUpdate(getAllData);
            return Json(new { result = result });
        }
    }
}
