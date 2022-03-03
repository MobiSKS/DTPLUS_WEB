using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer
{
    public class LinkCardsToMerchantModel
    {
        public LinkCardsToMerchantModel()
        {
            ObjAllocatedCardsToMerchant = new List<OTCCardModel>();
        }
        public string NoOfCardsAllocated { get; set; }
        public string MerchantId { get; set; }
        public List<OTCCardModel> ObjAllocatedCardsToMerchant { get; set; }
    }
}
