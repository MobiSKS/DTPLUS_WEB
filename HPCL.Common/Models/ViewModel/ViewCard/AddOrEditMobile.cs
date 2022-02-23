using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.ViewCard
{
    public partial class AddOrEditMobile
    {
       public AddOrEditMobile()
        {
            mobile = new List<AddOrEditMobile>();
            
        }
        [Required(ErrorMessage = "CustomerId is Required")]
        public string CustomerId { get; set; }
        public string CardNo { get; set; }


        [StringLength(10)]
        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter a valid mobile number")]
        public  string MobileNumber { get; set; }
        public string VehicleNumber { get; set; }
  
        public String FastTagNumber { get; set; }
        public DateTime MappingDate { get; set; }

        public virtual List<AddOrEditMobile> mobile { get; set; }
    }

    public class EditResponse
    {
        
        public string CardNumber { get; set; }
        public string VechileNo { get; set; }
        public string MobileNumber { get; set; }
        public String FastTagNumber { get; set; }
        public DateTime MappingDate { get; set; }
        public string Status { get; set; }
    }
}
