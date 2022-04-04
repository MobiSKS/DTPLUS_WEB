//// JavaScript Document



$(document).on("ajaxStart", function () {
    $("#loader").fadeIn();
})

$(document).on("ajaxStop", function () {
    $("#loader").fadeOut();
})

$(document).on("ajaxError", function () {
   // alert('test');
    setTimeout(function () {
        $("#loader").fadeOut();
    }, 100)
    
})

document.addEventListener("contextmenu", (event)=> event.preventDefault())
$(document).ready(function(){
    
    var userinput = document.querySelector(".searchUserInput");
    var searchFileds = document.querySelector(".searchFileds");
    if (userinput) {
        userinput.addEventListener("mouseenter", function(){			
            searchFileds.style.display ="block";
        })		
        userinput.addEventListener("mouseleave", function(){
            setTimeout(function(){
                searchFileds.style.display ="none";
            },1000)
        })
    }

$('input[type=password], input.captcha').bind('copy paste', function (e) {
    e.preventDefault();
});

$('.drawermenu').drawermenu({
    speed: 300,
    position: 'left'
});


/*if ($(window).outerWidth() < 991) {
    $('#sidebar').slideReveal({
        trigger: $("#trigger"),
        push: false,
        top: 0,
        width: 225,
        overlay: false
    });
}*/

//if ($(window).outerWidth() < 991) {
/*$('#sidebar_inner').slideReveal({
    trigger: $("#trigger_inner"),
    push: false,
    top: 0,
    width: 225,
    overlay: false
});*/
//}

})

if (localStorage.getItem("showregAddress")) {
var addressTab = document.getElementById("address-tab");
if (addressTab) {
    addressTab.click();
    addressTab.classList.remove("disable");
}

}

if (localStorage.getItem("keyOfficial")) {
document.getElementById("address-tab").classList.remove("disable");
document.getElementById("officialDetails-tab").click();
document.getElementById("officialDetails-tab").classList.remove("disable");
}

if (localStorage.getItem("cardDetails")) {
var cardDetailsTab = document.getElementById("cardDetails-tab");
if (cardDetailsTab) {
    cardDetailsTab.click();
    cardDetailsTab.classList.remove("disable");
}
document.getElementById("address-tab").classList.remove("disable");
document.getElementById("officialDetails-tab").classList.remove("disable");
document.getElementById("cardDetails-tab").classList.remove("disable");
document.getElementById("uploadDocuments-tab").classList.remove("disable");
}

