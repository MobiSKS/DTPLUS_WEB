using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.JCB
{
    public class JCBCustomerAdvancedSearchModel
    {
        public JCBCustomerAdvancedSearchModel()
        {
            lstCustomerType = new List<CustomerTypeModel>();
            CustomerZonalOfficeMdl = new List<CustomerZonalOfficeModel>();
            CustomerZonalOfficeMdl.Add(new CustomerZonalOfficeModel
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--Select--"
            });
            CustomerRegionMdl = new List<CustomerRegionModel>();
            CustomerRegionMdl.Add(new CustomerRegionModel
            {
                RegionalOfficeID = 0,
                RegionalOfficeName = "--Select--"
            });
        }
        public string Remarks { get; set; }
        public string CustomerName { get; set; }
        public string IsCustomerNameExist { get; set; }
        public string FormNumber { get; set; }
        public string IsFormNumberExist { get; set; }
        public string CustomerId { get; set; }
        public string IsCustomeridExist { get; set; }
        public int CustomerType { get; set; }
        public string IsCustomerTypeExist { get; set; }
        public int RegionalOfficeID { get; set; }
        public string IsRegionalOfficeExist { get; set; }
        public int ZonalOfficeId { get; set; }
        public string IsZonalOfficeExist { get; set; }
        public string Pincode { get; set; }
        public string IsPincodeExist { get; set; }
        public string MobileNo { get; set; }
        public string IsMobileExist { get; set; }
        public string OptionCustomerName { get; set; }
        public string OptionFormNumber { get; set; }
        public string OptionCustomerId { get; set; }
        public string OptionCustomerType { get; set; }
        public string OptionZonalOffice { get; set; }
        public string OptionRegionalOffice { get; set; }
        public string OptionPincode { get; set; }
        public string OptionResult { get; set; }
        public virtual List<CustomerTypeModel> lstCustomerType { get; set; }
        public virtual List<CustomerZonalOfficeModel> CustomerZonalOfficeMdl { get; set; }
        public virtual List<CustomerRegionModel> CustomerRegionMdl { get; set; }

    }
}
