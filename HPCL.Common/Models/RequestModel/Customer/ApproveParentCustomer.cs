using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class ApproveParentCustomer:BaseEntity
    {
        public ApproveParentCustomer()
        {
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Approvedby { get; set; }
        public string Action { get; set; }
        public int ActionType { get; set; }
        public List<ApproveParentCustomerRequest> ObjParentCustomerDtl { get; set; }
    }
    public class ApproveParentCustomerRequest
    {
        public string Id { get; set; }
        public string FormNumber { get; set; }
        public string Comment  { get; set; }
        public string ReferenceId { get; set; }

    }
}