function showregAddress() {
    debugger;
    //	if(localStorage.getItem("showregAddress")) {
    //		document.getElementById("address-tab").click();
    //		document.getElementById("address-tab").classList.remove("disable");
    //	}
    //	else {	
    var ret = false;

    //console.log(ret);

    if (document.applicationForm.FormNumber.value.trim() == "") {
        document.getElementById("formNumber_error").innerHTML = "Form Number field cannot be left blank";
        return ret;
    }
    else {

        if (document.getElementById("FormNumber").value.substring(0, 1) == "0") {
            document.getElementById("formNumber_error").innerHTML = "Invalid Form Number. Form Number limit Min-Max 10 digits";
            return ret;
        }

        if (document.applicationForm.FormNumber.value.length < 10) {
            document.getElementById("formNumber_error").innerHTML = "Invalid Form Number. Form Number limit Min-Max 10 digits";
            return ret;
        }
        else {
            document.getElementById("formNumber_error").innerHTML = "";
        }
    }

    if (document.applicationForm.CustomerTypeID.value == "0") {
        document.getElementById("customerType_error").innerHTML = "Customer Type field cannot be left blank";
        return ret;
    }
    else {
        document.getElementById("customerType_error").innerHTML = "";
    }

    if (document.applicationForm.CustomerSubTypeID.value == "0" || document.applicationForm.CustomerSubTypeID.value == "") {
        document.getElementById("customerSubType_error").innerHTML = "Customer Sub Type field cannot be left blank";
        return ret;
    }
    else {
        document.getElementById("customerSubType_error").innerHTML = "";
    }

    if (document.applicationForm.CustomerDateOfApplication.value == "") {
        document.getElementById("applicationDate_error").innerHTML = "Date of Application field cannot be left blank";
        return ret;
    }
    else {
        document.getElementById("applicationDate_error").innerHTML = "";
    }


    if (document.applicationForm.CustomerZonalOfficeID.value == "0") {
        document.getElementById("zonalOffice_error").innerHTML = "Zonal Office field cannot be left blank";
        return ret;
    }
    else {
        document.getElementById("zonalOffice_error").innerHTML = "";
    }

    if (document.applicationForm.CustomerRegionID.value == "0") {
        document.getElementById("regionalOffice_error").innerHTML = "Regional Office field cannot be left blank";
        return ret;
    }
    else {
        document.getElementById("regionalOffice_error").innerHTML = "";
    }

    if (document.applicationForm.CustomerSalesAreaID.value == "0") {
        document.getElementById("salesArea_error").innerHTML = "Sales Area field cannot be left blank";
        return ret;
    }
    else {
        document.getElementById("salesArea_error").innerHTML = "";
    }

    if (document.applicationForm.TypeOfCustomerID.value == "0") {
        document.getElementById("typeOfCustomer_error").innerHTML = "Type Of Customer field cannot be left blank";
        return ret;
    }
    else {
        document.getElementById("typeOfCustomer_error").innerHTML = "";
    }

    if (document.applicationForm.TierOfCustomerID.value == "0") {
        document.getElementById("tierOfCustomer_error").innerHTML = "Tier Of Customer field cannot be left blank";
        return ret;
    }
    else {
        document.getElementById("tierOfCustomer_error").innerHTML = "";
    }

    if (document.applicationForm.IndividualOrgNameTitle.value == "-1") {
        document.getElementById("salutaion_error").innerHTML = "Select Title";
        return ret;
    }
    else {
        document.getElementById("individualName_error").innerHTML = "";
    }


    if (document.applicationForm.IndividualOrgName.value.trim() == "") {
        document.getElementById("individualName_error").innerHTML = "Individual/Org. Name is required";
        return ret;
    }
    else {

        if (!isNaN(document.applicationForm.IndividualOrgName.value.trim())) {
            document.getElementById("individualName_error").innerHTML = "Invalid Individual/Org. Name";
            return ret;
        }
        else {
            document.getElementById("individualName_error").innerHTML = "";
        }

        if (!document.applicationForm.IndividualOrgName.value.match(nameWithSpaceCheck)) {
            document.getElementById("individualName_error").innerHTML = "Invalid Individual/Org. Name";
            return ret;
        }
        else {
            document.getElementById("individualName_error").innerHTML = "";
        }
    }
        

    if (document.applicationForm.CustomerNameOnCard.value.trim() == "") {
        document.getElementById("nameOnCard_error").innerHTML = "Name on Card field cannot be left blank";
        return ret;
    }
    else {
        if (!isNaN(document.applicationForm.CustomerNameOnCard.value.trim())) {
            document.getElementById("nameOnCard_error").innerHTML = "Invalid Name on Card";
            return ret;
        }
        else {
            document.getElementById("nameOnCard_error").innerHTML = "";
        }

        if (!document.applicationForm.CustomerNameOnCard.value.match(nameWithSpaceCheck)) {
            document.getElementById("nameOnCard_error").innerHTML = "Invalid Name on Card";
            return ret;
        }
        else {
            document.getElementById("nameOnCard_error").innerHTML = "";
        }
    }
    

    if (document.applicationForm.CustomerTbentityID.value == "0") {
        document.getElementById("typeOfbusiness_error").innerHTML = "Select Type of Business Entity";
        return ret;
    }
    else {
        document.getElementById("typeOfbusiness_error").innerHTML = "";
    }

    if (document.applicationForm.CustomerResidenceStatus.value == "-1") {
        document.getElementById("residenceStatus_error").innerHTML = "Residential Status field cannot be left blank";
        return ret;
    }
    else {
        document.getElementById("residenceStatus_error").innerHTML = "";
    }



    var formNumber = document.getElementById("FormNumber").value.trim();
    if (formNumber == "") {
        document.getElementById("formNumber_error").innerHTML = "Form Number field cannot be left blank";
        return ret;
    }
    else {
        document.getElementById("formNumber_error").innerHTML = "";
    }

    var fromNumberUsed = false;



    if (localStorage.getItem("FORMNOALREADYUSED") == 0) {
        document.getElementById("formNumber_error").innerHTML = "Form Number already exists";
        return ret;
    }
    else {
        document.getElementById("formNumber_error").innerHTML = "";
    }


    var TypeofBusinessEntityId = document.applicationForm.CustomerTbentityID.value;

    if (TypeofBusinessEntityId != 10) {

        if (document.applicationForm.CustomerIncomeTaxPan.value.trim() == "") {
            document.getElementById("incomeTaxPan_error").innerHTML = "Income Tax PAN field cannot be left blank";
            return ret;
        }
        else {
            document.getElementById("incomeTaxPan_error").innerHTML = "";
        }

        if (document.applicationForm.CustomerIncomeTaxPan.value.length < 10) {
            document.getElementById("incomeTaxPan_error").innerHTML = "Income Tax PAN length must be 10 digit";
            return ret;
        }
        else {
            document.getElementById("incomeTaxPan_error").innerHTML = "";
        }
    }
    else {
        if (document.applicationForm.CustomerIncomeTaxPan.value.trim() == "") {
            document.getElementById("incomeTaxPan_error").innerHTML = "Govt. Dept. Identification Details required";
            return ret;
        }
        else {
            document.getElementById("incomeTaxPan_error").innerHTML = "";
        }
    }
       

    /*else {
        y = document.applicationForm.mobileno.value;
        if((y.charAt(0)!="9") && (y.charAt(0)!="8") && (y.charAt(0)!="7") && (y.charAt(0)!="6"))
        {
            document.getElementById("phone_error").innerHTML="Mobile Number should start with 6, 7, 8, 9";
            return false
        }
        else if (y.length<10)
        {
            document.getElementById("phone_error").innerHTML="Invalid phone number (e.g.: 9999990000)";
            return false;
        }
        
            else {
                document.getElementById("phone_error").innerHTML="";
            }
    }*/





    /*if (document.applicationForm.pincode.value == "") {
       document.getElementById("picode_error").innerHTML="This information is required";
       return (false);
    } 
    else {
       var pin = document.applicationForm.pincode.value;
        if (pin.length<6)
       {
           document.getElementById("picode_error").innerHTML="Invalid Pincode, Must be six digits";
           return false;
       }
       else {
               document.getElementById("picode_error").innerHTML="";
           }
    }*/


    //var TypeofBusinessEntityId  = $('#CustomerTbentityID').va();

    var today = new Date();
    today.setHours(0, 0, 0, 0);

    const myArray = document.getElementById("CustomerDateOfApplication").value.split("-");

    var varDate = new Date(myArray[2], (myArray[1]-1), myArray[0], 0, 0, 0, 0);
    console.log(today);
    console.log(varDate);
    if (varDate > today) {
        document.getElementById("applicationDate_error").innerHTML = "Date of Application cannot be a future date";
        return ret;
    }


    if (TypeofBusinessEntityId != 10) {

        if (document.applicationForm.CustomerIncomeTaxPan.value == "") {
            document.getElementById("incomeTaxPan_error").innerHTML = "Income Tax PAN field cannot be left blank";
            return ret;
        }

        if (localStorage.getItem("PANNOALREADYUSED") == 0) {
            document.getElementById("incomeTaxPan_error").innerHTML = "PAN Number already exists";
            return ret;
        }
        else {
            document.getElementById("incomeTaxPan_error").innerHTML = "";
        }

        //solo Propritorship 4th Char Pan should be 'P'
        if (TypeofBusinessEntityId == 2) {
            var panno = document.applicationForm.CustomerIncomeTaxPan.value;

            let forthdigitPan = panno.substr(3, 1);
            console.log(forthdigitPan);
            if (forthdigitPan != 'P') {
                console.log('Inside');
                document.getElementById("incomeTaxPan_error").innerHTML = "Invalid Income Tax PAN";
                return ret;
            }
        }

        if (document.applicationForm.CustomerIncomeTaxPan.value == "") {
            document.getElementById("incomeTaxPan_error").innerHTML = "Income Tax PAN field cannot be left blank";
            return ret;
        }
        else {
            //    var result = false;
            var result = true;
            //    var panno = $('#CustomerIncomeTaxPan').val();
            //    var OrgName = $('#IndividualOrgName').val();
            //    var correctPANName = '';

            //    $.ajax({
            //        type: 'POST',  // http method
            //        url: "PANValidation/Customer",
            //        data: { PANNumber: document.applicationForm.CustomerIncomeTaxPan.value },  // data to submit
            //        dataType: "json",
            //        success: function (data, status, xhr) {
            //            //debugger;
            //            var jsonData = JSON.parse(data);
            //            if (status == 'success' && jsonData['status-code'] == '101') {
            //                result = true;

            //                console.log(jsonData);
            //                document.getElementById("address-tab").click();
            //                document.getElementById("address-tab").classList.remove("disable");
            //                //document.applicationForm.IndividualOrgName.value = jsonData["result"]["name"];
            //                correctPANName = jsonData["result"]["name"];
            //                if (OrgName == correctPANName) {
            //                    localStorage.setItem("showregAddress", true)
            //                }
            //                else {
            //                    result = false;
            //                    document.getElementById("incomeTaxPan_error").innerHTML = "Your pan card name not match with pan card name";
            //                }
            //            }
            //            else {
            //                document.getElementById("incomeTaxPan_error").innerHTML = "Invalid PAN Number";
            //            }
            //        },
            //        error: function (jqXhr, textStatus, errorMessage) {
            //            document.getElementById("incomeTaxPan_error").innerHTML = "Invalid PAN Number";
            //        }
            //    });

        }
        ret = result;
    }
    else {
        if (document.applicationForm.CustomerIncomeTaxPan.value == "") {
            document.getElementById("incomeTaxPan_error").innerHTML = "Govt. Dept. Identification Details required";
            return ret;
        }
        else {
            ret = true;
        }
    }

    if (localStorage.getItem("DUPLICATEPANUSED") == 0) {
        if (document.getElementById("PanCardRemarks").value.trim() == "") {
            document.getElementById("PanCardRemarks_error").innerHTML = "Pan Card Remarks field cannot be left blank";
            return (false);
        }
        else {
            document.getElementById("PanCardRemarks_error").innerHTML = "";
        }
    }
    else {
        document.getElementById("PanCardRemarks_error").innerHTML = "";
    }

    
    if (ret == false)
        return ret;

    document.getElementById("address-tab").click();
    document.getElementById("address-tab").classList.remove("disable");
    localStorage.setItem("showregAddress", true)


    /*if (!document.applicationForm.tc.checked) {
        //toast("true");
        document.getElementById("tc_error").innerHTML="Must Agree to Terms and Conditions";
        return (false);
    } 
    else {
        // toast("false");
            document.getElementById("tc_error").innerHTML="";
        }*/

    //document.applicationForm.submit();
    //$('#address-tab').tab('show');
    return ret;
    //}
}

