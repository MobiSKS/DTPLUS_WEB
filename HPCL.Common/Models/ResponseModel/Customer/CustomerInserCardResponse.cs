using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CustomerInserCardResponse
    {
        public int Status_Code { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Message { get; set; }

        public Boolean Success { get; set; }

        public List<CustomerInserCardResponseData> Data { get; set; }
    }

    public class CustomerInserCardResponseData
    {
        public int Status { get; set; }
        public string Reason { get; set; }

    }
}
