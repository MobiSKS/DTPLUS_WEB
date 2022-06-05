﻿using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.HDFCBankCreditPouch
{
    public class CustomerDetailsRes : ResponseMsg
    {
        public List<CustomerDetailsResData> data { get; set; }
    }

    public class CustomerDetailsResData
    {
        public string CustomerName { get; set; }
        public string NameOnCard { get; set; }
        public string LastTransactionDate { get; set; }
        public string TotalSpend { get; set; }
        public string RO { get; set; }
        public string ZO { get; set; }
    }
}