using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Security
{
    public class BindGridResponse : ResponseMsg
    {
        public List<BindGridResponseData> data { get; set; }
    }

    public class BindGridResponseData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string RBEId { get; set; }
        public string EmailId { get; set; }
        public string MakerComment { get; set; }
        public string RequestOn { get; set; }
        public string Requestedby { get; set; }
        public string OfficerStatusId { get; set; }
        public string OfficerStatusName { get; set; }
    }

}
