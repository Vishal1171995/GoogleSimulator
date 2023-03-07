$(document).ready(function () {
    var msg = "";
    $(function () {
        $('#txtADGroupHeadline').keyup(function () {
            debugger;
            var desc = $('#txtADGroupHeadline').val();
            var len = desc.length;
            if (desc.length >= 25) {
                this.value = this.value.substring(0, 25);
                $('#spntxtADGroupHeadline').text(0 + ' Characters Left');
            }
            else {
                $('#spntxtADGroupHeadline').text(25 - len + ' Characters Left');
            }
            $('#spntADGroupHeadlinePreview').text(this.value);
            $('#spntADGroupHeadlinePreview').css({ 'color': '#f47d35' });
        });
    });
    $(function () {
        $('#txtADGroupDesc1').keyup(function () {
            var desc = $('#txtADGroupDesc1').val();
            var len = desc.length;
            if (desc.length >= 35) {
                this.value = this.value.substring(0, 35);
                $('#spntxtADGroupDesc1').text(0 + ' Characters Left');
            }
            else {
                $('#spntxtADGroupDesc1').text(35 - len + ' Characters Left');
            }
            $('#spntADGroupDesc1Preview').text(this.value);
            $('#spntADGroupDesc1Preview').css({ 'color': '#46871f' });
        });
    });
    $(function () {
        $('#txtADGroupDesc2').keyup(function () {
            var desc = $('#txtADGroupDesc2').val();
            var len = desc.length;
            if (desc.length >= 35) {
                this.value = this.value.substring(0, 35);
                $('#spntxtADGroupDesc2').text(0 + ' Characters Left');
            }
            else {
                $('#spntxtADGroupDesc2').text(35 - len + ' Characters Left');
            }
            $('#spntADGroupDesc2Preview').text(this.value);
            $('#spntADGroupDesc2Preview').css({ 'color': '#2a2a2a' });
        });
    });

    $(function () {
        $('#txtADHeadline').keyup(function () {
            var desc = $('#txtADHeadline').val();
            var len = desc.length;
            if (desc.length >= 25) {
                this.value = this.value.substring(0, 25);
                $('#spntxtADHeadline').text(0 + ' Characters Left');
            }
            else {
                $('#spntxtADHeadline').text(25 - len + ' Characters Left');
            }
            $('#spntADHeadlinePreview').text(this.value);
            $('#spntADHeadlinePreview').css({ 'color': '#f47d35' });
        });
    });
    $(function () {
        $('#txtADDesc1').keyup(function () {
            var desc = $('#txtADDesc1').val();
            var len = desc.length;
            if (desc.length >= 35) {
                this.value = this.value.substring(0, 35);
                $('#spntxtADDesc1').text(0 + ' Characters Left');
            }
            else {
                $('#spntxtADDesc1').text(35 - len + ' Characters Left');
            }
            $('#spntADDesc1Preview').text(this.value);
            $('#spntADDesc1Preview').css({ 'color': '#46871f' });
        });
    });
    $(function () {
        $('#txtADDesc2').keyup(function () {
            var desc = $('#txtADDesc2').val();
            var len = desc.length;
            if (desc.length >= 35) {
                this.value = this.value.substring(0, 35);
                $('#spntxtADDesc2').text(0 + ' Characters Left');
            }
            else {
                $('#spntxtADDesc2').text(35 - len + ' Characters Left');
            }
            $('#spntADDesc2Preview').text(this.value);
            $('#spntADDesc2Preview').css({ 'color': '#2a2a2a' });
        });
    });



    $("#ddlCampaignAD").change(function () {
        if (this.value != "ALL") {
            getAdGroupsOnly(this.value, '#ddlADGroupName');
        }
        if (this.value == "ALL") {
            $("#ddlADGroupName").empty();
            $("#ddlADGroupName").append($('<option></option>').val("ALL").html("Select Ad Groups"));
        }
    });
    $("#ddlADGroupName").change(function () {
        $("#hidADGName").val($("#ddlADGroupName option:selected").text());
    });

    $("#ddlCampKey").change(function () {

        if (this.value != "") {
            getAdGroupsOnly(this.value, '#ddlAdgrpKey');
        }
    });

    getAllCamp(); //To bind all campaign dropdown
});
$(function () {
    var getData = function (request, response) {
        $.getJSON(
            "/Students/Home/GetKeywords?Keywordtxt=" + $('#txtAutoKey').val() + "",
            function (data) {
                response(data);
            });
    };
    var selectItem = function (event, ui) {
        $("#txtAutoKey").val(ui.item.value);
        return false;
    }
    $("#txtAutoKey").autocomplete({
        source: getData,
        select: selectItem,
        minLength: 1,
        change: function () {
            // $("#txtAutoKey").val("").css("display", 2);
            if ($("#txtAutoKey").val() != "") {
            }
        }

    });
});


