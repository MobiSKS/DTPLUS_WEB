using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.ManageRbe
{
    public class GetApproveChangeRbeMobile : BaseEntity
    {
        public string ApprovalStatus { get; set; }
        public string FirstName { get; set; }
        public string RBEId { get; set; }
    }
}
