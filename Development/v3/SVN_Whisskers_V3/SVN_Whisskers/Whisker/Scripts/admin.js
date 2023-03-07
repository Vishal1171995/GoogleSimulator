var nameExp = /^[A-Za-z ]{3,250}$/;
var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
var phoneno = /^\d{10,13}$/;
var progressTrigger;
var progressElem = $('span#progressCounter');
var resultsElem = $('span#results');
var recordCount = 0;


//#region Batch 
function BatchValidation() {
    if ($('#txtBatchName').val().length == 0) {
        $('#divBatchNotify').text("Batch name is required.");
        $('#txtBatchName').focus();
        return false;
    }
    if ($('#txtBatchName').val().length >= 30) {
        $('#divBatchNotify').text("Batch name must be less than 30 characters.");
        $('#txtBatchName').focus();
        return false;
    }
    if ($('#txtLocation').val().length == 0) {
        $('#divBatchNotify').text("Location is required");
        $('#txtLocation').focus();
        return false;
    }
    if ($('#txtLocation').val().length >= 30) {
        $('#divBatchNotify').text("Location must be less than 30 characters.");
        $('#txtLocation').focus();
        return false;
    }
    if ($('#txtAdress').val().length == 0) {
        $('#divBatchNotify').text("Address is required.");
        $('#txtAdress').focus();
        return false;
    }
    if ($('#txtAdress').val().length >= 100) {
        $('#divBatchNotify').text("Address must be less than 100 characters.");
        $('#txtAdress').focus();
        return false;
    }
    if ($('#txtBxBatchStartDate').val().length == 0) {
        $('#divBatchNotify').text("Start Date is required.");
        $('#txtBxBatchStartDate').focus();
        return false;
    }
    if ($('#txtBxBatchEndDate').val().length == 0) {
        $('#divBatchNotify').text("End Date is required.");
        $('#txtBxBatchEndDate').focus();
        return false;
    }
    if ($("select[id='dropBindTime'] option:selected").index() == 0) {
        $('#divBatchNotify').text("Batch time is required");
        $('#dropBindTime').focus();
        return false;
    }
}

function showBatchPopup() {
    $('#TitlePopupBatch').text("Add New Batch");
    ClearBatchPopup();
}

function getBatchesOnDemand() {

    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchTime': $('#dropBatchTime').val(),
        'TrainerCode': $('#dropTrainerCode').val(),
        'LocationCode': $('#dropLocationCode').val(),
    });
    $('#div_result2').empty();
    $.ajax({
        //global: false,
        url: "/Admin/Manage/getBatchesOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            htmlcontent = '<div class="row">' +
            '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table id="tableBatch"  class="display" width="100%">' +
                                '<thead><tr>' +
                                '<th></th>' +
                                '<th>Batch</th>' +
                                '<th>Trainers</th>' +
                                '<th>Students</th>' +
                                '<th>Location</th>' +
                                '<th>Address</th>' +
                                '<th>Start Date</th>' +
                                '<th>End Date</th>' +
                                '<th>Time </th>' +
                                ' <th ></th>' +
                                ' <th ></th>' +
                                ' </thead></tr><tbody>';

            if (data == '') {
                //htmlcontent += '<tr><td colspan="9" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var oldBatchCode = '';
            var index = 0;
            var TrainerName = '';
            $.each(data, function (i, item) {
                if (oldBatchCode != item.BatchCode) {
                    oldBatchCode = item.BatchCode;
                    TrainerName = '';
                    AdGroupsResultChild = $.grep(data, function (element, index) {

                        return element.BatchCode == item.BatchCode;
                    });
                    $.each(AdGroupsResultChild, function (k, childItem) {
                        
                        TrainerName = TrainerName + ', ' + childItem.TrainerName
                    });

                    //batchName = item.BatchName;
                    index = index + 1;
                    htmlcontent += '<tr><td></td>' +
                        '<td><a id ="' + item.BatchCode + '" href="/Admin/Manage/BatchView/' + item.BatchCode + '">' + item.BatchName + '</a></td>' +
                                    '<td>' + TrainerName.substr(1) + '</td>' +
                                    '<td>' + item.StudentCount + '</td>' +
                                    '<td>' + item.Location + '</td>' +
                                    '<td>' + item.Address + '</td>' +
                                    '<td>' + item.StartDate + '</td>' +
                                    '<td>' + item.EndDate + '</td>' +
                                    '<td>' + item.Time + '</td>' +
                                    '<td class="text-center"><a  id ="' + item.BatchCode + '" href="#" onclick="getSingleBatchDetails(' + "'" + item.BatchCode + "'" + ');"><i class="fa fa-pencil" data-toggle="modal" data-target="#myModal" aria-hidden="true"></i></a></td>' +
                                    '<td class="text-center"><a  id ="' + item.BatchCode + '" href="#" onclick="return deleteSingleBatch(' + "'" + item.BatchCode + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a></td>';

                }
            });
            htmlcontent += '</tbody></table></div></div></div></div></div>';
            $('#div_result2').html(htmlcontent);
            // BatchDataTable();
            ApplyAdminDataTable("tableBatch", "9,10");
        }, async: true,
        error: OnError
    });
}

function getSingleBatchDetails(BatchCode) {

    ClearBatchPopup();
    $('#TitlePopupBatch').text("Edit Batch");
    $('#hdnBatchCode').val('0');
    $('#hdnBatchCode').val(BatchCode);
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': BatchCode,
    });
    $.ajax({
        url: "/Admin/Manage/getSingleBatchDetails",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            debugger;
            //Binding Batch Details In Btach Popup
            $('#txtBatchName').val(data[0][0].BatchName);
            $('#txtLocation').val(data[0][0].Location);
            $('#txtAdress').val(data[0][0].Address);
            var StartDate = $.datepicker.formatDate("dd-M-yy", new Date(data[0][0].StartDate));
            $("#txtBxBatchStartDate").datepicker('setDate', StartDate);
            var EndDate = $.datepicker.formatDate("dd-M-yy", new Date(data[0][0].EndDate));
            $('#txtBxBatchEndDate').datepicker('option', 'minDate', new Date(StartDate));
            $("#txtBxBatchEndDate").datepicker('setDate', EndDate);
            $("select#dropBindTime").val(data[0][0].Time);
            $("#txtBxBatchStartDate").addClass("disable9");
        }, async: true,
        error: OnError
    });
}

function deleteSingleBatch(BatchCode) {
    if (confirm('Are you sure you want to delete ?')) {
        var SelectedData = JSON.stringify({
            'BatchCode': BatchCode,
        });
        $.ajax({
            url: "/Admin/Manage/deleteSingleBatch",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                $(".Loader").show();

            },
            complete: function () {
                $(".Loader").hide();
            },
            success: function (data) {
                if (data[0] == "0") {
                    $('#BatchSuccessHead').text('Can not delete, this batch is in use.');
                }
                if (data[0] == "1") {
                    $('#BatchSuccessHead').text('Batch is deleted successfully.');
                    getBatchesOnDemand();
                }
                //$('#BatchSuccessHead').text('You have deleted batch successfully');
                $('#ModalBatchSuccess').modal('show');
                $('#myModal').modal('hide');
            }, async: true,

            error: OnError
        });
    }
    else {
        return false;
    }
}

function CompleteBatchDetails(BatchCode) {

    getOnlySingleBatchDetails(BatchCode);
    getAssignedStudentsPerBatch(BatchCode);
    getAssignedTrainersPerBatch(BatchCode);
}



