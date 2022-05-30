using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CustomerRequest
{
    public class GetHotlistCardsPermanently : BaseEntity
    {
        public string CustomerID { get; set; }
        public string CardNo { get; set; }
    }
}
