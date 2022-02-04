﻿using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.Locations
{
    public class HeadOfficeDetails: BaseEntity
    {
        [Required(ErrorMessage = "Head Office Code is Required")]
        public string HQCode { get; set; }
        [Required(ErrorMessage = "Head Office Name is Required")]
        public string HQName { get; set; }
        [Required(ErrorMessage = "Head Office Short Name is Required")]
        public string HQShortName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class HeadOfficeDetailsResponse
    {
        public int HQID { get; set; }
        public string HQCode { get; set; }
        public string HQName { get; set; }
        public string HQShortName { get; set; }
    }

    public class UpdateHeadOfficeDetails: BaseEntity
    {
        public int HQID { get; set; }
        public string HQCode { get; set; }
        public string HQName { get; set; }
        public string HQShortName { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class InsertHeadOfficeDetailsResponse
    {
        public string Reason { get; set; }
    }

    public class UpdateHeadOfficeDetailsResponse
    {
        public string Reason { get; set; }
    }
}
