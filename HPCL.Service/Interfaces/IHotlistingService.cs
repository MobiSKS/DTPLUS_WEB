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
        Task<string> ApplyHotlistorReactivate([FromBody] HotlistorReactivateViewModel HotlistorReactivateViewModel);
        Task<ViewHotlistingorReactivateResponse> GetHotlistedorReactivatedData(string EntityTypeId, string EntityId);
    }
}