function showBasicInfo() {
document.getElementById("basicInfo-tab").click();
}
function showregAddressInfo() {
document.getElementById("address-tab").click();
}
function showkeyOfficialInfo() {
document.getElementById("officialDetails-tab").click();
}
function showCardInfo() {
document.getElementById("cardDetails-tab").click();
}
function showUploadDocument() {
document.getElementById("uploadDocuments-tab").click();
}

var sameAscheck = document.getElementById("sameAddressCheck");
var sameas = false;
if (sameAscheck) {
sameAscheck.addEventListener("change", function () {
    var permanent_add = document.querySelector("#permanent_add");

    if (document.getElementById("sameAddressCheck").checked == true) {

        permanent_add.querySelectorAll("input, select").forEach(function (i) {
            i.setAttribute("disabled", true);
        })
        sameas = true

    }
    else {
        permanent_add.querySelectorAll("input, select").forEach(function (i) {
            i.removeAttribute("disabled", true);
        })
        sameas = false
    }
})
}
function showOfficialDetails() {

    debugger;

    if (document.applicationForm.CommunicationAddress1.value.trim() == "") {
        document.getElementById("comm_address1_error").innerHTML = "Address field cannot be left blank";
        return false;
    }
    else {
        if (!document.getElementById("CommunicationAddress1").value.match(atLeastOneAlphabet)) {
            document.getElementById("comm_address1_error").innerHTML = "Invalid Address";
            return false;
        }
        else {
            document.getElementById("comm_address1_error").innerHTML = "";
        }
    }

    if (document.applicationForm.CommunicationAddress2.value.trim() == "") {
        document.getElementById("comm_address2_error").innerHTML = "Address field cannot be left blank";
        return false;
    }
    else {
        if (!document.getElementById("CommunicationAddress2").value.match(atLeastOneAlphabet)) {
            document.getElementById("comm_address2_error").innerHTML = "Invalid Address";
            return false;
        }
        else {
            document.getElementById("comm_address2_error").innerHTML = "";
        }
    }

    if (document.applicationForm.CommunicationAddress3.value.trim() != "")
    {
        if (!document.getElementById("CommunicationAddress3").value.match(atLeastOneAlphabet))
        {
            document.getElementById("comm_address3_error").innerHTML = "Invalid Address";
            return false;
        }
        else
        {
            document.getElementById("comm_address3_error").innerHTML = "";
        }
    }
    else
    {
        document.getElementById("comm_address3_error").innerHTML = "";
    }

    if (document.getElementById("CommunicationLocation").value.trim() != "")
    {
        if (!document.getElementById("CommunicationLocation").value.match(atLeastOneAlphabet))
        {
            document.getElementById("comm_location_error").innerHTML = "Invalid Location";
            return false;
        }
        else
        {
            document.getElementById("comm_location_error").innerHTML = "";
        }
    }
    else
    {
        document.getElementById("comm_location_error").innerHTML = "";
    }

    if (document.applicationForm.CommunicationCity.value.trim() == "") {
        document.getElementById("comm_city_error").innerHTML = "City field cannot be left blank";
        return false;
    }
    else {
        if (!document.getElementById("CommunicationCity").value.match(atLeastOneAlphabet)) {
            document.getElementById("comm_city_error").innerHTML = "Invalid City";
            return false;
        }
        else {
            document.getElementById("comm_city_error").innerHTML = "";
        }
    }

    if (document.applicationForm.CommunicationPinCode.value.trim() == "") {
        document.getElementById("comm_pincode_error").innerHTML = "Pin code field cannot be left blank";
        return (false);
    }
    else {
        if (document.applicationForm.CommunicationPinCode.value.substring(0, 1) == "0") {
            document.getElementById("comm_pincode_error").innerHTML = "Invalid Pin code. Min-Max 6 digits";
            return false;
        }
        else {
            document.getElementById("comm_pincode_error").innerHTML = "";
        }

        if (document.applicationForm.CommunicationPinCode.value.length < 6) {
            document.getElementById("comm_pincode_error").innerHTML = "Invalid Pin code. Min-Max 6 digits";
            return false;
        }
        else {
            document.getElementById("comm_pincode_error").innerHTML = "";
        }

        if (document.applicationForm.CommunicationPinCode.value.match(pincode)) {
            document.getElementById("comm_pincode_error").innerHTML = "";
        }
        else {
            document.getElementById("comm_pincode_error").innerHTML = "Invalid Pin Code. Min-Max 6 digits";
            return false;
        }
    }

    if (document.applicationForm.CommunicationStateID.value == "0" || document.applicationForm.CommunicationStateID.value == "") {
        document.getElementById("comm_states_error").innerHTML = "State field cannot be left blank";
        return false;
    }
    else {
        document.getElementById("comm_states_error").innerHTML = "";
    }
    console.log('Before District');
    if (document.applicationForm.CommunicationDistrictId.value == "0" || document.applicationForm.CommunicationDistrictId.value == "") {
        document.getElementById("comm_district_error").innerHTML = "District field cannot be left blank";
        return false;
    }
    else {
        document.getElementById("comm_district_error").innerHTML = "";
    }
    console.log('After District');

    //if (document.applicationForm.CommunicationDialCode.value == "") {
    //    document.getElementById("comm_officePhone_error").innerHTML = "Dial Code is required";
    //    return false;
    //}

    var stdCode = document.applicationForm.CommunicationDialCode.value.trim();
    if (stdCode != "") {

        if (stdCode.length < 2 || stdCode.length > 4) {
            document.getElementById("CommunicationDialCode_error").innerHTML = "Length 2 to 4 digits";
            return false;
        }
        else {
            document.getElementById("CommunicationDialCode_error").innerHTML = "";
        }

    }
    else {
        document.getElementById("CommunicationDialCode_error").innerHTML = "";
    }

    var phno = document.applicationForm.CommunicationPhoneNo.value.trim();
    if (phno != "") {
        if (phno.charAt(0) == "0") {
            document.getElementById("comm_officePhone_error").innerHTML = "Phone no cannot start with 0";
            return false;
        }
        else {
            document.getElementById("comm_officePhone_error").innerHTML = "";
        }

        if (phno.length < 6 || phno.length > 8) {
            document.getElementById("comm_officePhone_error").innerHTML = "Phone no length 6 to 8 digits";
            return false;
        }
        else {
            document.getElementById("comm_officePhone_error").innerHTML = "";
        }

    }
    else {
        document.getElementById("comm_officePhone_error").innerHTML = "";
    }


    var faxCode = document.applicationForm.CommunicationFaxCode.value.trim();
    if (faxCode != "") {

        if (faxCode.length < 2 || faxCode.length > 4) {
            document.getElementById("CommunicationFaxCode_error").innerHTML = "Length 2 to 4 digits";
            return false;
        }
        else {
            document.getElementById("CommunicationFaxCode_error").innerHTML = "";
        }

    }
    else {
        document.getElementById("CommunicationFaxCode_error").innerHTML = "";
    }

    var faxphno = document.applicationForm.CommunicationFax.value.trim();
    if (faxphno != "") {
        if (phno.charAt(0) == "0") {
            document.getElementById("CommunicationFax_error").innerHTML = "Fax no cannot start with 0";
            return false;
        }
        else {
            document.getElementById("CommunicationFax_error").innerHTML = "";
        }

        if (faxphno.length < 6 || faxphno.length > 8) {
            document.getElementById("CommunicationFax_error").innerHTML = "Fax no length 6 to 8 digits";
            return false;
        }
        else {
            document.getElementById("CommunicationFax_error").innerHTML = "";
        }

    }
    else {
        document.getElementById("CommunicationFax_error").innerHTML = "";
    }

    console.log('Before CommunicationMobileNumber check');
    if (document.applicationForm.CommunicationMobileNumber.value.trim() == "") {
        document.getElementById("comm_mobileNumber_error").innerHTML = "Mobile Number field cannot be left blank";
        return false;

    }
    else {

        if (document.getElementById("CommunicationMobileNumber").value.substring(0, 1) == "0") {
            document.getElementById("comm_mobileNumber_error").innerHTML = "Invalid Mobile Number. Min-Max 10 digits";
            return false;
        }
        else {
            document.getElementById("comm_mobileNumber_error").innerHTML = "";
        }

        if (document.getElementById("CommunicationMobileNumber").value.length < 10) {
            document.getElementById("comm_mobileNumber_error").innerHTML = "Invalid Mobile Number. Min-Max 10 digits";
            return false;
        }
        else {
            document.getElementById("comm_mobileNumber_error").innerHTML = "";
        }

        if (document.getElementById("CommunicationMobileNumber").value.match(phoneno)) {
            document.getElementById("comm_mobileNumber_error").innerHTML = "";
        }
        else {
            document.getElementById("comm_mobileNumber_error").innerHTML = "Invalid Mobile Number. Min-Max 10 digits";
            return false;
        }
    }
    console.log('After CommunicationMobileNumber check');

    if (document.applicationForm.CommunicationEmail.value.trim() == "") {
        document.getElementById("comm_email_error").innerHTML = "Email field cannot be left blank";
        return (false);
    }
    else {
        if (document.applicationForm.CommunicationEmail.value.match(email)) {
            document.getElementById("comm_email_error").innerHTML = "";
            //return true
        }
        else {
            document.getElementById("comm_email_error").innerHTML = "Invalid email id (e.g.: abc@gmail.com)";
            return (false);
        }
    }


    if (localStorage.getItem("MOBILENUMBERREADYUSED") == 0) {
        document.getElementById("comm_mobileNumber_error").innerHTML = "Mobile Number already exists";
        return (false);
    }
    else {
        document.getElementById("comm_mobileNumber_error").innerHTML = "";
    }

    if (localStorage.getItem("EMAILIDALREADYUSED") == 0) {
        document.getElementById("comm_email_error").innerHTML = "Email already exists";
        return (false);
    }
    else {
        document.getElementById("comm_email_error").innerHTML = "";
    }

    if (document.getElementById("sameAddressCheck").checked != true) {

        if (document.getElementById("PerOrRegAddress1").value.trim() == "") {
            document.getElementById("perma_address1_error").innerHTML = "Address field cannot be left blank";
            return false;
        }
        else {
            if (!document.getElementById("PerOrRegAddress1").value.match(atLeastOneAlphabet)) {
                document.getElementById("perma_address1_error").innerHTML = "Invalid Address";
                return false;
            }
            else {
                document.getElementById("perma_address1_error").innerHTML = "";
            }
        }

        if (document.getElementById("PerOrRegAddress2").value.trim() == "") {
            document.getElementById("perma_address2_error").innerHTML = "Address field cannot be left blank";
            return false;
        }
        else {
            if (!document.getElementById("PerOrRegAddress2").value.match(atLeastOneAlphabet)) {
                document.getElementById("perma_address2_error").innerHTML = "Invalid Address";
                return false;
            }
            else {
                document.getElementById("perma_address2_error").innerHTML = "";
            }
        }

        if (document.getElementById("PerOrRegAddress3").value.trim() != "")
        {
            if (!document.getElementById("PerOrRegAddress3").value.match(atLeastOneAlphabet))
            {
                document.getElementById("perma_address3_error").innerHTML = "Invalid Address";
                return false;
            }
            else
            {
                document.getElementById("perma_address3_error").innerHTML = "";
            }
        }
        else
        {
            document.getElementById("perma_address3_error").innerHTML = "";
        }

        if (document.getElementById("PerOrRegAddressLocation").value.trim() != "")
        {
            if (!document.getElementById("PerOrRegAddressLocation").value.match(atLeastOneAlphabet)) {
                document.getElementById("perma_location_error").innerHTML = "Invalid Location";
                return false;
            }
            else
            {
                document.getElementById("perma_location_error").innerHTML = "";
            }
        }
        else
        {
            document.getElementById("perma_location_error").innerHTML = "";
        }

        if (document.getElementById("PerOrRegAddressCity").value.trim() == "") {
            document.getElementById("perma_city_error").innerHTML = "City field cannot be left blank";
            return false;
        }
        else {
            if (!document.getElementById("PerOrRegAddressCity").value.match(atLeastOneAlphabet)) {
                document.getElementById("perma_city_error").innerHTML = "Invalid City";
                return false;
            }
            else {
                document.getElementById("perma_city_error").innerHTML = "";
            }
        }


        if (document.getElementById("PerOrRegAddressPinCode").value.trim() == "") {
            document.getElementById("perma_pincode_error").innerHTML = "Pin code field cannot be left blank";
            return (false);
        }
        else {
            if (document.getElementById("PerOrRegAddressPinCode").value.substring(0, 1) == "0") {
                document.getElementById("perma_pincode_error").innerHTML = "Invalid Pin code. Min-Max 6 digits";
                return false;
            }
            else {
                document.getElementById("perma_pincode_error").innerHTML = "";
            }

            if (document.getElementById("PerOrRegAddressPinCode").value.length < 6) {
                document.getElementById("perma_pincode_error").innerHTML = "Invalid Pin code. Min-Max 6 digits";
                return false;
            }
            else {
                document.getElementById("perma_pincode_error").innerHTML = "";
            }

            if (document.getElementById("PerOrRegAddressPinCode").value.match(pincode)) {
                document.getElementById("perma_pincode_error").innerHTML = "";
            }
            else {
                document.getElementById("perma_pincode_error").innerHTML = "Invalid Pin Code. Min-Max 6 digits";
                return false;
            }
        }


        if (document.getElementById("CommunicationDistrictId").value == "0" || document.getElementById("CommunicationDistrictId").value == "-1") {
            document.getElementById("comm_district_error").innerHTML = "District field cannot be left blank";
            return false;
        }
        else {
            document.getElementById("comm_district_error").innerHTML = "";
        }


        if (document.getElementById("PerOrRegAddressStateID").value == "0") {
            document.getElementById("perma_state_error").innerHTML = "State field cannot be left blank";
            return false;
        }
        else {
            document.getElementById("perma_state_error").innerHTML = "";
        }


        if (document.getElementById("PermanentDistrictId").value == "-1" || document.getElementById("PermanentDistrictId").value == "0") {
            document.getElementById("perma_district_error").innerHTML = "District field cannot be left blank";
            return false;
        }
        else {
            document.getElementById("perma_district_error").innerHTML = "";
        }


        ////if (document.applicationForm.PerOrRegAddressDialCode.value == "") {
        //if (document.getElementById("PerOrRegAddressDialCode").value == "") {
        //    document.getElementById("perma_officePhone_error").innerHTML = "Dial Code is required";
        //    return false;
        //}
        ////else if (document.applicationForm.PerOrRegAddressPhoneNumber.value == "") {
        //else if (document.getElementById("PerOrRegAddressPhoneNumber").value == "") {
        //    document.getElementById("perma_officePhone_error").innerHTML = "Phone (Home) is required";
        //    return false;
        //}
        //else {
        //    document.getElementById("perma_officePhone_error").innerHTML = "";
        //    //console.log("dfgdf");				
        //    //document.getElementById("officialDetails-tab").click();
        //    //document.getElementById("officialDetails-tab").classList.remove("disable");
        //    //return true;
        //}


    }
    else {

        permanent_add.querySelectorAll(".error").forEach(function (i) {
            i.innerHTML = '';
        })


        //    //$("#officialDetails-tab").click();
    }

    if (document.getElementById("IsDuplicatePanNo").value == "0") {
        if (document.getElementById("AllowPanDuplication").value != "Y") {
            $("#panvalidation").modal("show");
            return false;
        }
    }
    else {
        document.getElementById("PanCardRemarks").value = "";
        document.getElementById("lblPanCardRemarks").style.display = "none";
        document.getElementById("PanCardRemarks").style.display = "none";
    }

    document.getElementById("officialDetails-tab").click();
    document.getElementById("officialDetails-tab").classList.remove("disable");
    localStorage.setItem("keyOfficial", true);
    localStorage.removeItem("showregAddress");
}

