using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class GetUserResetPassword : BaseEntity
    {
        public string UserName { get; set; }
        public string EmailId { get; set; }
    }
}
