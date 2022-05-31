using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Dealer
{
    public class DealerRequestModel:BaseEntity
    {
        public DealerRequestModel()
        {
            TypeUpdateDealerCreditMapping = new List<DealerRequestDetails>();
        }
        public string CustomerID { get; set; }
        public string MerchantID{ get; set; }
        public string LimitAmount { get; set; }
        public string CreditCloseLimitTypeID { get; set; }
        public string Action { get; set; }
        public string CreditPeriod { get; set; }
        public string EffectiveDate { get; set; }
        public List<DealerRequestDetails> TypeUpdateDealerCreditMapping { get; set; }
    }
    public class DealerRequestDetails
    {
        public string MerchantID { get; set; }
        public string LimitAmount { get; set; }
        public string CreditCloseLimitType { get; set; }
    }
}
