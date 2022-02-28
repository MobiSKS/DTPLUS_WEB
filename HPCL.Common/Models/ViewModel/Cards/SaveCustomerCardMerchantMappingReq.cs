using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class SaveCustomerCardMerchantMappingReq : BaseEntity
    {
        public string CustomerID { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public ObjCardMerchantMap[] ObjCardMerchantMap { get; set; }
    }

    public class ObjCardMerchantMap
    {
        public string CardNo { get; set; }
        public string MerchantId { get; set; }
    }
}
