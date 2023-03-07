function CheckEmailID(TestDate, TestTime, centerID) {

    if (TestDate == "" || TestTime == "" || centerID == "") {
        $('#chkemailMsg').html('Please select all fields.');
        return false;
      }

    $.ajax({
        url: "/Utility/CheckTestDetails",
        method: 'GET',
        data: { TestDate: TestDate, TestTime: TestTime, centerID: centerID },
        success: function (data) {

            if (data != "0") {
                $('#chkemailMsg').html('This test has already exist.');
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