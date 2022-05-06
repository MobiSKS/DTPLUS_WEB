using HPCL.Common.Models.ResponseModel.TatkalCardCustomer;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.TatkalCardCustomer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models.RequestModel.TatkalCardCustomer;

namespace HPCL.Service.Interfaces
{
    public interface ITatkalCardCustomerService
    {
        Task<TatkalCustomerCardRequestInfo> RequestForTatkalCard();
        Task<TatkalCustomerCardRequestInfo> RequestForTatkalCard(TatkalCustomerCardRequestInfo tatkalCustomerCardRequestInfo);
        Task<TatkalCardCustomerModel> CreateTatkalCustomer();
        Task<List<TatkalCardCustomerViewResponse>> GetAllTatkalCustomerCard(TatkalViewRequest entity);
        Task<TatkalViewRequestModel> ViewAllocatedMapCard();
        Task<List<CustomerRegionModel>> GetRegionalDetailsDropDown(int ZonalOfficeID);
        Task<TatkalCardCustomerModel> CreateTatkalCustomer(TatkalCardCustomerModel customerModel);
        Task<ViewRequestedTatkalCardModel> ViewRequestedTatkalCard();
        Task<ViewRequestedTatkalCardResponse> GetViewRequestedTatkalCard(int RegionalId);
        Task<GetTatkalCardsResponseModel> GetMapTatkalCardtoCustomer(string customerId);
        Task<List<string>> UpdateTatkalCardtoCustomer([FromBody] MapTatkalCardtoCustomerUpdateModel UpdateDetails);
        Task<ViewTatkalCardsResponseModel> GetViewTatkalCards([FromBody] ViewTatkalCardRequestModel entity);
    }
}
