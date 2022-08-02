using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class ManageRolesViewModel:CommonResponseBase
    {
        public ManageRolesViewModel()
        {
            Data = new List<ManageRoleDetails>();
        }
        public string MenuNameId { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<ManageRoleDetails> Data { get; set; }
    }
    public class ManageRoleDetails
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string ID { get; set; }
    }
}
