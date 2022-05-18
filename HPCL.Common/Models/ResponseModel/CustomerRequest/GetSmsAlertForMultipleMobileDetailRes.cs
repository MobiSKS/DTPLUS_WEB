using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerRequest
{
    public class GetSmsAlertForMultipleMobileDetailRes : ResponseMsg
    {
        public List<CustomerMultipleMobileDetail> CustomerMultipleMobileDetail { get; set; }
        public List<CustomerDetail> CustomerDetail { get; set; }
    }

    public class CustomerMultipleMobileDetail
    { 
        public string CustomerID { get; set; }
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string StatusFlag { get; set; }
    }

    public class CustomerDetail
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string NameOnCard { get; set; }
        public string CommunicationMobileNo { get; set; }
    }
}
