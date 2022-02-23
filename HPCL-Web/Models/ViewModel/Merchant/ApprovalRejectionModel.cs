using System.Collections.Generic;

namespace HPCL_Web.Models.ViewModel.Merchant
{
    public class ApprovalRejectionModel
    {
        public ApprovalRejectionModel()
        {
            ObjApprovalRejectDetail = new List<ErpNComments>();
        }
        public string UserId { get; set; }
        public string Useragent { get; set; }
        public string Userip { get; set; }
        public string StatusId { get; set; }
        public string ApprovedBy { get; set; }
        public List<ErpNComments> ObjApprovalRejectDetail { get; set; }
    }

    public class ErpNComments
    {
        public string ErpCode { get; set; }
        public string Comments { get; set; }
    }
}
