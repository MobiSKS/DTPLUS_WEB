using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CustomerResponse
    {
        public int Status_Code { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Message { get; set; }

        public Boolean Success { get; set; }

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
