using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class UserCreationApprovalNonRBEModel : CommonResponseBase
    {
        public UserCreationApprovalNonRBEModel()
        {
            Data = new List<UserCreationApprovalDetails>();
        }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Comments { get; set; }
        public virtual List<UserCreationApprovalDetails> Data { get; set; }
    }
    public class UserCreationApprovalDetails
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SubLevelName { get; set; }
        public string Comments { get; set; }
        public string RequestedOn { get; set; }
        public string RequestedBy { get; set; }
    }
}