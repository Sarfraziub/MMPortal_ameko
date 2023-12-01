
$(document).ready(function () {
    SearchInterviews();
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

function BindPositionLevelList() {
    $("#ddlPositionLevel").val(0);
    $("#ddlPositionLevel").html("");
    $.ajax({
        type: "GET",
        url: "../ResourceRequisition/GetPositionLevelList",
        data: {},
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlPositionLevel").append($("<option></option>").val(0).html("-Select Position Level-"));
            $.each(data, function (i, item) {
                $("#ddlPositionLevel").append($("<option></option>").val(item.PositionLevelId).html(item.PositionLevelName));
            });
        },
        error: function (Result) {
            $("#ddlPositionLevel").append($("<option></option>").val(0).html("-Select Position Level-"));
        }
    });
}



function ClearFields() {
   
    $("#txtApplicantNo").val("");
    $("#txtInterviewNo").val("0");
    $("#txtInterviewFinalStatus").val("");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
}
function SearchInterviews() {
    var txtApplicantNo = $("#txtApplicantNo");
    var txtInterviewNo = $("#txtInterviewNo");
    var ddlInterviewFinalStatus = $("#ddlInterviewFinalStatus");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var requestData = {
        interviewNo: txtInterviewNo.val().trim(), applicantNo: txtApplicantNo.val(), interviewFinalStatus: ddlInterviewFinalStatus.val(),fromDate: txtFromDate.val(), toDate: txtToDate.val()  };
    $.ajax({
        url: "../Interview/GetInterviewList",
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