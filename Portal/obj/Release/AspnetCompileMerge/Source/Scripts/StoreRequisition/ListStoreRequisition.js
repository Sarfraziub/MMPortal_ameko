
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
    if (GetURLParamValue('request') != "" && GetURLParamValue('request') == "Super")
    {
        SearchStoreRequisition();
    }
   // SearchStoreRequisition();
    
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
    $("#txtWorkOrderNo").val("");
    $("#txtRequisitionNo").val("");
    $("#ddlCompanyBranch").val("0");
    $("#txtCustomerName").val("");
    $("#ddlCompanyBranch").val("0");
    $("#ddlRequisitionStatus").val("0");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    $("#ddlRequisitionType").val("0");
    $("#ddlApprovalStatus").val("0");
    
}
function SearchStoreRequisition() {
    var txtWorkOrderNo = $("#txtWorkOrderNo");
    var txtRequisitionNo = $("#txtRequisitionNo");
    var ddlRequisitionType = $("#ddlRequisitionType");
    var ddlCompanyBranch = $("#ddlCompanyBranch");
    var txtCustomerName = $("#txtCustomerName");
    var ddlRequisitionStatus = $("#ddlRequisitionStatus");
    var ddlApprovalStatus = $("#ddlApprovalStatus");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var ApprovalStatus = "";
    if ($("#hdnApprovalStatus").val() != 0) {
        ApprovalStatus = $("#hdnApprovalStatus").val();
        //$("#ddlApprovalStatus").val(ApprovalStatus);
    }
    else {
        ApprovalStatus = ddlApprovalStatus.val();
        //$("#ddlApprovalStatus").val(0);
    }

    var requestData = { requisitionNo: txtRequisitionNo.val().trim(), workOrderNo: txtWorkOrderNo.val().trim(), requisitionType: ddlRequisitionType.val(), customerName: txtCustomerName.val().trim(), companyBranchId: ddlCompanyBranch.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), requisitionStatus: ddlRequisitionStatus.val(), approvalStatus: ApprovalStatus };
    $.ajax({
        url: "../StoreRequisition/GetStoreRequisitionList",
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

$('body').on('change', '#ddlApprovalStatus', function () {
    $("#hdnApprovalStatus").val(0);
});

function GetURLParamValue(name) {
    if (name != "" || name != null) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        return results[1] || 0;
    }
}
