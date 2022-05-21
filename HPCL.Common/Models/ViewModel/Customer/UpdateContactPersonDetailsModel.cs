using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Customer
{
    public class UpdateContactPersonDetailsModel : BaseEntity
    {
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        public string KeyOffTitle { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string KeyOffFirstName { get; set; }

        public string KeyOffIndividualInitials { get; set; }
        public string KeyOffLastName { get; set; }
        public string KeyOffMiddleName { get; set; }
        [Required(ErrorMessage = "Designation is Required")]
        public string KeyOffDesignation { get; set; }
        public string KeyOffFaxCode { get; set; }
        public string KeyOffFax { get; set; }

        public string KeyOffDialCode { get; set; }

        public string KeyOffPhoneCode { get; set; }

        //[Required(ErrorMessage = "Phone Number is Required under communication Address")]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string KeyOffPhoneNumber { get; set; }

        [Required(ErrorMessage = "Mobile Phone number is Required under communication Address")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string KeyOffMobileNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string KeyOffEmail { get; set; }
        public string KeyOffDateOfAnniversary { get; set; }
        public string KeyOfficialDOB { get; set; }
        public string Remarks { get; set; }
    }
}