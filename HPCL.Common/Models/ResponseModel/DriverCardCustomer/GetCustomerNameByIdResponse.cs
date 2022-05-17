using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.DriverCardCustomer
{
    public class GetCustomerNameByIdResponse : CommonResponseBase
    {
        public List<CustomerNameData> Data { get; set; }
    }

    public class CustomerNameData
    {
        public string CustomerName { get; set; }
        public string PanCardNo { get; set; }
    }
}
