using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.CommonResponse;

namespace HPCL.Common.Models.ResponseModel.MerchantDashboard
{
    public class KeyEventAndFigureResponseModel : ResponseMsg
    {
        public string LastLogin { get; set; }
        public string LastTransaction { get; set; }
        public string LastCashReload { get; set; }
        public string LastCashSale { get; set; }
        public string LastCCMSRecharge { get; set; }
        public string LastBatchSettled { get; set; }
        public string UnsettledBatchNumber { get; set; }

        public string UnsettledTxnCount { get; set; }
        public string LastPasswordChange { get; set; }
        public string LastContactDetailsUpdate { get; set; }

    }
}
