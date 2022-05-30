using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ViewModel.TMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.TMS
{
    public class UpdateCustomerDetailForEnrollmentApprovalRequest: BaseEntity
    {
        public string CustomerID { get; set; }
        public UpdateCustomerDetailForEnrollmentApprovalRequest()
        {
            customerDetailForEnrollmentApproval = new List<CustomerDetailForEnrollmentApproval>();
        }
        public virtual List<CustomerDetailForEnrollmentApproval> customerDetailForEnrollmentApproval { get; set; }
    }
    public class CustomerDetailForEnrollmentApproval
    {
        public string CustomerID { get; set; }
        public string Remarks { get; set; }
        public string TMSStatus { get; set; }
    }
}