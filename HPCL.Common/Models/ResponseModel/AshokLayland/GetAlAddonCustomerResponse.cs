using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.AshokLayland
{
    public class GetAlAddonCustomerResponse : CommonResponseBase
    {
        public GetAlAddonOTCCardMappingCustomerDetails Data { get; set; }
    }
    public class GetAlAddonOTCCardMappingCustomerDetails
    {
        public List<GetCustomerDetails> GetCustomerNameAndNameOnCard { get; set; }
        public List<GetStatusDetails> GetStatus { get; set; }
    }
    public class GetCustomerDetails
    {
        public string CustomerOrgName { get; set; }
        public string NameOnCard { get; set; }
        public string Reason { get; set; }
    }
    public class GetStatusDetails
    {
        public string CustomerID { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
    }
}