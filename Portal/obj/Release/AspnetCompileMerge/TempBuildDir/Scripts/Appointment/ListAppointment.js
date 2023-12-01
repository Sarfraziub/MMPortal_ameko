
$(document).ready(function () {
    SearchAppointments();
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
    $("#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    }); 
});
 
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

function ClearFields() {
    $("#txtApplicantNo").val("");
    $("#txtInterviewNo").val("0");
    $("#txtInterviewFinalStatus").val("");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
}
function SearchAppointments() {
    var txtAppointLetterNo = $("#txtAppointLetterNo");
    var txtInterviewNo = $("#txtInterviewNo");
    var txtApplicantName = $("#txtApplicantName");
    var ddlAppointStatus = $("#ddlAppointStatus");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var requestData = {
        appointLetterNo: txtAppointLetterNo.val().trim(), interviewNo: txtInterviewNo.val(), applicantName: txtApplicantName.val(), appointStatus: ddlAppointStatus.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val()
    };
    $.ajax({
        url: "../Appointment/GetAppointmentList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divList").html("");
            $("#divList").html(err);
        },
        success: function (data) {
            $("#divList").html("");
            $("#divList").html(data);
        }
    });
}
function Export() {
    var divList = $("#divList");
    ExporttoExcel(divList);
}