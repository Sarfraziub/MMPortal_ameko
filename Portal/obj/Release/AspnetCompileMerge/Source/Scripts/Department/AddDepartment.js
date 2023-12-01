$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode"); 
    var hdnDepartmentId = $("#hdnDepartmentId");
    if (hdnDepartmentId.val() != "" && hdnDepartmentId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        GetDepartmentDetail(hdnDepartmentId.val());
        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
            $("#chkStatus").attr('disabled', true);
            
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
    $("#txtDepartmentName").focus();



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


function GetDepartmentDetail(departmentId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Department/GetDepartmentDetail",
        data: { departmentId: departmentId },
        dataType: "json",
        success: function (data) {
            $("#txtDepartmentName").val(data.DepartmentName);
            $("#txtDepartmentCode").val(data.DepartmentCode);
            if (data.DepartmentStatus == true) {
                $("#chkStatus").attr("checked", true);
            }
            else {
                $("#chkStatus").attr("checked", false);
            }
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}

function SaveData() {
    var txtDepartmentName = $("#txtDepartmentName");
    var txtDepartmentCode = $("#txtDepartmentCode");
    var hdnDepartmentId = $("#hdnDepartmentId");
    var chkStatus = $("#chkStatus").is(':checked') ? true : false;

    if (txtDepartmentName.val().trim() == "") {
        ShowModel("Alert", "Please enter Department Name")
        txtDepartmentName.focus();
        return false;
    }
    if (txtDepartmentCode.val().trim() == "") {
        ShowModel("Alert", "Please enter Department Code")
        txtDepartmentCode.focus();
        return false;
    }
    var departmentViewModel = {
        DepartmentId: hdnDepartmentId.val(),
        DepartmentName: txtDepartmentName.val().trim(),
        DepartmentCode: txtDepartmentCode.val().trim(),
        DepartmentStatus: chkStatus
    };
    var accessMode = 1;//Add Mode
    if (hdnDepartmentId.val() != null && hdnDepartmentId.val() != 0) {
        accessMode = 2;//Edit Mode
    }
    var requestData = { departmentViewModel: departmentViewModel };
    $.ajax({
        url: "../Department/AddEditDepartment?AccessMode=" + accessMode + "",
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
                    window.location.href = "../Department/AddEditDepartment?accessMode=1";
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
    $("#hdnDepartmentId").val("0");
    $("#txtDepartmentName").val("");
    $("#txtDepartmentCode").val("");
    $("#chkStatus").prop("checked", true);

}
function ResetPage() {
    if (confirm("Are you sure to reset all fields?")) {
        window.location.href = "../Department/AddEditDepartment?accessMode=1";
    }
}