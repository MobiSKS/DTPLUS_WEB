using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerRequest
{
    public class GetCardRenwalRequestListRes : ResponseMsg
    {
        public List<GetCardRenwalRequestListResData> data { get; set; }
    }

    public class GetCardRenwalRequestListResData
    {
        public string CustomerId { get; set; }
        public string IndividualOrgName { get; set; }
        public string CardNo { get; set; }
        public string ExpiryDate { get; set; }
        public string VechileNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
