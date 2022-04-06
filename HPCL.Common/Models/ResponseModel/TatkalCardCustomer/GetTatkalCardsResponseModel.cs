using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.TatkalCardCustomer
{
    public class GetTatkalCardsResponseModel : CommonResponseBase
    {
        public GetTatkalCardsResponseModel()
        {
           
            ObjGetCardDetailsTatkalCardsToTatkalCustomer = new List<TatkalCardDetails>();
            ObjGetStatusTatkalCardsToTatkalCustomer = new List<TatkalCardCustomerDetails>();
        }
        public virtual List<TatkalCardDetails> ObjGetCardDetailsTatkalCardsToTatkalCustomer { get; set; }
        public virtual List<TatkalCardCustomerDetails> ObjGetStatusTatkalCardsToTatkalCustomer { get; set; }
    }
    public class TatkalCardDetails
    {
        public string CardNo { get; set; }
    }
    public class TatkalCardCustomerDetails
    {
        public string Status { get; set; }
        public string Reason { get; set; }
        
    }
}
