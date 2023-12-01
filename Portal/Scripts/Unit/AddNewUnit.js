$(document).ready(function () {
    BindBaseUnitList();
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnUnitId = $("#hdnUnitId");
    $("#divMultipleUnits").hide();

    if (hdnUnitId.val() != "" && hdnUnitId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
            function () {
                GetUnitDetail(hdnUnitId.val());
            }, 2000);

        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();

        }
        else {
            $("#btnSave").hide();
            $("#btnUpdate").show();
            $("#btnReset").show();
        }
    }
    else {
        $("#btnSave").show();
        $("#btnUpdate").hide();
        $("#btnReset").show();
    }

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

function checkDec(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}



function ShowOtherUnits() {
    debugger

    var chkIsAddMultipleLimit = $("#chkIsAddMultipleLimit");

    if (chkIsAddMultipleLimit.prop("checked") == true) {
        $("#divMultipleUnits").show();
    }
    else {
        $("#divMultipleUnits").hide();
    }

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
function GetUnitDetail(unitId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Unit/GetUnitDetail",
        data: { unitId: unitId },
        dataType: "json",
        success: function (data) {
            $("#hdnUnitId").val(unitId);
            $("#txtUnitName").val(data.UnitName);
            $("#txtUnitShortName").val(data.UnitShortName);
            $("#ddlAllowDecimal").val(data.AllowDecimal);
            $("#txtUnitValue").val(data.UnitValue);
            $("#ddlBaseUnit").val(data.ParentId);
            $("#lblUnitShortName").html(data.UnitName);


            if (data.IsMultipleUnit) {
                $("#chkIsAddMultipleLimit").attr("checked", true);
                $("#divMultipleUnits").show();

            }
            else {
                $("#chkIsAddMultipleLimit").attr("checked", false);
                $("#divMultipleUnits").hide();
            }
            if (data.UnitName == "") {
                $("#btnRemoveImg").hide();
            }
            if (data.UnitName) {
                $("#btnRemoveImg").show();
            }
            var hdnAccessMode = $("#hdnAccessMode");
            if (hdnAccessMode.val() == "3") {
                $("#btnRemoveImg").hide();
            }

        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}
function SaveData() {
    var hdnUnitId = $("#hdnUnitId");
    var txtUnitName = $("#txtUnitName");
    var txtUnitShortName = $("#txtUnitShortName");
    var ddlAllowDecimal = $("#ddlAllowDecimal");
    var chkIsAddMultipleLimit = $("#chkIsAddMultipleLimit");
    var txtUnitValue = $("#txtUnitValue");
    var ddlBaseUnit = $("#ddlBaseUnit");

    if (txtUnitName.val().trim() == "") {
        ShowModel("Alert", "Please enter Unit Name")
        txtUnitName.focus();
        return false;
    }
    if (txtUnitShortName.val().trim() == "") {
        ShowModel("Alert", "Please enter Unit short name")
        txtUnitShortName.focus();
        return false;
    }
    if (ddlAllowDecimal.val() == "" || ddlAllowDecimal.val() == "0") {
        ShowModel("Alert", "Please select Allow decimal")
        ddlAllowDecimal.focus();
        return false;
    }

    var isMultipleLimit = true;
    if (chkIsAddMultipleLimit.prop("checked") == true) { isMultipleLimit = true; }
    else { isMultipleLimit = false; }


    var unitViewModel = {
        UnitId: hdnUnitId.val(), UnitName: txtUnitName.val().trim(), UnitShortName: txtUnitShortName.val().trim(),
        AllowDecimal: ddlAllowDecimal.val(), IsMultipleUnit: isMultipleLimit,
        UnitValue: txtUnitValue.val().trim(), ParentId: ddlBaseUnit.val()
    };
    var accessMode = 1;//Add Mode
    if (hdnUnitId.val() != null && hdnUnitId.val() != 0) {
        accessMode = 2;//Edit Mode
    }
    var requestData = { unitViewModel: unitViewModel };


    $.ajax({
        url: "../Unit/AddEditUnit",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                ClearFields();
                $("#btnSave").show();
                $("#btnUpdate").hide();

            }
            else {
                ShowModel("Error", data.message)
            }
        },
        error: function (err) {
            ShowModel("Error", err)
        }
    });


}
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);
}
function ClearFields() {
    $("#hdnUnitId").val("0");
    $("#txtUnitName").val("");
    $("#txtUnitShortName").val("");
    $("#ddlAllowDecimal").val("0");
    $("#txtUnitValue").val("");
    $("#chkIsAddMultipleLimit").attr("checked", false);
    $("#ddlBaseUnit").val("0");
}

function stopRKey(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}
document.onkeypress = stopRKey;

function ResetPage() {
    if (confirm("Are you sure to reset all fields?")) {
        window.location.href = "../Unit/AddEditUnit?accessMode=1";
    }
}
