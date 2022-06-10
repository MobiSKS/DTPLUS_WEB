using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class GetCustomerAddressRequestForApproval : CommonResponseBase
    {
        public OldAddressAndNewAddress Data { get; set; }
        public string CustomerId { get; set; }
        public GetCustomerAddressRequestForApproval()
        {
            Data = new OldAddressAndNewAddress();
        }
    }
    public class OldAddressAndNewAddress
    {
        public List<CustomerAddressDetails> ObjOldCustomerAddressValue { get; set; }
        public List<CustomerAddressDetails> ObjNewCustomerAddressValue { get; set; }
        public OldAddressAndNewAddress()
        {
            ObjOldCustomerAddressValue = new List<CustomerAddressDetails>();
            ObjNewCustomerAddressValue = new List<CustomerAddressDetails>();
        }
    }
    public class CustomerAddressDetails
    {
        public string CustomerId { get; set; }
        public string CommunicationAddress1 { get; set; }
        public string CommunicationAddress2 { get; set; }
        public string CommunicationAddress3 { get; set; }
        public string CommunicationLocation { get; set; }
        public string CommunicationCityName { get; set; }
        public string CommunicationPincode { get; set; }
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationMobileNo { get; set; }
        public string CommunicationFax { get; set; }
        public string CommunicationEmailid { get; set; }
        public string PermanentAddress1 { get; set; }
        public string PermanentAddress2 { get; set; }
        public string PermanentAddress3 { get; set; }
        public string PermanentLocation { get; set; }
        public string PermanentCityName { get; set; }
        public string PermanentPincode { get; set; }
        public string PermanentPhoneNo { get; set; }
        public string PermanentFax { get; set; }
        public string PanCardRemarks { get; set; }
    }
}