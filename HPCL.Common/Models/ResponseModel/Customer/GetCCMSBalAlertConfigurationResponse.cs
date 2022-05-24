using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class GetCCMSBalAlertConfigurationResponse: CommonResponseBase
    {
        public CCMSBalAlertConfiguration Data { get; set; }
        public GetCCMSBalAlertConfigurationResponse()
        {
            Data = new CCMSBalAlertConfiguration();
        }
    }
    public class CCMSBalAlertConfiguration
    {
        public CCMSBalAlertConfiguration()
        {
            CCMSCustomerDetail = new List<CCMSCustomerDetails>();
            CCMSAmountDetail = new List<CCMSAmountDetails>();
        }
        public List<CCMSCustomerDetails> CCMSCustomerDetail { get; set; }
        public List<CCMSAmountDetails> CCMSAmountDetail { get; set; }
    }
    public class CCMSCustomerDetails
    {
        public string IndividualOrgName { get; set; }
        public string NameOnCard { get; set; }
    }
    public class CCMSAmountDetails
    {
        public string Amount { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
