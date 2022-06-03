using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.DtpSupport;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using HPCL.Common.Helper;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
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

        public async Task<IActionResult> TeamMapping(TeamMappingViewModel teamMappingViewModel, string reset,string success,string error)
        { 
            var searchResult = await _dtpSupportService.TeamMappingSearch(teamMappingViewModel);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;
            return View(searchResult);
        }
        [HttpPost]
        public async Task<IActionResult> AddTeamMapping(TeamMappingViewModel teamMappingViewModel)
        {
            var searchResult = await _dtpSupportService.AddTeamMapping(teamMappingViewModel);
            return Json(searchResult);
        }
        public async Task<IActionResult> DeleteTeamMapping(string teamMappingId)
        {
            var searchResult = await _dtpSupportService.DeleteTeamMapping(teamMappingId);
            string succesMsg = "", errorMsg = "";
            if(searchResult[0].Status==1)
                succesMsg = searchResult[0].Reason;
            else if (searchResult[0].Status==0)
                errorMsg = searchResult[0].Reason;
            return RedirectToAction("TeamMapping", "DtpSupport",new { success = succesMsg,error=errorMsg});
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTeamMapping([FromBody]TeamMappingViewModel teamMappingViewModel)
        {
            var searchResult = await _dtpSupportService.UpdateTeamMapping(teamMappingViewModel);
            return Json(searchResult);
           
        }
        [HttpPost]
        public async Task<JsonResult> GetDetailForUserUnblock(string CustomerId, string UserName)
        {
            var searchResult = await _dtpSupportService.GetDetailForUserUnblock(CustomerId, UserName);
            return Json(new { searchResult = searchResult });
        }
        [HttpPost]
        public async Task<IActionResult> UnblockUser(UnblockUserModel model)
        {
            var Model = await _dtpSupportService.UnblockUser(model);

            return View(Model);
        }

    }
}
