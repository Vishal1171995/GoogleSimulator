

    $(document).ready(function () {
        
        var instituteid = $('#ddlinstitue').val();
       
        getstudentdetailsinstitutewise( instituteid);
    });
function getstudentdetailsinstitutewise(instituteid) {
    $.ajax({
        url: "/Report/Home/InstituteWiseStudentdetails",
        method: 'GET',
        data: {instituteid: instituteid },//"{district:'"+ district+ "',division:'"+division +"',city:'"+city +"'}",
        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function (data) {
            if (data.length != 0) {
                $("#instute tr").remove();
                //$("#Div_btndwnload").css('display', 'block');
                var table = "<table border='1'  id='instute'>";
                var row = '';
                var cnt = 0;
                var user = '';
                var date = '';
                row = '<thead ><tr><th id="id">Login ID</th><th Graduate Id="Name">Graduate Name</th><th id="gndrh" style=display:none;>Gender</th><th id="dob" style=display:none;>Date of Birth</th><th id="email">Email Id</th><th id="mob" style=display:none;>Mobile</th><th id="ftrname" style=display:none;>Father Name</th><th id="addrss" style=display:none;>Address</th><th id="instute">Institute</th><th id="distrct">Graduate District</th><th id="dvsn" style=display:none;>Graduate Division</th><th id="unvsty" style=display:none;>University</th><th id="dprtmnt" style=display:none;>Department</th><th id="batch" style=display:none;>Batch</th><th id="status">Status</th></tr><tbody>';
                for (var i = 0; i < data.length; i++) {
                    $("#btnExport").css('display', 'block');
                    row += '<tr >';
                    row += '<td align="center" >' + data[i].LOGINID + '</td>';
                    row += '<td align="center" >' + data[i].USERNAME + '</td>';
                    row += '<td  align="center" class="gndr" style=display:none;>' + data[i].GENDER + '</td>';
                    row += '<td align="center"  class="db" style=display:none;>' + data[i].dob + '</td>';
                    row += '<td align="center" >' + data[i].EMAILID + '</td>';
                    row += '<td align="center"  class="mob" style=display:none;>' + data[i].mobile + '</td>';
                    row += '<td  align="center" class="fthr" style=display:none;>' + data[i].FatherName + '</td>';
                    row += '<td  align="center" class="addrss" style=display:none;>' + data[i].ADDRESS + '</td>';
                    row += '<td  align="center" >' + data[i].InstituteName + '</td>';
                    row += '<td align="center" >' + data[i].DistrictName + '</td>';
                    row += '<td align="center"  style=display:none;>' + data[i].Division_Name + '</td>';
                    row += '<td  align="center" class="cntr" style=display:none;>' + data[i].centername + '</td>';
                    row += '<td align="center"  style=display:none;>' + data[i].department + '</td>';
                    row += '<td align="center"  class="batch" style=display:none;>' + data[i].BATCHNAME + '</td>';
                    row += '<td align="center"  >' + data[i].Status + '</td>';
                    row += '</tr>';

                }


                table = table + row + '</tbody></table>';
                var tableExport_institute = table;
                $('#studentReport_inst').html(table);
                $("#studentReport_inst").slideDown("slow");
                $('#instute').after('<div id="nav"></div>');
                paging('instute','nav','page');
                $('#Studentreport_Institute').html(tableExport_institute);
             
            }
            else {

                var v = "No Data Available";
                $('#studentReport_inst').text(v);

                $("#btnExport").css('display', 'none');
                $("#studentReport_inst").slideDown("slow");
            }

          
        },
        error: function (data) { alert('test'); }
    });
}


$("#abs_submit").click(function () {
      
    var institute = $('#ddlinstitue').val();
    if (institute != '--Select--') {
        getstudentdetailsinstitutewise(institute);
    }
    else
    {
           
        var msg = 'Please Select Institue';
        $('#msg_Error').text(msg);
        onClickOpen('poup_msg');
        return false;
    }
});

$("#btnExport").click(function (e) {
    //creating a temporary HTML link element (they support setting file names)
    var a = document.createElement('a');
    //getting data from our div that contains the HTML table
    var data_type = 'data:application/vnd.ms-excel';
    var table_div = document.getElementById('Studentreport_Institute');
    var table_html = table_div.outerHTML.replace(/ /g, '%20');
    a.href = data_type + ', ' + table_html;
    //setting the file name
    a.download = 'InstituteWiseGraduatedetails.xls';
    //triggering the function
    a.click();
    //just in case, prevent default behaviour
    e.preventDefault();

});

