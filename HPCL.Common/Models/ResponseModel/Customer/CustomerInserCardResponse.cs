using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CustomerInserCardResponse : CommonResponseBase
    {
        public List<CustomerInserCardResponseData> Data { get; set; }
    }

    public class CustomerInserCardResponseData
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
