using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Merchant
{
    public class MerchantRejectedModel : CommonResponseBase
    {
        public MerchantRejectedModel()
        {
            MerchantApprovalTableDetails = new List<MerchantApprovalTableModel>();
       
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public string CategoryID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ShowTable { get; set; }
        public string Message { get; set; }
        public virtual List<MerchantApprovalTableModel> MerchantApprovalTableDetails { get; set; }
    }
  
}
