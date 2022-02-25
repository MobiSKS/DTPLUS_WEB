using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.ViewCard;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class ViewCardController : Controller
    {

        private readonly IViewCardService _ViewService;

        public ViewCardController(IViewCardService ViewServices)
        {
            _ViewService = ViewServices;
        }


        public async Task<IActionResult> ViewCardSearch()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ViewCardSearch(ViewCardDetails entity)
        {
            var searchList = await _ViewService.ViewCardSearch(entity);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> SearchCardMapping(string Customerid)
        {
            var searchList = await _ViewService.SearchCardMapping(Customerid);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCards(ObjUpdateMobileandFastagNoInCard[] limitArray)
        {
            var reason = await _ViewService.UpdateCards(limitArray);

            ModelState.Clear();
            return Json(reason);
        }
    }

}


