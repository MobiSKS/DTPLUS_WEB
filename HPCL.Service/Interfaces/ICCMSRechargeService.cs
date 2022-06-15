using HPCL.Common.Models.ResponseModel.CCMSRecharge;
using HPCL.Common.Models.ViewModel.CCMSRecharge;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICCMSRechargeService
    {
        Task<GetDetailsByMobRes> GetDetailsByMObNo(string mobNo);
        Task<RedirectToPGResponse> RedirectToPG(RedirectToPGRequest entity);
    }
}
