using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer
{
    public class OTCCardDetailsResponse: CommonResponseBase
    {
        public List<OTCCardDetails> Data { get; set; }
    }

    public class OTCCardDetails
    {
        public string CardNo { get; set; }
    }

}
