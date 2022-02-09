namespace HPCL_Web.Models.Cards.SetCcmsLimit
{
    public class SearchCcmsLimitAll : BaseEntity
    {
        public string CustomerId { get; set; }
        public int Statusflag { get; set; }
    }

    public class SearchCcmsLimitAllResponse
    {
        public int ActualCCMSBalance { get; set; }
        public int UnallocatedCCMSBalance { get; set; }
    }
}
