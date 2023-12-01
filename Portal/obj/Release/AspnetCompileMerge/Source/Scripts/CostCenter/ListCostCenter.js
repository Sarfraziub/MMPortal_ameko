$(document).ready(function () {
    //$('#tblCompanyList').paging({ limit: 2 });
    SearchCostCenter();
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
    $("#txtCostCenterName").val(""); 
    $("#ddlStatus").val("");
}

function SearchCostCenter() {
    var txtCostCenterName = $("#txtCostCenterName"); 
    var hdnCostCenterId = $("#hdnCostCenterId");
    var ddlStatus = $("#ddlStatus");
    var requestData = {
        CostCenterName: txtCostCenterName.val().trim(),
        Status: ddlStatus.val()
    };

    $.ajax({
        url: "../CostCenter/GetCostCenterList",
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