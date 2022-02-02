namespace HPCL_Web.Models.ManageCards
{
    public class UpdateMobile : BaseEntity
    {
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class UpdateMobileModal
    {
        public string CardNumber { get; set; }
        public string MobileNumber { get; set; }
        public int LimitType { get; set; }
        public string Amount { get; set; }
    }
}
