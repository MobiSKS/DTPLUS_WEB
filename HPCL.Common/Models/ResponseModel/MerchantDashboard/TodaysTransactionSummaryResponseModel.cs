using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.CommonResponse;

namespace HPCL.Common.Models.ResponseModel.MerchantDashboard
{
    public class TodaysTransactionSummaryResponseModel : ResponseMsg
    {
        public string CardSale { get; set; }

        public string CCMSSale { get; set; }

        public string CashReload { get; set; }

        public string CCMSRecharge { get; set; }

        public string Status { get; set; }

        public string Reason { get; set; }


    }
}
