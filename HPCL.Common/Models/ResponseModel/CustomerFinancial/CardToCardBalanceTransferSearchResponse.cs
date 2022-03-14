using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerFinancial
{
    public class CardToCardBalanceTransferSearchResponse : ResponseMsg
    {
        public List<CardToCardResponse> data { get; set; }
    }

    public class CardToCardResponse
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string CustomerID { get; set; }
        public string VechileNo { get; set; }
        public string Mobileno { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public decimal CardBalance { get; set; }
    }
}