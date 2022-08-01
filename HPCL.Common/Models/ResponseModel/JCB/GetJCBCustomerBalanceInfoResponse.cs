using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.JCB
{
    public class GetJCBCustomerBalanceInfoResponse : CommonResponseBase
    {
        public List<GetJCBCustomerBalanceInfoData> Data { get; set; }
    }
    public class GetJCBCustomerBalanceInfoData
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