using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Hotlisting
{
    public class HotlistingRequestModel : BaseEntity
    {
        public string EntityIdVal { get; set; }
        public string Remarks { get; set; }
        public int EntityTypeId { get; set; }
        public int ActionId { get; set; }
        public int ReasonId { get; set; }
        public string ReasonDetails { get; set; }
        public string FromDate { get; set; }    
        public string ToDate { get; set; }
    }
}
