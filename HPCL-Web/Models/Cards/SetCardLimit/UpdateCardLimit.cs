namespace HPCL_Web.Models.Cards.SetCardLimit
{
    public class UpdateCardLimit : BaseEntity
    {
        public string Cardno { get; set; }
        public int Cashpurse { get; set; }
        public int Saletxn { get; set; }
        public int Dailysale { get; set; }
        public int Monthlysale { get; set; }
        public string ModifiedBy { get; set; }
    }
}
