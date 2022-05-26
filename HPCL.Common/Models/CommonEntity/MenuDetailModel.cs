using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.CommonEntity
{
    public class MenuDetailModel
    {
        public string UserID { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuNameId { get; set; }
        public Nullable<int> ParentMenuId { get; set; }
        public Nullable<int> MenuLevel { get; set; }
        public Nullable<int> MenuOrder { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ImageUrl { get; set; }
        public string Heading { get; set; }
        public string Details { get; set; }
        public string CalledControllerName { get; set; }
        public string CalledActionName { get; set; }
        public int CalledMenuId { get; set; }
        public int CalledMenuAllowedAction { get; set; }
        public int AllowedAction { get; set; }
    }
}
