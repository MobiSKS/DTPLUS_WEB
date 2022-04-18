namespace HPCL.Common.Resources
{
    public static class FieldValidation
    {
        #region "Regex Validation"

        public const string IndividualOrgName = @"^[a-zA-Z]{0,80}$";
        public const string IndividualOrgNameErrMsg = "Enter Valid Individual/Org. Name";

        public const string NameOnCard = @"^{0,22}$";
        public const string NameOnCardErrMsg = "Enter Valid Name on Card";

        public const string IncomeTaxPan = @"^\S{10}$";
        public const string IncomeTaxPanErrMsg = "";

        public const string ValidAddress = @"^{0,50}$";
        public const string ValidAddressErrMsg = "Address allowed maximum 50 character";

        public const string ValidCity = @"^{0,30}$";
        public const string ValidCityErrMsg = "City Name allowed maximum 30 character";

        public const string ValidPinCode = @"^(?!(0))[0-9]{6}$";
        public const string ValidPinCodeErrMsg = "Enter a valid Pin";

        public const string ValidFirstName = @"^[a-zA-Z]{0,50}$";
        public const string ValidFirstNameErrMsg = "Enter Valid First Name";

        public const string ValidUserName = @"^[a-zA-Z]{0,50}$";
        public const string ValidUserNameErrMsg = "Enter Valid First Name";

        public const string ValidEmail = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
        public const string ValidEmailErrMsg = "Enter a valid Email ID";

        public const string ValidMobileNumber = @"^(?!(0))[0-9]{10}$";
        public const string ValidMobileNumberErrMsg = "Enter a Valid Mobile Number";

        public const string ValidCustomerId = @"^(?=(2))[0-9]{10}$";
        public const string ValidCustomerIdErrMsg = "Invalid Customer ID";

        public const string ValidMerchantId = @"^(?=(3))[0-9]{10}$";
        public const string ValidMerchantIdErrMsg = "Invalid Merchant ID";

        public const string ValidAmount = @"^[1-9][0-9]*";
        public const string ValidAmountErrMsg = "Only digit allowed";

        public const string ValidDealerCode = @"^[a-zA-Z0-9]*$";
        public const string ValidDealerCodeErrMsg = "Enter a valid Dealer Code";

        public const string ValidCardNo = @"^(?!(0))[0-9]{16}$";
        public const string ValidCardNoErrMsg = "Enter a valid Card Number";

        public const string ValidFormNo = @"^(?!(0))[0-9]{10}$";
        public const string ValidFormNoErrMsg = "Enter valid Form Number";

        public const string ValidTerminalId = @"^(?=(5))[0-9]{10}$";
        public const string ValidTerminalIdErrMsg = "Invalid Terminal ID";

        public const string ValidCustomerReferenceNo = @"^(?!(0))[0-9]{10}$";
        public const string ValidCustomerReferenceNoErrMsg = "Please enter 10 digits Customer Reference No.";

        public const string ValidVachileNumber = @"^[A-Z]{2}[0-9]{2}[A-Z]{1,2}[0-9]{4}$";
        public const string ValidVachileNumberErrMsg = "Invalid Vachile Number";

        public const string ValidDocumentNumber = @"^[a-zA-Z0-9]{6,30}$";
        public const string ValidDocumentNumberErrMsg = "Invalid Document Number";
        #endregion

        #region "Not Empty Validation"

        public const string CustomerNotEmpty = "Customer ID should not be Empty";
        public const string MobNoNotEmpty = "Mobile Number should not be Empty";
        public const string MerchantNotEmpty = "Merchant ID should not be Empty";
        public const string AmountNotEmpty = "Amount should not be Empty";
        public const string TerminalNotEmpty = "Terminal ID should not be Empty";

        #endregion
    }
}