function OldWasteCode() {
    //function GettbleData() {

    //    var result;
    //    var data = [];
    //      $('tr.data-Map-Key').each(function () {

    //    var roco = $('#tblKeywords tr').length;


    //    if (roco == "1") {
    //        $("#spanmsgtble").show();
    //        return false;
    //    }
    //    else {
    //        alert(roco);
    //        $("#spanmsgtble").hide();
    //        for (var i = 2 ; i <= roco ; i++) {
    //            var Key = $('tr.data-Map-Key').find('#hdnkey' + i).val();//Bind to the first name with class f-name01  
    //            var keyBid = $('tr.data-Map-Key').find('#txtBid' + i).val();//Bind to the last name with class l-name01  
    //            var emailId = $(this).find('.email01').val();//Bind to the emailId with class email01  
    //            alert(Key + ' '  + i);
    //            alert(keyBid);
    //            var alldata =
    //            {
    //                'KeyCode': Key, //FName as per Employee class name in .cs  
    //                'keyBid': keyBid, //LName as per Employee class name in .cs  
    //                'CampaignCode': $("#ddlCampKey option:selected").val(),
    //                'AdGroupCode': $("#ddlAdgrpKey option:selected").val(),//'EmailId': emailId //EmailId as per Employee class name in .cs   
    //            }

    //            alert(alldata);
    //            data.push(alldata);
    //             break;
    //        }
    //        });    
    //        console.log(data);//  
    //        return data;   

    //        var data1 = JSON.stringify(data);
    //        console.log(data);    
    //        $.ajax({
    //            url: "/Students/Home/MapKeywordsAD",
    //            type: 'POST',
    //            dataType: 'json',
    //            contentType: 'application/json; charset=utf-8',
    //            data: JSON.stringify({ 'empdata': data1 }),
    //            success: function (data) {
    //                if (data == true) {
    //                    alert('Success');
    //                    window.location.href = '/Students/Home#keywords';
    //                    $("#divkeywordmodel").hide();
    //                    getMappedkeyWordsOnDemand();
    //                }
    //                if (data == false) {
    //                    alert('Error');
    //                }
    //                $('#ModalBatchSuccess').show();
    //            }, async: false,
    //            error: function (data) {
    //            }
    //        });
    //        $("#tblKeywords").html('');
    //        result = true;

    //        return false;
    //    }
    //    $("#tblKeywords").find("tr:not(:first)").remove();


    //    getMappedkeyWordsOnDemand();
    //    getAdGroupKeyWords($("#ddlCampKey option:selected").val(), '');
    //    $('#ddlCampaignADKeyW option[value=' + $("#ddlCampKey option:selected").val() + ']').attr('selected', 'selected');

    //}
    //function ShowResultStudentCampaignTab() {
    //    getCampaignsOnDemand();
    //    getADGroupsOnDemand();
    //    getMappedkeyWordsOnDemand();
    //    getCampaignAdsOnDemand();
    //    CheckStudentRFAStatus();
    //}
    //function getCamp(cCode) {

    //    var htmlcontent = "";
    //    $('#div_result').empty();
    //    $.ajax({
    //        url: "/Students/Home/GetCampaignData?cCode=" + cCode + "",
    //        method: 'GET',
    //        success: function (data) {

    //            htmlcontent = '<div class="row">' +

    //                               '<div class="col-md-12">' +
    //                                '<div class="campaings-table-border table-responsive">' +
    //                                '<table>' +
    //                                '<tr>' +
    //                                '<th>Campaigns</th>' +
    //                                '<th>Budget</th>' +
    //                                '<th>Status</th>' +
    //                                '<th>Impressions</th>' +
    //                                '<th>Clicks</th>' +
    //                                '<th>CTR </th>' +
    //                                '<th>Cost</th>' +
    //                                '<th>Avg CPC</th>' +
    //                                '<th>Conversions</th>' +
    //                                '<th>Cost Per Conversion</th>' +
    //                                '<th>Action</th>' +
    //                                '</tr>';


    //            $.each(data, function (i, item) {

    //                htmlcontent += '<tr><td><a href=\'javascript:void(0)\' onclick="getgroup(\'' + item.CampaignCode + '\');" >' + item.CampaignName + '</a></td>' +
    //                                '<td>' + item.Budget + '</td>' +
    //                                "<td></td>" +
    //                                '<td></td>' +
    //                                '<td></td>' +
    //                                '<td></td>' +
    //                                '<td></td>' +
    //                                '<td></td>' +
    //                                '<td></td>' +
    //                                '<td></td>' +
    //                                //'<td><a href="#" class="popup-btn" data-toggle="modal" data-target="#myModalEdit"  onclick="getEditCamp(\'' + item.CampaignCode + '\');"><img src="/Content/images/plus-icon.png" alt="icon" />Edit</a></td></tr>	';
    //                                '<td><a href="#"  data-toggle="modal" data-target="#myModal"  onclick="getEditCamp(\'' + item.CampaignCode + '\');"><img src="/Content/images/student/edit-icon.png" alt="delete" /></a></td></tr>	';


    //            });
    //        }, async: false,

    //        error: function (data) {

    //        }
    //    });
    //    htmlcontent += '</table></div></div></div>';
    //    $('#div_result').html(htmlcontent);

    //}

    //function getgroup(cCode) {

    //    location.href = "/Students/Home/Index#adgroups#" + cCode + "";
    //    location.reload();
    //    return true;
    //}
    //function getgroup_Ads(cCode, adGroup) {
    //    //alert(cCode + ' ' + adGroup)
    //    location.href = "/Students/Home/Index#ads#" + cCode + '~' + adGroup + "";
    //    location.reload();
    //    return true;
    //}

    //function getAllCamp1() {

    //    var htmlcontent = "";
    //    $('#ddlCampaign').empty();
    //    $('#ddlCampaign2').empty();
    //    $('#ddlCampaignADGM').empty();
    //    $('#ddlCampaignAD1').empty();
    //    $('#ddlCampaignAD').empty();
    //    $('#ddlCampaignADKeyW').empty();

    //    $('#ddlCampaign').append($('<option></option>').val("ALL").html("ALL Campaigns"));
    //    $('#ddlCampaign2').append($('<option></option>').val("ALL").html("ALL Campaigns"));
    //    $('#ddlCampaignADGM').append($('<option></option>').val("").html("ALL Campaigns"));
    //    $('#ddlCampaignAD1').append($('<option></option>').val("ALL").html("Select Campaign"));
    //    $('#ddlCampaignAD').append($('<option></option>').val("").html("ALL Campaigns"));
    //    $('#ddlCampaignADKeyW').append($('<option></option>').val("ALL").html("Select Campaign"));
    //    $.ajax({
    //        url: "/Students/Home/GetCampaigns",
    //        method: 'GET',

    //        //data: { cCode: '1' },
    //        success: function (data) {




    //            $.each(data, function (i, item) {

    //                $('#ddlCampaign').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
    //                $('#ddlCampaign2').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
    //                $('#ddlCampaignADGM').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
    //                $('#ddlCampaignAD1').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
    //                $('#ddlCampaignAD').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
    //                $('#ddlCampaignADKeyW').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
    //            });
    //        }, async: false,

    //        error: function (data) {

    //        }
    //    });



    //}
    //function getAdGroups(cCode) {

    //    var htmlcontent = "";
    //    var htmlcontentRow = "";
    //    $('#ADGroup_Result').empty();

    //    $.ajax({
    //        url: "/Students/Home/GetADGroupsData?cCode=" + cCode + "",
    //        method: 'GET',

    //        //data: { cCode: '1' },
    //        success: function (data) {

    //            htmlcontent = '<div class="row">' +
    //             '<div class="col-md-12">' +

    //               '<div class="campaings-table">' +

    //                   '<div class="panel-group" id="accordion">';


    //            var tempCampaign = '';
    //            var tempADGroup = '';
    //            var colapsCtr = 0;
    //            $.each(data, function (i, item) {
    //                if (tempCampaign != item.CampaignName && colapsCtr != 0) {
    //                    htmlcontent += '</table></div></div></div>';
    //                    htmlcontent += '</div>';
    //                    // tempCampaign = item.CampaignName;
    //                    htmlcontentRow = '';
    //                }
    //                if (tempCampaign != item.CampaignName) {
    //                    tempCampaign = item.CampaignName;
    //                    colapsCtr++;
    //                    htmlcontent += '<div class="panel panel-default">';
    //                    if (colapsCtr == 1) {
    //                        htmlcontent += '<div class="panel-heading active-panel">';
    //                    }
    //                    else {
    //                        htmlcontent += '<div class="panel-heading">';
    //                    }
    //                    htmlcontent += '<h4 class="panel-title">';
    //                    if (colapsCtr == 1) {
    //                        htmlcontent += '<a class="accordion-toggle" data-toggle="collapse" aria-expanded="true" data-parent="#accordion" href="#collapse' + colapsCtr + '">';
    //                    }
    //                    else {
    //                        htmlcontent += '<a class="accordion-toggle" data-toggle="collapse" aria-expanded="false" data-parent="#accordion" href="#collapse' + colapsCtr + '">';
    //                    }
    //                    htmlcontent += '' + item.CampaignName + '' +
    //                                   '<i class="indicator glyphicon glyphicon-chevron-down  pull-right"></i><i class="indicator glyphicon glyphicon-menu-hamburger  pull-right"></i>  <span class="pull-right">Netword Type: ' + item.NetWorkType + ', Location: ' + item.Location + ', Budget: $ ' + item.Budget + '</span> </a> ' +
    //                                   '</h4> </div>';
    //                    //}
    //                    // alert(tempADGroup + ' # ' + item.AdGroupName)
    //                    //if (tempADGroup != item.AdGroupName) {
    //                    if (colapsCtr == 1) {
    //                        htmlcontent += '<div id="collapse' + colapsCtr + '" class="panel-collapse collapse in">';
    //                    }
    //                    else {
    //                        htmlcontent += '<div id="collapse' + colapsCtr + '" class="panel-collapse collapse">';
    //                    }
    //                    htmlcontent += '<div class="panel-body">' +
    //                                   '<div class="campaings-table table-responsive">' +

    //                               '<table>' +

    //                                   '<tr>' +

    //                                       '<th>Ad Groups</th>' +
    //                                       '<th>Status</th>' +
    //                                       '<th>Impressions</th>' +
    //                                       '<th>Clicks</th>' +
    //                                       '<th>CTR</th>' +
    //                                       '<th>Cost</th>' +
    //                                       '<th>Avg CPC</th>' +
    //                                       '<th>Conversions</th>' +
    //                                       '<th>Cost Per Conversion</th>' +
    //                                       '<th>Action</th>' +
    //                                   '</tr>';
    //                }
    //                //alert(item.AdGroupName)
    //                htmlcontent += '<tr>' +

    //                    '<td><a href=\'javascript:void(0)\' onclick="getgroup_Ads(\'' + item.CampaignCode + '\'' + ',' + '\'' + item.AdGroupCode + '\');" >' + item.AdGroupName + '</a></td>' +
    //                    '<td></td>' +
    //                    '<td></td>' +
    //                    '<td></td>' +
    //                    '<td></td>' +
    //                    '<td></td>' +
    //                    '<td></td>' +
    //                    '<td></td>' +
    //                    '<td></td>' +
    //                    '<td><a href="#"  data-toggle="modal" data-target="#myModal1"  onclick="getEditAdgroup(\'' + item.CampaignCode + '\'' + ',' + ' \'' + item.AdGroupCode + '\');"><img src="/Content/images/student/edit-icon.png" alt="delete" /></a></td>' +
    //                '</tr>';




    //                //if (tempADGroup != item.AdGroupName) {
    //                //  htmlcontent += '</table></div></div></div>';
    //                //   tempADGroup = item.AdGroupName;
    //                //}




    //                //if (tempCampaign != item.CampaignName ) {
    //                //    htmlcontent += '</table></div></div></div>';
    //                //    htmlcontent += '</div>';
    //                //    tempCampaign = item.CampaignName;
    //                //    htmlcontentRow = '';
    //                //}


    //            });
    //        }, async: false,

    //        error: function (data) {
    //            alert(data);
    //        }
    //    });
    //    htmlcontent += '</div></div></div></div>';

    //    $('#ADGroup_Result').html(htmlcontent);

    //}
    //function getAds(cCode, adGroup) {
    //    //alert(adGroup);
    //    var htmlcontent = "";
    //    var htmlcontentRow = "";
    //    var commInfo = "";
    //    $('#ADs_Result').empty();
    //    $('#Cmp_Details_ADS').empty();

    //    $.ajax({
    //        url: "/Students/Home/GetADsData?cCode=" + cCode + "",
    //        method: 'GET',

    //        //data: { cCode: '1' },
    //        success: function (data) {

    //            htmlcontent = '<div class="row">' +
    //             '<div class="col-md-12">' +

    //               //'<div class="campaings-table">' +

    //                   '<div class="panel-group" id="accordion1">';


    //            var tempCampaign = '';
    //            var tempADGroup = '';
    //            var colapsCtr = 0;


    //            AdGroupsResult = $.grep(data, function (element, index) {

    //                return element.Parent == null && element.AdGroupCode == adGroup.replace('ALL', element.AdGroupCode);
    //                // return element.CampaignCode == 'CC0001'; //null;


    //            });

    //            $.each(AdGroupsResult, function (i, item) {


    //                colapsCtr = i;
    //                htmlcontent += '<div class="panel panel-default">';
    //                if (colapsCtr == 0) {
    //                    //  commInfo = item.CampaignName + ' <span class="pull-right">Netword Type: ' + item.NetWorkType + ', Location: ' + item.Location + ', Budget: $ ' + item.Budget + '</span>'
    //                    // $('#Cmp_Details_ADS').html(commInfo);
    //                    htmlcontent += '<div class="panel-heading active-panel">';
    //                }
    //                else {
    //                    htmlcontent += '<div class="panel-heading">';
    //                }
    //                htmlcontent += '<h4 class="panel-title">';
    //                if (colapsCtr == 0) {
    //                    htmlcontent += '<a class="accordion-toggle" data-toggle="collapse" aria-expanded="true" data-parent="#accordion1" href="#collapseAds' + colapsCtr + '">';
    //                }
    //                else {
    //                    htmlcontent += '<a class="accordion-toggle" data-toggle="collapse" aria-expanded="false" data-parent="#accordion1" href="#collapseAds' + colapsCtr + '">';
    //                }
    //                htmlcontent += item.CampaignName + '>' + item.AdGroupName + '' +
    //                               '<i class="indicator glyphicon glyphicon-chevron-down  pull-right"></i><i class="indicator glyphicon glyphicon-menu-hamburger  pull-right"></i>   </a> ' +
    //                               '</h4> </div>';

    //                if (colapsCtr == 0) {
    //                    htmlcontent += '<div id="collapseAds' + colapsCtr + '" class="panel-collapse collapse in">';
    //                }
    //                else {
    //                    htmlcontent += '<div id="collapseAds' + colapsCtr + '" class="panel-collapse collapse">';
    //                }

    //                htmlcontent += '<div class="panel-body">';


    //                htmlcontent += '<div id="myCarousel' + i + '" class="carousel slide" data-ride="carousel">';
    //                htmlcontent += '<div class="carousel-inner" role="listbox">';


    //                AdGroupsResultChild = $.grep(data, function (element, index) {

    //                    return element.Parent == item.AdGroupCode || element.AdGroupCode == item.AdGroupCode;

    //                });


    //                var l = 0;
    //                $.each(AdGroupsResultChild, function (k, childItem) {

    //                    //alert(childItem.AdGroupCode);
    //                    if (l == 0) {
    //                        htmlcontent += ' <div class="item active">' +
    //                            '<div class="item-slider">';
    //                    }
    //                    else if (l != 0 & l % 3 == 0) {


    //                        htmlcontent += '<div class="item">' +
    //                                        '<div class="item-slider">';
    //                    }



    //                    htmlcontent += ' <div class="item-list col-md-4">' +
    //                                                   '<a href=' + childItem.FinalUrl + '>' + childItem.HeadLine + '</a>' +
    //                                                   '<span class="green-text">' + childItem.DispUrl + '</span>' +
    //                                                   '<span class="item-text">' + childItem.DescLine1 + '' + childItem.DescLine2 + '</span>' +

    //                                                   '<a href="#"  data-toggle="modal" data-target="#myModal2"  onclick="getEditAds(\'' + item.CampaignCode + '\'' + ',' + ' \'' + item.AdGroupCode + '\'' + ',' + '\'' + childItem.AdCode + '\');"><img src="/Content/images/student/edit-icon.png" alt="delete" /></a>' +
    //                                               '</div>';
    //                    l++;

    //                    if (l != 0 & (l % 3 == 0 | l == AdGroupsResultChild.length)) {

    //                        htmlcontent += '<div class="clearfix"></div>' +
    //                    '</div>' +
    //                '</div>';
    //                    }
    //                });

    //                htmlcontent += '</div>';
    //                if (l > 0) {
    //                    htmlcontent += '<a class="left carousel-control" href="#myCarousel' + i + '" role="button" data-slide="prev">' +
    //                                            '<img src="/Content/images/arrow-left.png" class="arrow" aria-hidden="true">' +

    //                                            '<span class="sr-only">Previous</span>' +
    //                                        '</a>' +
    //                                        '<a class="right carousel-control" href="#myCarousel' + i + '" role="button" data-slide="next">' +
    //                                            '<img src="/Content/images/arrow-right.png" class="arrow" aria-hidden="true">' +
    //                                           ' <span class="sr-only">Next</span>' +
    //                                       ' </a>';

    //                    //'</div>';
    //                }

    //                htmlcontent += '</div></div></div></div>';



    //            });
    //            htmlcontent += '</div></div></div>';
    //        }, async: false,

    //        error: function (data) {
    //            alert(data);
    //        }
    //    });


    //    $('#ADs_Result').html(htmlcontent);

    //}

    //function getAdGroupKeyWords(cCode, adGroup) {
    //    //alert(adGroup);
    //    var htmlcontent = "";
    //    var htmlcontentRow = "";
    //    var commInfo = "";
    //    $('#Kewords_Result').empty();
    //    $('#Cmp_Details_Keywords').empty();

    //    $.ajax({
    //        url: "/Students/Home/GetKeywordsData?cCode=" + cCode + "",
    //        method: 'GET',

    //        //data: { cCode: '1' },
    //        success: function (data) {

    //            htmlcontent = '<div class="row">' +
    //             '<div class="col-md-12">' +

    //               '<div class="campaings-table">' +

    //                   '<div class="panel-group" id="accordion3">';


    //            var tempCampaign = '';
    //            var tempADGroup = '';
    //            var colapsCtr = 0;


    //            AdGroupsResult = $.grep(data, function (element, index) {

    //                return element.Parent == null && element.AdGroupCode == adGroup.replace('ALL', element.AdGroupCode);
    //                //return element.CampaignCode == 'CC0001'; //null;

    //            });
    //            colapsCtr = 0;
    //            var Adg = "";
    //            $.each(AdGroupsResult, function (i, item) {

    //                if (item.AdGroupCode != Adg) {
    //                    Adg = item.AdGroupCode;
    //                    //colapsCtr = i;
    //                    htmlcontent += '<div class="panel panel-default">';
    //                    if (colapsCtr == 0) {

    //                        commInfo = item.CampaignName + ' <span class="pull-right">Netword Type: ' + item.NetWorkType + ', Location: ' + item.Location + ', Budget: $ ' + item.Budget + '</span>'
    //                        $('#Cmp_Details_Keywords').html(commInfo);
    //                        htmlcontent += '<div class="panel-heading active-panel">';
    //                    }
    //                    else {
    //                        htmlcontent += '<div class="panel-heading">';
    //                    }
    //                    htmlcontent += '<h4 class="panel-title">';
    //                    if (colapsCtr == 0) {
    //                        htmlcontent += '<a class="accordion-toggle" data-toggle="collapse" aria-expanded="true" data-parent="#accordion3" href="#collapseKeyW' + colapsCtr + '">';
    //                    }
    //                    else {
    //                        htmlcontent += '<a class="accordion-toggle" data-toggle="collapse" aria-expanded="false" data-parent="#accordion3" href="#collapseKeyW' + colapsCtr + '">';
    //                    }

    //                    htmlcontent += '' + item.AdGroupName + '' +
    //                                   '<i class="indicator glyphicon glyphicon-chevron-down  pull-right"></i><i class="indicator glyphicon glyphicon-menu-hamburger  pull-right"></i>  </a> ' +
    //                                   '</h4> </div>';

    //                    if (colapsCtr == 0) {
    //                        htmlcontent += '<div id="collapseKeyW' + colapsCtr + '" class="panel-collapse collapse in">';
    //                    }
    //                    else {
    //                        htmlcontent += '<div id="collapseKeyW' + colapsCtr + '" class="panel-collapse collapse">';
    //                    }

    //                    htmlcontent += '<div class="panel-body">';


    //                    //htmlcontent += '<div id="myCarousel' + i + '" class="carousel slide" data-ride="carousel">';
    //                    //htmlcontent += '<div class="carousel-inner" role="listbox">';
    //                    htmlcontent += '<div class="campaings-table table-responsive"> <table> <tr>' +
    //                                    '<th>Keyword</th>' +
    //                                    '<th>Status</th>' +
    //                                    '<th>Impressions</th>' +
    //                                    '<th>Clicks</th>' +
    //                                    '<th>CTR</th>' +
    //                                    '<th>Cost</th>' +
    //                                    '<th>Avg CPC</th>' +
    //                                    '<th>Conversions</th>' +
    //                                    '<th>Cost Per Conversion</th>' +
    //                                    '<th>Action</th>' +
    //                                    '</tr>';


    //                    AdGroupsResultChild = $.grep(data, function (element, index) {

    //                        return element.AdGroupCode == item.AdGroupCode;

    //                    });


    //                    var l = 0;
    //                    $.each(AdGroupsResultChild, function (k, childItem) {

    //                        htmlcontent += ' <tr>' +

    //                                                '<td>' + childItem.KeyName + '</td>' +
    //                                                '<td></td>' +
    //                                                '<td></td>' +
    //                                                '<td></td>' +
    //                                                '<td></td>' +
    //                                                '<td></td>' +
    //                                                '<td></td>' +
    //                                                '<td></td>' +
    //                                                '<td></td>' +
    //                                                '<td><a href="#"  data-toggle="modal" data-target="#myModal4"  onclick="getEditKeyMapp(\'' + childItem.CampaignCode + '\'' + ',' + ' \'' + childItem.AdGroupCode + '\');"><img src="/Content/images/student/edit-icon.png" alt="delete" /></a></td>' +

    //                                                ' </tr>';
    //                    });

    //                    htmlcontent += '</table></div>';



    //                    htmlcontent += '</div></div></div>';

    //                    colapsCtr++;
    //                }// End if group name check

    //            });
    //            htmlcontent += '</div></div></div></div>'
    //        }, async: false,

    //        error: function (data) {
    //            alert(data);
    //        }
    //    });


    //    $('#Kewords_Result').html(htmlcontent);

    //}



    //function IsNumeric(strString) {

    //    var strValidChars = "0123456789";
    //    var strChar;
    //    var blnResult = true;
    //    //var strSequence = document.frmQuestionDetail.txtSequence.value; 
    //    //test strString consists of valid characters listed above 
    //    if (strString.length == 0)
    //        return false;
    //    for (i = 0; i < strString.length && blnResult == true; i++) {
    //        strChar = strString.charAt(i);
    //        if (strValidChars.indexOf(strChar) == -1) {
    //            blnResult = false;
    //        }
    //    }
    //    if (blnResult == false) {
    //        $("#msgspannum").show();
    //    }
    //    else {
    //        $("#msgspannum").hide();
    //    }
    //    return blnResult;
    //}

    //function isNumber(evt, element) {

    //    var charCode = (evt.which) ? evt.which : event.keyCode

    //    if (
    //        (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
    //        (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
    //        (charCode < 48 || charCode > 57))
    //        return false;

    //    return true;
    //}

    //function isNumberKey(evt) {
    //    var charCode = (evt.which) ? evt.which : event.keyCode;
    //    if (charCode != 46 && charCode > 31
    //    && (charCode < 48 || charCode > 57))
    //        return false;

    //    return true;
    //}

    //function validateURL(value) {
    //    return /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(value);
    //}
}

