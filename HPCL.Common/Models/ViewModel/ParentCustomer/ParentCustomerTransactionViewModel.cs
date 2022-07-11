using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class ParentCustomerTransactionViewModel:CommonResponseBase
    {
        public ParentCustomerTransactionViewModel()
        {
            Data = new List<CustomerTranasctionModel>();
        }
        public string ParentCustomerID { get; set; }
        public string ChildCustomerId { get; set; }
        public List<CustomerTranasctionModel> Data { get; set; } 
    }
    public class CustomerTranasctionModel
    {
        public string ChildCustomerID { get; set; }
        public string ChildCustomerName { get; set; }
        public string ControlCardNumber { get; set; }
        public string NameonCard { get; set; }
        public string RegionalOffice { get; set; }
        public string Status { get; set; }
    }
    public class Transactiondetails
    {
        public string TransactionID { get; set; }
    }
}
