using System;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System.Collections.Generic;
using HPCL.Common.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.CustomerSearch
{
    public class BasicSearchModel:BaseEntity
    {
        public string CustomerId { get; set; }
        public string MobileNo { get; set; }
        public string FormNumber { get; set; }
        public string NameonCard { get; set; }
        public string CustomerName { get; set; }
        public string CommunicationStateId { get; set; }
        public string CommunicationCityName { get; set; }

     
        public string RetailOutletStateName { get; set; }

        public BasicSearchModel()
        {
            RetailOutletStates = new List<StateResponseModal>();

            RetailOutletStates.Add(new StateResponseModal
            {
               // CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });
        }
        public int _StateID { get; set; }
           public virtual List<StateResponseModal> RetailOutletStates { get; set; }
}
}
