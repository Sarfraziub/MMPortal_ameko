
$(document).ready(function () {
    BindCustomerTypeList();
    //SearchCustomer();
});
function BindCustomerTypeList() {
    $.ajax({
        type: "GET",
        url: "../Customer/GetCustomerTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlCustomerType").append($("<option></option>").val(0).html("-Select Customer Type-"));
            $.each(data, function (i, item) {

                $("#ddlCustomerType").append($("<option></option>").val(item.CustomerTypeId).html(item.CustomerTypeDesc));
            });
        },
        error: function (Result) {
            $("#ddlCustomerType").append($("<option></option>").val(0).html("-Select Customer Type-"));
        }
    });
}
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
    $("#txtCustomerName").val("");
    $("#txtCustomerCode").val("");

    $("#txtMobileNo").val("");
    $("#ddlCustomerType").val("0");
    $("#ddlStatus").val("");
}
function SearchCustomer() {
    var txtCustomerName = $("#txtCustomerName");
    var txtCustomerCode = $("#txtCustomerCode");

    var txtMobileNo = $("#txtMobileNo");
    var ddlCustomerType = $("#ddlCustomerType");

    var ddlStatus = $("#ddlStatus");

    var requestData = { customerName: txtCustomerName.val().trim(), customerCode: txtCustomerCode.val().trim(), mobileNo: txtMobileNo.val().trim(), customerTypeid: ddlCustomerType.val(), customerStatus: ddlStatus.val() };
    $.ajax({
        url: "../Customer/GetCustomerList",
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
    $('#tblCountryList').paging({
            limit: -1,
            rowDisplayStyle: 'block',
            activePage: 0,
            rows: []
        });

 
    var divList = $("#divList");
    ExporttoExcel(divList);
}
 