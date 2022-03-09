using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Merchant
{
    public class MerchantModel
    {
        public MerchantModel()
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
        public string SearchMerchantID { get; set; }
        public string MerchantID { get; set; }
        public string  MerchantType { get; set; }
        [StringLength(10)]
        public string MappedMerchantID { get; set; }
        public string OutletCategory { get; set; }
        public string HighwayName { get; set; }
        public string SBUType { get; set; }
        public string PANCardNumber { get; set; }
        public string RetailOutletName { get; set; }
        public string DealerName { get; set; }
        public string DealerMobileNumber { get; set; }
        public string PreHighwayNumber { get; set; }
        public string HighwayNumber { get; set; }
        public string LPG_CNGSale { get; set; }
        public string GSTNumber { get; set; }
        public string Retail_Outlet_Address1 { get; set; }
        public string Retail_Outlet_Address2 { get; set; }
        public string Retail_Outlet_Address3 { get; set; }
        public string Retail_Outlet_Location { get; set; }
        public string Retail_Outlet_City { get; set; }
        public string Retail_Outlet_State { get; set; }
        public string Retail_Outlet_District { get; set; }
        public string Retail_Outlet_Pin { get; set; }
        public string Retail_Outlet_PhoneOfficeCode { get; set; }
        public string Retail_Outlet_PhoneOffice { get; set; }
        public string Retail_Outlet_FaxCode { get; set; }
        public string Retail_Outlet_Fax { get; set; }
        public string ZonalOffice { get; set; }
        public string RegionalOffice { get; set; }
        public string SalesArea { get; set; }
        [StringLength(8)]
        public string ERPCode { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Misc { get; set; }
        public string Comm_Address1 { get; set; }
        public string Comm_Address2 { get; set; }
        public string Comm_Address3 { get; set; }
        public string Comm_City { get; set; }
        public string Comm_State { get; set; }
        public string Comm_District { get; set; }
        public string Comm_Pin { get; set; }
        public string Comm_Pre_PhoneNumber { get; set; }
        public string Comm_PhoneNumber { get; set; }
        public string Comm_Pre_Fax { get; set; }
        public string Comm_Fax { get; set; }
        public string NumOfLiveTerminals { get; set; }
        public string TerminalTypeRequested { get; set; }
        public string Retail_DistictID { get; set; }
        public string ZonalOfficeID { get; set; }
        public string RegionalOfcID { get; set; }
        public string SalesAreaID { get; set; }
        public string Comm_DistictID { get; set; }
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