function showCardDetails() {
    debugger;

    if (document.applicationForm.KeyOffTitle.value == "-1") {
        document.getElementById("officialTitle_error").innerHTML = "Select Title";
        return false;
    }
    else {
        document.getElementById("officialTitle_error").innerHTML = "";
    }
    
    if (document.applicationForm.KeyOffFirstName.value.trim() == "") {
        document.getElementById("official_fName_error").innerHTML = "First Name is required";
        return false;
    }
    else {

        if (!isNaN(document.getElementById("KeyOffFirstName").value.trim())) {
            document.getElementById("official_fName_error").innerHTML = "Invalid First Name";
            return false;
        }
        else {
            document.getElementById("official_fName_error").innerHTML = "";
        }

        if (!document.getElementById("KeyOffFirstName").value.match(nameWithSpaceCheck)) {
            document.getElementById("official_fName_error").innerHTML = "Invalid First Name";
            return false;
        }
        else {
            document.getElementById("official_fName_error").innerHTML = "";
        }
    }

    if (document.getElementById("KeyOffMiddleName").value.trim() != "")
    {
        if (!isNaN(document.getElementById("KeyOffMiddleName").value.trim())) {
            document.getElementById("KeyOffMiddleName_error").innerHTML = "Invalid Middle Name";
            return false;
        }
        else {
            document.getElementById("KeyOffMiddleName_error").innerHTML = "";
        }

        if (!document.getElementById("KeyOffMiddleName").value.match(nameWithSpaceCheck)) {
            document.getElementById("KeyOffMiddleName_error").innerHTML = "Invalid Middle Name";
            return false;
        }
        else {
            document.getElementById("KeyOffMiddleName_error").innerHTML = "";
        }
    }
    else {
        document.getElementById("KeyOffMiddleName_error").innerHTML = "";
    }

    if (document.getElementById("KeyOffLastName").value.trim() != "")
    {
        if (!isNaN(document.getElementById("KeyOffLastName").value.trim())) {
            document.getElementById("KeyOffLastName_error").innerHTML = "Invalid Last Name";
            return false;
        }
        else {
            document.getElementById("KeyOffLastName_error").innerHTML = "";
        }

        if (!document.getElementById("KeyOffLastName").value.match(nameWithSpaceCheck)) {
            document.getElementById("KeyOffLastName_error").innerHTML = "Invalid Last Name";
            return false;
        }
        else {
            document.getElementById("KeyOffLastName_error").innerHTML = "";
        }
    }
    else {
        document.getElementById("KeyOffLastName_error").innerHTML = "";
    }

    var faxCode = document.applicationForm.KeyOffFaxCode.value.trim();
    if (faxCode != "") {

        if (faxCode.length < 2 || faxCode.length > 4) {
            document.getElementById("KeyOffFaxCode_error").innerHTML = "Length 2 to 4 digits";
            return false;
        }
        else {
            document.getElementById("KeyOffFaxCode_error").innerHTML = "";
        }

    }
    else {
        document.getElementById("KeyOffFaxCode_error").innerHTML = "";
    }

    var faxno = document.applicationForm.KeyOffFax.value.trim();
    if (faxno != "") {
        if (faxno.charAt(0) == "0") {
            document.getElementById("KeyOffFax_error").innerHTML = "Fax no cannot start with 0";
            return false;
        }
        else {
            document.getElementById("KeyOffFax_error").innerHTML = "";
        }

        if (faxno.length < 6 || faxno.length > 8) {
            document.getElementById("KeyOffFax_error").innerHTML = "Fax no length 6 to 8 digits";
            return false;
        }
        else {
            document.getElementById("KeyOffFax_error").innerHTML = "";
        }

    }
    else {
        document.getElementById("KeyOffFax_error").innerHTML = "";
    }


    var stdCode = document.applicationForm.KeyOffPhoneCode.value.trim();
    if (stdCode != "") {

        if (stdCode.length < 2 || stdCode.length > 4) {
            document.getElementById("KeyOffPhoneCode_error").innerHTML = "Length 2 to 4 digits";
            return false;
        }
        else {
            document.getElementById("KeyOffPhoneCode_error").innerHTML = "";
        }

    }
    else {
        document.getElementById("KeyOffPhoneCode_error").innerHTML = "";
    }

    var phno = document.applicationForm.KeyOffPhoneNumber.value.trim();
    if (phno != "") {
        if (phno.charAt(0) == "0") {
            document.getElementById("KeyOffPhoneNumber_error").innerHTML = "Phone no cannot start with 0";
            return false;
        }
        else {
            document.getElementById("KeyOffPhoneNumber_error").innerHTML = "";
        }

        if (phno.length < 6 || phno.length > 8) {
            document.getElementById("KeyOffPhoneNumber_error").innerHTML = "Phone no length 6 to 8 digits";
            return false;
        }
        else {
            document.getElementById("KeyOffPhoneNumber_error").innerHTML = "";
        }

    }
    else {
        document.getElementById("KeyOffPhoneNumber_error").innerHTML = "";
    }

    if (document.applicationForm.KeyOffEmail.value.trim() != "") {

        if (document.applicationForm.KeyOffEmail.value.match(email)) {
            document.getElementById("KeyOffEmail_error").innerHTML = "";
        }
        else {
            document.getElementById("KeyOffEmail_error").innerHTML = "Invalid email id (e.g.: abc@gmail.com)";
            return (false);
        }
    }

    if (document.applicationForm.KeyOffMobileNumber.value.trim() == "") {
        document.getElementById("official_mobile_error").innerHTML = "Mobile Number field cannot be left blank";
        return false;
    }
    else {
        if (document.getElementById("KeyOffMobileNumber").value.substring(0, 1) == "0") {
            document.getElementById("official_mobile_error").innerHTML = "Invalid Mobile Number. Min-Max 10 digits";
            return false;
        }
        else {
            document.getElementById("official_mobile_error").innerHTML = "";
        }

        if (document.getElementById("KeyOffMobileNumber").value.length < 10) {
            document.getElementById("official_mobile_error").innerHTML = "Invalid Mobile Number. Min-Max 10 digits";
            return false;
        }
        else {
            document.getElementById("official_mobile_error").innerHTML = "";
        }

        if (document.getElementById("KeyOffMobileNumber").value.match(phoneno)) {
            document.getElementById("official_mobile_error").innerHTML = "";
        }
        else {
            document.getElementById("official_mobile_error").innerHTML = "Invalid Mobile Number. Min-Max 10 digits";
            return false;
        }

    }

    if (document.applicationForm.KeyOffDesignation.value.trim() == "") {
        document.getElementById("official_designation_error").innerHTML = "Designation field cannot be left blank";
        return false;
    }
    else
    {
        if (!isNaN(document.getElementById("KeyOffDesignation").value.trim())) {
            document.getElementById("official_designation_error").innerHTML = "Invalid Designation";
            return false;
        }
        else {
            document.getElementById("official_designation_error").innerHTML = "";
        }

        if (!document.getElementById("KeyOffDesignation").value.match(nameWithSpaceCheck)) {
            document.getElementById("official_designation_error").innerHTML = "Invalid Designation";
            return false;
        }
        else {
            document.getElementById("official_designation_error").innerHTML = "";
        }
    }

    if (document.getElementById("KeyOfficialSecretAnswer").value.trim() != "")
    {
        if (!document.getElementById("KeyOfficialSecretAnswer").value.match(nameWithSpaceCheck)) {
            document.getElementById("KeyOfficialSecretAnswer_error").innerHTML = "Invalid Secret Answer";
            return false;
        }
        else
        {
            document.getElementById("KeyOfficialSecretAnswer_error").innerHTML = "";
        }
    }
    else
    {
        document.getElementById("KeyOfficialSecretAnswer_error").innerHTML = "";
    }

    if (localStorage.getItem("DUPLICATEPANUSED") == 0) {
        if (document.getElementById("PanCardRemarks").value.trim() == "") {
            document.getElementById("PanCardRemarks_error").innerHTML = "Pan Card Remarks field cannot be left blank";
            return (false);
        }
        else {
            document.getElementById("PanCardRemarks_error").innerHTML = "";
        }
    }
    else {
        document.getElementById("PanCardRemarks_error").innerHTML = "";
    }

    return true;

    //document.getElementById("cardDetails-tab").click();
    //document.getElementById("cardDetails-tab").classList.remove("disable");
    //document.getElementById("uploadDocuments-tab").classList.remove("disable");
    //localStorage.setItem("cardDetails", true)
    //localStorage.removeItem("keyOfficial")
}


