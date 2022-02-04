using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.Cards.ActivateReActivate
{
    public class SearchCards : BaseEntity
    {
        [Required(ErrorMessage = "Customer Id is required")]
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
    }

    public class SearchCardsResponse
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string VechileNo { get; set; }
        public string MobileNumber { get; set; }
        public int CardStatus { get; set; }
        public string Status { get; set; }
    }

    public class UpdateStatus : BaseEntity
    {
        public string CardNo { get; set; }
        public int Statusflag { get; set; }
        public string ModifiedBy { get; set; }
    }
}
