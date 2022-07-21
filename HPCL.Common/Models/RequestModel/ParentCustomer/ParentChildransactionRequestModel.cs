using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.ParentCustomer
{
    public class ParentChildransactionRequestModel:BaseEntity
    {
        public string ParentCustomerID { get; set; }
        public string ChildCustomerId { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CustomerId { get; set; }
        public string TransactionId { get; set; }
        public List<ChildCustomerRequestDetails> ObjChildCustomerIdDtl { get; set; }
    }
    public class ChildCustomerRequestDetails
    {
        public int Id { get; set; }
        public string ChildCustomerID { get; set; }
    }
}
