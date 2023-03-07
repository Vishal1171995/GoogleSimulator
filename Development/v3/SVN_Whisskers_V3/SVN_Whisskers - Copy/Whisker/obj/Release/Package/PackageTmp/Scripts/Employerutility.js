var emailstatus = false;
$(document).ready(function () {
    
    $("body").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
        }
    });

    $(".NoOnly").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^\d].+/, ""));
        if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });
});
function chkInstRegister() {
    var nameExp = /^[0-9a-zA-Z]+$/;
    //var nameExp = /^[A-Za-z0-9]{3,250}$/;
    var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    var phoneno = /^\+?([8-9]{2})\)?[-. ]?([0-9]{11})$/;
    var imagecheck = /\.(jpg|jpeg|png|gif)$/;
    var capImagecheck = /\.(JPG|JPEG|PNG|GIF)$/;
    

    if ($('#txt_InsName').val() == "") {

        $('#msg_Error').text("Employer name is required");
        onClickOpen('poup_msg');
        return false;
    }

    var val = $("#txt_InsName").val();
    if (!val.match(nameExp)) {

        $('#msg_Error').text('Please enter valid name');
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlCountry').val() == "0") {
        $('#msg_Error').text("Country is required");
        onClickOpen('poup_msg');
        return false;
    }

     

    if ($('#ddldivision').val() == "0") {
       
        $('#msg_Error').text("Organization Stated in is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddldistrict').val() == "0") {
        $('#msg_Error').text("District is required");
        onClickOpen('poup_msg');
        return false;
    }


    if ($('#emailID').val() == "") {
            $('#msg_Error').text("EmailID is required");
            onClickOpen('poup_msg');
            return false;
        }

    var emailID = $('#emailID').val();
        if (!emailID.match(ck_email)) {
            $('#msg_Error').text('Please enter valid email id');
            onClickOpen('poup_msg')
            return false;
        }


        if (emailstatus == 0) {
            $('#msg_Error').text('Email id already registered');
            onClickOpen('poup_msg')
            return false;

        }

        var pwd = $('#Password').val();

        if ($('#Password').val() == "") {
            $('#msg_Error').text("Password is required");
            onClickOpen('poup_msg');
            return false;
        }
        var confirmpwd = $('#ConfirmPassword').val();

        if ($('#ConfirmPassword').val() == "") {
            $('#msg_Error').text("Confirm Password is required");
            onClickOpen('poup_msg');
            return false;
        }

        if (pwd != confirmpwd) {
            $('#msg_Error').text('Password does not match');
            onClickOpen('poup_msg')
            return false;
        }

        if ($('#ddlfunctionalarea').val() == 0) {
            $('#msg_Error').text("Functional area is required");
            onClickOpen('poup_msg');
            return false;
        }

        if ($('#txt_CorporateAddress').val() == "") {
            $('#msg_Error').text("Corporate Address is required");
            onClickOpen('poup_msg');
            return false;
        }

        if ($('#CPName').val() == "") {
            $('#msg_Error').text("Contact person name is required");
            onClickOpen('poup_msg');
            return false;
        }
        
        if ($('#Prefixmobile').val() == "") {
            $('#msg_Error').text("Prefix mobile number is required");
            onClickOpen('poup_msg');
            return false;
        }
        if ($('#MobileNo').val() == "") {
            $('#msg_Error').text("Contact person mobile number is required");
            onClickOpen('poup_msg');
            return false;
        }


       
        


        return true;
}
$(document).ready(function () {

    $('#txtLandlineNo').mask("880-999-9999");


        $("#MobileNo,#FatherMobileNo,#HomeNo").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });
 });
function CheckEmailID(email) {
     $.ajax({
         url: "/Utility/CheckEmail",
         method: 'GET',
         data: { emailid: email },
         success: function (data) {

             if (data != "0") {
                 $('#chkemailMsg').html('Duplicate email ID');
                 emailstatus = 0;
             }
             else {
                 $('#chkemailMsg').text('');
                 emailstatus = 1;
             }
         },
         error: function (data) {
             //alert(data);
         }
     });
}

function EditPersonalinfoValidation() {

        if ($('#ddlCountry').val() == 0) {

        $('#msg_Error').text("Country is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlCity').val() == 0) {

        $('#msg_Error').text("City is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#PermanentAddress').val() == "") {

        $('#msg_Error').text("Corporate Address is Required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlFunctionalArea').val() == "0") {

        $('#msg_Error').text("Please Select Functional Area");
        onClickOpen('poup_msg');
        return false;
    }
    return true;

}
function EditReferenceValidation() {

   var mobileno = /^\d{12,13}$/;


    var txtMobileNo = $('#txtReferenceMobileNo').val();
    if (txtMobileNo == "")
    {
        $('#msg_Error').text('Mobile No. is Required.');
        onClickOpen('poup_msg');

        return false;
    }
    
    if (!txtMobileNo.match(mobileno))
    {

            $('#msg_Error').text('Please enter valid mobile number.');
            onClickOpen('poup_msg');

            return false;
    }
    
}

function JobValidation()
{
  
    var txtJtile = $('#Jtile').val();

    if (txtJtile == "") {
        $('#msg_Error').text('Job title is Required.');
        onClickOpen('poup_msg');

        return false;
    }
    
    var txtJobCode = $('#JobCode').val();

    if (txtJobCode == "") {
        $('#msg_Error').text('Job Code is Required.');
        onClickOpen('poup_msg');

        return false;
    }

    var txtJDesc = $('#JDesc').val();
    if (txtJDesc == "") {
        $('#msg_Error').text('Job Description is Required.');
        onClickOpen('poup_msg');

        return false;
    }
    
    //var txtNoofVacy = $('#NoofVacy').val();
    //if (txtNoofVacy == "") {
    //    $('#msg_Error').text('No. of Vacancy is Required.');
    //    onClickOpen('poup_msg');

    //    return false;
    //}

    if ($('#ddlcity').val() == 0) {

        $('#msg_Error').text("City is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlFunArea').val() == 0) {

        $('#msg_Error').text("Functional Area is required");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#ddljobroll').val() == 0) {

        $('#msg_Error').text("Job role is required");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#ddlworktype').val() == 0) {

        $('#msg_Error').text("Work type is required");
        onClickOpen('poup_msg');
        return false;
    }
    
    var txtlastdate = $('#txtLastDate').val();
    if (txtlastdate == "") {
        $('#msg_Error').text('Last apply date is required.');
        onClickOpen('poup_msg');

        return false;
    }
    return true;
}

function onClickOpen(divid) {

    document.getElementById(divid).style.display = "block";
}
function onClickClose(divid) {

    document.getElementById(divid).style.display = "none";

}