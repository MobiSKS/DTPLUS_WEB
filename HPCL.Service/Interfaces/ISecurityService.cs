using HPCL.Common.Models.ResponseModel.Security;
using HPCL.Common.Models.ViewModel.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ISecurityService
    {
        Task<BindGridResponse> UserCreationApproval(BindGrid entity);
        Task<ViewRbeDetailsResponse> ViewRbeDetails(string userName);
        Task<string> ApproveRbeUser(string userName, string comments);
        Task<string> RejectRbeUser(string userName, string comments);

    }
}
