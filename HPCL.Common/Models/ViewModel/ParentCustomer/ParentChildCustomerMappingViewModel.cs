using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class ParentChildCustomerMappingViewModel:CommonResponseBase
    {
        public ParentChildCustomerMappingViewModel()
        {
            Data = new List<ParentChildCustomerMappingDetails>();
        }
        public string ParentCustomerId { get; set; }
        public string ChildCustomerId { get; set; }
        public List<ParentChildCustomerMappingDetails> Data { get; set; }
    }
    public class ParentChildCustomerMappingDetails
    {
        public string CustomerId { get; set; }  
        public string NameonCard { get; set; }
        public string CustomerName { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
        public string Status { get;set; }
        public string Reason { get; set; }
    }
}
