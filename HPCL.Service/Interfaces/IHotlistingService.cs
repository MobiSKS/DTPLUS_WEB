using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Hotlisting;
using HPCL.Common.Models.ResponseModel.Hotlisting;
using HPCL.Common.Models.ViewModel.Hotlisting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IHotlistingService
    {
        Task<List<string>> ApplyHotlistorReactivate([FromBody] HotlistorReactivateViewModel HotlistorReactivateViewModel);
        Task<ViewHotlistingorReactivateResponse> GetHotlistedorReactivatedData(string EntityTypeId, string EntityId);
        Task<GetHotlistApprovalResponse> GetHotlistApproval(string EntityTypeId, string ActionId, string FromDate, string ToDate);
        Task<List<HotlistSuccessResponse>> UpdateHotlistApproval([FromBody] HotlistApprovalRequest hotlistApprovalRequest);
        Task<List<HotlistSuccessResponse>> CheckEntityAlreadyHotlisted(string EntityTypeId,string EntityId);
    }
}
