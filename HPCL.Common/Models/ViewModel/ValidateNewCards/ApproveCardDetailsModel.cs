
using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.ValidateNewCards
{
    public class ApproveCardDetailsModel : BaseEntity
    {
        public string CustomerReferenceNo { get; set; }
        public string Comments { get; set; }
        public string Approvalstatus { get; set; }
        public string ApprovedBy { get; set; }
        public string FormNumber { get; set; }
    }
}