﻿$(document).ready(function () {
    //var msg = "";
    //$("#btnCreateCamp").click(function () {
    //    //alert("Handler for .click() called.");

    //    if ($("#txtCampaignName").val() == '') {
    //        alert("Please Enter Campaign Name");
    //        return false;
    //    }
    //    else if ($("#ddlNetwordType").val() == '0') {
    //        alert("Please Select Network Type");
    //        return false;
    //    }
    //    else if ($("#txtLocation").val() == '') {
    //        alert("Please Enter Location");
    //        return false;
    //    }
    //    else if ($("#txtBudget").val() == '') {
    //        alert("Please Enter Budget");
    //        return false;
    //    }
    //    else {
    //        return true;
    //    }
    //});
    //$("#ddlCampaign").change(function () {

    //    getCamp(this.value);

    //});
    //$("#ddlCampaignAD1").change(function () {

    //    getAdGroupsOnly(this.value);

    //});
    //$("#ddlCampaignAD").change(function () {

    //    getAdGroupsOnly(this.value);

    //});
    //$("#ddlADGroupName").change(function () {
    //    //  alert('sss');
    //    // getAdGroupsOnly(this.value);
    //    $("#hidADGName").val($("#ddlADGroupName option:selected").text());
    //    // alert(  $("#ddlADGroupName option:selected").text());
    //    // alert(this.is("selected").text());


    //});
    //$("#ddlCampaign2").change(function () {

    //    getAdGroups(this.value);

    //});
    //$("#ddlCampaignAD1").change(function () {

    //    getAds(this.value, 'ALL');
    //});
    //$("#ddlADGroupName1").change(function () {


    //    getAds($("#ddlCampaignAD1").val(), this.value);
    //});
    //getAllCamp();
});


function getCamp(cCode) {
    var htmlcontent = "";
    $('#div_result').empty();
    $.ajax({
        url: "/Students/Home/GetCampaignData?cCode=" + cCode + "",
        method: 'GET',
        //data: { cCode: '1' },
        success: function (data) {

            htmlcontent = '<div class="row">' +

                               '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table>' +
                                '<tr>' +
                                '<th>Campaigns</th>' +
                                '<th>Budget</th>' +
                                '<th>Status</th>' +
                                '<th>Impressions</th>' +
                                '<th>Clicks</th>' +
                                '<th>CTR </th>' +
                                '<th>Cost</th>' +
                                '<th>Avg CPC</th>' +
                                '<th>Conversions</th>' +
                                '<th>Cost Per Conversion</th>' +
                                '</tr>';


            $.each(data, function (i, item) {

                htmlcontent += '<tr><td><a href=\'javascript:void(0)\' onclick="getgroup(\'' + item.CampaignCode + '\');" >' + item.CampaignName + '</a></td>' +
                                '<td>' + item.Budget + '</td>' +
                                "<td></td>" +
                                '<td></td>' +
                                '<td></td>' +
                                '<td></td>' +
                                '<td></td>' +
                                '<td></td>' +
                                '<td></td>' +
                                '<td></td></tr>	';


            });
        }, async: false,
        error: OnError
    });
    htmlcontent += '</table></div></div></div>';
    $('#div_result').html(htmlcontent);

}

