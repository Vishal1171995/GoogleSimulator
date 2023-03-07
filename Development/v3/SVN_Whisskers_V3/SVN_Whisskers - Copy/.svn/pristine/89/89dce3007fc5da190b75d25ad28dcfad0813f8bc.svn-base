$(document).ready(function () {
    var funid = $('#ddlfun').val();
    var empid = $('#ddlemplyr').val();
  
    getemployerwisestudentdetails(funid, empid)
});


$('#ddlfun').change(function () {
    var id = $('#ddlfun').val();
    var cnt = 0;

    $('#ddlemplyr').empty();

    $.ajax({
        type: "GET",
        url: "/Report/Home/getemployerbyfunarea",
        data: { id: id },
        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function (response) {
            $.each(response, function (Key, value) {
                if (value.USERID != "") {
                    if (cnt == 0) {
                        $("#ddlemplyr").append($("<option></option>").val('0').html('All'));

                    }

                    $("#ddlemplyr").append($("<option></option>").val(value.USERID).html(value.USERNAME));
                    cnt++;
                }
            });

            if (cnt == 0) {
                $("#ddlemplyr").append($("<option></option>").val('0').html('All'));
            }
        }
    });
});


$("#submit").click(function () {

  
    var funid = $('#ddlfun').val();
    var emp = $('#ddlemplyr').val();
    if (emp !== '--Select--') {
        getemployerwisestudentdetails(funid, emp)
    }
    else {

        var msg = 'Please Select another Functional Area';
        $('#msg_Error').text(msg);
        onClickOpen('poup_msg');
        return false;
    }


});

function getemployerwisestudentdetails(funid, empid) {
    debugger
    $.ajax({
        url: "/Report/Home/getemployerwisestudent",
        method: 'GET',
        data: { funid: funid, empid: empid},//"{district:'"+ district+ "',division:'"+division +"',city:'"+city +"'}",
        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function (data) {
            if (data.length != 0) {
                $("#Student_data tr").remove();
                var table = "<table border='1' id='Student_data'>";
                var row = '';
                var cnt = 0;
                var user = '';
                var date = '';
                row = '<thead ><tr><th id="emp">Employer</th><th  Id="funName">Functional Area Name</th>' +
                     '<th id="job" style=display:none;>Job Tittle</th>' +
                     '<th  Id="Id">Graduate ID</th>' +
                    '<th id="gName">Graduate Name</th><th id="emid" style=display:none; >Email Id</th>' +
                    '<th id="mob">Mobile</th><th id="bth" >Batch Name</th>' +
                    '<th id="status">Status</th>' +
                    '<th id="istitut">Institute</th>' +
                   ' </tr><tbody>';
                for (var i = 0; i < data.length; i++) {
                    $("#btnExport").css('display', 'block');
                    row += '<tr >';
                    row += '<td align="center" >' + data[i].Employer + '</td>';
                    row += '<td align="center" >' + data[i].FunctionalAreaName + '</td>';
                    row += '<td align="center"  class="tittle" style=display:none;>' + data[i].jobtitle + '</td>';
                    row += '<td align="center"  >' + data[i].LOGINID + '</td>';
                    row += '<td align="center"  >' + data[i].StudentName + '</td>';
                    row += '<td align="center"  style=display:none;>' + data[i].EMAILID + '</td>';
                    row += '<td align="center"  >' + data[i].MOBILE + '</td>';
                    row += '<td align="center"  >' + data[i].BATCHNAME + '</td>';
                    row += '<td align="center"  >' + data[i].Status + '</td>';
                    row += '<td align="center"  >' + data[i].institute + '</td>';
                    row += '</tr>';

                }
                table = table + row + '</tbody></table>';
                var tableExport_employer = table;
                $('#studentReport').html(table);
                $("#studentReport").slideDown("slow");
                //  $('#data').after('<div id="prvs"></div>');
                $('#Student_data').after('<div id="nav"></div>');
                paging('Student_data','nav','page2');
                $('#Studentreport_Employer').html(tableExport_employer);
            }
            else {

                var v = "No Data Available";
                $('#studentReport').text(v);
                $("#btnExport").css('display', 'none');
                $("#studentReport").slideDown("slow");
            }

          

        },
        error: function (data) { alert('test'); }
    });

  

}

$("#btnExport").click(function (e) {

    //creating a temporary HTML link element (they support setting file names)
    var a = document.createElement('a');
    //getting data from our div that contains the HTML table
    var data_type = 'data:application/vnd.ms-excel';
    var table_div = document.getElementById('Studentreport_Employer');
    var table_html = table_div.outerHTML.replace(/ /g, '%20');
    a.href = data_type + ', ' + table_html;
    //setting the file name
    a.download = 'EmployerWiseGraduatedetails.xls';
    //triggering the function
    a.click();
    //just in case, prevent default behaviour
    e.preventDefault();

});