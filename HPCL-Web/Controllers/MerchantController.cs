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
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            return View();
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
            if (!string.IsNullOrEmpty(merchantMdl.SearchMerchantId) || string.IsNullOrEmpty(merchantMdl.SearchMerchantId))
            {
                return RedirectToAction("CreateMerchant", new { MerchantIDValue = merchantMdl.SearchMerchantId });
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
            }
            else
            {
                merchantMdl.Error = "";
                merchantMdl.Success = tuple.Item2;
            }

            ViewBag.Reason = tuple.Item2;
            ViewBag.Status = tuple.Item1;
            return View(merchantMdl);
        }
        public async Task<IActionResult> VerifyMerchant(MerchantApprovalModel merchaApprovalMdl)
        {
            var modals = await _merchantServices.VerifyMerchant(merchaApprovalMdl);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> ActionOnMerchantID([FromBody] ApproveRejectListRequestModal approvalRejectionMdl)
        {
            var reason = await _merchantServices.ActionOnMerchantID(approvalRejectionMdl);
            return Json(reason);
        }
        public async Task<IActionResult> RejectedMerchant(MerchantApprovalModel merchaApprovalMdl)
        {
            var modals = await _merchantServices.RejectedMerchant(merchaApprovalMdl);
            return View(modals);
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
