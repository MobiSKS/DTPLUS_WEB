using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.CustomerDashboard
{
    public class LastFiveTransactionsResponseModel
    {
        public string AccountNo { get; set; }
        public string VehicleNo_UserName { get; set; }
        public string TxnDate { get; set; }
        public string TxnType { get; set; }
        public string Amount { get; set; }
     
    }
}
