using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.MobileDispenser;
using HPCL.Common.Models.ResponseModel.MobileDispenser;
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

       

        public async Task<IActionResult> MobileDispenserRetailsOutletMapping()
        
        
        
        {
            
            var modals = await _mobiledispenser.MobileDispenserRetailOutletMapping();

            var modals1 = await _mobiledispenser.GetStatusMobileDispenser();





            //ViewBag.StatusName = modals1;


            MobileDispenserViewModel multi_Dropdownlist = new MobileDispenserViewModel

            {

                GetStatus = await _mobiledispenser.GetStatusMobileDispenser(),

                GetAllDataMobileDispenser = await _mobiledispenser.MobileDispenserRetailOutletMapping()

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
        public async Task<IActionResult> SearchMobileDispenserRetailsOutletMapping(string merchantId, string status)
        {
            var modals = await _mobiledispenser.SearchMobileDispenserRetailOutletMapping(merchantId, status);

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

        public async Task<IActionResult> CreateMobileDispenser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMobileDispenser(InsertMobileDispenserRetailOutletMappingRequest ObjMobileDis)
        {
            var tuple = await _mobiledispenser.CreateMobileDispenserRetailOutletMapping(ObjMobileDis);
            if (tuple.Item1 == "0")
            {
                
                ViewBag.Reason = tuple.Item2;
                ViewBag.Status = tuple.Item1;
                return View(ObjMobileDis);
            }
            else
            {
                ViewBag.Reason = tuple.Item2;
                ViewBag.Status = tuple.Item1;
                return View(ObjMobileDis);
            }
               
        }
    }
}
