using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.Cards;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class CardsController : Controller
    {
        private readonly ICardServices _cardService;

        public CardsController(ICardServices cardServices)
        {
            _cardService = cardServices;
        }
        public async Task<IActionResult> Index()
        {
            return View();
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
        public async Task<JsonResult> CardlessMapping(UpdateMobileModal entity)
        {
            var reason = await _cardService.CardlessMapping(entity);
            ModelState.Clear();

            return Json(reason);
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
            var reason = await _cardService.UpdateCards(limitArray);

            ModelState.Clear();
            return Json(reason);
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
        public async Task<JsonResult> UpdateCcmsLimitAllCards(GetCcmsLimitAll entity)
        {
            var reason = await _cardService.UpdateCcmsLimitAllCards(entity);

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
    }
}
