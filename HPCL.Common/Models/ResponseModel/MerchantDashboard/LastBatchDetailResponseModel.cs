using System;
using System.Collections.Generic;
using HPCL.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.CommonResponse;

namespace HPCL.Common.Models.ResponseModel.MerchantDashboard
{
    public class LastBatchDetailResponseModel : ResponseMsg
    {
        public string TerminalId { get; set; }

        public string BatchID { get; set; }

        public string SettlementDate { get; set; }

        public string Receivable_Rs_ { get; set; }

        public string Payable { get; set; }

        public string Status { get; set; }

        public string Reason { get; set; }


    }
}
