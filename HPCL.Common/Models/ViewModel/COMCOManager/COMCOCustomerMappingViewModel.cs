using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.COMCOManager
{
    public class COMCOCustomerMappingViewModel : CommonResponseBase
    {
        public string CustomerID { get; set; }
        public string MerchantID { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string RetailOutletName { get; set; }
        public GetCOMCOMapCustomerDetails Data { get; set; }
        public COMCOCustomerMappingViewModel()
        {
            Data = new GetCOMCOMapCustomerDetails();
        }
    }
    public class GetCOMCOMapCustomerDetails
    {
        public List<COMCOCustomerDetails> CustomerDetails { get; set; }
        public List<COCOMerchantDetails> MerchantDetails { get; set; }
        public GetCOMCOMapCustomerDetails()
        {
            CustomerDetails = new List<COMCOCustomerDetails>();
            MerchantDetails = new List<COCOMerchantDetails>();
        }
    }
    public class COCOMerchantDetails
    {
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string RetailOutletName { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
    public class COMCOCustomerDetails
    {
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string CustomerID { get; set; }
        public string IndividualOrgName { get; set; }
        public string NameOnCard { get; set; }
        public string CustomerAddress { get; set; }
        public string CommunicationPincode { get; set; }
    }
}