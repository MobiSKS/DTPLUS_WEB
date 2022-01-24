using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models
{
    public class OfficerModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string OfficerType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public Int32 Pin { get; set; }
        public Int32 Mobile { get; set; }
        public Int32 Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public virtual IEnumerable<OfficerTypeModel> OfficerTypeMdl { get; set; }
    }

    public class OfficerTypeModel
    {
        public int OfficerID { get; set; }
        public string OfficerTypeName { get; set; }
    }
}
