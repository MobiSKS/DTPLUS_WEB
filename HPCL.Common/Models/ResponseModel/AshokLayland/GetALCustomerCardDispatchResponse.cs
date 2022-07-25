﻿using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.AshokLayland
{
    public class GetALCustomerCardDispatchResponse : CommonResponseBase
    {
        public List<GetALCustomerCardDispatchData> Data { get; set; }
        public GetALCustomerCardDispatchResponse()
        {
            Data = new List<GetALCustomerCardDispatchData>();
        }
    }
    public class GetALCustomerCardDispatchData
    {
        public string CardNo { get; set; }
        public string DispatchDate { get; set; }
        public int CourierName { get; set; }
        public string AirwaysBillNo { get; set; }
        public string DispatchStatus { get; set; }
        public string DeliveredTo { get; set; }
        public string DeliveryDate { get; set; }
        public string ReturnReason { get; set; }
        public string MOComments { get; set; }
    }
}