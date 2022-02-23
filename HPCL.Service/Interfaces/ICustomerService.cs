using HPCL.Common.Models.ViewModel.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<UploadDoc> UploadDoc(string CustomerReferenceNo);
        Task<List<UploadDocResponse>> UploadDoc(UploadDoc entity);
        Task<string> SaveUploadDoc(UploadDoc entity);
    }
}
