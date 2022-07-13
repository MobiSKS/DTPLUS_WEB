using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ViewModel.ParentCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.ParentCustomer
{
    public  class ParentCustomerRequestModel:BaseEntity
    {
        public string ZO { get; set; }
        public string RO { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FormId { get; set; }
        public string FormNumber { get; set; }
        public string RequestId { get; set; }
        
        public string ParentCustomerID { get; set; }
        public virtual List<ChildCustomerDetails> ChildCustomerIds { get; set; }
    }
}
