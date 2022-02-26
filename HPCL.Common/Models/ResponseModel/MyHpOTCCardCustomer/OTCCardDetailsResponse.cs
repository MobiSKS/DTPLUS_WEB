using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer
{
    public class OTCCardDetailsResponse: CommonResponseBase
    {
        public List<CardDetails> Data { get; set; }
    }

    public class CardDetails
    {
        public string CardNo { get; set; }
    }

}