function submitForm() {

document.applicationForm.submit();
localStorage.clear();
}

function playVideo(e) {
$('video').trigger('pause');
$('.play_btn').fadeIn();
$(e).fadeOut();
$(e).next('video').trigger('play');
}

topMenu = $("#mainNav"),
topMenuHeight = topMenu.outerHeight() + 1,
// All list items
menuItems = topMenu.find("a:not(a[href='#'])"),
// Anchors corresponding to menu items
scrollItems = menuItems.map(function () {
    var item = $($(this).attr("href"));
    if (item.length) { return item; }
});

// Bind click handler to menu items
// so we can get a fancy scroll animation
var count = 0;
menuItems.click(function (e) {
var href = $(this).attr("href"),
    offsetTop = href === "#" ? 0 : $(href).offset().top - topMenuHeight + 1;


if (count > 0) {
    $('html, body').stop().animate({
        scrollTop: offsetTop - 180
    }, 850);

}
else {
    $('html, body').stop().animate({
        scrollTop: offsetTop - 300
    }, 850);
}
count++;


e.preventDefault();
});

$(window).on("scroll", function () {
if ($(window).scrollTop() > 50) {
    $("nav").addClass("header_sticky")
}
else {
    $("nav").removeClass("header_sticky")
}

if ($(window).scrollTop() > 350) {
    $("#mainNav").addClass("scroll_sticky")
}
else {
    $("#mainNav").removeClass("scroll_sticky")
}

//console.log(offsetTop);
//$(".scroll_spy a[href='"+offsetTop+"']").addClass("active");
})

