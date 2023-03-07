var emailstatus = 0;
var facrowcount = 1;

$(document).ready(function () {
    $('#txtLandlineNo').mask("880-999-9999");
    $("body").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
        }
    });

    $(".NoOnly").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^\d].+/, ""));

        if (event.which == 8) {

        }
        else if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });
});



$(function () {
    $('#txt_InsName').keyup(function () {
        var yourInput = $(this).val();
        re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
        var isSplChar = re.test(yourInput);
        if (isSplChar) {
            var no_spl_char = yourInput.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
            $(this).val(no_spl_char);
        }
    });
});


function addfacility()
{
    facrowcount += 1;
    var facitem="<tr><td>"+facrowcount+"</td><td>";
        facitem+="<input type='text' name='NoRoom' maxlength='20' /></td>";
        facitem+="<td> <input type='number' name='NoStudent' class='NoOnly'  max='1000' /></td>";
        facitem += "<td> <input type='number' name='NoComput' class='NoOnly' max='1000' /> </td></tr>";
        $('#tab_facility').append(facitem);
}

function chkInstRegister()
{
    $('#msg_Error').text('');

    var nameExp = /^[A-Za-z ]{3,250}$/;
    var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    var phoneno = /^\d{10,13}$/;


    var val = $("#txt_InsName").val().trim();
    if (!val.match(nameExp)) {
       
        $('#msg_Error').text('Please enter valid institution name.');
        onClickOpen('poup_msg');
        $("#txt_InsName").focus();
        return false;
    }

    if (!$('input[name=instituteType]:checked').length > 0) {

        $('#msg_Error').text('Please select institution type.');
        onClickOpen('poup_msg');
        return false;
    }

    
    if ($('#ddldivision').val() == "0") {
        $('#msg_Error').text('Please select division.');
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddldistrict').val() == "0") {
        $('#msg_Error').text('Please select district.');
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlCity').val() == ""){
        $('#msg_Error').text('Please select city.');
        onClickOpen('poup_msg');
        return false;
    }

    var Address = $('#txt_Address').val();
    if (Address == "") {

        $('#msg_Error').text('Please enter address.');
        onClickOpen('poup_msg')
        return false;
    }

    var Cpname=$('#CPName').val();
    if (!Cpname.match(nameExp))
    {
        $('#msg_Error').text('Please enter valid contact person name.');
        onClickOpen('poup_msg');
        return false;
    }

    var txtMobileNo = $('#MobileNo').val();
    if (!txtMobileNo.match(phoneno))
    {
        $('#msg_Error').text('Please enter valid mobile number.');
        onClickOpen('poup_msg')
        return false;
    }

    var emailID = $('#emailID').val();
    if (!emailID.match(ck_email)) {
        $('#msg_Error').text('Please enter valid email id.');
        onClickOpen('poup_msg')
        return false;
    }

    
   
    var pwd = $('#txt_password').val().trim();
    if (pwd=="") {
        $('#msg_Error').text('Please enter valid password.');
        onClickOpen('poup_msg')
        return false;
    }

    if (pwd.length<6) {
        $('#msg_Error').text('Please enter minimum 6 character for password.');
        onClickOpen('poup_msg')
        return false;
    }




    var cpwd = $('#txt_CPassword').val().trim();
    if (cpwd=="") {
        $('#msg_Error').text('Please confirm password.');
        onClickOpen('poup_msg')
        return false;
    }

    if (pwd!=cpwd) {
        $('#msg_Error').text('Password does not match.');
        onClickOpen('poup_msg')
        return false;
    }




  
    CheckEmailID(emailID);
    if (emailstatus==0) {
        $('#msg_Error').text('Email id already registered');
        onClickOpen('poup_msg')
        return false;
    }

    //if ($("input[name=wday]:checked").length < 1) {
    //    $('#msg_Error').text('Please select training days.');
    //    onClickOpen('poup_msg');
    //    return false;
    //}

    var out = true;
    $('#Container input[type=text]').each(function () {
        if ($(this).val() != "") {
            out = validatetime($(this).val());
            $(this).focus();

            if (out == "false")
                return false;
        }
    });

    if (out == "false" || out == false) {
        return false;
    }
    return true;
  
}

function CheckEmailID(email)
{
    $.ajax({
        url:"/Utility/CheckEmail",
        method: 'GET',
        data: { emailid: email },
        success: function (data) {

            if (data != "0") {
                $('#chkemailMsg').html('Duplicate email id.');
                emailstatus=0;
            }
            else{
                $('#chkemailMsg').text('');
                emailstatus=1;
            }
        },
        error: function (data) {
           // alert(data);
        }
    });
}


function onClickOpen(divid) {

    document.getElementById(divid).style.display = "block";
}

function onClickClose(divid) {

    document.getElementById(divid).style.display = "none";
}