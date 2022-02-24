using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Officer
{
    public class OfficerDetailsRequestModal : BaseEntity
    {
        public string OfficerType { get; set; }
        public string Location { get; set; }
    }
}
