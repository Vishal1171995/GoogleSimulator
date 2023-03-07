function CallCentreValidation() {
    var val = $("input[name='RDB']:checked").val();
    if ($("input[name='RDB']:checked").val() == "1")
    {
    
        if ($('#Remark').val() == "") {

            $('#msg_Error').text("Please Enter Remarks");
            onClickOpen('poup_msg');
            return false;
        }

        else if ($('#Feedback').val() == "") {

            $('#msg_Error').text("Please Enter Feedback");
            onClickOpen('poup_msg');
            return false;
        }

        else
            return true;
    }

    if ($("input[name='RDB']:checked").val() == null)
    {
            $('#msg_Error').text("Please Select Graduate Avalaible Or Not");
            onClickOpen('poup_msg');
            return false;
    }
}

function onClickOpen(divid) {

    document.getElementById(divid).style.display = "block";
}
function onClickClose(divid) {

    document.getElementById(divid).style.display = "none";

}
