using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Common.Models.ViewModel.ValidateNewCards;
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
    public class ValidateNewCardsController : Controller
    {
        private readonly IValidateNewCardsService _validateNewCardsService;
        private readonly ICommonActionService _commonActionService;

        public ValidateNewCardsController(IValidateNewCardsService validateNewCardsService, ICommonActionService commonActionService)
        {
            _validateNewCardsService = validateNewCardsService;
            _commonActionService = commonActionService;
        }
        public async Task<IActionResult> Details(ValidateNewCardsModel validateNewCardsMdl)
        {
            var modals = await _validateNewCardsService.Details(validateNewCardsMdl);
            List<StateResponseModal> lstState = new List<StateResponseModal>();
            lstState = await _commonActionService.GetStateList();
            modals.States.AddRange(lstState);
            return View(modals);
        }
        public async Task<IActionResult> GetCardDetailsForApproval(string CustomerRefNo,string CustomerType)
        {
            ViewBag.CustomerType=CustomerType;
            var modals = await _validateNewCardsService.GetCardDetailsForApproval(CustomerRefNo);
            return PartialView("~/Views/ValidateNewCards/_GetValidateFormDetails.cshtml", modals);
        }
        public async Task<IActionResult> ActionOnCards([FromBody] ApproveCardDetailsModel approveRejectModel)
        {
            var reason = await _validateNewCardsService.ActionOnCards(approveRejectModel);
            return Json(reason);
        }
    }
}
