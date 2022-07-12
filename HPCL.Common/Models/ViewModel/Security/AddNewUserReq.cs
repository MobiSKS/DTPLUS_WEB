using HPCL.Common.Models.CommonEntity;
using System.Collections.Generic;

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StateId { get; set; }
        public string ActionType { get; set; }
        public string UserRole { get; set; }
        public virtual List<UserLocations> TypeManageUsersAddUserRole { get; set; }
        public virtual List<UserLocations> TypeManageUsersAddUserRoleWithStatusFlag { get; set; }
        public string UpdateStatus { get; set; }
        public string RoleId { get; set; }
    }
    public class UserLocations
    {
        public string ZO { get; set; }
        public string RO { get; set; }
        public string StatusFlag { get; set; }
    }
}
