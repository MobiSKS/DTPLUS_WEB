using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class GetCustomerCardWiseBalanceResponse : CommonResponseBase
    {
        public List<GetCustomerCardWiseBalanceResponseData> Data { get; set; }
    }
    public class GetCustomerCardWiseBalanceResponseData
    {
        public string CardNo { get; set; }
        public string LastDateOfTransaction { get; set; }
        public string VechileNo { get; set; }
        public string Mobileno { get; set; }
        public string CardBalance { get; set; }

    }
}
