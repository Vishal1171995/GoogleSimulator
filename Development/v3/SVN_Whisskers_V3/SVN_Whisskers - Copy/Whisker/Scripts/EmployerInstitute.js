
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

    var nameExp = /^[A-Za-z ]{3,250}$/;
    var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    var phoneno = /^\+?([8-9]{2})\)?[-. ]?([0-9]{11})$/;
    var imagecheck = /\.(jpg|jpeg|png|gif)$/;
    var capImagecheck = /\.(JPG|JPEG|PNG|GIF)$/;
    

    if ($('#txt_InsName').val().trim() == "") {

        $('#msg_Error').text("Employer name is required");
        onClickOpen('poup_msg');
        return false;
    }

    var val = $("#txt_InsName").val().trim();
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

     

    if ($('#ddlDistrict').val() == "0") {
        $('#msg_Error').text("City is required");
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

        var pwd = $('#Password').val().trim();

        if ($('#Password').val().trim() == "") {
            $('#msg_Error').text("Password is required");
            onClickOpen('poup_msg');
            return false;
        }

        if (pwd.length < 6) {
            $('#msg_Error').text('Please enter minimum 6 character for password.');
            onClickOpen('poup_msg')
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
         error: function (data) { alert(data); }
     });
 }
function onClickOpen(divid) {

    document.getElementById(divid).style.display = "block";
}
function onClickClose(divid) {

    document.getElementById(divid).style.display = "none";

}

