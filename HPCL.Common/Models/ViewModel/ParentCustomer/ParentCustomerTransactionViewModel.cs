using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class ParentCustomerTransactionViewModel : CommonResponseBase
    {
        public ParentCustomerTransactionViewModel()
        {
            Data = new List<CustomerTranasctionModel>();

            FromDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");

            ChildCustomerIds = new List<ChildCustomerDetails>();
            SelectedChildCustomerIds = new List<ChildCustomerDetails>();

        }
        public string ParentCustomerID { get; set; }
        public string ChildCustomerId { get; set; }
        public List<CustomerTranasctionModel> Data { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public bool AllCustomerSelect { get; set; }
        public virtual List<ChildCustomerDetails> ChildCustomerIds { get; set; }
        public virtual List<ChildCustomerDetails> SelectedChildCustomerIds { get; set; }
    }
    public class CustomerTranasctionModel
    {
        public string ChildCustomerID { get; set; }
        public string ChildId { get; set; }
        public string ChildCustomerName { get; set; }
        public string ControlCardNo { get; set; }
        public string NameonCard { get; set; }
        public string RegionalOfficeName { get; set; }
        public string StatusName { get; set; }

    }
   
   
    public class ChildCustomerDetails
    {
        public string ChildCustomerID { get; set; }
    }
}