/*start Working code*/

//#region  Campaigns
function ClearCampaignPopup() {
    $('#txtCampaignName').val('');
    $("select#ddlNetwordType")[0].selectedIndex = 0;
    $('#txtLocation').val('');
    $('#txtBudget').val('');
    if ($("#CampaignError").length > 0) {
        document.getElementById("CampaignError").remove();
    }
    if ($("#CampaignStatus").length > 0) {
        $("input[name=someSwitchOption001]")[0].checked = true;
    }
    $("#btnCreateCamp").attr("disabled", false);
    $('#hdnCampId').val('0');
    $('#myModal').modal('hide');
    $('#divCampNotify').text("All Fields are required");
}
function HideCampaignStatus() {
    $('#CampaignStatus').hide();
}
function showCampaignPopup() {
    HideCampaignStatus();
    $('#TitlePopupCampaign').text("Create AdWords Campaign");
    ClearCampaignPopup();
}
function CampaignValidation() {
    debugger;
    var CampaignErrors = '<span id="CampaignError" class="error-red"></span>';
    if ($("#CampaignError").length > 0) {
        $("#CampaignError").remove();
    }
    if ($('#txtCampaignName').val().length == 0) {
        $('#txtCampaignName').focus();
        $('#txtCampaignName').after(CampaignErrors);
        $('#CampaignError').text("Campaign name is required");
        return false;
    }
    if ($('#txtCampaignName').val().length >= 100) {
        $('#txtCampaignName').focus();
        $('#txtCampaignName').after(CampaignErrors);
        $('#CampaignError').text("Campaign name must be less than 100 characters");
        return false;
    }
    if ($("select[id='ddlNetwordType'] option:selected").index() == 0) {
        $('#ddlNetwordType').focus();
        $('#ddlNetwordType').after(CampaignErrors);
        $('#CampaignError').text("Network Type is required");
        // $("#btnCreateAccount").attr("disabled", false);
        return false;
    }
    if ($('#txtLocation').val().length == 0) {
        $('#txtLocation').focus();
        $('#txtLocation').after(CampaignErrors);
        $('#CampaignError').text("Location is required");
        // $("#btnCreateAccount").attr("disabled", false);
        return false;
    }
    if ($('#txtLocation').val().length >= 30) {
        $('#txtLocation').focus();
        $('#txtLocation').after(CampaignErrors);
        $('#CampaignError').text("Location must be less than 30 characters");
        return false;
    }
    if ($('#txtBudget').val().length == 0) {
        $('#txtBudget').focus();
        $('#txtBudget').after(CampaignErrors);
        $('#CampaignError').text("Budget is required");
        // $("#btnCreateAccount").attr("disabled", false);
        return false;
    }
    
    if ($('#txtBudget').val() > 50000) {
        $('#txtBudget').focus();
        $('#txtBudget').after(CampaignErrors);
        $('#CampaignError').text("Max budget limit is 50000");
        // $("#btnCreateAccount").attr("disabled", false);
        return false;
    }
    var float = /^\s*(\+|-)?((\d+(\.\d+)?)|(\.\d+))\s*$/;
    var Budget = $('#txtBudget').val();
    if (!float.test(Budget)) {
        $('#txtBudget').focus();
        $('#txtBudget').after(CampaignErrors);
        $('#CampaignError').text("Budget should be a numeric value.");
        // $("#btnCreateAccount").attr("disabled", false);
        return false;
    }
    if ($("#CampaignStatus").length > 0) {
        debugger;
        if ($("input[name=someSwitchOption001]")[0].checked == false)
        {
            alert("Are you sure you want to permanently delete this campaign ?\nYou will not be able to restore this campaign again.");
            return false;
        }
        else {
            return true;
        }
    }

    else {
        return true;
    }
}
function getCampaignsOnDemand() {
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'CampaignCode': $('#ddlUniqueCampaign').val(),
    });
    $('#div_result').empty();
    $.ajax({
        //url: "/Students/Home/GetCampaignData",
        url: "/Students/Home/getCampaignsOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            //$("#div_result").addClass("loadSectn");
        },
        complete: function () {
            $("#div_result").removeClass("loadSectn");
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
                                '<table id="campaignTable">' +
                                '<thead id="campaignHead"><tr>' +
                                '<th></th>' +
                                '<th>Campaigns</th>' +
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
                                '<th>Settings</th>' +
                                '</tr></thead><tbody id="CampaignBody">';
            var index = 0;
            $.each(data, function (i, item) {
                index = index + 1;
                CountCamp = data.length;
                //CampaignDropdownChange(' + item.CampaignCode + ')
                htmlcontent += '<tr><td>' + index + '</td>' +
                    '<td class="nav nav-tabs"><a href=\'#ADGROUPS\' onClick=\'$("#ddlUniqueCampaign").val(' + item.CampaignCode + ');  CampaignDropdownChange(' + item.CampaignCode + '); $("#li_adgroups a").click();\'>' + item.CampaignName + '</a></td>' +
                                '<td>' + item.Budget.toFixed(2) + '</td>';

              htmlcontent += '<td>' + (item.NewImpression).toFixed(0) + '</td>' +
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

                htmlcontent += //'<td><a href="#" class="popup-btn" data-toggle="modal" data-target="#myModalEdit"  onclick="getEditCamp(\'' + item.CampaignCode + '\');"><img src="/Content/images/plus-icon.png" alt="icon" />Edit</a></td></tr>	';
                                '<td>' +
                                '<span class="showTooltip visible-lg-inline-block" data-original-title="">' +
                                '<a href="#" data-toggle="modal" class="disableItem" data-target="#myModal"  onclick="getEditCamp(\'' + item.CampaignCode + '\');"><img src="/Content/images/student/edit-icon.png" alt="delete" /></a></span></td></tr>';
                Budget += parseFloat(checkint(item.Budget));
            });
            if (data != '') {
                //debugger;
                var TotalCTR = ((parseFloat(Clicks.toFixed(0)) / parseFloat(RevNewImps.toFixed(0))) * 100).toFixed(2).replace("NaN", "0.00");
                var ImpressionShare = (RevNewImps.toFixed(0) / Imps.toFixed(0)).toFixed(2).replace("NaN", "0.00");
                var ImpressionLost = (parseFloat(ImpressionShare) == 0) ? "0.00" : (100 - parseFloat(ImpressionShare)).toFixed(2);
                //var ImpressionLost = (100 - parseFloat(ImpressionShare)).toFixed(2);
                var CVR = (parseFloat(cnver.toFixed(0)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");;
                var AvgCPC = (parseFloat(Cost.toFixed(2)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");


                htmlcontent += '<tfoot id="KeywordFooter"><tr><td><b>Total</b></td>' +
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
                               '<td></td>' +
                               '</tr>';

                htmlcontent += '<tr><td><b>Avg</b></td>' +
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
            htmlcontent += '</tbody></table></div></div></div>';
            $('#div_result').html(htmlcontent);
            $("#CapaignFooter").insertAfter("#campaignHead");
            $(".Loader").hide();
            CheckStudentRFAStatus();
            ApplyDataTable("campaignTable", "13");

        }, async: false,
        error: OnError
    });
}
function getEditCamp(cCode) {
    ClearCampaignPopup();
    $('#TitlePopupCampaign').text("Edit AdWords Campaign");
    var htmlcontent = "";
    var ddlval = "";
    $('#CampaignStatus').show();
    // $('#div_EditCamp').empty();
    $.ajax({
        url: "/Students/Home/GetCampaignData?cCode=" + cCode + "",
        method: 'GET',
        //data: { cCode: '1' },
        success: function (data) {
            $.each(data, function (i, item) {
                ddlval = item.NetWorkType;
                $('#txtCampaignName').val(item.CampaignName);
                $("select#ddlNetwordType").val('1');
                $('#txtLocation').val(item.Location);
                $('#txtBudget').val(item.Budget);
                $('#hdnCampId').val(item.CampaignCode);
                $("#someSwitchOptionDefault").prop('checked', item.isActive);
                /* htmlcontent += '<table><tr><td>Campaign Name</td>' +
                                 '<td><div class="form-group"><input type="text" id="txtCampaignNameEdit" name="txtCampaignNameEdit" value=\'' + item.CampaignName + '\'  maxlength="100" class="form-control" data-val="true" data-val-required="Campaign name is required." data-val-msg="Campaign name is required."> ' +
                                 '<span id="msgvalidE1" class="field-validation-valid text-danger" data-valmsg-for="txtCampaignNameEdit" data-valmsg-replace="true"></span></div></td></tr>' +
                                 '<tr><td>Network Type</td>' +
                                 '<td><div class="form-group"><select class="form-control" id="ddlNetwordTypeEdit" name="ddlNetwordTypeEdit" data-val="true" data-val-required="Network Type is required." data-val-msg="Network Type is required."> <option value="">Select</option><option value="1">Type1</option><option value="2">Type2</option> </select> ' +
                                 '<span id="msgvalidE2" class="field-validation-valid text-danger" data-valmsg-for="ddlNetwordTypeEdit" data-valmsg-replace="true"></span></div></td></tr>' +
                                 '<tr><td>Location</td>' +
                                 '<td><div class="form-group"><input type="text" id="txtLocationEdit" name="txtLocationEdit" value=\'' + item.Location + '\' class="form-control" data-val="true" data-val-required="Location is required." data-val-msg="Location is required.">' +
                                 '<span id="msgvalidE3" class="field-validation-valid text-danger" data-valmsg-for="txtLocationEdit" data-valmsg-replace="true"></span></div></td></tr>' +
                                 '<tr><td>Budget</td>' +
                                 '<td><div class="form-group"><input type="text" id="txtBudgetEdit" name="txtBudgetEdit" value=\'' + item.Budget + '\'class="form-control" data-val="true" data-val-required="Budget is required." data-val-msg="Budget is required.">' +
                                 '<span id="msgvalidE4" class="field-validation-valid text-danger" data-valmsg-for="txtBudgetEdit" data-valmsg-replace="true"></span></div> <input type="hidden" id="hdnCampId" name="hdnCampId" value=\'' + item.CampaignCode + '\' /></td></tr></table>';*/
            });
        }, async: false,
        error: OnError
    });



}
//#end region Campaigns


//#region  Adgroups
function AdGroupFilter(CampaignCode, AdGroupCode) {
    $("#ddlUniqueCampaign").val(CampaignCode);
    if ($('#ddlUniqueCampaign').prop('selectedIndex') != 0) {
        $("#ddlUniqueAdGroups").css("pointer-events", "");
        $("#ddlUniqueAdGroups").css("opacity", "");
    }
    else {
        $("#ddlUniqueAdGroups").css("pointer-events", "none");
        $("#ddlUniqueAdGroups").css("opacity", "0.6");
    }
    BindAdGroupsOnlyDropdown(CampaignCode, '#ddlUniqueAdGroups');
    $("#ddlUniqueAdGroups").val(AdGroupCode);
    BindStudentCampaignTab();
}
function ClearAdgroupPopup() {
    debugger;
    $("#div_Adtext").removeClass("disable6");
    $("select#ddlCampaignADGM")[0].selectedIndex = 0;
    $('#txtADGroupName').val('');
    $('#txtADGroupHeadline').val('');
    $('#txtADGroupDesc1').val('');
    $('#txtADGroupDesc2').val('');
    $('#txtADGroupDespUrl').val('');
    $('#txtADGroupFinalUrl').val('');
    $('#spntxtADGroupHeadline').text('25 Characters left');
    $('#spntxtADGroupDesc1').text('35 Characters left');
    $('#spntxtADGroupDesc2').text('35 Characters left');

    $('#spntADGroupHeadlinePreview').text('');
    $('#spntADGroupDesc1Preview').text('');
    $('#spntADGroupDesc2Preview').text('');

    if ($("#AdgroupError").length > 0) {
        document.getElementById("AdgroupError").remove();
    }
    $("#btnCreateAdgroups").attr("disabled", false);
    $('#hdnAdGroupId').val('0');
    $('#myModal1').modal('hide');
    $('#divAdgroupNotify').text("All Fields are required");
}
function showAdgroupPopup() {
    $('#TitlePopupAdgroup').text("Create Adgroup");
    ClearAdgroupPopup();
}
function AdgroupValidation() {
    debugger
    var AdgroupErrors = '<span id="AdgroupError" class="error-red"></span>';
    if (document.contains(document.getElementById("AdgroupError"))) {
        document.getElementById("AdgroupError").remove();
    }

    if ($("select[id='ddlCampaignADGM'] option:selected").index() == 0) {
        $('#ddlCampaignADGM').focus();
        $('#ddlCampaignADGM').after(AdgroupErrors);
        $('#AdgroupError').text("Campaign is required");
        return false;
    }
    if ($('#txtADGroupName').val().length == 0) {
        $('#txtADGroupName').focus();
        $('#txtADGroupName').after(AdgroupErrors);
        $('#AdgroupError').text("Ad group name is required");
        return false;
    }
    if ($('#txtADGroupName').val().length >= 50) {
        $('#txtADGroupName').focus();
        $('#txtADGroupName').after(AdgroupErrors);
        $('#AdgroupError').text("Ad group name must be less than 50 characters");
        return false;
    }
    if ($('#txtADGroupHeadline').val().length == 0) {
        $('#txtADGroupHeadline').focus();
        $('#txtADGroupHeadline').after(AdgroupErrors);
        $('#AdgroupError').text("Headline is required");
        return false;
    }
    if ($('#txtADGroupDesc1').val().length == 0) {
        $('#txtADGroupDesc1').focus();
        $('#txtADGroupDesc1').after(AdgroupErrors);
        $('#AdgroupError').text("Description line 1 is required");
        return false;
    }
    if ($('#txtADGroupDesc2').val().length == 0) {
        $('#txtADGroupDesc2').focus();
        $('#txtADGroupDesc2').after(AdgroupErrors);
        $('#AdgroupError').text("Description line 2 is required");
        return false;
    }
    if ($('#txtADGroupDespUrl').val().length == 0) {
        $('#txtADGroupDespUrl').focus();
        $('#txtADGroupDespUrl').after(AdgroupErrors);
        $('#AdgroupError').text("Display url is required");
        return false;
    }
    if ($('#txtADGroupDespUrl').val().length >= 35) {
        $('#txtADGroupDespUrl').focus();
        $('#txtADGroupDespUrl').after(AdgroupErrors);
        $('#AdgroupError').text("Display url must be less than 35 characters");
        return false;
    }
    if ($('#txtADGroupFinalUrl').val().length == 0) {
        $('#txtADGroupFinalUrl').focus();
        $('#txtADGroupFinalUrl').after(AdgroupErrors);
        $('#AdgroupError').text("Final url is required");
        return false;
    }
    if ($('#txtADGroupFinalUrl').val().length >= 35) {
        $('#txtADGroupFinalUrl').focus();
        $('#txtADGroupFinalUrl').after(AdgroupErrors);
        $('#AdgroupError').text("Final url must be less than 35 characters");
        return false;
    }

    else {
        return true;
    }
}
function getADGroupsOnDemand() {
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'CampaignCode': $('#ddlUniqueCampaign').val(),
        'AdGroupCode': $('#ddlUniqueAdGroups').val(),
    });
    $('#ADGroup_Result').empty();
    $.ajax({
        url: "/Students/Home/getADGroupsOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            //$(".Loader").show();
        },
        complete: function () {
            $("#ADGroup_Result").removeClass("loadSectn");
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
            htmlcontent = '<div class="row">' +

                               '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table id="AdgroupTable">' +
                                '<thead id="AdgroupHead"><tr>' +
                                '<th></th>' +
                                       '<th>Adgroup Name</th>' +
                                       '<th>Campaign Name</th>' +
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
                                       '<th>Settings</th>' +
                                   '</tr></thead><tbody id="AdgroupBody">';
            var index = 0;
            $.each(data, function (i, item) {
                debugger;
                CountCamp = data.length;
                index = index + 1;
                htmlcontent += '<tr><td>' + index + '</td>' +
                    //onClick=\'$("#ddlUniqueCampaign").val(' + item.CampaignCode + ');  CampaignDropdownChange(' + item.CampaignCode + '); $("#ddlUniqueAdGroups").val(' + item.AdGroupCode + '); $("#li_ads a").click();\'
                     '<td><a href=\'#KEYWORDS\'  onClick=\'$("#ddlUniqueCampaign").val(' + item.CampaignCode + ');  CampaignDropdownChange(' + item.CampaignCode + '); $("#ddlUniqueAdGroups").val(' + item.AdGroupCode + '); $("#li_keywords a").click();\'>' + item.AdGroupName + '</a></td>' +
                    '<td>' + item.CampaignName + '</td>';


                htmlcontent += '<td>' + (item.NewImpression).toFixed(0) + '</td>' +
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

                htmlcontent += '<td>' +
                               '<span class="showTooltip visible-lg-inline-block" data-original-title="">' +
                               '<a href="#"  class="disableItem" data-toggle="modal" data-target="#myModal1"  onclick="getEditAdgroup(\'' + item.CampaignCode + '\'' + ',' + ' \'' + item.AdGroupCode + '\');"><img src="/Content/images/student/edit-icon.png" alt="delete" /></a></span></td>' +
                               '</tr>';

                //Budget += parseFloat(checkint(item.Budget));
                //Imps += parseFloat(checkint(item.Impressions));
                //vClicks += parseFloat(checkint(item.Clicks));
                //CTR += parseFloat(checkint(item.CTR));
                //Cost += parseFloat(checkint(item.COST));

            });
            if (data != '') {
                var TotalCTR = ((parseFloat(Clicks.toFixed(0)) / parseFloat(RevNewImps.toFixed(0))) * 100).toFixed(2).replace("NaN", "0.00");
                var ImpressionShare = (RevNewImps.toFixed(0) / Imps.toFixed(0)).toFixed(2).replace("NaN", "0.00");
                var ImpressionLost = (parseFloat(ImpressionShare) == 0) ? "0.00" : (100 - parseFloat(ImpressionShare)).toFixed(2);
                //var ImpressionLost = (100 - parseFloat(ImpressionShare)).toFixed(2);
                var CVR = (parseFloat(cnver.toFixed(0)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");;
                var AvgCPC = (parseFloat(Cost.toFixed(2)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");


                htmlcontent += '<tfoot id="KeywordFooter"><tr><td><b>Total</b></td>' +
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

                htmlcontent += '<tr><td><b>Avg</b></td>' +
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
            htmlcontent += '</tbody></table></div></div></div>';
            $('#ADGroup_Result').html(htmlcontent);
            $("#AdgroupFooter").insertAfter("#AdgroupHead");
            CheckStudentRFAStatus();
            ApplyDataTable("AdgroupTable", "14");
        }, async: false,
        error: OnError
    });
}
function getEditAdgroup(cCode, AdgroupCode) {
    ClearAdgroupPopup();
    $('#TitlePopupAdgroup').text("Edit Adgroup");
    var htmlcontent = "";
    var ddlval = "";
    $('#ddlCampaignADGM').empty();
    $.ajax({
        url: "/Students/Home/GetADGroupsDataByID?cCode=" + cCode + "&AdgroupCode=" + AdgroupCode + "",
        method: 'GET',
        success: function (data) {
            $.each(data, function (i, item) {
                ddlval = item.CampaignCode;
                $('#txtADGroupName').val(item.AdGroupName);
                $('#txtADGroupHeadline').val(item.HeadLine);
                $('#txtADGroupDesc1').val(item.DescLine1);
                $('#txtADGroupDesc2').val(item.DescLine2);
                $('#txtADGroupDespUrl').val(item.DispUrl);
                $('#txtADGroupFinalUrl').val(item.FinalUrl);
                $('#hdnAdGroupId').val(AdgroupCode);

                $('#spntxtADGroupHeadline').text(25 - $('#txtADGroupHeadline').val().length + ' Characters Left');
                $('#spntxtADGroupDesc1').text(35 - $('#txtADGroupDesc1').val().length + ' Characters Left');
                $('#spntxtADGroupDesc2').text(35 - $('#txtADGroupDesc2').val().length + ' Characters Left');

                $('#spntADGroupHeadlinePreview').text($('#txtADGroupHeadline').val());
                $('#spntADGroupDesc1Preview').text($('#txtADGroupDesc1').val());
                $('#spntADGroupDesc2Preview').text($('#txtADGroupDesc2').val());
            });
        }, async: false,
        error: OnError
    });
    $('#ddlCampaignADGM').append($('<option></option>').val("").html("ALL Campaigns"));
    $.ajax({
        url: "/Students/Home/GetCampaigns",
        method: 'GET',
        success: function (data) {
            $.each(data, function (i, item) {
                $('#ddlCampaignADGM').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
            });
        }, async: false,
        error: OnError
    });

    //$('#ddlCampaignADGMEdit').val(ddlval);
    $('#ddlCampaignADGM option[value=' + ddlval + ']').attr('selected', 'selected');
    $("#div_Adtext").addClass("disable6");
}
//#end region Adgroups


//#region Ads
function ClearAdPopup() {
    debugger;
    $("select#ddlCampaignAD")[0].selectedIndex = 0;
    $("select#ddlADGroupName")[0].selectedIndex = 0;
    $('#txtADHeadline').val('');
    $('#txtADDesc1').val('');
    $('#txtADDesc2').val('');
    $('#txtADDespUrl').val('');
    $('#txtADFinalUrl').val('');
    $('#spntxtADHeadline').text('25 Characters left');
    $('#spntxtADDesc1').text('35 Characters left');
    $('#spntxtADDesc2').text('35 Characters left');

    $('#spntADHeadlinePreview').text('');
    $('#spntADDesc1Preview').text('');
    $('#spntADDesc2Preview').text('');
    if ($("#AdError").length > 0) {
        document.getElementById("AdError").remove();
    }
    $("#btnCreateAds").attr("disabled", false);
    $('#hdnAdId').val('0');
    $('#myModal2').modal('hide');
    $('#divAdNotify').text("All Fields are required");
}
function showAdPopup() {
    $('#TitlePopupAd').text("Create Ad");
    ClearAdPopup();
}
function AdValidation() {
    debugger
    var AdErrors = '<span id="AdError" class="error-red"></span>';
    if (document.contains(document.getElementById("AdError"))) {
        document.getElementById("AdError").remove();
    }

    if ($("select[id='ddlCampaignAD'] option:selected").index() == 0) {
        $('#ddlCampaignAD').focus();
        $('#ddlCampaignAD').after(AdErrors);
        $('#AdError').text("Campaign is required");
        return false;
    }
    if ($("select[id='ddlADGroupName'] option:selected").index() == 0) {
        $('#ddlADGroupName').focus();
        $('#ddlADGroupName').after(AdErrors);
        $('#AdError').text("Ad Group is required");
        return false;
    }

    if ($('#txtADHeadline').val().length == 0) {
        $('#txtADHeadline').focus();
        $('#txtADHeadline').after(AdErrors);
        $('#AdError').text("Headline is required");
        return false;
    }
    if ($('#txtADDesc1').val().length == 0) {
        $('#txtADDesc1').focus();
        $('#txtADDesc1').after(AdErrors);
        $('#AdError').text("Description line 1 is required");
        return false;
    }
    if ($('#txtADDesc2').val().length == 0) {
        $('#txtADDesc2').focus();
        $('#txtADDesc2').after(AdErrors);
        $('#AdError').text("Description line 2 is required");
        return false;
    }
    if ($('#txtADDespUrl').val().length == 0) {
        $('#txtADDespUrl').focus();
        $('#txtADDespUrl').after(AdErrors);
        $('#AdError').text("Display url is required");
        return false;
    }
    if ($('#txtADDespUrl').val().length >= 35) {
        $('#txtADDespUrl').focus();
        $('#txtADDespUrl').after(AdErrors);
        $('#AdError').text("Display url must be less than 35 characters");
        return false;
    }
    if ($('#txtADFinalUrl').val().length == 0) {
        $('#txtADFinalUrl').focus();
        $('#txtADFinalUrl').after(AdErrors);
        $('#AdError').text("Final url is required");
        return false;
    }
    if ($('#txtADFinalUrl').val().length >= 35) {
        $('#txtADFinalUrl').focus();
        $('#txtADFinalUrl').after(AdErrors);
        $('#AdError').text("Final url must be less than 35 characters");
        return false;
    }
    else {
        return true;
    }
}
function getCampaignAdsOnDemand() {
    ClearAdPopup();
    var cCode = $('#ddlUniqueCampaign').val();
    var adGroup = $('#ddlUniqueAdGroups').val();
    var htmlcontent = "";
    var htmlcontentRow = "";
    var commInfo = "";
    var oldAdgroupOld = "old";
    var SelectedData = JSON.stringify({
        'CampaignCode': $('#ddlUniqueCampaign').val(),
        'AdGroupCode': $('#ddlUniqueAdGroups').val(),
    });
    $('#ADs_Result').empty();
    $('#Cmp_Details_ADS').empty();

    $.ajax({
        url: "/Students/Home/getCampaignAdsOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            $("#ADs_Result").removeClass("loadSectn");
        },
        success: function (data) {

            htmlcontent = '<div class="row">' +
             '<div class="col-md-12">' +
             '<div class="panel-group" id="accordion1">';
            var tempCampaign = '';
            var tempADGroup = '';
            var colapsCtr = 0;
            AdGroupsResult = $.grep(data, function (element, index) {
                return element.AdGroupCode == adGroup.replace('ALL', element.AdGroupCode);
                // return element.CampaignCode == 'CC0001'; //null;


            });

            $.each(AdGroupsResult, function (i, item) {

                if (oldAdgroupOld != item.AdGroupCode) {
                    oldAdgroupOld = item.AdGroupCode;
                    colapsCtr = i;
                    htmlcontent += '<div class="panel panel-default">';
                    if (colapsCtr == 0) {
                        //  commInfo = item.CampaignName + ' <span class="pull-right">Netword Type: ' + item.NetWorkType + ', Location: ' + item.Location + ', Budget: $ ' + item.Budget + '</span>'
                        // $('#Cmp_Details_ADS').html(commInfo);
                        htmlcontent += '<div class="panel-heading active-panel">';
                    }
                    else {
                        htmlcontent += '<div class="panel-heading">';
                    }
                    htmlcontent += '<h4 class="panel-title">';
                    if (colapsCtr == 0) {
                        htmlcontent += '<a class="accordion-toggle" data-toggle="collapse" aria-expanded="true" data-parent="#accordion1" href="#collapseAds' + colapsCtr + '">';
                    }
                    else {
                        htmlcontent += '<a class="accordion-toggle" data-toggle="collapse" aria-expanded="false" data-parent="#accordion1" href="#collapseAds' + colapsCtr + ' " >';
                    }
                    htmlcontent += item.CampaignName + ' > ' + item.AdGroupName + '' +
                                   '<i id="open" class="indicator glyphicon glyphicon-chevron-down  pull-right"></i><i id="close" class="indicator glyphicon glyphicon-menu-hamburger  pull-right"></i>   </a> ' +
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

                        return element.AdGroupCode == item.AdGroupCode;

                    });


                    var l = 0;
                    $.each(AdGroupsResultChild, function (k, childItem) {

                        //alert(childItem.AdGroupCode);
                        if (l == 0) {
                            htmlcontent += ' <div class="item active">' +
                                '<div class="item-slider">';
                        }
                        else if (l != 0 & l % 2 == 0) {


                            htmlcontent += '<div class="item">' +
                                            '<div class="item-slider">';
                        }


                        htmlcontent += ' <div class="item-list col-md-6">' +
                                                       '<a href="http://' + childItem.FinalUrl + '" target="_blank" class="blue">' + childItem.HeadLine + '</a>' +
                                                       '<span class="green-text">' + childItem.DispUrl + '</span>' +
                                                       '<span class="item-text">' + childItem.DescLine1 + '</br>' + childItem.DescLine2 + '</span>' +
                                                       '<span class="showTooltip visible-lg-inline-block" data-original-title="">' +
                                                       '<a class="disableItem" href="#"  data-toggle="modal" data-target="#myModal2"  onclick="getEditAds(\'' + item.CampaignCode + '\'' + ',' + ' \'' + item.AdGroupCode + '\'' + ',' + '\'' + childItem.AdCode + '\');"><img src="/Content/images/student/edit-icon.png" alt="delete" /></a></span>' +
                                                   '</div>';
                        l++;

                        if (l != 0 & (l % 2 == 0 | l == AdGroupsResultChild.length)) {

                            htmlcontent += '<div class="clearfix"></div>' +
                        '</div>' +
                    '</div>';
                        }
                    });

                    htmlcontent += '</div>';
                    if (l > 0) {
                        htmlcontent += '<a class="left carousel-control" href="#myCarousel' + i + '" role="button" data-slide="prev">' +
                                                '<img src="/Content/images/arrow-left.png" class="arrow" aria-hidden="true">' +

                                                '<span class="sr-only">Previous</span>' +
                                            '</a>' +
                                            '<a class="right carousel-control" href="#myCarousel' + i + '" role="button" data-slide="next">' +
                                                '<img src="/Content/images/arrow-right.png" class="arrow" aria-hidden="true">' +
                                               ' <span class="sr-only">Next</span>' +
                                           ' </a>';

                        //'</div>';
                    }

                    htmlcontent += '</div></div></div></div>';



                }
            });
            htmlcontent += '</div></div></div>';
            $('#ADs_Result').html(htmlcontent);
            CheckStudentRFAStatus();
        }, async: false,
        error: OnError
    });
}
function getEditAds(cCode, AdgroupCode, AdCode) {
    ClearAdPopup();
    var htmlcontent = "";
    var ddlval = "";
    var ddlval1 = "";
    $('#hdnAdId').val("0");
    $('#ddlCampaignAD').empty();
    $('#ddlADGroupName').empty();
    var SelectedData = JSON.stringify({
        'CampaignCode': cCode,
        'AdGroupCode': AdgroupCode,
        'AdCode': AdCode,
    });

    $.ajax({
        url: "/Students/Home/getCampaignAdsOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        success: function (data) {
            data1 = $.grep(data, function (ele, index) {
                //alert(cCode +" && " + AdgroupCode + " && " + AdCode);
                return ele.CampaignCode == cCode && ele.Parent == AdgroupCode && ele.AdGroupCode == AdCode;
            });
            var l = 0;
            $.each(data, function (k, childItem) {
                ddlval = childItem.CampaignCode;
                ddlval1 = childItem.AdGroupCode;
                $('#txtADHeadline').val(childItem.HeadLine);
                $('#txtADDesc1').val(childItem.DescLine1);
                $('#txtADDesc2').val(childItem.DescLine2);
                $('#txtADDespUrl').val(childItem.DispUrl);
                $('#txtADFinalUrl').val(childItem.FinalUrl);
                $('#hdnAdGrpId').val(childItem.AdGroupCode);
                $('#hdnAdId').val(childItem.AdCode);

                $('#spntxtADHeadline').text(25 - $('#txtADHeadline').val().length + ' Characters Left');
                $('#spntxtADDesc1').text(35 - $('#txtADDesc1').val().length + ' Characters Left');
                $('#spntxtADDesc2').text(35 - $('#txtADDesc2').val().length + ' Characters Left');

                $('#spntADHeadlinePreview').text($('#txtADHeadline').val());
                $('#spntADDesc1Preview').text($('#txtADDesc1').val());
                $('#spntADDesc2Preview').text($('#txtADDesc2').val());

            });
        }, async: false,
        error: OnError
    });
    $('#ddlCampaignAD').append($('<option></option>').val("").html("Select Campaign"));
    $('#ddlADGroupName').append($('<option></option>').val("").html("Select Ad Group"));
    $.ajax({
        url: "/Students/Home/GetCampaigns",
        method: 'GET',

        //data: { cCode: '1' },
        success: function (data) {


            $.each(data, function (i, item) {

                $('#ddlCampaignAD').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));


            });
        }, async: false,
        error: OnError
    });
    $('#ddlCampaignAD option[value=' + ddlval + ']').attr('selected', 'selected');


    $.ajax({
        url: "/Students/Home/GetADGroupsDataDistinct?cCode=" + cCode + "",
        method: 'GET',
        success: function (data) {


            $.each(data, function (i, item) {

                //$('#ddlADGroupName1').append($('<option></option>').val(item.AdGroupCode).html(item.AdGroupName))
                $('#ddlADGroupName').append($('<option></option>').val(item.AdGroupCode).html(item.AdGroupName))


            });
        }, async: false,
        error: OnError
    });

    $('#ddlADGroupName option[value=' + ddlval1 + ']').attr('selected', 'selected');

}
//#end region


