using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CustomerRBE
    {
        public int Status_Code { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Message { get; set; }

        public Boolean Success { get; set; }

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
