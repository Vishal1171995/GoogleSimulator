
$(document).ready(function () {
          var district =$('#ddldistrict').val();
          var division =$('#ddldivision').val();
          var city = $('#ddlcity').val();
          getstudentdetailslocationwise(district, division, city);
      });
function getstudentdetailslocationwise(district, division, city) {
    $.ajax({
        url: "/Report/Home/LocationWiseStudentdetails",
        method: 'GET',
        data: { district: district, division: division },//"{district:'"+ district+ "',division:'"+division +"',city:'"+city +"'}",
        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function (data) {
            if (data.length != 0) {
                $("#Locationdata tr").remove();
                var table = "<table border='1'  id='Locationdata' >";
                var row = '';
                var cnt = 0;
                var user = '';
                var date = '';
                row = '<thead ><tr><th id="id">Login ID</th><th Graduate Id="Name">Graduate Name</th><th id="gndrh" style=display:none;>Gender</th><th id="dob" style=display:none;>Date of Birth</th><th id="email">Email Id</th><th id="mob" style=display:none;>Mobile</th><th id="ftrname" style=display:none;>Father Name</th><th id="addrss" style=display:none;>Address</th><th id="dvsn">Division</th><th id="distrct">District</th><th id="unvsty">University</th><th id="dprtmnt">Department</th><th id="batch" style=display:none;>Batch</th><th id="status">Status</th></tr><tbody>';
                for (var i = 0; i < data.length; i++) {
                    $("#btnExport").css('display', 'block');

                    row += '<tr >';
                    row += '<td align="center" >' + data[i].LOGINID + '</td>';
                    row += '<td align="center" >' + data[i].USERNAME + '</td>';
                    row += '<td  align="center" class="gndr" style=display:none;>' + data[i].GENDER + '</td>';
                    row += '<td align="center"  class="db" style=display:none;>' + data[i].DOB + '</td>';
                    row += '<td align="center" >' + data[i].EMAILID + '</td>';
                    row += '<td  align="center" class="mob" style=display:none;>' + data[i].MOBILE + '</td>';
                    row += '<td align="center"  class="fthr" style=display:none;>' + data[i].FatherName + '</td>';
                    row += '<td align="center"  class="addrss" style=display:none;>' + data[i].ADDRESS + '</td>';
                    row += '<td align="center" >' + data[i].Division_Name + '</td>';
                    row += '<td align="center" >' + data[i].DistrictName + '</td>';
                    row += '<td align="center"  class="cntr">' + data[i].centername + '</td>';
                    row += '<td align="center" >' + data[i].department + '</td>';
                    row += '<td  align="center" class="batch" style=display:none;>' + data[i].BATCHNAME + '</td>';
                    row += '<td align="center"  >' + data[i].Status + '</td>';
                    row += '</tr>';

                }

                  
                table = table + row + '</tbody></table>';

                var tableExport = table;
               
                $('#Location_Studentreport').html(table);
                $("#Location_Studentreport").slideDown("slow");
                $('#Locationdata').after('<div id="nav"></div>');
                paging('Locationdata','nav','page3');
                $('#Studentreport_Locationwise').html(tableExport);
               
            }
            else {

                var v = "No Data Available";
                $('#Location_Studentreport').text(v);
                $("#btnExport").css('display', 'none');
                $("#Location_Studentreport").slideDown("slow");
            }


          
        },
        error: function (data) { alert('test'); }
    });
}

$("#abs_submit").click(function () {
    var district = $('#ddldistrict').val();
    var division = $('#ddldivision').val();
    getstudentdetailslocationwise(district, division);
});


$('#ddldivision').change(function () {
    var divid = $('#ddldivision').val();
    var cnt = 0;
    $('#ddldistrict').empty();
    debugger

    $.ajax({
        type: "GET",
        url: "/Report/Home/Getdistrict",
        data: { divid: divid },
        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function (response) {
            if (divid == 0) {

               // $("#ddldistrict").append($("<option></option>").val('0').html('All'));
            }
            $.each(response, function (Key, value) {
                if (value.ID != "") {
                    if (cnt == 0) {
                        $("#ddldistrict").append($("<option></option>").val('0').html('All'));
                    }


                    $("#ddldistrict").append($("<option></option>").val(value.ID).html(value.DistrictName));
                    cnt++;
                }


            });

            if (cnt == 0) {
                $("#ddldistrict").append($("<option></option>").val('0').html('All'));
            }
        }
    });
});

           

        
$("#btnExport").click(function (e) {
    //creating a temporary HTML link element (they support setting file names)
    var a = document.createElement('a');
    //getting data from our div that contains the HTML table
    var data_type = 'data:application/vnd.ms-excel';
    var table_div = document.getElementById('Studentreport_Locationwise');
    var table_html = table_div.outerHTML.replace(/ /g, '%20');
    a.href = data_type + ', ' + table_html;
    //setting the file name
    a.download = 'LocationWiseGraduatedetails.xls';
    //triggering the function
    a.click();
    //just in case, prevent default behaviour
    e.preventDefault();
    
});


       

