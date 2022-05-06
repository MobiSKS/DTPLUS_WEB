using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.TMS
{
    public class ViewCustomerSearch : CommonResponseBase
    {
        public virtual List<ViewCustomerSearchResult> Data { get; set; }
        public ViewCustomerSearch()
        {
            Data = new List<ViewCustomerSearchResult>();
        }
    }
    public class ViewCustomerSearchResult
    {
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string TMSUserId { get; set; }
        public string TMSStatus { get; set; }
        public string TMSStatusID { get; set; }
    }

}