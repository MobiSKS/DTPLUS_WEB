using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Hotlisting;
using HPCL.Common.Models.ViewModel.Hotlisting;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class HotlistingController : Controller
    {
        private readonly IHotlistingService _hotlistingService;
        private readonly ICommonActionService _commonActionService;
        public HotlistingController(IHotlistingService hotlistingService, ICommonActionService commonActionService)
        {
            _hotlistingService = hotlistingService;
            _commonActionService = commonActionService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> HotlistAndReactivate()
        {
            HotlistorReactivateViewModel HotlistorReactivateMdl = new HotlistorReactivateViewModel();

            var entitytype = await _commonActionService.GetEntityTypeList();
            HotlistorReactivateMdl.HotlistEntity.AddRange(entitytype);

        
            return View(HotlistorReactivateMdl);
        }
        [HttpPost]
        public async Task<IActionResult> ApplyHotlistorReactivate([FromBody] HotlistorReactivateViewModel HotlistorReactivateResponseModel)
        {
            var result = await _hotlistingService.ApplyHotlistorReactivate(HotlistorReactivateResponseModel);
            return Json(result);
        }
        public async Task<IActionResult> ViewHotlistedEntity()
        {
            HotlistorReactivateViewModel HotlistorReactivateMdl = new HotlistorReactivateViewModel();

            var entitytype = await _commonActionService.GetEntityTypeList();
            HotlistorReactivateMdl.HotlistEntity.AddRange(entitytype);


            return View(HotlistorReactivateMdl);
        }
        [HttpPost]
        public async Task<IActionResult> GetHotlistedorReactivatedData(string EntityTypeId,string EntityId)
        {
            var result = await _hotlistingService.GetHotlistedorReactivatedData(EntityTypeId, EntityId);
            return Json(result);
        }
        public async Task<IActionResult> HotlistApprovalView()
        {
            HotlistorReactivateViewModel HotlistorReactivateMdl = new HotlistorReactivateViewModel();

            var entitytype = await _commonActionService.GetEntityTypeList();
            HotlistorReactivateMdl.HotlistEntity.AddRange(entitytype);


            return View(HotlistorReactivateMdl);
        }
        public async Task<IActionResult> GetHotlistApproval(string EntityTypeId,string ActionId,string FromDate,string ToDate)
        {
            var result = await _hotlistingService.GetHotlistApproval(EntityTypeId, ActionId, FromDate, ToDate);
            return PartialView("~/Views/Hotlisting/_HotlistApprovalTblView.cshtml", result);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateHotlistApproval([FromBody] HotlistApprovalRequest hotlistApprovalRequest)
        {
            var result = await _hotlistingService.UpdateHotlistApproval(hotlistApprovalRequest);
            return Json(result);
        }
    }
}
