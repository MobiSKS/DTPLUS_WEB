using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer
{
    public class OTCUnAllocatedCard : CommonResponseBase
    {
        public OTCUnAllocatedCardsResponse Data { get; set; }
    }
    public class OTCUnAllocatedCardsResponse
    {
        public List<NoOfUnAllocatedCard> ObjNoOfUnAllocatedCard { get; set; }
        public List<UnAllocatedCard> ObjUnAllocatedCard { get; set; }
    }

    public class NoOfUnAllocatedCard
    {
        public int NoOfUnAllocatedCards { get; set; }
    }

    public class UnAllocatedCard
    {
        public String CardNo { get; set; }
    }
}