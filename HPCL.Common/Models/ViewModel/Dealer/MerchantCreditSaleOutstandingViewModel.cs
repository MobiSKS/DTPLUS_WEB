using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Dealer
{
    public class MerchantCreditSaleOutstandingViewModel:CommonResponseBase
    {
        public MerchantCreditSaleOutstandingViewModel()
        {
            Data=new MerchantCreditSaleOutstandingData();
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
       
        public string MerchantID { get; set; }
        public string CustomerID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public MerchantCreditSaleOutstandingData Data { get; set; }

    }
    public class MerchantCreditSaleOutstandingData
    {
        public MerchantCreditSaleOutstandingData()
        {
            MerchantDetails = new List<MerchantDetails>();
            MerchantCustomerMappedDetails = new List<MerchantCustomerMappedDetails>();
        }
        public List<MerchantDetails> MerchantDetails { get; set; }

        public List<MerchantCustomerMappedDetails> MerchantCustomerMappedDetails { get; set; }
    }
    public class MerchantDetails
    {

        public string MerchantId { get; set; }
        public string RetailOutletName { get; set;  }
        public string RegionalOfficeName { get; set; }
        public string ZonalOfficeName { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

    }
    public class MerchantCustomerMappedDetails
    {
        public string CustomerId { get; set; }
        public string IndividualOrgName { get; set; }
        public string Outstanding { get; set; }
        public string LimitBalance { get; set; }
        public string CCMSBalanceStatus { get; set; }
        public string CreditCloseLimit { get; set; }

    }
    
}
