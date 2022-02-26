using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.ViewCard
{
   public class UpdateMobileRequest:BaseEntity
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
}
