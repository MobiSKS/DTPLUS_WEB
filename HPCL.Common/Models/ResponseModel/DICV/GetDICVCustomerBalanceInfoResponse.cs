using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.DICV
{
    public class GetDICVCustomerBalanceInfoResponse : CommonResponseBase
    {
        public List<GetDICVCustomerBalanceInfoData> Data { get; set; }
    }
    public class GetDICVCustomerBalanceInfoData
    {
        public string CustomerID { get; set; }
        public string CardBalance { get; set; }
        public string CCMSLimitValue { get; set; }
        public string Drivestars { get; set; }
        public string ExpiredDrivestars { get; set; }
        public string ExpiringDrivestars { get; set; }
        public string CashPurseLimit { get; set; }
        public string DailyCashLimitBalance { get; set; }
        public string CustomerName { get; set; }
        public string RegionalOfficeName { get; set; }
    }
}