﻿namespace HPCL_Web.Helper
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
        public static string getSalesArea = "api/dtplus/settings/get_sales_area";
        public static string validateUserName = "api/dtplus/officer/check_username";
        public static string insertOfficer = "api/dtplus/officer/insert_officer";
        public static string updateOfficer = "api/dtplus/officer/update_officer";
        public static string getOfficerDetails = "api/dtplus/officer/get_officer_detail";
        public static string deleteOfficer = "api/dtplus/officer/delete_officer";
        public static string bindOfficer = "api/dtplus/officer/bind_officer";
        public static string zonalOffice = "api/dtplus/zonaloffice/get_zonal_office";
        public static string regionalOffice = "api/dtplus/regionaloffice/get_regional_office";
        public static string createHeadOffice= "/dtpwebapi/api/dtplus/hq/insert_hq";
        public static string updateHeadOffice = "/dtpwebapi/api/dtplus/hq/update_hq";

        #endregion

        #region Customer

        public static string getCustomerType= "/dtpwebapi/api/dtplus/customer/get_customer_type";

        #endregion

        #region Cards

        public static string GetStatusTypeUrl= "/dtpwebapi/api/dtplus/settings/get_entity_status_type";
        public static string SearchCardUrl = "/dtpwebapi/api/dtplus/card/search_manage_card";
        public static string GetCardDetailsUrl= "/dtpwebapi/api/dtplus/card/get_card_limit_features";
        public static string UpdateMobileUrl = "/dtpwebapi/api/dtplus/card/update_mobile_in_card";
        public static string UpdateServiceUrl = "/dtpwebapi/api/dtplus/card/update_service_on_card";
        public static string GetAllCardStatusUrl = "/dtpwebapi/api/dtplus/card/get_all_card_with_status";
        public static string UpdateCardStatusUrl = "/dtpwebapi/api/dtplus/card/update_card_status";
        #endregion

        #region Merchant
        public static string getMerchantType = "api/dtplus/merchant/get_merchant_type";
        public static string getOutletCategory = "api/dtplus/merchant/get_outlet_category";
        public static string getSbu = "api/dtplus/merchant/get_sbu";
        #endregion
    }
}
