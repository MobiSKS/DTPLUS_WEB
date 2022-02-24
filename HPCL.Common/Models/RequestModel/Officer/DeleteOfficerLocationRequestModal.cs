using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Officer
{
    public class DeleteOfficerLocationRequestModal : BaseEntity
    {
        public string UserName { get; set; }
        public int ZO { get; set; }
        public int RO { get; set; }
        public string ModifiedBy { get; set; }
    }
}
