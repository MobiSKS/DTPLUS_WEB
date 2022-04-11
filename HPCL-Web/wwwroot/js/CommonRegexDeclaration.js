var phoneno = /^[6789]\d{9}$/;
//var email = /[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$/; //Not checking if .com is present
var email = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
var number = /^\d+$/;
var pincode = /^[1-9][0-9]{5}$/;
var mappedMerchantID = /^(?=(3))[0-9]{10}$/;
var pancard = /[A-Z]{5}\d{4}[A-Z]{1}/;
var gstNumber = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;
var lpgCngSale = /^(?:[1-9][0-9]{0,4}(d{1,2})?|100000|100000.00)$/;
var erpCodeCheck = /^(?=(1))[0-9]{8}$/;
var nameCheck = /^[A-Za-z]{2,40}$/;
var noOfTerminalsCheck = /^[1-2]{1}$/;
var mappedterminalID = /^(?=(5))[0-9]{10}$/;
var mappedCustomerId = /^(?=(2))[0-9]{10}$/;
var noOfTerminalsCheck = /^[1-2]{1}$/;
var nameWithNumCheck = /^[A-Za-z0-9]{2,40}$/;
var nameCheckwithSpace = /^[A-Za-z ]{2,40}$/;
var nameCheckwithNumandSpace = /^[A-Za-z0-9 ]{2,40}$/;
var onlyNumbers = /^[0-9]{2,40}$/;
var highwayName = /^[a-z\d\-_\s]+$/i;
///^[A-Z0-9 _]*[A-Z0-9][A-Z0-9 _]*$/;
var noSpecialChars = /^[A-Za-z0-9 ]+$/; //Regex for Valid Characters i.e. Alphabets, Numbers and Space 
var atLeastOneAlphabet = /[a-zA-Z]/;   //To check if a string contains at least one letter 
var secretQuestion = /[0-9a-zA-Z /-]/;
var mappedDealerName = /^.*\D+.*$/;
var notOnlySpecialChars = /^[A-Za-z0-9]{1,50}$/;