//#region Keyword
function showKeywordPopup() {
    debugger;
    $('#TitlePopupKeyword').text("Add Keyword");
    ClearKeywordPopup();
    $.ajax({
        url: "/Students/Home/ClearKeywordSession",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data) {
        }, async: false,
        error: OnError
    });
}
//this function is used to add row in table
function AddKeywordstemp() {
    var CampKey = $("select#ddlCampKey").val();
    var AdgrpKey = $("select#ddlAdgrpKey").val();
    var txtAutoKey = $("#txtAutoKey").val();
    //alert(ddl1 +'-'+ ddl2+'-'+ txtval);
    if ($("select#ddlCampKey")[0].selectedIndex == 0) {
        $("#divKeywordNotify").show();
        $('#divKeywordNotify').text("Campaign is required");
        $('#ddlCampKey').focus();
        return false;
    }
    if ($("select#ddlAdgrpKey")[0].selectedIndex == 0) {
        $("#divKeywordNotify").show();
        $('#divKeywordNotify').text("Ad Group Name is required");
        $('#ddlAdgrpKey').focus();
        return false;
    }
    if (txtAutoKey == "") {
        $("#divKeywordNotify").show();
        $('#divKeywordNotify').text("Keyword is required");
        $('#txtAutoKey').focus();
        return false;
    }
    else {
        $('#divKeywordNotify').text("All fields are required");
        $("#divkeywordmodel").show();
        debugger;
        var tempdata = getKeywordBidValue();
        var data1 = tempdata;
        var SelectedData = JSON.stringify({
            'CampaignCode': $("select#ddlCampKey").val(),
            'AdGroupCode': $("select#ddlAdgrpKey").val(),
            'Keywordtxt': $('#txtAutoKey').val(),
            'alldata': data1,
        });
        var keybid = "";
        //var keycodedb = "";
        $.ajax({
            //url: "/Students/Home/GetKeywordsAllData?Keywordtxt=" + $('#txtAutoKey').val() + "",
            url: "/Students/Home/CheckKeywordsToKeywordDataTable",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            //data: { cCode: '1' },
            success: function (data) {
                debugger;
                if (data == "Already exist") {
                    $('#divKeywordNotify').text("Already exist");
                    $('#txtAutoKey').focus();
                }
                else {
                    debugger;
                    EmptyKeywordTable();
                    hasDataKeywordtable(data);
                    var json = $.parseJSON(data);
                    var rowCount = $('#tblKeywords tr').length;
                    var trKeywordId = "trKeywordId" + rowCount;
                    var inputKeywordbid = "txtBid" + rowCount;
                    var t = 1;
                    for (var i = 0; i < json.length; ++i) {
                        if (json[i].KeywordStatus != "DELETE") {
                            var result = '<tr class="data-Map-Key" id="' + json[i].KeywordRowId + '"><td  id="' + "trKeywordId" + t + '" style="display:none">' + json[i].KeywordRowId + '</td><td>' + json[i].KeywordName + '';
                            result += '<td><input type="text"  id="' + "txtBid" + t + '"   value="' + json[i].KeywordBid + '"  placeholder="Number" maxlength="6"/></td><td><a href="#keywords"><img src="/Content/images/student/delete.png" alt="delete"  onclick="return deleteKeywordRow(' + json[i].KeywordRowId + ')" /></a></td></tr>';
                            $('#tblKeywords').append(result);
                            t++;
                        }
                    }
                }
            }, async: false,
            error: OnError
        });
        //var rowCount = $('#tblKeywords tr').length + 1;
        //var row = "txtkey" + rowCount;
        //var rowbid = "txtBid" + rowCount;
        //var rowhide = "hdnkey" + rowCount;
        //var trcount = "tr" + rowCount;
        //var data = '<tr class="data-Map-Key" id="' + trcount + '"><td>' + $('#txtAutoKey').val() + '<input type="hidden" id="' + rowhide + '" name="' + rowhide + '" value="' + keycodedb + '" /><input type="hidden" id="' + row + '" name="' + row + '" value="' + $('#txtAutoKey').val() + '" /></td>';
        //data += '<td><input type="text" id="' + rowbid + '"   value="' + keybid + '"  placeholder="Number" onkeypress="return isNumberKey(event)"/></td><td><a href="#"><img src="/Content/images/student/delete.png" alt="delete"  onclick="deleterow($(this)' + ',' + ' \'' + rowCount + '\')" /></a></td></tr>';
        //$('#tblKeywords').append(data);
    }
    return false;
}
function getMappedkeyWordsOnDemand() {
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'CampaignCode': $('#ddlUniqueCampaign').val(),
        'AdGroupCode': $('#ddlUniqueAdGroups').val(),
    });
    $('#Kewords_Result').empty();
    $.ajax({
        url: "/Students/Home/getMappedkeyWordsOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        beforeSend: function () {
            //$(".Loader").show();
        },
        complete: function () {
            $("#Kewords_Result").removeClass("loadSectn");
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
                                '<table id="KeywordTable">' +
                                '<thead id="KeywordHead"><tr>' +
                                 '<th></th>' +
                                 '<th title="Keyword Name">Keyword Name</th>' +
                                 '<th title="Adgroup Name">Adgroup Name</th>' +
                                 '<th title="Campaign Name">Campaign Name</th>' +
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
                                 '<th>Settings</th>' +
                                 '</tr></thead><tbody id="KeywordBody">';


            $.each(data, function (i, item) {
                //
                //
                debugger;
                CountCamp = data.length;
                htmlcontent += ' <tr><td></td>' +
                                            '<td>' + item.KeyName + '</td>' +
                                            '<td>' + item.AdGroupName + '</td>' +
                                            '<td>' + item.CampaignName + '</td>';

                htmlcontent += '<td>' + (item.NewImpression).toFixed(0) + '</td>' +
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


                htmlcontent += '<td>' +
                                            '<span class="showTooltip visible-lg-inline-block" data-original-title="">' +
                                            '<a href="#" class="disableItem"  data-toggle="modal" data-target="#myModal4"  onclick="getSingleKeywordDetails(\'' + item.CampaignCode + '\'' + ',' + ' \'' + item.AdGroupCode + '\');"><img src="/Content/images/student/edit-icon.png" alt="delete" /></a></span></td>' +
                                            ' </tr>';
                //alert(CostCpc);
                //alert(parseFloat(checkint(item.CostPerConversions).toFixed(2)));
            });
            if (data != '') {
                var TotalCTR = ((parseFloat(Clicks.toFixed(0)) / parseFloat(RevNewImps.toFixed(0))) * 100).toFixed(2).replace("NaN", "0.00");
                var ImpressionShare = (RevNewImps.toFixed(0) / Imps.toFixed(0)).toFixed(2).replace("NaN", "0.00");
                var ImpressionLost = (parseFloat(ImpressionShare) == 0) ? "0.00" : (100 - parseFloat(ImpressionShare)).toFixed(2);
                //var ImpressionLost = (100 - parseFloat(ImpressionShare)).toFixed(2);
                var CVR = (parseFloat(cnver.toFixed(0)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");;
                var AvgCPC = (parseFloat(Cost.toFixed(2)) / parseFloat(Clicks.toFixed(0))).toFixed(2).replace("NaN", "0.00");

                htmlcontent += '<tfoot id="KeywordFooter"><tr><td><b>Total</b></td>' +
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
                                   '<td></td>' +
        '</tr></tfoot>';
            }
            htmlcontent += '</tbody></table></div></div></div>';
            $('#Kewords_Result').html(htmlcontent);
            CheckStudentRFAStatus();
            ApplyDataTable("KeywordTable", "14");
        }, async: false,
        error: OnError
    });
}
//this function is used to delete row
function deleteKeywordRow(ID) {
    var tempdata = getKeywordBidValue();
    var data1 = tempdata;
    var SelectedData = JSON.stringify({
        'KeywordRowId': ID,
        'alldata': data1,
    });
    $.ajax({
        url: "/Students/Home/deleteKeywordRow",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        success: function (data) {
            EmptyKeywordTable();
            hasDataKeywordtable(data);
            var json = $.parseJSON(data);
            var rowCount = $('#tblKeywords tr').length + 1;
            var trKeywordId = "trKeywordId" + rowCount;
            var inputKeywordbid = "txtBid" + rowCount;
            var t = 1;
            for (var i = 0; i < json.length; ++i) {
                if (json[i].KeywordStatus != "DELETE") {
                    var result = '<tr class="data-Map-Key" id="' + json[i].KeywordRowId + '"><td  id="' + "trKeywordId" + t + '" style="display:none;">' + json[i].KeywordRowId + '</td><td>' + json[i].KeywordName + '';
                    result += '<td><input type="text" id="' + "txtBid" + t + '"    value="' + json[i].KeywordBid + '"  placeholder="Number" maxlength="6"/></td><td><a href="#keywords"><img src="/Content/images/student/delete.png" alt="delete"  onclick="deleteKeywordRow(' + json[i].KeywordRowId + ')" /></a></td></tr>';
                    $('#tblKeywords').append(result);
                    t++;
                }
            }
        }, async: false,
        error: OnError
    });
    return false;
}
function getEditKeyMapp(campcode, gropcode) {
    debugger;
    // alert(campcode + ' ' + gropcode)


    // $("#tblKeywords").find("tr:not(:first)").remove();
    $("#divkeywordmodel").show();
    $('#ddlCampKey option[value=' + campcode + ']').attr('selected', 'selected');
    getAdGroupsOnly(campcode, "#ddlAdgrpKey");
    alert(gropcode)
    $('#ddlAdgrpKey option[value=' + gropcode + ']').attr('selected', 'selected');

    $.ajax({
        url: "/Students/Home/GetKeywordsData?cCode=" + campcode + "",
        method: 'GET',
        //data: { cCode: '1' },
        success: function (data) {

            KeyResult = $.grep(data, function (element, index) {

                return element.Parent == null && element.AdGroupCode == gropcode;
                //return element.CampaignCode == 'CC0001'; //null;

            });
            $.each(KeyResult, function (k, item) {
                var rowCount = $('#tblKeywords tr').length + 1;
                var row = "txtkey" + rowCount;
                var rowbid = "txtBid" + rowCount;
                var rowhide = "hdnkey" + rowCount;
                var trcount = "tr" + rowCount;
                var newbid = ""
                if (item.NewBid == null) {
                    newbid = "N/A";
                }
                else {
                    newbid = item.NewBid;
                }

                //alert(item.KeyName)
                var data = '<tr class="data-Map-Key" id="' + trcount + '"><td>' + item.KeyName + '<input type="hidden" id="' + rowhide + '" name="' + rowhide + '" value="' + item.KeyCode + '" /></td>';
                data += '<td><input type="text" id="' + rowbid + '"   value="' + newbid + '"  placeholder="Number" maxlength="6"/></td><td><a href="#"><img src="/Content/images/student/delete.png" alt="delete"  onclick="deleterow($(this)' + ',' + '0,' + '\'' + campcode + '\'' + ',' + ' \'' + gropcode + '\'' + ',' + ' \'' + item.KeyCode + '\')" /></a></td></tr>';
                $('#tblKeywords').append(data);


            });
        }, async: false,
        error: OnError
    });



}
function clearpopup() {

    $("#divkeywordmodel").hide();
    $("#tblKeywords").find("tr:not(:first)").remove();
}
function ClearKeywordPopup() {
    $("select#ddlCampKey")[0].selectedIndex = 0;
    $("select#ddlAdgrpKey")[0].selectedIndex = 0;
    $('#txtAutoKey').val('');
    $('#divKeywordNotify').text('All fields are required');
    $("#btnKeyMappAddMain").attr("disabled", false);
    $("#divkeywordmodel").hide();
    $("select#ddlCampKey").attr("disabled", false);
    $("select#ddlAdgrpKey").attr("disabled", false);
    $('#hdnKeywordFlag').val('CREATE');
    EmptyKeywordTable();
}
//this function is used to empty table
function EmptyKeywordTable() {
    $('#tblKeywords').empty();
    var result = '<tr><th>Keyword/Pharses</th><th>Bid</th><th>Setting</th></tr>';
    $('#tblKeywords').append(result);
}

