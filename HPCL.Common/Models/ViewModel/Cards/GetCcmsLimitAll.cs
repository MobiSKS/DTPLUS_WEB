using HPCL.Common.Models.CommonEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class GetCcmsLimitAll : BaseEntity
    {
        public GetCcmsLimitAll()
        {
            CardStatusList = new List<StatusModal>();
            LimitTypeModals = new List<LimitTypeModal>();
        }

        [Required(ErrorMessage = "Customer Id is required")]
        public string CustomerId { get; set; }
        public int StatusFlag { get; set; }
        public int TypeOfLimit { get; set; }
        public int CcmsLimit { get; set; }

        public virtual List<StatusModal> CardStatusList { get; set; }
        public virtual List<LimitTypeModal> LimitTypeModals { get; set; }
    }


    public class UpdateCcmsLimitAll : BaseEntity
    {
        public string CustomerId { get; set; }
        public int LimitType { get; set; }
        public int Amount { get; set; }
        public string ModifiedBy { get; set; }
    }
}
