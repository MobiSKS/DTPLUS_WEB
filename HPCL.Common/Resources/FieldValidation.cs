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
        public const string ValidAddressErrMsg = "";

        public const string ValidCity = @"^{0,30}$";
        public const string ValidCityErrMsg = "";

        public const string ValidPinCode = @"^(?!(0))[0-9]{6}$";
        public const string ValidPinCodeErrMsg = "";

        public const string ValidName = @"^[a-zA-Z]{0,50}$";
        public const string ValidNameErrMsg = "Enter Valid Name";

        public const string ValidEmail = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
        public const string ValidEmailErrMsg = "";

        public const string ValidMobileNumber = @"^[0-9]{10}$";
        public const string ValidMobileNumberErrMsg = "Enter a Valid Mobile Number";

        #endregion

        #region "Not Empty Validation"

        public const string CustomerNotEmpty = "Customer ID should not be Empty";
        public const string MobNoNotEmpty = "Mobile Number should not be Empty";

        #endregion
    }
}
