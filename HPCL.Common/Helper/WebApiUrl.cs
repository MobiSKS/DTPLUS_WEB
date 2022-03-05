namespace HPCL.Common.Helper
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
        public static string insertRbeOfficer = "api/dtplus/officer/insert_rbe_officer";
        public static string getSalesArea = "api/dtplus/settings/get_sales_area";
        public static string updateOfficer = "api/dtplus/officer/update_officer";
        public static string getOfficerDetails = "api/dtplus/officer/get_officer_detail";
        public static string deleteOfficer = "api/dtplus/officer/delete_officer";
        public static string bindOfficer = "api/dtplus/officer/bind_officer";
        public static string zonalOffice = "api/dtplus/zonaloffice/get_zonal_office";
        public static string regionalOffice = "api/dtplus/regionaloffice/get_regional_office";
        public static string createHeadOffice = "/dtpwebapi/api/dtplus/hq/insert_hq";
        public static string updateHeadOffice = "/dtpwebapi/api/dtplus/hq/update_hq";
        public static string getLocationMapping = "api/dtplus/officer/get_officer_location_mapping";
        public static string insertOfficerLocationMapping = "api/dtplus/officer/insert_officer_location_mapping";
        public static string deleteOfficerLocationMapping = "api/dtplus/officer/delete_officer_location_mapping";
        public static string getOfficerDetailByLocation = "api/dtplus/officer/get_officer_detail_by_location";

        #endregion       

        #region Cards

        public static string GetStatusTypeUrl = "/dtpwebapi/api/dtplus/settings/get_entity_status_type";
        public static string SearchCardUrl = "/dtpwebapi/api/dtplus/card/search_manage_card";
        public static string GetCardDetailsUrl = "/dtpwebapi/api/dtplus/card/get_card_limit_features";
        public static string UpdateMobileUrl = "/dtpwebapi/api/dtplus/card/update_mobile_in_card";
        public static string UpdateServiceUrl = "/dtpwebapi/api/dtplus/card/update_service_on_card";
        public static string GetAllCardStatusUrl = "/dtpwebapi/api/dtplus/card/get_all_card_with_status";
        public static string UpdateCardStatusUrl = "/dtpwebapi/api/dtplus/card/update_card_status";
        public static string GetLimitTypeUrl = "/dtpwebapi/api/dtplus/card/get_ccms_limit_master";
        public static string GetCardLimitUrl = "/dtpwebapi/api/dtplus/card/get_card_limit";
        public static string UpdateCardLimitUrl = "/dtpwebapi/api/dtplus/card/update_card_limits";
        public static string SearchCcmsAllCardLimitUrl = "/dtpwebapi/api/dtplus/card/get_ccms_limit_for_all_cards";
        public static string UpdateCcmsAllCardLimitUrl = "/dtpwebapi/api/dtplus/card/update_ccms_limit_for_all_cards";
        public static string SearchCcmsIndividualCardLimitUrl = "/dtpwebapi/api/dtplus/card/get_ccms_limit";
        public static string UpdateCcmsIndividualCardLimitUrl = "/dtpwebapi/api/dtplus/card/update_ccms_limits";
        public static string ViewCardLimitsUrl = "/dtpwebapi/api/dtplus/card/view_card_limits";
        public static string MobileAddOrEditUrl = "/dtpwebapi/api/dtplus/card/update_mobile_and_fastag_no_in_card";
        public static string getValidateNewCardLists = "api/dtplus/card/bind_pending_customer_for_card_approval";
        public static string getCardDetailsForCardApproval = "api/dtplus/card/get_card_detail_for_card_approval";
        public static string approveRejectCard = "api/dtplus/card/approve_reject_card";
        public static string CardControlUrl = "/dtpwebapi/api/dtplus/customer/get_control_card_number";
        public static string SearchCardMappingUrl = "/dtpwebapi/api/dtplus/card/search_card_mapping_detail";
        public static string GetCardListUrl = "/dtpwebapi/api/dtplus/customer/get_card_list_from_customer_id";
        public static string GetCustMapDetailsUrl = "/dtpwebapi/api/dtplus/customer/get_customer_details_for_mapping_card_merchant";
        public static string GetMerchantForMapUrl = "/dtpwebapi/api/dtplus/customer/get_merchant_for_card_mapping";
        public static string SaveCustomerMerchantMapUrl = "/dtpwebapi/api/dtplus/customer/add_customer_card_merchant_mapping";
        public static string TerminalInstallationRequestUrl = "/dtpwebapi/api/dtplus/merchant/search_for_terminal_installation_request";
        public static string InsertInstallationRequestUrl = "/dtpwebapi/api/dtplus/merchant/insert_terminal_installation_request";
        public static string MappingAllowedCardsToMerchantUrl = "/dtpwebapi/api/dtplus/customer/get_mapping_user_cards_to_merchants";

        public static string GetTerminalInstallReqAppUrl = "/dtpwebapi/api/dtplus/merchant/get_terminal_installation_request_approval";
        public static string ApproveRejectTerminalUrl= "/dtpwebapi/api/dtplus/merchant/insert_terminal_installation_request_approval";
        #endregion

        #region Merchant
        public static string getMerchantType = "api/dtplus/merchant/get_merchant_type";
        public static string getOutletCategory = "api/dtplus/merchant/get_outlet_category";
        public static string getSbu = "api/dtplus/merchant/get_sbu";
        public static string insertMerchant = "api/dtplus/merchant/insert_merchant";
        public static string updateMerchant = "api/dtplus/merchant/update_merchant";
        public static string getMerchantByMerchantID = "api/dtplus/merchant/get_merchant_by_merchant_Id";
        public static string getMerchantApprovalList = "api/dtplus/merchant/get_merchant_approval_list";
        public static string approveRejectMerchant = "api/dtplus/merchant/approve_reject_merchant";
        public static string getRejectedMerchant = "api/dtplus/merchant/get_rejected_merchant";
        public static string getMerchantbyERPCode = "api/dtplus/merchant/get_merchant_by_erp_code";
        public static string searchforterminalinstallationrequestclose = "api/dtplus/merchant/search_for_terminal_installation_request_close";
        public static string getreasonlist = "api/dtplus/merchant/get_reason_list";
        public static string updateterminalinstallationrequestclose = "api/dtplus/merchant/update_terminal_installation_request_close";
        public static string viewterminalinstallationrequeststatus = "api/dtplus/merchant/view_terminal_installation_request_status";
        public static string getterminaldeinstallationrequest = "api/dtplus/merchant/get_terminal_de_installation_request";
        public static string updateterminaldeinstalrequest = "api/dtplus/merchant/update_terminal_de_instal_request";
        public static string getterminaldeinstallationrequestclose = "api/dtplus/merchant/get_terminal_de_installation_request_close";
        public static string terminaldeinstalupdaterequestclose = "api/dtplus/merchant/terminal_de_instal_update_request_close";
        public static string viewterminaldeinstallationrequeststatus = "api/dtplus/merchant/view_terminal_de_installation_request_status";
        public static string getproblematicdeinstalledtodeinstalled = "api/dtplus/merchant/get_problematic_de_installed_to_deinstalled";
        public static string insertproblematicdeinstalledtodeinstalled = "api/dtplus/merchant/insert_problematic_de_installed_to_deinstalled";

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
        public static string getCustomerByReferenceno = "/dtpwebapi/api/dtplus/customer/get_name_and_form_number_by_reference_no";
        public static string getCustomerRbeId = "/dtpwebapi/api/dtplus/customer/get_rbe_id";
        public static string searchCustRefUrl = "/dtpwebapi/api/dtplus/customer/get_name_and_form_number_by_reference_no";
        public static string GetProofTyleUrl = "/dtpwebapi/api/dtplus/settings/get_proof_type";
        public static string UploadKycUrl = "/dtpwebapi/api/dtplus/customer/upload_customer_kyc";
        public static string insertCustomerCard = "/dtpwebapi/api/dtplus/card/add_card";

        public static string bindPendingCustomerUrl = "/dtpwebapi/api/dtplus/customer/bind_pending_customer";
        public static string getFeeWaiverDetailsUrl = "/dtpwebapi/api/dtplus/customer/get_approve_fee_waiver_detail";
        public static string approveRejectFeeWaiverUrl = "/dtpwebapi/api/dtplus/customer/approve_reject_fee_waiver";

        public static string Viewonlineformstatus = "/dtpwebapi/api/dtplus/customer/view_online_form_status";
        public static string getCustomerPendingForApproval = "/dtpwebapi/api/dtplus/customer/bind_unverfied_customer";
        public static string getcustomerdetailsByFormNumber = "/dtpwebapi/api/dtplus/customer/get_unverfied_customer_detail_by_form_number";
        public static string approveOrrejectcustomer = "/dtpwebapi/api/dtplus/customer/approve_reject_customer";
        public static string checkformnumberDuplication = "/dtpwebapi/api/dtplus/customer/check_form_number";
        public static string checkemailidDuplication = "/dtpwebapi/api/dtplus/customer/check_email_id";
        public static string checkmobileNoDuplication = "/dtpwebapi/api/dtplus/customer/check_mobile_number";

        public static string getPendingCustUrl = "/dtpwebapi/api/dtplus/customer/get_pending_customer_detail_by_form_number";
        public static string checkPanNoDuplication = "/dtpwebapi/api/dtplus/customer/check_pan_card";
        public static string getCustomerDetails = "/dtpwebapi/api/dtplus/customer/get_customer_detail";
        #endregion

        #region Login

        public static string getLoginUrl = "/dtpwebapi/api/dtplus/login/get_login";
        #endregion

        #region Security

        public static string BindRbeDetailsUrl = "/dtpwebapi/api/dtplus/officer/bind_rbe_detail";
        public static string ViewRbeDataUrl = "/dtpwebapi/api/dtplus/officer/get_rbe_detail_by_user_name";
        public static string ApproveRejectRbeUserUrl = "/dtpwebapi/api/dtplus/officer/approve_reject_rbe";
        #endregion

        #region DriverCard
        public static string insertDriverCardRequest = "/dtpwebapi/api/dtplus/customer/insert_driver_card_request";
        public static string getAllUnAllocatedCardsForDriverCard = "/dtpwebapi/api/dtplus/merchant/get_all_un_allocated_cards_for_driver_card";
        public static string allocatedDriverCardToMerchant = "/dtpwebapi/api/dtplus/merchant/allocated_driver_card_to_merchant";
        public static string getAvailityDriverCard = "/dtpwebapi/api/dtplus/merchant/get_availity_driver_card";
        public static string insertDriverCardCustomer = "/dtpwebapi/api/dtplus/merchant/insert_driver_card_customer";
        public static string getcustomerNameByCustomerId = "/dtpwebapi/api/dtplus/customer/get_customer_name_by_customer_id";
        public static string ViewRequestDriverCard = "/dtpwebapi/api/dtplus/merchant/view_requested_driver_card";
        #endregion

        #region MyHpOTCCardCustomer
        public static string insertOTCCardRequest = "/dtpwebapi/api/dtplus/customer/insert_otc_card_request";
        public static string searchMerchantForCardCreation = "/dtpwebapi/api/dtplus/merchant/search_merchant_for_card_creation";
        public static string getAvailityOtcCard = "/dtpwebapi/api/dtplus/merchant/get_availity_otc_card";
        public static string insertOtcCustomer = "/dtpwebapi/api/dtplus/merchant/insert_otc_customer";
        public static string verifyMerchantByMerchantidAndRegionalid = "/dtpwebapi/api/dtplus/merchant/verify_merchant_by_merchant_id_and_regional_id";
        public static string getAllUnAllocatedCardsForOtcCard = "/dtpwebapi/api/dtplus/merchant/get_all_un_allocated_cards_for_otc_card";
        public static string allocatedOtcCardToMerchant = "/dtpwebapi/api/dtplus/merchant/allocated_otc_card_to_merchant";
        public static string ViewOTCCardRequest = "/dtpwebapi/api/dtplus/merchant/view_requested_otc_card";
        #endregion

        #region TatkalCardCustomer
        public static string insertTatkalCardRequest = "/dtpwebapi/api/dtplus/customer/insert_tatkal_card_request";
        #endregion

        #region "Terminal"
        public static string getTerminalDeInstallationRequestApproval = "api/dtplus/merchant/get_terminal_de_installation_request_approval";
        public static string updateTerminalDeInstallationRequestApproval = "api/dtplus/merchant/insert_terminal_de_installation_request_approval";
        public static string getTerminalDeInstallationRequestAuthorization = "api/dtplus/merchant/get_terminal_de_installation_request_authorization";
        public static string updateTerminalDeInstallationRequestAuthorization = "api/dtplus/merchant/insert_terminal_de_installation_request_authorization";
        #endregion
    }
}
