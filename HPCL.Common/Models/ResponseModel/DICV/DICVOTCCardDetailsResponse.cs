using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.DICV
{
    public class DICVOTCCardDetailsResponse : CommonResponseBase
    {
        public List<DICVOTCCardDetails> Data { get; set; }
    }

    public class DICVOTCCardDetails
    {
        public string CardNo { get; set; }
    }
}
