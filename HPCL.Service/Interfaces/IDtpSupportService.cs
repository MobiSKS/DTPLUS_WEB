using HPCL.Common.Models.ResponseModel.DtpSupport;
using HPCL.Common.Models.ViewModel.DtpSupport;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IDtpSupportService
    {
        Task<GetBlockUnblockCustomerCcmsAccountByCustomeridResponse> GetBlockUnblockCustomerCcmsAccount(string customerId);
        Task<string> UpdateCustomerCcmsAccountStatus (BlockUnblockCustomerCcmsAccount entity);
    }
}
