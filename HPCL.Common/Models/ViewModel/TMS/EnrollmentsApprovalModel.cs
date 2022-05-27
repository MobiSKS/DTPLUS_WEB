using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.TMS
{
    public class EnrollmentsApprovalModel: CommonResponseBase
    {
        public EnrollmentsApprovalModel()
        {
            StatusResponseMdl = new List<StatusResponseModal>();
            Data = new List<EnrollmentsApprovalDetails>();
        }
        public virtual List<StatusResponseModal> StatusResponseMdl { get; set; }
        public string TMSUserId { get; set; }
        public string CustomerID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int TMSStatus { get; set; }
        public virtual List<EnrollmentsApprovalDetails> Data { get; set; }
    }
    public class EnrollmentsApprovalDetails
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string RequestedDate { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
    }
}
