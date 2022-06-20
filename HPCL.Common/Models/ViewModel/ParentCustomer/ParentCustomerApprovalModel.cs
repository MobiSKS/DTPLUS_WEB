using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class ParentCustomerApprovalModel:CommonResponseBase
    {
        public ParentCustomerApprovalModel()
        {
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
            Data = new List<ParentCustomerApprovalDetails>();
        }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<ParentCustomerApprovalDetails> Data { get; set; }
    }
    public class ParentCustomerApprovalDetails
    {
        public string CustomerName { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
        public string City { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Comments { get; set; }
        public string CustomerId { get; set; }
        public string FormNumber { get; set; }
        public string Id { get; set; }


    }
}
