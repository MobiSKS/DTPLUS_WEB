using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class GetDistrictListRequestModel: BaseEntity
    {
        public string StateID { get; set; }
    }
}
