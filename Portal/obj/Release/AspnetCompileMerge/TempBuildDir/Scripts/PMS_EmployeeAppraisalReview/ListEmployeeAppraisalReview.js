
$(document).ready(function () {
    SearchEmployeeAppraisalReview();
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
    $("#txtEmployeeName").val("");
    $("#ddlStatus").val("");
 
    
}
function SearchEmployeeAppraisalReview() {
    var txtEmployeeName = $("#txtEmployeeName");
    var ddlStatus = $("#ddlStatus");
    

    var requestData = {
        employeeName: txtEmployeeName.val().trim(),
        pmsFinalStatus: ddlStatus.val()
    };
    $.ajax({
        url: "../PMS_EmployeeAppraisalReview/GetEmployeeAppraisalReviewList",
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
