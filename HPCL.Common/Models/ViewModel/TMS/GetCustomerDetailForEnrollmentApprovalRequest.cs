using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.TMS
{
    public class GetCustomerDetailForEnrollmentApprovalRequest: BaseEntity
    {
        public string CustomerID { get; set; }
        public string TMSUserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TMSStatus { get; set; }
    }
}
