using HPCL.Common.Models.CommonEntity;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.ViewCard
{
    public class ViewCardDetails : BaseEntity
    {
        [Required(ErrorMessage = "CustomerId is Required")]
        public string Customerid { get; set; }

        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string VechileNo { get; set; }
        public string FastagNo { get; set; }

        public string ModifiedBy { get; set; }





    }
}