using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.DtpSupport
{
    public class TeamMappingSearchRequest:BaseEntity
    {
        public string ZBMID { get; set; }
        public string ZBMName { get; set; }
        public string RSMID { get; set; }
        public string RSMName { get; set; }
        public string RBEID { get; set; }
        public string RBEName { get; set; }
        public string Location { get; set; }
    }
}
