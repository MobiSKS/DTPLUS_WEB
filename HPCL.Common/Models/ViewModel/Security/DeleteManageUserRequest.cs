using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class DeleteManageUserRequest : BaseEntity
    {
        public TypeDeleteUserManage[] TypeDeleteUserManage { get; set; }
    }

    public class TypeDeleteUserManage
    {
        public string UserName { get; set; }
    }
}
