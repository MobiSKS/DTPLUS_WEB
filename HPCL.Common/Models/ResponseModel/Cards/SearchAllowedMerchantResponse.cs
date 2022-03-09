using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class SearchAllowedMerchantResponse : ResponseMsg
    {
      public List<AllowedMerchant> data { get; set; }
    }

    public class AllowedMerchant
    {
        public string CardNo { get; set; }
        public string MerchantID { get; set; }
        public string CustomerID { get; set; }
        public string MerchantName { get; set; }
        public string Status { get; set; }
    }
}
