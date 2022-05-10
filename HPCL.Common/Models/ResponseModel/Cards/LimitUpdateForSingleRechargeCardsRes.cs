using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class LimitUpdateForSingleRechargeCardsRes : ResponseMsg
    {
        public List<LimitUpdateForSingleRechargeCardsResData> data { get; set; }
    }

    public class LimitUpdateForSingleRechargeCardsResData
    {
        public string CardNumber { get; set; }
        public string VechileNo { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string CardStatus { get; set; }
        public int LimitId { get; set; }
        public string TypeOfLimit { get; set; }
        public Decimal CCMSLimitValue { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
