using HPCL_Web.Models.CommonEntity;

namespace HPCL_Web.Models.ViewModel.ValidateNewCards
{
    public class ApproveCardDetailsModel : BaseEntity
    {
        public string CustomerReferenceNo { get; set; }
        public string Comments { get; set; }
        public string Approvalstatus { get; set; }
        public string ApprovedBy { get; set; }
    }
}
