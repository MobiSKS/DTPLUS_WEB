using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer
{
    public class MIDAllocationOfCardsModel
    {
        public MIDAllocationOfCardsModel()
        {
            RegionMdl = new List<CustomerRegionModel>();
            RegionMdl.Add(new CustomerRegionModel
            {
                RegionalOfficeID = 0,
                RegionalOfficeName = "--Select--"
            });
        }
        public virtual List<CustomerRegionModel> RegionMdl { get; set; }

        [Required(ErrorMessage = "No of Allocated Cards is Required")]
        public string NoofAllocatedCards { get; set; }

        public string NoOfUnallocatedCards { get; set; }
        
        [Required(ErrorMessage = "Region Name is Required")]
        public int CustomerRegionID { get; set; }
        public string Remarks { get; set; }
        public int Internel_Status_Code { get; set; }

        [Required(ErrorMessage = "Merchant ID is Required")]
        public string MerchantID { get; set; }
    }
}
