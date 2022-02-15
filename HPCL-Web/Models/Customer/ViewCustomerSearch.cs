using System;
using System.Collections.Generic;

namespace HPCL_Web.Models.Customer
{

    public class ViewCustomerSearch : BaseEntity
    {
        public ViewCustomerSearch()
        {
            CustomerViewModalOutputs = new List<CustomerViewModalOutput>();
        }

        public int StatusId { get; set; }

        public Int64 FormNumber { get; set; }

        public Int64 CustomerReferenceNo { get; set; }

        public string CustomerID { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public virtual List<CustomerViewModalOutput> CustomerViewModalOutputs { get; set; }
    }
    public class CustomerViewModalOutput
    {
        
        
        public string ZonalOfficeName { get; set; }
        
        public string RegionalOfficeName { get; set; }

        public Int64 FormNumber { get; set; }

        
        public Int64 CustomerReferenceNo { get; set; }

        
        public string CustomerID { get; set; }

        
        public string CustomerName { get; set; }

        
        public string CreatedDate { get; set; }

        
        public string CreatedBy { get; set; }
        
        public string StatusName { get; set; }
       
        public string KYCStatus { get; set; }
    }
}
