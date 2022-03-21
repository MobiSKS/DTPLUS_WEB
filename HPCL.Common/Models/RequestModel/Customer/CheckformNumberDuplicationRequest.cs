using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class CheckformNumberDuplicationRequest: BaseEntity
    {
        public string FormNumber { get; set; }
    }
}
