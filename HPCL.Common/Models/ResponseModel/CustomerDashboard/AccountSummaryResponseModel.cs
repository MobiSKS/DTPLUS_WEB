using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.CustomerDashboard
{
    public class AccountSummaryResponseModel : ResponseMsg
    {
        public string CCMS { get; set; }
        public string CardCash { get; set; }
        public string Drivestars { get; set; }
        public string LastUpdateForCCMS { get; set; }
        public string LastUpdateForCardCash { get; set; }
        public string LastUpdateForDrivestars { get; set; }
      

    }
}
