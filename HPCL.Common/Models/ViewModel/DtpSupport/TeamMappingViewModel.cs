using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.DtpSupport
{
    public class TeamMappingViewModel:CommonResponseBase
    {
        public string ZBMID { get; set; }
        public string ZBMName { get; set; }
        public string RSMID { get; set; }
        public string RSMName { get; set; }
        public string RBEID { get; set; }
        public string RBEName { get; set; } 
        public string Location { get; set; }
        public virtual List<TeamMappingSearchResponseData> Data { get; set; }
    }
    public class TeamMappingSearchResponseData
    {
        public string ZBMId { get; set; }
        public string ZBMName { get; set; }
        public string RSMId { get; set; }
        public string RSMName { get; set; }
        public string RBEId { get; set; }
        public string RBEName { get; set; }
        public string Location { get; set; }

        public string TeamMappingId { get; set; }
    }
}
