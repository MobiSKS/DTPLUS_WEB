using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ViewModel.Interface;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class InterfacesController : Controller
    {
        
        private readonly IInterfaceService _interfaceService;
        private readonly ICommonActionService _commonActionService;
        public InterfacesController(IInterfaceService interfaceService, ICommonActionService commonActionService)
        {
            _interfaceService = interfaceService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View(SessionMenuModel.menuList);
        }
        public async Task<IActionResult> SearchCustomerandCardForm()
        {
            SearchCustomerandCardFormModel SearchCustomerandCardFormModel = new SearchCustomerandCardFormModel();

            var recordType = await _commonActionService.GetRecordType();
            var stateList= await _commonActionService.GetStateList();
            SearchCustomerandCardFormModel.RecordTypeList.AddRange(recordType);
            SearchCustomerandCardFormModel.States.AddRange(stateList);
            return View(SearchCustomerandCardFormModel);
        }
        public async Task<IActionResult> GetCustomerandCardFormDetails(string EntityId,string FormNumber,string StateID,string CityName)
        {
            var modals = await _interfaceService.GetCustomerandCardFormDetails(EntityId, FormNumber, StateID, CityName);
            if(EntityId=="1")
                return PartialView("~/Views/Interfaces/_CustomerFormDetails.cshtml", modals);
            else
                return PartialView("~/Views/Interfaces/_CardFormDetails.cshtml", modals);
        }
    }
}
