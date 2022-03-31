using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ApplicationFormDataEntry;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IApplicationFormDataEntryService
    {
        Task<GetCustomerNameResponse> GetCustomerName(string customerId);
        Task<List<SuccessResponse>> CheckAddOnForm(string formNumber);
    }
}
