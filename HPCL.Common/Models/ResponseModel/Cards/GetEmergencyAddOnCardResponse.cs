using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class GetEmergencyAddOnCardResponse : ResponseMsg
    {
        public List<GetEmergencyAddOnCardResponseData> data { get; set; }
    }

    public class GetEmergencyAddOnCardResponseData
    {
        public string CardNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
