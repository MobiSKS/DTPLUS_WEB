using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace HPCL.Service.Interfaces
{
    public interface IMyHpOTCCardCustomerService
    {
        Task<RequestForOTCCardModel> RequestForOTCCard();
        Task<RequestForOTCCardModel> RequestForOTCCard(RequestForOTCCardModel requestForDriverCardModel);

        Task<MyHPOTCCardCustomerModel> CustomerCardCreation();

        Task<MerchantDetailsResponseOTCCardCustomer> GetMerchantDetailsByMerchantId(string MerchantID);

        Task<List<CardDetails>> GetAvailableOTCCardByRegionalId(string RegionalId, string MerchantID);
        Task<MyHPOTCCardCustomerModel> CustomerCardCreation(MyHPOTCCardCustomerModel customerModel);
        Task<CommonResponseData> VerifyMerchantByMerchantidAndRegionalid(string RegionalId, string MerchantID);
        Task<OTCUnAllocatedCardsResponse> GetAllUnAllocatedCardsForOtcCard(string RegionalId);
        Task<MIDAllocationOfCardsModel> OTCCardsAllocation();
        Task<CommonResponseData> SaveOTCCardsAllocation([FromBody] LinkCardsToMerchantModel linkCardsToMerchantModel);

    }
}
