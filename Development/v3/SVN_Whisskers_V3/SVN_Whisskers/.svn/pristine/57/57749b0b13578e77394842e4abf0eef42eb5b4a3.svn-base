var emailstatus = false;
var validday = /^[0-9]{0,2}$/;
var validyear = /^[0-9]{4,4}$/;
var MonthArr = ["jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec"];
var dobStatus = false;
var StrdobStatus = null;
var mobileno = /^\d{10,11}$/;

$(document).ready(function () {

    $("body").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
        }
    });

    $(".NoOnly").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^\d].+/, ""));

        if (event.which == 8)
        {

        }
        else  if ((event.which < 48 || event.which > 57) ) {
            event.preventDefault();
        }
    });
});

function FeedbackValidation() {

    if ($('#ddlSession').val() == "0") {
        $('#msg_Error').text("Please Select Session");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlFaculty').val() == "0") {

        $('#msg_Error').text("Please Select Faculty");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#txtMessage').val() == "") {

        $('#msg_Error').text("Message is required");
        onClickOpen('poup_msg');
        return false;
    }

    return true;
}
function ValidateFileUpload() {
    var fuData = document.getElementById('Imagefile');
    var FileUploadPath = fuData.value;

    //To check if user upload any file

    var Extension = FileUploadPath.substring(
            FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

    //The file uploaded is an image

    if (Extension == "gif" || Extension == "png" || Extension == "bmp"
                        || Extension == "jpeg" || Extension == "jpg") {

        // To Display
        if (fuData.files && fuData.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#blah').attr('src', e.target.result);
            }

            reader.readAsDataURL(fuData.files[0]);
        }

    }

        //The file upload is NOT an image
    else {
        $('#msg_Error').text("Only '.jpeg','.jpg', '.png', '.gif', '.bmp' formats are allowed.");
        onClickOpen('poup_msg');

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
    var FileSize = 0;
    if (fieldObj.value != "") {
        FileSize = fieldObj.files[0].size;
    }
    //var FileSizeMB = (FileSize / 10485760).toFixed(2);


    if ((FileExt != "jpg" && FileExt != "jpeg" && FileExt != "png") || FileSize > 1048576) {
        //var error = "File type : " + FileExt + "\n\n";
        //error += "Size: " + FileSizeMB + " MB \n\n";
        //error += "Please make sure your file is in pdf or doc format and less than 1 MB.\n\n";
        $('#msg_Error').text('Please make sure your file format is in .png, .jpeg, .jpg and file is less than 1 MB');
        onClickOpen('poup_msg');
        fieldObj.value = "";
        return false;

    }
    return true;
}

function checkCV(fieldObj) {
    var FileName = fieldObj.value;
    var FileExt = FileName.substr(FileName.lastIndexOf('.') + 1);
    var FileSize = 0;
    if (fieldObj.value != "") {
        FileSize = fieldObj.files[0].size;
        if (FileSize == 0 && FileSize == "")
        {
            $('#msg_Error').text('CV is mandatory');
            onClickOpen('poup_msg');
            return false;
        }
    }
    //var FileSizeMB = (FileSize / 10485760).toFixed(2);


    if ((FileExt != "doc" && FileExt != "docx"  ) || FileSize > 1048576) {
        //var error = "File type : " + FileExt + "\n\n";
        //error += "Size: " + FileSizeMB + " MB \n\n";
        //error += "Please make sure your file is in pdf or doc format and less than 1 MB.\n\n";
        $('#msg_Error').text('Please make sure your file format is in .doc, .docx and file is less than 1 MB');
        onClickOpen('poup_msg');
        fieldObj.value = "";
        return false;

    }
    return true;
}

function EditPersonalinfoValidation()
{
    
    var phoneno = /^\+?([8-9]{2})\)?[-. ]?([0-9]{11})$/;
    var mobileno = /^\d{10,11}$/;
    var imagecheck = /\.(jpg|jpeg|png|gif)$/;
    var capImagecheck = /\.(JPG|JPEG|PNG|GIF)$/;

    if ($('#MobileNo').val() == "") {
        
        $('#msg_Error').text("mobile no. is required");
        onClickOpen('poup_msg');
        return false;
    }

    var txtMobileNo = $('#MobileNo').val();

    if (!txtMobileNo.match(mobileno)) {
       
        $('#msg_Error').text('Please enter valid mobile number.');
        onClickOpen('poup_msg');
        $("#MobileNo").focus();
        return false;
    }

    //if ($('#HomeNo').val() == "") {

    //    $('#msg_Error').text("Home no. is required");
    //    onClickOpen('poup_msg');
    //    return false;
    //}

    if ($('#ddldivision').val() == "") {

        $('#msg_Error').text("Division Name is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddldistrict').val() == "0") {

        $('#msg_Error').text("District Name is required");
        onClickOpen('poup_msg');
        return false;
    }
    //if ($('#ddlCity1').val() == "") {

    //    $('#msg_Error').text("City Name is required");
    //    onClickOpen('poup_msg');
    //    return false;
    //}

    if ($('#PresentAddress').val() == "") {

        $('#msg_Error').text("Present Address is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#PermanentAddress').val() == "") {

        $('#msg_Error').text("Permanent Address is required"); 
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#IdentificationNo').val() == "") {

        $('#msg_Error').text("National ID is required");
        onClickOpen('poup_msg');
        return false;
    }

    GetDateFormat();
    if (dobStatus == 0) {
        return false;
    }


    if ($('#userdob').val() == "") {
        $('#msg_Error').text("DOB is required");
        onClickOpen('poup_msg');
        return false;
    }




    var currentTime = new Date();
    var startDateFrom = new Date(currentTime.getFullYear() - 35, currentTime.getMonth(), 1);
    var startDateTo = new Date(currentTime.getFullYear() - 15, currentTime.getMonth(), 0);



    var dob = $('#userdob').val();
    var Userdob = new Date(dob);
    var Dobyear = Userdob.getFullYear();
    var StartDateFromyear = new Date(startDateFrom);
    var sdyear1 = StartDateFromyear.getFullYear();
    var StartDateToyear = new Date(startDateTo);
    var sdyear2 = StartDateToyear.getFullYear();

    if (Dobyear < sdyear1 || Dobyear > sdyear2) {


        $('#msg_Error').text("Your age should be betwwen 15 to 35 years");
        onClickOpen('poup_msg');
        return false;
    }



}



function EditReferenceValidation()
{
    var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    var mobileno = /^\d{10,11}$/;
    //var txtMobileNo = $('#txtFatherMobileNo').val();
    var emailID = $('#txtFatherEmailID').val();

    if (emailID != "")
    {

      
    }
    else
    {
        //if (!emailID.match(ck_email))
        //{

        //    $('#msg_Error').text('Please enter valid Father email id');
        //    onClickOpen('poup_msg')
        //    return false;
        //}
    }
    var txtMobileNo = $('#txtFatherMobileNo').val();
 
    if (txtMobileNo != "")
    {
        if (!txtMobileNo.match(mobileno)) {

            $('#msg_Error').text('Please enter valid Father mobile number.');
            onClickOpen('poup_msg');

            return false;
        }
    }
    else
    {

        
    }
}

//function SubmitData(s)
//{
//    alert(s);
//    if (s == 1)
//    {
//       return Validation();
//    }
//    else
//    {
//        return false;
//    }

//}

function Validation() {  
 

    var nameExp = /^[A-Za-z .]{3,250}$/;
    var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    var phoneno = /^\+?([8-9]{2})\)?[-. ]?([0-9]{11})$/;
    var mobileno = /^\d{10,11}$/;
    var imagecheck = /\.(jpg|jpeg|png|gif)$/;
    var capImagecheck = /\.(JPG|JPEG|PNG|GIF)$/;

  

    if ($('#Stufname').val().trim() == "") {
        
        $('#msg_Error').text("Graduate name is required");
        onClickOpen('poup_msg');
        return false;
    }

    var val = $("#Stufname").val().trim();
    if (!val.match(nameExp)) {

        $('#msg_Error').text('Please enter valid name');
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#MobileNo').val() == "") {
        $('#msg_Error').text("Mobile number is required");
        onClickOpen('poup_msg');
        return false;
    }

    var txtMobileNo = $('#MobileNo').val();

    if (!txtMobileNo.match(mobileno)) {

        $('#msg_Error').text('Please enter valid mobile number.');
        onClickOpen('poup_msg');
        $("#MobileNo").focus();
        return false;
    }

    

    if ($("input[name='inlineRadioOptions']:checked").val()) {

    }
    else {
        $('#msg_Error').text("Please Select Gender");
        onClickOpen('poup_msg');
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

    if ($("input[name='StatusRadio']:checked").val()) {

    }
    else {
        $('#msg_Error').text("Present Status is Required");
        onClickOpen('poup_msg');
        return false;
    }

    var Statusvalue = $("input[name='StatusRadio']:checked").val();
 
   if (Statusvalue == "Studying")
    {
      
        if ($('#ddlInstitute').val() == "")
        {
            $('#msg_Error').text("Training/Academic Institute is required");
            onClickOpen('poup_msg');
            return false;
        }

        else if ($('#ddlInstitute').val() == "0")
        {

            if ($('#University').val() == "")
           {
            
            $('#msg_Error').text("University is Required");
            onClickOpen('poup_msg');
            return false;
          }

        }

        if ($('#ddlDept').val() == "") {
            $('#msg_Error').text("Department is required");
            onClickOpen('poup_msg');
            return false;
        }
        else if ($('#ddlDept').val() == "0") {

            if ($('#txtDept').val() == "") {

                $('#msg_Error').text("Department is Required");
                onClickOpen('poup_msg');
                return false;
            }

        }

    }

    if ($('#userdob').val() == "") {
        $('#msg_Error').text("DOB is required");
        onClickOpen('poup_msg');
        return false;
    }


    GetDateFormat();
    if (dobStatus == 0) {
            return false;
    }

    var currentTime = new Date();
    var startDateFrom = new Date(currentTime.getFullYear() - 35, currentTime.getMonth(), 1);
    var startDateTo = new Date(currentTime.getFullYear() - 15, currentTime.getMonth(), 0);

   

    var dob = $('#userdob').val();
    var Userdob = new Date(dob);
    var Dobyear = Userdob.getFullYear();
    var StartDateFromyear = new Date(startDateFrom);
    var sdyear1 = StartDateFromyear.getFullYear();
    var StartDateToyear = new Date(startDateTo);
    var sdyear2 = StartDateToyear.getFullYear();

    if (Dobyear < sdyear1 || Dobyear > sdyear2) {
       
     
        $('#msg_Error').text("Your age should be betwwen 15 to 35 years");
        onClickOpen('poup_msg');
        return false;
    }

  

    if ($('#ddlDivision').val() == "0")
    {
        $('#msg_Error').text("Division is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlCity').val() == "0")
    {
        $('#msg_Error').text("City is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddldistrict').val() == "0") {
        $('#msg_Error').text("District is required");
        onClickOpen('poup_msg');
        return false;
    }
   
    if ($('#IdentificationNo').val() == "") {
        $('#msg_Error').text("National ID is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#EmailID').val() == "") {
        $('#msg_Error').text("EmailID is required");
        onClickOpen('poup_msg');
        return false;
    }

    var emailID = $('#EmailID').val();
    if (!emailID.match(ck_email)) {
        $('#msg_Error').text('Please enter valid email id');
        onClickOpen('poup_msg')
        return false;
    }


    CheckEmailID(emailID)
    if (emailstatus == 0) {
        emailstatus = 0;
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


    var confirmpwd = $('#ConfirmPassword').val().trim();

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

    if ($('#PresentAddress').val() == "") {
        $('#msg_Error').text("Present Address is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlTrainingType').val() == "0") {
        $('#msg_Error').text("Training Type is required");
        onClickOpen('poup_msg');
        return false;
    }
   
    var emailID = $('#FatherEmailID').val();
    if (emailID == "")
    {


    }
    else if (!emailID.match(ck_email))
    {
        $('#msg_Error').text('Please enter valid Father email id');
        onClickOpen('poup_msg')
        return false;
    }

    var txtFatherMobileNo = $('#FatherMobileNo').val();

    if (txtFatherMobileNo == "")
    {
     
    }
    else if (!txtFatherMobileNo.match(mobileno)) 
    {

            $('#msg_Error').text('Please enter valid Father mobile number.');
            onClickOpen('poup_msg');

            return false;
    }


    if ($('#HSCStream').val() == "0") {
        $('#msg_Error').text("HSC Stream is required");
        onClickOpen('poup_msg');
        return false;
    }

    
    if ($('#txtcollege').val() == "") {
        $('#msg_Error').text("HSC College is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlYear').val() == "0") {
        $('#msg_Error').text("HSC Year is required");
        onClickOpen('poup_msg');
        return false;
    }
    
    $('#Confirm_Error').text("Are u sure to Submit");
    onClickOpen('Confirm_msg');

   
}

function TestValidation() {

    if ($('#Date').val() == "") {

        $('#msg_Error').text("Exam date is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlcentre').val() == 0) {

        $('#msg_Error').text("Centre is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlCity').val() == 0) {
        $('#msg_Error').text("City is required");
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
            //alert(data)
            if (data != "0") {
                $('#msg_Error').text("Duplicate email ID");
                onClickOpen('poup_msg');
                //$('#chkemailMsg').html('Duplicate email ID');
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

var validday = /^[0-9]{0,2}$/;
var validyear = /^[0-9]{4,4}$/;

function GetDateFormat() {

   
    var dateval = $("#userdob").val();
    if (dateval != "") {
        //var d1 = Date.parse($('#' + dateval).val().toString().replace(/([0-9]+)\/([0-9]+)/, '$2/$1'));
        var SplitDate = dateval.split('-');

        if (SplitDate.length < 2) {
            dobStatus = 0;
            StrdobStatus = "Please enter valid date ";
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




function onClickOpen(divid) {

    document.getElementById(divid).style.display = "block";
}


function onClickClose(divid) {

    document.getElementById(divid).style.display = "none";

}