//this function checks if data is present or not..?
//if data is present then disable dropdownlist otherwise enable
function hasDataKeywordtable(data) {
    debugger;
    var count = 0;
    var json = $.parseJSON(data);
    for (var i = 0; i < json.length; ++i) {
        if (json[i].KeywordStatus == "DELETE") {
            count++;
        }
    }
    if (count != json.length) {
        $("select#ddlCampKey").attr("disabled", true);
        $("select#ddlAdgrpKey").attr("disabled", true);
    }
    else {
        $("select#ddlCampKey").attr("disabled", false);
        $("select#ddlAdgrpKey").attr("disabled", false);
    }
}
function ExistKeywordHTMLTable(KeywordText) {
    var roco = $('#tblKeywords tr').length;
    for (var i = 2 ; i <= roco ; i++) {
        var Key = $('tr.data-Map-Key').find('#trKeywordName' + i).html();//Bind to the first name with class f-name01  
        if (KeywordText.toUpperCase() == Key.toUpperCase()) {
            return true;
        }
    }
    return false;
}
function SaveMapKeywords() {
    var data = [];
    var roco = $('#tblKeywords tr').length;
    if (roco == "1") {
        $('#divKeywordNotify').text('Atleast one keyword is required');
        return false;
    }
    else {
        var tempdata = getKeywordBidValue();
        var data1 = tempdata;
        var SelectedData = JSON.stringify({
            'CampaignCode': $("select#ddlCampKey").val(),
            'AdGroupCode': $("select#ddlAdgrpKey").val(),
            'alldata': data1,
            'hdnKeywordFlag': $('#hdnKeywordFlag').val(),

        });
    }
    $.ajax({
        url: "/Students/Home/SaveMapKeywords",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        success: function (data) {
            if (data[0] == "FALSE") {
                //$('#divKeywordNotify').text('Incorrect bid value indexes are :' + data[1]);
                $('#divKeywordNotify').text(data[1]);
            }
            if (data[0] == "TRUE") {
                if (data[1] == "CREATE")
                    $('#SuccessHead').text('You have mapped Keywords successfully');
                if (data[1] == "EDIT")
                    $('#SuccessHead').text('You have mapped Keywords successfully');
                $("#myModal4").hide();
                $('#btnCancel').click();
                getMappedkeyWordsOnDemand();
                $('#ModalSuccess').modal('show');
            }
        }, async: false,
        error: OnError
    });
    return false;
}
//this function is used to get bid values from all rows.
function getKeywordBidValue() {
    debugger;
    var data = [];
    var roco = $('#tblKeywords tr').length;
    for (var i = 1 ; i < roco ; i++) {
        var temp = $('#trKeywordId' + i);
        var RowId = $('#trKeywordId' + i)[0].innerText;
        var BidValue = $('tr.data-Map-Key').find('#txtBid' + i).val();
        var alldata =
        {
            'RowId': RowId,
            'BidValue': BidValue,
        }
        data.push(alldata);
    }
    var data1 = JSON.stringify(data);
    return data1;
}
function getSingleKeywordDetails(campcode, gropcode) {
    debugger;
    ClearKeywordPopup();
    $("#divkeywordmodel").show();
    $('#TitlePopupKeyword').text("Edit Keyword");
    $('#hdnKeywordFlag').val('0');
    $('#hdnKeywordFlag').val('EDIT');
    getAdGroupsOnly(campcode, "#ddlAdgrpKey");
    $('#ddlCampKey').val(campcode).attr("selected", "selected");
    $('#ddlAdgrpKey').val(gropcode).attr("selected", "selected");
    var SelectedData = JSON.stringify({
        'CampaignCode': campcode,
        'AdGroupCode': gropcode,
    });
    $.ajax({
        url: "/Students/Home/getSingleKeywordDetails",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        success: function (data) {
            EmptyKeywordTable();
            hasDataKeywordtable(data);
            var json = $.parseJSON(data);
            var rowCount = $('#tblKeywords tr').length;
            var trKeywordId = "trKeywordId" + rowCount;
            var inputKeywordbid = "txtBid" + rowCount;
            var t = 1;
            for (var i = 0; i < json.length; ++i) {
                if (json[i].KeywordStatus != "DELETE") {
                    var result = '<tr class="data-Map-Key" id="' + json[i].KeywordRowId + '"><td  id="' + "trKeywordId" + t + '" style="display:none;">' + json[i].KeywordRowId + '</td><td>' + json[i].KeywordName + '';
                    result += '<td><input type="text"  id="' + "txtBid" + t + '"   value="' + json[i].KeywordBid + '"  placeholder="Number" maxlength="6"/></td><td><a href="#keywords"><img src="/Content/images/student/delete.png" alt="delete"  onclick="return deleteKeywordRow(' + json[i].KeywordRowId + ')" /></a></td></tr>';
                    $('#tblKeywords').append(result);
                    t++;
                }
            }
        }, async: false,
        error: OnError
    });
    return false;
}
//#end region


