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
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class ViewCardController : Controller
    {

        private readonly IViewCardService _ViewService;

        public ViewCardController(IViewCardService ViewServices)
        {
            _ViewService = ViewServices;
        }


        public async Task<IActionResult> AddOrEditMobile(string CustomerId)
        {
            ViewBag.CustomerId = CustomerId;    
            return View();
        }
        public async Task<IActionResult> ViewCardLimits()
        {
            return View();
        }
        
        
        public async Task<ActionResult> ViewCardSearch(string CustomerId)
        {
            var searchList = await _ViewService.ViewCardSearch(CustomerId);

            return PartialView("~/Views/ViewCard/_CardLimitsSearchTblView.cshtml", searchList);
        }

        [HttpPost]
        public async Task<JsonResult> SearchCardMapping(ViewCardDetails viewCardDetails)
        {
            var searchList = await _ViewService.SearchCardMapping(viewCardDetails);

            ModelState.Clear();
            return Json(searchList);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCards(ObjUpdateMobileandFastagNoInCard[] limitArray)
        {
            var reason = await _ViewService.UpdateCards(limitArray);

            ModelState.Clear();
            return Json(reason);
        }
        [HttpPost]
        public async Task<JsonResult> AddCardMappingDetails(ViewCardDetails viewCardDetails)
        {
            var searchList = await _ViewService.AddCardMappingDetails(viewCardDetails);

            ModelState.Clear();
            return Json(searchList);
        }
    }

}


