using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Hotlisting
{
    public class ViewHotlistingorReactivateResponse : CommonResponseBase
    {
        public ViewHotlistingorReactivateResponse()
        {
            Data = new List<ViewHotlistingorReactivateDetails>();
        }
        public virtual List<ViewHotlistingorReactivateDetails> Data{ get; set; }
    }
    public class ViewHotlistingorReactivateDetails
    {
        public string HotlistDate { get; set; }
        public string EntityType { get; set; }
        public string EntityIdValue { get; set; }
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string Remarks { get; set; }
        public string Action { get; set; }
        public string Reason { get; set; }
    }
}
