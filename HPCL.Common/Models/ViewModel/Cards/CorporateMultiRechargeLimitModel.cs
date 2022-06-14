using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class CorporateMultiRechargeLimitModel:CommonResponseBase
    {
        public string CustomerID { get; set; }
        public CorporateMultiRechargeLimitModel()
        {
            Data = new List<CorporateMultiRechargeLimitDetails>();
        }
        public List<CorporateMultiRechargeLimitDetails> Data { get; set; }
        public List<LimitRequestDetails> ObjLimitConfig { get; set; }
    }
    public class CorporateMultiRechargeLimitDetails
    {
        public string LimitId { get; set; }
        public string TypeOfLimit { get; set; }
        public string CheckUncheckId { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
   
}
