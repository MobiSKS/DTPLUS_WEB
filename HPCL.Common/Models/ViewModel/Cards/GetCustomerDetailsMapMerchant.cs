using HPCL.Common.Models.CommonEntity;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class GetCustomerDetailsMapMerchant : BaseEntity
    {
        [Required(ErrorMessage = "Customer Id is required")]
        public string CustomerID { get; set; }
        public string MerchantID { get; set; }
        public string StateID { get; set; }
        public string DistrictID { get; set; }
        public string City { get; set; }
        public string HighwayName { get; set; }
        public string HighwayNo1 { get; set; }
        public string HighwayNo2 { get; set; }
    }

    public class CustomerStateModel
    {
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
}
