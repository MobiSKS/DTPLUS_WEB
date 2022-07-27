using HPCL.Common.Models.ResponseModel.MODashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IMODashboardService
    {
        Task<List<PendingTerminalResponseModel>> PendingTerminal(string UserName);
        Task<List<UserInformationResponseModel>> UserInformation(string UserName);
        Task<List<RegionInformationResponseModel>> RegionInformation(string UserName);
        Task<List<GetNotificationContentResponseModel>> GetNotificationContent(string UserType);

    }
}
