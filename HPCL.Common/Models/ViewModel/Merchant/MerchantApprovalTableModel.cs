namespace HPCL.Common.Models.ViewModel.Merchant
{
    public class MerchantApprovalTableModel
    {
        public string MerchantId { get; set; }
        public bool Check { get; set; }
        public string ErpCode { get; set; }
        public string ZonalOfficeId { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeId { get; set; }
        public string RegionalOfficeName { get; set; }
        public string City { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedTime { get; set; }
        public string VerifiedBy { get; set; }
        public string VerifiedDate { get; set; }
        public string Comments { get; set; }
        public string MerchantTypeId { get; set; }
        public string MerchantTypeName { get; set; }
        public string RetailOutletName { get; set; }
        public string RejectedDate { get; set; }
        public string CreatedDate { get; set; }
        public string RetailOutletCity { get; set; }
    }
}
