
$(document).ready(function () {
    var trngtype = $('#ddltrngtype').val();
    var batch = $('#ddlbtch').val();
    var trainerid = $('#ddltranr').val();
    gettrainerwisestudentdetails(trngtype, batch, trainerid)
});


$('#ddltrngtype').change(function () {
    var id = $('#ddltrngtype').val();
    var cnt = 0;
    var trnr = 0;
    $('#ddlbtch').empty();
   // $('#ddltranr').empty();
    $.ajax({
        type: "GET",
        url: "/Report/Home/getbatchbytrngtype",
        data: { id: id },
        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function (response) {
            var arr = new Array();
            $.each(response, function (Key, value) {
                if (value.ID != "") {
                    if (cnt == 0) {
                        $("#ddlbtch").append($("<option></option>").val('0').html('All'));
                       // $("#ddltranr").append($("<option></option>").val('0').html('All'));
                     
                    }

                    $("#ddlbtch").append($("<option></option>").val(value.ID).html(value.BATCHNAME));
                    cnt++;
                }
            });

            var temp = '';
            //  $.each(response, function (Key, value) {

            //for(var i=0;i<response.length;i++)
            //{

            
            //    if (response.TRAINERID != "") {

                 

            //        if (trnr == 0) {

            //            $("#ddltranr").append($("<option></option>").val('0').html('All'));
            //            $("#ddltranr").append($("<option></option>").val(response[i].TRAINERID).html(response[i].USERNAME));
            //            trnr++;
            //        }

            //    else if(response[i].TRAINERID != response.d[i - 1].TRAINERID && id!=0)
            //        {
            //            $("#ddltranr").append($("<option></option>").val(response[i].TRAINERID).html(response[i].USERNAME));
            //            trnr++;
            //        }

                
                                          
                   
            //    }
            //}

            //if (trnr == 0) {
            //    $("#ddltranr").append($("<option></option>").html('--Select--'));
            //}
        }
    });
});

$('#ddlbtch').change(function () {
    var id = $('#ddlbtch').val();
    var cnt = 0;
   
    $('#ddltranr').empty();

    $.ajax({
        type: "GET",
        url: "/Report/Home/gettrainerbybatch",
        data: { id: id },
        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function (response) {
            $.each(response, function (Key, value) {
                if (value.ID != "") {
                    if (cnt == 0) {
                        $("#ddltranr").append($("<option></option>").val('0').html('All'));

                    }

                    $("#ddltranr").append($("<option></option>").val(value.ID).html(value.Name));
                    cnt++;
                }
            });

            if (cnt == 0) {
                $("#ddltranr").append($("<option></option>").html('--Select--'));
            }
        }
    });
});


$("#submit").click(function () {


    var trngtype = $('#ddltrngtype').val();
    var batch = $('#ddlbtch').val();
    var trainerid = $('#ddltranr').val();
    if (trainerid != '--Select--') {
        gettrainerwisestudentdetails(trngtype, batch, trainerid)
    }
    else {

        var msg = 'Please Select another batch';
        $('#msg_Error').text(msg);
        onClickOpen('poup_msg');
        return false;
    }
  
 
});

function gettrainerwisestudentdetails(trngtype, batch, trainerid) {
    $.ajax({
        url: "/Report/Home/getTrainerwisestudentdetails",
        method: 'GET',
        data: { trngtype: trngtype, batch: batch, trainerid: trainerid },//"{district:'"+ district+ "',division:'"+division +"',city:'"+city +"'}",
        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function (data) {
            if (data.length != 0) {
                var table = '';
                var tableExport_trainer = '';
                $("#Trainerdata tr").remove();
                table = "<table border='1'  id='Trainerdata'>";
                var row = '';
                var cnt = 0;
                var user = '';
                var date = '';
              
                row = '<thead ><tr><th id="id">Login ID</th><th Graduate Id="Name">Graduate Name</th>'+
                    '<th id="gndrh" style=display:none;>Gender</th><th id="dob" style=display:none;>Date of Birth</th>' +
                    '<th id="trngtype">Training Type</th><th id="bth" >Batch</th>' +
                    '<th id="trainer">Trainer</th>' +
                    '<th id="istitut">Institute</th>' +
                    '<th id="email" style=display:none;>Email Id</th><th id="mob" style=display:none;>Mobile</th>' +
                    '<th id="ftrname" style=display:none;>Father Name</th><th id="addrss" style=display:none;>Address</th>'+
                    '<th id="dvsn" style=display:none;>Graduate Division</th><th id="distrct" style=display:none;>Graduate District</th><th id="city">Graduate City</th><th id="unvsty" style=display:none;>Graduate University</th>' +
                    '<th id="dprtmnt" style=display:none;>Department</th><th id="status">Status</th></tr><tbody>';
                for (var i = 0; i < data.length; i++) {
                    $("#btnExport").css('display', 'block');

                    row += '<tr >';
                    row += '<td align="center" >' + data[i].LOGINID + '</td>';
                    row += '<td align="center" >' + data[i].USERNAME + '</td>';
                    row += '<td  align="center" class="gndr" style=display:none;>' + data[i].GENDER + '</td>';
                    row += '<td align="center"  class="db" style=display:none;>' + data[i].dob + '</td>';
                    row += '<td align="center" >' + data[i].TrainingType + '</td>';
                    row += '<td align="center" >' + data[i].BATCHNAME + '</td>';
                    row += '<td align="center" >' + data[i].TrainerName + '</td>';
                    row += '<td align="center" >' + data[i].InstituteName + '</td>';
                    row += '<td  align="center" style=display:none;>' + data[i].EMAILID + '</td>';
                    row += '<td align="center"  class="mob" style=display:none;>' + data[i].MOBILE + '</td>';
                    row += '<td align="center"  class="fthr" style=display:none;>' + data[i].FatherName + '</td>';
                    row += '<td  align="center" class="addrss" style=display:none;>' + data[i].ADDRESS + '</td>';
                    row += '<td align="center"  style=display:none;>' + data[i].Division_Name + '</td>';
                    row += '<td  align="center" style=display:none;>' + data[i].DistrictName + '</td>';
                    row += '<td align="center" >' + data[i].City_Name + '</td>';
                    row += '<td  align="center" class="cntr" style=display:none;>' + data[i].centername + '</td>';
                    row += '<td  align="center" style=display:none;>' + data[i].department + '</td>';
                    row += '<td align="center"  >' + data[i].Status + '</td>';
                    row += '</tr>';

                }


                table = table + row + '</tbody></table>';
                tableExport_trainer = table;
                $('#studentReport_trainer').html(table);
                $("#studentReport_trainer").slideDown("slow");
                $('#Trainerdata').after('<div id="nav"></div>');
                paging('Trainerdata','nav','page1');
                $('#Studentreport_Trainer').html(tableExport_trainer);

               
            }
            else {

                var v = "No Data Available";
                $('#studentReport_trainer').text(v);
                $("#btnExport").css('display', 'none');
                $("#studentReport_trainer").slideDown("slow");
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
    var table_div = document.getElementById('Studentreport_Trainer');
    var table_html = table_div.outerHTML.replace(/ /g, '%20');
    a.href = data_type + ', ' + table_html;
    //setting the file name
    a.download = 'Studentdetails.xls';
    //triggering the function
    a.click();
    //just in case, prevent default behaviour
    e.preventDefault();


});