using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models;
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ResponseModel.Customer;

namespace HPCL.Service.Interfaces
{
    public interface IMyHpOTCCardCustomerService
    {
        Task<RequestForOTCCardModel> RequestForOTCCard();
        Task<RequestForOTCCardModel> RequestForOTCCard(RequestForOTCCardModel requestForDriverCardModel);

        Task<MyHPOTCCardCustomerModel> CustomerCardCreation();
               
        Task<List<OTCCardDetails>> GetAvailableOTCCardByRegionalId(string RegionalId, string MerchantID);
        Task<MyHPOTCCardCustomerModel> CustomerCardCreation(MyHPOTCCardCustomerModel customerModel);
        Task<OTCUnAllocatedCardsResponse> GetAllUnAllocatedCardsForOtcCard(string RegionalId);
        Task<MIDAllocationOfCardsModel> OTCCardsAllocation();
        Task<CommonResponseData> SaveOTCCardsAllocation([FromBody] LinkCardsToMerchantModel linkCardsToMerchantModel);
        Task<ViewOTCCardResponse> GetAllViewCardsForOtcCard(string RegionalId);
        Task<MIDAllocationOfCardsModel> ViewOTCCards();

        Task<GetCardAllocationActivation> GetCardAllocationActivation();
        Task<ViewOTCCardsMerchatMappingModel> ViewOTCCardsMerchatMapping();
        Task<OTCCardMerchantAllocationResponse> ViewOTCCardMerchantAllocation(string MerchantId, string CardNo);
        Task<MyCardAllocationandActivationModel> SearchCardActivationandAllocation(GetCardAllocationActivation entity);
        Task<GetCardAllocationActivation> MyHPOTCCardAllocationandActivation();
        Task<DealerWiseMyHPOTCCardRequestModel> DealerOTCCardRequests();
        Task<DealerWiseMyHPOTCCardRequestModel> DealerOTCCardRequests(DealerWiseMyHPOTCCardRequestModel dealerWiseMyHPOTCCardRequestModel);
        Task<CustomerInserCardResponseData> CheckMobilNoAlreadyUsedForOTCCard(string MobileNo);
        Task<MyHPOTCCardCustomerModel> OTCCustomerCreation();
        Task<List<OTCCardDetails>> GetAvailableOTCCardUserWise();
        Task<MyHPOTCCardCustomerModel> InsertOTCCustomer(MyHPOTCCardCustomerModel customerModel);
    }
}