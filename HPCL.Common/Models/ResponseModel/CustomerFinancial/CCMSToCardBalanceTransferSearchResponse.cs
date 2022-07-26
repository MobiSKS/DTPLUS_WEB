using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerFinancial
{
    public class CCMSToCardBalanceTransferSearchResponse : ResponseMsg
    {
        public DataObject data { get; set; }
    }

    public class DataObject
    { 
        public List<AvailableCcmsBalanceModelOutput> availableCcmsBalanceModelOutput { get; set; }
        public List<GetCcmsToCardBalanceTransferDetailModelOutput> getCcmsToCardBalanceTransferDetailModelOutput { get; set; }
    }

    public class AvailableCcmsBalanceModelOutput
    { 
        public Decimal AvailableCcmsBalance { get; set; }
    }

    public class GetCcmsToCardBalanceTransferDetailModelOutput
    {
        public int SrNumber { get; set; }
        public string CardNo { get; set; }
        public string VechileNo { get; set; }
        public string Mobileno { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public Decimal Balance { get; set; }
        public Decimal CashPurseLimit { get; set; }
        public string CCMSLimit { get; set; }
        public Decimal CCMSLimitValue { get; set; }
        public string CardStatus { get; set; }
        public string Reason { get; set; }
    }
}
