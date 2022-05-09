using HPCL.Common.Models.ViewModel.DtpSupport;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class DtpSupportController : Controller
    {
        private readonly IDtpSupportService _dtpSupportService;
        public DtpSupportController(IDtpSupportService dtpSupportService)
        {
            _dtpSupportService = dtpSupportService;
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
        public async Task<IActionResult> TeamMapping(TeamMappingViewModel teamMappingViewModel, string reset)
        {
            var searchResult = await _dtpSupportService.TeamMappingSearch(teamMappingViewModel);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            return View(searchResult);
        }
        [HttpPost]
        public async Task<IActionResult> AddTeamMapping(TeamMappingViewModel teamMappingViewModel)
        {
            var searchResult = await _dtpSupportService.AddTeamMapping(teamMappingViewModel);
            return Json(searchResult);
        }

    }
}
