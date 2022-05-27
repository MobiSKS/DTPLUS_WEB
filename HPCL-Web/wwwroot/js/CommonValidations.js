function isNumberKey(evt)
{
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function IsAlphaNumeric(keyCode)
{
    debugger;
    return ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 8 || keyCode == 9 || keyCode == 46 ||
        (keyCode >= 96 && keyCode <= 105) || keyCode == 37 || keyCode == 38 || keyCode == 39 || keyCode == 40);
}

function AlphaNumericWithSpace(keyCode)
{
    debugger;
    return ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 8 || keyCode == 9 || keyCode == 46 ||
        (keyCode >= 96 && keyCode <= 105) || keyCode == 37 || keyCode == 38 || keyCode == 39 || keyCode == 40);
}