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
    }
}
