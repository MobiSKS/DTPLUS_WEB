using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.AshokLeyLand
{
    public class ALCustomerProfileModel : BaseEntity
    {
        public ALCustomerProfileModel()
        {
            CustomerZonalOfficeMdl = new List<CustomerZonalOfficeModel>();
            CustomerZonalOfficeMdl.Add(new CustomerZonalOfficeModel
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "Select Zonal Office"
            });
            CustomerRegionMdl = new List<CustomerRegionModel>();
            CustomerRegionMdl.Add(new CustomerRegionModel
            {
                RegionalOfficeID = 0,
                RegionalOfficeName = "Select Region"
            });
            CustomerStateMdl = new List<StateResponseModal>();
            CustomerStateMdl.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });
            CustomerDistrictMdl = new List<CustomerDistrictModel>();
            CustomerTbentityMdl = new List<CustomerTbentityModel>();
            CustomerTbentityMdl.Add(new CustomerTbentityModel
            {
                TBEntityID = 0,
                TBEntityName = "Select Type of Business Entity"
            });
            
            SalesAreaMdl = new List<SalesAreaModel>();

            SalesAreaMdl.Add(new SalesAreaModel
            {
                SalesAreaID = 0,
                SalesAreaName = "Select Sales Area"
            });

            SBUTypes = new List<SbuTypeResponseModal>();
        }
        public string CustomerTypeId { get; set; }
        public string CustomerSubtypeId { get; set; }

        public string CustomerZonalOfficeID { get; set; }
        public string SalesArea { get; set; }

        public string CustomerIncomeTaxPan { get; set; }

        public string CustomerTbentityID { get; set; }


        public string CustomerNameOnCard { get; set; }

        public string CustomerResidenceStatus { get; set; }
        public string IndividualOrgNameTitle { get; set; }

        public string IndividualOrgName { get; set; }
        public string CommunicationAddress1 { get; set; }
        public string CommunicationAddress2 { get; set; }
        public string CommunicationAddress3 { get; set; }
        public string CommunicationLocation { get; set; }

        public string CommunicationCity { get; set; }
        public string CommunicationPinCode { get; set; }
        public string CommunicationStateID { get; set; }

        public string CommunicationDistrictId { get; set; }


        //public string CommunicationDialCode { get; set; }
        //public string CommunicationPhoneNo { get; set; }
        //public string CommunicationFaxCode { get; set; }
        //public string CommunicationFax { get; set; }

        public string CommunicationMobileNumber { get; set; }

        public string CommunicationEmail { get; set; }


        //public string PerOrRegAddress1 { get; set; }
        //public string PerOrRegAddress2 { get; set; }
        //public string PerOrRegAddress3 { get; set; }

        //public string PerOrRegAddressLocation { get; set; }

        //public string PerOrRegAddressCity { get; set; }
        //public string PerOrRegAddressPinCode { get; set; }
        //public string PerOrRegAddressStateID { get; set; }
        //public string PerOrRegAddressState { get; set; }
        //public string PerOrRegAddressDistrict { get; set; }

        //public string PermanentDistrictId { get; set; }

        //public string PerOrRegAddressDialCode { get; set; }
        //public string PerOrRegAddressPhoneNumber { get; set; }
        //public string PermanentFaxCode { get; set; }
        //public string PermanentFax { get; set; }


        // Key Office Details

        //public string KeyOffTitle { get; set; }

        //public string KeyOffFirstName { get; set; }

        //public string KeyOffIndividualInitials { get; set; }
        //public string KeyOffLastName { get; set; }
        //public string KeyOffMiddleName { get; set; }
        //public string KeyOffDesignation { get; set; }
        //public string KeyOffFaxCode { get; set; }
        //public string KeyOffFax { get; set; }

        //public string KeyOffDialCode { get; set; }

        //public string KeyOffPhoneCode { get; set; }
        //public string KeyOffPhoneNumber { get; set; }

        //public string KeyOffMobileNumber { get; set; }

        //public string KeyOffEmail { get; set; }
        //public string KeyOffDateOfAnniversary { get; set; }
        //public string KeyOfficialDOB { get; set; }

        //public string CustomerTypeOfFleetID { get; set; }
        //public string KeyOfficialSecretQuestion { get; set; }
        //public string KeyOfficialSecretQuestionName { get; set; }
        //public string KeyOfficialSecretAnswer { get; set; }
        //public string AreaOfOperation { get; set; }
        //public string FleetSizeNoOfVechileOwnedHCV { get; set; }
        //public string FleetSizeNoOfVechileOwnedLCV { get; set; }
        //public string FleetSizeNoOfVechileOwnedMUVSUV { get; set; }
        //public string FleetSizeNoOfVechileOwnedCarJeep { get; set; }
        //public string KeyOfficialCardAppliedFor { get; set; }

        //public string KeyOfficialApproxMonthlySpendsonVechile1 { get; set; }
        //public string KeyOfficialApproxMonthlySpendsonVechile2 { get; set; }
        //public string NoOfCards { get; set; }


        //public string TypeOfCustomerID { get; set; }

        //public string TierOfCustomerID { get; set; }

        //public string CustomerSalesAreaID { get; set; }

        public string FormNumber { get; set; }

        public string OfficerTypeID { get; set; }
        //public virtual List<CardDetails> CardDetailsMdl { get; set; }
        //public virtual List<CustomerTypeModel> CustomerTypeMdl { get; set; }
        //public virtual List<CustomerSubTypeModel> CustomerSubTypeMdl { get; set; }
        public virtual List<CustomerZonalOfficeModel> CustomerZonalOfficeMdl { get; set; }
        public virtual List<CustomerRegionModel> CustomerRegionMdl { get; set; }
        public virtual List<StateResponseModal> CustomerStateMdl { get; set; }
        public virtual List<CustomerDistrictModel> CustomerDistrictMdl { get; set; }
        public virtual List<CustomerTbentityModel> CustomerTbentityMdl { get; set; }
        //public virtual List<CustomerTypeOfFleetModel> CustomerTypeOfFleetMdl { get; set; }
        //public virtual List<CustomerSecretQueModel> CustomerSecretQueMdl { get; set; }
        //public virtual List<VehicleTypeModel> VehicleTypeMdl { get; set; }
        public virtual List<SalesAreaModel> SalesAreaMdl { get; set; }

        //public virtual List<CustomerProfileResponse> CustomerProfileResponses { get; set; }

        public string CustomerSignedOn { get; set; }

        public string CustomerFormNumber { get; set; }
        public string CustomerApplicationDate { get; set; }
        public string CustomerRegionalOfficeID { get; set; }
        [StringLength(10)]
        public string CustomerId { get; set; }
        public string NameOnCard { get; set; }

        public string IncomeTaxPan { get; set; }

        public virtual List<SbuTypeResponseModal> SBUTypes { get; set; }
        public int SBUTypeID { get; set; }
        public int CustomerSalesAreaID { get; set; }
        
    }
}