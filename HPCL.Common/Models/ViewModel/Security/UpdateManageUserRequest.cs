﻿using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class UpdateManageUserRequest : BaseEntity
    {
        public string UserName { get; set; }
        public string Actions { get; set; }
    }
}