//Start Trainer Block
function getCurrentBatches(cCode) {
    $('#h1Batch').empty();
    if (cCode == 'c')
        $('#h1Batch').text("Current Batches");
    if (cCode == 'f')
        $('#h1Batch').text("Future Batches");
    var htmlcontent = "";
    $('#div_result1').empty();
    $.ajax({
        url: "/Trainers/Home/GetCurrentBatchesData?cCode=" + cCode + "",
        method: 'GET',
        cache: false,
        success: function (data) {
            htmlcontent = '<div class="campaigns-tab">' + '<div class="container">' + '<div class="row">' +
            '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table id="tableBatch">' +
                                '<thead><tr>' +
                                '<th>Batches</th>' +
                                '<th>Office</th>' +
                                '<th>Address</th>' +
                                '<th>Start Date</th>' +
                                '<th>End Date</th>' +
                                //'<th>Time </th>' +
                                '<th></th>' +
                                '</thead></tr><tbody>';
            $.each(data, function (i, item) {
                htmlcontent += '<tr><td><a id ="' + item.BatchCode + '" href="/Trainers/Home/CreateAccount/' + item.BatchCode + '">' + item.BatchName + '</a></td>' +
                                '<td>' + item.Location + '</td>' +
                                '<td>' + item.Address + '</td>' +
                                '<td>' + item.StartDate + '</td>' +
                                '<td>' + item.EndDate + '</td>' +
                                //'<td>' + item.Time + '</td>' +
                                '<td><a href="/Trainers/Home/CreateAccount/' + item.BatchCode + '");" >View</a></td>';
            });
            htmlcontent += '</tbody></table></div></div></div></div></div>';
            $('#div_result1').html(htmlcontent);
            BatchDataTable();
        }, async: true,
        error: OnError
    });

}
function GetAllAccountDetails(accCode) {
    var htmlcontent = "";
    $('#AllAccounts').empty();
    $.ajax({
        url: "/Trainers/Home/GetAllAccountDetails?cCode=" + accCode + "",
        method: 'GET',
        cache: false,
        //data: { cCode: '1' },
        success: function (data) {
            htmlcontent = '<div id="AllAccounts1" class="panel-body">' + '<ul>';
            $.each(data, function (i, item) {
                htmlcontent += '<li><a id="' + item.AccCode + '"><div class="logo-img-scroll"><img src="' + item.ImagePath + '" alt="amazone" /> <span>' + item.AccName + '</span></div> <div class="clearfix"></div></a></li>';
            });
        }, async: false,
        error: OnError
    });
    htmlcontent += '</ul></div>';
    $('#AllAccounts').html(htmlcontent);
}
function GetAllBatches(BatchCode) {
    var htmlcontentStudent = "";
    var htmlcontentAccount = "";
    var htmlcontentSavedAccount = "";

    $('#ddlBatch').empty();
    $('#StudentPerBatch1').empty();
    //$('#ddlBatch').append($('<option></option>').val("").html("Choose Batch"));
    $.ajax({
        url: "/Trainers/Home/GetAllBatches?BatchCode=" + BatchCode + "",
        method: 'GET',
        cache: false,
        success: function (data) {
            //Binding Dropdown list
            $.each(data[0], function (i, item) {

                if (item.BatchCode == data[1].BatchCode) {
                    $('#ddlBatch').append($('<option selected></option>').val(item.BatchCode).html(item.BatchName));
                }
                else {
                    $('#ddlBatch').append($('<option></option>').val(item.BatchCode).html(item.BatchName));
                }
            });

            //Binding Account list
            htmlcontentAccount = '<ul>';
            if (data[3] == '') {
                htmlcontentAccount += '<span>No Data is Present</span>';
            }
            $.each(data[3], function (i, item) {
                htmlcontentAccount += '<li><a id="' + item.AccCode + '"><div class="logo-img-scroll  col-left"><img src="' + item.ImagePath + '" alt="amazone" /> <span>' + item.AccName + '</span></div> <div class="clearfix"></div></a></li>';
            });

            //Binding Student list
            htmlcontentStudent = '<ul>';
            if (data[2] == '') {
                htmlcontentStudent += '<span>No Data is Present</span>';
                $("#aResetAccount").addClass("hidden");
                $("#btnSaveDraft").addClass("hidden1");
            }
            $.each(data[2], function (i, item) {
                $("#aResetAccount").removeClass("hidden");
                $("#btnSaveDraft").removeClass("hidden1");
                htmlcontentStudent += '<li><a id="' + item.UserID + '">' + item.StudentFirstName + '</a></li>';
            });

            //Binding Batch basic Details list
            $('#spnBatchNam').text(data[1].BatchName);
            $('#spnLocation').text(data[1].Location);
            $('#spnTrainerNam').text(data[1].TrainerName);
            $('#spnStartDate').text(data[1].StartDate);
            $('#spnTime').text(data[1].Time);

            //Binding Saved Account Details
            $.each(data[4], function (i, item) {
                htmlcontentSavedAccount += ' <div class=" col-md-12">' +
                    '<div class=" col-border">' +
                        '<div id="' + item.AccCode + '" class="col-md-4">' +
                           '<div class="logo-img col-left"><img src="' +
                           item.ImagePath + '" class="img-responsive"'
                + 'alt="image" /></div>' +
                            '<div class="col-right">' +
                            item.AccName + '</div></div>' +
                        '<div class="col-md-8  col-border-left">' +

                            '<div class="student-col">' +
                                '<div class="col-icon"><img src="/Content/images/trainer/group-icon.png" alt="icon" />' +
                                '</div>' +
                                '<div id= "' + item.AccCode + '"class="col-icon-right text-right">' +
                                    '<ul>';
                if (item.rfaId != 0) {
                    htmlcontentSavedAccount += '<li><button id="btnFinalSubmit" class="button btn-float" onclick="CancelAuction(' + "'" + item.rfaId + "','" + item.BatchCode + "','" + item.AccCode + "'" + ');">Cancel RFA Request</button></li>';
                }
                if (item.Status == 0) {
                    htmlcontentSavedAccount += '<li><i class="fa fa-save" aria-hidden="true" style="color:#ff975a; font-size:22px;"></i>' + '</li>' +
                    '<li><a id="' + item.BatchCode + '"  onclick="EditAccountDetails(' + "'" + item.BatchCode + "'" + ",'" + item.AccCode + "'" + ');"><img src="/Content/images/trainer/edit-icon.png" alt="Edit"/></a></li>' +
                    '<li><a onclick="DeleteAccountDetails(' + "'" + item.BatchCode + "'" + ",'" + item.AccCode + "'" + ');"><img src="/Content/images/trainer/cancel-icon.png" alt="Cancel" /></a></li>';
                }
                if (item.Status == 1) {
                    htmlcontentSavedAccount += '<li class="hidden"><div class="lblGray">Submitted</div>' +
                    '</li>';
                }
                htmlcontentSavedAccount += '</ul>' +
            '</div>' +
            '<div class="clearfix"></div>' +
        '</div>' +
        '<p>' + item.UserNames + '</p>' +
    '</div>' +

    '<div class="clearfix"></div>' +
'</div>' +
'</div>';
            });
        }, async: false,
        error: OnError
    });

    htmlcontentAccount += '</ul>';
    $('#AllAccounts1').html(htmlcontentAccount);

    htmlcontentStudent += '</ul></div>';
    $('#StudentPerBatch1').html(htmlcontentStudent);
    $('#SavedAcountDetails').html(htmlcontentSavedAccount);

    AccountSelection();
    StudentSelection();

    $("#btnUpdateDraft").addClass("hidden");
    $("#btnSaveDraft").removeClass("hidden");
    $("#btnFinalSubmit").removeClass("hidden");
    $('#StudentsCount').text("You have select only " + 0);
    $("#aResetAccount").text('Reset');
    $("#hdnResetAccount").val(BatchCode);
    //$('#aResetAccount').on('click', GetAllBatches(BatchCode));
    //  $('#aResetAccount').click(GetAllBatches(BatchCode));

    CheckRfaApplicable();
}
function StudentSelection() {
    $("#StudentPerBatch1 ul li").click(function () {
        if ($(this).hasClass("active")) {
            $(this).toggleClass("active");
            //alert("active to toggle");
        }
        else if ($('#StudentPerBatch1 li.active').length < 5) {
            //alert("unactive to active");
            $(this).toggleClass("active");
        }
        else {
            $(this).toggleClass("active");
            // alert('Only 5 Students are allowed');
        }
        $('#StudentsCount').text("You have select only " + $('#StudentPerBatch1 li.active').length);
        //alert($("#StudentPerBatch1 li.active").children().attr('id'));
        //$("#StudentPerBatch1 li.active").children().each(function () {
        //    alert("Focused Elem_id = " + this.id);
        //});
    });
}
function AccountSelection() {
    $("#AllAccounts1 ul li").click(function () {
        if ($(this).hasClass("active")) {
            $(this).toggleClass("active");
            //alert("active to toggle");
        }
        else if ($('#AllAccounts1 li.active').length < 1) {
            //alert("unactive to active");
            $(this).toggleClass("active");
        }
        else {
            alert('Only 1 Account is allowed');
        }
    });
}
function SaveAccount(StatusCode) {
    var selectedStudent = "";
    var selectedAccount = "";
    if ($('#StudentPerBatch1 li.active').length > 0) {
        $("#StudentPerBatch1 li.active").children().each(function () {
            selectedStudent += this.id + ",";
        });
        // alert(selectedStudent);
    }
    if ($('#AllAccounts1 li.active').length > 0) {
        $("#AllAccounts1 li.active").children().each(function () {
            selectedAccount += this.id + ",";
        });
        // alert(selectedAccount);
    }
    if (selectedStudent == '' || selectedAccount == '') {
        alert('Select atleast one Student with one account');
    }
    else {

        //var json = JSON.stringify(SelectedData);
        var SelectedData = JSON.stringify({
            'AccountDetails': selectedAccount,
            'StudentDetails': selectedStudent,
            'Status': StatusCode,
        });
        //alert(SelectedData);
        $.ajax({
            //url: "/Trainers/Home/SaveAccount?SelectedData=" + json + "",
            url: "/Trainers/Home/SaveAccount",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            success: function (data) {
                alert("Saved Successfully");
                GetAllBatches($("#ddlBatch option:selected").val());
            }, async: false,
            error: OnError
        });
    }

    //-------------Start Sample Syntax------------
    //alert("Focused Elem_id = " + this.id);
    //alert($("#AllAccounts1 li.active").children(":first").attr('id'));
    //alert($("#AllAccounts1 li.active").children(":first").attr('id'));
    //---------------End Sample Syntax---------
}
function FSaveAccount(StatusCode) {
    if (confirm("Are you sure you want to continue...?")) {
        //var json = JSON.stringify(SelectedData);
        var SelectedData = JSON.stringify({
            'Status': StatusCode,
            'BatchCode': $('#ddlBatch').val(),
        });
        //alert(SelectedData);
        $.ajax({
            //url: "/Trainers/Home/SaveAccount?SelectedData=" + json + "",
            url: "/Trainers/Home/FSaveAccount",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            success: function (data)
            {
                if (data == true)
                {
                    alert("Submitted Successfully");
                    GetAllBatches($("#ddlBatch option:selected").val());
                }
                if (data == false) {
                    alert("No data is present to submit");
                }
            }, async: false,
            error: OnError
        });
    }
}
function EditAccountDetails(BatchCode, AccCode) {
    var StudentCount = 0;
    var htmlcontentAccount = "";
    var htmlcontentSavedAccount = "";
    var SelectedData = JSON.stringify({
        'BatchCode': BatchCode,
        'AccCode': AccCode,
    });
    $('#StudentPerBatch1').empty();
    $.ajax({
        url: "/Trainers/Home/EditAccountDetails",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        success: function (data) {
            BindStudentList(data[2]);
            BindAccountList(data[3]);
        }, async: false,
        error: OnError
    });
    StudentCount = $('#StudentPerBatch1 li.active').length
    AccountSelection();
    StudentSelection();
    $('#StudentsCount').text("You have select only " + StudentCount);
    $("#btnUpdateDraft").removeClass("hidden");
    $("#btnSaveDraft").addClass("hidden");
    $("#btnUpdateDraft").removeClass("hidden");
    $("#btnFinalSubmit").addClass("hidden");
    $("#aResetAccount").removeClass("hidden");
    $("#aResetAccount").text('Cancel');
    $("#hdnResetAccount").val(BatchCode);
    //$('#aResetAccount').on('click', GetAllBatches(BatchCode));
    //$('#aResetAccount').click(GetAllBatches(BatchCode));
}
function UpdateAccount(StatusCode) {
    var selectedStudent = "";
    var selectedAccount = "";
    if ($('#StudentPerBatch1 li.active').length > 0) {
        $("#StudentPerBatch1 li.active").children().each(function () {
            selectedStudent += this.id + ",";
        });
        // alert(selectedStudent);
    }
    if ($('#AllAccounts1 li.active').length > 0) {
        $("#AllAccounts1 li.active").children().each(function () {
            selectedAccount += this.id + ",";
        });
        // alert(selectedAccount);
    }
    if (selectedStudent == '' || selectedAccount == '') {
        alert('Select atleast one Student with one account');
    }
    else {

        //var json = JSON.stringify(SelectedData);
        var SelectedData = JSON.stringify({
            'AccountDetails': selectedAccount,
            'StudentDetails': selectedStudent,
            'Status': StatusCode,
        });
        //alert(SelectedData);
        $.ajax({
            //url: "/Trainers/Home/SaveAccount?SelectedData=" + json + "",
            url: "/Trainers/Home/UpdateAccount",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            success: function (data) {
                alert("Updated Successfully");
                GetAllBatches($("#ddlBatch option:selected").val());
            }, async: false,
            error: OnError
        });
    }

    //-------------Start Sample Syntax------------
    //alert("Focused Elem_id = " + this.id);
    //alert($("#AllAccounts1 li.active").children(":first").attr('id'));
    //alert($("#AllAccounts1 li.active").children(":first").attr('id'));
    //---------------End Sample Syntax---------
}
function DeleteAccountDetails(BatchCode, AccCode) {
    if (confirm("Are you sure you want to delete. ?")) {

        var SelectedData = JSON.stringify({
            'BatchCode': BatchCode,
            'AccCode': AccCode,
        });
        $.ajax({
            url: "/Trainers/Home/DeleteAccountDetails",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            success: function (data) {
                alert("Deleted Successfully");
                GetAllBatches($("#ddlBatch option:selected").val());
            }, async: false,
            error: OnError
        });
    }
}
function BindStudentList(data) {
    //Binding Student list
    var htmlcontentStudent = "";
    htmlcontentStudent = '<ul>';
    if (data == '') {
        htmlcontentStudent += '<span>No Data is Present</span>';
    }
    $.each(data, function (i, item) {
        if (item.AccCode == null) {
            htmlcontentStudent += '<li><a id="' + item.UserID + '">' + item.StudentFirstName + '</a></li>';
        }
        else {
            htmlcontentStudent += '<li class="active"><a id="' + item.UserID + '">' + item.StudentFirstName + '</a></li>';
        }
    });
    htmlcontentStudent += '</ul></div>';
    $('#StudentPerBatch1').html(htmlcontentStudent);
}
function BindAccountList(data) {
    //Binding Account list
    var htmlcontentAccount = "";
    htmlcontentAccount = '<ul>';
    if (data == '') {
        htmlcontentAccount += '<span>No Data is Present</span>';
    }
    $.each(data, function (i, item) {
        htmlcontentAccount += '<li class="active"><a id="' + item.AccCode + '"><div class="logo-img-scroll col-left"><img src="' + item.ImagePath + '" alt="amazone" /> <span>' + item.AccName + '</span></div> <div class="clearfix"></div></a></li>';
    });
    htmlcontentAccount += '</ul>';
    $('#AllAccounts1').html(htmlcontentAccount);
}
function CancelAuction(rfaId, BatchCode, AccCode) {
    if (confirm("Are you sure you want to continue...?")) {
        var SelectedData = JSON.stringify({
            'rfaId': rfaId,
            'BatchCode': BatchCode,
            'AccCode': AccCode,
        });
        //alert(SelectedData);
        $.ajax({
            //url: "/Trainers/Home/SaveAccount?SelectedData=" + json + "",
            url: "/Trainers/Home/CancelAuction",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            success: function (data) {
                if (data == true) {
                    alert("Request Canceled Successfully");
                    GetAllBatches($("#ddlBatch option:selected").val());
                }
                if (data == false) {
                }
            }, async: false,
            error: OnError
        });
    }
}
function StartRfa() {
    var SelectedData = JSON.stringify({
        'CurrentBatchCode': $('#ddlBatch').val(),
    });
    $.ajax({
        url: "/Trainers/Home/RFA",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
            $("#msgDiv").html("RFA is running...");
        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            alert(data);
        }, async: true,
        error: OnError
    });
}
//Report Section

//Report Section
function GetBatchDropdown() {
    debugger;
    $('#ddlBatchReport').empty();
    $.ajax({
        url: "/Trainers/Report/GetBatchDropdown",
        method: 'GET',
        cache: false,
        success: function (data) {
            debugger;
            if (data[0].length > 0) {
                //Binding Dropdown list
                $.each(data[0], function (i, item) {
                    if (i == 0)
                        $('#ddlBatchReport').append($('<option selected></option>').val(item.BatchCode).html(item.BatchName));
                    else {
                        $('#ddlBatchReport').append($('<option></option>').val(item.BatchCode).html(item.BatchName));
                    }
                });
            }
            else {
                window.location.href = '/Trainers/Report/NoBatch';
                response.end();
                response.flush();
                event.stopPropagation();
            }
        }, async: false,
        error: OnError
    });
}
function GetAccountDropdown() {
    var BatchCode = $('#ddlBatchReport').val();
    $('#ddlAccount').empty();
    $('#ddlAccount').append($('<option selected></option>').val("").html("ALL"));
    $.ajax({
        url: "/Trainers/Report/GetAccountDropdown?BatchCode=" + BatchCode + "",
        method: 'GET',
        cache: false,
        success: function (data) {
            //Binding Dropdown list
            $.each(data[0], function (i, item) {
                if (i == 0)
                    $('#ddlAccount').append($('<option></option>').val(item.AccCode).html(item.AccName));
                else {
                    $('#ddlAccount').append($('<option></option>').val(item.AccCode).html(item.AccName));
                }
            });
        }, async: true,
        error: OnError
    });
}
function GetBatchDetails() {
    var BatchCode = $('#ddlBatchReport').val();
    $.ajax({
        url: "/Trainers/Report/GetBatchDetails?BatchCode=" + BatchCode + "",
        method: 'GET',
        cache: false,
        success: function (data) {
            //Binding Batch basic Details list
            $('#spnBatchNamReport').text(data[0].BatchName);
            $('#spnLocationReport').text(data[0].Location);
            $('#spnTrainerNamReport').text(data[0].TrainerName);
            $('#spnStartDateReport').text(data[0].StartDate);
        }, async: false,
        error: OnError
    });
}


