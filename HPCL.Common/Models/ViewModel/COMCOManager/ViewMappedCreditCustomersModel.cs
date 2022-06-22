using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.COMCOManager
{
    public class ViewMappedCreditCustomersModel : CommonResponseBase
    {
        public string CustomerID { get; set; }
        public string MerchantID { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string RetailOutletName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public GetCOMCOViewMappedCustomer Data { get; set; }
        public ViewMappedCreditCustomersModel()
        {
            Data = new GetCOMCOViewMappedCustomer();

            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
    }
    public class GetCOMCOViewMappedCustomer
    {
        public List<MappedCustomerDetails> MappedDetails { get; set; }
        public List<COCOMerchantDetails> MerchantDetails { get; set; }
        public GetCOMCOViewMappedCustomer()
        {
            MappedDetails = new List<MappedCustomerDetails>();
            MerchantDetails = new List<COCOMerchantDetails>();
        }
    }
    public class MappedCustomerDetails
    {
        public string CustomerID { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string IndividualOrgName { get; set; }
        public string NameOnCard { get; set; }
        public string MappingDate { get; set; }
        public string MappingBy { get; set; }
    }
}