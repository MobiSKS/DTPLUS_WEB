using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.ParentCustomer
{
    public class PCTransactionLocationrequest:BaseEntity
    {
        public string CustomerId { get; set; }
        public string TransactionId { get; set; }
    }
}
