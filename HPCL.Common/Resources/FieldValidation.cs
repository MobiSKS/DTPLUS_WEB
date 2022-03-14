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

        public const string ValidName = @"^[a-zA-Z]{0,50}$";
        public const string ValidNameErrMsg = "Enter Valid Name";

        public const string ValidEmail = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
        public const string ValidEmailErrMsg = "Enter a valid Email ID";

        public const string ValidMobileNumber = @"^(?!(0))[0-9]{10}$";
        public const string ValidMobileNumberErrMsg = "Enter a Valid Mobile Number";

        public const string ValidCustomerId = @"^(?=(2))[0-9]{10}$";
        public const string ValidCustomerIdErrMsg = "start with 2 and contains 10 digits";

        public const string ValidMerchantId = @"^(?=(3))[0-9]{10}$";
        public const string ValidMerchantIdErrMsg = "start with 3 and contains 10 digits";

        public const string ValidAmount = @"^[1-9][0-9]*";
        public const string ValidAmountErrMsg = "Only digit allowed";

        public const string ValidDealerCode = @"^[a-zA-Z0-9]*$";
        public const string ValidDealerCodeErrMsg = "Enter a valid Dealer Code";

        public const string ValidCardNo = @"^(?!(0))[0-9]{16}$";
        public const string ValidCardNoErrMsg = "Enter a valid Card Number";
        #endregion

        #region "Not Empty Validation"

        public const string CustomerNotEmpty = "Customer ID should not be Empty";
        public const string MobNoNotEmpty = "Mobile Number should not be Empty";
        public const string MerchantNotEmpty = "Merchant ID should not be Empty";
        public const string AmountNotEmpty = "Amount should not be Empty";
        #endregion
    }
}
