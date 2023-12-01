
$(document).ready(function () {
    //$("#txtFromDate").attr('readOnly', true);
    //$("#txtToDate").attr('readOnly', true);
    //$("#txtFromDate,#txtToDate").datepicker({
    //    changeMonth: true,
    //    changeYear: true,
    //    dateFormat: 'dd-M-yy',
    //    yearRange: '-10:+100',
    //    onSelect: function (selected) {

    //    }
    //});
    SearchPendingStoreRequisition();
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
function SearchPendingStoreRequisition() {
    var hdnStoreRequisitionNo = $("#hdnStoreRequisitionNo");
    //var txtRequisitionNo = $("#txtRequisitionNo");

    //if (txtRequisitionNo.val() == "")
    //{
    //    txtRequisitionNo.notify("Please enter requisition no.!");
    //    txtRequisitionNo.focus();
    //    return false;
    //}
    var requestData = {
        requisitionNo: hdnStoreRequisitionNo.val().trim()
    };
    $.ajax({
        url: "../StoreRequisition/GetPendingStoreRequisitionList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divPendingStoreRequisitionList").html("");
            $("#divPendingStoreRequisitionList").html(err);
        },
        success: function (data) {
            $("#divPendingStoreRequisitionList").html("");
            $("#divPendingStoreRequisitionList").html(data);
        }
    });
}
function Export() {
    var divList = $("#divPendingStoreRequisitionList");
    ExporttoExcel(divList);
}