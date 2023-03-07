
function validatetime(val)
{
    
    var strval = val;
    var strval1;

    //minimum lenght is 6. example 1:2 AM
    if (strval.length < 3) {
        $('#msg_Error').text("Invalid time. Time format should be HH:MM.");
        onClickOpen('poup_msg');
        return false;
    }

    //Maximum length is 8. example 10:45 AM
    if (strval.length > 5) {
        $('#msg_Error').text("invalid time. Time format should be HH:MM.");
        onClickOpen('poup_msg');
        return false;
    }

    //Removing all space
    strval = trimAllSpace(strval);

    ////Checking AM/PM
    //if (strval.charAt(strval.length - 1) != "M" && strval.charAt(
    //    strval.length - 1) != "m") {
    //    alert("Invalid time. Time shoule be end with AM or PM.");
    //    return false;
    //}
    //else if (strval.charAt(strval.length - 2) != 'A' && strval.charAt(
    //    strval.length - 2) != 'a' && strval.charAt(
    //    strval.length - 2) != 'p' && strval.charAt(strval.length - 2) != 'P') {
    //    alert("Invalid time. Time shoule be end with AM or PM.");
    //    return false;
    //}

    //Give one space before AM/PM

    strval1 = strval.substring(0, strval.length - 2);
    strval1 = strval1 + '' + strval.substring(strval.length - 2, strval.length)

    strval = strval1;

    var pos1 = strval.indexOf(':');
    //document.Form1.TextBox1.value = strval;

    if (pos1 < 0) {
        $('#msg_Error').text("invalid time. A colon(:) is missing between hour and minute.");
        onClickOpen('poup_msg');
        return false;
    }
    else if (pos1 > 2 || pos1 < 1) {
        $('#msg_Error').text("invalid time. Time format should be HH:MM.");
        onClickOpen('poup_msg');
        return false;
    }

    //Checking hours
    var horval = trimString(strval.substring(0, pos1));

    if (horval == -100) {
        $('#msg_Error').text("Invalid time. Hour should contain only integer value (0-23).");
        onClickOpen('poup_msg');
        return false;
    }

    if (horval > 23) {
        $('#msg_Error').text("Invalid time. Hour can not be greater that 24.");
        onClickOpen('poup_msg');
        return false;
    }
    else if (horval < 0) {
        $('#msg_Error').text("Invalid time. Hour can not be less than 0.");
        onClickOpen('poup_msg');
        return false;
    }
    //Completes checking hours.

    //Checking minutes.
    var minval = trimString(strval.substring(pos1 + 1, pos1 + 3));

    if (minval == -100) {
        $('#msg_Error').text("Invalid time. Minute should have only integer value (0-59).");
        onClickOpen('poup_msg');
        return false;
    }

    if (minval > 59) {
        $('#msg_Error').text("Invalid time. Minute can not be more than 59.");
        onClickOpen('poup_msg');
        return false;
    }
    else if (minval < 0) {
        $('#msg_Error').text("Invalid time. Minute can not be less than 0.");
        onClickOpen('poup_msg');
        return false;
    }

    //Checking minutes completed.

    //Checking one space after the mintues 
    //minpos = pos1 + minval.length + 1;
    //if (strval.charAt(minpos) != ' ') {
    //    alert("Invalid time. Space missing after minute.Time should have HH:MM AM/PM format.");
    //    return false;
    //}

    return true;
}

function trimAllSpace(str) {
    var str1 = '';
    var i = 0;
    while (i != str.length) {
        if (str.charAt(i) != ' ')
            str1 = str1 + str.charAt(i); i++;
    }
    return str1;
}


function trimString(str) {
    var str1 = '';
    var i = 0;
    while (i != str.length) {
        if (str.charAt(i) != ' ') str1 = str1 + str.charAt(i); i++;
    }
    var retval = IsNumeric(str1);
    if (retval == false)
        return -100;
    else
        return str1;
}

function IsNumeric(strString) {
    var strValidChars = "0123456789";
    var strChar;
    var blnResult = true;
    //var strSequence = document.frmQuestionDetail.txtSequence.value; 
    //test strString consists of valid characters listed above 
    if (strString.length == 0)
        return false;
    for (i = 0; i < strString.length && blnResult == true; i++) {
        strChar = strString.charAt(i);
        if (strValidChars.indexOf(strChar) == -1) {
            blnResult = false;
        }
    }
    return blnResult;
}