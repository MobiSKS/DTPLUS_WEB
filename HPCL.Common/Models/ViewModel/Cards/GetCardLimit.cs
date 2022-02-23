using HPCL.Common.Models.CommonEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class GetCardLimit : BaseEntity
    {
        public GetCardLimit()
        {
            CardStatusList = new List<StatusModal>();
        }

        [Required(ErrorMessage = "Customer Id is required")]
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public int Statusflag { get; set; }

        public virtual List<StatusModal> CardStatusList { get; set; }
    }
}
