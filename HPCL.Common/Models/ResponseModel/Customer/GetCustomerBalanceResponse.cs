using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class GetCustomerBalanceResponse: CommonResponseBase
    {
        public List<GetCustomerBalanceResponseData> Data { get; set; }
    }
    public class GetCustomerBalanceResponseData
    {
        public string CustomerID { get; set; }
        public string CardBalance { get; set; }
        public string CCMSLimitValue { get; set; }
        public string Drivestars { get; set; }
        public string ExpiredDrivestars { get; set; }
        public string ExpiringDrivestars { get; set; }
        public string CashPurseLimit { get; set; }
        public string DailyCashLimitBalance { get; set; }
        public string IndividualOrgName { get; set; }
        public string RegionalOfficeName { get; set; }
    }
}
