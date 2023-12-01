$(document).ready(function () {  
    $("#txtApplicationNo").attr('readOnly', true);   
    $("#txtApplicationDate").attr('readOnly', true);
    $("#txtRejectedReason").attr("disabled", true);
    $("#txtApplicationDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    }); 
    $("#txtEmployee").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Employee/GetEmployeeAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: {
                    term: request.term, departmentId: $("#txtEmployee").val(), designationId: $("#txtEmployee").val()
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.FirstName, value: item.EmployeeId, EmployeeCode: item.EmployeeCode, MobileNo: item.MobileNo
                        };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtEmployee").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtEmployee").val(ui.item.label);
            $("#hdnEmployeeId").val(ui.item.value);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtEmployee").val("");
                $("#hdnEmployeeId").val("0");
                ShowModel("Alert", "Please select Employee from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + " || " + item.EmployeeCode + "</b><br>" + item.MobileNo + "</div>")
      .appendTo(ul);
};


    BindEmployeeClaimApplicationTypeList();


    var hdnAccessMode = $("#hdnAccessMode");
    var hdnApplicationId = $("#hdnApplicationId");
    if (hdnApplicationId.val() != "" && hdnApplicationId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetEmployeeClaimApplicationDetail(hdnApplicationId.val());
       }, 2000);

        

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
 
function BindEmployeeClaimApplicationTypeList() {
    $.ajax({
        type: "GET",
        url: "../EmployeeClaimApplication/GetEmployeeClaimApplicationTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlClaimType").append($("<option></option>").val(0).html("Select Claim Type"));
            $.each(data, function (i, item) {
                $("#ddlClaimType").append($("<option></option>").val(item.ClaimTypeId).html(item.ClaimTypeName));
            });
        },
        error: function (Result) {
            $("#ddlClaimType").append($("<option></option>").val(0).html("Select Claim Type"));
        }
    });
}
   
function GetEmployeeClaimApplicationDetail(applicationId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../EmployeeClaimApplication/GetEmployeeClaimApplicationDetail",
        data: { applicationId: applicationId },
        dataType: "json",
        success: function (data) {
            $("#txtApplicationNo").val(data.ApplicationNo);
            $("#hdnApplicationId").val(data.ApplicationId);
            $("#txtApplicationDate").val(data.ApplicationDate);
            $("#ddlClaimStatus").val(data.ClaimTypeId);
            $("#txtEmployee").val(data.EmployeeName);
            $("#hdnEmployeeId").val(data.EmployeeId);
            $("#txtClaimReason").val(data.ClaimReason);
            $("#txtClaimAmount").val(data.ClaimAmount);
            $("#ddlClaimStatus").val(data.ClaimStatus);
            if (data.RejectReason != "") {
                $("#divReject").show();
                $("#txtRejectedReason").val(data.RejectReason);
                $("#txtRejectedDate").val(data.RejectDate);
            }
            $("#btnAddNew").show();


        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}

function SaveData() {

    var hdnApplicationId = $("#hdnApplicationId");
    var ddlClaimStatus = $("#ddlAssetStatus");
    var txtRejectedReason = $("#txtRejectedReason");
    if (ddlClaimStatus.val() == "0" || ddlClaimStatus.val() == undefined) {
        ShowModel("Alert", "Please Enter Asset Status")
        return false;

    }
    if (ddlClaimStatus.val() == "Rejected") {
        if (txtRejectedReason.val() == "") {
            ShowModel("Alert", "Please Enter Loan Reason")
            return false;

        }
    }

    var employeeClaimApplicationViewModel = {
        ApplicationId: hdnApplicationId.val(),
        ClaimStatus: ddlClaimStatus.val(),
        RejectReason: txtRejectedReason.val()
    };       
    var requestData = { employeeClaimApplicationViewModel: employeeClaimApplicationViewModel };
    $.ajax({
        url: "../EmployeeClaimApplication/ApprovalRejectedEmployeeClaimApplication",
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
                       window.location.href = "../EmployeeClaimApplication/ListEmployeeClaimApplicationApproval";
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
    $("#txtApplicationNo").val("");
    $("#hdnApplicationId").val("0");
    $("#txtApplicationDate").val("");
    $("#ddlAssetType").val("0");
    $("#hdnEmployeeId").val("0");
    $("#txtEmployee").val("");   
    $("#txtAssetReason").val("");
    $("#ddlAssetStatus").val("Final");
    $("#btnSave").show();
    $("#btnUpdate").hide();


}
 
function EnableDisableRejectReason() {
    var approvalStatus = $("#ddlAssetStatus option:selected").text();
    if (approvalStatus == "Rejected") {
        $("#txtRejectedReason").attr("disabled", false);
    }
    else {
        $("#txtRejectedReason").attr("disabled", true);
        $("#txtRejectedReason").val("");
    }
}
 
 
 