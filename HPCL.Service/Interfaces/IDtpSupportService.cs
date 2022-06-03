using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.DtpSupport;
using HPCL.Common.Models.ViewModel.DtpSupport;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IDtpSupportService
    {
        Task<GetBlockUnblockCustomerCcmsAccountByCustomeridResponse> GetBlockUnblockCustomerCcmsAccount(string customerId);
        Task<string> UpdateCustomerCcmsAccountStatus (BlockUnblockCustomerCcmsAccount entity);
        Task<List<SuccessResponse>> GetCardBalanceTransferDetails(string CardNo);
        Task<UnblockUserModel> UnblockUser();
        Task<GeneralUpdatesModel> GeneralUpdates();
        Task<GetEntityOldFieldValueResponse> GetEntityOldFieldValue(string EntityTypeId, string EntityFieldId, string CustomerIdOrCardOrMerchantId);
        Task<GeneralUpdatesModel> GeneralUpdates(GeneralUpdatesModel model);
        Task<TeamMappingViewModel> TeamMappingSearch(TeamMappingViewModel teamMappingViewModel);
        Task<List<SuccessResponse>> AddTeamMapping(TeamMappingViewModel teamMappingViewModel);
        Task<List<SuccessResponse>> DeleteTeamMapping(string teamMappingId);
        Task<List<SuccessResponse>> UpdateTeamMapping(TeamMappingViewModel teamMappingViewModel);
        Task<GetDetailForUserUnblockResponse> GetDetailForUserUnblock(string CustomerId, string UserName);
        Task<UnblockUserModel> UnblockUser(UnblockUserModel model);
    }
}