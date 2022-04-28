using HPCL.Common.Models.ViewModel.DtpSupport;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        
    }
}
