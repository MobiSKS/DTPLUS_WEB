using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ViewModel.VolvoEicher;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class VolvoEicherController : Controller
    {
        private readonly IVolvoEicherService _volvoEicherService;
        private readonly ICommonActionService _commonActionService;
        public VolvoEicherController(IVolvoEicherService volvoEicherService, ICommonActionService commonActionService)
        {
            _volvoEicherService = volvoEicherService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewVEDealerOTCDCardStatus()
        {
            VEDealerOTCCardStatusViewModel modals = new VEDealerOTCCardStatusViewModel();
            return View(modals);
        }

        public async Task<IActionResult> GetVEDealerCardStatus(string DealerCode)
        {
            var modals = await _volvoEicherService.GetVEDealerCardStatus(DealerCode);
            return PartialView("~/Views/VolvoEicher/_VEDealerCardStatusTbl.cshtml", modals);
        }
        [HttpPost]
        public async Task<JsonResult> BindCustomerDetails(string CustomerId, string NameOnCard)
        {

            var customerCardInfo = await _volvoEicherService.BindCustomerDetails(CustomerId, NameOnCard);
            ModelState.Clear();
            return Json(customerCardInfo);
        }
        public async Task<IActionResult> ManageProfile()
        {
            VEManageProfile custMdl = new VEManageProfile();

            custMdl.CustomerTypeMdl.AddRange(await _commonActionService.GetCustomerTypeListDropdown());

       
            custMdl.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType("1"));

            custMdl.CustomerTbentityMdl.AddRange(await _commonActionService.GetCustomerTbentityListDropdown());

            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());

            custMdl.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

            custMdl.CustomerTypeOfFleetMdl.AddRange(await _commonActionService.GetCustomerTypeOfFleetDropdown());

            return View(custMdl);

        }
        public IActionResult VEDealerEnrollment()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> VEDealerEnrollment(string str)
        {
            var result = await _volvoEicherService.InsertVEDealerEnrollment(str);
            return Json(new { result = result });
        }

        [HttpPost]
        public async Task<JsonResult> SearchVEDealer(string dealerCode, string dtpCode)
        {
            var searchList = await _volvoEicherService.SearchVEDealer(dealerCode, dtpCode);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> VEDealerEnrollmentUpdate(string getAllData)
        {
            var result = await _volvoEicherService.VEDealerEnrollmentUpdate(getAllData);
            return Json(new { result = result });
        }

    }
}
