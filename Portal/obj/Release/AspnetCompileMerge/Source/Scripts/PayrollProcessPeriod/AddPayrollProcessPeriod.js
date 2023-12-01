$(document).ready(function () {
 
    $("#txtPayrollProcessDate").attr('readOnly', true);
    $("#txtPayrollProcessingStartDate").attr('readOnly', true);
    $("#txtPayrollProcessingEndDate").attr('readOnly', true);

    $("#ddlPayrollProcessStatus").attr('disabled', true);
    $("#ddlPayrollLocked").attr('disabled', true);
    $("#txtPayrollLockDate").attr('readOnly', true);

    $("#txtCreatedBy").attr('readOnly', true);
    $("#txtCreatedDate").attr('readOnly', true);
    $("#txtModifiedBy").attr('readOnly', true);
    $("#txtModifiedDate").attr('readOnly', true);
    BindPayrollMonthList();

   
    
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnPayrollProcessingPeriodId = $("#hdnPayrollProcessingPeriodId");
    if (hdnPayrollProcessingPeriodId.val() != "" && hdnPayrollProcessingPeriodId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetPayrollProcessPeriodDetail(hdnPayrollProcessingPeriodId.val());
       }, 1000);

        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
            $("#chkstatus").attr('disabled', true);
            $(".editonly").hide();
            

        }
        else {
            $("#ddlMonth").attr('disabled', true);
            
            $("#btnSave").show();
            $(".editonly").show();
        }
    }
    else {
        $("#btnSave").show();
        $(".editonly").show();
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


function BindPayrollMonthList() {
    $("#ddlMonth").val(0);
    $("#ddlMonth").html("");
    $.ajax({
        type: "GET",
        url: "../PayrollProcessPeriod/GetPayrollMonthList",
        data: {},
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlMonth").append($("<option></option>").val(0).html("-Select Month-"));
            $.each(data, function (i, item) {
                $("#ddlMonth").append($("<option></option>").val(item.MonthId).html(item.MonthShortName));
            });
        },
        error: function (Result) {
            $("#ddlMonth").append($("<option></option>").val(0).html("-Select Month-"));
        }
    });
}

function GetProcessStartEndDate() {
    var monthId = $("#ddlMonth option:selected").val();
    $("#txtPayrollProcessingStartDate").val("");
    $("#txtPayrollProcessingEndDate").val("");
    $.ajax({
        type: "GET",
        url: "../PayrollProcessPeriod/GetPayrollMonthStartAndEndDate",
        data: { monthId: monthId },
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#txtPayrollProcessingStartDate").val(data.PayrollProcessingStartDate);
            $("#txtPayrollProcessingEndDate").val(data.PayrollProcessingEndDate);
            
            
        },
        error: function (Result) {
            $("#txtPayrollProcessingStartDate").val("");
            $("#txtPayrollProcessingEndDate").val("");
        }
    });
}
function GetPayrollProcessPeriodDetail(payrollProcessingPeriodId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../PayrollProcessPeriod/GetPayrollProcessPeriodDetail",
        data: { payrollProcessingPeriodId: payrollProcessingPeriodId },
        dataType: "json",
        success: function (data) {
            $("#ddlMonth").val(data.MonthId);
            $("#hdnpayrollProcessingPeriodId").val(data.PayrollProcessingPeriodId);
            $("#txtPayrollProcessDate").val(data.PayrollProcessDate);
            $("#txtPayrollProcessingStartDate").val(data.PayrollProcessingStartDate);
            $("#txtPayrollProcessingEndDate").val(data.PayrollProcessingEndDate);
            $("#ddlPayrollProcessStatus").val(data.PayrollProcessStatus);
            $("#ddlPayrollLocked").val(data.PayrollLocked);
            $("#txtPayrollLockDate").val(data.PayrollLockDate);
            $("#divCreated").show();
            $("#txtCreatedBy").val(data.CreatedByUserName);
            $("#txtCreatedDate").val(data.CreatedDate);
            if (data.ModifiedByUserName != "") {
                $("#divModified").show();
                $("#txtModifiedBy").val(data.ModifiedByUserName);
                $("#txtModifiedDate").val(data.ModifiedDate);
            }
            if (data.PayrollLocked=="1")
            {
                $("#btnSave").hide();
            }
            else
            {
                $("#btnSave").show();
            
            }
          

        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });



}
function SaveData() {
    var ddlMonth = $("#ddlMonth");
    var hdnPayrollProcessingPeriodId = $("#hdnPayrollProcessingPeriodId");
    var txtPayrollProcessDate = $("#txtPayrollProcessDate");
    var txtPayrollProcessingStartDate = $("#txtPayrollProcessingStartDate");
    var txtPayrollProcessingEndDate = $("#txtPayrollProcessingEndDate");
    var ddlPayrollProcessStatus = $("#ddlPayrollProcessStatus");
    var ddlPayrollLocked = $("#ddlPayrollLocked");
    var txtPayrollLockDate = $("#txtPayrollLockDate");
   
    
    if (ddlMonth.val() == "" || ddlMonth.val() == "0") {
        ShowModel("Alert", "Please select Payroll Processing Month")
        return false;
    }
  

    var payrollProcessingViewModel = {
        PayrollProcessingPeriodId: hdnPayrollProcessingPeriodId.val(),
        PayrollProcessingStartDate: txtPayrollProcessingStartDate.val().trim(),
        PayrollProcessingEndDate: txtPayrollProcessingEndDate.val().trim(),
        MonthId: ddlMonth.val(),
        PayrollProcessStatus: ddlPayrollProcessStatus.val(),
        PayrollProcessDate: txtPayrollProcessDate.val(),
        PayrollLocked: ddlPayrollLocked.val(),
        PayrollLockDate: txtPayrollLockDate.val()
    };
    var accessMode = 1;//Add Mode
    if (hdnPayrollProcessingPeriodId.val() != null && hdnPayrollProcessingPeriodId.val() != 0) {
        accessMode = 2;//Edit Mode
    }
    var requestData = {
        payrollProcessPeriodViewModel: payrollProcessingViewModel
    };
    $.ajax({
        url: "../PayrollProcessPeriod/AddEditPayrollProcessPeriod?AccessMode=" + accessMode + "",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                setTimeout(
                   function () {
                       window.location.href = "../PayrollProcessPeriod/AddEditPayrollProcessPeriod?payrollProcessingPeriodId=" + data.trnId + "&AccessMode=3";
                   }, 2000);

                $("#btnSave").show();
              
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

function ResetPage() {
    if (confirm("Are you sure to reset all fields?")) {
        window.location.href = "../PayrollProcessPeriod/AddEditPayrollProcessPeriod?accessMode=1";
    }
}