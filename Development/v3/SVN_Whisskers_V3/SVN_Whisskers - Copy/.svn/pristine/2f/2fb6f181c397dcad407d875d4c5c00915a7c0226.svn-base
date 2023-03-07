var emailstatus = false;
var facrowcount = 1;
var dobStatus = false;
var StrdobStatus = null;
var MonthArr = ["jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec"];
var validday = /^[0-9]{0,2}$/;
var validyear = /^[0-9]{4,4}$/;
 var mobileno = /^\d{10,11}$/;
 var certstatus = 0;
 var certfile = 0;
 var flag = 0;
 var flg = 0;
 var tritm=null;
$(document).ready(function () {
   
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
    $('#UName').keyup(function () {
        var yourInput = $(this).val();
        re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
        var isSplChar = re.test(yourInput);
        if (isSplChar) {
            var no_spl_char = yourInput.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
            $(this).val(no_spl_char);
        }
    });
});

function FeedbackValidation() {

    if ($('#ddlbatch').val() == "0")
    {
       
        $('#msg_Error').text("Please Select Batch");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlSession').val() == "0")
    {
        $('#msg_Error').text("Please Select Session");
        onClickOpen('poup_msg');
        return false;
    }

    
    if ($('#ddlGraduate').val() == "0") {
        $('#msg_Error').text("Please Select Graduate");
        onClickOpen('poup_msg');
        return false;
    }

  
    if ($('#SessionDate').val() == "")
    {

        $('#msg_Error').text("Session Date is required");
        onClickOpen('poup_msg');
        return false;
    }

    GetDateFormat($("#SessionDate").val());

    if (dobStatus == 0) {

        return false;
    }

    if ($('#txtMessage').val() == "") {

        $('#msg_Error').text("Message is required");
        onClickOpen('poup_msg');
        return false;
    }
    return true;
}
function AssessmentValidation() {
    var fuData = document.getElementById('UploadAssessmentFile');
    var FileUploadPath = fuData.value;

    //To check if user upload any file
    if (FileUploadPath == '') {
        $('#msg_Error').text('Please Upload file.');
        onClickOpen('poup_msg');
        return false;
    }
}
function checkFile(fieldObj) {

    var FileName = fieldObj.value;
    var FileExt = FileName.substr(FileName.lastIndexOf('.') + 1);
    var FileSize = fieldObj.files[0].size;
    //var FileSizeMB = (FileSize / 10485760).toFixed(2);

    if ((FileExt != "jpg" && FileExt != "jpeg" && FileExt != "png" && FileExt != "pdf") || FileSize > 1048576) {
        //var error = "File type : " + FileExt + "\n\n";
        //error += "Size: " + FileSizeMB + " MB \n\n";
        //error += "Please make sure your file is in pdf or doc format and less than 1 MB.\n\n";
        $('#msg_Error').text('Please make sure your file is in png,jpeg,jpg or pdf format and less than 1 MB.');
        onClickOpen('poup_msg');
        fieldObj.value = "";
        return false;

    }
    return true;
}
function checkProfileImage(fieldObj) {

    var FileName = fieldObj.value;
    var FileExt = FileName.substr(FileName.lastIndexOf('.') + 1);
    var FileSize = fieldObj.files[0].size;
    //var FileSizeMB = (FileSize / 10485760).toFixed(2);

    if ((FileExt != "jpg" && FileExt != "jpeg" && FileExt != "png") || FileSize > 1048576) {
        //var error = "File type : " + FileExt + "\n\n";
        //error += "Size: " + FileSizeMB + " MB \n\n";
        //error += "Please make sure your file is in pdf or doc format and less than 1 MB.\n\n";
        $('#msg_Error').text('Please make sure your file is in png,jpeg,jpg format and less than 1 MB.');
        onClickOpen('poup_msg');
        fieldObj.value = "";
        return false;

    }
    return true;
}

function chkFacultyRegister() {
  
  

    $('#msg_Error').text('');
   
    var nameExp = /^[A-Za-z ]{3,250}$/;
    var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    var phoneno = /^\d{10,11}$/;

    var val = $("#UName").val().trim();

   
    if ($('#UName').val().trim() == "") {

        $('#msg_Error').text("Faculty name is required");
        onClickOpen('poup_msg');
        return false;
    }

    if (!val.match(nameExp)) {

        $('#msg_Error').text('Please enter valid name.');
        onClickOpen('poup_msg');
        $("#UName").focus();
        return false;
    }

    if ($('#MobileNo').val() == "") {

        $('#msg_Error').text("mobile no. is required");
        onClickOpen('poup_msg');
        return false;
    }

    var txtMobileNo = $('#MobileNo').val();

    if (!txtMobileNo.match(phoneno)) {

        $('#msg_Error').text('Please enter valid mobile number.');
        onClickOpen('poup_msg');
        $("#MobileNo").focus();
        return false;
    }

    if (!$('input[name=Gender]:checked').length > 0) {
        $('#msg_Error').text('Please select gender.');
        onClickOpen('poup_msg');
        $("input[name=Gender]:checked").focus();
        return false;
    }


    var fuData = document.getElementById('Imagefile');
    var FileUploadPath = fuData.value;

    //To check if user upload any file
    if (FileUploadPath == '') {
        $('#msg_Error').text('Profile Picture is mandatory');
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#MobileNo').val() == "") {

        $('#msg_Error').text("mobile no. is required");
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
        onClickOpen('poup_msg');
        $("#emailID").focus();
        return false;
    }

    CheckEmailID(emailID)
    if (emailstatus == 0) {
        $('#msg_Error').text('EmailID is already registered');
        onClickOpen('poup_msg')
        emailstatus = 0;
        return false;
    }


   
    
    var DOB = $("#CheckDate").val();
    if (DOB=="") {
        $('#msg_Error').text('Please enter date of birth');
        onClickOpen('poup_msg');
        return false;
    }


    GetDateFormat($("#CheckDate").val());

    if (dobStatus == 0) {
       
        return false;
    }

       var pwd = $('#PSWD').val();
    if (pwd == "") {
        $('#msg_Error').text('Please enter password');
        onClickOpen('poup_msg');
        $("#PSWD").focus();
        return false;
    }


    if (pwd.length < 6) {
        $('#msg_Error').text('Please enter minimum 6 character for password.');
        onClickOpen('poup_msg')
        return false;
    }


    var cpwd = $('#ConfirmPSWD').val();
    if (cpwd == "") {
        $('#msg_Error').text('Please enter confirm password');
        onClickOpen('poup_msg');
        $("#ConfirmPSWD").focus();
        return false;
    }

    if (pwd != cpwd) {
        $('#msg_Error').text('Password does not match');
        onClickOpen('poup_msg');
        $("#ConfirmPSWD").focus();
        return false;
    }


    if ($('#ddlcountry').val() == "0") {
        $('#msg_Error').text("Country is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($("#ValidUpto").val()!="")
 {
    GetDateFormat($("#ValidUpto").val());
    if (dobStatus == 0) {

        return false;
    }

    }

    if (!$('input[name=Status]:checked').length > 0) {
        $('#msg_Error').text('Please select present status.');
        onClickOpen('poup_msg');
        $("input[name=Status]:checked").focus();
        return false;
    }

    if ($('input[name=Status]:checked').val() == "2") {

        if ($('#InstitueEmployed').val() == "0") {
            $('#msg_Error').text("Training/Academic Institute is required");
            onClickOpen('poup_msg');
            return false;
        }
    }


    if ($('#TotalExp').val() == "0") {

        $('#msg_Error').text('Please select total experience');
        onClickOpen('poup_msg');
        $("#TotalExp").focus();
        return false;
    } 
    
   
    if ($('#TrainingExp').val() == "0") {

        $('#msg_Error').text('Please select training experience');
        onClickOpen('poup_msg');
        $("#TotalExp").focus();
        return false;
    }
    

    var Address = $('#txt_Address').val();
    if (Address == "") {

        $('#msg_Error').text('Please enter present address');
        onClickOpen('poup_msg');
        $("#txt_Address").focus();
        return false;
    }
   
    if ($('#HSCStream').val() == "0") {

        $('#msg_Error').text('Please select HSC Stream');
        onClickOpen('poup_msg');
        return false;
    }
    
    if ($('#HSCYear').val() == "0") {

        $('#msg_Error').text('Please select HSC Year');
        onClickOpen('poup_msg');
        return false;
    }

    var HSCCollege = $('#HSCCollege').val();
    if (HSCCollege == "") {

        $('#msg_Error').text('Please input HSC qualifying college');
        onClickOpen('poup_msg');
        return false;
    }

    var HSCYear = $('#HSCYear').val();
    if (HSCYear == "") {

        $('#msg_Error').text('Please select HSC qualifying year');
        onClickOpen('poup_msg');
        return false;
    }


    var HSCData = document.getElementById('HSCfile');
    var FileUploadPathHSC = HSCData.value;
    //To check if user upload any file
    if (FileUploadPathHSC == '') {
        $('#msg_Error').text('Please Upload HSC Certificate');
        onClickOpen('poup_msg');
        return false;
    }


     var flag1 = 0;

    $('.a').each(function () {
        var strCollege = $(this).val();

        if (strCollege != null && strCollege != "") {
            if (!strCollege.match(nameExp)) {
              
                flag1 = 1;
              
            }
            else { }
        }
        else { }
    });
    if (flag1 == 1) {
        flag1 = 0;
        $('#msg_Error').text('Please enter valid college/University name in HSC.');
        onClickOpen('poup_msg');
        return false;
    }
    else { }
   










    if ($('#GraduateStream').val() == "0") {

        $('#msg_Error').text('Please select Graduate Stream');
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#GraduateYear').val() == "0") {

        $('#msg_Error').text('Please select Graduate Year');
        onClickOpen('poup_msg');
        return false;
    }

    var GraduateCollege = $('#GraduateCollege').val();
    if (GraduateCollege == "") {

        $('#msg_Error').text('Please input Graduate qualifying college');
        onClickOpen('poup_msg');
        return false;
    }

    var GraduateYear = $('#GraduateYear').val();
    if (GraduateYear == "") {

        $('#msg_Error').text('Please select Graduate qualifying year');
        onClickOpen('poup_msg');
        return false;
    }


    var GraduateData = document.getElementById('GraduateFile');
    var FileUploadPathGraduate = GraduateData.value;
    //To check if user upload any file
    if (FileUploadPathGraduate == '') {
        $('#msg_Error').text('Please Upload Graduate Certificate');
        onClickOpen('poup_msg');
        return false;
    }


    var flag2 = 0;

    $('.a').each(function () {
        var strCollege2 = $(this).val();

        if (strCollege2 != null && strCollege2 != "") {
            if (!strCollege2.match(nameExp)) {

                flag2 = 1;

            }
            else { }
        }
        else { }
    });
    if (flag2 == 1) {
        flag2 = 0;
        $('#msg_Error').text('Please enter valid college/University name in Graduate.');
        onClickOpen('poup_msg');
        return false;
    }
    else { }





















   
    if (emailstatus == 0) {
        $('#msg_Error').text('Email id alreday registered');
        onClickOpen('poup_msg');
        $("#emailID").focus();
        emailstatus = 1;
        return false;
    }

    $('#Confirm_Error').text("Are u sure to Submit");
    onClickOpen('Confirm_msg');
   
}

function CheckEmailID(email) {
    $.ajax({
        url: "/Utility/CheckEmail",
        method: 'GET',
        data: { emailid: email },
        success: function (data) {

            if (data != "0") {
                $('#msg_Error').text("Duplicate email ID");
                onClickOpen('poup_msg');
                emailstatus = 0;
            }
            else {
                $('#chkemailMsg').text('');
                emailstatus = 1;
            }
        },
        error: function (data) {
            //alert(data.error);
        }
    });
}

function GetDateFormat(dateval) {

    //var dateval = $("#CheckDate").val();
    
    if (dateval != "") {
        //var d1 = Date.parse($('#' + dateval).val().toString().replace(/([0-9]+)\/([0-9]+)/, '$2/$1'));
        var SplitDate = dateval.split('-');

        if (SplitDate.length < 2)
        {
            dobStatus = 0;
            StrdobStatus = "Please enter valid date";
            $('#msg_Error').text(StrdobStatus);
            onClickOpen('poup_msg');
            return false;
        }
        
        var IndexVal = MonthArr.indexOf(SplitDate[1].toLowerCase());

    
        if (!SplitDate[0].match(validday)) {
            dobStatus = 0;
            StrdobStatus = "Please enter valid day ";
            $('#msg_Error').text(StrdobStatus);
            onClickOpen('poup_msg');
            return false;
        }

        var Iday = parseInt(SplitDate[0]);
        if (Iday > 31) {
            dobStatus = 0;
            StrdobStatus = "Please enter valid date";
            $('#msg_Error').text(StrdobStatus);
            onClickOpen('poup_msg');
            return false;
        }

        if (!(IndexVal > -1)) {

            dobStatus = 0;
            StrdobStatus = "Please enter valid month ";
            $('#msg_Error').text(StrdobStatus);
            onClickOpen('poup_msg');
            return false;
        }
        if (!SplitDate[2].match(validyear)) {

            dobStatus = 0;
            StrdobStatus = "Please enter valid year ";
            $('#msg_Error').text(StrdobStatus);
            onClickOpen('poup_msg');
            return false;
        }

        dobStatus = 1;
        return true;

    }
}

function EditPersonalinfoValidation() {

 
    var imagecheck = /\.(jpg|jpeg|png|gif)$/;
    var capImagecheck = /\.(JPG|JPEG|PNG|GIF)$/;

    if ($('#MobileNo').val() == "") {

        $('#msg_Error').text("mobile no. is required");
        onClickOpen('poup_msg');
        return false;
    }

    //if ($('#HomeNo').val() == "") {       
    //    $('#msg_Error').text("Home no. is required");
    //    onClickOpen('poup_msg');
    //    return false;
    //}

    if ($('#ddlcountry').val() == "0") {
        $('#msg_Error').text("Please Select Country");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#ddldivision').val() == "") {

        $('#msg_Error').text("Division Name is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#PresentAddress').val() == "") {       
        $('#msg_Error').text("Present Address is required");
        onClickOpen('poup_msg');
        return false;
    }

    //if ($('#PermanentAddress').val() == "") {

    //    $('#msg_Error').text("Permanent Address is required");
    //    onClickOpen('poup_msg');
    //    return false;
    //}

    //if ($('#IdentificationNo').val() == "") {

    //    $('#msg_Error').text("National ID is required");
    //    onClickOpen('poup_msg');
    //    return false;
    //}
    var txtMobileNo = $('#MobileNo').val();

    if (!txtMobileNo.match(mobileno)) {

        $('#msg_Error').text('Please enter valid mobile number.');
        onClickOpen('poup_msg');
        $("#MobileNo").focus();
        return false;
    }

   
  
    if ($('#userdob').val() == "") {
       
        $('#msg_Error').text("DOB is required");
        onClickOpen('poup_msg');
        return false;
    }

     GetDateFormat($("#userdob").val());

    if (dobStatus == 0) {

        return false;
    }

    if ($('#ddlTotalExperiance').val() == "0") {

        $('#msg_Error').text("Please Select Total Experience");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlTrainingExperience').val() == "0") {

        $('#msg_Error').text("Please Select Training Experience");
        onClickOpen('poup_msg');
        return false;
    }
    

}
function EditReferenceValidation() {

   
    var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    var mobileno = /^\d{10,11}$/;
   
    var emailID = $('#txtReferenceEmailID').val();
    if (emailID != "")
    {

        if (!emailID.match(ck_email)) {

            $('#msg_Error').text('Please enter valid email id');
            onClickOpen('poup_msg')
            return false;
        }
    }
   
    var txtMobileNo = $('#txtReferenceMobileNo').val();
    if (txtMobileNo!= "") {


        if (!txtMobileNo.match(mobileno)) {

            $('#msg_Error').text('Please enter valid mobile number.');
            onClickOpen('poup_msg');

            return false;
        }
    }
}
function EditQualificationValidation() {
  
    CertValidation();
   
    if (certfile == 0) {
        return false;
    }


    EditQualification();
    if (certstatus == 0) {

        return false;

    }
    return true;
}
$('#addQual1').click(function () {

    flag = 0;

    if (flag == 0) {
        $("#AddButton").hide();
        $('#QualTable').show();
        $('#QualTable').append(tritm);
        flag = 1;
      
    }


 

});

$('#addQual').click(function () {

    flg = 0;

    if (flg == 0)
    {
        var tritm = "<tr>" + $("#NewRowCertificate").html() + "</tr>";
        $('#QualTable').append(tritm);

        flg = 1;
        
    }

});

function CertValidation() {
  
    var fuData = document.getElementById('file');
    var FileUploadPath = fuData.value;

    //To check if user upload any file
   
    if (flag==1 || flg==1)
    {
        var fuData = document.getElementById('file');
        var FileUploadPath = fuData.value;

        //To check if user upload any file
        if (FileUploadPath == '') {
            $('#msg_Error').text('Certificate file is mandatory');
            onClickOpen('poup_msg');
            certfile = 0;
            flag = 0;
            flg = 0;
            return false;
        }
    }

    certfile = 1;

    return true;
   
}
function EditQualification() {


for (var intCounter = 0; intCounter < 3; intCounter++)
    {

        if ($('#ddlStream').val() == "0") {

            $('#msg_Error').text('Please select HSC Stream');
            onClickOpen('poup_msg');
            certstatus = 0;
            return false;

        }

        var HSCCollege = $('#txtcentre').val();
        if (HSCCollege == "") {

            $('#msg_Error').text('Please input HSC qualifying college');
            onClickOpen('poup_msg');
            certstatus = 0;
            return false;
        }

       
        if ($('#ddlyear').val() == "0") {

            $('#msg_Error').text('Please select HSC qualifying year');
            onClickOpen('poup_msg');
            certstatus = 0;
            return false;
        }

        certstatus = 1;

    }
    return true;

}


function onClickOpen(divid) {

    document.getElementById(divid).style.display = "block";
}
function onClickClose(divid) {

    document.getElementById(divid).style.display = "none";

}
