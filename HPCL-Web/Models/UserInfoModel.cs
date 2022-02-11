using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models
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
    }
}
