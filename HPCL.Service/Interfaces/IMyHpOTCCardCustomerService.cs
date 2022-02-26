using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;

namespace HPCL.Service.Interfaces
{
    public interface IMyHpOTCCardCustomerService
    {
        Task<RequestForOTCCardModel> RequestForOTCCard();
        Task<RequestForOTCCardModel> RequestForOTCCard(RequestForOTCCardModel requestForDriverCardModel);

        Task<MyHPOTCCardCustomerModel> CustomerCardCreation();

        Task<MerchantDetailsResponseOTCCardCustomer> GetMerchantDetailsByMerchantId(string MerchantID);

        Task<List<CardDetails>> GetAvailableOTCCardByRegionalId(string RegionalId);
        Task<string> PANValidation(string PANNumber);
        Task<MyHPOTCCardCustomerModel> CustomerCardCreation(MyHPOTCCardCustomerModel customerModel);

    }
}
