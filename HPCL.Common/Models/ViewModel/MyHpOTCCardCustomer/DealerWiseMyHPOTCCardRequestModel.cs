using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer
{
    public class DealerWiseMyHPOTCCardRequestModel : BaseEntity
    {

        [Required(ErrorMessage = "No of Cards is Required")]
        public string NoofCards { get; set; }

        [Required(ErrorMessage = "Merchant Id is Required")]
        public string MerchantId { get; set; }

        public string Remarks { get; set; }
        public int Internel_Status_Code { get; set; }
    }
}
