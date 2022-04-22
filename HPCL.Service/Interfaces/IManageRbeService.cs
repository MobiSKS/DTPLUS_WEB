﻿using HPCL.Common.Models.CommonEntity;
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
    }
}
