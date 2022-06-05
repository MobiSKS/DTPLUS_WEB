using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class SearchCustomerResponseGrid
    {
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public int TotalCards { get; set; }
        public int CreatedRoleId { get; set; }
        public string CreatedRoleName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string KYCStatus { get; set; }
        public string ApprovalComment { get; set; } 
        public string CustomerId { get; set; }

    }
}