//#region BatchView Page ---> This function is use to get batch details by using batch code
function getOnlySingleBatchDetails(BatchCode) {
    var SelectedData = JSON.stringify({
        'BatchCode': BatchCode,
    });
    $.ajax({
        url: "/Admin/Manage/getSingleBatchDetails",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            //Binding Batch Details In BtachView Page
            //alert(data[0][0].BatchName);
            var tem1p = $('#spnBatchName')[0].innerHTML;
            $('#spnBatchName').text(data[0][0].BatchName);
            $('#spnLocation').text(data[0][0].Location);
            $('#spnAddress').text(data[0][0].Address);
            var StartDate = $.datepicker.formatDate("dd-M-yy", new Date(data[0][0].StartDate));
            var EndDate = $.datepicker.formatDate("dd-M-yy", new Date(data[0][0].EndDate));
            $("#spnDates").text(StartDate + ' To ' + EndDate);

            //$("#txtBxBatchEndDate").datepicker('setDate', EndDate);
        }, async: true,

        error: OnError
    });
}
function AssignStudentsToBatch() {

    var BatchCode = $("#BatchCode").val();
    var UserID = $("#StudentID").val();
    if ($("#SelectedStudents").val() == "") {
        alert("Please select student by typing in student text field.")
        $("#SelectedStudents").focus();
    }
    if (UserID == "" && $("#SelectedStudents").val() != "") {
        alert("Please select students from the given list.")
        $("#SelectedStudents").focus();
    }
    if (UserID != "" && BatchCode != "0") {
        var SelectedData = JSON.stringify({
            'BatchCode': BatchCode,
            'UserID': UserID,
        })
        $.ajax({
            url: "/Admin/Manage/AssignedStudentPerBatch",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                //$(".Loader").show();
            },
            complete: function () {
                //  $(".Loader").hide();
            },
            success: function (data) {
                getAssignedStudentsPerBatch(BatchCode);
                $('#SelectedStudents').val("");
            }, async: true,

            error: function (data) {
                alert("Error is : " + data);
            }
        });
    }
    return false;
}
function getAssignedStudentsPerBatch(BatchCode) {
    $('#btnBatchView').addClass("disable6");
    $("#StudentID").val("");
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': BatchCode,
    });
    $('#div_AssignedStudent').empty();
    $.ajax({
        //global: false,
        url: "/Admin/Manage/getAssignedStudentsPerBatch",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            //$(".Loader").show();

        },
        complete: function () {
            // $(".Loader").hide();
        },
        success: function (data) {
            htmlcontent = '<div class="data-table table-responsive">' +
                                '<table id="tableAssignedStudent"  class="display">' +
                                '<thead><tr>' +
                                '<th></th>' +
                                '<th>Students</th>' +
                                '<th>Location</th>' +
                                '<th>Email ID</th>' +
                                '<th>Contact</th>' +
                                '<th>Status</th>' +
                                ' <th ></th>' +
                                ' </thead></tr><tbody>';
            if (data == '') {
                //htmlcontent += '<tr><td colspan="9" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var finalsubmit = 0;
            $.each(data, function (i, item) {

                var index = i + 1;
                var status = "";
                var deleterow = "";
                if (item.isMappingActive == true) {
                    status = '<td><span class="lblRed">Submitted</span></td>';
                    deleterow = '<td class="text-center"><a style="pointer-events: none; opacity: 0.6;" id ="' + item.BatchCode + '" href="" onclick="return deleteAssignedStudentPerBatch(' + "'" + item.BatchCode + "','" + item.StudentCode + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a></td>';

                }
                if (item.isMappingActive == false) {
                   
                    if (item.isMappingActive == false && finalsubmit == 0) {
                        //alert('enable final submit');
                        $('#btnBatchView').removeClass("disable6");
                        finalsubmit++;
                    }

                    //<i class="fa fa-save" aria-hidden="true" style="color:#ff975a; font-size:22px;"></i>
                    status = '<td><span class="lblRed">Draft</span></td>';
                    deleterow = '<td class="text-center"><a id ="' + item.BatchCode + '" href="" onclick="return deleteAssignedStudentPerBatch(' + "'" + item.BatchCode + "','" + item.StudentCode + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a></td>';
                }
                htmlcontent += '<tr><td>' + index + '</td>' +
                    '<td><a>' + item.FullName + '</a></td>' +
                                '<td>' + item.LocationName + '</td>' +
                                '<td>' + item.Email + '</td>' +
                                '<td>' + item.Contact + '</td>' +
                                status +
                                deleterow;


            });
            htmlcontent += '</tbody></table></div></div></div></div></div>';
            $('#div_AssignedStudent').html(htmlcontent);
            // BatchAssignedStudentDataTable();
            ApplyAdminDataTable("tableAssignedStudent", "6");
        }, async: true,

        error: OnError
    });
}
function deleteAssignedStudentPerBatch(BatchCode, UserID) {
    if (confirm('Are you sure you want to delete?')) {
        var htmlcontent = "";
        var SelectedData = JSON.stringify({
            'BatchCode': BatchCode,
            'UserID': UserID,
        });
        $.ajax({
            url: "/Admin/Manage/deleteAssignedStudentPerBatch",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                // $(".Loader").show();
            },
            complete: function () {
                // $(".Loader").hide();
            },
            success: function (data) {
                getAssignedStudentsPerBatch($("#BatchCode").val());
            }, async: true,
            error: OnError
        });
        return false;
    }
    else {
        return false;
    }
}

