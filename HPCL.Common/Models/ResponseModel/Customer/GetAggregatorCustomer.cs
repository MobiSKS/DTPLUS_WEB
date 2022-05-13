using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class GetAggregatorCustomer:CommonResponseBase
    {
        public string FormNumber { get; set; }  
        public string CustomerReferenceNo { get; set; }  
        public string CustomerName { get; set; }    
        public string CustomerAddress { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; } 
    }
}
