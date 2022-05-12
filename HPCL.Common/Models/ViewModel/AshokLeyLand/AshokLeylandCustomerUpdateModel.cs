using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.Officers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.AshokLeyLand
{
    public class AshokLeylandCustomerUpdateModel : BaseEntity
    {
        public AshokLeylandCustomerUpdateModel()
        {
            CustomerStateMdl = new List<StateResponseModal>();
            CustomerStateMdl.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });

            CommunicationDistrictMdl = new List<OfficerDistrictModel>();
            CommunicationDistrictMdl.Add(new OfficerDistrictModel
            {
                districtID = 0,
                districtName = "Select District"
            });
        }

        public virtual List<StateResponseModal> CustomerStateMdl { get; set; }
        public virtual List<OfficerDistrictModel> CommunicationDistrictMdl { get; set; }

        [Required(ErrorMessage = "Customer Or Org Name is Required")]
        public string IndividualOrgName { get; set; }

        public string IndividualOrgNameTitle { get; set; }

        [Required(ErrorMessage = "Name on Card is Required")]
        public string NameOnCard { get; set; }

        [Required(ErrorMessage = "Address1 is Required")]
        public string CommunicationAddress1 { get; set; }

        [Required(ErrorMessage = "Address2 is Required")]
        public string CommunicationAddress2 { get; set; }

        [Required(ErrorMessage = "City is Required")]
        public string CommunicationCityName { get; set; }

        [Required(ErrorMessage = "Pin Code is Required")]
        public string CommunicationPincode { get; set; }

        [Required(ErrorMessage = "State is Required")]
        public string CommunicationStateId { get; set; }

        [Required(ErrorMessage = "District is Required")]
        public string CommunicationDistrictId { get; set; }
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationFax { get; set; }

        [Required(ErrorMessage = "Mobile is Required")]
        public string CommunicationMobileNo { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string CommunicationEmailid { get; set; }
       
        [Required(ErrorMessage = "Dealer Code is Required")]
        public string DealerCode { get; set; }
        public string SalesExecutiveEmployeeID { get; set; }
        public string Message { get; set; }

        public string CommunicationDialCode { get; set; }
        public string CommunicationPhonePart2 { get; set; }
        public string CommunicationFaxCode { get; set; }
        public string CommunicationFaxPart2 { get; set; }
        public string CustomerID { get; set; }
        public int Internel_Status_Code { get; set; }
        public string CASID { get; set; }
        public string CADID { get; set; }
    }

}