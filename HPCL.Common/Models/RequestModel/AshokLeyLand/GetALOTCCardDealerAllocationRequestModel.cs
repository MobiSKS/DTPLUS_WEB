using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.AshokLeyLand
{
    public class GetALOTCCardDealerAllocationRequestModel : BaseEntity
    {
        public string DealerCode { get; set; }
        public string CardNo { get; set; }
    }
}
