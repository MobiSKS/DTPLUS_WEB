using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.DICV
{
    public class DICVHotlistingRequestModel : BaseEntity
    {
        public string Remarks { get; set; }
        public int EntityTypeId { get; set; }
        public int ActionId { get; set; }
        public int ReasonId { get; set; }
        public string RemarksOthers { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
    }
}