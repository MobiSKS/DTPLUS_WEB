using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.MerchantFinancials
{
    public class MerchantERPReloadSaleEarningModel
    {
        public string MerchantId { get; set; }
        public string TerminalOrMerchant { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<MerchantERPReloadSaleEarningDetails> SaleEarningDetails { get; set; }
        public string Message { get; set; }
        public MerchantERPReloadSaleEarningModel()
        {
            SaleEarningDetails = new List<MerchantERPReloadSaleEarningDetails>();
        }
    }

    public class MerchantERPReloadSaleEarningDetails
    {
        public string TerminalID { get; set; }
        public string BatchID { get; set; }
        public string SettlementDate { get; set; }
        public string Sale { get; set; }
        public string Reload { get; set; }
        public string Earning { get; set; }
    }
}