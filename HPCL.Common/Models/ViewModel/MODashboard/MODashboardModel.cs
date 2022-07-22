using HPCL.Common.Models.ResponseModel.MODashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.MODashboard
{
    public class MODashboardModel
    {
        public MODashboardModel()
        {
            RegionInformationResponseModel = new List<RegionInformationResponseModel>();
            PendingTerminalResponseModel = new List<PendingTerminalResponseModel>();
            UserInformationResponseModel = new List<UserInformationResponseModel>();
        }
        public List<RegionInformationResponseModel> RegionInformationResponseModel { get; set; }
        public List<PendingTerminalResponseModel> PendingTerminalResponseModel { get; set; }
        public List<UserInformationResponseModel> UserInformationResponseModel { get; set; }
    }
}