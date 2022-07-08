using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public  class GetUserManageRoleModel:CommonResponseBase
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public GetUserManageRoleList Data { get; set; }
    }
    public class GetUserManageRoleList
    {
        public List<MainandSubLevelDetails> tblMainAndSubLevelRoleMap { get; set; }
        public List<MenuDetails> tblMenuDetails { get; set; }
    }
    public class MainandSubLevelDetails
    {
        public string SubLevelName { get; set; }
        public string Description { get; set; }
    }
    public class MenuDetails    
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string ParentMenuId { get; set; }
        public string MenuLevel { get; set; }
        public string MenuOrder { get; set; }
        public string AllowedAction { get; set; }
        public string IsFinalPage { get; set; }
    }
}