function AssignTrainersToBatch() {

    var BatchCode = $("#BatchCode").val();
    var UserID = $("#TrainerID").val();
    if ($("#SelectedTrainers").val() == "") {
        alert("Please select trainer by typing in trainer text field.")
        $("#SelectedTrainers").focus();
    }
    if (UserID == "" && $("#SelectedTrainers").val() != "") {
        alert("Please select trainer from the given list.")
        $("#SelectedTrainers").focus();
    }
    if (UserID != "" && BatchCode != "0") {
        var SelectedData = JSON.stringify({
            'BatchCode': BatchCode,
            'UserID': UserID,
        })
        $.ajax({
            url: "/Admin/Manage/AssignedTrainerPerBatch",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                //$(".Loader").show();
            },
            complete: function () {
                //  $(".Loader").hide();
            },
            success: function (data) {
                getAssignedTrainersPerBatch(BatchCode);
                $('#SelectedTrainers').val("");
            }, async: true,

            error: function (data) {
                alert("Error is : " + data);
            }
        });
    }
    return false;
}
function getAssignedTrainersPerBatch(BatchCode) {
    $("#TrainerID").val("");
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': BatchCode,
    });
    $('#div_AssignedTrainer').empty();
    $.ajax({
        //global: false,
        url: "/Admin/Manage/getAssignedTrainersPerBatch",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            //$(".Loader").show();

        },
        complete: function () {
            // $(".Loader").hide();
        },
        success: function (data) {
            htmlcontent = '<div class="data-table table-responsive">' +
                                '<table id="tableAssignedTrainer"  class="display">' +
                                '<thead><tr>' +
                                '<th></th>' +
                                '<th>Trainers</th>' +
                                '<th>Location</th>' +
                                '<th>Email ID</th>' +
                                '<th>Contact</th>' +
                                '<th>Status</th>' +
                                ' <th ></th>' +
                                ' </thead></tr><tbody>';

            if (data == '') {
                //htmlcontent += '<tr><td colspan="9" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            $.each(data, function (i, item) {

                var index = i + 1;
                var status = "";
                var deleterow = "";
                if (item.isMappingActive == true) {
                    status = '<td><span class="lblRed">Submitted</span></td>';
                    deleterow = '<td class="text-center"><a style="pointer-events: none; opacity: 0.6;" id ="' + item.BatchCode + '" href="" onclick="return deleteAssignedTrainerPerBatch(' + "'" + item.BatchCode + "','" + item.StudentCode + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a></td>';

                }
                var finalsubmit = 0;
                if (item.isMappingActive == false) {
                    if (item.isMappingActive == false && finalsubmit == 0) {
                        //alert('enable final submit');
                        $('#btnBatchView').removeClass("disable6");
                        finalsubmit++;
                    }
                    //<i class="fa fa-save" aria-hidden="true" style="color:#ff975a; font-size:22px;"></i>
                    status = '<td><span class="lblRed">Draft</span></td>';
                    deleterow = '<td class="text-center"><a id ="' + item.BatchCode + '" href="" onclick="return deleteAssignedTrainerPerBatch(' + "'" + item.BatchCode + "','" + item.StudentCode + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a></td>';
                }
                htmlcontent += '<tr><td>' + index + '</td>' +
                    '<td><a>' + item.FullName + '</a></td>' +
                                '<td>' + item.LocationName + '</td>' +
                                '<td>' + item.Email + '</td>' +
                                '<td>' + item.Contact + '</td>' +
                                status +
                                deleterow;


            });
            htmlcontent += '</tbody></table></div></div></div></div></div>';
            $('#div_AssignedTrainer').html(htmlcontent);
            //BatchAssignedTrainerDataTable();
            ApplyAdminDataTable("tableAssignedTrainer", "6");
        }, async: true,

        error: OnError
    });
}
function deleteAssignedTrainerPerBatch(BatchCode, UserID) {
    if (confirm('Are you sure you want to delete?')) {
        var htmlcontent = "";
        var SelectedData = JSON.stringify({
            'BatchCode': BatchCode,
            'UserID': UserID,
        });
        $.ajax({
            url: "/Admin/Manage/deleteAssignedTrainerPerBatch",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                // $(".Loader").show();
            },
            complete: function () {
                // $(".Loader").hide();
            },
            success: function (data) {
                getAssignedTrainersPerBatch($("#BatchCode").val());
            }, async: true,
            error: OnError
        });
        return false;
    }
    else {
        return false;
    }
}
function FinalAssignStudentAndTrainersToBatch() {
    $("#btnBatchView").attr("disabled", true);
    var BatchCode = $("#BatchCode").val();
    if (BatchCode != "0") {
        var SelectedData = JSON.stringify({
            'BatchCode': BatchCode,
        })
        $.ajax({
            url: "/Admin/Manage/FinalAssignStudentAndTrainersToBatch",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                //$(".Loader").show();
            },
            complete: function () {
                //  $(".Loader").hide();
            },
            success: function (data) {

                var JsonObj = data[0];
                if (data[0] == "0") {
                    $('#BatchViewSuccessHead').text('No records found To be update.');
                }
                if (data[0] == "1") {
                    $('#BatchViewSuccessHead').text('Batch is updated successfully.');
                }
                $("#btnBatchView").attr("disabled", false);

                getAssignedStudentsPerBatch(BatchCode);
                $('#SelectedStudents').val("");
                $("#StudentID").val("");
                getAssignedTrainersPerBatch(BatchCode);
                $('#SelectedTrainers').val("");
                $("#TrainerID").val("");
                $('#ModalBatchViewSuccess').modal('show');
            }, async: true,

            error: OnError
        });
    }
}
//#endregion BatchView Page

//#endregion Batch



//#region Trainer 
function TrainerValidation() {
    if ($('#txtFirstName').val().length == 0) {
        $('#divTrainerNotify').text("First name is required.");
        $('#txtFirstName').focus();
        return false;
    }
    if ($('#txtFirstName').val().length >= 15) {
        $('#divTrainerNotify').text("First name must be less than 15 characters.");
        $('#txtFirstName').focus();
        return false;
    }
    //if ($('#txtMiddleName').val().length == 0) {
    //    $('#divStudentNotify').text("Client Middle Name is required");
    //    $('#txtMiddleName').focus();
    //    return false;
    //}
    if ($('#txtLastName').val().length == 0) {
        $('#divTrainerNotify').text("Last name is required.");
        $('#txtLastName').focus();
        return false;
    }
    if ($('#txtLastName').val().length >= 15) {
        $('#divTrainerNotify').text("Last name must be less than 15 characters.");
        $('#txtLastName').focus();
        return false;
    }
    if ($('#txtLocation').val().length == 0) {
        $('#divTrainerNotify').text("Location is required.");
        $('#txtLocation').focus();
        return false;
    }
    if ($('#txtLocation').val().length >= 20) {
        $('#divTrainerNotify').text("Location must be less than 20 characters.");
        $('#txtLocation').focus();
        return false;
    }
    if ($('#txtEmail').val().length == 0) {
        $('#divTrainerNotify').text("Email ID is required.");
        $('#txtEmail').focus();
        return false;
    }
    else if (!$("#txtEmail").val().match(ck_email)) {
        $('#divTrainerNotify').text("Valid Email ID is required.");
        $('#txtEmail').focus();
        return false;
    }
    if ($('#txtContact').val().length == 0) {
        $('#divTrainerNotify').text("Contact is required.");
        $('#txtContact').focus();
        return false;
    }
    else if (!$("#txtContact").val().match(phoneno)) {
        $('#divTrainerNotify').text("Valid contact is required.");
        $('#txtContact').focus();
        return false;
    }

}
function showTrainerPopup() {
    $('#TitlePopupTrainer').text("Add New Trainer");
    ClearTrainerPopup();
}
function getTrainersOnDemand() {

    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'TrainerCode': $('#dropTrainerCode').val(),
        'BatchCode': $('#dropBatchCode').val(),
        'LocationCode': $('#dropLocationCode').val(),
    });
    $('#div_result4').empty();
    $.ajax({
        //global: false,
        url: "/Admin/Manage/getTrainersOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",

        beforeSend: function () {

            $(".Loader").show();

        },
        complete: function () {

            $(".Loader").hide();
        },
        success: function (data) {
            htmlcontent = '<div class="row">' +
            '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table id="tableTrainer">' +
                                '<thead><tr>' +
                                '<th></th>' +
                                '<th>Trainer</th>' +
                                '<th>Location</th>' +
                                '<th>Email ID</th>' +
                                '<th>Contact</th>' +
                                '<th>Batch</th>' +
                                ' <th ></th>' +
                                ' <th ></th>' +
                                ' </thead></tr>';

            if (data == '') {
                //htmlcontent += '<tr><td colspan="9">No Results Found</tr></td>';
            }
            var oldEmail = '';
            var index = 0;
            var batchName = '';
            $.each(data, function (i, item) {
                if (oldEmail != item.Email) {
                    oldEmail = item.Email;
                    batchName = '';
                    AdGroupsResultChild = $.grep(data, function (element, index) {

                        return element.Email == item.Email;
                    });
                    $.each(AdGroupsResultChild, function (k, childItem) {
                        batchName = batchName + ', ' + childItem.BatchName
                    });

                    //batchName = item.BatchName;
                    index = index + 1;
                    htmlcontent += '<tr><td></td>' +
                                    '<td>' + item.FullName + '</td>' +
                                    '<td>' + item.LocationName + '</td>' +
                                    '<td>' + item.Email + '</td>' +
                                    '<td>' + item.Contact + '</td>' +
                                    '<td id="bt' + index + '">' + batchName.substr(1) + '</td>' +
                                    '<td class="text-center"><a  id ="' + item.TrainerCode + '" href="#" onclick="getSingleTrainerDetails(' + "'" + item.TrainerCode + "'" + ');"><i class="fa fa-pencil" data-toggle="modal" data-target="#myModal27" aria-hidden="true"></i></a></td>' +
                                    '<td class="text-center"><a  id ="' + item.TrainerCode + '" href="#" onclick="return deleteSingleTrainer(' + "'" + item.TrainerCode + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a></td></tr>';

                }

            });
            htmlcontent += '</table></div></div></div></div></div>';
            $('#div_result4').html(htmlcontent);
            //$("#bt3").append("hello");
            //TrainerDataTable();
            ApplyAdminDataTable("tableTrainer", "6,7");
        }, async: true,
        error: OnError
    });
}
function getSingleTrainerDetails(TrainerCode) {

    ClearTrainerPopup();
    href = "/Admin/Manage/TrainerResetPwd/";
    $('#TitlePopupTrainer').text("Edit Trainer");
    $("#TrainerResetPwd").removeClass("hidden");
    $('#hdnTrainerCode').val('0');
    $('#hdnTrainerCode').val(TrainerCode);
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'TrainerCode': TrainerCode,
    });
    $.ajax({
        url: "/Admin/Manage/getSingleTrainerDetails",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            //Binding Student Details In Student Popup
            $('#txtFirstName').val(data[0][0].FirstName);
            $('#txtMiddleName').val(data[0][0].MiddleName);
            $('#txtLastName').val(data[0][0].LastName);
            $('#txtLocation').val(data[0][0].LocationName);
            $('#txtEmail').val(data[0][0].Email);
            $('#txtContact').val(data[0][0].Contact);
            $("#txtEmail").attr('readonly', true);

            $('#txtContact').val(data[0][0].Contact);
        }, async: true,
        error: OnError
    });
}
function deleteSingleTrainer(TrainerCode) {
    if (confirm('Are you sure you want to delete?')) {
        var SelectedData = JSON.stringify({
            'TrainerCode': TrainerCode,
        });
        $.ajax({
            url: "/Admin/Manage/deleteSingleTrainer",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                $(".Loader").show();

            },
            complete: function () {
                $(".Loader").hide();
            },
            success: function (data) {
                if (data[0] == "0") {
                    $('#TrainerSuccessHead').text('You can' + "'t" + '  delete this trainer as the trainer is already assigned to batch.');
                }
                if (data[0] == "1") {
                    $('#TrainerSuccessHead').text('Trainer is deleted successfully.');
                    getTrainersOnDemand();
                }
                //getTrainersOnDemand();
                //$('#TrainerSuccessHead').text('You have Deleted Trainer successfully');
                $('#ModalTrainerSuccess').modal('show');
                $('#myModal27').modal('hide');
            }, async: true,
            error: OnError
        });
    }
    else {
        return false;
    }
}
function SaveTrainerExcel() {

    var hjjh = $('#excelUpload')[0];
    hjjh = $('#excelUpload');
    var formData = new FormData($('#excelUpload')[0]);
    $.ajax({
        url: '/Admin/Manage/SaveTrainerExcel',  //Server script to process data
        type: 'POST',
        data: formData,
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            getTrainersOnDemand();
            CloseExcelPopup(data);
        }, async: true,

        error: OnError
    });
    return false;
}
function HelperTrainerDownloadTemplateExcel() {
    var htmlcontent = "";
    htmlcontent =
        '<table id="tempTableTrainer">' +
        //'<caption>All Search Query  List</caption><colgroup align="left"></colgroup><colgroup span="5" align="left"></colgroup>' +
                        '<thead><tr>' +
                        '<th>FirstName</th>' +
                        '<th>MiddleName</th>' +
                        '<th>LastName</th>' +
                        '<th>Location</th>' +
                        '<th>Email</th>' +
                        '<th>Contact</th>' +
                        ' </thead></tr><tbody>';
    htmlcontent += '</tbody></table>';
    fnExcelReport(htmlcontent, "Download_Trainer");
}
function TrainerResetPwd() {
    var SelectedData = JSON.stringify({
        'UserId': $('#hdnTrainerCode').val(),
    });
    $.ajax({
        url: "/Admin/Manage/ResetPwd",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            ClearTrainerPopup();
            if (data == "1") {
                $('#TrainerSuccessHead').text('Password has been reset successfully and has been sent to your registered Email.');
            }
            else {
                $('#TrainerSuccessHead').text('Sorry! reset password is failed');
            }
            $('#ModalTrainerSuccess').modal('show');
            $('#hdnTrainerCode').val('0');
        }, async: true,
        error: OnError
    });
}
//#endregion Trainer



