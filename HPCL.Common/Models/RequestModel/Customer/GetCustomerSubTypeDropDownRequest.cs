using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class GetCustomerSubTypeDropDownRequest : BaseEntity
    {
        public string CustomerTypeId { get; set; }
    }
}
