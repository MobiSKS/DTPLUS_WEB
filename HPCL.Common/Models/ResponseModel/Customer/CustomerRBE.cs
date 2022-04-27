using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CustomerRBE : CommonResponseBase
    {
        public List<CustomerRBEData> Data { get; set; }
    }

    public class CustomerRBEData
    {
        public string RBEId { get; set; }
        public string RBEName { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}
