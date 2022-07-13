using HPCL.Common.Models;
using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ResponseModel.VolvoEicher;
using HPCL.Common.Models.ViewModel.VolvoEicher;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IVolvoEicherService
    {
        Task<VEDealerOTCCardStatusViewModel> GetVEDealerCardStatus(string DealerCode);
        Task<List<VECustomerProfileResponse>> BindCustomerDetailsForSearch(string CustomerId, string NameOnCard);
        Task<InsertResponse> InsertVEDealerEnrollment(string str);
        Task<SearchAlResult> SearchVEDealer(string dealerCode, string dtpCode);
        Task<InsertResponse> VEDealerEnrollmentUpdate(string getAllData);
        Task<CommonResponseData> CheckVEDealerCodeExists(string DealerCode);
        Task<VEOTCCardRequestModel> VEDealerOTCCardRequest();
        Task<VEOTCCardRequestModel> VEDealerOTCCardRequest(VEOTCCardRequestModel model);
        Task<InsertResponse> ResetVEDealerPassword(string UserName);
        Task<ViewVEDealerOTCCardDetailsModel> ViewVEDealerUnmappedOTCCardDetails();
        Task<VEOTCCardDealerAllocationResponse> GetViewVEOTCCardDealerAllocation(string DealerCode, string CardNo);
        Task<VECardCreationModel> CreateMultipleOTCCard();
        Task<GetSalesExecutiveEmployeeIDResponse> GetVESalesExecutiveEmpId(string dealerCode);
        Task<VECardCreationModel> GetMultipleOTCCardPartialView([FromBody] List<VECardEntryDetails> arrs);
        Task<VECardCreationModel> CreateMultipleOTCCard(VECardCreationModel model);
        Task<List<VEOTCCardResponse>> GetAvailableVEOTCCardForDealer(string DealerCode);
        Task<VEManageProfile> ManageProfile();
        Task<List<SearchGridResponse>> CardDetailsForSearch(string CustomerId, string CustomerTypeId);
        Task<VEAddonOTCCardMapping> ExistingCustomerCardMap();
        Task<InsertResponse> UpdateVECostomerProfile(string str);
        Task<GetCustomerDetails> GetVEAddonOTCCardMappingCustomerDetails(string customerId);
        Task<VEAddonOTCCardMapping> GetVEAddonOTCCardCustomerDetailsPartialView([FromBody] List<VEAddonOTCCardDetails> arrs);
        Task<VEAddonOTCCardMapping> GetVEAddonOTCCardAddCardsPartialView([FromBody] List<VEAddonOTCCardDetails> arrs);
        Task<VEAddonOTCCardMapping> ExistingCustomerCardMap(VEAddonOTCCardMapping addAddOnCard);
    }
}