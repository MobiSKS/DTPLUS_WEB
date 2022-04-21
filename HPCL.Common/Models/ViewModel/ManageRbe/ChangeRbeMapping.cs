using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.ManageRbe
{
    public class ChangeRbeMapping : BaseEntity
    {
        public string PreRBEUserName { get; set; }
        public string NewRBEUserName { get; set; }
    }
}
