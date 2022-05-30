using System.ComponentModel.DataAnnotations;


namespace HPCL.Common.Models.CommonEntity
{
    public class MenuRequestModel : BaseEntity
    {
        [Required]
        public string UserType { get; set; }
    }
}
