using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.AshokLeyLand;
using Microsoft.AspNetCore.Mvc;
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
        Task<List<OTCCardDetails>> GetAvailableAlOTCCardForDealer(string DealerCode);
        Task<AshokLeylandCardCreationModel> CreateMultipleOTCCard(AshokLeylandCardCreationModel ashokLeylandCardCreationModel);
        Task<ViewALOTCCardDealerMappingModel> ViewALOTCCardsDealerMapping();
        Task<ALOTCCardDealerAllocationResponse> GetViewALOTCCardDealerAllocation(string DealerCode, string CardNo);
        Task<AddonOTCCardMapping> AddonOTCCardMapping();
        Task<GetCustomerDetails> GetAlAddonOTCCardMappingCustomerDetails(string customerId);
        Task<AddonOTCCardMapping> GetAlAddonOTCCardCustomerDetailsPartialView([FromBody] List<AddonOTCCardDetails> arrs);
        Task<AddonOTCCardMapping> GetAlAddonOTCCardAddCardsPartialView([FromBody] List<AddonOTCCardDetails> arrs);
        Task<AddonOTCCardMapping> AddonOTCCardMapping(AddonOTCCardMapping addAddOnCard);
        Task<AddonOTCCardMapping> GetAlSalesExeEmpIdAddOnOTCCardMapping(string dealerCode);
        Task<AshokLeylandCustomerUpdateModel> UpdateALCustomer();
        Task<AshokLeylandCustomerUpdateModel> GetCustomerAddress(string CustomerId);
        Task<AshokLeylandCustomerUpdateModel> UpdateALCustomer(AshokLeylandCustomerUpdateModel model);
        Task<AshokLeylandCardCreationModel> GetMultipleOTCCardPartialView([FromBody] List<ALCardEntryDetails> arrs);
        Task<GetAlCustomerDetailForVerificationModel> VerifyCustomerDocuments(GetAlCustomerDetailForVerificationModel model);
        Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> AproveRejectCustomer(string CustomerID, string Comments, string Approvalstatus);
        Task<List<StatusResponseModal>> GetAlStatusType();
        Task<ALCustomerProfileModel> ManageProfile();
        Task<List<CustomerProfileResponse>> BindCustomerDetailsForSearch(string CustomerId, string NameOnCard);
        Task<List<SearchGridResponse>> CardDetailsForSearch(string CustomerId, string CustomerTypeId);
        Task<InsertResponse> UpdateAlCostomerProfile(string str);
    }
}