using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class ConvertParenttoAggregatorViewModel : CommonResponseBase
    {
        public string CustomerId { get; set; }
        public string NameOnCard { get; set; }
        public ConvertParenttoAggregatorViewModel()
        {
            Data = new List<SearchCustomerData>();
        }
        public virtual List<SearchCustomerData> Data { get; set; }
    }
}
