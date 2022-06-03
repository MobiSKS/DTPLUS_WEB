using HPCL.Common.Models.ViewModel.HDFCBankCreditPouch;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IHDFCBankCreditPouchService
    {
        Task<string> GetCustomerDetails(CustomerDetailsReq entity);
    }
}
