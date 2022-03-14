using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HPCL.Common.Models.ViewModel.AshokLeyLand
{
    public class ALOTCCardRequestModel: BaseEntity
    {
        [Required(ErrorMessage = "No of Cards is Required")]
        public string NoofCards { get; set; }

        [Required(ErrorMessage = "Dealer Code is Required")]
        public string DealerCode { get; set; }
        public string Remarks { get; set; }
        public int Internel_Status_Code { get; set; }
    }
}
