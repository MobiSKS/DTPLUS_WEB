using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Cards
{
   public class ControlCardRequest : BaseEntity
    {

        [Required(ErrorMessage = "CustomerId is Required")]
        [StringLength(10)]
        public string CustomerId { get; set; }
    }
}
