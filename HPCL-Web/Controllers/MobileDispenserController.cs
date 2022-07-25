using HPCL.Common.Helper;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class MobileDispenserController : Controller
    {
        private readonly IMobileDispenser _mobiledispenser;

        public MobileDispenserController(IMobileDispenser mobiledispenser)
        {
            _mobiledispenser = mobiledispenser;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> MobileDispenserRetailOutletMapping(string MobileDispenserId, int Status)
        {
            var modals = await _mobiledispenser.MobileDispenserRetailOutletMapping(MobileDispenserId, Status);

            ViewBag.Status = Status;
            MobileDispenserId = "";
            Status = 0;

            return View(modals);
        }
    }
}
