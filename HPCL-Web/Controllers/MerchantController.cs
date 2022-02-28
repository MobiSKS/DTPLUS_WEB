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
    public class MerchantController : Controller
    {
        private readonly IMerchantServices _merchantServices;
        private readonly ICommonActionService _commonActionService;

        public MerchantController(IMerchantServices merchantServices, ICommonActionService commonActionService)
        {
            _merchantServices = merchantServices;
            _commonActionService = commonActionService;
        }
        public async Task<IActionResult> CreateMerchant(string merchantIdValue, string fromDate, string toDate, string category)
        {
            var modals = await _merchantServices.CreateMerchant(merchantIdValue, fromDate, toDate, category);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMerchant(MerchantGetDetailsModel merchantMdl)
        {
            if (!string.IsNullOrEmpty(merchantMdl.SearchMerchantId))
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
    }
}
