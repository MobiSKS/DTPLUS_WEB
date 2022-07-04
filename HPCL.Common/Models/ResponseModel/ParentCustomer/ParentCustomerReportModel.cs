using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.ParentCustomer
{
    public class ParentCustomerReportModel:CommonResponseBase
    {
        public List<ParentCustomerReportDetails> Data { get; set; }
    }
    public class ParentCustomerReportDetails
    {
        public string CustomerID { get; set; }
        public string FormNumber { get; set; }
        public string RequestId { get; set; }
        public string CustomerName { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
        public string CityName { get; set; }
        public string NameOnCard { get; set; }
        public string CreatedDate { get; set; }
        public string RequestStatus { get; set; }

        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Status { get; set; }


    }
}
