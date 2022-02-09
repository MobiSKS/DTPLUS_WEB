using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.Cards.SetCcmsLimit
{
    public class SetIndividualLimit : BaseEntity
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string VehicleNo { get; set; }
    }


    public class SearchIndividualCardsResponse
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public int YearOfRegistration { get; set; }
        public string MobileNumber { get; set; }
        public string VehicleMake { get; set; }
        public int CCMSLimitOption { get; set; }
        public string Description { get; set; }
        public int CCMSReloadSaleLimitValue { get; set; }
    }

    public class CCMSBalanceDetail
    {
        public int ActualCCMSBalance { get; set; }
        public int UnallocatedCCMSBalance { get; set; }
    }
}
