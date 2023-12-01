
$(document).ready(function () {
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
    SearchPO();
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
    $("#txtPONo").val("");
    $("#txtVendorName").val("");
    $("#txtRefNo").val("");
    $("#ddlApprovalStatus").val("Active");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    
}
function SearchPO() {
    var txtPONo = $("#txtPONo");
    var txtVendorName = $("#txtVendorName");
    var ddlApprovalStatus = $("#ddlApprovalStatus");
    var txtRefNo = $("#txtRefNo");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var approvalStatus = "0";
    if ($("#hdnApprovalStatus").val() != "0" && ddlApprovalStatus.val() == "0") {
        approvalStatus = $("#hdnApprovalStatus").val();
        ddlApprovalStatus.val(approvalStatus);
    }
    else {
        if (ddlApprovalStatus.val() == "Pending") {
            approvalStatus = "0";
            $("#hdnApprovalStatus").val("0");
        }
        else if (ddlApprovalStatus.val() == "Approved") {
            approvalStatus = ddlApprovalStatus.val();
            $("#hdnApprovalStatus").val("0");
        }
        else if (ddlApprovalStatus.val() == "Canceled") {
            approvalStatus = ddlApprovalStatus.val();
            $("#hdnApprovalStatus").val("0");
        }
        else if (ddlApprovalStatus.val() == "Recommendation") {
            approvalStatus = ddlApprovalStatus.val();
            $("#hdnApprovalStatus").val("0");
        }
        else {
            $("#hdnApprovalStatus").val("0");
        }
    }
    var requestData = { poNo: txtPONo.val().trim(), vendorName: txtVendorName.val().trim(), refNo: txtRefNo.val().trim(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), approvalStatus: approvalStatus };
    $.ajax({
        url: "../PO/GetApprovedPOList",
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
