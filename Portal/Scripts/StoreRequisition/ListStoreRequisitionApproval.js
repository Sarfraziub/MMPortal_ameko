
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
    BindCompanyBranchList();
    SearchStoreRequisition();
    
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
   
    window.location.href = "../StoreRequisition/ListApprovedStoreRequisition";
                        
}

function SearchStoreRequisition() {
    var txtWorkOrderNo = $("#txtWorkOrderNo");
    var txtRequisitionNo = $("#txtRequisitionNo");
    var ddlRequisitionType = $("#ddlRequisitionType");
    var ddlCompanyBranch = $("#ddlCompanyBranch");
    var txtCustomerName = $("#txtCustomerName");
    var ddlApprovalStatus = $("#ddlApprovalStatus");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var requisitionType = "";
    var POType = "";
    if (ddlApprovalStatus.val() != "0") {
        requisitionType = ddlApprovalStatus.val();
    }
    if ($("#hdnRequisitionType").val() == 0) {
        requisitionType = ddlApprovalStatus.val();
    }
    //else if (ddlApprovalStatus.val() == "0") {
    //    requisitionType = ddlApprovalStatus.val();
    //}
    else {
        if ($("#hdnRequisitionType").val() != 0) {
            requisitionType = $("#hdnRequisitionType").val();
        }
        if ($("#hdnRequisitionStatus").val() != 0) {
            requisitionType = $("#hdnRequisitionStatus").val();
        }
    }

    if (ddlRequisitionType.val() != "0") {
        POType = ddlRequisitionType.val();
    }
    else if (ddlRequisitionType.val() == "0") {
        POType = ddlRequisitionType.val();
    }
    else {
        if ($("#hdnPOType").val() != 0) {
            POType = $("#hdnPOType").val();
        }
    }

    var requestData = { requisitionNo: txtRequisitionNo.val().trim(), workOrderNo: txtWorkOrderNo.val().trim(), requisitionType: POType, customerName: txtCustomerName.val().trim(), companyBranchId: ddlCompanyBranch.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), approvalStatus: requisitionType };
    $.ajax({
        url: "../StoreRequisition/GetStoreRequisitionApprovalList",
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

function BindCompanyBranchList() {
    $("#ddlCompanyBranch").val(0);
    $("#ddlCompanyBranch").html("");
    $.ajax({
        type: "GET",
        url: "../DeliveryChallan/GetCompanyBranchList",
        data: {},
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
            $.each(data, function (i, item) {
                $("#ddlCompanyBranch").append($("<option></option>").val(item.CompanyBranchId).html(item.BranchName));
            });
        },
        error: function (Result) {
            $("#ddlCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
        }
    });
}
$('body', '#ddlApprovalStatus', function () {
    $('#hdnRequisitionType').val('0');
});