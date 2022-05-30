using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Merchant;
using HPCL.Common.Models.ViewModel.Merchant;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class MerchantController : Controller
    {
        private readonly IMerchantServices _merchantServices;
        private readonly ICommonActionService _commonActionService;

        public MerchantController(IMerchantServices merchantServices, ICommonActionService commonActionService)
        {
            _merchantServices = merchantServices;
            _commonActionService = commonActionService;
        }
        public async Task<IActionResult> Index()
        {
            return View(SessionMenuModel.menuList);
        }
        public async Task<IActionResult> Profile()
        {
            return View(SessionMenuModel.menuList);
        }
        public async Task<IActionResult> CreateMerchant(string merchantIdValue, string fromDate, string toDate, string category, string ERPCode, string actionFlow)
        {
            var modals = await _merchantServices.CreateMerchant(merchantIdValue, fromDate, toDate, category, ERPCode, actionFlow);
            if (!string.IsNullOrEmpty(modals.Message))
            {
                return View(modals);
            }

            if (!string.IsNullOrEmpty(ERPCode))
                ViewBag.RejectedStatus = "true";
            else
                ViewBag.RejectedStatus = "";
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMerchant(MerchantGetDetailsModel merchantMdl)
        {
            if (!string.IsNullOrEmpty(merchantMdl.SearchMerchantId) || merchantMdl.Search == "Search")
            {
                return RedirectToAction("CreateMerchant", new { MerchantIDValue = merchantMdl.SearchMerchantId, actionFlow = "Edit" });
            }
            var tuple = await _merchantServices.CreateMerchant(merchantMdl);

            if (tuple.Item1 == "0")
            {
                merchantMdl.MerchantTypes.AddRange(await _commonActionService.GetMerchantTypeList());
                merchantMdl.OutletCategories.AddRange(await _commonActionService.GetOutletCategoryList());
                merchantMdl.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
                merchantMdl.RetailOutletStates.AddRange(await _commonActionService.GetStateList());
                merchantMdl.CommStates.AddRange(await _commonActionService.GetStateList());
                merchantMdl.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
                merchantMdl.Error = tuple.Item2;
                merchantMdl.Success = "";
                ViewBag.Reason = tuple.Item2;
                ViewBag.Status = tuple.Item1;
                return View(merchantMdl);
            }
            else
            {
                MerchantGetDetailsModel merchantMdl2 = new MerchantGetDetailsModel();
                merchantMdl2.MerchantTypes.AddRange(await _commonActionService.GetMerchantTypeList());
                merchantMdl2.OutletCategories.AddRange(await _commonActionService.GetOutletCategoryList());
                merchantMdl2.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
                merchantMdl2.RetailOutletStates.AddRange(await _commonActionService.GetStateList());
                merchantMdl2.CommStates.AddRange(await _commonActionService.GetStateList());
                merchantMdl2.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
                merchantMdl2.Error = "";
                merchantMdl2.Success = tuple.Item2;
                ModelState.Clear();
                ViewBag.Reason = tuple.Item2;
                ViewBag.Status = tuple.Item1;
                return View(merchantMdl2);
            }
        }
        
        public async Task<IActionResult> VerifyMerchant(MerchantApprovalModel merchaApprovalMdl,string reset)
        {
            var modals = await _merchantServices.VerifyMerchant(merchaApprovalMdl);
            ViewBag.Reset =String.IsNullOrEmpty(reset)?"":reset;
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> ActionOnMerchantID([FromBody] ApproveRejectListRequestModal approvalRejectionMdl)
        {
            var reason = await _merchantServices.ActionOnMerchantID(approvalRejectionMdl);
            return Json(reason);
        }
        public async Task<IActionResult> RejectedMerchant()
        {
            MerchantRejectedModel modals = new MerchantRejectedModel();
            return View(modals);
        }
        public async Task<IActionResult> SearchRejectedMerchants(string FromDate, string ToDate)
        {
            var modals = await _merchantServices.RejectedMerchant(FromDate,ToDate);
            return PartialView("~/Views/Merchant/_RejectedMerchantsViewTbl.cshtml", modals);
        }
        public async Task<IActionResult> MerchantSummary(string ERPCode, string fromDate, string toDate)
        {
            var modals = await _merchantServices.MerchantSummary(ERPCode, fromDate, toDate);
            return View(modals);
        }

        public async Task<IActionResult> SearchMerchant()
        {
            var modals = await _merchantServices.SearchMerchant();
            return View(modals);
        }
        public async Task<IActionResult> SearchMerchantDetails(string merchantId, string erpCode, string retailOutletName, string city, string highwayNo, string status)
        {
            var modals = await _merchantServices.SearchMerchantDetails(merchantId, erpCode, retailOutletName, city, highwayNo, status);
            return PartialView("~/Views/Merchant/_SearchResultForMerchantTable.cshtml", modals);
        }
    }
}
