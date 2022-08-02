using System;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
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
    }
}
