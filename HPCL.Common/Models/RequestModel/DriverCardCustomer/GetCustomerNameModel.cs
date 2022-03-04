using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.DriverCardCustomer
{
    public class GetCustomerNameModel : BaseEntity
    {
        public string CustomerName { get; set; }
        public string CustomerID { get; set; }
    }
}
