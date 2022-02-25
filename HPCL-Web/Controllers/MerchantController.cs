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

        public MerchantController(IMerchantServices merchantServices)
        {
            _merchantServices = merchantServices;
        }
        public async Task<IActionResult> CreateMerchant(string merchantIdValue, string fromDate, string toDate, string category, string reason)
        {
            var modals = await _merchantServices.CreateMerchant(merchantIdValue, fromDate, toDate, category);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMerchant(MerchantGetDetailsModel merchantMdl)
        {
            var reason = await _merchantServices.CreateMerchant(merchantMdl);
            return RedirectToAction("CreateMerchant", "Merchant", new { reason = reason });
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