//#region Student 
function StudentValidation() {
    if ($('#txtFirstName').val().length == 0) {
        $('#divStudentNotify').text("First name is required");
        $('#txtFirstName').focus();
        return false;
    }
    if ($('#txtFirstName').val().length >= 15) {
        $('#divStudentNotify').text("First name must be less than 15 characters.");
        $('#txtFirstName').focus();
        return false;
    }
    //if ($('#txtMiddleName').val().length == 0) {
    //    $('#divStudentNotify').text("Client Middle Name is required");
    //    $('#txtMiddleName').focus();
    //    return false;
    //}
    if ($('#txtLastName').val().length == 0) {
        $('#divStudentNotify').text("Last name is required");
        $('#txtLastName').focus();
        return false;
    }
    if ($('#txtLastName').val().length >= 15) {
        $('#divStudentNotify').text("Last name must be less than 15 characters.");
        $('#txtLastName').focus();
        return false;
    }
    if ($('#txtLocation').val().length == 0) {
        $('#divStudentNotify').text("Location is required");
        $('#txtLocation').focus();
        return false;
    }
    if ($('#txtLocation').val().length >= 20) {
        $('#divStudentNotify').text("Location must be less than 20 characters.");
        $('#txtLocation').focus();
        return false;
    }
    if ($('#txtEmail').val().length == 0) {
        $('#divStudentNotify').text("Email ID is required");
        $('#txtEmail').focus();
        return false;
    }
    else if (!$("#txtEmail").val().match(ck_email)) {
        $('#divStudentNotify').text("Valid Email ID is required");
        $('#txtEmail').focus();
        return false;
    }

    if ($('#txtContact').val().length == 0) {
        $('#divStudentNotify').text("Contact is required");
        $('#txtContact').focus();
        return false;
    }
    else if (!$("#txtContact").val().match(phoneno)) {
        $('#divStudentNotify').text("Valid contact is required");
        $('#txtContact').focus();
        return false;
    }

}
function showStudentPopup() {
    $('#TitlePopupStudent').text("Add New Student");
    ClearStudentPopup();
}
function getStudentsOnDemand() {

    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#dropBatchCode').val(),
        'LocationCode': $('#dropLocationCode').val(),
    });
    $('#div_result3').empty();
    $.ajax({
        //global: false,
        url: "/Admin/Manage/getStudentsOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {

            htmlcontent = '<div class="row">' +
            '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table id="tableStudent">' +
                                '<thead><tr>' +
                                '<th></th>' +
                                '<th>Student</th>' +
                                '<th>Location</th>' +
                                '<th>Email ID</th>' +
                                '<th>Contact</th>' +
                                '<th>Batch</th>' +
                                ' <th ></th>' +
                                ' <th ></th>' +
                                ' </thead></tr>';

            if (data == '') {
                //htmlcontent += '<tr><td colspan="9">No Results Found</tr></td>';
            }
            var oldEmail = '';
            var index = 0;
            var batchName = '';
            $.each(data, function (i, item) {
                if (oldEmail != item.Email) {
                    oldEmail = item.Email;
                    batchName = '';
                    AdGroupsResultChild = $.grep(data, function (element, index) {

                        return element.Email == item.Email;
                    });
                    $.each(AdGroupsResultChild, function (k, childItem) {
                        
                        batchName = batchName + ', ' + childItem.BatchName
                    });

                    //batchName = item.BatchName;
                    index = index + 1;
                    htmlcontent += '<tr><td></td>' +
                    '<td>' + item.FullName + '</td>' +
                                    '<td>' + item.LocationName + '</td>' +
                                    '<td>' + item.Email + '</td>' +
                                    '<td>' + item.Contact + '</td>' +
                                    '<td>' + batchName.substr(1) + '</td>' +
                                    '<td class="text-center"><a  id ="' + item.StudentCode + '" href="#" onclick="getSingleStudentDetails(' + "'" + item.StudentCode + "'" + ');"><i class="fa fa-pencil" data-toggle="modal" data-target="#myModal32" aria-hidden="true"></i></a></td>' +
                                    '<td class="text-center"><a  id ="' + item.StudentCode + '" href="#" onclick="return deleteSingleStudent(' + "'" + item.StudentCode + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a></td>';
                }

            });
            htmlcontent += '</table></div></div></div></div></div>';
            $('#div_result3').html(htmlcontent);
            //StudentDataTable();
            ApplyAdminDataTable("tableStudent", "6,7");
        }, async: true,
        error: OnError
    });
}
function getSingleStudentDetails(StudentCode) {

    ClearStudentPopup();
    $('#TitlePopupStudent').text("Edit Student");
    $("#StudentResetPwd").removeClass("hidden");
    $('#hdnStudentCode').val('0');
    $('#hdnStudentCode').val(StudentCode);
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'StudentCode': StudentCode,
    });
    $.ajax({
        url: "/Admin/Manage/getSingleStudentDetails",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            //Binding Student Details In Student Popup
            $('#txtFirstName').val(data[0][0].FirstName);
            $('#txtMiddleName').val(data[0][0].MiddleName);
            $('#txtLastName').val(data[0][0].LastName);
            $('#txtLocation').val(data[0][0].LocationName);
            $('#txtEmail').val(data[0][0].Email);
            $('#txtContact').val(data[0][0].Contact);
            $("#txtEmail").attr('readonly', true);
        }, async: true,
        error: OnError
    });
}
function deleteSingleStudent(StudentCode) {
    if (confirm('Are you sure you want to delete?')) {
        var SelectedData = JSON.stringify({
            'StudentCode': StudentCode,
        });
        $.ajax({
            url: "/Admin/Manage/deleteSingleStudent",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                $(".Loader").show();

            },
            complete: function () {
                $(".Loader").hide();
            },
            success: function (data) {
                if (data[0] == "0") {
                    $('#StudentSuccessHead').text('You can' + "'t" + ' delete this Student as the student is already assigned to batch');
                }
                if (data[0] == "1") {
                    $('#StudentSuccessHead').text('You have deleted student successfully');
                    getStudentsOnDemand();
                }
                //getStudentsOnDemand();
                //$('#StudentSuccessHead').text('You have Deleted Student successfully');
                $('#ModalStudentSuccess').modal('show');
                $('#myModal32').modal('hide');
            }, async: true,
            error: OnError
        });
    }
    else {
        return false;
    }
}
function SaveStudentExcel() {

    var hjjh = $('#excelUpload')[0];
    hjjh = $('#excelUpload');
    var formData = new FormData($('#excelUpload')[0]);
    $.ajax({
        url: '/Admin/Manage/SaveStudentExcel',  //Server script to process data
        type: 'POST',
        data: formData,
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            getStudentsOnDemand();
            CloseExcelPopup(data);
        }, async: true,
        error: OnError
    });
    return false;
}
function HelperStudentDownloadTemplateExcel() {
    var htmlcontent = "";
    htmlcontent =
        '<table id="tempTableStudent">' +
        //'<caption>All Search Query  List</caption><colgroup align="left"></colgroup><colgroup span="5" align="left"></colgroup>' +
                        '<thead><tr>' +
                        '<th>FirstName</th>' +
                        '<th>MiddleName</th>' +
                        '<th>LastName</th>' +
                        '<th>Location</th>' +
                        '<th>Email</th>' +
                        '<th>Contact</th>' +
                        ' </thead></tr><tbody>';
    htmlcontent += '</tbody></table>';
    fnExcelReport(htmlcontent, "Download_Student");
}
function StudentResetPwd() {
    var SelectedData = JSON.stringify({
        'UserId': $('#hdnStudentCode').val(),
    });
    $.ajax({
        url: "/Admin/Manage/ResetPwd",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            ClearStudentPopup();
            if (data == "1") {
                $('#StudentSuccessHead').text('Password has been reset successfully and has been sent to your registered Email.');
            }
            else {
                $('#StudentSuccessHead').text('Sorry! reset password is failed');
            }
            $('#ModalStudentSuccess').modal('show');
            $('#hdnStudentCode').val('0');
        }, async: true,
        error: OnError
    });
}
//#endregion Student



