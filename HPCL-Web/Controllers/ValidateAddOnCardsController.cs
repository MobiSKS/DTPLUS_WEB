using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.ValidateAddOnCards;
using HPCL.Common.Models.ViewModel.ValidateNewCards;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class ValidateAddOnCardsController : Controller
    {
        private readonly IValidateAddOnCardsService _validateNewCardsService;
        private readonly ICommonActionService _commonActionService;

        public ValidateAddOnCardsController(IValidateAddOnCardsService validateNewCardsService, ICommonActionService commonActionService)
        {
            _validateNewCardsService = validateNewCardsService;
            _commonActionService = commonActionService;
        }

        public async Task<IActionResult> Details(ValidateAddOnCardsModel validateNewCardsMdl, string reset)
        {
            var modals = await _validateNewCardsService.Details(validateNewCardsMdl);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            List<StateResponseModal> lstState = new List<StateResponseModal>();
            lstState = await _commonActionService.GetStateList();
            modals.States.AddRange(lstState);
            return View(modals);
        }
        public async Task<IActionResult> GetCardDetailsForApproval(string CustomerRefNo, string CustomerType)
        {
            ViewBag.CustomerType = CustomerType;
            var modals = await _validateNewCardsService.GetCardDetailsForApproval(CustomerRefNo);
            return PartialView("~/Views/ValidateAddOnCards/_GetValidateFormDetails.cshtml", modals);
        }
        public async Task<IActionResult> ActionOnCards([FromBody] ApproveCardDetailsModel approveRejectModel)
        {
            var commonResponseData = await _validateNewCardsService.ActionOnCards(approveRejectModel);
            return Json(new { commonResponseData = commonResponseData });
        }

    }
}
