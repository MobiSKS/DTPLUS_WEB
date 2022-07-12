using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerSearch
{
    public class ControlCardPinResetRes : ResponseMsg
    {
        public List<ControlCardPinResetResData> data { get; set; }
    }

    public class ControlCardPinResetResData
    {
        public string MobileNo { get; set; }
        public string CIN { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
