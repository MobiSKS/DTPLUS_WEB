using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class GetCustomerContactPersonRequestForApproval: CommonResponseBase
    {
        public OldAndNewContactPersonDetails Data { get; set; }
        public string CustomerId { get; set; }
        public GetCustomerContactPersonRequestForApproval()
        {
            Data = new OldAndNewContactPersonDetails();
        }
    }
    public class OldAndNewContactPersonDetails
    {
        public List<CustomerContactPersonDetails> ObjOldCustomerContactValue { get; set; }
        public List<CustomerContactPersonDetails> ObjNewCustomerContactValue { get; set; }
        public OldAndNewContactPersonDetails()
        {
            ObjOldCustomerContactValue = new List<CustomerContactPersonDetails>();
            ObjNewCustomerContactValue = new List<CustomerContactPersonDetails>();
        }
    }
    public class CustomerContactPersonDetails
    {
        public string CustomerId { get; set; }
        public string KeyOfficialTitle { get; set; }
        public string KeyOfficialFirstName { get; set; }
        public string KeyOfficialMiddleName { get; set; }
        public string KeyOfficialLastName { get; set; }
        public string KeyOfficialDesignation { get; set; }
        public string KeyOfficialPhoneNo { get; set; }
        public string KeyOfficialFax { get; set; }
        public string KeyOfficialDOA { get; set; }
        public string KeyOfficialDOB { get; set; }
        public string KeyOfficialIndividualInitials { get; set; }
        public string KeyOfficialMobile { get; set; }
        public string KeyOfficialEmail { get; set; }
    }
}