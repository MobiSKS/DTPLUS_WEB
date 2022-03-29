using HPCL.Common.Models.ResponseModel.ManageRbe;
using HPCL.Common.Models.ViewModel.ManageRbe;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IManageRbeService
    {
        Task<ChangeRbeMappingResponse> ChangeRbeMapping(RbeMapping entity);
        Task<RbeUserListResponse> RbeUserList(RbeUserList entity);
    }
}