//#region Keyword 
function KeywordValidation() {
    if ($('#txtKeywordName').val().length == 0) {
        $('#divKeywordNotify').text("Keyword name is required.");
        $('#txtKeywordName').focus();
        return false;
    }
    if ($('#txtKeywordName').val().length >= 50) {
        $('#divKeywordNotify').text("Keyword must be less than 50 characters.");
        $('#txtKeywordName').focus();
        return false;
    }
    if ($('#txtKeywordAvgMonthSearch').val().length == 0) {
        $('#divKeywordNotify').text("Average monthly search field is required.");
        $('#txtKeywordAvgMonthSearch').focus();
        return false;
    }
    if ($('#txtKeywordAvgMonthSearch').val().length > 5) {
        $('#divKeywordNotify').text("Max 5 digit value is allowed for average monthly search.");
        $('#txtKeywordAvgMonthSearch').focus();
        return false;
    }
    if ($('#txtKeywordSuggestBid').val().length == 0) {
        $('#divKeywordNotify').text("Suggested bid is required.");
        $('#txtKeywordSuggestBid').focus();
        return false;
    }
    if ($('#txtKeywordSuggestBid').val().length > 6) {
        $('#divKeywordNotify').text("Max 6 digit value is allowed for Suggested bid.");
        $('#txtKeywordSuggestBid').focus();
        return false;
    }
}
function showKeywordPopup() {
    $('#TitlePopupKeyword').text("Add New Keyword");
    ClearKeywordPopup();
}
function getKeywordsOnDemand() {
    var htmlcontent = "";
    $('#div_result6').empty();
    $.ajax({
        //global: false,
        url: "/Admin/Manage/getKeywordsOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            //$(".Loader").hide();
        },
        success: function (data) {
            htmlcontent = '<div class="row">' +
            '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table id="tableKeyword">' +
                                '<thead><tr>' +
                                '<th></th>' +
                                '<th>Keyword</th>' +
                                '<th>Avg Monthly Searches</th>' +
                                '<th>Suggested Bid</th>' +
                                ' <th ></th>' +
                                ' <th ></th>' +
                                ' </thead></tr><tbody>';

            if (data == '') {
                //htmlcontent += '<tr><td colspan="9" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            $.each(data, function (i, item) {
                var index = i + 1;
                htmlcontent += '<tr><td></td>' +
                    '<td>' + item.KeyName + '</td>' +
                                '<td>' + (item.AvgMonthlySrch == null ? "-" : item.AvgMonthlySrch) + '</td>' +
                                '<td>' + (item.SuggestedBid == null ? "-" : item.SuggestedBid) + '</td>' +
                                '<td class="text-center"><a  id ="' + item.KeyCode + '" href="#" onclick="getSingleKeywordDetails(' + "'" + item.KeyCode + "'" + ');"><i class="fa fa-pencil" data-toggle="modal" data-target="#myModal29" aria-hidden="true"></i></a></td>' +
                                '<td class="text-center"><a  id ="' + item.KeyCode + '" href="#" onclick="return deleteSingleKeyword(' + "'" + item.KeyCode + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a></td>';
            });
            htmlcontent += '</tbody></table></div></div></div></div></div>';
            $('#div_result6').html(htmlcontent);

            // tableToExcel('tableKeyword', 'W3C Example Table');
            // KeywordDataTable();
            ApplyAdminDataTable("tableKeyword", "4,5");
            //window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#div_result6').html()));
            $(".Loader").hide();
        }, async: true,
        error: OnError
    });

}
function getSingleKeywordDetails(KeywordCode) {

    ClearKeywordPopup();
    $('#TitlePopupBatch').text("Edit Keyword");
    $('#hdnKeywordCode').val('0');
    $('#hdnKeywordCode').val(KeywordCode);
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'KeywordCode': KeywordCode,
    });
    $.ajax({
        url: "/Admin/Manage/getSingleKeywordDetails",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {

            //Binding Batch Details In Btach Popup
            $('#txtKeywordName').val(data[0].KeyName);
            $('#txtKeywordAvgMonthSearch').val(data[0].AvgMonthlySrch);
            $('#txtKeywordSuggestBid').val(data[0].SuggestedBid);
        }, async: true,
        error: OnError
    });
}
function deleteSingleKeyword(KeywordCode) {
    if (confirm('Are you sure you want to delete?')) {
       
        var SelectedData = JSON.stringify({
            'KeywordCode': KeywordCode,
        });
        $.ajax({
            url: "/Admin/Manage/deleteSingleKeyword",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                 //$(".Loader").show();
            },
            complete: function () {
                 //$(".Loader").hide();
            },
            success: function (data) {
                if (data[0] == "0") {
                    $('#KeywordSuccessHead').text('You cant delete, this Keyword is in use.');
                    //$(".Loader").hide();
                }
                if (data[0] == "1") {
                    $('#KeywordSuccessHead').text('Keyword is deleted successfully.');
                   // expect(parser.parse(raw)).toThrow(new Error("Parsing is not possible"));
                    getKeywordsOnDemand();
                    
                }
                //getKeywordsOnDemand();
                //$('#KeywordSuccessHead').text('You have Deleted Keyword successfully');
               
                $('#ModalKeywordSuccess').modal('show');
                $('#myModal29').modal('hide');
                //$(".Loader").hide();
            }, async: true,
            error: OnError
        });
    }
    else {
        return false;
    }
}
function SaveKeywordExcel() {

    var hjjh = $('#excelUpload')[0];
    hjjh = $('#excelUpload');
    var formData = new FormData($('#excelUpload')[0]);
    $.ajax({
        url: '/Admin/Manage/SaveKeywordExcel',  //Server script to process data
        type: 'POST',
        data: formData,
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            getKeywordsOnDemand();
            CloseExcelPopup(data);
        }, async: true,
        error: OnError
    });
    return false;
}
function HelperKeywordDownloadTemplateExcel() {
    var htmlcontent = "";
    htmlcontent =
        '<table id="tempTableKeyword">' +
        //'<caption>All Search Query  List</caption><colgroup align="left"></colgroup><colgroup span="5" align="left"></colgroup>' +
                        '<thead><tr>' +
                        '<th>KeywordName</th>' +
                        '<th>Avg Monthely Searches</th>' +
                        '<th>Suggested Bid</th>' +
                        ' </thead></tr><tbody>';
    htmlcontent += '</tbody></table>';
    fnExcelReport(htmlcontent, "Download_Keyword");
}
function HelperKeywordDownloadExcel() {
    var htmlcontent = "";
    $.ajax({
        url: "/Admin/Manage/getKeywordsOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data) {
            htmlcontent =
                '<table border="2px" id="tempTableKeyword">' +
                '<caption>All Keywords List</caption><colgroup align="left"></colgroup><colgroup span="5" align="left"></colgroup>' +
                                '<thead><tr>' +
                                '<th>Index</th>' +
                                '<th>Keywords</th>' +
                                '<th>Avg Monthely Searches</th>' +
                                '<th>Suggested Bid</th>' +
                                ' </thead></tr><tbody>';

            if (data == '') {
                htmlcontent += '<tr><td colspan="5">No Results Found</tr></td>';
            }
            $.each(data, function (i, item) {
                var index = i + 1;
                htmlcontent += '<tr><td>' + index + '</td>' +
                '<td>' + item.KeyName + '</td>' +
                                '<td>' + item.AvgMonthlySrch + '</td>' +
                                '<td>' + item.SuggestedBid + '</td>';
            });
        }, async: false,
        error: OnError
    });
    htmlcontent += '</tbody></table>';
    fnExcelReport(htmlcontent, "Download_Keyword");
}
//#endregion Keyword


