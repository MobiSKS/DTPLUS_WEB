using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class BindGrid : BaseEntity
    {
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string StatusId { get; set; }
        public string Comments { get; set; }
    }

    public class ViewRbeDetails : BaseEntity
    {
        public string UserName { get; set; }
    }

    public class ApproveRejectRbeUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Comments { get; set; }
        public int Approvalstatus { get; set; }
        public string ApprovedBy { get; set; }
    }
}
