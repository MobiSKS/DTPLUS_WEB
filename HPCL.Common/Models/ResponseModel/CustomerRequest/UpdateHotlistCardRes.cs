using HPCL.Common.Models.ResponseModel.CommonResponse;

namespace HPCL.Common.Models.ResponseModel.CustomerRequest
{
    public class UpdateHotlistCardRes
    {
        public string CardNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
