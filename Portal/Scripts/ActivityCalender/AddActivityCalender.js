$(document).ready(function () {
    $("#txtActivityDate").attr('readOnly', true);
    
    BindCalenderList();
    $("#txtActivityDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        minDate: '0',
        onSelect: function (selected) {
        }
    });
    var hdnAccessMode = $("#hdnAccessMode"); 
    var hdnActivityCalenderId = $("#hdnActivityCalenderId");
    if (hdnActivityCalenderId.val() != "" && hdnActivityCalenderId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        GetActivityCalenderDetail(hdnActivityCalenderId.val());
        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
            $("#chkStatus").attr('disabled', true);
        }
        else {
            $("#btnSave").hide();
            $("#btnUpdate").show();
            $("#btnReset").show();
        }
    }
    else {
        $("#btnSave").show();
        $("#btnUpdate").hide();
        $("#btnReset").show();
    }
    $("#txtActivityDate").focus();
   
});
//$(".alpha-only").on("keydown", function (event) {
//    // Allow controls such as backspace
//    var arr = [8, 16, 17, 20, 35, 36, 37, 38, 39, 40, 45, 46];

//    // Allow letters
//    for (var i = 65; i <= 90; i++) {
//        arr.push(i);
//    }

//    // Prevent default if not in array
//    if (jQuery.inArray(event.which, arr) === -1) {
//        event.preventDefault();
//    }
//});


$(".alpha-only").on("input", function () {
    var regexp = /[^a-zA-Z]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});
$(".alpha-space-only").on("input", function () {
    var regexp = /[^a-zA-Z\s]+$/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});
$(".numeric-only").on("input", function () {
    var regexp = /\D/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});
$(".alpha-numeric-only").on("input", function () {
    var regexp = /[^a-zA-Z0-9]/g;
    if ($(this).val().match(regexp)) {
        $(this).val($(this).val().replace(regexp, ''));
    }
});
 
function BindCalenderList() {
    $.ajax({
        type: "GET",
        url: "../ActivityCalender/GetCalenderList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) { 
            $("#ddlCalender").append($("<option></option>").val(0).html("-Select Calender-"));
            $.each(data, function (i, item) {
                $("#ddlCalender").append($("<option></option>").val(item.CalenderId).html(item.CalenderName));
            });
        },
        error: function (Result) {
            $("#ddlCalender").append($("<option></option>").val(0).html("-Select Calender-"));
        }
    });
}

function GetActivityCalenderDetail(activitycalenderId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../ActivityCalender/GetActivityCalenderDetail",
        data: { activitycalenderId: activitycalenderId },
        dataType: "json",
        success: function (data) {
            $("#txtActivityDate").val(data.ActivityDate);
            $("#txtActivityDescription").val(data.ActivityDescription);
            $("#ddlCalender").val(data.CalenderId);
            if (data.ActivityStatus == true) { 
                $("#chkStatus").attr("checked", true);
            }
            else {
                $("#chkStatus").attr("checked", false);
            }
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}

function SaveData() {
    var txtActivityDate = $("#txtActivityDate");
    var txtActivityDescription = $("#txtActivityDescription");
    var ddlCalender = $("#ddlCalender");
    var hdnActivityCalenderId = $("#hdnActivityCalenderId");
    var chkStatus = $("#chkStatus").is(':checked') ? true : false;

    if (txtActivityDate.val() == "") {
        ShowModel("Alert", "Please select Activity Date")
        txtActivityDate.focus();
        return false;
    } 
    if (ddlCalender.val() == 0) {
        ShowModel("Alert", "Please Select Calender Id")
        ddlDepartment.focus();
        return false;
    }
    
    var activitycalenderViewModel = {
        ActivityCalenderId: hdnActivityCalenderId.val(),
        ActivityDate: txtActivityDate.val(),
        ActivityDescription: txtActivityDescription.val().trim(),
        CalenderId: ddlCalender.val().trim(),
        ActivityStatus: chkStatus
    };
    var accessMode = 1;//Add Mode
    if (hdnActivityCalenderId.val() != null && hdnActivityCalenderId.val() != 0) {
        accessMode = 2;//Edit Mode
    }
    var requestData = { activitycalenderViewModel: activitycalenderViewModel };
    $.ajax({
        url: "../ActivityCalender/AddEditActivityCalender?AccessMode=" + accessMode + "",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                ClearFields();
                setTimeout(
         function () {
             window.location.href = "../ActivityCalender/AddEditActivityCalender?accessMode=1";
         }, 2000);
                $("#btnSave").show();
                $("#btnUpdate").hide();
            }
            else {
                ShowModel("Error", data.message)
            }
        },
        error: function (err) {
            ShowModel("Error", err)
        }
    });

}
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);
}
function ClearFields() {
    $("#hdnActivityCalenderId").val("0");
    $("#txtActivityDate").val($("#hdnCurrentDate").val());
    $("#txtActivityDescription").val("");
    $("#ddlCalender").val("0");
    $("#chkStatus").prop("checked", true);

}
function ResetPage() {
    if (confirm("Are you sure to reset all fields?")) {
        window.location.href = "../ActivityCalender/AddEditActivityCalender?accessMode=1";
    }
}