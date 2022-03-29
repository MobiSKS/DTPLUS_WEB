using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Interface
{
    public class GetCustomerandCardFormResponse:CommonResponseBase
    {
        public GetCustomerandCardFormResponse()
        {
            CustomerFormDetails = new List<GetCustomerFormDetails>();
            CardFormDetails = new List<GetCardFormDetails>();
        }
        public virtual List<GetCustomerFormDetails> CustomerFormDetails { get; set; }
        public virtual List<GetCardFormDetails> CardFormDetails { get; set; }
    }
    public class GetCustomerFormDetails
    {
        public string CustomerReferenceNo { get; set; }
        public string NameOnCard { get; set; }
        public string CustomerName { get; set; }
        public string RegionalOffice { get; set; }
        public string FormNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CustomerStatus { get; set; }
        public string CardStatus { get; set; }
    }
    public class GetCardFormDetails
    {
        public string CustomerReferenceNo { get; set; }
        public string VechileNo { get; set; }
        public string YearOfRegistration { get; set; }
        public string Manufacturer { get; set; }
        public string CustomerID { get; set; }
        public string VehicleType { get; set; }
        public string FormNumber { get; set; }
        public string Status { get; set; }
    }
}