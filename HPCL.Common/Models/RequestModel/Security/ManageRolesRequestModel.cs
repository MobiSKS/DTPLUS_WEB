using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Security
{
    public class ManageRolesRequestModel:BaseEntity
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string RoleDescription { get; set; }
        public string MainLevelId { get; set; }
        public string SubLevelId { get; set; }
        public string SubLevelName { get; set; }
        public List<UpdateRoleModel> ObjUpdate { get; set; }
        public List<UpdateRoleModel> TypeInsertAddManageUsers { get; set; }
        public List<DeleteRoleModel> TypeRoleNameAndRoleDescriptionMapping { get; set; }
    }
    public class UpdateRoleModel
    {
        public string MenuId { get; set; }
        public string AllowedAction { get; set; }
    }
    public class DeleteRoleModel
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
