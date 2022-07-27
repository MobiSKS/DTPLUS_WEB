using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.MODashboard;
using HPCL.Common.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using HPCL.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.IO;
using System.IO.Compression;
using HPCL.Common.Models.ResponseModel.Aggregator;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Newtonsoft.Json;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class MODashboardController : Controller
    {
        private readonly IMODashboardService _modashboardservice;
        private readonly ICommonActionService _commonActionService;

        public MODashboardController(IMODashboardService customerDashboardServices, ICommonActionService commonActionService)
        {
            _modashboardservice = customerDashboardServices;
            _commonActionService = commonActionService;
        }
        public string UserType { get;  set; }
        public async Task<IActionResult> MODashboard(string UserName)
        {
            UserName = "adityamo";
            var modashboardlist = await _modashboardservice.UserInformation(UserName);
            var RegionInformationList = await _modashboardservice.RegionInformation(UserName);
            var PendingTerminalList = await _modashboardservice.PendingTerminal(UserName);
            var GetNotificationContent = await _modashboardservice.GetNotificationContent(UserType);

            MODashboardModel MODashboard = new MODashboardModel();
            MODashboard.UserInformationResponseModel = modashboardlist;

            MODashboard.RegionInformationResponseModel = RegionInformationList;
            MODashboard.PendingTerminalResponseModel = PendingTerminalList;

            MODashboard.GetNotificationContentResponseModel = GetNotificationContent;

            return View(MODashboard);
        }
        [HttpPost]
        public async Task<JsonResult> GetPendingTerminal(string userName)
        {
            MODashboardModel MODashboard = new MODashboardModel();

           var pendingTerminal = await _modashboardservice.PendingTerminal(userName);

            return Json(pendingTerminal);
        }
    }
}
    