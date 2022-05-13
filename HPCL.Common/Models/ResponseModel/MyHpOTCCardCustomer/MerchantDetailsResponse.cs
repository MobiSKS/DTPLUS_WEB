using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer
{

    public class MerchantResponseOTCCardCustomer : CommonResponseBase
    {
        public List<MerchantDetailsResponseOTCCardCustomer> Data {get; set; }
    }

    public class MerchantDetailsResponseOTCCardCustomer : CommonResponseBase
    {
        public string RetailOutletName { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string SalesAreaName { get; set; }
        public int RegionalOfficeId { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
