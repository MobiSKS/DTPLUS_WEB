using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.RequestModel.JCB;
using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ResponseModel.JCB;
using HPCL.Common.Models.ResponseModel.VolvoEicher;
using HPCL.Common.Models.ViewModel.JCB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IJCBService
    {
        Task<CommonResponseData> CheckJCBDealerCodeExists(string DealerCode);
        Task<InsertResponse> InsertJCBDealerEnrollment(string str);
        Task<InsertResponse> JCBDealerEnrollmentUpdate(string getAllData);
        Task<SearchAlResult> SearchJCBDealer(string dealerCode, string dtpCode, string OfficerType);
        Task<JCBOTCCardRequestModel> JCBDealerOTCCardRequest();
        Task<GetJCBBalanceOTCCardResponse> CheckJCBDealerBalanceQty(string DealerCode);
        Task<JCBOTCCardRequestModel> JCBDealerOTCCardRequest(JCBOTCCardRequestModel model);
        Task<ViewJCBUnmappedOTCCardsModel> ViewJCBUnmappedOTCCards();
        Task<JCBOTCCardDealerAllocationResponse> GetViewJCBOTCCardDealerAllocation(string DealerCode, string CardNo, bool ShowUnmappedCard);
        Task<JCBCustomerEnrollmentModel> JCBCustomerEnrollment();
        Task<GetSalesExecutiveEmployeeIDResponse> GetJCBSalesExeEmpId(string dealerCode);
        Task<JCBCustomerEnrollmentModel> GetJCBCustomerOTCCardPartialView([FromBody] List<JCBCardEntryDetails> arrs);
        Task<List<JCBOTCCardDetails>> GetAvailableJCBOTCCardForDealer(string DealerCode);
        Task<JCBCustomerEnrollmentModel> JCBCustomerEnrollment(JCBCustomerEnrollmentModel customerModel);
        Task<JCBManageProfile> JCBManageProfile();
        Task<List<JCBCustomerProfileResponse>> BindCustomerDetailsForSearch(string CardNo, string Email, string CustomerId, string MobileNo);
        Task<List<SearchGridResponse>> CardDetailsForSearch(string CustomerId, string CustomerTypeId);
        Task<GetJCBCommunicationEmailResetPasswordResponse> GetJCBCommunicationEmailResetPassword(string CustomerId);
        Task<InsertResponse> UpdateJCBCommunicationEmailResetPassword(string CustomerId, string AlternateEmailId);
        Task<JCBSearchManageCards> JCBManageCards(JCBCustomerCards entity, string editFlag);
        Task<JCBSearchDetailsByCardId> JCBViewCardDetails(string CardId);
        Task<JCBUpdateMobileModal> JCBCardlessMapping(string cardNumber, string mobileNumber, string LimitTypeName, string CCMSReloadSaleLimitValue);
        Task<List<SuccessResponse>> JCBCardlessMappingUpdate(string mobNoNew, string crdNo);
        Task<JCBViewCardSearch> SearchCardMapping(JCBViewCardDetails viewCardDetails);
        Task<List<string>> UpdateCards(JCBUpdateMobileandFastagNoInCard[] limitArray);
        Task<JCBViewCardSearch> AddCardMappingDetails(JCBViewCardDetails viewCardDetails);
        Task<GetJCBDealerCardDispatchResponse> GetJCBDealerCardDispatchDetails(string CustomerID);
        Task<InsertResponse> ResetJCBDealerPassword(string UserName);
        Task<JCBHotlistorReactivateViewModel> JCBHotlistAndReactivate();
        Task<List<JCBHotlistReason>> GetReasonListForEntities(string EntityTypeId);
        Task<List<string>> ApplyHotlistorReactivate([FromBody] JCBHotlistorReactivateViewModel hotlistorReactivateViewModel);
        Task<InsertResponse> EnableDisableJCBDealer(string DealerCode, string OfficerType, string EnableDisableFlag);
        Task<JCBViewDealerOTCCardStatusModel> ViewJCBDealerOTCCardStatus();
        Task<GetJCBDealerOTCCardStatusResponse> GetViewJCBDealerOTCCardStatus(string DealerCode, string CardNo);
    }
}