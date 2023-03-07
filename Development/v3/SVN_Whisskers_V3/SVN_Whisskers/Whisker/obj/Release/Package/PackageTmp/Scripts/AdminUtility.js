var nameExp = /^[A-Za-z ]{3,250}$/;
var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
var phoneno = /^\d{10,13}$/;
var ii = "0";
function CourseValidation() {

    var val = $("#CourseName").val();
    if (!val.match(nameExp)) {

        $('#msg_Error').text('Please enter valid course name.');
        onClickOpen('poup_msg');
        $("#CourseName").focus();
        return false;
    }

    if (!$('input[name=chkSession]:checked').length > 0) {

        $('#msg_Error').text('Please select session.');
        onClickOpen('poup_msg');
        return false;
    }
}
$(document).ready(function () {
    //$('#ScheduleStartDate').datepicker({
    //    dateFormat: 'dd-M-yy'
    //});

    //$('#ScheduleEndDate').datepicker({
    //    dateFormat: 'dd-M-yy'
    //});

    var Sdate = $('#Startdate').val();
    var Edate = $('#EndDate').val();
    var AEdate = $('#Approxenddate').val();

    if (AEdate != 'N/A') {
        $('#ScheduleStartDate').datepicker({
            dateFormat: 'dd-M-yy',
            minDate: Sdate,
            onSelect: function (dat, inst) {
                $('#ScheduleEndDate').datepicker('option', 'minDate', dat);
            },
            maxDate: AEdate
        });

        $('#ScheduleEndDate').datepicker({

            dateFormat: 'dd-M-yy',
            maxDate: AEdate

        });
    }
    else {
        $('#ScheduleStartDate').datepicker({
            dateFormat: 'dd-M-yy',
            minDate: Sdate,
            onSelect: function (dat, inst) {
                $('#ScheduleEndDate').datepicker('option', 'minDate', dat);
            },
            maxDate: Edate
        });

        $('#ScheduleEndDate').datepicker({

            dateFormat: 'dd-M-yy',
            maxDate: Edate

        });
    }





});
function BatchValidation() {
  
    if ($('#ddlprogramtype').val() == "0") {

        $('#msg_Error').text("Program type is required");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlCity').val() == "0") {

        $('#msg_Error').text("Please Select City");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#ddlInstitute').val() == "0") {

        $('#msg_Error').text("Please Select Institute");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#ddlslot').val() == "0") {

        $('#msg_Error').text("Please Select Institute Slot");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#ddltrainer').val() == "0") {

        $('#msg_Error').text("Please Select Faculty");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#Startdate').val() == "") {

        $('#msg_Error').text("Please Select Start Date");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#EndDate').val() == "") {

        $('#msg_Error').text("Please Select End Date");
        onClickOpen('poup_msg');
        return false;
    }

    if ($('#EndDate').val() == "") {

        $('#msg_Error').text("Please Select End Date");
        onClickOpen('poup_msg');
        return false;
    }

    
    if ($('#ddlInstitute').val() == "0") {

        $('#msg_Error').text("Please Select Institute");
        onClickOpen('poup_msg');
        return false;
    }

    return true;
   
}
function SpecialisedBatchValidation() {
    var BatchID = $('#HdnBatchID').val();
    var StartDate = $('#Startdate').val();
    var EndDate = $('#EndDate').val();
   


    if ($('#ddlSessionName').val() == "0") {

        $('#msg_Error').text("Please Select Course Type");
        onClickOpen('poup_msg');
        return false;
    }

   

    if ($('#Startdate').val() == "") {

        $('#msg_Error').text("Please Select Start Date");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#EndDate').val() == "") {

        $('#msg_Error').text("Please Select End Date");
        onClickOpen('poup_msg');
        return false;
    }
    //if (EndDate < StartDate)
    //{
        
    //    $('#msg_Error').text("End date can't be less than start date");
    //    onClickOpen('poup_msg');
    //    return false;
    //}
    //$.ajax({
    //    url: "/Admin/SpecialisedBatch/CheckSessionDate",
    //    method: 'GET',
    //    async: false,
    //    data: { BatchID: BatchID, SessionStartdate: StartDate, SessionEnddate: EndDate },
    //    success: function (data) {


    //        ii = data;

    //    },

    //    error: function (data) {  }

    //});

    //if (ii == "0") {
    //    $('#msg_Error').text("Specialized Batch Session should be between Start Date and End Date");
    //    onClickOpen('poup_msg');
    //    return false;
    //}


    return true;
}

function CheckSPSessionDate() {

    var BatchID = $('#BatchID').val();
    var SessionStartDate = $('#trainerstartdate1').val();
    var SessionEndDate = $('#trainerenddate1').val();


    if ($('#ddlsessionlist').val() == "0") {

        $('#msg_Error').text("Please Select Session");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#trainerstartdate1').val() == '') {

        $('#msg_Error').text("Start Date is Required");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#trainerenddate1').val() == '') {

        $('#msg_Error').text("End Date is Required");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#ddlsloat').val() == "0") {

        $('#msg_Error').text("Please Select Slot");
        onClickOpen('poup_msg');
        return false;
    }

    //if (EndDate < StartDate) {

    //    $('#msg_Error').text("End date can't be less than start date");
    //    onClickOpen('poup_msg');
    //    return false;
    //}
    //$.ajax({
    //    url: "/Admin/SpecialisedBatch/CheckSessionDate",
    //    method: 'GET',
    //    async: false,
    //    data: { BatchID: BatchID, SessionStartdate: SessionStartDate, SessionEnddate: SessionEndDate },
    //    success: function (data) {
         
          
    //        ii = data;

    //    },

    //    error: function (data) { alert(data.error); }

    //});

    //if (ii == "0") {
    //    $('#msg_Error').text("Specialized Batch Session should be between Start Date and End Date");
    //    onClickOpen('poup_msg');
    //    return false;
    //}


    return true;

}

function MessageValidation()
{
    if ($('#Subject').val() == "") {

        $('#msg_Error').text("Subject is Required");
        onClickOpen('poup_msg');
        return false;
    }
    if ($('#Message').val() == "") {

        $('#msg_Error').text("Message is Required");
        onClickOpen('poup_msg');
        return false;
    }
}


