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
            TypeUpdateDealerCreditPaymentInBulk = new List<DealerCreditPaymentDetails>();
        }
        public string CustomerID { get; set; }
        public string MerchantID{ get; set; }
        public string LimitAmount { get; set; }
        public string CreditCloseLimitTypeID { get; set; }
        public string Action { get; set; }
        public string CreditPeriod { get; set; }
        public string EffectiveDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<DealerRequestDetails> TypeUpdateDealerCreditMapping { get; set; }
        public List<DealerCreditPaymentDetails> TypeUpdateDealerCreditPaymentInBulk { get; set; }
        public string StatementDate { get; set; }
        public string SourceofPayment { get; set; }
        public string Amount { get; set; }
        public string OTP { get; set; }
    }
    public class DealerRequestDetails
    {
        public string MerchantID { get; set; }
        public string LimitAmount { get; set; }
        public string CreditCloseLimitType { get; set; }
    }
    public class DealerCreditPaymentDetails
    {
        public string MerchantID { get; set; }
        public string CustomerID { get; set; }
        public string RetailOutletName { get; set; }
        public string Outstanding { get; set; }
        public string Amount { get; set; }
    }
}