//#region Phrases 
function PhraseValidation() {
    if ($('#txtPhraseName').val().length == 0) {
        $('#divPhraseNotify').text("Search query is required.");
        $('#txtPhraseName').focus();
        return false;
    }
    if ($('#txtPhraseName').val().length >= 50) {
        $('#divPhraseNotify').text("Search Query must be less than 50 characters.");
        $('#txtPhraseName').focus();
        return false;
    }
    if ($('#txtPhraseTraffic').val().length == 0) {
        $('#divPhraseNotify').text("Traffic field is required.");
        $('#txtPhraseTraffic').focus();
        return false;
    }
    if ($('#txtPhraseTraffic').val().length > 5) {
        $('#divPhraseNotify').text("max 5 digit value is allowed for traffic.");
        $('#txtPhraseTraffic').focus();
        return false;
    }
}
function showPhrasePopup() {
    $('#TitlePopupPhrase').text("Add New Search Query");
    ClearPhrasePopup();
}
function getPhrasesOnDemandNew() {
    var htmlcontent = "";
    $('#div_result6').empty();
    $.ajax({
        //global: false,
        url: "/Admin/Manage/getPhrasesOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            htmlcontent = '<div class="row">' +
            '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table id="tablePhrase">' +
                                '<thead><tr>' +
                                '<th width="30px;"></th>' +
                                '<th>Search Query</th>' +
                                '<th>Traffic</th>' +
                                ' <th  width="30px;"></th>' +
                                ' <th  width="30px;"></th>' +
                                ' </thead></tr><tbody>';

            if (data == '') {
                //htmlcontent += '<tr><td colspan="9" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            $.each(data, function (i, item) {

                var index = i + 1;
                htmlcontent += '<tr><td>' + index + '</td>' +
                    '<td>' + item.SearchQuery + '</td>' +
                                '<td>' + item.Traffic + '</td>' +
                                '<td class="text-center"><a  id ="' + item.Id + '" href="#" onclick="getSinglePhraseDetails(' + "'" + item.Id + "'" + ');"><i class="fa fa-pencil" data-toggle="modal" data-target="#myModal29" aria-hidden="true"></i></a></td>' +
                                '<td class="text-center"><a  id ="' + item.Id + '" href="#" onclick="deleteSinglePhrase(' + "'" + item.Id + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a></td>';


            });
            htmlcontent += '</tbody></table></div></div></div></div></div>';
            $('#div_result6').html(htmlcontent);

            // tableToExcel('tableKeyword', 'W3C Example Table');
            PhraseDataTable();
            //window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#div_result6').html()));
        }, async: true,
        error: OnError
    });

}
function getPhrasesOnDemand() {
    var htmlcontent = "";
    $('#div_result6').empty();
    $.ajax({
        //global: false,
        url: "/Admin/Manage/getPhrasesOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {

            htmlcontent = '<div class="row">' +
            '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table id="tablePhrase">' +
                                '<thead><tr>' +
                                '<th width="30px;"></th>' +
                                '<th>Search Query</th>' +
                                '<th>Traffic</th>' +
                                ' <th  width="30px;"></th>' +
                                ' <th  width="30px;"></th>' +
                                ' </thead></tr><tbody>';

            if (data == '') {
                //htmlcontent += '<tr><td colspan="9" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            $.each(data, function (i, item) {

                var index = i + 1;
                htmlcontent += '<tr><td></td>' +
                    '<td>' + item.SearchQuery + '</td>' +
                                '<td>' + item.Traffic + '</td>' +
                                '<td class="text-center"><a  id ="' + item.Id + '" href="#" onclick="getSinglePhraseDetails(' + "'" + item.Id + "'" + ');"><i class="fa fa-pencil" data-toggle="modal" data-target="#myModal29" aria-hidden="true"></i></a></td>' +
                                '<td class="text-center"><a  id ="' + item.Id + '" href="#" onclick="return deleteSinglePhrase(' + "'" + item.Id + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a></td>';


            });
            htmlcontent += '</tbody></table></div></div></div></div></div>';
            $('#div_result6').html(htmlcontent);

            // tableToExcel('tableKeyword', 'W3C Example Table');
            PhraseDataTable();
            //ApplyAdminDataTable("tablePhrase");
            //window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#div_result6').html()));
        }, async: true,
        error: OnError
    });

}
function getSinglePhraseDetails(PhraseCode) {

    ClearPhrasePopup();
    $('#TitlePopupPhrase').text("Edit Search Query");
    $('#hdnPhraseCode').val('0');
    $('#hdnPhraseCode').val(PhraseCode);
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'PhraseCode': PhraseCode,
    });
    $.ajax({
        url: "/Admin/Manage/getSinglePhraseDetails",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {

            //Binding Batch Details In Btach Popup
            $('#txtPhraseName').val(data[0].SearchQuery);
            $('#txtPhraseTraffic').val(data[0].Traffic);
        }, async: true,
        error: OnError
    });
}
function deleteSinglePhrase(PhraseCode) {
    if (confirm('Are you sure you want to delete?')) {
        var SelectedData = JSON.stringify({
            'PhraseCode': PhraseCode,
        });
        $.ajax({
            url: "/Admin/Manage/deleteSinglePhrase",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                $(".Loader").show();

            },
            complete: function () {
                $(".Loader").hide();
            },
            success: function (data) {
                if (data[0] == "0") {
                    $('#PhraseSuccessHead').text('You cant delete, this Search Query is in use.');
                }
                if (data[0] == "1") {
                    $('#PhraseSuccessHead').text('Search Query is deleted successfully.');
                    PhraseServerSidedatatable();
                }
                //getKeywordsOnDemand();
                //$('#KeywordSuccessHead').text('You have Deleted Keyword successfully');
                $('#ModalPhraseSuccess').modal('show');
                $('#myModal29').modal('hide');
            }, async: true,
            error: OnError
        });
    }
    else {
        return false;
    }
}
function SavePhraseExcel() {
    debugger;
    var hjjh = $('#excelUpload')[0];
    hjjh = $('#excelUpload');
    var formData = new FormData($('#excelUpload')[0]);
    $.ajax({
        url: '/Admin/Manage/SavePhraseExcel',  //Server script to process data
        type: 'POST',
        data: formData,
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            CloseExcelPopup(data);
            ClearPhrasePopup();
            PhraseServerSidedatatable();
            //getPhrasesOnDemand();
        }, async: true,
        error: OnError
    });
    function OnError(xhr, errorType, exception) {
        
        var responseText;
        $("#dialog").html("");
        try {
            alert(responseText.Message);
            responseText = jQuery.parseJSON(xhr.responseText);
            $("#dialog").append("<div><b>" + errorType + " " + exception + "</b></div>");
            //$("#dialog").append("<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>");
            //$("#dialog").append("<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>");
            $("#dialog").append("<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>");
        } catch (e) {
            responseText = xhr.responseText;
            $("#dialog").html(responseText);
        }
        $("#dialog").dialog({
            title: "jQuery Exception Details",
            width: 700,
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            }
        });
    }
    
    return false;
}
function HelperPhraseDownloadTemplateExcel() {
    var htmlcontent = "";
    htmlcontent =
        '<table id="tempTablePhrase">' +
        //'<caption>All Search Query  List</caption><colgroup align="left"></colgroup><colgroup span="5" align="left"></colgroup>' +
                        '<thead><tr>' +
                        '<th>SearchQuery</th>' +
                        '<th>Traffic</th>' +
                        ' </thead></tr><tbody>';
    htmlcontent += '</tbody></table>';
    fnExcelReport(htmlcontent, "Download_Phrase");
}
function HelperPhraseDownloadExcel() {
    var htmlcontent = "";
    $.ajax({
        url: "/Admin/Manage/getPhrasesOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data) {
            htmlcontent =
                '<table border="2px" id="tempTablePhrase">' +
                '<caption>All Search Query List</caption><colgroup align="left"></colgroup><colgroup span="5" align="left"></colgroup>' +
                                '<thead><tr>' +
                                '<th>Index</th>' +
                                '<th>Search Query</th>' +
                                '<th>Traffic</th>' +
                                ' </thead></tr><tbody>';

            if (data == '') {
                htmlcontent += '<tr><td colspan="5">No Results Found</tr></td>';
            }
            $.each(data, function (i, item) {
                var index = i + 1;
                htmlcontent += '<tr><td>' + index + '</td>' +
                '<td>' + item.SearchQuery + '</td>' +
                                '<td>' + item.Traffic + '</td>';
            });
        }, async: false,

        error: function (data) {
            alert("Either internet slow or Internal Error");
        }
    });
    htmlcontent += '</tbody></table>';
    fnExcelReport(htmlcontent, "Download_Phrase");
}
//#endregion Phrases



