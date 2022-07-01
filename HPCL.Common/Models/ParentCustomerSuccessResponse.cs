using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models
{
    public  class ParentCustomerSuccessResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
        public string ActionName { get; set; }
        public string customerId { get; set; }
    }

}
