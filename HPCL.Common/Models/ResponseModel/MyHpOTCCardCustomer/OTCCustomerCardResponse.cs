using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer
{
    public class OTCCustomerCardResponse : CommonResponseBase
    {
        public List<OTCCustomerCardResponseData> Data { get; set; }
    }

    public class OTCCustomerCardResponseData
    {
        public string ReferenceId { get; set; }
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public string CustomerID { get; set; }
        public string Password { get; set; }

    }

}