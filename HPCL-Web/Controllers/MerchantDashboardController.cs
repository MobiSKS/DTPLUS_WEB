using HPCL.Common.Models.ViewModel;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using HPCL.Common.Models.ResponseModel.Aggregator;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.MerchantDashboard;
using HPCL.Common.Models.ResponseModel.CommonResponse;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class MerchantDashboardController : Controller
    {

        private readonly IMerchantDashboardServices _merchantDashboardService;
        private readonly ICommonActionService _commonActionService;

        public MerchantDashboardController(IMerchantDashboardServices merchantDashboardService, ICommonActionService commonActionService)
        {
            _merchantDashboardService = merchantDashboardService;
            _commonActionService = commonActionService;
        }
        public async Task<IActionResult> Dashboard(string MerchantId)
        {
            var dashboardkeyInformation = await _merchantDashboardService.keyInformation(MerchantId);
            var todaysTransaction = await _merchantDashboardService.TodaysTransactionSummaries(MerchantId);
            //var notificationContent = await _merchantDashboardService.NotificationResponsesDetails(UserType);

            //var lastTransaction= await _merchantDashboardService.lastTrasaction(MerchantId);
            //var lastBatchDetails = await _merchantDashboardService.lastBatches(MerchantId);
            // var salesEarningDetails = await _merchantDashboardService.lastSaleReloadEarningDetails(MerchantId);
            //var keyEventsFigures= await _merchantDashboardService.lastKeyEventAndFigure(MerchantId);

            MerchantDashboardModel merchantDashboard = new MerchantDashboardModel();

            merchantDashboard.KeyInformationResponseModels = dashboardkeyInformation;
            merchantDashboard.todaysTransactionResponseModels = todaysTransaction;
            //merchantDashboard.NotificationResponseModels = notificationContent;
            // merchantDashboard.LastTrasactionResponseModels= lastTransaction;
            // merchantDashboard.LastBatchDetailResponseModels = lastBatchDetails;
            // merchantDashboard.LastSaleReloadEarningDetailsResponseModels = salesEarningDetails;
            //merchantDashboard.keyEVentsAndFigureResponseModels = keyEventsFigures;



            return View(merchantDashboard);
        }

        //public async Task<IActionResult> NotificationContent(string UserType)
        //{

        //    var notificationContent = await _merchantDashboardService.NotificationResponsesDetails(UserType);



        //    MerchantDashboardModel merchantDashboard = new MerchantDashboardModel();


        //    merchantDashboard.NotificationResponseModels = notificationContent;




        //    return View(merchantDashboard);
        //}

        [HttpPost]
        public async Task<JsonResult> NotificationContent(string UserType)
        {
            MerchantDashboardModel merchantDashboard = new MerchantDashboardModel();
            var notificationContent = await _merchantDashboardService.NotificationResponsesDetails(UserType);

            return Json(notificationContent);
        }

        [HttpPost]
        public async Task<JsonResult> GetLastTrasnaction(string MerchantId)
        {
            MerchantDashboardModel merchantDashboard = new MerchantDashboardModel();

            var lastTransaction = await _merchantDashboardService.lastTrasaction(MerchantId);

            return Json(lastTransaction);
        }

        /// <summary>
        /// Get Last batch detail
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetLastBatch(string MerchantId)
        {
            MerchantDashboardModel merchantDashboard = new MerchantDashboardModel();

            var lastBatch = await _merchantDashboardService.lastBatches(MerchantId);

            return Json(lastBatch);
        }


        /// <summary>
        /// Get Last Sales
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetLastSalesEarning(string MerchantId)
        {
            MerchantDashboardModel merchantDashboard = new MerchantDashboardModel();

            var lastSales = await _merchantDashboardService.lastSaleReloadEarningDetails(MerchantId);

            return Json(lastSales);
        }


        /// <summary>
        /// Get last key Event and figures
        /// </summary>



        [HttpPost]
        public async Task<JsonResult> GetkeyLastEvents(string MerchantId)
        {
            MerchantDashboardModel merchantDashboard = new MerchantDashboardModel();

            var lastKeyEvent = await _merchantDashboardService.lastKeyEventAndFigure(MerchantId);

            return Json(lastKeyEvent);
        }
    }
}