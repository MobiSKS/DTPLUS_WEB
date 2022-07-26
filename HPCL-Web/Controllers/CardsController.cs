using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Cards;
using HPCL.Common.Models.ViewModel.Cards;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class CardsController : Controller
    {
        private readonly ICardServices _cardService;
        private readonly ICommonActionService _commonActionService;

        public CardsController(ICardServices cardServices, ICommonActionService commonActionService)
        {
            _cardService = cardServices;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View(SessionMenuModel.menuList);
        }
        public IActionResult ManageCards()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ManageCards(CustomerCards entity, string editFlag)
        {
            var searchList = await _cardService.ManageCards(entity, editFlag);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }


        [HttpPost]
        public async Task<JsonResult> ViewCardDetails(string CardId)
        {
            var searchList = await _cardService.ViewCardDetails(CardId);

            ModelState.Clear();
            return Json(new
            {
                searchList = searchList
            });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateService(string serviceId, bool flag)
        {
            var reason = await _cardService.UpdateService(serviceId, flag);

            ModelState.Clear();
            return Json(reason);
        }

        public async Task<IActionResult> CardlessMapping(string cardNumber, string mobileNumber, string LimitTypeName, string CCMSReloadSaleLimitValue)
        {
            var editMobBody = await _cardService.CardlessMapping(cardNumber, mobileNumber, LimitTypeName, CCMSReloadSaleLimitValue);
            return View(editMobBody);
        }

        [HttpPost]
        public async Task<JsonResult> CardlessMappingUpdate(string mobNoNew, string crdNo)
        {
            var updateResponse = await _cardService.CardlessMappingUpdate(mobNoNew, crdNo);
            ModelState.Clear();

            return Json(new { updateResponse = updateResponse });
        }

        public IActionResult AcDcCardSearch()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AcDcCardSearch(SearchCards entity)
        {
            var searchList = await _cardService.AcDcCardSearch(entity);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateStatus(string cardNo, int Statusflag)
        {
            var reason = await _cardService.UpdateStatus(cardNo, Statusflag);

            ModelState.Clear();
            return Json(reason);
        }

        public IActionResult SetSaleLimit()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SetSaleLimit(GetCardLimit entity)
        {
            var searchList = await _cardService.SetSaleLimit(entity);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }


        [HttpPost]
        public async Task<JsonResult> UpdateCards(ObjCardLimits[] limitArray)
        {
            var updateResponse = await _cardService.UpdateCards(limitArray);

            ModelState.Clear();
            return Json(new { updateResponse  = updateResponse });
        }

        public IActionResult SetCcmsLimitForAllCards()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SearchCcmsLimitForAllCards(GetCcmsLimitAll entity)
        {
            var searchCcmsCard = await _cardService.SearchCcmsLimitForAllCards(entity);

            ModelState.Clear();
            return Json(new { searchCcmsCard = searchCcmsCard });
        }


        [HttpPost]
        public async Task<JsonResult> UpdateCcmsLimitAllCards(int limitype, int amountVal)
        {
            var reason = await _cardService.UpdateCcmsLimitAllCards(limitype, amountVal);

            ModelState.Clear();
            return Json(reason);
        }

        public IActionResult SetCcmsForIndCards()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SetCcmsForIndCards(SetIndividualLimit entity)
        {
            var searchList = await _cardService.SetCcmsForIndCards(entity);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }


        [HttpPost]
        public async Task<JsonResult> UpdateCcmsIndividualCard(string objCCMSLimits, string viewGirds)
        {
            var reason = await _cardService.UpdateCcmsIndividualCard(objCCMSLimits, viewGirds);

            ModelState.Clear();
            return Json(reason);
        }

        public async Task<IActionResult> ViewUpdatedGrid()
        {
            var vGrid = await _cardService.ViewUpdatedGrid();

            return View(vGrid);
        }

        public IActionResult MappingCardToMerchant()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> MappingCardToMerchant(SearchAllowedMerchant entity)
        {
            var searchList = await _cardService.SearchAllowedMerchant(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> GetCardList()
        {
            var cardList = await _cardService.GetCardList();
            return Json(new { cardList = cardList });
        }

        public IActionResult ManageMapping()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ManageMapping(GetCustomerDetailsMapMerchant entity)
        {
            var searchList = await _cardService.ManageMapping(entity);

            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> GetMerchantForMapping(GetCustomerDetailsMapMerchant entity)
        {
            //GetCustomerDetailsMapMerchant[] arrs = JsonConvert.DeserializeObject<GetCustomerDetailsMapMerchant[]>(reqBody);

            //var entity = new GetCustomerDetailsMapMerchant
            //{
            //    MerchantID = arrs[0].MerchantID,
            //    StateID = arrs[0].StateID,
            //    DistrictID = arrs[0].DistrictID,
            //    City = arrs[0].City,
            //    HighwayName = arrs[0].HighwayName,
            //    HighwayNo2 = arrs[0].HighwayNo2
            //};

            var merchantList = await _cardService.GetMerchantForMapping(entity);
            return Json(new { merchantList = merchantList });
        }

        [HttpPost]
        public async Task<JsonResult> SaveCustomerMappingMerchant(string objCardMerchantMaps, string status)
        {
            var reason = await _cardService.SaveCustomerMappingMerchant(objCardMerchantMaps, status);
            ModelState.Clear();
            return Json(reason);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCustomerMappingMerchant(string objCardMerchantMaps, string status)
        {
            var reason = await _cardService.SaveCustomerMappingMerchant(objCardMerchantMaps, status);
            ModelState.Clear();
            return Json(reason);
        }

        public IActionResult LimitUpdateForSingleRechargeCards()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> LimitUpdateForSingleRechargeCards(GetLimitUpdateForSingleRechargeCards entity)
        {
            var searchList = await _cardService.LimitUpdateForSingleRechargeCardsService(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> LimitUpdateForSingleRecharge(string objCCMSLimits)
        {
            var reasonList = await _cardService.LimitUpdateForSingleRecharge(objCCMSLimits);
            return Json(new { reasonList = reasonList });
        }

        public IActionResult EmergencyAddOnCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> EmergencyAddOnCard(GetEmergencyAddOnCard entity)
        {
            var searchList = await _cardService.EmergencyAddOnCard(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> MapEmergencyAddOnCard(string objCards)
        {
            var reasonList = await _cardService.MapEmergencyAddOnCard(objCards);
            return Json(new { reasonList = reasonList });
        }

        public IActionResult SetCardLimitViaExcelFileUpload(string CustomerId)
        {
            GetSetCardLimitViaExcelFileUpload uploadExcel = new GetSetCardLimitViaExcelFileUpload();
            uploadExcel.CustomerId = CustomerId;
            return View(uploadExcel);
        }

        [HttpPost]
        public async Task<JsonResult> SearchSetSaleLimitForUploadExcel(string customerId)
        {
            var entity = new GetCardLimit
            {
                CustomerId = customerId,
                CardNo = "",
                MobileNo = "",
                Statusflag = 4
            };

            var searchList = await _cardService.SetSaleLimit(entity);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }
        public async Task<IActionResult> EnableCustomerServices()
        {
            var modals = await _cardService.EnableCustomerServices();
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> GetDetailForEnableDisableProductsAndTransactions(string CustomerId, string CardNo, string MobileNo)
        {
            var searchList = await _cardService.GetDetailForEnableDisableProductsAndTransactions(CustomerId, CardNo, MobileNo);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> EnableDisableProductsAndTransaction(string ObjProducts, string ObjTransactions, string CustomerId, string CardNo, string MobileNo)
        {
            var commonResponseData = await _cardService.EnableDisableProductsAndTransaction(ObjProducts, ObjTransactions, CustomerId, CardNo, MobileNo);
            return Json(new { commonResponseData = commonResponseData });
        }
        public async Task<IActionResult> CorporateMultiRechargeLimitConfig()
        {
            CorporateMultiRechargeLimitModel modals = new CorporateMultiRechargeLimitModel();
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> CorporateMultiRechargeLimitConfig(CorporateMultiRechargeLimitRequest reqModel)
        {
            CorporateMultiRechargeLimitModel modals = new CorporateMultiRechargeLimitModel();
            if (reqModel != null)
            {
                if (reqModel.CustomerID != null)
                    modals = await _cardService.GetCustomerRechargeLimitConfig(reqModel.CustomerID);
            }
           //ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            return View(modals);
        }  
        [HttpPost]
        public async Task<JsonResult> ConfigureLimits([FromBody] CorporateMultiRechargeLimitRequest reqModel)
        {
            var  modals = await _cardService.ConfigureLimits(reqModel);
            return Json(modals);
        }

        [HttpPost]
        public async Task<JsonResult> GetGenericAttachedVehicle(GetGenericAttachedVehicleReq entity)
        {
            var searchList = await _cardService.GetGenericAttachedVehicle(entity);
            return Json(new { searchList = searchList });
        }

        public async Task<IActionResult> CardWiseLimitAuditTrail()
        {
            GetCardWiseLimit entity = new GetCardWiseLimit();
            entity.LimitTypeLst.AddRange(await _commonActionService.GetAllLimitType());
            return View(entity);
        }

        [HttpPost]
        public async Task<JsonResult> CardWiseLimitAuditTrail(GetCardWiseLimit entity)
        {
            var searchList = await _cardService.CardWiseLimit(entity);
            return Json(new { searchList = searchList });
        }
    }
}
