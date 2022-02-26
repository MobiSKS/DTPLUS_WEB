using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly ICommonActionService _commonActionService;
        public CommonController(ICommonActionService commonActionService)
        {
            _commonActionService = commonActionService;
        }

        [HttpPost]
        public async Task<JsonResult> GetLimitType()
        {
            var sortedtList = await _commonActionService.GetLimitType();

            ModelState.Clear();
            return Json(new { sortedtList = sortedtList });
        }

        [HttpPost]
        public async Task<JsonResult> GetOfficerTypeDetails()
        {
            var sortedtList = await _commonActionService.GetOfficerTypeList();

            ModelState.Clear();
            return Json(sortedtList);
        }

        [HttpPost]
        public async Task<JsonResult> GetLocationDetails(string OfcrType)
        {
            if (OfcrType.Contains("1") || OfcrType.Contains("4") || OfcrType.Contains("6"))
            {
                var sortedtList = await _commonActionService.GetRegionalOfficeList("0");

                ModelState.Clear();
                return Json(sortedtList);
            }
            else if (OfcrType.Contains("3") || OfcrType.Contains("5"))
            {
                var sortedtList = await _commonActionService.GetZonalOfficeList();

                ModelState.Clear();
                return Json(sortedtList);
            }
            else
            {
                var sortedtList = await _commonActionService.GetHqList();

                ModelState.Clear();
                return Json(sortedtList);
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetDistrictDetails(string stateId)
        {
            var sortedtList = await _commonActionService.GetDistrictList(stateId);

            ModelState.Clear();
            return Json(sortedtList);
        }

        [HttpPost]
        public async Task<JsonResult> GetRegionalOfcDetails(string zonalOfcId)
        {
            var sortedtList = await _commonActionService.GetRegionalOfficeList(zonalOfcId);

            ModelState.Clear();
            return Json(sortedtList);
        }
        public async Task<JsonResult> GetSalesAreaList(string regionalOfcId)
        {
            var sortedtList = await _commonActionService.GetSalesAreaList(regionalOfcId);

            ModelState.Clear();
            return Json(sortedtList);
        }
        [HttpPost]
        public async Task<JsonResult> ValidateUserName(string userName)
        {
            var check = await _commonActionService.ValidateUserName(userName);

            ModelState.Clear();
            return Json(check);
        }
    }
}
