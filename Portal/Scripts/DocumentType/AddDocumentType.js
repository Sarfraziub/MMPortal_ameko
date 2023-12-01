$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode"); 
    var hdnDocumentTypeId = $("#hdnDocumentTypeId");
    if (hdnDocumentTypeId.val() != "" && hdnDocumentTypeId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        GetDocumentTypeDetail(hdnDocumentTypeId.val());
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


function GetDocumentTypeDetail(documenttypeId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../DocumentType/GetDocumentTypeDetail",
        data: { documenttypeId: documenttypeId },
        dataType: "json",
        success: function (data) {
            $("#txtDocumentTypeDesc").val(data.DocumentTypeDesc);
            if (data.DocumentType_Status == true) {
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
    var txtDocumentTypeDesc = $("#txtDocumentTypeDesc");
    var hdnDocumentTypeId = $("#hdnDocumentTypeId");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;

    if (txtDocumentTypeDesc.val().trim() == "") {
        ShowModel("Alert", "Please enter Document Type Desc")
        txtDocumentTypeDesc.focus();
        return false;
    }

    var documenttypeViewModel = {
        DocumentTypeId: hdnDocumentTypeId.val(),
        DocumentTypeDesc: txtDocumentTypeDesc.val().trim(),
        DocumentType_Status: chkstatus
    };
    var requestData = { documenttypeViewModel: documenttypeViewModel };
    $.ajax({
        url: "../DocumentType/AddEditDocumentType",
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
                   window.location.href = "../DocumentType/AddEditDocumentType";
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
    $("#hdnDocumentTypeId").val("0");
    $("#txtDocumentTypeDesc").val("");
    $("#chkstatus").prop("checked", true);

}
function stopRKey(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}
document.onkeypress = stopRKey;