using HPCL.Common.Models.ResponseModel.CustomerRequest;
using HPCL.Common.Models.ViewModel.CustomerRequest;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerRequestService
    {
        Task<GetSmsAlertForMultipleMobileDetailRes> GetSmsAlertForMultipleMobileDetail(GetSmsAlertForMultipleMobileDetailReq entity);
    }
}
