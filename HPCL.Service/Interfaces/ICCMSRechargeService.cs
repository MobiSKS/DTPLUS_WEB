using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.CCMSRecharge;
using HPCL.Common.Models.ViewModel.CCMSRecharge;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICCMSRechargeService
    {
        Task<GetDetailsByMobRes> GetDetailsByMObNoCust(string mobNo, string customerId);
        Task<RedirectToPGResponse> RedirectToPG(string customerId, string mobNo, string controlCardNo, string amount);
        Task<CCCMSRecGenerateOtpRes> CCCMSRecGenerateOtp(string mobNo, string customerId);
        Task<List<CCCMSRecVerifyOtpRes>> CCCMSRecVerifyOtp(string mobNo, string otp, string customerId);
    }
}
