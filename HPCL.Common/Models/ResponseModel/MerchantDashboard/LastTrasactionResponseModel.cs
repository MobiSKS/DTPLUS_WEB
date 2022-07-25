using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.MerchantDashboard
{
    public class LastTrasactionResponseModel : ResponseMsg
    {

        public string TerminalId { get; set; }

        public string AccountNo { get; set; }

        public string VechileNo { get; set; }

        public string TxnDate { get; set; }

        public string TxnType { get; set; }

        public string Amount { get; set; }

       




    }
}
