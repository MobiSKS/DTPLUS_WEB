using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class AddNewAggregatorUser:BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
       
        public virtual List<DeleteLocations> TypeAddManageAggregatorUsers { get; set; }
       
    }
    public class AddUserDetails
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string ControlType { get; set; }
    }
  
}