function CheckRfaApplicable() {

    var htmlAcccontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatch').val()
    });
    //$('#div_resultAccRpt').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Home/CheckRfaApplicable",
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
            if (data[0] == "TRUE") {
                $("#rfaBtn").text('Ready for RFA');
                $("#rfaBtn").css("pointer-events", "");
                $("#rfaBtn").css("opacity", "");
            }
            if (data[0] == "FALSE") {
                $("#rfaBtn").text('Waiting for RFA');
                $("#rfaBtn").css("pointer-events", "none");
                $("#rfaBtn").css("opacity", "0.6");
            }
            //$("#rfaBtn").addClass()
        }, async: true,
        error: OnError
    });
}
function GetAccountReport() {
    var htmlAcccontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val(),
        'AccCode': $('#ddlAccount').val()
    });
    $('#div_resultAccRpt').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Report/GetAccountReport",
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
            var Budget = 0;
            var Imps = 0;
            var RevNewImps = 0;
            var Clicks = 0;
            var CTR = 0;
            var Cost = 0;
            var CPC = 0;
            var cnver = 0;
            var CostCpc = 0;
            var CountCamp = 0;
            htmlAcccontent = '<div class="row">' +
           '<div class="col-md-12">' +
            '<div class="campaings-table-border table-responsive">' +
            '<table id="tbl_resultAccRpt">' +
            '<thead id="AccHead">' +
            '<tr>' +
            '<th>Sno.</th>' +
            '<th>Acc Name</th>' +
            '<th>Budget</th>' +
            '<th title="Impression">Impression</th>' +
            '<th title="Clicks">Clicks</th>' +
            '<th title="CTR (%)">CTR (%)</th>' +
            '<th title="Average Cost per click">Avg CPC</th>' +
            '<th title="Conversions">Conversions</th>' +
            '<th title="CVR(%)">CVR(%)</th>' +//8
            '<th title="CPA (Cost per acquisition)">CPA (Cost per Conversion)</th>' +
            '<th title="Average Position">Avg Pos</th>' +
            '<th title="Cost">Cost</th>' +
            '<th title="Impression Share (%)">Impression Share (%)</th>' +//6
            '<th title="Impression Share Lost (%)">Impression Share Lost (%)</th>' +//7
            '</tr></thead><tbody id="CampaignBody">';
            if (data == '')
            {
               // htmlAcccontent += '<tr><td colspan="9" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var index = 0;
            $.each(data[0], function (i, item)
            {
                index = index + 1;
                htmlAcccontent += '<tr><td>' + index + '</td> ' +
                                 '<td><a href="#campaigns" onClick=\' $("#hdnaccCode").val(' + item.AccCode + '); $("#li_AccReportCampaigns a").click();\'>' + item.AccName + '</a></td>' +
                                 '<td>' + (item.Budget).toFixed(2) + '</td>' +
                                 '<td>' + (item.NewImpression).toFixed(0) + '</td>' +
                                 '<td>' + (item.NewClicks).toFixed(0) + '</td>' +
                                 '<td>' + (item.CTR).toFixed(2) + '</td>' +
                                 '<td>' + (item.RevNewCostPerClick).toFixed(2) + '</td>' +
                                 '<td>' + item.NewConversions.toFixed(0) + '</td>' +
                                 '<td>' + (item.CVR).toFixed(2) + '</td>' +
                                 '<td>' + item.NewCostPerConversions.toFixed(2) + '</td>' +
                                 '<td>' + item.AvgPos.toFixed(0) + '</td>' +
                                 '<td>' + (item.NewCost).toFixed(2) + '</td>' +
                                 '<td>' + (item.ImpressionShare).toFixed(2) + '</td>' +
                                 '<td>' + (item.ImpressionLost).toFixed(2) + '</td>';

                RevNewImps += parseFloat(checkint(item.NewImpression).toFixed(0));
                Imps += parseFloat(checkint(item.Impressions).toFixed(0));
                Clicks += parseFloat(checkint(item.NewClicks).toFixed(0));
                // CTR += parseFloat(checkint(item.CTR).toFixed(2));
                Cost += parseFloat(checkint(item.NewCost).toFixed(2));
                CPC += parseFloat(checkint(item.RevNewCostPerClick).toFixed(2));
                cnver += parseFloat(checkint(item.NewConversions).toFixed(0));
                CostCpc += parseFloat(checkint(item.NewCostPerConversions).toFixed(2));
                Budget += parseFloat(checkint(item.Budget));

            });
            if (data != '') {
                debugger;
                var TotalCTR = ((parseFloat(Clicks.toFixed(0)) / parseFloat(RevNewImps.toFixed(0))) * 100).toFixed(2).replace("NaN", "0.00");
                var ImpressionShare = (RevNewImps.toFixed(0) / Imps.toFixed(0)).toFixed(2).replace("NaN", "0.00");
                var ImpressionLost = (parseFloat(ImpressionShare) == 0) ? "0.00" : (100 - parseFloat(ImpressionShare)).toFixed(2);
                //var ImpressionLost = (100 - parseFloat(ImpressionShare)).toFixed(2);
                var CVR = (parseFloat(cnver.toFixed(0)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");;
                var AvgCPC = (parseFloat(Cost.toFixed(2)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");


                htmlAcccontent += '<tfoot id="KeywordFooter"><tr><td><b>Total</b></td>' +
                               '<td></td>' +
                               '<td>' + Budget.toFixed(2) + '</td>' +
                               '<td>' + RevNewImps.toFixed(0) + '</td>' +
                               '<td>' + Clicks.toFixed(0) + '</td>' +
                               '<td>' + TotalCTR + '</td>' + //7
                               '<td></td>' + //8
                               '<td>' + (parseFloat(cnver)).toFixed(0) + '</td>' +
                               '<td></td>' +
                               '<td>' + ((Cost).toFixed(2) / cnver.toFixed(0)).toFixed(2).replace("NaN", "0.00") + '</td>' +
                               '<td></td>' +
                               '<td>' + (parseFloat(Cost)).toFixed(2) + '</td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '</tr>';

                htmlAcccontent += '<tr><td><b>Avg</b></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +//6
                                   '<td></td>' +//7
                                   '<td>' + AvgCPC + '</td>' +//8
                                   '<td></td>' +
                                   '<td>' + CVR + '</td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td>' + ImpressionShare + '</td>' +
                                   '<td>' + ImpressionLost + '</td>' +
        '</tr></tfoot>';
            }
            htmlAcccontent += '</tbody></table></div></div></div>';
            $('#div_resultAccRpt').html(htmlAcccontent);
            ApplyDataTable("tbl_resultAccRpt", "0");
            // IndustryTopSearchDataTable();
        }, async: true,
        error: OnError
    });
}
function GetAccountCampReport() {
    var htmlAcccontent1 = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val(),
        'AccCode': $('#hdnaccCode').val(),
    });
    $('#div_resultAccCampRpt').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Report/GetAccountCampReport",
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
            var Budget = 0;
            var Imps = 0;
            var RevNewImps = 0;
            var Clicks = 0;
            var CTR = 0;
            var Cost = 0;
            var CPC = 0;
            var cnver = 0;
            var CostCpc = 0;
            var CountCamp = 0;

            htmlAcccontent1 = '<div class="row">' +
                '<div class="col-md-12">' +
                 '<div class="campaings-table-border table-responsive">' +
                 '<table id="tbl_resultAccCampRpt">' +
                 '<thead id="campaignHead"><tr>' +
                 '<th>Sno.</th>' +
                 '<th>Account Name</th>' +
                 '<th>Campaign Name</th>' +
                 '<th>Budget</th>' +
                 '<th title="Impression">Impression</th>' +
                 '<th title="Clicks">Clicks</th>' +
                 '<th title="CTR (%)">CTR (%)</th>' +
                 '<th title="Average Cost per click">Avg CPC</th>' +
                 '<th title="Conversions">Conversions</th>' +
                 '<th title="CVR(%)">CVR(%)</th>' +//8
                 '<th title="CPA (Cost per acquisition)">CPA (Cost per Conversion)</th>' +
                 '<th title="Average Position">Avg Pos</th>' +
                 '<th title="Cost">Cost</th>' +
                 '<th title="Impression Share (%)">Impression Share (%)</th>' +//6
                 '<th title="Impression Share Lost (%)">Impression Share Lost (%)</th>' +//7
                 '</tr></thead><tbody id="CampaignBody">';

            if (data == '') {
                //htmlAcccontent1 += '<tr><td colspan="15" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var index = 0;
            $.each(data[0], function (i, item) {
                debugger;
                index = index + 1;
                htmlAcccontent1 += '<tr><td>' + index + '</td> ' +
                    '<td>' + item.accName + '</td> ' +

                     '<td><a href="#AdGroup" onClick=\' $("#hdncampCode").val(' + item.CampaignCode + '); $("#li_AccReportAdGroup a").click();\'>' + item.CampaignName + '</a></td>' +
                                '<td>' + item.Budget.toFixed(2) + '</td>';

                htmlAcccontent1 += '<td>' + (item.NewImpression).toFixed(0) + '</td>' +
                             '<td>' + (item.NewClicks).toFixed(0) + '</td>' +
                             '<td>' + (item.CTR).toFixed(2) + '</td>' +
                             '<td>' + (item.RevNewCostPerClick).toFixed(2) + '</td>' +
                             '<td>' + item.NewConversions.toFixed(0) + '</td>' +
                             '<td>' + (item.CVR).toFixed(2) + '</td>' +
                             '<td>' + item.NewCostPerConversions.toFixed(2) + '</td>' +
                             '<td>' + item.AvgPos.toFixed(0) + '</td>' +
                             '<td>' + (item.NewCost).toFixed(2) + '</td>' +
                             '<td>' + (item.ImpressionShare).toFixed(2) + '</td>' +
                             '<td>' + (item.ImpressionLost).toFixed(2) + '</td>';
                RevNewImps += parseFloat(checkint(item.NewImpression).toFixed(0));
                Imps += parseFloat(checkint(item.Impressions).toFixed(0));
                Clicks += parseFloat(checkint(item.NewClicks).toFixed(0));
                // CTR += parseFloat(checkint(item.CTR).toFixed(2));
                Cost += parseFloat(checkint(item.NewCost).toFixed(2));
                CPC += parseFloat(checkint(item.RevNewCostPerClick).toFixed(2));
                cnver += parseFloat(checkint(item.NewConversions).toFixed(0));
                CostCpc += parseFloat(checkint(item.NewCostPerConversions).toFixed(2));
                Budget += parseFloat(checkint(item.Budget));

            });

            if (data != '') {
                debugger;
                var TotalCTR = ((parseFloat(Clicks.toFixed(0)) / parseFloat(RevNewImps.toFixed(0))) * 100).toFixed(2).replace("NaN", "0.00");
                var ImpressionShare = (RevNewImps.toFixed(0) / Imps.toFixed(0)).toFixed(2).replace("NaN", "0.00");
                var ImpressionLost = (parseFloat(ImpressionShare) == 0) ? "0.00" : (100 - parseFloat(ImpressionShare)).toFixed(2);
                //var ImpressionLost = (100 - parseFloat(ImpressionShare)).toFixed(2);
                var CVR = (parseFloat(cnver.toFixed(0)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");;
                var AvgCPC = (parseFloat(Cost.toFixed(2)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");


                htmlAcccontent1 += '<tfoot id="KeywordFooter"><tr><td><b>Total</b></td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '<td>' + Budget.toFixed(2) + '</td>' +
                               '<td>' + RevNewImps.toFixed(0) + '</td>' +
                               '<td>' + Clicks.toFixed(0) + '</td>' +
                               '<td>' + TotalCTR + '</td>' + //7
                               '<td></td>' + //8
                               '<td>' + (parseFloat(cnver)).toFixed(0) + '</td>' +
                               '<td></td>' +
                               '<td>' + ((Cost).toFixed(2) / cnver.toFixed(0)).toFixed(2).replace("NaN", "0.00") + '</td>' +
                               '<td></td>' +
                               '<td>' + (parseFloat(Cost)).toFixed(2) + '</td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '</tr>';

                htmlAcccontent1 += '<tr><td><b>Avg</b></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +//6
                                   '<td></td>' +//7
                                   '<td>' + AvgCPC + '</td>' +//8
                                   '<td></td>' +
                                   '<td>' + CVR + '</td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td>' + ImpressionShare + '</td>' +
                                   '<td>' + ImpressionLost + '</td>' +
        '</tr></tfoot>';
            }

            htmlAcccontent1 += '</tbody></table></div></div></div>';
            $('#div_resultAccCampRpt').html(htmlAcccontent1);
            ApplyDataTable("tbl_resultAccCampRpt", "0");
            // IndustryTopSearchDataTable();
        }, async: true,
        error: OnError
    });
}
function GetAccountAdGrpReport() {
    var htmlAcccontent2 = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val(),
        'CampCode': $('#hdncampCode').val(),
    });
    $('#div_resultAccAdGrpRpt').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Report/GetAccountAdGrpReport",
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
            var Budget = 0;
            var Imps = 0;
            var RevNewImps = 0;
            var Clicks = 0.0;
            var CTR = 0;
            var Cost = 0;
            var CPC = 0;
            var cnver = 0;
            var CostCpc = 0;
            var CountCamp = 0;

            htmlAcccontent2 = '<div class="row">' +
                '<div class="col-md-12">' +
                 '<div class="campaings-table-border table-responsive">' +
                 '<table id="tbl_resultAccAdGrpRpt">' +
                 '<thead id="AdgroupHead"><tr>' +
                 '<th>SNo. </th>' +
                 '<th>Account Name</th>' +
                 '<th>Campaign Name</th>' +
                 '<th>Adgroup Name</th>' +
                 '<th>Budget</th>' +

                '<th title="Impression">Impression</th>' +
                '<th title="Clicks">Clicks</th>' +
                '<th title="CTR (%)">CTR (%)</th>' +
                '<th title="Average Cost per click">Avg CPC</th>' +
                '<th title="Conversions">Conversions</th>' +
                '<th title="CVR(%)">CVR(%)</th>' +//8
                '<th title="CPA (Cost per acquisition)">CPA (Cost per Conversion)</th>' +
                '<th title="Average Position">Avg Pos</th>' +
                '<th title="Cost">Cost</th>' +
                '<th title="Impression Share (%)">Impression Share (%)</th>' +//6
                '<th title="Impression Share Lost (%)">Impression Share Lost (%)</th>' +//7
              '</tr></thead><tbody id="AdgroupBody">';
            if (data == '') {
                //htmlAcccontent2 += '<tr><td colspan="15" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var index = 0;
            $.each(data[0], function (i, item) {
                index = index + 1;
                htmlAcccontent2 += '<tr><td>' + index + '</td>' +
                    '<td>' + item.accName + '</td>' +
                      '<td>' + item.CampaignName + '</td>' +
                                '<td><a href="#keyword" onClick=\' $("#hdnadgroupCode").val(' + item.AdGroupCode + '); $("#li_AccReportkeyword a").click();\'>' + item.AdGroupName + '</a></td>' +
                                '<td>' + item.Budget.toFixed(2) + '</td>';



                htmlAcccontent2 += '<td>' + (item.NewImpression).toFixed(0) + '</td>' +
                  '<td>' + (item.NewClicks).toFixed(0) + '</td>' +
                  '<td>' + (item.CTR).toFixed(2) + '</td>' +
                  '<td>' + (item.RevNewCostPerClick).toFixed(2) + '</td>' +
                  '<td>' + item.NewConversions.toFixed(0) + '</td>' +
                  '<td>' + (item.CVR).toFixed(2) + '</td>' +
                  '<td>' + item.NewCostPerConversions.toFixed(2) + '</td>' +
                  '<td>' + item.AvgPos.toFixed(0) + '</td>' +
                  '<td>' + (item.NewCost).toFixed(2) + '</td>' +
                  '<td>' + (item.ImpressionShare).toFixed(2) + '</td>' +
                  '<td>' + (item.ImpressionLost).toFixed(2) + '</td>';


                RevNewImps += parseFloat(checkint(item.NewImpression).toFixed(0));
                Imps += parseFloat(checkint(item.Impressions).toFixed(0));
                Clicks += parseFloat(checkint(item.NewClicks).toFixed(0));
                //CTR += parseFloat(checkint(item.CTR).toFixed(2));
                Cost += parseFloat(checkint(item.NewCost).toFixed(2));
                CPC += parseFloat(checkint(item.RevNewCostPerClick).toFixed(2));
                cnver += parseFloat(checkint(item.NewConversions).toFixed(0));
                CostCpc += parseFloat(checkint(item.NewCostPerConversions).toFixed(2));


                //'<td></td>' +
                ////'<td><a href="#" class="popup-btn" data-toggle="modal" data-target="#myModalEdit"  onclick="getEditCamp(\'' + item.CampaignCode + '\');"><img src="/Content/images/plus-icon.png" alt="icon" />Edit</a></td></tr>	';
                //'<td><a href="#"  data-toggle="modal" data-target="#myModal"  onclick="getEditCamp(\'' + item.BatchCode + '\');"><img src="/Content/images/student/edit-icon.png" alt="delete" /></a></td></tr>	';

            });

            if (data != '') {
                var TotalCTR = ((parseFloat(Clicks.toFixed(0)) / parseFloat(RevNewImps.toFixed(0))) * 100).toFixed(2).replace("NaN", "0.00");
                var ImpressionShare = (RevNewImps.toFixed(0) / Imps.toFixed(0)).toFixed(2).replace("NaN", "0.00");
                var ImpressionLost = (parseFloat(ImpressionShare) == 0) ? "0.00" : (100 - parseFloat(ImpressionShare)).toFixed(2);
                //var ImpressionLost = (100 - parseFloat(ImpressionShare)).toFixed(2);
                var CVR = (parseFloat(cnver.toFixed(0)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");;
                var AvgCPC = (parseFloat(Cost.toFixed(2)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");


                htmlAcccontent2 += '<tfoot id="KeywordFooter"><tr><td><b>Total</b></td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '<td>' + RevNewImps.toFixed(0) + '</td>' +
                               '<td>' + Clicks.toFixed(0) + '</td>' +
                               '<td>' + TotalCTR + '</td>' + //7
                               '<td></td>' + //8
                               '<td>' + (parseFloat(cnver)).toFixed(0) + '</td>' +
                               '<td></td>' +
                               '<td>' + ((Cost).toFixed(2) / cnver.toFixed(0)).toFixed(2).replace("NaN", "0.00") + '</td>' +
                               '<td></td>' +
                               '<td>' + (parseFloat(Cost)).toFixed(2) + '</td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '</tr>';

                htmlAcccontent2 += '<tr><td><b>Avg</b></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +//6
                                   '<td></td>' +//7
                                   '<td>' + AvgCPC + '</td>' +//8
                                   '<td></td>' +
                                   '<td>' + CVR + '</td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td>' + ImpressionShare + '</td>' +
                                   '<td>' + ImpressionLost + '</td>' +
                                   '</tr></tfoot>';
            }

            htmlAcccontent2 += '</tbody></table></div></div></div>';
            $('#div_resultAccAdGrpRpt').html(htmlAcccontent2);
            ApplyDataTable("tbl_resultAccAdGrpRpt", "0");
        }, async: true,
        error: OnError
    });
}
function GetAccountKeywrdReport() {
    var htmlAcccontent3 = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val(),
        'AdgroupCode': $('#hdnadgroupCode').val(),
    });
    $('#div_resultAccKeywrdRpt').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Report/GetAccountKeywrdReport",
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
            var Budget = 0;
            var Imps = 0;
            var RevNewImps = 0;
            var Clicks = 0;
            var CTR = 0;
            var Cost = 0;
            var CPC = 0;
            var cnver = 0;
            var CostCpc = 0;
            var CountCamp = 0;
            htmlAcccontent3 = '<div class="row">' +
                '<div class="col-md-12">' +
                 '<div class="campaings-table-border table-responsive">' +
                 '<table id="tbl_resultAccKeywrdRpt">' +
                 '<thead id="KeywrdPlnrErrHead"><tr>' +
                 '<th></th>' +
                 '<th title="Account Name">Account Name</th>' +
                 '<th title="Campaign Name">Campaign Name</th>' +
                 '<th title="Adgroup Name">Adgroup Name</th>' +
                 '<th title="Keyword Name">Keyword Name</th>' +
                 '<th title="Impression">Impression</th>' +
                 '<th title="Clicks">Clicks</th>' +
                 '<th title="CTR (%)">CTR (%)</th>' +
                 '<th title="Average Cost per click">Avg CPC</th>' +
                 '<th title="Conversions">Conversions</th>' +
                 '<th title="CVR(%)">CVR(%)</th>' +//8
                 '<th title="CPA (Cost per acquisition)">CPA (Cost per Conversion)</th>' +
                 '<th title="Average Position">Avg Pos</th>' +
                 '<th title="Cost">Cost</th>' +
                 '<th title="Impression Share (%)">Impression Share (%)</th>' +//6
                 '<th title="Impression Share Lost (%)">Impression Share Lost (%)</th>' +//7
                 '</tr></thead><tbody id="KeywrdPlnrErrBody">';
            if (data == '') {
               // htmlAcccontent3 += '<tr><td colspan="15" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var index = 0;
            $.each(data[0], function (i, item) {
                index = index + 1;
                htmlAcccontent3 += '<tr><td>' + index + '</td>' +
                    '<td>' + item.accName + '</td>' +
                    '<td>' + item.CampaignName + '</td>' +
                    '<td>' + item.AdGroupName + '</td>' +
                    '<td>' + item.KeyName + '</td>';


                htmlAcccontent3 += '<td>' + (item.NewImpression).toFixed(0) + '</td>' +
                    '<td>' + (item.NewClicks).toFixed(0) + '</td>' +
                    '<td>' + (item.CTR).toFixed(2) + '</td>' +
                    '<td>' + (item.RevNewCostPerClick).toFixed(2) + '</td>' +
                    '<td>' + item.NewConversions.toFixed(0) + '</td>' +
                    '<td>' + (item.CVR).toFixed(2) + '</td>' +
                    '<td>' + item.NewCostPerConversions.toFixed(2) + '</td>' +
                    '<td>' + item.AvgPos.toFixed(0) + '</td>' +
                    '<td>' + (item.NewCost).toFixed(2) + '</td>' +
                    '<td>' + (item.ImpressionShare).toFixed(2) + '</td>' +
                    '<td>' + (item.ImpressionLost).toFixed(2) + '</td>';

                RevNewImps += parseFloat(checkint(item.NewImpression).toFixed(0));
                Imps += parseFloat(checkint(item.Impressions).toFixed(0));
                Clicks += parseFloat(checkint(item.NewClicks));
                // CTR += parseFloat(checkint(item.CTR).toFixed(2));
                Cost += parseFloat(checkint(item.NewCost));
                CPC += parseFloat(checkint(item.RevNewCostPerClick).toFixed(2));
                cnver += parseFloat(checkint(item.NewConversions).toFixed(0));
                CostCpc += parseFloat(checkint(item.NewCostPerConversions).toFixed(2));

                //'<td></td>' +
                ////'<td><a href="#" class="popup-btn" data-toggle="modal" data-target="#myModalEdit"  onclick="getEditCamp(\'' + item.CampaignCode + '\');"><img src="/Content/images/plus-icon.png" alt="icon" />Edit</a></td></tr>	';
                //'<td><a href="#"  data-toggle="modal" data-target="#myModal"  onclick="getEditCamp(\'' + item.BatchCode + '\');"><img src="/Content/images/student/edit-icon.png" alt="delete" /></a></td></tr>	';

            });

            if (data != '') {
                var TotalCTR = ((parseFloat(Clicks.toFixed(0)) / parseFloat(RevNewImps.toFixed(0))) * 100).toFixed(2).replace("NaN", "0.00");
                var ImpressionShare = (RevNewImps.toFixed(0) / Imps.toFixed(0)).toFixed(2).replace("NaN", "0.00");
                var ImpressionLost = (parseFloat(ImpressionShare) == 0) ? "0.00" : (100 - parseFloat(ImpressionShare)).toFixed(2);
                //var ImpressionLost = (100 - parseFloat(ImpressionShare)).toFixed(2);
                var CVR = (parseFloat(cnver.toFixed(0)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");;
                var AvgCPC = (parseFloat(Cost.toFixed(2)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");

                htmlAcccontent3 += '<tfoot id="KeywordFooter"><tr><td><b>Total</b></td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '<td>' + RevNewImps.toFixed(0) + '</td>' +
                               '<td>' + Clicks.toFixed(0) + '</td>' +
                               '<td>' + TotalCTR + '</td>' + //7
                               '<td></td>' + //8
                               '<td>' + (parseFloat(cnver)).toFixed(0) + '</td>' +
                               '<td></td>' +
                               '<td>' + ((Cost).toFixed(2) / cnver.toFixed(0)).toFixed(2).replace("NaN", "0.00") + '</td>' +
                               '<td></td>' +
                               '<td>' + (parseFloat(Cost)).toFixed(2) + '</td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '</tr>';

                htmlAcccontent3 += '<tr><td><b>Avg</b></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +//6
                                   '<td></td>' +//7
                                   '<td>' + AvgCPC + '</td>' +//8
                                   '<td></td>' +
                                   '<td>' + CVR + '</td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td>' + ImpressionShare + '</td>' +
                                   '<td>' + ImpressionLost + '</td>' +
                                   '<td></td>' +
        '</tr></tfoot>';
            }

            htmlAcccontent3 += '</tbody></table></div></div></div>';
            $('#div_resultAccKeywrdRpt').html(htmlAcccontent3);
            ApplyDataTable("tbl_resultAccKeywrdRpt", "0");
            // IndustryTopSearchDataTable();
        }, async: true,
        error: OnError
    });
}


function GetIndustryReportTopSearch() {
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val()
    });
    $('#div_rptkey').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Report/GetIndustryReportTopSearch",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            //$(".Loader").hide();
        },
        success: function (data) {
            htmlAcccontent = '<div class="row">' +
                        '<div class="col-md-12">' +
                         '<div class="campaings-table-border table-responsive">' +
                         '<table id="tbl_resultkeyRpt"><thead>' +
                         '<tr>' +
                         '<th>S.No</th>' +
                         '<th>Account</th>' +
                         '<th>Keyword</th>' +
                         '<th>Impression Share (%)</th>' +
                         '<th>Cost</th>' +
                         '</tr></thead><tbody>';
            if (data == '') {
                htmlAcccontent += '<tr><td colspan="5" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }

            $.each(data[0], function (i, item) {
                htmlAcccontent += '<tr><td>' + (parseInt(i) + 1) + '</td>' +
                    '<td>' + item.AccName + '</td>' +
                                '<td>' + item.KeyName + '</td>' +
                                 '<td>' + item.ImpressionShare.toFixed(2) + '</td>' +
                                  '<td>' + item.Cost + '</td>' +
                                 '</tr>';


            });
            htmlAcccontent += '</tbody></table></div></div></div>';
            $('#div_rptkey').html(htmlAcccontent);
            //$(".Loader").hide();
            // IndustryTopSearchDataTable();
        }, async: true,
        error: OnError
    });
}
function GetIndustryReportTopThemes() {
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val()
    });
    $('#div_rptthemes').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Report/GetIndustryReportTopThemes",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
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
                         '<table id="tbl_resultRpt_themes"><thead>' +
                         '<tr>' +
                         '<th>S.No</th>' +
                         '<th>Account</th>' +
                         '<th>Campaign</th>' +
                         '<th>Ad Group</th>' +
                         '<th>Impression Share (%)</th>' +
                         '<th>Cost</th>' +
                         '</tr></thead><tbody>';
            if (data == '') {
                htmlcontent += '<tr><td colspan="6" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }

            $.each(data[0], function (i, item) {
                htmlcontent += '<tr><td>' + (parseInt(i) + 1) + '</td>' +
                    '<td>' + item.AccName + '</td>' +
                    '<td>' + item.CampaignName + '</td>' +
                                '<td>' + item.AdGroupName + '</td>' +
                                 '<td>' + item.ImpressionShare.toFixed(2) + '</td>' +
                                  '<td>' + item.Cost + '</td>' +
                                 '</tr>';


            });
            htmlcontent += '</tbody></table></div></div></div>';
            $('#div_rptthemes').html(htmlcontent);
            // IndustryTopSearchDataTable();
            $(".Loader").hide();
        }, async: true,
        error: OnError
    });
}
function GetIndustryReportCampaignMatrix()
{
    debugger;
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val()
    });
    $('#div_IndCampRpt').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Report/GetIndustryReportCampaignMatrix",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            //$(".Loader").hide();
        },
        success: function (data) {
            var Budget = 0;
            var Imps = 0;
            var RevNewImps = 0;
            var Clicks = 0;
            var CTR = 0;
            var Cost = 0;
            var CPC = 0;
            var cnver = 0;
            var CostCpc = 0;
            var CountCamp = 0;

            htmlcontent = '<div class="row">' +
                '<div class="col-md-12">' + 
                 '<div class="campaings-table-border table-responsive">' +
                 '<table id="tbl_resultIndCampRpt">' +
                 '<thead id="campaignHead"><tr>' +
                 '<th>Sno.</th>' +
                 '<th>Account Name</th>' +
                 '<th>Campaign Name</th>' +
                 '<th>Budget</th>' +
                 '<th title="Impression">Impression</th>' +
                 '<th title="Clicks">Clicks</th>' +
                 '<th title="CTR (%)">CTR (%)</th>' +
                 '<th title="Average Cost per click">Avg CPC</th>' +
                 '<th title="Conversions">Conversions</th>' +
                 '<th title="CVR(%)">CVR(%)</th>' +//8
                 '<th title="CPA (Cost per acquisition)">CPA (Cost per Conversion)</th>' +
                 '<th title="Average Position">Avg Pos</th>' +
                 '<th title="Cost">Cost</th>' +
                 '<th title="Impression Share (%)">Impression Share (%)</th>' +//6
                 '<th title="Impression Share Lost (%)">Impression Share Lost (%)</th>' +//7
                 '</tr></thead><tbody id="CampaignBody">';

            if (data == '') {
               // htmlcontent += '<tr><td colspan="15" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var index = 0;
            $.each(data[0], function (i, item) {
                debugger;
                index = index + 1;
                htmlcontent += '<tr><td>' + index + '</td> ' +
                               '<td>' + item.accName + '</td> ' +
                               '<td>' + item.CampaignName + '</td>' +
                               '<td>' + item.Budget.toFixed(2) + '</td>'+
                               '<td>' + (item.NewImpression).toFixed(0) + '</td>' +
                               '<td>' + (item.NewClicks).toFixed(0) + '</td>' +
                               '<td>' + (item.CTR).toFixed(2) + '</td>' +
                               '<td>' + (item.RevNewCostPerClick).toFixed(2) + '</td>' +
                               '<td>' + item.NewConversions.toFixed(0) + '</td>' +
                               '<td>' + (item.CVR).toFixed(2) + '</td>' +
                               '<td>' + item.NewCostPerConversions.toFixed(2) + '</td>' +
                               '<td>' + item.AvgPos.toFixed(0) + '</td>' +
                               '<td>' + (item.NewCost).toFixed(2) + '</td>' +
                               '<td>' + (item.ImpressionShare).toFixed(2) + '</td>' +
                               '<td>' + (item.ImpressionLost).toFixed(2) + '</td>';
                RevNewImps += parseFloat(checkint(item.NewImpression).toFixed(0));
                Imps += parseFloat(checkint(item.Impressions).toFixed(0));
                Clicks += parseFloat(checkint(item.NewClicks).toFixed(0));
                // CTR += parseFloat(checkint(item.CTR).toFixed(2));
                Cost += parseFloat(checkint(item.NewCost).toFixed(2));
                CPC += parseFloat(checkint(item.RevNewCostPerClick).toFixed(2));
                cnver += parseFloat(checkint(item.NewConversions).toFixed(0));
                CostCpc += parseFloat(checkint(item.NewCostPerConversions).toFixed(2));
                Budget += parseFloat(checkint(item.Budget));

            });

            if (data != '') {
                debugger;
                var TotalCTR = ((parseFloat(Clicks.toFixed(0)) / parseFloat(RevNewImps.toFixed(0))) * 100).toFixed(2).replace("NaN", "0.00");
                var ImpressionShare = (RevNewImps.toFixed(0) / Imps.toFixed(0)).toFixed(2).replace("NaN", "0.00");
                var ImpressionLost = (parseFloat(ImpressionShare) == 0) ? "0.00" : (100 - parseFloat(ImpressionShare)).toFixed(2);
                //var ImpressionLost = (100 - parseFloat(ImpressionShare)).toFixed(2);
                var CVR = (parseFloat(cnver.toFixed(0)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");;
                var AvgCPC = (parseFloat(Cost.toFixed(2)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");


                htmlcontent += '<tfoot id="KeywordFooter"><tr><td><b>Total</b></td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '<td>' + Budget.toFixed(2) + '</td>' +
                               '<td>' + RevNewImps.toFixed(0) + '</td>' +
                               '<td>' + Clicks.toFixed(0) + '</td>' +
                               '<td>' + TotalCTR + '</td>' + //7
                               '<td></td>' + //8
                               '<td>' + (parseFloat(cnver)).toFixed(0) + '</td>' +
                               '<td></td>' +
                               '<td>' + ((Cost).toFixed(2) / cnver.toFixed(0)).toFixed(2).replace("NaN", "0.00") + '</td>' +
                               '<td></td>' +
                               '<td>' + (parseFloat(Cost)).toFixed(2) + '</td>' +
                               '<td></td>' +
                               '<td></td>' +
                               '</tr>';

                htmlcontent += '<tr><td><b>Avg</b></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +//6
                                   '<td></td>' +//7
                                   '<td>' + AvgCPC + '</td>' +//8
                                   '<td></td>' +
                                   '<td>' + CVR + '</td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td></td>' +
                                   '<td>' + ImpressionShare + '</td>' +
                                   '<td>' + ImpressionLost + '</td>' +
        '</tr></tfoot>';
            }
            htmlcontent += '</tbody></table></div></div></div>';
            $('#div_IndCampRpt').html(htmlcontent);
            ApplyDataTable("tbl_resultIndCampRpt", "0");
            $(".Loader").hide();
        }, async: true,
        error: OnError
    });
}
function GetIndustryReportTopAccount() {

    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val()
    });
    $('#div_IndAccRpt').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Report/GetIndustryReportTopAccount",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            //$(".Loader").hide();
        },
        success: function (data) {
            htmlAcccontent = '<div class="row">' +
                    '<div class="col-md-12">' +
                     '<div class="campaings-table-border table-responsive">' +
                     '<table id="tbl_resultIndAccRpt">' +
                     '<tr>' +
                     '<th>Sno.</th>' +
                     '<th>Account</th>' +
                     '<th>Impression Share(%)</th>' +
                     '<th>Cost</th>' +
                     '<th title="Conversions">Conv</th>' +
                     '<th title="Cost/Conversion">Cost/Conv</th>' +
                     '</tr>';
            if (data == '') {
                htmlAcccontent += '<tr><td colspan="4" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var index = 0;
            $.each(data[0], function (i, item) {
                index = index + 1;
                htmlAcccontent += '<tr><td>' + index + '</td>' +
                    '<td>' + item.AccName + '</td>' +
                                    '<td>' + item.ImpressionShare.toFixed(2) + '</td>' +
                                    '<td>' + item.Cost + '</td>' +
                      '<td>' + item.NewConversions.toFixed(0) + '</td>' +
                                '<td>' + item.NewCostPerConversions.toFixed(2) + '</td></tr>';

            });
            htmlAcccontent += '</table></div></div></div>';
            $('#div_IndAccRpt').html(htmlAcccontent);
            //BatchDataTable();
            $(".Loader").hide();
        }, async: true,
        error: OnError
    });
}
function GetIndustryReportRfaComparsnByAcc() {
    $("#TopRFAGridDownload").attr("href", "/Trainers/Report/GetIndustryReportRfaComparsnByAccExcel/" + $('#ddlBatchReport').val());
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val()
    });
    $('#div_IndRfaRpt').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Report/GetIndustryReportRfaComparsnByAcc",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            //$(".Loader").hide();
        },
        success: function (data) {
            htmlAcccontent = '<div class="row">' +
                    '<div class="col-md-12">' +
                     '<div class="campaings-table-border table-responsive">' +
                     '<table id="tbl_resultIndAccRpt">' +
                     '<tr>' +
                     '<th>Account</th>' +
                     '<th title="RFA (dd mmm yyyy hh:mm)">RFA</th>' +
                     '<th>Impressions</th>' +
                     '<th>Clicks</th>' +
                     '<th>Conversions</th>' +
                     '<th>Cost</th>' +
                     '<th>Cost/Conversion</th>' +
                     '<th>Cost/Click</th>' +
                     '<th>Impression Share (%)</th>' +
                     '</tr>';
            if (data == '') {
                htmlAcccontent += '<tr><td colspan="10" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var oldAccCode = '';
            var rowsNo = 0;
            var rowsName = 0;
            var rowSNo = 0;
            $.each(data[0], function (i, item) {
                debugger;

                rowsName = rowsName + 1;
                htmlAcccontent += '<tr>';
                if (oldAccCode != item.AccCode) {
                    rowSNo = 0;
                    oldAccCode = item.AccCode;
                    rowsName = 1;
                    AccChild = $.grep(data[0], function (element, index) {
                        debugger;
                        return element.AccCode == item.AccCode;
                    });
                    rowsNo = 0;
                    $.each(AccChild, function (k, childItem) {
                        rowsNo = rowsNo + 1;
                    });

                    htmlAcccontent += '<td rowspan="' + rowsNo + '"><a id="' + item.AccCode + '"><div class="logo-img-scroll col-left"><img src="' + item.ImagePath + '" alt="amazone" /></div> <div class="clearfix"></div></a></td>';
                    $.each(data[0], function (i, item) {
                        debugger
                        if (item.AccCode == oldAccCode) {
                            rowSNo = rowSNo + 1;
                        }
                    });
                }

                htmlAcccontent += '<td>' + item.RunOn + '</td>' +
                    '<td>' + item.RevNewImpression + '</td>' +
                                    '<td>' + item.RevNewClicks + '</td>' +
                                    '<td>' + item.RevNewConversions + '</td>' +
                                    '<td>' + item.RevActualCost + '</td>' +
                                    '<td>' + item.RevNewCostPerConversions.toFixed(2) + '</td>' +
                                    '<td>' + item.RevNewCostPerClick.toFixed(2) + '</td>' +
                                    '<td>' + item.ImpressionShare.toFixed(2) + '</td>';
                rowSNo = rowSNo - 1;

            });
            htmlAcccontent += '</table></div></div></div>';
            $('#div_IndRfaRpt').html(htmlAcccontent);
            //BatchDataTable();
            $(".Loader").hide();
        }, async: true,
        error: OnError
    });
}
function GetIndustryReportRfaComparsnByRfa() {
    $("#TopRFAGridDownload").attr("href", "/Trainers/Report/GetIndustryReportRfaComparsnByRFAExcel/" + $('#ddlBatchReport').val());
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val()
    });
    $('#div_IndRfaRpt').empty();
    $.ajax({
        //global: false,
        url: "/Trainers/Report/GetIndustryReportRfaComparsnByRfa",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            // $(".Loader").hide();
        },
        success: function (data) {
            debugger;
            htmlAcccontent = '<div class="row">' +
                    '<div class="col-md-12">' +
                     '<div class="campaings-table-border table-responsive">' +
                     '<table id="tbl_resultIndAccRpt">' +
                     '<tr>' +
                     '<th title="RFA (dd mmm yyyy hh:mm)">RFA</th>' +
                     '<th>Account</th>' +
                     '<th>Impressions</th>' +
                     '<th>Clicks</th>' +
                     '<th>Conversions</th>' +
                     '<th>Cost</th>' +
                     '<th>Cost/Conversion</th>' +
                     '<th>Cost/Click</th>' +
                     '<th>Impression Share (%)</th>' +
                     '</tr>';
            if (data == '') {
                htmlAcccontent += '<tr><td colspan="10" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var oldRfaId = '';
            var oldRfaIdTemp = '';
            var rowsNo = 0;
            var rowsName = 0;
            var rowSNo = 0;
            $.each(data[0], function (i, item) {
                debugger
                if (oldRfaIdTemp != item.RFA_Id) {
                    oldRfaIdTemp = item.RFA_Id;
                    rowSNo = rowSNo + 1;
                }
            });
            $.each(data[0], function (i, item) {
                debugger;
                rowsName = rowsName + 1;
                htmlAcccontent += '<tr>';
                if (oldRfaId != item.RFA_Id) {
                    oldRfaId = item.RFA_Id;
                    rowsName = 1;
                    AccChild = $.grep(data[0], function (element, index) {
                        debugger;
                        return element.RFA_Id == item.RFA_Id;
                    });
                    rowsNo = 0;
                    $.each(AccChild, function (k, childItem) {
                        debugger;
                        rowsNo = rowsNo + 1;
                    });
                    htmlAcccontent += '<td rowspan="' + rowsNo + '"><a id="' + item.RFA_Id + '">' + "" + item.RunOn + '</a></td>';
                    rowSNo = rowSNo - 1;
                }
                htmlAcccontent += '<td>' + item.AccName + '</td>' +
                    '<td>' + item.RevNewImpression + '</td>' +
                                    '<td>' + item.RevNewClicks + '</td>' +
                                    '<td>' + item.RevNewConversions + '</td>' +
                                    '<td>' + item.RevActualCost + '</td>' +
                                    '<td>' + item.RevNewCostPerConversions.toFixed(2) + '</td>' +
                                    '<td>' + item.RevNewCostPerClick.toFixed(2) + '</td>' +
                                    '<td>' + item.ImpressionShare.toFixed(2) + '</td>';
            });
            htmlAcccontent += '</table></div></div></div>';
            $('#div_IndRfaRpt').html(htmlAcccontent);
            //BatchDataTable();
            $(".Loader").hide();
        }, async: true,
        error: OnError
    });
}
function GetIndustryReportRfaComparsnByGraph() {
    if ($("select[id='ddlAccount'] option:selected").index() == 0) {
        alert('Please select account');
        return false;
    }
    else {
        var htmlcontent = "";
        var SelectedData = JSON.stringify({
            'BatchCode': $('#ddlBatchReport').val(),
            'AccCode': $('#ddlAccount').val(),
            'RFAID': $('#ddlRfaList').val()
        });
        $('#div_IndRfa_graph').hide();
        $('#div_IndRfa_graph_msg').text("");
        $.ajax({
            //global: false,
            url: "/Trainers/Report/GetIndustryReportRfaComparsnByGraph",
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
                if (data != "") {
                    $('#div_IndRfa_graph').show();
                    var rowNum = 0;
                    var dataPoints = [];
                    var dataPoints1 = [];
                    var dataPoints2 = [];
                    var dataPoints3 = [];
                    var dataPoints4 = [];
                    var dataPoints5 = [];
                    var dataPoints6 = [];
                    var dataPoints7 = [];

                    $.each(data, function (i, item) {
                        if ($("select[id='ddlRfaList'] option:selected").index() == 0) {
                            rowNum = rowNum + 1;
                            columnName = "RFA " + rowNum;
                        }
                        else {
                            columnName = "RFA " + $("select[id='ddlRfaList'] option:selected").index();
                        }
                        // viewData.ClicksData.push({});
                        //columnName = "RFA " + rowNum;



                        //  viewData.ClicksData[rowNum][columnName] = item.NewClicks;
                        dataPoints.push({ label: columnName, y: parseInt(item.RevNewCostPerConversions) });
                        dataPoints1.push({ label: columnName, y: parseInt(item.RevNewCostPerClick) });
                        dataPoints2.push({ label: columnName, y: parseInt(item.RevNewImpression) });
                        dataPoints3.push({ label: columnName, y: parseInt(item.ImpressionShare) });
                        dataPoints4.push({ label: columnName, y: parseInt(item.RevNewClicks) });
                        dataPoints5.push({ label: columnName, y: parseInt(item.RevNewConversions) });
                        dataPoints6.push({ label: columnName, y: parseFloat(item.CTR.toFixed(2)) });
                        dataPoints7.push({ label: columnName, y: parseFloat(item.CVR.toFixed(2)) });
                    });
                    var chart = new CanvasJS.Chart("chartContainer1",
                        {
                            title: {
                                text: " Cost Per Conversion(INR)"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            //dataPointMaxWidth: 40,
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",

                                    dataPoints: dataPoints
                                }
                            ]
                        });

                    chart.render();

                    var chart = new CanvasJS.Chart("chartContainer2",
                        {
                            title: {
                                text: " Cost Per Click(INR)"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints1
                                }
                            ]
                        });
                    debugger;
                    chart.render();


                    var chart = new CanvasJS.Chart("chartContainer3",
                        {
                            title: {
                                text: "Impressions"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints2
                                }
                            ]
                        });

                    chart.render();


                    var chart = new CanvasJS.Chart("chartContainer4",
                        {
                            title: {
                                text: "Impression Share"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints3
                                }
                            ]
                        });

                    chart.render();


                    var chart = new CanvasJS.Chart("chartContainer5",
                        {
                            title: {
                                text: "Clicks"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints4
                                }
                            ]
                        });

                    chart.render();


                    var chart = new CanvasJS.Chart("chartContainer6",
                        {
                            title: {
                                text: "Conversion"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints5
                                }
                            ]
                        });

                    chart.render();

                    var chart = new CanvasJS.Chart("chartContainer7",
                        {
                            title: {
                                text: "CTR"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints6
                                }
                            ]
                        });

                    chart.render();

                    var chart = new CanvasJS.Chart("chartContainer8",
                        {
                            title: {
                                text: "CVR"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints7
                                }
                            ]
                        });

                    chart.render();
                }
                else {
                    $('#div_IndRfa_graph_msg').text("Select account to display graphs");
                }
            }, async: true,
            error: OnError
        });
    }
}
function GetIndustryReportRfaHistoriesByGraph() {
    if ($("select[id='ddlAccount'] option:selected").index() == 0) {
        alert('Please select account');
        return false;
    }
    if ($("select[id='ddlRfaList'] option:selected").index() == 0) {
        alert('Please select RFA');
        return false;
    }
    else {
        var htmlcontent = "";
        var SelectedData = JSON.stringify({
            'BatchCode': $('#ddlBatchReport').val(),
            'AccCode': $('#ddlAccount').val(),
            'RFAID': $('#ddlRfaList').val()
        });
        $('#div_IndRfa_graph').hide();
        $('#div_IndRfa_graph_msg').text("");
        $.ajax({
            //global: false,
            url: "/Trainers/Report/GetIndustryReportRfaHistoriesByGraph",
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
                if (data != "") {
                    $('#div_IndRfa_graph').show();
                    var rowNum = 0;
                    var dataPoints = [];
                    var dataPoints1 = [];
                    var dataPoints2 = [];
                    var dataPoints3 = [];
                    var dataPoints4 = [];
                    var dataPoints5 = [];
                    var dataPoints6 = [];
                    var dataPoints7 = [];

                    $.each(data, function (i, item) {
                        debugger;
                        if (i == 0) {
                            rowNum = rowNum + 1;
                            columnName = $("#ddlAccount option:selected").text();
                        }
                        if (i == 1) {
                            columnName = "Avg";
                        }
                        if (i == 2) {
                            columnName = item.AccName;
                        }
                        // viewData.ClicksData.push({});
                        //columnName = "RFA " + rowNum;



                        //  viewData.ClicksData[rowNum][columnName] = item.NewClicks;
                        dataPoints.push({ label: columnName, y: parseInt(item.RevNewCostPerConversions) });
                        dataPoints1.push({ label: columnName, y: parseInt(item.RevNewCostPerClick) });
                        dataPoints2.push({ label: columnName, y: parseInt(item.RevNewImpression) });
                        dataPoints3.push({ label: columnName, y: parseInt(item.ImpressionShare) });
                        dataPoints4.push({ label: columnName, y: parseInt(item.RevNewClicks) });
                        dataPoints5.push({ label: columnName, y: parseInt(item.RevNewConversions) });
                        dataPoints6.push({ label: columnName, y: parseFloat(item.CTR.toFixed(2)) });
                        dataPoints7.push({ label: columnName, y: parseFloat(item.CVR.toFixed(2)) });
                    });
                    var chart = new CanvasJS.Chart("chartContainer1",
                        {
                            title: {
                                text: " Cost Per Conversion(INR)"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            //dataPointMaxWidth: 40,
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",

                                    dataPoints: dataPoints
                                }
                            ]
                        });

                    chart.render();

                    var chart = new CanvasJS.Chart("chartContainer2",
                        {
                            title: {
                                text: " Cost Per Click(INR)"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints1
                                }
                            ]
                        });
                    debugger;
                    chart.render();


                    var chart = new CanvasJS.Chart("chartContainer3",
                        {
                            title: {
                                text: "Impressions"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints2
                                }
                            ]
                        });

                    chart.render();


                    var chart = new CanvasJS.Chart("chartContainer4",
                        {
                            title: {
                                text: "Impression Share"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints3
                                }
                            ]
                        });

                    chart.render();


                    var chart = new CanvasJS.Chart("chartContainer5",
                        {
                            title: {
                                text: "Clicks"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints4
                                }
                            ]
                        });

                    chart.render();


                    var chart = new CanvasJS.Chart("chartContainer6",
                        {
                            title: {
                                text: "Conversion"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints5
                                }
                            ]
                        });

                    chart.render();

                    var chart = new CanvasJS.Chart("chartContainer7",
                        {
                            title: {
                                text: "CTR"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints6
                                }
                            ]
                        });

                    chart.render();

                    var chart = new CanvasJS.Chart("chartContainer8",
                        {
                            title: {
                                text: "CVR"
                            },
                            animationEnabled: true,
                            axisX: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            axisY: {
                                labelFontSize: 16,
                                gridThickness: 0
                            },
                            data: [
                                {
                                    indexLabelFontSize: 15,
                                    indexLabelFontColor: "white",
                                    type: "column",
                                    indexLabel: "{y}",
                                    indexLabelPlacement: "inside",
                                    indexLabelOrientation: "horizontal",
                                    dataPoints: dataPoints7
                                }
                            ]
                        });

                    chart.render();
                }
                else {
                    $('#div_IndRfa_graph_msg').text("Select account to display graphs");
                }
            }, async: true,
            error: OnError
        });
    }
}
function GetRfaAccountDropdown() {
    var BatchCode = $('#ddlBatchReport').val();
    $('#ddlAccount').empty();
    $('#ddlAccount').append($('<option selected></option>').val("").html("Choose Account"));
    $.ajax({
        url: "/Trainers/Report/GetAccountDropdown?BatchCode=" + BatchCode + "",
        method: 'GET',
        cache: false,
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            // $(".Loader").hide();
        },
        success: function (data) {
            //Binding Dropdown list
            $.each(data[0], function (i, item) {
                if (i == 0)
                    $('#ddlAccount').append($('<option></option>').val(item.AccCode).html(item.AccName));
                else {
                    $('#ddlAccount').append($('<option></option>').val(item.AccCode).html(item.AccName));
                }
            });
            $(".Loader").hide();
            //GetRfaListDropdown();
        }, async: true,
        error: OnError

    });
}
function GetRfaListDropdown() {
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val(),
        'AccCode': $('#ddlAccount').val()
    });
    $('#ddlRfaList').empty();
    $('#ddlRfaList').append($('<option selected></option>').val("").html("Choose RFA"));
    $.ajax({
        url: "/Trainers/Report/GetIndustryReportRfaComparsnByGraph",
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
            //Binding Dropdown list
            debugger;
            $.each(data, function (i, item) {
                debugger;
                if (i == 0)
                    $('#ddlRfaList').append($('<option></option>').val(item.RFA_Id).html("RFA " + (i + 1)));
                else {
                    $('#ddlRfaList').append($('<option></option>').val(item.RFA_Id).html("RFA " + (i + 1)));
                }
            });
            $(".Loader").hide();
        }, async: true,
        error: OnError
    });
}
//End Trainer Block

