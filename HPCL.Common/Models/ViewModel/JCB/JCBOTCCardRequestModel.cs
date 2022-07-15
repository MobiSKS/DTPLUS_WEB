using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.JCB
{
    public class JCBOTCCardRequestModel : BaseEntity
    {
        [Required(ErrorMessage = "Please enter Number of Cards")]
        public string NoofCards { get; set; }

        [Required(ErrorMessage = "Please enter Dealer Code")]
        public string DealerCode { get; set; }
        public string Remarks { get; set; }
        public int Internel_Status_Code { get; set; }
        public string BalanceCard { get; set; }
    }
}