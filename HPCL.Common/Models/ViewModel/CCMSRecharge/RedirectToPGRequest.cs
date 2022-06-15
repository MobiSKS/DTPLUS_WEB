using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.CCMSRecharge
{
    public class RedirectToPGRequest : BaseEntity
    {
        public string MobileNo { get; set; }
        public Decimal Amount { get; set; }
        public string orderId { get; set; }
        public string controlCardNo { get; set; }
        public string customerId { get; set; }
    }
}
