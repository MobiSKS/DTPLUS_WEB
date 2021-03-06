using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CustomerResponse: CommonResponseBase
    {
        public List<CustomerResponseData> Data { get; set; }
    }

    public class CustomerResponseData
    {
        public string ReferenceId { get; set; }
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }

    }
}
