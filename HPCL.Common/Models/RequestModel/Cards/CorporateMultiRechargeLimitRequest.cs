using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Cards
{
    public  class CorporateMultiRechargeLimitRequest:BaseEntity
    {
        public string CustomerID { get; set; }
        public CorporateMultiRechargeLimitRequest()
        {
            ObjLimitConfig = new List<LimitRequestDetails>();
        }
        public List<LimitRequestDetails> ObjLimitConfig { get; set; }
    }
    public class LimitRequestDetails
    {
        public string CustomerID { get; set; }
        public int Limittype { get; set; }
        public int StatusFlag { get; set; }
    }
}
