﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models
{
    public class OfficerLocationModel
    {
        [Required(ErrorMessage = "Zonal Office is Required")]
        public int ZoneOfcID { get; set; }
        public int RegionalOfcID { get; set; }
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }
        public bool IsNewUser { get; set; }
        public virtual List<ZoneOffice> ZoneOffices { get; set; }
        public virtual List<RegionalOffice> RegionalOffices { get; set; }
        public virtual List<UserName> UserNames { get; set; }
    }
    public class ZoneOffice
    {
        public int ZoneOfficeID { get; set; }
        public string ZomeOfficeName { get; set; }
    }
    public class RegionalOffice
    {
        public int RegionalOfficeID { get; set; }
        public string RegionalOfficeName { get; set; }
    }
    public class UserName
    {
        public int UserID { get; set; }
        public string UserNameValue { get; set; }
    }
}
