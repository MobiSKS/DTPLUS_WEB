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
        public string CustomerID { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        public string KeyOfficialTitle { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string KeyOfficialFirstName { get; set; }

        public string KeyOfficialIndividualInitial { get; set; }
        public string KeyOfficialLastName { get; set; }
        public string KeyOfficialMiddleName { get; set; }
        [Required(ErrorMessage = "Designation is Required")]
        public string KeyOfficialDesignation { get; set; }
        public string KeyOffFaxCode { get; set; }
        public string KeyOffFaxPart2 { get; set; }
        public string KeyOfficialFax { get; set; }        
        public string KeyOffPhoneCode { get; set; }
        public string KeyOffPhonePart2 { get; set; }

        //[Required(ErrorMessage = "Phone Number is Required under communication Address")]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string KeyOfficialPhoneNo { get; set; }

        [Required(ErrorMessage = "Mobile Phone number is Required")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string KeyOfficialMobile { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string KeyOfficialEmail { get; set; }
        public string KeyOfficialDOA { get; set; }
        public string KeyDOA { get; set; }
        public string KeyOfficialDOB { get; set; }
        public string KeyDOB { get; set; }
        public string Remarks { get; set; }
        public int Internel_Status_Code { get; set; }
        public int Status { get; set; }
    }
}