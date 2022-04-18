using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class GetCardLimitResponse : ResponseMsg
    {
       public List<LimitResponse> data { get; set; }
    }

    public class LimitResponse
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public int YearOfRegistration { get; set; }
        public string MobileNumber { get; set; }
        public string VehicleMake { get; set; }
        public Decimal CashPurseLimit { get; set; }
        public Decimal SaleTxnLimit { get; set; }
        public Decimal DailySaleLimit { get; set; }
        public Decimal MonthlySaleLimit { get; set; }
    }
}
