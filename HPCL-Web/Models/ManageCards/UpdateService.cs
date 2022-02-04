namespace HPCL_Web.Models.ManageCards
{
    public class UpdateService : BaseEntity
    {
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public int ServiceId { get; set; }
        public int Flag { get; set; }
        public string CreatedBy { get; set; }
    }
}
