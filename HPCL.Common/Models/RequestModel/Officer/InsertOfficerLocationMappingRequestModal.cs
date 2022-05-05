using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Officer
{
    public class InsertOfficerLocationMappingRequestModal : BaseEntity
    {
        public int OfficerId { get; set; }
        public string UserName { get; set; }
        public int ZO { get; set; }
        public int RO { get; set; }
        //public string Createdby { get; set; }
    }
}
