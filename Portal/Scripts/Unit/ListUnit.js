
$(document).ready(function () {

    BindBaseUnitList();

    setTimeout(
        function () {
             SearchUnits();
        }, 1000);

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
    $("#hdnUnitId").val("0");
    $("#txtUnitName").val("");
    $("#txtUnitShortName").val("");
    $("#ddlAllowDecimal").val("0");
    $("#txtUnitValue").val("");
    $("#chkIsAddMultipleLimit").attr("checked", false);
    $("#ddlBaseUnit").val("0");
}
function SearchUnits() {

    var txtUnitName = $("#txtUnitName").val();
    var txtUnitShortName = $("#txtUnitShortName").val();
    var ddlBaseUnit = $("#ddlBaseUnit").val();
    debugger
    var requestData = { unitName: txtUnitName, unitShortName: txtUnitShortName, parentId: parseInt(ddlBaseUnit)  };
    $.ajax({
        url: "../Unit/GetUnitList",
        data: requestData,
        dataType: "html",
        asnc: true,
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


function BindBaseUnitList() {

    $.ajax({
        type: "GET",
        url: "../Unit/GetBaseUnitList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlBaseUnit").append($("<option></option>").val(0).html("-Select Base Unit-"));
            $.each(data, function (i, item) {

                $("#ddlBaseUnit").append($("<option></option>").val(item.UnitId).html(item.UnitName + " (" + item.UnitShortName + ")"));
            });
        },
        error: function (Result) {
            $("#ddlBaseUnit").append($("<option></option>").val(0).html("-Select Base Unit-"));
        }
    });
}
function Export() {
    var divList = $("#divList");
    ExporttoExcel(divList);
}