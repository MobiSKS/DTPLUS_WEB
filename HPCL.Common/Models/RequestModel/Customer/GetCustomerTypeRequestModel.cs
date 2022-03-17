using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class GetCustomerTypeRequestModel: BaseEntity
    {
        public string CTFlag { get; set; }
    }
}
