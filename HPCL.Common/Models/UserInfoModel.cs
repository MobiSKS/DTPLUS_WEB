using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models
{
    public class UserInfoModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserId { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Useragent { get; set; }
        public string Userip { get; set; }
        [Required]
        [StringLength(6)]
        public string CaptchaCode { get; set; }
    }
}
