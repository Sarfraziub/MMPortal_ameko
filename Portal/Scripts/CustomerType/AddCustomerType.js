$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode"); 
    var hdnCustomerTypeId = $("#hdnCustomerTypeId");
    if (hdnCustomerTypeId.val() != "" && hdnCustomerTypeId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        GetCustomerTypeDetail(hdnCustomerTypeId.val());
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
    $("#txtCustomerTypeDesc").focus();



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


function GetCustomerTypeDetail(customertypeId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../CustomerType/GetCustomerTypeDetail",
        data: { customertypeId: customertypeId },
        dataType: "json",
        success: function (data) {
            $("#txtCustomerTypeDesc").val(data.CustomerTypeDesc);
            if (data.CustomerType_Status == true) {
                $("#chkstatus").attr("checked", true);
            }
            else {
                $("#chkstatus").attr("checked", false);
            }
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}

function SaveData() {
    var txtCustomerTypeDesc = $("#txtCustomerTypeDesc");
    var hdnCustomerTypeId = $("#hdnCustomerTypeId");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;

    if (txtCustomerTypeDesc.val().trim() == "") {
        ShowModel("Alert", "Please enter  Customer Type Desc")
        txtCustomerTypeDesc.focus();
        return false;
    }

    var customertypeViewModel = {
        CustomerTypeId: hdnCustomerTypeId.val(),
        CustomerTypeDesc: txtCustomerTypeDesc.val().trim(),
        CustomerType_Status: chkstatus
    };
    var requestData = { customertypeViewModel: customertypeViewModel };
    $.ajax({
        url: "../CustomerType/AddEditCustomerType",
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
                   window.location.href = "../CustomerType/AddEditCustomerType";
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
    $("#hdnCustomerTypeId").val("0");
    $("#txtCustomerTypeDesc").val("");
    $("#chkstatus").prop("checked", true);

}
function stopRKey(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}
document.onkeypress = stopRKey;
