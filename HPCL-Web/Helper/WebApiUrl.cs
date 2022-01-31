namespace HPCL_Web.Helper
{
    public static class WebApiUrl
    {
        #region Account
        
        public static string generatetoken = "api/dtplus/generate_token";
        public static string getuserlogin = "api/dtplus/login/get_user_login";

        #endregion
        #region Officer

        public static string getOfficerType = "/dtpwebapi/api/dtplus/officer/get_officer_type";
        public static string getLocationZone = "/dtpwebapi/api/dtplus/settings/get_zone";
        public static string getLocationRegion = "/dtpwebapi/api/dtplus/settings/get_region";
        public static string getLocationHq = "/dtpwebapi/api/dtplus/hq/get_hq";
        public static string getState = "/dtpwebapi/api/dtplus/settings/get_state";
        public static string getDistrict = "/dtpwebapi/api/dtplus/settings/get_district";
        public static string validateUserName = "/dtpwebapi/api/dtplus/officer/check_username";
        public static string insertOfficer = "/dtpwebapi/api/dtplus/officer/insert_officer";
        public static string updateOfficer = "/dtpwebapi/api/dtplus/officer/update_officer";
        public static string getOfficerDetails = "/dtpwebapi/api/dtplus/officer/get_officer_detail";
        public static string deleteOfficer = "/dtpwebapi/api/dtplus/officer/delete_officer";
        public static string bindOfficer = "/dtpwebapi/api/dtplus/officer/bind_officer";
        public static string zonalOffice = "/dtpwebapi/api/dtplus/zonaloffice/get_zonal_office";
        public static string regionalOffice = "/dtpwebapi/api/dtplus/regionaloffice/get_regional_office";
        public static string createHeadOffice= "/dtpwebapi/api/dtplus/hq/insert_hq";
        public static string updateHeadOffice = "/dtpwebapi/api/dtplus/hq/update_hq";

        #endregion

        #region Customer

        public static string getCustomerType= "/dtpwebapi/api/dtplus/customer/get_customer_type";

        #endregion
    }
}
