using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Security
{
    public class UserCreationViewRequest : BaseEntity
    {
        public string UserName { get; set; }
        public string Status { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FirstName { get; set; }
    }
}