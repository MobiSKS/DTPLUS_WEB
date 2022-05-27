using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Dealer
{
    public class DealerCreditMappingViewModel:CommonResponseBase
    {
        public string CustomerID { get; set; }  
        public GetDealerCreditMappingDetails Data { get; set; }
        
    }
    public class GetDealerCreditMappingDetails
    {
        public List<CustomerCCMSBalanceDetails> CustomerCCMSBalanceDetails { get; set; }
        public List<CustomerDetails> CustomerDetails { get; set; }
        public List<CustomerMerchantMappedDetails> CustomerMerchantMappedDetails { get; set; }
    }
    public class CustomerCCMSBalanceDetails
    {
        public string CCMSBalance { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
    public class CustomerDetails
    {
        public string IndividualOrgName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string ZonalOfficeName { get; set; }
        public string CustomerAddress { get; set; }
    }
    public class CustomerMerchantMappedDetails
    {
        public string MerchantId { get; set; }
        public string OutletNameAndLocation { get; set; }
        public string CreditCloseLimitType { get; set; }
        public string CreditCloseLimit { get; set; }
        public string Outstanding { get; set; }
        public string CreditCloseLimitBalance { get; set; }
        public string Status { get; set; }
    }
}
