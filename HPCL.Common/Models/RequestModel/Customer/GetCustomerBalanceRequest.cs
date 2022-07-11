using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class GetCustomerBalanceRequest: BaseEntity
    {
        public string CustomerID { get; set; }
        public string ParentCustomerID { get; set; }
        public string ChildCustomerId { get; set; }
    }
}