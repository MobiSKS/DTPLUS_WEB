using HPCL.Common.Models.ResponseModel.CCMSRecharge;
using HPCL.Common.Models.ViewModel.CCMSRecharge;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICCMSRechargeService
    {
        Task<GetDetailsByMobRes> GetDetailsByMObNoCust(string mobNo, string customerId);
        Task<RedirectToPGResponse> RedirectToPG(string customerId, string mobNo, string controlCardNo, string amount);
    }
}
