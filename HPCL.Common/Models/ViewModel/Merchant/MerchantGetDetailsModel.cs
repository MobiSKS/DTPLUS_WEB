using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Merchant
{
    public class MerchantGetDetailsModel
    {
        public MerchantGetDetailsModel()
        {
            MerchantTypes = new List<MerchantTypeResponseModal>();
            OutletCategories = new List<OutletCategoryResponseModal>();
            SBUTypes = new List<SbuTypeResponseModal>();
            RetailOutletStates = new List<StateResponseModal>();
            RetailOutletDistricts = new List<DistrictResponseModal>();
            CommStates = new List<StateResponseModal>();
            CommDistricts = new List<DistrictResponseModal>();
            ZonalOffices = new List<ZonalOfficeResponseModal>();
            SalesAreas = new List<SalesAreaResponseModal>();
            PreHighwayNumbers = new List<PreHighwayNumberResponseModal>();
            MerchantTypes.Add(new MerchantTypeResponseModal
            {
                MerchantTypeCode = 0,
                MerchantTypeName = "--Select--"
            });
            OutletCategories.Add(new OutletCategoryResponseModal
            {
                OutletCategoryCode = 0,
                OutletCategoryName = "--Select--"
            });
            SBUTypes.Add(new SbuTypeResponseModal
            {
                SBUId = 0,
                SBUName = "--Select--"
            });
            RetailOutletStates.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });
            CommStates.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });
            ZonalOffices.Add(new ZonalOfficeResponseModal
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--Select--"
            });
            PreHighwayNumbers.Add(new PreHighwayNumberResponseModal
            {
                PreHighwayNumberID = 1,
                PreHighwayNumberName = "NH"
            });
            PreHighwayNumbers.Add(new PreHighwayNumberResponseModal
            {
                PreHighwayNumberID = 2,
                PreHighwayNumberName = "SH"
            });
        }
        [StringLength(10)]
        public string SearchMerchantId { get; set; }
        public string MerchantId { get; set; }
        [StringLength(8)]
        public string ErpCode { get; set; }
        public string RetailOutletName { get; set; }
        public string MerchantTypeId { get; set; }
        public string MerchantTypeName { get; set; }
        public string DealerName { get; set; }
        [StringLength(10)]
        public string MappedMerchantId { get; set; }
        [StringLength(10)]
        public string DealerMobileNo { get; set; }
        public string OutletCategoryId { get; set; }
        public string OutletCategoryName { get; set; }
        public string HighwayNo1 { get; set; }
        [StringLength(50)]
        public string HighwayNo2 { get; set; }
        [StringLength(50)]
        public string HighwayName { get; set; }
        public string SBUTypeId { get; set; }
        public string SBUName { get; set; }
        [StringLength(6)]
        public string LPGCNGSale { get; set; }
        [StringLength(10)]
        public string PancardNumber { get; set; }
        [StringLength(15)]
        public string GSTNumber { get; set; }
        [StringLength(50)]
        public string RetailOutletAddress1 { get; set; }
        [StringLength(50)]
        public string RetailOutletAddress2 { get; set; }
        [StringLength(50)]
        public string RetailOutletAddress3 { get; set; }
        public string RetailOutletLocation { get; set; }
        [StringLength(20)]
        public string RetailOutletCity { get; set; }
        public string RetailOutletStateId { get; set; }
        public string RetailOutletStateName { get; set; }
        public string RetailOutletDistrictId { get; set; }
        public string RetailOutletDistrictName { get; set; }
        [StringLength(6)]
        public string RetailOutletPinNumber { get; set; }
        [StringLength(12)]
        public string RetailOutletPhoneNumber { get; set; }
        [StringLength(12)]
        public string RetailOutletFax { get; set; }
        public string ZonalOfficeId { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeId { get; set; }
        public string RegionalOfficeName { get; set; }
        public string SalesAreaId { get; set; }
        public string SalesAreaName { get; set; }
        [StringLength(30)]
        public string ContactPersonNameFirstName { get; set; }
        [StringLength(30)]
        public string ContactPersonNameMiddleName { get; set; }
        [StringLength(30)]
        public string ContactPersonNameLastName { get; set; }
        [StringLength(10)]
        public string MobileNo { get; set; }
        [StringLength(50)]
        public string EmailId { get; set; }
        public string Mics { get; set; }
        [StringLength(50)]
        public string CommunicationAddress1 { get; set; }
        [StringLength(50)]
        public string CommunicationAddress2 { get; set; }
        [StringLength(50)]
        public string CommunicationAddress3 { get; set; }
        public string CommunicationLocation { get; set; }
        [StringLength(20)]
        public string CommunicationCity { get; set; }
        public string CommunicationStateId { get; set; }
        public string CommunicationStateName { get; set; }
        public string CommunicationDistrictId { get; set; }
        public string CommunicationDistrictName { get; set; }
        [StringLength(6)]
        public string CommunicationPinNumber { get; set; }
        [StringLength(12)]
        public string CommunicationPhoneNumber { get; set; }
        [StringLength(12)]
        public string CommunicationFax { get; set; }
        [StringLength(1)]
        public string NoofLiveTerminals { get; set; }
        public string TerminalTypeRequested { get; set; }
        public string MerchantStatusId { get; set; }
        public string MerchantStatusName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedTime { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedTime { get; set; }
        public string ReferenceId { get; set; }
        public string Comments { get; set; }
        public string Approvedon { get; set; }
        public string ApprovedBy { get; set; }
        public string Model_State { get; set; }
        public string RetailDistrictIdVal { get; set; }
        public string RegionalOfcIdVal { get; set; }
        public string SalesAreaIdVal { get; set; }
        public string CommDistrictIdVal { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
        public string Error { get; set; }
        public string Success { get; set; }
        public string Search { get; set; }
        public virtual List<MerchantTypeResponseModal> MerchantTypes { get; set; }
        public virtual List<OutletCategoryResponseModal> OutletCategories { get; set; }
        public virtual List<SbuTypeResponseModal> SBUTypes { get; set; }
        public virtual List<StateResponseModal> RetailOutletStates { get; set; }
        public virtual List<DistrictResponseModal> RetailOutletDistricts { get; set; }
        public virtual List<StateResponseModal> CommStates { get; set; }
        public virtual List<DistrictResponseModal> CommDistricts { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZonalOffices { get; set; }
        public virtual List<SalesAreaResponseModal> SalesAreas { get; set; }
        public virtual List<PreHighwayNumberResponseModal> PreHighwayNumbers { get; set; }
    }
}
