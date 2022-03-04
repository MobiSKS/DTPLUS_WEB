using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.DriverCardCustomer
{
    public class GetCustomerNameByIdResponse
    {
        public int Status_Code { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Message { get; set; }
        public Boolean Success { get; set; }
        public string CustomerName { get; set; }
        public List<CustomerNameData> Data { get; set; }
    }

    public class CustomerNameData
    {
        public string CustomerName { get; set; }
    }
}
