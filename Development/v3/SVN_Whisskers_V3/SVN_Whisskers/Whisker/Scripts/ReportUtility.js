function ReportFeedbackValidation() {

    if ($('#StartDate').val() == "") {

        $('#msg_Error').text("Please Enter Start Date");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#EndDate').val() == "") {

        $('#msg_Error').text("Please Enter End Date");
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
