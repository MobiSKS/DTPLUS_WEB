using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Officer
{
    public class DeleteOfficerRequestModal : BaseEntity
    {
        public string OfficerID { get; set; }
        public string ModifiedBy { get; set; }
    }
}
