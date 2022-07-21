using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class BasicSearchViewModel : CommonResponseBase
    {
        public string CustomerId { get; set; }
        public string FormNumber { get; set; }
        public string NameOnCard { get; set; }
        public string CustomerName { get; set; }
        public string StateId { get; set; }
        public string City { get; set; }
        public string SortResult { get; set; }
        public string MobileNumber { get; set; }
        public string CardNumber { get; set; }
        public string CardTypeId { get; set; }
        public string VehicleNo { get; set; }
        public string IssueDate { get; set; }
        public string SearchType { get; set; }
        public BasicSearchViewModel()
        {
            SearchStateMdl = new List<StateResponseModal>();
            SearchStateMdl.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "--ALL--"
            });
          
            Data = new List<SearchCustomerData>();
        }
        public virtual List<StateResponseModal> SearchStateMdl { get; set; }
      
        public virtual List<SearchCustomerData> Data { get; set; }
    }
  
}
