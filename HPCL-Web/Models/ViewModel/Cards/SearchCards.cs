using HPCL_Web.Models.CommonEntity;
using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.ViewModel.Cards
{
    public class SearchCards : BaseEntity
    {
        [Required(ErrorMessage = "Customer Id is required")]
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
    }

    public class UpdateStatus : BaseEntity
    {
        public string CardNo { get; set; }
        public int Statusflag { get; set; }
        public string ModifiedBy { get; set; }
    }
}
