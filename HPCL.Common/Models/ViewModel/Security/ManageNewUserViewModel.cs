using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public  class ManageNewUserViewModel:CommonResponseBase
    {
        public ManageNewUserViewModel()
        {
            CustomerSecretQueMdl = new List<CustomerSecretQueModel>();
            CustomerSecretQueMdl.Add(new CustomerSecretQueModel
            {
                SecretQuestionId = 0,
                SecretQuestionName = "Select Secret Question"
            });
            getUserRolesandregions = new List<GetUserRolesAndRegions>();
            Data = new List<RolesAssigned>();
        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public virtual List<CustomerSecretQueModel> CustomerSecretQueMdl { get; set; }
        public virtual List<GetUserRolesAndRegions> getUserRolesandregions { get; set; }
        
        public string CreatedDate { get; set; }
        public string LastLogin { get; set; }
        public string UserType { get; set; }
        public List<RolesAssigned> Data { get; set; }
    }
    public class RolesAssigned
    {
        public string UserRole { get; set; }
        public string Location { get; set; }
        public string RoleId { get; set; }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
    }
}
