using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Customer;
using HPCL.Common.Models.RequestModel.ParentCustomer;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.ParentCustomer;
using HPCL.Common.Models.ViewModel.Aggregator;
using HPCL.Common.Models.ViewModel.ParentCustomer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IParentCustomerService
    {
        Task<ManageProfileViewModel> ManageProfile(string CustomerId, string NameOnCard);
        Task<ManageProfileViewModel> ManageProfile(ManageProfileViewModel cust);
        Task<ParentCustomerApprovalModel> ApproveParentCustomer(ParentCustomerApprovalModel ApprovalMdl);
        Task<List<string>> ActionParentCustomerApproval([FromBody] ApproveParentCustomer approveParentCustomer);
        Task<ParentCustomerApprovalModel> AuthorizeParentCustomer(ParentCustomerApprovalModel ApprovalMdl);
        Task<List<string>> ActionParentCustomerAuthorize([FromBody] ApproveParentCustomer approveParentCustomer);
        Task<ViewCustomerCardorDispatchDetails> GetCardDetails(string CustomerId, string RequestId);
        Task<ViewCustomerCardorDispatchDetails> GetDispatchDetails(string CustomerId, string RequestId);
        Task<ManageProfileViewModel> UpdateParentCustomer(string CustomerId, string RequestId);
        Task<ManageProfileViewModel> UpdateParentCustomer(ManageProfileViewModel cust);
        Task<ParentCustomerReportModel> SearchParentCustomerRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string FormNumber,string SBUtypeId);
        Task<ParentCustomerReportModel> SearchParentCustomerRequestStatusReport(string FormNumber, string RequestId);
        Task<ParentCustomerBalanceInfoModel> GetCustomerBalanceInfo(ParentCustomerBalanceInfoModel requestInfo);
        Task<ParentCustomerBalanceInfoModel> GetCustomerDetailsByCustomerID(string CustomerID);
        Task<CustomerCCMSBalanceModel> GetCCMSBalanceDetails(string CustomerID);
        Task<GetCustomerCardWiseBalanceResponse> GetCustomerCardWiseBalance(string CustomerID);
        Task<ParentCustomerTransactionViewModel> ParentCustomerTransactionDetails(ParentCustomerTransactionViewModel requestInfo);
        Task<ParentChildCustomerMappingViewModel> ParentChildCustomerMapping(ParentChildCustomerMappingRequest requestInfo);
        Task<ViewParentChildTransactionDetailsModel> ViewParentChildTransactionDetails(String ParentCustomerID);
        Task<List<SuccessResponse>> ValidateChildCustomerId(string CustomerId);
        Task<ParentChildCustomerMappingViewModel> ConfirmParentChildCustomerMapping(ParentChildCustomerMappingRequest requestInfo);
        Task<CustomerDriveStarsDetailsModel> GetDriveStarsDetails(string CustomerID);
        Task<List<SuccessResponse>> ValidateParentCustomerId(string CustomerId);
        Task<ControlCardSearchModel> GetCustomerControlCard(string CustomerID);
        Task<List<SuccessResponse>> SubmitRestPinforParentCustomer([FromBody] ControlCardPinRestRequestModel reqModel);
        Task<List<SuccessResponse>> ConfigureSMSAlerts([FromBody] ParentCustomerSearchRequestModel reqModel);
        Task<PCConfigureSMSAlertModel> GetPCAvailableSMSAlerts(string CustomerID);
        Task<List<SuccessResponse>> UpdateDndSmsAlertsConfigure(string CustomerId);
        Task<AccountStatementRequestViewModel> GetAccountStatementRequest(string CustomerID);
        Task<List<SuccessResponse>> UpdateAccountStatementRequest([FromBody] AccountStatementRequestModel reqEntity);
        Task<List<SuccessResponse>> InsertAccountStatementRequest([FromBody] AccountStatementRequestModel reqEntity);
        Task<ViewParentChildTransactionDetailsModel> GetParentChildTransactionDetails(ParentChildransactionRequestModel requestInfo);
        Task<BasicSearchViewModel> CustomerBasicSearch(BasicSearchViewModel reqEntity);
        Task<ViewParentChildTransactionDetailsModel> GetTransactionLocationDetails(PCTransactionLocationrequest requestInfo);
        Task<ConvertParenttoAggregatorViewModel> ConvertParentToAggregator(string CustomerId, string NameOnCard);
        Task<ManageAggregatorViewModel> UpdateParentasAggregatorCustomer(ManageAggregatorViewModel cust);
        Task<ParentChildBalanceTransferViewModel> ParentChildBalanceFundTransfer(ParentChildBalanceTransferViewModel reqEntity);
    }
}
