using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class CustomerResetPasswordRequest:BaseEntity
    {
        public string CustomerId { get; set; }
        public string AlternateEmailId { get; set; }
    }
}
