using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Dealer
{
    public class GenerateOTPCreditClosePaymentModel:CommonResponseBase
    {
        public List<OTPDetails> Data { get; set; }
    }
    public class OTPDetails
    {
        public string OTP { get; set; }
        public string CTID { get; set; }

        public string Mobileno { get; set; }

        public string OutletName { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

    }
}
