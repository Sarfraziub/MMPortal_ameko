
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
    //SearchQuotation();
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
    $("#txtQuotationNo").val("");
    $("#txtCustomerName").val("");
    $("#ddlApprovalStatus").val("0");
    $("#txtRefNo").val("");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    
}
function SearchQuotation() {
    var txtQuotationNo = $("#txtQuotationNo");
    var txtVendorName = $("#txtVendorName");
    var ddlApprovalStatus = $("#ddlApprovalStatus");
    var txtRefNo = $("#txtRefNo");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var requestData = { quotationNo: txtQuotationNo.val().trim(), vendorName: txtVendorName.val().trim(), refNo: txtRefNo.val().trim(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), approvalStatus: ddlApprovalStatus.val() };
    $.ajax({
        url: "../PurchaseQuotation/GetPurchaseQuotationList",
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

function OpenPurchaseIndentSearchPopup() {
    $("#SearchPurchaseIndentModel").modal();
    BindSearchCompanyBranchList();
}

function BindSearchCompanyBranchList() {
    $("#ddlSearchCompanyBranch").val(0);
    $("#ddlSearchCompanyBranch").html("");
    $.ajax({
        type: "GET",
        url: "../DeliveryChallan/GetCompanyBranchList",
        data: {},
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlSearchCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
            $.each(data, function (i, item) {
                $("#ddlSearchCompanyBranch").append($("<option></option>").val(item.CompanyBranchId).html(item.BranchName));
            });
        },
        error: function (Result) {
            $("#ddlSearchCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
        }
    });
}

function SelectPurchaseIndent(purchaseIndentId, purchaseIndentNo, purchaseIndentDate) {
    $("#txtPurchaseIndentNo").val(purchaseIndentNo);
    $("#hdnPurchaseIndentId").val(purchaseIndentId);
    $("#txtPurchaseIndentDate").val(purchaseIndentDate);

    $("#SearchPurchaseIndentModel").modal('hide');
}

function SearchPurchaseIndent() {
    var txtIndentNo = $("#txtIndentNo");
    var ddlIndentType = $("#ddlIndentType");
    var txtCustomerName = $("#txtCustomerName");
    var ddlSearchCompanyBranch = $("#ddlSearchCompanyBranch");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var requestData = { indentNo: txtIndentNo.val().trim(), indentType: ddlIndentType.val(), customerName: txtCustomerName.val().trim(), companyBranchId: ddlSearchCompanyBranch.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), displayType: "Popup", approvalStatus: "Final" };
    $.ajax({
        url: "../PurchaseQuotation/GetPurchaseIndentList",
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