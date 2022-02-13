using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models.Merchant
{
    public class MerchantModel
    {
        public MerchantModel()
        {
            MerchantTypes = new List<MerchantTypeModel>();
            OutletCategories = new List<OutletCategoryModel>();
            SBUTypes = new List<SBUTypeModel>();
            RetailOutletStates = new List<RetailOutletStateModel>();
            RetailOutletDistricts = new List<RetailOutletDistrictModel>();
            CommStates = new List<CommStateModel>();
            CommDistricts = new List<CommDistrictModel>();
            ZonalOffices = new List<ZonalOfficeModel>();
            SalesAreas = new List<MerchantSalesAreaModel>();
            PreHighwayNumbers = new List<PreHighwayNumberModel>();
            MerchantTypes.Add(new MerchantTypeModel
            {
                MerchantTypeCode = 0,
                MerchantTypeName = "--Select--"
            });
            OutletCategories.Add(new OutletCategoryModel    
            {
                OutletCategoryCode = 0,
                OutletCategoryName = "--Select--"
            });
            SBUTypes.Add(new SBUTypeModel
            {
                SBUId = 0,
                SBUName = "--Select--"
            });
            RetailOutletStates.Add(new RetailOutletStateModel
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });
            CommStates.Add(new CommStateModel
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });
            ZonalOffices.Add(new ZonalOfficeModel
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--Select--"
            });
            PreHighwayNumbers.Add(new PreHighwayNumberModel
            {
                PreHighwayNumberID = 1,
                PreHighwayNumberName = "NH"
            });
            PreHighwayNumbers.Add(new PreHighwayNumberModel
            { 
                PreHighwayNumberID = 2,
                PreHighwayNumberName = "SH"
            });
        }
        public string SearchMerchantID { get; set; }
        public string MerchantID { get; set; }
        public string  MerchantType { get; set; }
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
        public virtual List<MerchantTypeModel> MerchantTypes { get; set; }
        public virtual List<OutletCategoryModel> OutletCategories { get; set; }
        public virtual List<SBUTypeModel> SBUTypes { get; set; }
        public virtual List<RetailOutletStateModel> RetailOutletStates { get; set; }
        public virtual List<RetailOutletDistrictModel> RetailOutletDistricts { get; set; }
        public virtual List<CommStateModel> CommStates { get; set; }
        public virtual List<CommDistrictModel> CommDistricts { get; set; }
        public virtual List<ZonalOfficeModel> ZonalOffices { get; set; }
        public virtual List<MerchantSalesAreaModel> SalesAreas { get; set; }
        public virtual List<PreHighwayNumberModel> PreHighwayNumbers { get; set; }
    }
    public class MerchantTypeModel
    {
        public int MerchantTypeCode { get; set; }
        public string MerchantTypeName { get; set; }
    }
    public class OutletCategoryModel
    {
        public int OutletCategoryCode { get; set; }
        public string OutletCategoryName { get; set; }
    }
    public class SBUTypeModel
    {
        public int SBUId { get; set; }
        public string SBUName { get; set; }
    }
    public class RetailOutletStateModel
    {
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
    public class RetailOutletDistrictModel
    {
        public int stateID { get; set; }
        public int districtID { get; set; }
        public string districtName { get; set; }
    }
    public class CommStateModel
    {
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
    public class CommDistrictModel
    {
        public int stateID { get; set; }
        public int districtID { get; set; }
        public string districtName { get; set; }
    }
    public class ZonalOfficeModel
    {
        public int ZonalOfficeID { get; set; }
        public string ZonalOfficeName { get; set; }
    }
    public class RegionalOfficeModel
    {
        public int RegionalOfficeID { get; set; }
        public string RegionalOfficeName { get; set; }
    }
    public class MerchantSalesAreaModel
    {
        public int RegionID { get; set; }
        public int SalesAreaID { get; set; }
        public string SalesAreaName { get; set; }
    }
    public class PreHighwayNumberModel
    {
        public int PreHighwayNumberID { get; set; }
        public string PreHighwayNumberName { get; set; }
    }
}
