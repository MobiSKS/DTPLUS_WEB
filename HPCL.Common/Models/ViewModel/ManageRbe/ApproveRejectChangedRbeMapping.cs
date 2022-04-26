using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.ManageRbe
{
    public class ApproveRejectChangedRbeMapping : BaseEntity
    {
        public string PreRBEUserName { get; set; }
        public string MappingStatus { get; set; }
    }
}
