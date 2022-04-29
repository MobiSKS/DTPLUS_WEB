using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ManageRbe;
using HPCL.Common.Models.ViewModel.ManageRbe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IManageRbeService
    {
        Task<ChangeRbeMappingResponse> ChangeRbeMapping(RbeMapping entity);
        Task<RbeUserListResponse> RbeUserList(RbeUserList entity);
        Task<RbeDeviceIdResetRequestResponse> RbeDeviceIdResetRequestService(RbeDeviceIdResetRequest entity);
        Task<ChangeRbeMappingByUserNameResponse> ChangeRbeMappingByUserName(string newUserName, string userName);
        Task<List<SuccessResponse>> UserNameVerifyOtp(string newUserName, string userName, string otp);
        Task<GetApproveChnagedRbeMappingResponse> ApproveChangedRbeMapping(GetApproveChangedRbeMapping entity);
        Task<List<ApproveRejectChangedRbeMappingResponse>> ApproveRejectChangedRbeMappingSerivce(string userName, string actionPress);
        Task<RbeMobileChangeResponse> RbeMobileChangeRequestService(RbeMobileChange entity);
        Task<GetSendOtpChangeRbeMobileResponse> GetOtpMobileChangeReqService(string newMobileNo);
        Task<List<SuccessResponse>> VerifyOtpMobileChangeReqService(string existMobNo, string newMobileNo, string otp);
        Task<GetApproveChangeRbeMobileResponse> ApproveChangeRbeMobileService(GetApproveChangeRbeMobile entity);
        Task<List<ApproveRejectChangedRbeMappingResponse>> ApproveRejectChangedRbeMobileService(string existMobNo, string mappingStatus);
        Task<GetDeviceIdResetRequestRespose> GetDeviceIdResetRequestSerivce(GetDeviceIdResetRequest entity);
        Task<GetSendOtpChangeRbeMobileResponse> GetOtpRbeDeviceResetService(string mobNo);
        Task<List<SuccessResponse>> ValidateOtpRbeDeviceResetService(string MobileNo, string otp);
    }
}
