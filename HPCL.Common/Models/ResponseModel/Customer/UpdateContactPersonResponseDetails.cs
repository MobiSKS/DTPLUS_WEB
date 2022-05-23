using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class UpdateContactPersonResponseDetails
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string Ph_Office { get; set; }
        public string Fax { get; set; }
        public string DateofAnniversary { get; set; }
        public string IndividualInitial { get; set; }
        public string DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
        public string KeyOffFaxCode { get; set; }
        public string KeyOffFaxPart2 { get; set; }
        public string KeyOffPhoneCode { get; set; }
        public string KeyOffPhonePart2 { get; set; }
    }
}