//#region Account 
function AccountValidation() {
    if ($('#txtAccountName').val().length == 0) {
        $('#divAccountNotify').text("Account name is required.");
        $('#txtAccountName').focus();
        // $("#btnCreateAccount").attr("disabled", false);
        return false;
    }
    if ($('#txtAccountName').val().length >= 20) {
        $('#divAccountNotify').text("Account name must be less than 20 characters.");
        $('#txtAccountName').focus();
        return false;
    }
    if ($('#hdnAccountCode').val() == '0') {
        if ($('#file').val().length == 0) {
            $('#divAccountNotify').text("Account image is required.");
            $('#file').focus();
            // $("#btnCreateAccount").attr("disabled", false);
            return false;
        }
    }
    if ($('#txtAccountDesrpsn').val().length == 0) {
        $('#divAccountNotify').text("Description is required.");
        $('#txtAccountDesrpsn').focus();
        // $("#btnCreateAccount").attr("disabled", false);
        return false;
    }
    else {
        //$("#btnCreateAccount").text('1');
        //$("#btnCreateAccount").attr("disabled", true);
        if (confirm('Are you sure you want to continue?')) {
            return createAccount();
        }
        else {
            return false;
        }
    }
}
function showAccPopup() {
    $('#TitlePopupAccount').text("Add Account");
    ClearAccountPopup();
}
function getAccountsOnDemand() {

    var htmlcontent = "";
    $('#div_result4').empty();
    $.ajax({
        //global: false,
        url: "/Admin/Manage/getAccountsOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {

            htmlcontent = '<div class="row">' +
            '<div class="col-md-12">';

            if (data == '') {
                htmlcontent += '<div class="item-row no_result">No Results Found</div>';
            }
            $.each(data, function (i, item) {

                if (i % 2 == 0) {
                    htmlcontent += '<div class="item-row">'
                }

                htmlcontent += '<div class="list-item">' +
                    '<div>' +
                    '<img src="' + item.ImagePath + '" class="img-box">' +
                    '<div class="brand-details">' +
                    '<h4><a>' + item.AccName + '</a></h4>' +
                    '<div class="ibox-tools">' +
                    '<a  href="#" onclick="getSingleAccountDetails(' + "'" + item.AccCode + "'" + ');">' +
                    '<i class="fa fa-pencil"  data-toggle="modal" data-target="#myModal30" aria-hidden="true"></i></a>' +
                    '<a  href="#" onclick="return deleteSingleAccount(' + "'" + item.AccCode + "'" + ');"><i class="fa fa-trash" aria-hidden="true"></i></a>' +
                    '</div>' +
                    ' <span>e-Commerce</span>' +
                    '</div></div>' +
                    '<p>' + item.Description + '</p>' +
                    '</div>';
                if (i % 2 != 0) {
                    htmlcontent += '</div>';
                }

            });
            htmlcontent += '</div></div></div>';
            $('#div_result5').html(htmlcontent);
        }, async: true,
        error: OnError
    });
}
function createAccount() {
    debugger;
    var a = $("#btnCreateAccount").text();
    //alert($("#btnCreateAccount").text());
    var hjjh = $('#frmUplaodFileAdd')[0];
    var formData = new FormData($('#frmUplaodFileAdd')[0]);
    $.ajax({
        url: '/Admin/Manage/CreateAccount',  //Server script to process data
        type: 'POST',
        //xhr: function () {  // Custom XMLHttpRequest
        //    var myXhr = $.ajaxSettings.xhr();
        //    if (myXhr.upload) { // Check if upload property exists
        //        myXhr.upload.addEventListener('progress',
        //        progressHandlingFunction, false); // For handling the progress of the upload
        //    }
        //    return myXhr;
        //},
        data: formData,
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            error = 0;
            closepoup(data);
        }, async: true,
        error: OnError
    });
    
    return false;
}
function getSingleAccountDetails(AccountCode) {

    ClearAccountPopup();
    $('#TitlePopupAccount').text("Edit Account");
    $('#hdnAccountCode').val('0');
    $('#hdnAccountCode').val(AccountCode);
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'AccountCode': AccountCode,
    });
    $.ajax({
        url: "/Admin/Manage/getSingleAccountDetails",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();

        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            //Binding Student Details In Student Popup
            $('#txtAccountName').val(data[0].AccName);
            $("#imgAcc").removeClass("hidden").addClass("show");
            $("#imgAcc").attr("src", data[0].ImagePath);
            $('#txtAccountDesrpsn').val(data[0].Description);
            $('#spntxtAccount').text(300 - $('#txtAccountDesrpsn').val().length + ' Characters Left');
        }, async: true,
        error: OnError
    });
}
function deleteSingleAccount(AccountCode) {
    if (confirm('Are you sure you want to delete?')) {
        var SelectedData = JSON.stringify({
            'AccountCode': AccountCode,
        });
        $.ajax({
            url: "/Admin/Manage/deleteSingleAccount",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            beforeSend: function () {
                $(".Loader").show();

            },
            complete: function () {
                $(".Loader").hide();
            },
            success: function (data) {
                if (data[0] == "0") {
                    $('#AccountSuccessHead').text('You can' + "'t" + '  delete this account as the account is already assigned to students.');
                }
                if (data[0] == "1") {
                    $('#AccountSuccessHead').text('Account is deleted successfully');
                    getAccountsOnDemand();
                }
                //getAccountsOnDemand();
                //$('#AccountSuccessHead').text('You have Deleted account successfully');
                $('#ModalAccountSuccess').modal('show');
                $('#myModal30').modal('hide');
            }, async: true,
            error: OnError
        });
    }
    else {
        return false;
    }
}

