using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Officer
{
    public class GetOfficerDetailsRequestModal : BaseEntity
    {
        public string ZO { get; set; }
        public string RO { get; set; }
        public string StateId { get; set; }
        public string DistrictId { get; set; }
    }
}
