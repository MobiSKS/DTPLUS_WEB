using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer
{
    public class AllocateCardsToMerchantRequestModel : BaseEntity
    {
        public AllocateCardsToMerchantRequestModel()
        {
            ObjAllocatedCardsToMerchant = new List<OTCCardModel>();
        }
        public string NoOfCardsAllocated { get; set; }
        public string MerchantId { get; set; }
        public List<OTCCardModel> ObjAllocatedCardsToMerchant { get; set; }
    }

}