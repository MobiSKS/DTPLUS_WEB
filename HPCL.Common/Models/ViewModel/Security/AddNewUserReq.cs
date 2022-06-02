using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class AddNewUserReq : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int SecretQuestion { get; set; }
        public string SecretQuestionAnswer { get; set; }
    }
}
