using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Customer;
using HPCL.Common.Models.ResponseModel.CustomerRequest;
using HPCL.Common.Models.ViewModel.CustomerRequest;
using Microsoft.AspNetCore.Mvc;
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
        Task<List<SuccessResponse>> UpdateSmsAlertsConfigure(string CustomerId, string SmsAlertList);
        Task<List<SuccessResponse>> UpdateDndSmsAlertsConfigure(string CustomerId);
        Task<GetHotlistCardsPermanentlyRes> HotlistCardsPermanently(GetHotlistCardsPermanently entity);
        Task<List<UpdateHotlistCardRes>> UpdatePermanentlyHotlistCards(string CustomerId, string cardsList);
        Task<ConfigureEmailAlertViewModel> ConfigureEmailAlerts(string CustomerId);
        Task<List<SuccessResponse>> UpdateConfigureEmailAlert([FromBody] ConfigureEmailAlertRequest reqModel);
        Task<ApproveCardRenwalRequestRes> GetApproveCardRenwalRequest(ApproveCardRenwalRequestReq entity);
        Task<List<SuccessResponse>> UpdateApproveCardRenwalRequest(string actionType, string appRejValues);
    }
}
