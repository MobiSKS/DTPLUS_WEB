using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class GetValidateNewCustomerRequestModel : BaseEntity
    {
        public string Createdon { get; set; }
        public string FormNumber { get; set; }
        public string StateId { get; set; }
        public string CustomerName { get; set; }
    }
}