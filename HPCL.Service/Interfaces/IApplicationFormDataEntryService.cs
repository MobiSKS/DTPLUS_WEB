using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ApplicationFormDataEntry;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.ApplicationFormDataEntry;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IApplicationFormDataEntryService
    {
        Task<GetCustomerNameResponse> GetCustomerName(string customerId);
        Task<List<SuccessResponse>> CheckAddOnForm(string formNumber);
        Task<AddAddOnCard> AddAddOnCards();
        Task<AddAddOnCard> GetAddOnCardsPartialView(List<ObjCardDetail> objCardDetails);
        Task<AddAddOnCard> CustomerAddCardVehicleTbl(List<ObjCardDetail> objCardDetails);
        Task<AddAddOnCard> AddAddOnCards(AddAddOnCard addAddOnCard);
        Task<CustomerInserCardResponseData> CheckCardIdentifierAlreadyUsed(string cardIdentifier, string customerID);
    }
}