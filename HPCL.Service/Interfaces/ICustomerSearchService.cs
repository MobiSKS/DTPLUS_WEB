using HPCL.Common.Models.ResponseModel.CustomerSearch;
using HPCL.Common.Models.ViewModel.CustomerSearch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerSearchService
    {
        Task<ControlCardPinResetRes> CCPinReset(ControlCardPinResetReq entity);
        Task<BasicSearchModel> SearchCustomer();
        Task<List<SearchDetailsTableModel>> SearchCustomerDetails(string customerId, string mobileNo, string formNumber, string nameonCard, string CustomerName, string communicationStateId, string communicationCityName);
    }
}
