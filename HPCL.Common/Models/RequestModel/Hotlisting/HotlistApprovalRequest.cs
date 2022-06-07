using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Hotlisting
{
    public class HotlistApprovalRequest : BaseEntity
    {
        public int EntityTypeId { get; set; }
        public int ActionId { get; set; }
        public string ActionOnRequest { get; set; }
        public virtual List<HotlistApprovalReqDetails> ObjUpdateHotlistApprovalEntityCode { get; set; }
    }
    public class HotlistApprovalReqDetails
    {
        public string entityCode { get; set; }
    }
}
