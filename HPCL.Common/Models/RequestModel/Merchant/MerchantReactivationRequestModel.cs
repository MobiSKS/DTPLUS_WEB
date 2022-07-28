using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Merchant
{
    public class MerchantReactivationRequestModel:BaseEntity
    {
        public string MerchantID { get; set; }
        public string MerchantZO { get; set; }
        public string MerchantRO { get; set; }
        public string MerchantStatus { get; set; }
        public string HotlistDate { get; set; }
        public string SBUType { get; set; }


    }

}
