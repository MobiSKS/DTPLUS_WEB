using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.JCB
{
    public class JCBCustomerAdvancedSearchRequest : BaseEntity
    {
        public string CustomerName { get; set; }
        public string OptionCustomerName { get; set; }
        public bool IsCustomerNameExist { get; set; }
        public string FormNumber { get; set; }
        public string OptionFormNumber { get; set; }
        public bool IsFormNumberExist { get; set; }
        public string Customerid { get; set; }
        public string OptionCustomerId { get; set; }
        public bool IsCustomeridExist { get; set; }
        public string CustomerType { get; set; }
        public string OptionCustomerType { get; set; }
        public bool IsCustomerTypeExist { get; set; }
        public string ZonalOfficeId { get; set; }
        public string OptionZonalOffice { get; set; }
        public bool IsZonalOfficeExist { get; set; }
        public string RegionalOfficeID { get; set; }
        public string OptionRegionalOffice { get; set; }
        public bool IsRegionalOfficeExist { get; set; }
        public string Pincode { get; set; }
        public string OptionPincode { get; set; }
        public bool IsPincodeExist { get; set; }
        public string MobileNo { get; set; }
        public bool IsMobileExist { get; set; }
    }
}