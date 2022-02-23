using HPCL_Web.Models.CommonEntity;
using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.ViewModel.Cards
{
    public class SetIndividualLimit : BaseEntity
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string VehicleNo { get; set; }
    }
}
