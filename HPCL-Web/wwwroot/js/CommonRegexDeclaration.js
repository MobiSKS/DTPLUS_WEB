﻿var phoneno = /^[6789]\d{9}$/;
var email = /[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$/;
var number = /^\d+$/;
var pincode = /^[1-9][0-9]{5}$/;
var mappedMerchantID = /^(?=(3))[0-9]{10}$/;
var pancard = /[A-Z]{5}\d{4}[A-Z]{1}/;
var gstNumber = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;
var lpgCngSale = /^(?:[1-9][0-9]{0,4}(d{1,2})?|100000|100000.00)$/;
var erpCodeCheck = /^(?=(1))[0-9]{8}$/;
var nameCheck = /^[A-Za-z]{2,40}$/;
var noOfTerminalsCheck = /^[1-2]{1}$/;
var nameWithSpaceCheck = /[a-zA-Z][a-zA-Z ]+[a-zA-Z]$/;