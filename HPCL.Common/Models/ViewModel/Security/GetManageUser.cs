using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class GetManageUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LastLoginTime { get; set; }
        public string UserRole { get; set; }
        public int ShowDisabled { get; set; }
    }
}