function getgroup(cCode) {

    location.href = "/Students/Home/Index#adgroups#" + cCode + "";
    location.reload();
    return true;
}

function getAllCamp() {

    var htmlcontent = "";
    $('#ddlCampaign').empty();
    $('#ddlCampaign2').empty();
    $('#ddlCampaignADGM').empty();
    $('#ddlCampaignAD1').empty();
    $('#ddlCampaignAD').empty();


    $('#ddlCampaign').append($('<option></option>').val("ALL").html("ALL Campaigns"));
    $('#ddlCampaign2').append($('<option></option>').val("ALL").html("ALL Campaigns"));
    $('#ddlCampaignADGM').append($('<option></option>').val("ALL").html("ALL Campaigns"));
    $('#ddlCampaignAD1').append($('<option></option>').val("ALL").html("Select Campaign"));
    $('#ddlCampaignAD').append($('<option></option>').val("ALL").html("ALL Campaigns"));
    $.ajax({
        url: "/Students/Home/GetCampaigns",
        method: 'GET',

        //data: { cCode: '1' },
        success: function (data) {




            $.each(data, function (i, item) {

                $('#ddlCampaign').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
                $('#ddlCampaign2').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
                $('#ddlCampaignADGM').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
                $('#ddlCampaignAD1').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
                $('#ddlCampaignAD').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));


            });
        }, async: false,
        error: OnError
    });



}
function getAdGroups(cCode) {

    var htmlcontent = "";
    var htmlcontentRow = "";
    $('#ADGroup_Result').empty();

    $.ajax({
        url: "/Students/Home/GetADGroupsData?cCode=" + cCode + "",
        method: 'GET',

        //data: { cCode: '1' },
        success: function (data) {

            htmlcontent = '<div class="row">' +
             '<div class="col-md-12">' +

               '<div class="campaings-table">' +

                   '<div class="panel-group" id="accordion">';


            var tempCampaign = '';
            var tempADGroup = '';
            var colapsCtr = 0;
            $.each(data, function (i, item) {
                if (tempCampaign != item.CampaignName && colapsCtr != 0) {
                    htmlcontent += '</table></div></div></div>';
                    htmlcontent += '</div>';
                    // tempCampaign = item.CampaignName;
                    htmlcontentRow = '';
                }
                if (tempCampaign != item.CampaignName) {
                    tempCampaign = item.CampaignName;
                    colapsCtr++;
                    htmlcontent += '<div class="panel panel-default">';
                    if (colapsCtr == 1) {
                        htmlcontent += '<div class="panel-heading active-panel">';
                    }
                    else {
                        htmlcontent += '<div class="panel-heading">';
                    }
                    htmlcontent += '<h4 class="panel-title">' +
                                   '<a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapse' + colapsCtr + '">' +
                                   '' + item.CampaignName + '' +
                                   '<i class="indicator glyphicon glyphicon-chevron-down  pull-right"></i>  <span class="pull-right">Netword Type: ' + item.NetWorkType + ', Location: ' + item.Location + ', Budget: $ ' + item.Budget + '</span> </a> ' +
                                   '</h4> </div>';
                    //}
                    // alert(tempADGroup + ' # ' + item.AdGroupName)
                    //if (tempADGroup != item.AdGroupName) {
                    if (colapsCtr == 1) {
                        htmlcontent += '<div id="collapse' + colapsCtr + '" class="panel-collapse collapse in">';
                    }
                    else {
                        htmlcontent += '<div id="collapse' + colapsCtr + '" class="panel-collapse collapse">';
                    }
                    htmlcontent += '<div class="panel-body">' +
                                   '<div class="campaings-table table-responsive">' +

                               '<table>' +

                                   '<tr>' +

                                       '<th>Ad Groups</th>' +
                                       '<th>Status</th>' +
                                       '<th>Impressions</th>' +
                                       '<th>Clicks</th>' +
                                       '<th>CTR</th>' +
                                       '<th>Cost</th>' +
                                       '<th>Avg CPC</th>' +
                                       '<th>Conversions</th>' +
                                       '<th>Cost Per Conversion</th>' +

                                   '</tr>';
                }
                //alert(item.AdGroupName)
                htmlcontent += '<tr>' +

      				'<td><a href="#" >' + item.AdGroupName + '</a></td>' +
      				'<td></td>' +
      				'<td></td>' +
      				'<td></td>' +
      				'<td></td>' +
      				'<td></td>' +
      				'<td></td>' +
      				'<td></td>' +
      				'<td></td>' +


      			'</tr>';




                //if (tempADGroup != item.AdGroupName) {
                //  htmlcontent += '</table></div></div></div>';
                //   tempADGroup = item.AdGroupName;
                //}




                //if (tempCampaign != item.CampaignName ) {
                //    htmlcontent += '</table></div></div></div>';
                //    htmlcontent += '</div>';
                //    tempCampaign = item.CampaignName;
                //    htmlcontentRow = '';
                //}


            });
        }, async: false,
        error: OnError
    });
    htmlcontent += '</div></div></div></div>';

    $('#ADGroup_Result').html(htmlcontent);

}
function getAds(cCode, adGroup) {
    //alert(adGroup);
    var htmlcontent = "";
    var htmlcontentRow = "";
    var commInfo = "";
    $('#ADs_Result').empty();
    $('.ad-group-text').empty();

    $.ajax({
        url: "/Students/Home/GetADsData?cCode=" + cCode + "",
        method: 'GET',

        //data: { cCode: '1' },
        success: function (data) {

            htmlcontent = '<div class="row">' +
             '<div class="col-md-12">' +

               //'<div class="campaings-table">' +

                   '<div class="panel-group" id="accordion1">';


            var tempCampaign = '';
            var tempADGroup = '';
            var colapsCtr = 0;


            AdGroupsResult = $.grep(data, function (element, index) {

                return element.Parent == null && element.AdGroupCode == adGroup.replace('ALL', element.AdGroupCode);
                // return element.CampaignCode == 'CC0001'; //null;


            });

            $.each(AdGroupsResult, function (i, item) {


                colapsCtr = i;
                htmlcontent += '<div class="panel panel-default">';
                if (colapsCtr == 0) {
                    commInfo = item.CampaignName + ' <span class="pull-right">Netword Type: ' + item.NetWorkType + ', Location: ' + item.Location + ', Budget: $ ' + item.Budget + '</span>'
                    $('.ad-group-text').html(commInfo);
                    htmlcontent += '<div class="panel-heading active-panel">';
                }
                else {
                    htmlcontent += '<div class="panel-heading">';
                }
                htmlcontent += '<h4 class="panel-title">' +
                               '<a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#collapseAds' + colapsCtr + '">' +
                               '' + item.AdGroupName + '' +
                               '<i class="indicator glyphicon glyphicon-chevron-down  pull-right"></i>  </a> ' +
                               '</h4> </div>';

                if (colapsCtr == 0) {
                    htmlcontent += '<div id="collapseAds' + colapsCtr + '" class="panel-collapse collapse in">';
                }
                else {
                    htmlcontent += '<div id="collapseAds' + colapsCtr + '" class="panel-collapse collapse">';
                }

                htmlcontent += '<div class="panel-body">';


                htmlcontent += '<div id="myCarousel' + i + '" class="carousel slide" data-ride="carousel">';
                htmlcontent += '<div class="carousel-inner" role="listbox">';


                AdGroupsResultChild = $.grep(data, function (element, index) {

                    return element.Parent == item.AdGroupCode;

                });


                var l = 0;
                $.each(AdGroupsResultChild, function (k, childItem) {

                    // alert(childItem.Parent);
                    if (l == 0) {
                        htmlcontent += ' <div class="item active">' +
                            '<div class="item-slider">';
                    }
                    else if (l != 0 & l % 3 == 0) {


                        htmlcontent += '<div class="item">' +
                                        '<div class="item-slider">';
                    }



                    htmlcontent += ' <div class="item-list col-md-4">' +
                                                   '<a href=' + childItem.FinalUrl + '>' + childItem.ADText + '</a>' +
                                                  ' <span class="green-text">' + childItem.DispUrl + '</span>' +
                                                   '<span class="item-text">' + childItem.DescLine1 + '' + childItem.DescLine2 + '</span>' +
                                               '</div>';
                    l++;
                    if (l != 0 & l % 3 == 0) {
                        htmlcontent += '<div class="clearfix"></div>' +
                    '</div>' +
                '</div>';
                    }
                });

                htmlcontent += '</div>';
                htmlcontent += '<a class="left carousel-control" href="#myCarousel' + i + '" role="button" data-slide="prev">' +
                                        '<img src="/Content/images/arrow-left.png" class="arrow" aria-hidden="true">' +

                                        '<span class="sr-only">Previous</span>' +
                                    '</a>' +
                                    '<a class="right carousel-control" href="#myCarousel' + i + '" role="button" data-slide="next">' +
                                        '<img src="/Content/images/arrow-right.png" class="arrow" aria-hidden="true">' +
                                       ' <span class="sr-only">Next</span>' +
                                   ' </a>' +
                                '</div>';


                htmlcontent += '</div></div></div>';



            });

        }, async: false,
        error: OnError
    });
    //htmlcontent += '</div></div></div></div>';

    $('#ADs_Result').html(htmlcontent);

}
function getAdGroupsOnly(cCode) {

    $('#ddlADGroupName1').empty();
    $('#ddlADGroupName').empty();


    $('#ddlADGroupName1').append($('<option></option>').val("ALL").html("ALL Ad Groups"));
    $('#ddlADGroupName').append($('<option></option>').val("0").html("Select Ad Groups"));
    $.ajax({
        url: "/Students/Home/GetADGroupsDataDistinct?cCode=" + cCode + "",
        method: 'GET',
        success: function (data) {


            $.each(data, function (i, item) {

                $('#ddlADGroupName1').append($('<option></option>').val(item.AdGroupCode).html(item.AdGroupName))
                $('#ddlADGroupName').append($('<option></option>').val(item.AdGroupCode).html(item.AdGroupName))


            });
        }, async: false,
        error: OnError
    });


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
function isNumber(evt, element) {

    var charCode = (evt.which) ? evt.which : event.keyCode

    if (
        (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
        (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
        (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function checkint(val) {

    if ($.isNumeric(val)) {
        val = val;
    }
    else {
        val = 0.0;
    }
    // alert(val);
    return val;
}


function ApplyDataTable(id, disableSorting) {
    var t = $('#' + id).DataTable({
        "searching": false,
        responsive: true,
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "language": {
            "emptyTable": "No results found"
        },
        aoColumnDefs: [
                           { aTargets: [0], bSortable: false },
                           { aTargets: [parseInt(disableSorting.split(",")[0])], bSortable: false },
                           { aTargets: [parseInt(disableSorting.split(",")[1])], bSortable: false }
        ],
        //"order": [[1, 'asc']]
        "order": []
    });

    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}
function OnError(xhr, errorType, exception) {
    debugger;
    $(".Loader").hide();
    var responseText;
    $("#dialog").html("");
    try {
        //var newTitle = $(xhr.responseText).filter('title').text();
        //$("#dialog").append("<div>" + newTitle + "</div>");

        responseText = jQuery.parseJSON(xhr.responseText);
        $("#dialog").append("<div><b>" + errorType + " " + exception + "</b></div>");
        $("#dialog").append("<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>");
        $("#dialog").append("<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>");
        $("#dialog").append("<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>");

    } catch (e) {
        responseText = xhr.responseText;
        $("body").html(responseText);
    }
    $("#dialog").dialog({
        title: "jQuery Exception Details",
        width: 900,
        height: 600,
        buttons: {
            Close: function () {
                $(this).dialog('close');
            }
        }
    });

}