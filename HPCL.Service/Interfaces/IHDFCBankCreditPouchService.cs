using HPCL.Common.Models.ResponseModel.HDFCBankCreditPouch;
using HPCL.Common.Models.ViewModel.HDFCBankCreditPouch;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IHDFCBankCreditPouchService
    {
        Task<CustomerDetailsRes> GetCustomerDetails(CustomerDetailsReq entity);
    }
}
