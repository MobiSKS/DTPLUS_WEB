using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.AshokLeyLand;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IAshokLeyLandService
    {
        Task<InsertResponse> InsertAlEnroll(string str);
        Task<SearchAlResult> SearchDealer(string dealerCode, string dtpCode);
        Task<InsertResponse> AlEnrollUpdate(string getAllData);
        Task<ALOTCCardRequestModel> DealerOTCCardRequest();
        Task<ALOTCCardRequestModel> DealerOTCCardRequest(ALOTCCardRequestModel alOTCCardRequestModel);
        Task<AshokLeylandCardCreationModel> CreateMultipleOTCCard();
        Task<List<CardDetails>> GetAvailableAlOTCCardForDealer(string DealerCode);
        Task<AshokLeylandCardCreationModel> CreateMultipleOTCCard(AshokLeylandCardCreationModel ashokLeylandCardCreationModel);
        Task<ViewALOTCCardDealerMappingModel> ViewALOTCCardsDealerMapping();
        Task<ALOTCCardDealerAllocationResponse> GetViewALOTCCardDealerAllocation(string DealerCode, string CardNo);
        Task<GetCustomerDetails> GetAlAddonOTCCardMappingCustomerDetails(string customerId);
        Task<AddonOTCCardMapping> GetAlAddonOTCCardCustomerDetailsPartialView(string str);
        Task<AddonOTCCardMapping> GetAlAddonOTCCardAddCardsPartialView(string str);
        Task<AddonOTCCardMapping> AddonOTCCardMapping(AddonOTCCardMapping addAddOnCard);
    }
}
