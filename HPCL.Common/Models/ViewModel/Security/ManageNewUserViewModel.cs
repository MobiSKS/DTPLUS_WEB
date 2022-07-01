using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public  class ManageNewUserViewModel
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
        }
        public string NewUserName { get; set; }
        public string NewEmail { get; set; }
        public string NewPassword { get; set; }
        public string NewConfirmPassword { get; set; }
        public string NewSecretQuestion { get; set; }
        public string NewSecretAnswer { get; set; }
        public virtual List<CustomerSecretQueModel> CustomerSecretQueMdl { get; set; }
        public virtual List<GetUserRolesAndRegions> getUserRolesandregions { get; set; }
        

    }
    
}
