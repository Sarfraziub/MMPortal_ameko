
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
    //SearchCustomerSiteMRN();
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
    $("#txtCustomerSiteMRNNo").val("");
    $("#txtVendorName").val("");
    $("#txtDispatchRefNo").val("");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    
}
function SearchCustomerSiteMRN() {
    var txtCustomerSiteMRNNo = $("#txtCustomerSiteMRNNo");
    var txtVendorName = $("#txtVendorName");
    var ddlApprovalStatus = $("#ddlApprovalStatus");
    var txtDispatchRefNo = $("#txtDispatchRefNo");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var ddlSiteMRNType = $("#ddlSiteMRNType");

    var requestData = { customerSiteMrnNo: txtCustomerSiteMRNNo.val().trim(), vendorName: txtVendorName.val().trim(), dispatchrefNo: txtDispatchRefNo.val().trim(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), approvalStatus: ddlApprovalStatus.val(), isLocal: ddlSiteMRNType.val() };
    $.ajax({
        url: "../CustomerSiteMRN/GetCustomerSiteMRNList",
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
