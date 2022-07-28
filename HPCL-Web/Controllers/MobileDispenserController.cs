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

        public async Task<IActionResult> MobileDispenserRetailsOutletMapping(string MobileDispenserId, int Status)
        {
            var modals = await _mobiledispenser.MobileDispenserRetailOutletMapping(MobileDispenserId, Status);

           //ViewBag.Status = Status;
            //ViewBag.Status = Status;
            //MobileDispenserId = "";
            //Status = 0;

            return View(modals);
        }

        public async Task<IActionResult> SearchMobileDispenserRetailsOutletMapping(string MobileDispenserId, int Status)
        {
            var modals = await _mobiledispenser.SearchMobileDispenserRetailOutletMapping(MobileDispenserId, Status);

            //ViewBag.Status = Status;
            //ViewBag.Status = Status;
            //MobileDispenserId = "";
            //Status = 0;

            return View("~/Views/MobileDispenser/_SearchMobileDispenserRetailsOutletMapping.cshtml", modals);
        }


        public async Task<IActionResult> StatusMobileDispenserRetailsOutletMapping(string Status)
        {
            var modals = await _mobiledispenser.GetStatusMobileDispenserRetailOutletMapping(Status);

         

            return View("~/Views/MobileDispenser/_StatusMobileDispenserRetailsOutletMapping.cshtml", modals);
        }
    }
}
