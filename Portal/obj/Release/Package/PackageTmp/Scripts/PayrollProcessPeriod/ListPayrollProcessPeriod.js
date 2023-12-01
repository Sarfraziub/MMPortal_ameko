$(document).ready(function () {
    BindPayrollMonthList();
 
    setTimeout(
    function () {
       // SearchPayrollProcessPeriod();
    }, 500);
  
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


function ClearFields() {
    $("#ddlMonth").val("0");
    $("#ddlPayrollProcessStatus").val("");
    $("#ddlPayrollLocked").val("");
   
}
function SearchPayrollProcessPeriod() {
    var ddlMonth = $("#ddlMonth");
    var ddlPayrollProcessStatus = $("#ddlPayrollProcessStatus");
    var ddlPayrollLocked = $("#ddlPayrollLocked");
  
    var requestData = {
        monthId: ddlMonth.val(), payrollProcessStatus: ddlPayrollProcessStatus.val(),
        payrollLocked: ddlPayrollLocked.val() };
    $.ajax({
        url: "../PayrollProcessPeriod/GetPayrollProcessPeriodList",
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