$('.brand-slider').owlCarousel({
items: 5,
loop: true,
margin: 10,
merge: true,
autoplayTimeout: 3000,
autoplaySpeed: 700,
autoplay: true,
responsive: {
    320: {
        mergeFit: true,
        items: 2,
    },
    480: {
        mergeFit: true,
        items: 3,
    },
    678: {
        mergeFit: true,
        items: 4,
    },
    1000: {
        mergeFit: false
    }
}
});
$('.aids-slider').owlCarousel({
items: 3,
loop: true,
margin: 10,
merge: true,
autoplayTimeout: 3000,
autoplaySpeed: 700,
autoplay: true,
dots: false,
responsive: {
    320: {
        mergeFit: true,
        items: 1,
    },
    480: {
        mergeFit: true,
        items: 1,
    },
    678: {
        mergeFit: true,
        items: 3,
    },
    1000: {
        mergeFit: false
    }
}
});
$('.brand-slider33').owlCarousel({
items: 6,
loop: true,
margin: 10,
merge: true,
autoplayTimeout: 3000,
autoplaySpeed: 700,
autoplay: true,
responsive: {
    320: {
        mergeFit: true,
        items: 3,
    },
    480: {
        mergeFit: true,
        items: 3,
    },
    678: {
        mergeFit: true,
        items: 4,
    },
    1000: {
        mergeFit: false
    }
}
});
$('.speciality_slider').owlCarousel({
items: 3,
loop: false,
margin: 10,
merge: true,
autoplayTimeout: 3000,
autoplaySpeed: 700,
dots: false,
nav: true,
navText: ["<", ">"],
autoplay: true,
responsive: {
    320: {
        mergeFit: true,
        items: 1,
        margin: 0,
    },
    480: {
        mergeFit: true,
        items: 1,
    },
    678: {
        mergeFit: true,
        items: 2,
    },
    1000: {
        mergeFit: false
    }
}
});
$('.article_slider').owlCarousel({
items: 3,
loop: false,
margin: 10,
merge: true,
autoplayTimeout: 3000,
autoplaySpeed: 700,
dots: false,
nav: true,
navText: ["<", ">"],
autoplay: true,
responsive: {
    320: {
        mergeFit: true,
        items: 1,
        margin: 10,
    },
    480: {
        mergeFit: true,
        items: 1,
    },
    678: {
        mergeFit: true,
        items: 2,
    },
    1000: {
        mergeFit: false
    }
}
});
$('.testimonial_slider').owlCarousel({
items: 3,
loop: false,
margin: 10,
merge: true,
autoplayTimeout: 3000,
autoplaySpeed: 700,
dots: false,
nav: true,
navText: ["<", ">"],
autoplay: false,
responsive: {
    320: {
        mergeFit: true,
        items: 1,
        margin: 0,
    },
    480: {
        mergeFit: true,
        items: 1,
    },
    678: {
        mergeFit: true,
        items: 2,
    },
    1000: {
        mergeFit: false
    }
}
});
$('.condition_slider').owlCarousel({
items: 5,
loop: false,
margin: 2,
merge: true,
autoplayTimeout: 3000,
autoplaySpeed: 700,
dots: false,
nav: true,
navText: ["<", ">"],
autoplay: false,
responsive: {
    320: {
        mergeFit: true,
        items: 1,
        margin: 0,
    },
    480: {
        mergeFit: true,
        items: 3,
    },
    678: {
        mergeFit: true,
        items: 4,
    },
    1000: {
        mergeFit: false
    }
}
});
$('.main-slider').owlCarousel({
items: 1,
loop: true,
margin: 0,
merge: true,
autoplayTimeout: 3000,
autoplaySpeed: 700,
autoplay: false,
dots: true,
nav: false,
navText: ["<img src='assets/images/arrow_left.png' class='img-fluid'>", "<img src='assets/images/arrow_right.png' class='img-fluid'>"]
});
$('.analytics-slider').owlCarousel({
items: 1,
loop: true,
margin: 10,
merge: true,
nav: false,
autoplayTimeout: 3000,
autoplaySpeed: 700,
autoplay: true,
responsive: {
    600: {
        items: 1,
        margin: 10,
    },
    0: {
        items: 1,
        mergeFit: false
    }
}
});
$('.live_on_slider').owlCarousel({
items: 2,
loop: true,
margin: 10,
merge: true,
nav: false,
autoplayTimeout: 3000,
autoplaySpeed: 700,
autoplay: true,
responsive: {
    600: {
        items: 2,
        margin: 10,
    },
    0: {
        items: 1,
        mergeFit: false
    }
}
});
$('.reviews-slider').owlCarousel({
items: 1,
loop: true,
margin: 10,
merge: true,
nav: false,
autoplayTimeout: 3000,
autoplaySpeed: 700,
autoplay: true,
responsive: {
    0: {
        items: 1,
        margin: 0,
    },
    540: {
        items: 1,
        margin: 0,
    },
    1000: {
        items: 1,
        nav: false,
        margin: 10,
        mergeFit: false
    }
}
});

