using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.EFT;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{


    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class EFTController : Controller
    {
        private readonly IEFTService _eftService;
        private readonly ICommonActionService _commonActionService;
        public EFTController(IEFTService eftService, ICommonActionService commonActionService)
        {
            _eftService = eftService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CCMSRechargethroughEFT()
        {
            var modals = new CCMSRechargethroughEFTModel();
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> CCMSRechargethroughEFT(CCMSRechargethroughEFTModel reqEntity)
        {

            var modals = new CCMSRechargethroughEFTModel();
            if (reqEntity.TransactionDetailFile != null)
            {
                var uploadResponse = await _eftService.EFTRechargeDetailValidation(reqEntity);
                //if (uploadResponse.Data[0].Status == "1")
                //{
                //    var response=await _eftService.CCMSRechargethroughEFT(reqEntity);
                //    modals.Status = response[0].Status.ToString();
                //    modals.Reason = response[0].Reason;
                //}
                //else
                //{
          
                modals.UploadEFTSummaryData = uploadResponse;
                
                modals.TransactionDetailFile= reqEntity.TransactionDetailFile;
                //}
            }
            return View(modals);
        }
        public async Task<IActionResult> CCMSRechargeApproval()
        {
            var modals = new CCMSRechargeApprovalModel();
            return View(modals);
        }

    }
}