//#region RFA
function EnableGoForAuction() {
    $("#rfastatus").text('Current request submitted');
    $("#rfastatus").css("color", "green");
    $("#linkreqsent").text('Request Submitted');
    $("#linkreqsent").css("pointer-events", "none");
    $("#linkreqsent").css("opacity", "0.6");
    $(".disableItem").each(function () {
        //$(this).prop('disabled', true);
        $(this).css("pointer-events", "none");
        $(this).css("opacity", "0.6");
        $(this).attr("disabled", true);
    });
    $(".showTooltip").attr('data-original-title', 'Request Submitted...');
    $(".showTooltip").tooltip({
        'placement': 'bottom'
    });
}
function CheckStudentRFAStatus() {
    var SelectedData = JSON.stringify({
        'Status': "CHECK",
    });
    $.ajax({
        url: "/Students/Home/GoForAuction",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        data: SelectedData,
        beforeSend: function () {
            $(".Loader").show();
        },
        complete: function () {
            $(".Loader").hide();
        },
        success: function (data) {
            if (data == "1") {
                EnableGoForAuction();
            }
            if (data == "2") {
                EnableGoForAuction();
            }
            //$('#ModalBatchSuccess').show();
        }, async: false,
        error: OnError
    });
}
function GoForAuction() {
    if (confirm("Are you sure you want to continue...?") == true) {
        var SelectedData = JSON.stringify({
            'Status': "INSERT",
        });
        $.ajax({
            url: "/Students/Home/GoForAuction",
            method: 'POST',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            data: SelectedData,
            success: function (data) {
                if (data == 0) {
                    $('#SuccessHead').text('Add a keyword to send request for auction.');
                    $('#ModalSuccess').modal('show');
                    // alert('Successfully');
                    // EnableGoForAuction();
                }
                if (data == 1) {
                    $('#SuccessHead').text('RFA submitted successfully. Click anywhere on the page to continue.');
                    $('#ModalSuccess').modal('show');
                    // alert('Successfully');

                    EnableGoForAuction();
                }
                if (data == 2) {
                    $('#BatchSuccessHead').text("Auction request is not accepted.");
                    $('#SuccessHead').text('Already Request Sent');
                    $('#ModalSuccess').modal('show');
                    //alert('Already Request Sent');
                    EnableGoForAuction();
                }
                if (data == 3) {
                    //$('#BatchSuccessHead').text("Auction request is not accepted ");
                    //$('#SuccessHead').text('Already Request Sent');
                    //$('#ModalSuccess').modal('show');
                    ////alert('Already Request Sent');
                    //EnableGoForAuction();
                }
                //$('#ModalBatchSuccess').show();
            }, async: false,
            error: OnError
        });
    }
    return false;
}
//#endregion RFA


