using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.VolvoEicher
{
    public class GetVEDealerCardDispatchResponse : CommonResponseBase
    {
        public List<GetVEDealerCardDispatchData> Data { get; set; }
        public GetVEDealerCardDispatchResponse()
        {
            Data = new List<GetVEDealerCardDispatchData>();
        }
    }
    public class GetVEDealerCardDispatchData
    {
        public string CardNo { get; set; }
        public string DispatchDate { get; set; }
        public int CourierName { get; set; }
        public string AirwaysBillNo { get; set; }
        public string DispatchStatus { get; set; }
        public string DeliveredTo { get; set; }
        public string DeliveryDate { get; set; }
        public string ReturnReason { get; set; }
        public string MOComments { get; set; }
    }
}