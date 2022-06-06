using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.Officers;

namespace HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer
{
    public class MyHPOTCCardCustomerModel : BaseEntity
    {

        public MyHPOTCCardCustomerModel()
        {
            ObjOTCCardEntryDetail = new List<OTCCardEntryDetailsMdl>();

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

        [Required(ErrorMessage = "Individual Organisation Title is Required")]
        public string IndividualOrgNameTitle { get; set; }

        [Required(ErrorMessage = "Individual Organisation Name is Required")]
        public string IndividualOrgName { get; set; }

        [Required(ErrorMessage = "Name On Card is Required")]
        public string NameOnCard { get; set; }

        [Required(ErrorMessage = "Income Tax Pan is Required")]
        public string IncomeTaxPan { get; set; }

        [Required(ErrorMessage = "Communication Address 1 is Required")]
        public string CommunicationAddress1 { get; set; }

        [Required(ErrorMessage = "Communication Address 2 is Required")]
        public string CommunicationAddress2 { get; set; }

        [Required(ErrorMessage = "Communication City is Required")]
        public string CommunicationCityName { get; set; }

        [Required(ErrorMessage = "Communication PIN is Required")]
        public string CommunicationPincode { get; set; }

        [Required(ErrorMessage = "Communication State is Required")]
        public int CommunicationStateId { get; set; }

        [Required(ErrorMessage = "Communication District is Required")]
        public int CommunicationDistrictId { get; set; }
        public string CommunicationDialCode { get; set; }
        public string CommunicationPhonePart2 { get; set; }
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationFaxCode { get; set; }
        public string CommunicationFaxPart2 { get; set; }
        public string CommunicationFax { get; set; }

        [Required(ErrorMessage = "Communication Mobile No is Required")]
        public string CommunicationMobileNo { get; set; }

        [Required(ErrorMessage = "Communication Email is Required")]
        public string CommunicationEmailid { get; set; }

        [Required(ErrorMessage = "Communication Form Number is Required")]
        public string FormNumber { get; set; }
        public string MerchantId { get; set; }
        public string CopyofDriverLicense { get; set; }
        public string CopyofVehicleRegistrationCertificate { get; set; }

        public virtual List<OTCCardEntryDetailsMdl> ObjOTCCardEntryDetail { get; set; }

        public virtual List<StateResponseModal> CustomerStateMdl { get; set; }
        public virtual List<OfficerDistrictModel> CommunicationDistrictMdl { get; set; }

        public string CardNumber1 { get; set; }
        public string CardNumber2 { get; set; }
        public string VehicleNo1 { get; set; }
        public string VehicleNo2 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public int RegionalOfficeId { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Remarks { get; set; }
        public string LoggedInAs { get; set; }
        public string Zone { get; set; }
        public string RegionalOffice { get; set; }
        public string SalesArea { get; set; }
        public string OutletName { get; set; }
        public string ExternalPANAPIStatus { get; set; }
        public string ExternalVehicleAPIStatus { get; set; }
        public string UserName { get; set; }        
    }

    [Serializable()]
    public class OTCCardEntryDetailsMdl
    {
        public string VechileNo { get; set; }
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
    }

}
