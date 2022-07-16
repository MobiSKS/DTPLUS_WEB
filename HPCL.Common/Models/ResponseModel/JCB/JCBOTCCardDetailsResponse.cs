using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.JCB
{
    public class JCBOTCCardDetailsResponse : CommonResponseBase
    {
        public List<JCBOTCCardDetails> Data { get; set; }
    }

    public class JCBOTCCardDetails
    {
        public string CardNo { get; set; }
    }

}