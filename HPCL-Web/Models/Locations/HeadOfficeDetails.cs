using System.ComponentModel.DataAnnotations;

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
        public int CreatedBy { get; set; }
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
        public int ModifiedBy { get; set; }
    }

    public class InsertHeadOfficeDetailsResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }

    public class UpdateHeadOfficeDetailsResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
