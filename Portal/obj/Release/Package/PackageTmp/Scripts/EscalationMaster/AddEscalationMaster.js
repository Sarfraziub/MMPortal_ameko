$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode"); 
    var hdnEscalationMasterId = $("#hdnEscalationMasterId");
    if (hdnEscalationMasterId.val() != "" && hdnEscalationMasterId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        GetEscalationMasterDetail(hdnEscalationMasterId.val());
        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
            $("#chkstatus").attr('disabled', true);
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
//$(".alpha-only").on("keydown", function (event) {
//    // Allow controls such as backspace
//    var arr = [8, 16, 17, 20, 35, 36, 37, 38, 39, 40, 45, 46];

//    // Allow letters
//    for (var i = 65; i <= 90; i++) {
//        arr.push(i);
//    }

//    // Prevent default if not in array
//    if (jQuery.inArray(event.which, arr) === -1) {
//        event.preventDefault();
//    }
//});

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


function GetEscalationMasterDetail(escalationId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../EscalationMaster/GetEscalationMasterDetail",
        data: { EscalationId: escalationId },
        dataType: "json",
        success: function (data) {
            $("#ddlProcessType").val(data.ProcessType);
            $("#txtEscalationDays").val(data.EscalationDays);
            $("#hdnEscalationMasterId").val(data.EscalationId);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}

function SaveData() {
    var ddlProcessType = $("#ddlProcessType");
    var hdnEscalationMasterId = $("#hdnEscalationMasterId");
    var txtEscalationDays = $("#txtEscalationDays");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;

    if (ddlProcessType.val() == "0") {
        ShowModel("Alert", "Please select process type.")
        ddlProcessType.focus();
        return false;
    }

    if (txtEscalationDays.val().trim() == "") {
        ShowModel("Alert", "Please enter escalation days.")
        txtEscalationDays.focus();
        return false;
    }

    var escalationMasterViewModel = {
        EscalationId: hdnEscalationMasterId.val(),
        ProcessType: ddlProcessType.val(),
        EscalationDays: txtEscalationDays.val()
    };
    var requestData = { escalationMasterViewModel: escalationMasterViewModel };
    $.ajax({
        url: "../EscalationMaster/AddEditEscalationMaster",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                ClearFields();
                setTimeout(
               function () {
                   window.location.href = "../EscalationMaster/AddEditEscalationMaster";
               }, 2000);
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
    $("#hdnEscalationMasterId").val("0");
    $("#txtEscalationDays").val("");
    $("#ddlProcessType").val("0");

}
function stopRKey(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}
document.onkeypress = stopRKey;
