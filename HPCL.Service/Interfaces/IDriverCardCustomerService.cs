using HPCL.Common.Models;
using HPCL.Common.Models.ResponseModel.DriverCardCustomer;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.DriverCardCustomer;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IDriverCardCustomerService
    {
        Task<RequestForDriverCardModel> RequestForDriverCard();
        Task<RequestForDriverCardModel> RequestForDriverCard(RequestForDriverCardModel requestForDriverCardModel);
        Task<DriverCardCustomerModel> CreateDriverCardCustomer();
        Task<DriverCardAllocationToMerchantModel> DriverCardAllocation();
        Task<OTCUnAllocatedCardsResponse> GetAllUnAllocatedDriverCards(string RegionalId);
        Task<CommonResponseData> SaveDriverCardsAllocation([FromBody] LinkCardsToMerchantModel linkCardsToMerchantModel);
        Task<List<CardDetails>> GetAvailableDriverCardByRegionalId(string RegionalId, string MerchantID);
        Task<DriverCardCustomerModel> CreateDriverCardCustomer(DriverCardCustomerModel customerModel);
        Task<GetCustomerNameByIdResponse> GetCustomerNameByCustomerId(string CustomerID);

        Task<List<ViewDriverCardResponse>> GetAllViewDriverCard(RequestForViewDriverCard entity);
        Task<RequestForDriverCardModel> ViewRequestDriverCard();
        Task<ViewDriverCardMerchatMappingModel> ViewDriverCardsMerchatMapping();
        Task<DriverCardMerchantAllocationResponse> ViewDriverCardMerchantAllocation(string MerchantId, string CardNo);

        Task<DriverCardAllocationanadActivationViewModel> GetDriverCardActivationAllocationDetails(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string customerId);
        Task<DriverCardAllocationanadActivationViewModel> DriverCardAllocationandActivation();
        Task<DealerWiseDriverCardRequestModel> DealerDriverCardRequests();
        Task<DealerWiseDriverCardRequestModel> DealerDriverCardRequests(DealerWiseDriverCardRequestModel dealerWiseDriverCardRequestModel);

    }
}
