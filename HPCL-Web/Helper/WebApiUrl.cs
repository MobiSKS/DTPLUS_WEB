namespace HPCL_Web.Helper
{
    public static class WebApiUrl
    {
        #region Account
        
        public static string generatetoken = "api/dtplus/generate_token";
        public static string getuserlogin = "api/dtplus/login/get_user_login";

        #endregion
        #region Officer

        public static string getOfficerType = "api/dtplus/officer/get_officer_type";
        public static string getLocationZone = "api/dtplus/settings/get_zone";
        public static string getLocationRegion = "api/dtplus/settings/get_region";
        public static string getLocationHq = "api/dtplus/hq/get_hq";
        public static string getState = "api/dtplus/settings/get_state";
        public static string getDistrict = "api/dtplus/settings/get_district";
        public static string validateUserName = "api/dtplus/officer/check_username";
        public static string insertOfficer = "api/dtplus/officer/insert_officer";

        #endregion



        #region customer

        public static string getCustomerType = "/dtpwebapi/api/dtplus/customer/get_customer_type";
        public static string getZonalOffice = "/dtpwebapi/api/dtplus/zonaloffice/get_zonal_office";
        public static string getRegionalOffice = "/dtpwebapi/api/dtplus/regionaloffice/get_regional_office";
        public static string getCustomerSubType = "/dtpwebapi/api/dtplus/customer/get_customer_sub_type";
        public static string getTbentityName = "/dtpwebapi/api/dtplus/customer/get_tbentity_name";
        public static string getTypeOfFleet = "/dtpwebapi/api/dtplus/customer/get_type_of_fleet";
        public static string getSecretQuestion = "/dtpwebapi/api/dtplus/customer/get_secret_question";
        public static string insertCustomer = "/dtpwebapi/api/dtplus/customer/insert_customer";
        public static string getVehicleTpe = "/dtpwebapi/api/dtplus/customer/get_vehicle_type";
        public static string uplaodCustomerKyc = "/dtpwebapi/api/dtplus/customer/upload_customer_kyc";



        #endregion
    }
}
