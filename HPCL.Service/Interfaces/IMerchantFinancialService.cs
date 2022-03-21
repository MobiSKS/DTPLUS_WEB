using HPCL.Common.Models.ResponseModel.MerchantFinancial;
using HPCL.Common.Models.ViewModel.MerchantFinancials;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IMerchantFinancialService
    {
        Task<UploadMerchantCautionLimitResponse> ViewUploadMerchantCautionLimit(GetUploadMerchantCautionLimit entity);
        Task<MerchantSettlementDetailsResponse> SettlementDetails(GetMerchantSettlementDetails entity);
        Task<BatchDetailsResponse> GetBatchDetails(string terminalId, int batchId);
        Task<GetTerminalDetailsResponse> GetTerminalDetails(string terminalId);
    }
}
