using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class ManageAggregatorNewUserModel : CommonResponseBase
    {
        public ManageAggregatorNewUserModel()
        {

            GetAggregatorRoles = new List<AggregatorRoles>();
        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CreatedDate { get; set; }
        public string LastLogin { get; set; }
        public virtual List<AggregatorRoles> GetAggregatorRoles { get; set; }

    }
    public class AggregatorRoles
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string ControlType { get; set; }
    }
}