//#region Industry Report
function GetBatchDropdown() {
    debugger;
    $('#ddlBatchReport').empty();
    $.ajax({
        url: "/Students/Report/GetBatchDropdown",
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
        }, async: false,
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
        url: "/Students/Report/GetIndustryReportTopSearch",
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
        url: "/Students/Report/GetIndustryReportTopThemes",
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
function GetIndustryReportCampaignMatrix() {
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val()
    });
    $('#div_IndCampRpt').empty();
    $.ajax({
        //global: false,
        url: "/Students/Report/GetIndustryReportCampaignMatrix",
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
                 '<table id="tbl_resultIndCampRpt"><thead id="campaignMatrixHead">' +
                 '<tr>' +
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
                //htmlcontent += '<tr><td colspan="15" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var index = 0;
            $.each(data[0], function (i, item) {
                debugger;
                index = index + 1;
                htmlcontent += '<tr><td>' + index + '</td> ' +
                               '<td>' + item.accName + '</td> ' +
                               '<td>' + item.CampaignName + '</td>' +
                               '<td>' + item.Budget.toFixed(2) + '</td>' +
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
            $("#CapaignMatrixFooter").insertAfter("#campaignMatrixHead");
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
        url: "/Students/Report/GetIndustryReportTopAccount",
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
        url: "/Students/Report/GetIndustryReportRfaComparsnByAcc",
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
                     '<th>RFA</th>' +
                     '<th>Impressions</th>' +
                     '<th>Clicks</th>' +
                     '<th>Conversions</th>' +
                     '<th>Cost</th>' +
                     '<th>Cost/Conversion</th>' +
                     '<th>Cost/Click</th>' +
                     '<th>Impression Share</th>' +
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

                htmlAcccontent += '<td>' + "RFA " + rowSNo + " (" + item.RunOn + ' )</td>' +
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
        url: "/Students/Report/GetIndustryReportRfaComparsnByRfa",
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
                     '<th>RFA</th>' +
                     '<th>Account</th>' +
                     '<th>Impressions</th>' +
                     '<th>Clicks</th>' +
                     '<th>Conversions</th>' +
                     '<th>Cost</th>' +
                     '<th>Cost/Conversion</th>' +
                     '<th>Cost/Click</th>' +
                     '<th>Impression Share</th>' +
                     '</tr>';
            if (data == '') {
                htmlAcccontent += '<tr><td colspan="10" class="dataTables_empty">No Results Found</tr></td>';
                //htmlcontent += '<tr><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td><td>No Results Found</td></tr>';
            }
            var oldRfaId = '';
            var rowsNo = 0;
            var rowsName = 0;
            var rowSNo = 0;
            $.each(data[0], function (i, item) {
                debugger
                if (oldRfaId != item.RFA_Id) {
                    // oldRfaId = item.RFA_Id;
                    rowSNo = rowSNo + 1;
                }
            });
            $.each(data[0], function (i, item) {
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
                        rowsNo = rowsNo + 1;
                    });
                    htmlAcccontent += '<td rowspan="' + rowsNo + '"><a id="' + item.RFA_Id + '">' + "RFA " + rowSNo + " (" + item.RunOn + ' )</a></td>';
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
            url: "/Students/Report/GetIndustryReportRfaComparsnByGraph",
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
            url: "/Students/Report/GetIndustryReportRfaHistoriesByGraph",
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
    $('#ddlAccount').append($('<option selected></option>').val("").html("Account"));
    $.ajax({
        url: "/Students/Report/GetAccountDropdown?BatchCode=" + BatchCode + "",
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
        }, async: true,
        error: OnError
    });
}
function GetRfaListDropdown() {
    debugger;
    var SelectedData = JSON.stringify({
        'BatchCode': $('#ddlBatchReport').val(),
        'AccCode': $('#ddlAccount').val()
    });
    $('#ddlRfaList').empty();
    $('#ddlRfaList').append($('<option selected></option>').val("").html("Choose RFA"));
    $.ajax({
        url: "/Students/Report/GetIndustryReportRfaComparsnByGraph",
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
//#End Region



//#region Others

//#endregion Others


//#region CommonFunction
function CampaignDropdownChange(value) {
    if ($('#ddlUniqueCampaign').prop('selectedIndex') != 0) {
        $("#ddlUniqueAdGroups").css("pointer-events", "");
        $("#ddlUniqueAdGroups").css("opacity", "");
    }
    else {
        $("#ddlUniqueAdGroups").css("pointer-events", "none");
        $("#ddlUniqueAdGroups").css("opacity", "0.6");
    }
    BindAdGroupsOnlyDropdown(value, '#ddlUniqueAdGroups');

    BindStudentCampaignTab();
}
function BindAdGroupsOnlyDropdown(cCode, ctrl) {
    $(ctrl).empty();
    $(ctrl).append($('<option></option>').val("ALL").html("ALL Ad Groups"));
    if (cCode != 'ALL') {
        $.ajax({
            url: "/Students/Home/GetADGroupsDataDistinct?cCode=" + cCode + "",
            method: 'GET',
            success: function (data) {
                $.each(data, function (i, item) {
                    $(ctrl).append($('<option></option>').val(item.AdGroupCode).html(item.AdGroupName))
                });
            }, async: false,
            error: OnError
        });
    }
}
function getAllCamp() {
    var htmlcontent = "";
    $('#ddlCampaignADGM').empty();
    $('#ddlCampaignAD').empty();
    $('#ddlCampKey').empty();

    $('#ddlCampaignADGM').append($('<option></option>').val("ALL").html("Select Campaign"));
    $('#ddlCampaignAD').append($('<option></option>').val("ALL").html("Select Campaign"));
    $('#ddlCampKey').append($('<option></option>').val("ALL").html("Select Campaign"));
    $.ajax({
        url: "/Students/Home/GetCampaigns",
        method: 'GET',
        success: function (data) {
            $.each(data, function (i, item) {
                $('#ddlCampaignADGM').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
                $('#ddlCampaignAD').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
                $('#ddlCampKey').append($('<option></option>').val(item.CampaignCode).html(item.CampaignName));
            });
        }, async: false,
        error: OnError
    });
}
function getAdGroupsOnly(cCode, ctrl) {
    $(ctrl).empty();
    $(ctrl).append($('<option></option>').val("ALL").html("Select Ad Groups"));
    $.ajax({
        url: "/Students/Home/GetADGroupsDataDistinct?cCode=" + cCode + "",
        method: 'GET',
        success: function (data) {


            $.each(data, function (i, item) {


                $(ctrl).append($('<option></option>').val(item.AdGroupCode).html(item.AdGroupName))



            });
        }, async: false,
        error: OnError
    });
} //Ad Group dropdownlist of ads pop up
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
function isInteger(x) { return typeof x === "number" && isFinite(x) && Math.floor(x) === x; }
function isFloat(x) { return !!(x % 1); }
//#endregion CommonFunction

/*end Working code*/


//#region UseFulCode But Unused
function fnExcelReport(htmlcontent, Name) {

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
    $('#excelCampaignName').text($('#ddlUniqueCampaign option:selected').text());
    $('#excelAdGroupName').text($('#ddlUniqueAdGroups option:selected').text());
    $('#hdnexcelCampaignValue').val($('#ddlUniqueCampaign').val());
    $('#hdnexcelAdGroupValue').val($('#ddlUniqueAdGroups').val());
    ClearExcelPopup();
    ShowPopup();
}
function ExcelValidation() {
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
            if ($('#hdnIdenType').val() == 'KEYPLAN') {
                return SaveKeyPlanExcel();
            }
            if ($('#hdnIdenType').val() == 'GET_KEYPLAN') {
                return getKeyPlanExcel();
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
//#endregion UseFulCode But Unused



//#region Unused Code
function unusedCode() {
    //function KeyPlanDownloadExcel() {

    //    var htmlcontent = "";
    //    //var SelectedData = JSON.stringify({
    //    //    'CampaignCode': $('#ddlUniqueCampaign').val(),
    //    //    'AdGroupCode': $('#ddlUniqueAdGroups').val(),
    //    //});
    //    $.ajax({
    //        url: "/Admin/Manage/getKeywordsOnDemand",
    //        method: 'POST',
    //        cache: false,
    //        contentType: 'application/json; charset=utf-8',
    //        dataType: "json",
    //        success: function (data) {
    //            htmlcontent =
    //                '<table border="2px" id="tempTablePhrase">' +
    //                '<caption>All Keyword List</caption><colgroup align="left"></colgroup><colgroup span="5" align="left"></colgroup>' +
    //                                '<thead><tr>' +
    //                                '<th>Index</th>' +
    //                                '<th>Keyword Name</th>' +
    //                                '<th>Bid</th>' +
    //                                '</thead></tr><tbody>';

    //            if (data == '') {
    //                htmlcontent += '<tr><td colspan="5">No Results Found</tr></td>';
    //            }
    //            $.each(data, function (i, item) {

    //                var index = i + 1;
    //                htmlcontent += '<tr><td>' + index + '</td>' +
    //                '<td>' + item.KeyName + '</td>' +
    //                                '<td>' + item.SuggestedBid + '</td>';
    //            });
    //        }, async: false,

    //        error: function (data) {
    //            alert("Either internet slow or Internal Error");
    //        }
    //    });
    //    htmlcontent += '</tbody></table>';
    //    fnExcelReport(htmlcontent, "Download_KeywordPlanner");
    //}
    //function KeyPlanCampaignDropdownChange(value) {
    //    //BindAdGroupsOnlyDropdown(value, '#ddlUniqueAdGroups');
    //    getKeywordPlannerOnDemand();
    //}
    //function KeyPlanAdGroupDropdownChange() {
    //    getKeywordPlannerOnDemand();
    //}
}
//#endregion Unused Code


function setDropdown(gg) {
    alert(gg);
    //$('#ddlUniqueCampaign').val(gg);
}
function getKeywordPlannerOnDemand() {
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'CampaignCode': null,
        'AdGroupCode': null,
    });
    $('#Kewords_Result').empty();
    $.ajax({
        url: "/Students/Home/getMappedkeyWordsOnDemand",
        method: 'POST',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: SelectedData,
        dataType: "json",
        success: function (data) {

            htmlcontent = '<div class="row">' +

                               '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table id="tableKeyPlan">' +
                                '<thead><tr>' +
                                '<th>Campaign</th>' +
                                '<th>Ad Group </th>' +
                                '<th>Keyword</th>' +
                                //'<th>Impressions</th>' +
                                //'<th>Clicks</th>' +
                                //'<th>CTR</th>' +
                                //'<th>Cost</th>' +
                                //'<th>Avg CPC</th>' +
                                //'<th>Conversions</th>' +
                                //'<th>CPC</th>' +
                                 '<th>Bid</th>' +
                                '</tr></thead><tbody>';


            $.each(data, function (i, item) {
                //
                htmlcontent += ' <tr>' +
                                            '<td>' + item.CampaignName + '</td>' +
                                            '<td>' + item.AdGroupName + '</td>' +
                                            '<td>' + item.KeyName + '</td>' +
                                            //'<td></td>' +
                                            //'<td>' + item.Impressions + '</td>' +
                                            //'<td>' + item.Clicks + '</td>' +
                                            //'<td>' + item.CTR + '</td>' +
                                            //'<td>' + item.COST + '</td>' +
                                            //'<td>' + item.CPC + '</td>' +
                                            //'<td>' + item.Conversions + '</td>' +
                                            //'<td>' + item.CPC + '</td>' +
                                            '<td>' + item.NewBid + '</td>' +
                                            ' </tr>';
            });
        }, async: false,
        error: OnError
    });
    htmlcontent += '</tbody></table></div></div></div>';
    $('#Kewords_Result').html(htmlcontent);
    KeyPlanDataTable();
}
function SaveKeyPlanExcel() {
    var hjjh = $('#excelUpload')[0];
    hjjh = $('#excelUpload');
    var formData = new FormData($('#excelUpload')[0]);
    var UpdateBidStatus = document.getElementById('UpdateBid').checked;
    formData.append("UpdateBidStatus", UpdateBidStatus);
    $.ajax({
        url: '/Students/Home/SaveKeyPlanExcel',  //Server script to process data
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
            getKeywordPlannerOnDemand();
            CloseExcelPopup(data);

        }, async: true,
        error: OnError
    });
    return false;
}
function getKeyPlanExcel() {
    if ($('#keywordIdeas').val().length == 0) {
        $('#uploadLabelValidation').text("Please enter keyword ideas.");
        $('#keywordIdeas').focus();
        //$('#divExcelNotify1').show();
        return false;
    }
    else {
        var SelectedData = JSON.stringify({
            'KeywordIdeas': $('#keywordIdeas').val(),
            'excludeKeyword': $('#excludeKeyword').val(),
        });
        $.ajax({
            url: '/Students/Home/GetKeyPlanExcel',  //Server script to process data
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: SelectedData,
            dataType: "json",
            //Options to tell jQuery not to process data or worry about content-type.
            cache: false,
            beforeSend: function () {
                $(".Loader").show();
            },
            complete: function () {
                $(".Loader").hide();
            },
            success: function (data) {
                $(".Loader").hide();
                alert(data);
                $('#uploadLabelValidation').text(data);
                ShowGetKeyPlanExcel();
                //$('#divExcelNotify1').show();
                //DownloadGetKeyPlanExcel.click();
            }, async: true,
            error: OnError
        });
        function OnError(xhr, errorType, exception) {
            debugger;
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
}
function getKeywordPlannerImportCount(Status) {
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
        'Status': Status,
    });
    $('#KwrdPlnrImprtErrHist').empty();
    $.ajax({
        url: "/Students/Home/getKeywordPlannerImportCount",
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
                                '<table id="KeywrdPlnrErrTable">' +
                               '<thead id="KeywrdPlnrErrHead"><tr>' +
                                    '<th>Row No</th>' +
                                    '<th>Campaign Name</th>' +
                                    '<th>Ad group Name</th>' +
                                    '<th>Keyword</th>' +
                                    '<th>Bid</th>' +
                                   '</tr></thead><tbody id="KeywrdPlnrErrBody">';


            $.each(data, function (i, item) {
                debugger;
                htmlcontent += '<td>' + item.RowNo + '</td>' +
                                            '<td>' + item.CampaignName + '</td>' +
                                            '<td>' + item.AdgroupName + '</td>' +
                                            '<td>' + item.KeywordName + '</td>' +
                                            '<td>' + item.Bid + '</td>' +
                                            ' </tr>';
            });
            htmlcontent += '</tbody></table></div></div></div>';
            $('#KwrdPlnrImprtErrHist').html(htmlcontent);
            //ApplyDataTable("KeywordTable", "12");
        }, async: false,
        error: OnError
    });
}
function ShowGetKeyPlanExcel() {
    var htmlcontent = "";
    var SelectedData = JSON.stringify({
    });
    $('#ShowGetKeyPlanExcel').empty();
    $('#ShowGetKeyPlanExcel').empty('wngfklmewf.ew');

    $.ajax({
        //global: false,
        url: "/Students/Home/ShowGetKeyPlanExcel",
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
            var result = $.parseJSON(data);
            if (result != '') {
                htmlcontent = '<div class="download"><a class="fa-download-icon" id="DownloadGetKeyPlanExcel" href="/Students/Home/DownloadGetKeyPlanCSV"><i class="fa fa-download" area-hidden="true"></i>  Download</a></div>';

            } htmlcontent += '<div class="row">' +
            '<div class="col-md-12">' +
                                '<div class="campaings-table-border table-responsive">' +
                                '<table id="tableGetKeyPlanExcel">' +
                                '<thead><tr>' +
                                '<th></th>' +
                                '<th>Uploaded Keyword Idea</th>' +
                                '<th>Search Query</th>' +
                                '<th>Suggested Bid</th>' +
                                '<th>Traffic</th>' +

                                ' </thead></tr>';
            $.each(result, function (i, item) {
                var index = i + 1;
                htmlcontent += '<tr><td></td>' +
                    '<td>' + item.UploadedkeywordIdea + '</td>' +
                      '<td>' + item.DBKeyName + '</td>' +
                        '<td>' + item.SuggestedBid + '</td>' +
                          '<td>' + item.AvgMonthlySrch + '</td>';

            });
            htmlcontent += '</table></div></div></div>';
            $('#ShowGetKeyPlanExcel').html(htmlcontent);
            $('#ShowGetKeyPlanExcel').show();
            ApplyDataTable("tableGetKeyPlanExcel", "0");
        }, async: true,
        error: OnError
    });
}
function OldApplyDataTable(id) {
    $('#' + id).dataTable({
        "searching": false,
        //scrollY: '50vh',
        //scrollCollapse: true,
        //scrollY: '50vh',
        //scrollCollapse: true,
        //order: [],
        //columnDefs: [{ orderable: false, targets: [0] }],
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
function ApplyDataTable(id, disableSorting) {
    var t = $('#' + id).DataTable({
        "searching": false,
        responsive: true,
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
        //var newTitle = $(xhr.responseText).filter('title').text();
        //$("#dialog").append("<div>" + newTitle + "</div>");

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