//#endregion Trainer


//#region Change 

//#endregion Trainer

//#region CommonFunction
function HelperfnExcelReport(htmlcontent, Name) {
    var tab_text = htmlcontent;
    var textRange; var j = 0;

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
    {
        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, Name + ".xls");
    }
    else //other browser not tested on IE 11
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
    return (sa);
}
function fadeOut(id) {

    $("#" + id).fadeOut(function () {
        $(this).fadeIn();
    });
}
function OpenExcelPopup(Type1) {

    $('#hdnIdenType').val(Type1);
    ClearExcelPopup();
    ShowPopup();
}
function ExcelValidation() {
    debugger;
    if ($('#excelfile').val().length == 0) {
        $('#divExcelNotify').text("csv file is required");
        $('#excelfile').focus();
        $('#divExcelNotify').show();
        return false;
    }
    else {
        //$("#btnCreateAccount").text('1');
        //$("#btnCreateAccount").attr("disabled", true);
        if (confirm('Are you sure you want to continue?')) {
            if ($('#hdnIdenType').val() == 'TRAINER') {
                return SaveTrainerExcel();
            }
            if ($('#hdnIdenType').val() == 'STUDENT') {
                return SaveStudentExcel();
            }
            if ($('#hdnIdenType').val() == 'PHRASE') {
                return SavePhraseExcel();
            }
            if ($('#hdnIdenType').val() == 'KEYWORD') {
                return SaveKeywordExcel();
            }
            else {
                return null;
            }

        }
        else {
            return false;
        }
    }
}



//Table To Excel
var tableToExcel = (function () {
    //var uri = 'data:application/vnd.ms-excel;base64,'
    //  , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
    //  , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
    //  , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    //return function (table, name) {
    //    if (!table.nodeType) table = document.getElementById(table)
    //    var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
    //    window.location.href = uri + base64(format(template, ctx))
    //}
})()
//#endregion CommonFunction
function OldAdminDataTable(id) {
    //check admin.css DataTable section if you want to hide header paging
    $('#' + id).dataTable({
        "searching": false,
        //scrollY: '50vh',
        //scrollCollapse: true,
        //scrollY: '50vh',
        //scrollCollapse: true,
        order: [],
        columnDefs: [{ orderable: false, targets: [0] }],
        "bLengthChange": true,
        "language": {
            "emptyTable": "No results found"
        },
        "oLanguage":
        {
            "sEmptyTable": "No records found"
        }
    });
}
function ApplyAdminDataTable(id, disableSorting) {
    var t = $('#' + id).DataTable({
        "searching": false,
        "order": [],
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "language": {
            "emptyTable": "No results found"
        },
        //scrollY: '50vh',
        //scrollCollapse: true,
        //scrollY: '50vh',
        //scrollCollapse: true,
        aoColumnDefs: [
                           { aTargets: [0], bSortable: false },
                           { aTargets: [parseInt(disableSorting.split(",")[0])], bSortable: false },
                           { aTargets: [parseInt(disableSorting.split(",")[1])], bSortable: false }
        ],
        //"order": [[1, 'asc']]
    });

    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}
function ApplyAdminDataTableWithSrch(id, disableSorting, Search) {
    var t = $('#' + id).DataTable({
        "searching": Search,
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        aoColumnDefs: [
                           { aTargets: [0], bSortable: false },
                           { aTargets: [parseInt(disableSorting.split(",")[0])], bSortable: false },
                           { aTargets: [parseInt(disableSorting.split(",")[1])], bSortable: false }
        ],
        "language": {
            "emptyTable": "No results found"
        },
        "order": [[1, 'asc']]
    });

    var oTable;
    oTable = $('#' + id).dataTable();

    $('#selectError').change(function () {
        oTable.fnFilter($(this).val());
    });

    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}
function refreshPage() {
    window.location.reload();
}

function OnError(xhr, errorType, exception) {
    debugger;
    var responseText;
    $("#dialog").html("");
    try {
        responseText = jQuery.parseJSON(xhr.responseText);
        $("#dialog").append("<div><b>" + errorType + " " + exception + "</b></div>");
        $("#dialog").append("<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>");
        $("#dialog").append("<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>");
        $("#dialog").append("<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>");

    } catch (e)
    {
        responseText = xhr.responseText;
        $("body").html(responseText);
    }
    $("#dialog").dialog({
        title: "Error Details",
        width: 900,
        height: 600,
        buttons: {
            Close: function () {
                $(this).dialog('close');
            }
        }
    });
}

$(document).ajaxStart(function () {

    $(".Loader").show();
});
$(document).ajaxStop(function () {
   
    $(".Loader").hide();
});
$(document).ajaxError(function () {
   
    $(".Loader").hide();
});
window.onerror = function (msg, url, line)
{
    $(".Loader").hide();
    //alert("Message : " + msg + "\n" + "url : " + url+ "\n" + "Line number : " + line);
}