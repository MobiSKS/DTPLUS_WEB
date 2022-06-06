using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class GetValidateNewCustomerRequestModel : BaseEntity
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FormNumber { get; set; }
        public string StateId { get; set; }
        public string CustomerName { get; set; }
        public string RegionalOfficeId { get; set; }
        public string Status { get; set; }
        public string CustomerStatus { get; set; }//for Verfiy/reject in Verfiy Fleet Customer
        public string CustomerId { get; set; }
        public string VerifyRemark { get; set; }
        public string VerifyBy { get; set; }
    }
}