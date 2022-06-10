using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class GetUserManageMenuModel:CommonResponseBase
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<GetUserManageMenu> Data { get; set; }
    }
    public class GetUserManageMenu
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string ParentMenuId { get; set; }
        public string MenuLevel { get; set; }
        public string MenuOrder { get; set; }
        public string Status { get; set; }
        public string MenuNameId { get; set; }
        public string Reason { get; set; }
    }
}
