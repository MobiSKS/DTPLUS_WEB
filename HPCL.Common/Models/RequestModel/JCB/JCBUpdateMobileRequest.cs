using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.JCB
{
    public class JCBUpdateMobileRequest : BaseEntity
    {
        public JCBUpdateMobileandFastagNoInCard[] ObjUpdateMobileandFastagNoInCard { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class JCBUpdateMobileandFastagNoInCard
    {
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string FastagNo { get; set; }
    }
}