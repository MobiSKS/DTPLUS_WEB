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
        //public static string getLocationZone = "api/dtplus/settings/get_zone";
        //public static string getLocationRegion = "api/dtplus/settings/get_region";
        public static string getLocationHq = "api/dtplus/hq/get_hq";
        public static string getState = "api/dtplus/state/get_state";
        public static string getDistrict = "api/dtplus/district/get_district";
        public static string validateUserName = "api/dtplus/officer/check_username";
        public static string insertOfficer = "api/dtplus/officer/insert_officer";
        public static string insertRbeOfficer = "api/dtplus/RBE/insert_rbe";
        public static string getSalesArea = "api/dtplus/settings/get_sales_area";
        public static string updateOfficer = "api/dtplus/officer/update_officer";
        public static string getOfficerDetails = "api/dtplus/officer/get_officer_detail";
        public static string deleteOfficer = "api/dtplus/officer/delete_officer";
        public static string bindOfficer = "api/dtplus/officer/bind_officer";
        public static string zonalOffice = "api/dtplus/zonaloffice/get_zonal_office";
        public static string regionalOffice = "api/dtplus/regionaloffice/get_regional_office";
        public static string createHeadOffice = "api/dtplus/hq/insert_hq";
        public static string updateHeadOffice = "api/dtplus/hq/update_hq";
        public static string getLocationMapping = "api/dtplus/officer/get_officer_location_mapping";
        public static string insertOfficerLocationMapping = "api/dtplus/officer/insert_officer_location_mapping";
        public static string deleteOfficerLocationMapping = "api/dtplus/officer/delete_officer_location_mapping";
        public static string getOfficerDetailByLocation = "api/dtplus/officer/get_officer_detail_by_location";

        #endregion       

        #region Cards

        public static string GetStatusTypeUrl = "api/dtplus/settings/get_entity_status_type";
        public static string SearchCardUrl = "api/dtplus/card/search_manage_card";
        public static string GetCardDetailsUrl = "api/dtplus/card/get_card_limit_features";
        public static string UpdateMobileUrl = "api/dtplus/card/update_mobile_in_card";
        public static string UpdateServiceUrl = "api/dtplus/card/update_service_on_card";
        public static string GetAllCardStatusUrl = "api/dtplus/card/get_all_card_with_status";
        public static string UpdateCardStatusUrl = "api/dtplus/card/update_card_status";
        public static string GetLimitTypeUrl = "api/dtplus/card/get_ccms_limit_master";
        public static string GetCardLimitUrl = "api/dtplus/card/get_card_limit";
        public static string UpdateCardLimitUrl = "api/dtplus/card/update_card_limits";
        public static string SearchCcmsAllCardLimitUrl = "api/dtplus/card/get_ccms_limit_for_all_cards";
        public static string UpdateCcmsAllCardLimitUrl = "api/dtplus/card/update_ccms_limit_for_all_cards";
        public static string SearchCcmsIndividualCardLimitUrl = "api/dtplus/card/get_ccms_limit";
        public static string UpdateCcmsIndividualCardLimitUrl = "api/dtplus/card/update_ccms_limits";
        public static string ViewCardLimitsUrl = "api/dtplus/card/view_card_limits";
        public static string MobileAddOrEditUrl = "api/dtplus/card/update_mobile_and_fastag_no_in_card";
        public static string getValidateNewCardLists = "api/dtplus/card/bind_pending_customer_for_card_approval";
        public static string getCardDetailsForCardApproval = "api/dtplus/card/get_card_detail_for_card_approval";
        public static string approveRejectCard = "api/dtplus/card/approve_reject_card";
        public static string CardControlUrl = "api/dtplus/customer/get_control_card_number";
        public static string SearchCardMappingUrl = "api/dtplus/card/search_card_mapping_detail";
        public static string GetCardListUrl = "api/dtplus/customer/get_card_list_from_customer_id";
        public static string GetCustMapDetailsUrl = "api/dtplus/customer/get_customer_details_for_mapping_card_merchant";
        public static string GetMerchantForMapUrl = "api/dtplus/customer/get_merchant_for_card_mapping";
        public static string SaveCustomerMerchantMapUrl = "api/dtplus/customer/add_customer_card_merchant_mapping";
        public static string TerminalInstallationRequestUrl = "api/dtplus/terminal/search_for_terminal_installation_request";
        public static string InsertInstallationRequestUrl = "api/dtplus/terminal/insert_terminal_installation_request";
        public static string MappingAllowedCardsToMerchantUrl = "api/dtplus/customer/get_mapping_user_cards_to_merchants";
        public static string GetTerminalInstallReqAppUrl = "api/dtplus/terminal/get_terminal_installation_request_approval";
        public static string ApproveRejectTerminalUrl= "api/dtplus/terminal/insert_terminal_installation_request_approval";
        public static string searchcardmappingdetailswithblankmobile = "api/dtplus/card/search_card_mapping_details_with_blank_mobile";
        public static string LimitUpdateForSingleRechargeCardsUrl = "api/dtplus/card/get_cards_for_limit_update_for_single_recharge";
        public static string LimitUpdateForSingleRechargeUrl = "api/dtplus/card/limit_update_for_single_recharge";
        public static string GetEmergencyAddOnCardUrl = "api/dtplus/card/get_detail_for_emergency_replacement_cards";
        public static string MapEmergencyAddOnCardUrl = "api/dtplus/card/emergency_replacement_cards";
        public static string getlistcreditcloselimittype = "api/dtplus/dealercredit/get_list_credit_close_limit_type";
        public static string GetHotlistReasonListUrl = "api/dtplus/hotlist/Get_Hotlist_Reason";
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
        public static string searchforterminalinstallationrequestclose = "api/dtplus/terminal/search_for_terminal_installation_request_close";
        public static string getreasonlist = "api/dtplus/terminal/get_reason_list";
        public static string updateterminalinstallationrequestclose = "api/dtplus/terminal/update_terminal_installation_request_close";
        public static string viewterminalinstallationrequeststatus = "api/dtplus/terminal/view_terminal_installation_request_status";
        public static string getterminaldeinstallationrequest = "api/dtplus/terminal/get_terminal_de_installation_request";
        public static string updateterminaldeinstalrequest = "api/dtplus/terminal/update_terminal_de_instal_request";
        public static string getterminaldeinstallationrequestclose = "api/dtplus/terminal/get_terminal_de_installation_request_close";
        public static string terminaldeinstalupdaterequestclose = "api/dtplus/terminal/terminal_de_instal_update_request_close";
        public static string viewterminaldeinstallationrequeststatus = "api/dtplus/terminal/view_terminal_de_installation_request_status";
        public static string getproblematicdeinstalledtodeinstalled = "api/dtplus/terminal/get_problematic_de_installed_to_deinstalled";
        public static string insertproblematicdeinstalledtodeinstalled = "api/dtplus/terminal/insert_problematic_de_installed_to_deinstalled";
        public static string searchMerchant = "api/dtplus/merchant/search_merchant";
        public static string searchTerminal = "api/dtplus/terminal/search_terminal";
        public static string getMerchantStatusList = "api/dtplus/merchant/get_merchant_status";
        public static string merchantErpReloadSaleEarningDetail = "api/dtplus/merchant/merchant_erp_reload_sale_earning_detail";
        public static string merchantReceivablePayableDetail = "api/dtplus/merchant/merchant_receivable_payable_detail";
        public static string validateErpCode = "api/dtplus/merchant/validate_merchant_erp_code";
        public static string validateMappedMerchantID = "api/dtplus/merchant/check_mapped_merchant_id";
        #endregion

        #region customer

        public static string getCustomerType = "api/dtplus/customer/get_customer_type";
        public static string getZonalOffice = "api/dtplus/zonaloffice/get_zonal_office";
        public static string getRegionalOffice = "api/dtplus/regionaloffice/get_regional_office";
        public static string getCustomerSubType = "api/dtplus/customer/get_customer_sub_type";
        public static string getTbentityName = "api/dtplus/customer/get_tbentity_name";
        public static string getTypeOfFleet = "api/dtplus/customer/get_type_of_fleet";
        public static string getSecretQuestion = "api/dtplus/customer/get_secret_question";
        public static string insertCustomer = "api/dtplus/customer/insert_customer";
        public static string getVehicleTpe = "api/dtplus/customer/get_vehicle_type";
        public static string uplaodCustomerKyc = "api/dtplus/customer/upload_customer_kyc";
        public static string getCustomerByReferenceno = "api/dtplus/customer/get_name_and_form_number_by_reference_no";
        public static string getNameAndFormNumberByReferenceNoForAddCard = "api/dtplus/customer/get_name_and_form_number_by_reference_no_for_add_card";

        public static string searchCustRefUrl = "api/dtplus/customer/get_name_and_form_number_by_reference_no";
        public static string GetProofTyleUrl = "api/dtplus/settings/get_proof_type";
        public static string UploadKycUrl = "api/dtplus/customer/upload_customer_kyc";
        public static string insertCustomerCard = "api/dtplus/card/add_card";

        public static string bindPendingCustomerUrl = "api/dtplus/customer/bind_pending_customer";
        public static string getFeeWaiverDetailsUrl = "api/dtplus/customer/get_approve_fee_waiver_detail";
        public static string approveRejectFeeWaiverUrl = "api/dtplus/customer/approve_reject_fee_waiver";

        public static string Viewonlineformstatus = "api/dtplus/customer/view_online_form_status";
        public static string getCustomerPendingForApproval = "api/dtplus/customer/bind_unverfied_customer";
        public static string getcustomerdetailsByFormNumber = "api/dtplus/customer/get_unverfied_customer_detail_by_form_number";
        public static string approveOrrejectcustomer = "api/dtplus/customer/approve_reject_customer";
        public static string checkformnumberDuplication = "api/dtplus/customer/check_form_number";
        public static string checkemailidDuplication = "api/dtplus/customer/check_email_id";
        public static string checkmobileNoDuplication = "api/dtplus/customer/check_mobile_number";

        public static string getPendingCustUrl = "api/dtplus/customer/get_pending_customer_detail_by_form_number";
        public static string checkPanNoDuplication = "api/dtplus/customer/check_pan_card";
        public static string getCustomerDetails = "api/dtplus/customer/get_customer_detail";
        public static string getCustomerBalanceInfo = "api/dtplus/customer/get_customer_balance_info";
        public static string getCustomerCardWiseBalances = "api/dtplus/customer/get_customer_card_wise_balances";
        public static string getCustomerByCustomerId = "api/dtplus/customer/get_customer_by_customer_id";
        public static string getccmsbalanceinfoforcustomerid = "api/dtplus/customer/get_ccms_balance_info_for_customer_id";
        public static string getFormNumber = "api/dtplus/customer/get_form_number";
        public static string checkPanCardByDistrictid = "api/dtplus/customer/check_pan_card_by_district_id";
        public static string gettransactionssummary = "api/dtplus/customer/get_transactions_summary";
        public static string updateCustomer = "api/dtplus/customer/update_customer";
        public static string checkPanCardByDistrictIdAndCustomerReferenceno = "api/dtplus/customer/check_pan_card_by_district_id_and_customer_reference_no";
        public static string checkVechileNo = "api/dtplus/card/check_vechile_no";
        public static string customerAddOnUser = "api/dtplus/customer/customer_add_on_user";
        public static string insertaggregatorcustomer = "api/dtplus/aggregatorcustomer/insert_aggregator_customer";
        public static string getaggregatorcustomer = "api/dtplus/aggregatorcustomer/get_aggregator_customer";
        public static string approverejectaggregatorcustomer = "api/dtplus/aggregatorcustomer/approve_reject_aggregator_customer";
        public static string getunverfiedaggregatorcustomerbyformnumber = "api/dtplus/aggregatorcustomer/get_unverfied_aggregator_customer_detail_by_form_number";
        public static string bindunverfiedaggregatorcustomer = "api/dtplus/aggregatorcustomer/bind_unverfied_aggregator_customer";
        public static string updateaggregatorcustomer = "api/dtplus/aggregatorcustomer/update_aggregator_customer";
        public static string uploadaggregatorcustomerkyc = "api/dtplus/aggregatorcustomer/upload_aggregator_customer_kyc";
        public static string getaggregatornameandformnumberbyreferenceno = "api/dtplus/aggregatorcustomer/get_aggregator_name_and_form_number_by_reference_no";
        public static string insertaggregatornormalfleetcustomer = "api/dtplus/aggregatorcustomer/insert_aggregator_normal_fleet_customer";
        public static string getaggregatorfleetcustomer = "api/dtplus/aggregatorcustomer/get_aggregator_customer";
        public static string uploadaggregatornormalfleetcustomerkyc = "api/dtplus/aggregatorcustomer/upload_aggregator_normal_fleet_customer_kyc";
        public static string getfleetcustomernameandformnumberbyreferenceno = "api/dtplus/aggregatorcustomer/get_aggregator_normal_fleet_customer_name_and_form_number_by_reference_no";
        public static string getunverfiedfleetcustomerbyformnumber = "api/dtplus/aggregatorcustomer/get_unverfied_aggregator_normal_fleet_customer_detail_by_form_number";
        public static string getUpdateContactPersonDetails = "api/dtplus/customer/get_update_contact_person_details";
        public static string updateContactPersonDetails = "api/dtplus/customer/update_contact_person_details";
        public static string aggregatornormalfleetcustomeraddcard = "api/dtplus/aggregatorcustomer/aggregator_normal_fleet_customer_add_card";
        public static string getCcmsBalAlertConfiguration = "api/dtplus/customer/get_ccms_bal_alert_configuration";
        public static string updateCcmsbalAlertConfiguration = "api/dtplus/customer/update_ccmsbal_alert_configuration";
        #endregion

        #region Login

        public static string getMenu = "api/dtplus/login/get_menu_details_for_user";
        public static string getLoginUrl = "api/dtplus/login/get_login";
        #endregion

        #region Security

        public static string BindRbeDetailsUrl = "api/dtplus/RBE/bind_rbe_detail";
        public static string ViewRbeDataUrl = "api/dtplus/RBE/get_rbe_detail_by_user_name";
        public static string ApproveRejectRbeUserUrl = "api/dtplus/RBE/approve_reject_rbe";
        #endregion

        #region DriverCard
        public static string insertDriverCardRequest = "api/dtplus/driver/insert_driver_card_request";
        public static string getAllUnAllocatedCardsForDriverCard = "api/dtplus/driver/get_all_un_allocated_cards_for_driver_card";
        public static string allocatedDriverCardToMerchant = "api/dtplus/driver/allocated_driver_card_to_merchant";
        public static string getAvailityDriverCard = "api/dtplus/driver/get_availity_driver_card";
        public static string insertDriverCardCustomer = "api/dtplus/driver/insert_driver_card_customer";
        public static string getcustomerNameByCustomerId = "api/dtplus/customer/get_customer_name_by_customer_id";
        public static string ViewRequestDriverCard = "api/dtplus/driver/view_requested_driver_card";
        public static string viewDriverCardMerchantAllocation = "api/dtplus/driver/view_driver_card_merchant_allocation";
        public static string getdrivercardallocationactivation = "api/dtplus/driver/get_driver_card_allocation_activation";
        public static string insertDealerWiseDriverCardRequest = "api/dtplus/driver/insert_dealer_wise_driver_card_request";
        #endregion

        #region MyHpOTCCardCustomer
        public static string insertOTCCardRequest = "api/dtplus/otc/insert_otc_card_request";
        public static string searchMerchantForCardCreation = "api/dtplus/merchant/search_merchant_for_card_creation";
        public static string getAvailityOtcCard = "api/dtplus/otc/get_availity_otc_card";
        public static string insertOtcCustomer = "api/dtplus/otc/insert_otc_customer";
        public static string verifyMerchantByMerchantidAndRegionalid = "api/dtplus/merchant/verify_merchant_by_merchant_id_and_regional_id";
        public static string getAllUnAllocatedCardsForOtcCard = "api/dtplus/otc/get_all_un_allocated_cards_for_otc_card";
        public static string allocatedOtcCardToMerchant = "api/dtplus/otc/allocated_otc_card_to_merchant";
        public static string ViewOTCCardRequest = "api/dtplus/otc/view_requested_otc_card";
        public static string viewOtcCardMerchantAllocation = "api/dtplus/otc/view_otc_card_merchant_allocation";
        public static string getotccardallocationactivation = "api/dtplus/otc/get_otc_card_allocation_activation";
        public static string insertDealerWiseOtcCardRequest = "api/dtplus/otc/insert_dealer_wise_otc_card_request";
        public static string checkMobileNo = "api/dtplus/card/check_mobile_no";
        #endregion

        #region TatkalCardCustomer
        public static string insertTatkalCardRequest = "api/dtplus/tatkal/insert_tatkal_card_request";
        public static string insertTatkalCardCustomer = "api/dtplus/tatkal/insert_tatkal_card_customer";
        public static string viewRequestedTatkalCard = "api/dtplus/tatkal/view_requested_tatkal_card";
        public static string maptatkalcardstotatkalcustomer = "api/dtplus/tatkal/map_tatkal_cards_to_tatkal_customer";
        public static string updatemaptatkalcardstotatkalcustomer = "api/dtplus/tatkal/update_map_tatkal_cards_to_tatkal_customer";
        public static string viewtatkalcards = "api/dtplus/tatkal/view_tatkal_cards";
        #endregion

        #region Terminal
        public static string getTerminalDeInstallationRequestApproval = "api/dtplus/terminal/get_terminal_de_installation_request_approval";
        public static string updateTerminalDeInstallationRequestApproval = "api/dtplus/terminal/insert_terminal_de_installation_request_approval";
        public static string getTerminalDeInstallationRequestAuthorization = "api/dtplus/terminal/get_terminal_de_installation_request_authorization";
        public static string updateTerminalDeInstallationRequestAuthorization = "api/dtplus/terminal/insert_terminal_de_installation_request_authorization";
        public static string ManageTerminalUrl = "api/dtplus/terminal/get_manage_terminal_detail";
        public static string getstatustypesforterminal = "api/dtplus/settings/get_status_types_for_terminal";
        #endregion

        #region AshokLeyLand

        public static string InsertAlDetailsUrl = "api/dtplus/ashokleyland/insert_al_dealer_enrollment";
        public static string UpdateAlDetailsUrl = "api/dtplus/ashokleyland/update_al_dealer_enrollment";
        public static string getDelerNameUrl = "api/dtplus/ashokleyland/get_al_dealer_detail";
        public static string checkDealerCode = "api/dtplus/ashokleyland/check_dealer_code";
        public static string insertDealerWiseAlOtcCardRequest = "api/dtplus/ashokleyland/insert_dealer_wise_al_otc_card_request";
        public static string getAvailityAlOTCCard = "api/dtplus/ashokleyland/get_availity_al_otc_card";
        public static string insertAlCustomer = "api/dtplus/ashokleyland/insert_al_customer";
        public static string viewAlOTCCardDealerAllocation = "api/dtplus/ashokleyland/view_al_otc_card_dealer_allocation";
        public static string getAladdonOTCCardMappingCustomerDetails = "api/dtplus/ashokleyland/get_al_addon_otc_card_mapping_customer_details";
        public static string alAddonOTCCard = "api/dtplus/ashokleyland/al_addon_otc_card";
        public static string getAlSalesExeEmpidAddonOtcCardMapping = "api/dtplus/ashokleyland/get_al_sales_exe_empid_addon_otc_card_mapping";
        public static string getAlCustomerDetail = "api/dtplus/ashokleyland/get_al_customer_detail";
        public static string updateAlCustomerDetail = "api/dtplus/ashokleyland/update_al_customer_detail";
        public static string getAlCustomerDetailForVerification = "api/dtplus/ashokleyland/get_al_customer_detail_for_verification";
        public static string updateAlCustomerStatus = "api/dtplus/ashokleyland/update_al_customer_status";
        #endregion

        #region Customer Financial

        public static string GetCardToCCMSTransferUrl = "api/dtplus/card/get_card_to_ccms_balance_transfer";
        public static string GetCCMSToCardTransferUrl = "api/dtplus/card/get_ccms_to_card_balance_transfer";
        public static string GetCardToCardTransferUrl = "api/dtplus/card/get_card_to_card_balance_transfer";
        public static string GetViewAccountStatementUrl = "api/dtplus/customer/view_account_statement_summary";
        public static string CCMSToCardsAmtTransferUrl = "api/dtplus/card/transfer_amount_ccms_to_card";
        public static string CardToCardsAmtTransferUrl = "api/dtplus/card/transfer_amount_card_to_card";
        public static string CardToCCMSsAmtTransferUrl = "api/dtplus/card/transfer_amount_card_to_ccms";

        #endregion

        #region Merchant Financial

        public static string GetUploadMerchantCautionLimitUrl = "api/dtplus/merchant/view_merchant_caution_limit";
        public static string GetSattlementDetailsUrl = "api/dtplus/merchant/merchant_settlement_detail";
        public static string GetBatchDetailsUrl = "api/dtplus/merchant/merchant_batch_detail";
        public static string GetTerminalDetailsUrl = "api/dtplus/terminal/terminal_detail";
        public static string GetTransactionDetailsUrl = "api/dtplus/merchant/merchant_transaction_detail";
        public static string GetTransationTypeUrl = "api/dtplus/settings/get_transaction_type";
        public static string getmerchantsalereloaddeltadetail = "api/dtplus/merchant/merchant_sale_reload_delta_detail";

        #endregion

        #region Locations

        public static string GetCountryRegionUrl = "api/dtplus/countryregion/get_country_region";
        public static string GetCityUrl = "api/dtplus/city/get_city";
        public static string DeleteCityUrl = "api/dtplus/city/delete_city";
        public static string DeleteZonalOfficeUrl = "api/dtplus/zonaloffice/delete_zonal_office";
        public static string DeleteRegionalOfficeUrl = "api/dtplus/regionaloffice/delete_regional_office";
        public static string DeleteCountryRegionUrl = "api/dtplus/countryregion/delete_country_region";
        public static string DeleteStateUrl = "api/dtplus/state/delete_state";
        public static string DeleteDistrictUrl = "api/dtplus/district/delete_district";

        #endregion


        #region Interfaces
        public static string getrecordtype = "api/dtplus/settings/get_record_type";
        public static string searchcustomerandcardform = "api/dtplus/customer/search_customer_and_card_form";
        #endregion


        #region Admin Hotlist
        public static string getactionlist = "api/dtplus/hotlist/get_action_list";
        public static string getentitytypelist = "api/dtplus/hotlist/get_entity_type_list";
        public static string getreasonlistforentities = "api/dtplus/hotlist/get_reason_list_for_entities";
        public static string updatehotlistorreactivate = "api/dtplus/hotlist/update_hotlist_or_reactivate";
        public static string gethotlistedorreactivateddetails = "api/dtplus/hotlist/get_hotlisted_or_reactivated_details";
        public static string gethotlistapproval = "api/dtplus/hotlist/get_hotlist_approval";
        public static string updatehotlistapproval = "api/dtplus/hotlist/update_hotlist_approval";

        #endregion

        #region RBE
        public static string GetMangeRbeMappingUrl = "api/dtplus/RBE/change_rbe_mapping";
        public static string GetRbeUserListUrl = "api/dtplus/RBE/manage_rbe_user";
        public static string getCustomerRbeId = "api/dtplus/RBE/get_rbe_id";
        public static string GetRbeDeviceIdResetRequest = "api/dtplus/RBE/get_rbe_deviceid_reset_request";
        public static string ChangeRbeMappingByUserNameUrl = "api/dtplus/RBE/request_to_change_rbe_mapping";
        public static string UserNameVerifyOtpUrl = "api/dtplus/RBE/validate_otp_rbe_mapping";
        public static string GetRbeMappingStatusUrl = "api/dtplus/RBE/get_rbe_mapping_status";
        public static string GetApproveChangedRbeMappingUrl = "api/dtplus/RBE/get_approve_changed_rbe_mapping";
        public static string ApproveRejectChangedRbeMappingUrl = "api/dtplus/RBE/approve_reject_changed_rbe_mapping";
        public static string RbeMobileChangeRequestUrl = "api/dtplus/RBE/get_rbe_mobile_change_request";
        public static string GetOtpMobileChangeReqUrl = "api/dtplus/RBE/send_otp_change_rbe_mobile";
        public static string VerifyOtpMobileChangeReqUrl = "api/dtplus/RBE/validate_otp_change_rbe_mobile";
        public static string ApproveChangeRbeMobileUrl = "api/dtplus/RBE/get_approve_change_rbe_mobile";
        public static string ApproveRejectChangedRbeMobileUrl = "api/dtplus/RBE/approve_reject_changed_rbe_mobile";
        public static string GetDeviceIdResetRequestUrl = "api/dtplus/RBE/get_rbe_deviceid_reset_request";
        public static string GetOtpRbeDeviceResetUrl = "api/dtplus/RBE/send_otp_reset_rbe_device";
        public static string ValidateOtpRbeDeviceResetUrl = "api/dtplus/RBE/validate_otp_reset_rbe_device";
        public static string GetApproveDeviceIdResetRequestUrl = "api/dtplus/RBE/get_approve_change_rbe_device_reset";
        public static string GetApproveRejectDeviceResetUrl = "api/dtplus/RBE/approve_reject_changed_rbe_device_reset";
        #endregion

        #region Application Form Data Entry
        public static string GetCustomerNameUrl = "api/dtplus/customer/get_name_and_formnumber_by_customerid";
        public static string CheckAddOnFormUrl = "api/dtplus/card/check_addon_formnumber";
        public static string addAddonCard = "api/dtplus/card/add_addon_card";
        public static string checkCardIdentifierNo = "api/dtplus/card/check_card_identifier_no";
        #endregion

        #region "DTP Support"
        public static string GetBlockUnblockCustomerCcmsAccountByCustomeridUrl = "api/dtplus/dtp/get_block_unblock_customer_ccms_account_by_customerid";
        public static string BlockUnblockCustomerCcmsAccountUrl = "api/dtplus/dtp/block_unblock_customer_ccms_account";
        public static string GetCardBalanceTransfer = "api/dtplus/dtp/card_balance_transfer";
        public static string GetEntity = "api/dtplus/settings/get_entity";
        public static string GetEntityFieldByEntityTypeId = "api/dtplus/dtp/get_entity_field_by_entity_type_id";
        public static string GetEntityOldFieldValue = "api/dtplus/dtp/get_entity_old_field_value";
        public static string UpdateGeneralEntityField = "api/dtplus/dtp/update_general_entity_field";
        public static string GetTeamMapping = "api/dtplus/dtp/get_team_mapping ";
        public static string InsertTeamMapping = "api/dtplus/dtp/insert_team_mapping ";
        public static string DeleteTeamMapping = "api/dtplus/dtp/delete_team_mapping ";
        public static string updateteammapping = "api/dtplus/dtp/update_team_mapping ";
        public static string getDetailForUserUnblockByCustomeridOrUsername = "api/dtplus/dtp/get_detail_for_user_unblock_by_customerid_or_username";
        public static string userUnblock = "api/dtplus/dtp/user_unblock";
        #endregion

        #region TMS
        public static string GetCustomerSearchDetails = "api/dtplus/TMS/bind_enroll_transport_management_system";
        public static string EnrollTransportManagementSystem = "api/dtplus/TMS/get_enroll_transport_management_system";
        public static string GetDetailsForCustomerUpdate = "api/dtplus/TMS/get_details_for_customer_update";
        public static string UpdateCustomerAddress = "api/dtplus/TMS/update_customer_address";
        public static string GetVehicleEnrollmentStatus = "api/dtplus/TMS/get_vehicle_enrollment_status";
        public static string GetVehicleEnrollmentDetail = "api/dtplus/TMS/Get_Vehicle_Enrollment_Detail";
        public static string InsertVehicleEnrollmentStatus = "api/dtplus/TMS/insert_vehicle_enrollment_status";
        public static string GetManageEnrollments = "api/dtplus/TMS/get_manage_enrollments";
        public static string UpdateTmsEnrollmentTmsStatus = "api/dtplus/TMS/update_tms_enrollment_tms_status";
        public static string GetTransportManagementSystemUrl = "api/dtplus/TMS/get_transport_management_system_url";
        public static string GetCustomerDetailForEnrollmentApproval = "api/dtplus/TMS/get_customer_detail_for_enrollment_approval";
        public static string UpdateCustomerDetailForEnrollmentApproval = "api/dtplus/TMS/update_customer_detail_for_enrollment_approval";
        public static string GetTmsEnrollmentStatus = "api/dtplus/TMS/get_tms_enrollment_status";
        #endregion

        #region "Customer Requests"
        public static string GetSmsAlertForMultipleMobileDetailUrl = "api/dtplus/configure/get_sms_alert_for_multiple_mobile_detail";
        public static string UpdateSmsAlertForMultipleMobileDetailUrl = "api/dtplus/configure/update_sms_alert_for_multiple_mobiledetail";
        public static string DeleteSmsAlertForMultipleMobileDetailUrl = "api/dtplus/configure/delete_sms_alert_for_multiple_mobiledetail";
        public static string GetCardRenwalRequestUrl = "api/dtplus/card/get_card_renewal_request_detail";
        public static string UpdateCardRenwalRequestUrl = "api/dtplus/card/update_card_renewal_request";
        public static string GetConfigureSmsAlertsUrl = "api/dtplus/configure/get_configure_sms_alerts_details_by_customerid";
        public static string UpdateConfigureSmsAlertsUrl = "api/dtplus/configure/update_configure_sms_alerts";
        public static string UpdateDndSmsAlertsConfigureUrl = "api/dtplus/configure/update_dnd_configure_sms_alerts";
        public static string GetHotlistCardsPermanentlyUrl = "api/dtplus/hotlist/get_hotlist_cards_details";
        public static string UpdatePermanentlyHotlistCardsUrl = "api/dtplus/hotlist/Update_Permanently_Hotlist_Cards";
        #endregion

        #region Configure
        public static string GetSmsAlertsToIndividualCardUsersDetails = "api/dtplus/configure/get_sms_alerts_to_individual_card_users_details";
        public static string DeleteSmsAlertsToIndividualCardUsers = "api/dtplus/configure/delete_sms_alerts_to_individual_card_users";
        public static string UpdateSmsAlertsToIndividualCardUsers = "api/dtplus/configure/update_sms_alerts_to_individual_card_users";
        #endregion

        #region Dealer
        public static string getdealercreditmappingdetails = "api/dtplus/dealercredit/get_dealer_credit_mapping_details";
        public static string insertmapdealerforcreditsale = "api/dtplus/dealercredit/insert_map_dealer_for_credit_sale";
        public static string customermerchantmappingenabledisable = "api/dtplus/dealercredit/customer_merchant_mapping_enable_disable";
        public static string updatedealercreditmapping = "api/dtplus/dealercredit/update_dealer_credit_mapping";
        #endregion
    }
}
