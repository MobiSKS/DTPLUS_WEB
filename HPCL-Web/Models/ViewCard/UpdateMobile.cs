using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models.ViewCard
{
    public class UpdateMobile : BaseEntity
    {


        public ObjUpdateMobileandFastagNoInCard[] ObjUpdateMobileandFastagNoInCard { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class ObjUpdateMobileandFastagNoInCard
    {
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string FastagNo { get; set; }
    }

    public class UpdateMobileResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
