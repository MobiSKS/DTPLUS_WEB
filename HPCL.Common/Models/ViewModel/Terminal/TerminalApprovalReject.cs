using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Terminal
{
    public class TerminalApprovalReject : BaseEntity
    {
        public string Remark { get; set; }
        public string Action { get; set; }
        public ObjMerchantTerminalInsertInput[] ObjMerchantTerminalInsertInput { get; set; }
    }

    public class ObjMerchantTerminalInsertInput
    {
        public string MerchantId { get; set; }
        public string TerminalID { get; set; }
    }
}
