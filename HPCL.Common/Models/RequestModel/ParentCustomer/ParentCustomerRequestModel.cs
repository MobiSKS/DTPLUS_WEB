using HPCL.Common.Models.CommonEntity;
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
    }
}
