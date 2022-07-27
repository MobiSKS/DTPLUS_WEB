using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.DICV
{
    public class DICVCustomerResetPassword : BaseEntity
    {
        public string CustomerId { get; set; }
        public string AlternateEmailId { get; set; }
        public string RegisteredEmailId { get; set; }
    }
}