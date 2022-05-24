using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.CustomerRequest;
using HPCL.Common.Models.ViewModel.CustomerRequest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerRequestService
    {
        Task<GetSmsAlertForMultipleMobileDetailRes> GetSmsAlertForMultipleMobileDetail(GetSmsAlertForMultipleMobileDetailReq entity);
        Task<List<SuccessResponse>> UpdateSmsAlertForMultipleMobileDetail(string customerDetailForSmsAlert);
        Task<List<SuccessResponse>> DeleteSmsAlertForMultipleMobileDetail(string CustomerID, string MobileNo);
        Task<GetCardRenwalRequestListRes> GetCardRenwalRequest(GetCardRenwalRequestList entity);
        Task<List<SuccessResponse>> UpdateCardRenwalRequest(string CustomerId, string updatePostArray);
        Task<ConfigureSmsAlertsRes> GetSmsAlertsConfigure(GetConfigureSmsAlerts entity);
    }
}
