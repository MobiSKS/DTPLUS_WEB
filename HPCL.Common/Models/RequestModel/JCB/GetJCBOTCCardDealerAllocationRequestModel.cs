using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.JCB
{
    public class GetJCBOTCCardDealerAllocationRequestModel : BaseEntity
    {
        public string DealerCode { get; set; }
        public string CardNo { get; set; }
    }
}