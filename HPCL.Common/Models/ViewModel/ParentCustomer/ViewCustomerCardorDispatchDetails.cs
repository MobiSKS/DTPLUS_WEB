using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class ViewCustomerCardorDispatchDetails:CommonResponseBase
    {
       public ViewCustomerCardorDispatchDetails()
        {
            CardData = new List<ViewCardDetails>();
            DispatchData = new List<ViewDispatchDetails>();
        }

       public List<ViewCardDetails> CardData { get; set; }
        public List<ViewDispatchDetails> DispatchData { get; set; }
        public string Search { get; set; }
    }
    public class ViewCardDetails
    {
        public string CardNumber { get; set; }
        public string VehicleNumber { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
    }
    public class ViewDispatchDetails
    {
        public string DispatchNumber { get; set; }
    }
}