$("input[name=prob_faced]").on("change", function () {
if ($("input[name=prob_faced]:checked").val() == 'others') {
    $("#other_problem").fadeIn();
}
else {
    $("#other_problem").fadeOut();
}
})

jQuery(document).ready(function ($) {
$('.question').on('click', function () {
    if ($(this).hasClass('active')) {
        $('.question').removeClass('active');
        $('.arrow').removeClass('arrow-active');
        $('.answer').slideUp();
    }
    else {
        $('.question').removeClass('active');
        $('.arrow').removeClass('arrow-active');
        $('.answer').slideUp();
        $(this).addClass('active');
        $(this).children('.arrow').addClass('arrow-active');
        $(this).children('.answer').slideToggle('slow');
    }
});
});


$(document).ready(function () {



var x, i, j, l, ll, selElmnt, a, b, c;
/* Look for any elements with the class "custom-select": */
x = document.getElementsByClassName("search_filter_btn");
l = x.length;
for (i = 0; i < l; i++) {
    selElmnt = x[i].getElementsByTagName("select")[0];
    ll = selElmnt.length;
    /* For each element, create a new DIV that will act as the selected item: */
    a = document.createElement("DIV");
    a.setAttribute("class", "select-selected");
    a.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
    x[i].appendChild(a);
    /* For each element, create a new DIV that will contain the option list: */
    b = document.createElement("DIV");
    b.setAttribute("class", "select-items select-hide");
    for (j = 1; j < ll; j++) {
        /* For each option in the original select element,
        create a new DIV that will act as an option item: */
        c = document.createElement("DIV");
        c.innerHTML = selElmnt.options[j].innerHTML;
        c.addEventListener("click", function (e) {
            /* When an item is clicked, update the original select box,
            and the selected item: */
            var y, i, k, s, h, sl, yl;
            s = this.parentNode.parentNode.getElementsByTagName("select")[0];
            sl = s.length;
            h = this.parentNode.previousSibling;
            for (i = 0; i < sl; i++) {
                if (s.options[i].innerHTML == this.innerHTML) {
                    s.selectedIndex = i;
                    h.innerHTML = this.innerHTML;
                    y = this.parentNode.getElementsByClassName("same-as-selected");
                    yl = y.length;
                    for (k = 0; k < yl; k++) {
                        y[k].removeAttribute("class");
                    }
                    this.setAttribute("class", "same-as-selected");
                    break;
                }
            }
            h.click();
        });
        b.appendChild(c);
    }
    x[i].appendChild(b);
    a.addEventListener("click", function (e) {
        /* When the select box is clicked, close any other select boxes,
        and open/close the current select box: */
        e.stopPropagation();
        closeAllSelect(this);
        this.nextSibling.classList.toggle("select-hide");
        this.classList.toggle("select-arrow-active");
    });
}
})

