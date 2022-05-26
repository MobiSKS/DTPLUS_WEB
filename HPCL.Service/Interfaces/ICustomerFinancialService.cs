using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.CustomerFinancial;
using HPCL.Common.Models.ViewModel.CustomerFinancial;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerFinancialService
    {
        Task<CardToCCMSBalanceTransferSearchResponse> SearchCardToCCMSTransfer(BalanceTransferSearchModel entity);
        Task<CCMSToCardBalanceTransferSearchResponse> SearchCCMSToCardTransfer(BalanceTransferSearchModel entity);
        Task<CardToCardBalanceTransferSearchResponse> SearchCardToCardTransfer(BalanceTransferSearchModel entity);
        Task<CustomerTransactionResponseModel> GetCustomerTransactionDetails(string CustomerID, string CardNo, string MobileNo, string FromDate, string ToDate);
        Task<GetViewAccountStatementResponse> ViewAccountStatement(GetViewAccountStatement entity);
        Task<List<SuccessResponse>> CCMSToCardAmtTransfer(string customerId, string ccmsToCardTransfer);
        Task<CardToCardAmountTransferRes> CardToCardAmtTransfer(string customerId, string cardToCardTransfer);
        Task<List<SuccessResponse>> CardToCCMSAmtTransfer(string customerId, string cardToCCMSTransfer);
    }
}
