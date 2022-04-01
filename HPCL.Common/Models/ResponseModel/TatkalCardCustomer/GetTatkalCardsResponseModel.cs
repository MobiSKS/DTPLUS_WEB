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
            data = new List<TatkalCardDetails>();
        }
        public virtual List<TatkalCardDetails> data { get; set; }
    }
    public class TatkalCardDetails
    {
        public string CardNo { get; set; }
    }
}
