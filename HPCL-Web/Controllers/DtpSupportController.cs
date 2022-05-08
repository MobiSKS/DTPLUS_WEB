using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.DtpSupport;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class DtpSupportController : Controller
    {
        private readonly IDtpSupportService _dtpSupportService;
        private readonly ICommonActionService _commonActionService;
        public DtpSupportController(IDtpSupportService dtpSupportService, ICommonActionService commonActionService)
        {
            _dtpSupportService = dtpSupportService;
            _commonActionService = commonActionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BlockUnBlockCustomerCcmsAccount()
        {
            return View();
        }
        public IActionResult CardBalanceTransfer()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SearchCustomerCcmsAccount(string customerId)
        {
            var searchResult = await _dtpSupportService.GetBlockUnblockCustomerCcmsAccount(customerId);
            return Json(new { searchResult = searchResult });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateBlockUnBlockCcmsAccount(BlockUnblockCustomerCcmsAccount entity)
        {
            var searchResult = await _dtpSupportService.UpdateCustomerCcmsAccountStatus(entity);
            return Json(searchResult);
        }
        public async Task<JsonResult> GetCardBalanceTransferDetails(string CardNo)
        {
            var searchResult = await _dtpSupportService.GetCardBalanceTransferDetails(CardNo);
            return Json(searchResult);
        }
        public async Task<IActionResult> UnblockUser()
        {
            var modals = await _dtpSupportService.UnblockUser();
            return View(modals);
        }
        public async Task<IActionResult> GeneralUpdates()
        {
            var modals = await _dtpSupportService.GeneralUpdates();
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> GetEntityFieldModelList(string EntityTypeId)
        {
            List<GetEntityFieldModel> lstEntityField = new List<GetEntityFieldModel>();

            if (Convert.ToInt32(string.IsNullOrEmpty(EntityTypeId) ? "0" : EntityTypeId) > 0)
            {
                lstEntityField = await _commonActionService.GetEntityFieldModelList(EntityTypeId);
            }
            else
            {
                lstEntityField.Add(new GetEntityFieldModel
                {
                    entityFieldId = 0,
                    entityFieldName = "Select"
                });
            }            
            
            return Json(lstEntityField);
        }
        [HttpPost]
        public async Task<JsonResult> GetEntityOldFieldValue(string EntityTypeId, string EntityFieldId, string CustomerIdOrCardOrMerchantId)
        {
            var searchResult = await _dtpSupportService.GetEntityOldFieldValue(EntityTypeId, EntityFieldId, CustomerIdOrCardOrMerchantId);
            return Json(new { searchResult = searchResult });
        }

        [HttpPost]
        public async Task<IActionResult> GeneralUpdates(GeneralUpdatesModel model)
        {
            var Model = await _dtpSupportService.GeneralUpdates(model);
            
            return View(Model);
        }
        [HttpPost]
        public async Task<JsonResult> GetsalesAreaList(String RegionID)
        {
            List<SalesAreaResponseModal> lst = new List<SalesAreaResponseModal>();
            lst = await _commonActionService.GetSalesAreaList(RegionID);
            lst.Insert(0, new SalesAreaResponseModal
            {
                SalesAreaID = 0,
                SalesAreaName = "Select"
            });

            return Json(lst);
        }

    }
}
