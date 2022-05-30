using System.Collections.Generic;

namespace HPCL.Common.Models.CommonEntity
{
    public static class SessionMenuModel
    {
        static SessionMenuModel()
        {
            menuList = new List<MenuDetailModel>();
            sessionList = new List<SessionDataModelDetails>();
        }
        public static List<MenuDetailModel> menuList { get; set; }
        public static List<SessionDataModelDetails> sessionList { get; set; }
    }
}
