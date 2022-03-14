using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CustomerCCMSBalanceModel:CommonResponseBase
    {
        public CustomerCCMSBalanceModel()
        {
            Data = new List<CustomerCCMSBalanceDetails>();
        }
        public List<CustomerCCMSBalanceDetails> Data { get; set; }
    }
    public class CustomerCCMSBalanceDetails
    {
        public string Mode { get; set; }
        public string Description { get; set; }
        public string TransactionDate { get; set; }
        public string OpeningBalance { get; set; }
        public string PostingMethod { get; set; }
        public string Amount { get; set; }
        public string ClosingBalance { get; set; }
    }

}
