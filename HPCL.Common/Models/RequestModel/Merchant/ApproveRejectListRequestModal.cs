using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Merchant
{
    public class ApproveRejectListRequestModal : BaseEntity
    {
        public ApproveRejectListRequestModal()
        {
            ObjApprovalRejectDetail = new List<ErpNCommentsModal>();
        }
        public string StatusId { get; set; }
        public string ApprovedBy { get; set; }
        public string CategoryId { get; set; }
        public List<ErpNCommentsModal> ObjApprovalRejectDetail { get; set; }
    }

    public class ErpNCommentsModal
    {
        public string ErpCode { get; set; }
        public string Comments { get; set; }
    }
}
