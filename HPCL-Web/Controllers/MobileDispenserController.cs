using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.MobileDispenser;
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

            var modals1 = await _mobiledispenser.GetStatusMobileDispenser();





            //ViewBag.StatusName = modals1;


            MobileDispenserViewModel multi_Dropdownlist = new MobileDispenserViewModel

            {

                GetStatus = await _mobiledispenser.GetStatusMobileDispenser(),

                GetAllDataMobileDispenser = await _mobiledispenser.MobileDispenserRetailOutletMapping(MobileDispenserId, Status)

            };

            // ViewBag.StatusName = modals1;


            //MobileDispenserViewModel obj = new MobileDispenserViewModel();
            //obj.GetAllDataMobileDispenser = modals;

            // obj.GetStatus = modals1;
            // ViewBag.StatusName = modals1;
            //ViewBag.Status = Status;
            //MobileDispenserId = "";
            //Status = 0;

            return View(multi_Dropdownlist);
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
