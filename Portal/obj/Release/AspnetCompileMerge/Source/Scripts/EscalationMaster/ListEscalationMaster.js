$(document).ready(function () {
    //$('#tblCompanyList').paging({ limit: 2 });
    SearchEscalationMaster();
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
    $("#txtCustomerTypeDesc").val("");
    $("#ddlStatus").val("");
}

function SearchEscalationMaster() {
    var ddlProcessType = $("#ddlProcessType");
    var EscalationProcessType = "";
    if (ddlProcessType.val() == "0") {
        EscalationProcessType = "";
    }
    else {
        EscalationProcessType = ddlProcessType.val();
    }
    var requestData = {
        processType: EscalationProcessType
    };

    $.ajax({
        url: "../EscalationMaster/GetEscalationMasterList",
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
