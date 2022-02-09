namespace HPCL_Web.Models.Cards.SetCcmsLimit
{
    public class UpdateCcmsIndLimit : BaseEntity
    {
        public ObjCCMSLimits[] ObjCCMSLimits { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class ObjCCMSLimits
    {
        public string Cardno { get; set; }
        public string Mobileno { get; set; }
        public int Limittype { get; set; }
        public int Amount { get; set; }
    }

    public class ViewGird
    {
        public string Cardno { get; set; }
        public string vehicleNo { get; set; }
        public string issueDate { get; set; }
        public string expiryDate { get; set; }
        public string status { get; set; }
        public string Mobileno { get; set; }
        public int Limittype { get; set; }
        public string limitTypeText { get; set; }
        public int Amount { get; set; }
    }
}
