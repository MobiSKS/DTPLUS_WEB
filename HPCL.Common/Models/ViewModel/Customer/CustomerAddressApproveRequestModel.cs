using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Customer
{
    public class CustomerAddressApproveRequestModel : CommonResponseBase
    {
        public CustomerAddressApproveRequestModel()
        {
            Data = new List<CustomerAddressApproveDetails>();

            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public virtual List<CustomerAddressApproveDetails> Data { get; set; }
    }
    public class CustomerAddressApproveDetails
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
        public string City { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string PAN { get; set; }
        public string DuplicatePANReason { get; set; }
        public string Comments { get; set; }
    }
}