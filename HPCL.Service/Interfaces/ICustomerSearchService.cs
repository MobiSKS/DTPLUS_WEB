using HPCL.Common.Models.ResponseModel.CustomerSearch;
using HPCL.Common.Models.ViewModel.CustomerSearch;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerSearchService
    {
        Task<ControlCardPinResetRes> CCPinReset(ControlCardPinResetReq entity);
    }
}
