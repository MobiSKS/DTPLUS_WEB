using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CustomerAddressUpdateResponse : CommonResponseBase
    {
        public List<CustomerAddressUpdateResponseData> Data { get; set; }
    }

    public class CustomerAddressUpdateResponseData
    {
        public int Status { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
    }
}