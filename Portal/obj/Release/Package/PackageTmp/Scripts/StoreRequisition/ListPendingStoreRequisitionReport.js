
$(document).ready(function () {
    GenerateReportParameters();
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
    $("#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {
            GenerateReportParameters();
        }
    });
    //SearchStoreRequisitionForMRN();
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
    $("#txtRequisitionNo").val("");
    $("#txtCustomerName").val("");
    $("#ddlCompanyBranch").val("0");
    $("#ddlRequisitionStatus").val("0");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    $("#ddlRequisitionType").val("0");
}
function SearchStoreRequisitionForMRN() {
    var txtRequisitionNo = $("#txtRequisitionNo");
    var ddlRequisitionType = $("#ddlRequisitionType");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");

    var requestData = {
        requisitionNo: txtRequisitionNo.val().trim(),
        requisitionType: ddlRequisitionType.val(),
        fromDate: txtFromDate.val(),
        toDate: txtToDate.val()
    };
    $.ajax({
        url: "../StoreRequisition/GetStoreRequisitionListForMRN",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divListForMRN").html("");
            $("#divListForMRN").html(err);
        },
        success: function (data) {
            $("#divListForMRN").html("");
            $("#divListForMRN").html(data);
        }
    });
}

function GenerateReportParameters() {

    var url = "../StoreRequisition/GetPendingStoreRequisitionsReport?requisitionNo=" + $("#txtRequisitionNo").val() + "&requisitionType=" + $("#ddlRequisitionType").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#lnkGenerateReport').attr({
        href: url,
        target: '_blank'
    });
   
}

$('body').on('change', '#txtRequisitionNo', function () {
    GenerateReportParameters();
});
$('body').on('change', '#ddlRequisitionType', function () {
    GenerateReportParameters();
});