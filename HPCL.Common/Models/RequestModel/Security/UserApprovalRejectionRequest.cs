using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Security
{
    public class UserApprovalRejectionRequest : BaseEntity
    {
        public string ActionType { get; set; }
        public List<UserApprovalRejectionDetails> TypeApprovalRejectionList { get; set; }
        public UserApprovalRejectionRequest()
        {
            TypeApprovalRejectionList = new List<UserApprovalRejectionDetails>();
        }
    }
    public class UserApprovalRejectionDetails
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}