function closeAllSelect(elmnt) {
/* A function that will close all select boxes in the document,
except the current select box: */
var x, y, i, xl, yl, arrNo = [];
x = document.getElementsByClassName("select-items");
y = document.getElementsByClassName("select-selected");
xl = x.length;
yl = y.length;
for (i = 0; i < yl; i++) {
    if (elmnt == y[i]) {
        arrNo.push(i)
    } else {
        y[i].classList.remove("select-arrow-active");
    }
}
for (i = 0; i < xl; i++) {
    if (arrNo.indexOf(i)) {
        x[i].classList.add("select-hide");
    }
}
}

/* If the user clicks anywhere outside the select box,
then close all select boxes: */
document.addEventListener("click", closeAllSelect);

//
//$(".top_nav li").on('click', 'a[href^="#"]:not(.primary_btn)', function (event) {
//	$(".top_nav li").removeClass("active");
//	$(this).parent("li").addClass("active")
//	//console.log(event.target.attributes.href);
//    event.preventDefault();
//    $('html, body').animate({
//        scrollTop: $($.attr(this, 'href')).offset().top - 80
//    }, 1000);
//	$("#navbar-collapse").collapse('hide');
//});


function Validate() {

if (document.register_form.username.value == "") {
    document.getElementById("username_error").innerHTML = "This information is required";
    document.register_form.username.focus;
    return (false);
}
else {
    document.getElementById("username_error").innerHTML = "";
}

if (document.register_form.password.value == "") {
    document.getElementById("pass_error").innerHTML = "This information is required";
    document.register_form.password.focus;
    return (false);
}
else {
    document.getElementById("pass_error").innerHTML = "";
}

if (document.register_form.captcha.value == "") {
    document.getElementById("captcha_error").innerHTML = "This information is required";
    document.register_form.captcha.focus;
    return (false);
}
else {
    document.getElementById("captcha_error").innerHTML = "";
}

}

function GetClientConfirmation(o) {
    debugger;
    var selectedButton = o.innerText;

    console.log(selectedButton);

    if (selectedButton == "OK") {
        $('#AllowPanDuplication').val('Y');
        $("#PanCardRemarks").prop('readonly', false);
        document.getElementById("lblPanCardRemarks").style.display = "block";
        document.getElementById("PanCardRemarks").style.display = "block";
        //document.getElementById("officialDetails-tab").click();
        //document.getElementById("officialDetails-tab").classList.remove("disable");
        //localStorage.setItem("keyOfficial", true);
        //localStorage.removeItem("showregAddress");
        showBasicInfo();
    }
    else {
        $('#AllowPanDuplication').val('N');
        $("#PanCardRemarks").prop('readonly', true);
        document.getElementById("lblPanCardRemarks").style.display = "none";
        document.getElementById("PanCardRemarks").style.display = "none";
        document.getElementById("address-tab").click();
        document.getElementById("address-tab").classList.remove("disable");
        localStorage.setItem("showregAddress", true)
    }
    console.log($('#AllowPanDuplication').val());
    $('#panvalidation').modal('hide');
}

function ValidatePAN() {
    debugger;
    var panno = $('#CustomerIncomeTaxPan').val().trim();
    var OrgName = $('#IndividualOrgName').val().trim();
    var customerTbentityid = $("#CustomerTbentityID").val();
    var correctPANName = '';

    if (panno == '') {
        document.getElementById("incomeTaxPan_error").innerHTML = "Income Tax PAN field cannot be left blank";
        return;
    }


    if ($('#CustomerIncomeTaxPan').val().match(pancard)) {
        document.getElementById("incomeTaxPan_error").innerHTML = "Income Tax PAN is valid";
    }
    else {
        document.getElementById("incomeTaxPan_error").innerHTML = "Invalid Income Tax PAN";
        return (false);
    }

    //if (OrgName == '') {
    //    alert('Enter Individual Or Org. Name');
    //    return;
    //}
    ////solo Propritorship 4th Char Pan should be 'P'
    if (customerTbentityid == '2') {
        let forthdigitPan = panno.substr(3, 1);
        console.log(forthdigitPan);
        if (forthdigitPan != 'P') {
            console.log('Inside');
            document.getElementById("incomeTaxPan_error").innerHTML = "Invalid Income Tax PAN";
            alert('Please Enter Valid Pan Number');
            return;
        }
        else {
            document.getElementById("incomeTaxPan_error").innerHTML = "Income Tax PAN is valid";
        }
    }

    //var result = false;

    //$.ajax({
    //    type: 'POST',  // http method
    //    url: "PANValidation/Customer",
    //    data: { PANNumber: panno },  // data to submit
    //    dataType: "json",
    //    success: function (data, status, xhr) {
    //        //debugger;
    //        var jsonData = JSON.parse(data);
    //        if (status == 'success' && jsonData['status-code'] == '101') {
    //            result = true;

    //            console.log(jsonData);
    //            console.log(result);
    //            //document.getElementById("address-tab").click();
    //            //document.getElementById("address-tab").classList.remove("disable");
    //            //document.applicationForm.IndividualOrgName.value = jsonData["result"]["name"];
    //            correctPANName = jsonData["result"]["name"];
    //            //localStorage.setItem("showregAddress", true)

    //            if (result == false) {
    //                document.getElementById("incomeTaxPan_error").innerHTML = "Invalid Income Tax PAN";
    //            }
    //            else {
    //                document.getElementById("incomeTaxPan_error").innerHTML = "";
    //            }
    //            console.log(correctPANName);
    //            console.log(OrgName);
    //            if (correctPANName != OrgName) {
    //                document.getElementById("incomeTaxPan_error").innerHTML = "Your pan card name not match with pan card name";
    //            }
    //            else {
    //                document.getElementById("incomeTaxPan_error").innerHTML = "";
    //            }

    //        }
    //        else {
    //            document.getElementById("incomeTaxPan_error").innerHTML = "Invalid PAN Number";
    //            console.log(jsonData['status-code']);
    //            //document.applicationForm.CustomerIncomeTaxPan.focus();
    //        }
    //    },
    //    error: function (jqXhr, textStatus, errorMessage) {
    //        document.getElementById("incomeTaxPan_error").innerHTML = "Invalid PAN Number";
    //        console.log(jsonData['status-code']);
    //        //document.applicationForm.CustomerIncomeTaxPan.focus();
    //    }
    //});

}
