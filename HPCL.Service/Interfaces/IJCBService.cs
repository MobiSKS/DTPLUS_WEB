using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
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
        Task<JCBOTCCardDealerAllocationResponse> GetViewJCBOTCCardDealerAllocation(string DealerCode, string CardNo);
        Task<JCBCustomerEnrollmentModel> JCBCustomerEnrollment();
        Task<GetSalesExecutiveEmployeeIDResponse> GetJCBSalesExeEmpId(string dealerCode);
        Task<JCBCustomerEnrollmentModel> GetJCBCustomerOTCCardPartialView([FromBody] List<JCBCardEntryDetails> arrs);
        Task<List<JCBOTCCardDetails>> GetAvailableJCBOTCCardForDealer(string DealerCode);
        Task<JCBCustomerEnrollmentModel> JCBCustomerEnrollment(JCBCustomerEnrollmentModel customerModel);
        Task<JCBManageProfile> JCBManageProfile();
        Task<List<JCBCustomerProfileResponse>> BindCustomerDetailsForSearch(string CustomerId, string NameOnCard);
        Task<List<SearchGridResponse>> CardDetailsForSearch(string CustomerId, string CustomerTypeId);
        Task<GetJCBCommunicationEmailResetPasswordResponse> GetJCBCommunicationEmailResetPassword(string CustomerId);
        Task<InsertResponse> UpdateJCBCommunicationEmailResetPassword(string CustomerId, string AlternateEmailId);
        Task<JCBSearchManageCards> JCBManageCards(JCBCustomerCards entity, string editFlag);
        Task<JCBSearchDetailsByCardId> JCBViewCardDetails(string CardId);
        Task<JCBUpdateMobileModal> JCBCardlessMapping(string cardNumber, string mobileNumber, string LimitTypeName, string CCMSReloadSaleLimitValue);
        Task<List<SuccessResponse>> JCBCardlessMappingUpdate(string mobNoNew, string crdNo);
    }
}