using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer
{
    public class TatkalCardCustomerModel : BaseEntity
    {

        public TatkalCardCustomerModel()
        {
            CustomerStateMdl = new List<StateResponseModal>();
            CustomerStateMdl.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "--Select State--"
            });

            CustomerZonalOfficeMdl = new List<CustomerZonalOfficeModel>();
            CustomerZonalOfficeMdl.Add(new CustomerZonalOfficeModel
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--Select--"
            });

            CustomerRegionMdl = new List<CustomerRegionModel>();
            CustomerRegionMdl.Add(new CustomerRegionModel
            {
                RegionalOfficeID = 0,
                RegionalOfficeName = "--Select--"
            });

            CustomerSecretQueMdl = new List<CustomerSecretQueModel>();
            CustomerSecretQueMdl.Add(new CustomerSecretQueModel
            {
                SecretQuestionId = 0,
                SecretQuestionName = "--Select--"
            });
            DateOfApplication= DateTime.Now.ToString("dd-MM-yyyy");
            SignedOn = DateTime.Now.ToString("dd-MM-yyyy");
        }

        [Required(ErrorMessage = "Driver Title is Required")]
        public string IndividualOrgNameTitle { get; set; }

        [Required(ErrorMessage = "Driver Name is Required")]
        public string IndividualOrgName { get; set; }

        [Required(ErrorMessage = "Income Tax Pan is Required")]
        public string IncomeTaxPan { get; set; }

        [Required(ErrorMessage = "Communication Address is Required")]
        public string CommunicationAddress1 { get; set; }
        public string CommunicationDialCode { get; set; }
        public string CommunicationPhonePart2 { get; set; }
        public string CommunicationPhoneNo { get; set; }

        [Required(ErrorMessage = "Communication Mobile No is Required")]
        public string CommunicationMobileNo { get; set; }

        [Required(ErrorMessage = "Communication Email is Required")]
        public string CommunicationEmailid { get; set; }

        [Required(ErrorMessage = "Communication Form Number is Required")]
        public string FormNumber { get; set; }

        public virtual List<StateResponseModal> CustomerStateMdl { get; set; }
        public virtual List<CustomerRegionModel> CustomerRegionMdl { get; set; }
        public virtual List<CustomerZonalOfficeModel> CustomerZonalOfficeMdl { get; set; }

        [Required(ErrorMessage = "Zonal Office is Required")]
        public int ZonalOffice { get; set; }

        public int Internel_Status_Code { get; set; }
        public string Remarks { get; set; }
               
        public string RegionalOffice { get; set; }

        [Required(ErrorMessage = "Communication State is Required")]
        public int CommunicationStateId { get; set; }

        public virtual List<CustomerSecretQueModel> CustomerSecretQueMdl { get; set; }

        public string KeyOfficialSecretQuestion { get; set; }
        public string KeyOfficialSecretAnswer { get; set; }

        public string DateOfApplication { get; set; }
        public string SignedOn { get; set; }
        public string NameOnCard { get; set; }
        public string ExternalPANAPIStatus { get; set; }

    }


}