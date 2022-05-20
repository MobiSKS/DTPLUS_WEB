using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Configure
{
    public class SMSAlertstoIndividualCardUsersModel : CommonResponseBase
    {
        public string CustomerID { get; set; }
        public string CardNo { get; set; }
        public string NameOnCard { get; set; }
        public string IndividualOrgName { get; set; }
        public SMSAlertDetails Data { get; set; }
        public SMSAlertstoIndividualCardUsersModel()
        {
            Data = new SMSAlertDetails();
        }
    }
    public class SMSAlertDetails
    {
        public List<CustomerDetails> CustomerDetails { get; set; }
        public List<CustCardDetails> CardDetails { get; set; }
        public SMSAlertDetails()
        {
            CustomerDetails = new List<CustomerDetails>();
            CardDetails = new List<CustCardDetails>();
        }
    }
    public class CustomerDetails
    {
        public string IndividualOrgName { get; set; }
        public string NameOnCard { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
    public class CustCardDetails
    {
        public string cardNo { get; set; }
        public string VechileNo { get; set; }
        public string Mobileno { get; set; }
    }
}