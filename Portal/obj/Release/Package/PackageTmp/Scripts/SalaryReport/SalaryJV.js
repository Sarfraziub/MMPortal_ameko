﻿$(document).ready(function () {
 
    
    $("#ddlPayrollProcessStatus").attr('disabled', true);
    $("#ddlPayrollLocked").attr('disabled', true);
 
    BindPayrollMonthList();
    BindCompanyBranchList();
    BindDepartmentList();
    $("#ddlDesignation").append($("<option></option>").val(0).html("-Select Designation-"));

    GenerateReportParameters();

 
 
});



function BindPayrollMonthList() {
    $("#ddlMonth").val(0);
    $("#ddlMonth").html("");
    $.ajax({
        type: "GET",
        url: "../SalaryReport/GetProcessedMonthList",
        data: {},
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlMonth").append($("<option></option>").val(0).html("-Select Period-"));
            $.each(data, function (i, item) {
                $("#ddlMonth").append($("<option></option>").val(item.PayrollProcessingPeriodId).html(item.PayrollProcessingStartDate + ' - ' + item.PayrollProcessingEndDate));
            });
        },
        error: function (Result) {
            $("#ddlMonth").append($("<option></option>").val(0).html("-Select Period-"));
        }
    });
}

function BindDepartmentList() {
    $.ajax({
        type: "GET",
        url: "../Employee/GetDepartmentList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlDepartment").append($("<option></option>").val(0).html("-Select Department-"));
            $.each(data, function (i, item) {

                $("#ddlDepartment").append($("<option></option>").val(item.DepartmentId).html(item.DepartmentName));
            });
        },
        error: function (Result) {
            $("#ddlDepartment").append($("<option></option>").val(0).html("-Select Department-"));
        }
    });
}
function BindCompanyBranchList() {
    $("#ddlCompanyBranch").val(0);
    $("#ddlCompanyBranch").html("");
    $.ajax({
        type: "GET",
        url: "../DeliveryChallan/GetCompanyBranchList",
        data: {},
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
            $.each(data, function (i, item) {
                $("#ddlCompanyBranch").append($("<option></option>").val(item.CompanyBranchId).html(item.BranchName));
            });
        },
        error: function (Result) {
            $("#ddlCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
        }
    });
}
function BindDesignationList(designationId) {
    var departmentId = $("#ddlDepartment option:selected").val();
    $("#ddlDesignation").val(0);
    $("#ddlDesignation").html("");
    if (departmentId != undefined && departmentId != "" && departmentId != "0") {
        var data = { departmentId: departmentId };
        $.ajax({
            type: "GET",
            url: "../Employee/GetDesignationList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlDesignation").append($("<option></option>").val(0).html("-Select Designation-"));
                $.each(data, function (i, item) {
                    $("#ddlDesignation").append($("<option></option>").val(item.DesignationId).html(item.DesignationName));
                });
                $("#ddlDesignation").val(designationId);
            },
            error: function (Result) {
                $("#ddlDesignation").append($("<option></option>").val(0).html("-Select Designation-"));
            }
        });
    }
    else {

        $("#ddlDesignation").append($("<option></option>").val(0).html("-Select Designation-"));
    }
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
function GetPayrollProcessPeriodDetail(obj) {
    var payrollProcessingPeriodId = $(obj).val();
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../PayrollProcessPeriod/GetPayrollProcessPeriodDetail",
        data: { payrollProcessingPeriodId: payrollProcessingPeriodId },
        dataType: "json",
        success: function (data) {
            
            $("#hdnpayrollProcessingPeriodId").val(data.PayrollProcessingPeriodId);
            $("#ddlPayrollProcessStatus").val(data.PayrollProcessStatus);
            $("#ddlPayrollLocked").val(data.PayrollLocked);
            

        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });



}

function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}
function OpenPrintPopup() {
    var ddlMonth = $("#ddlMonth");
    if (ddlMonth.val() == "" || ddlMonth.val() == "0") {
        ShowModel("Alert", "Please select Salary Period!!!")
        return false;
    }
    GenerateReportParameters();
    $("#printModel").modal();

    
}
function ShowHidePrintOption() {
    var reportOption = $("#ddlPrintOption").val();
    if (reportOption == "PDF") {
        $("#btnPdf").show();
        $("#btnExcel").hide();
    }
    else if (reportOption == "Excel") {
        $("#btnExcel").show();
        $("#btnPdf").hide();
    }
    else
    {
        $("#btnExcel").hide();
        $("#btnPdf").hide();
    }
}
function GenerateReportParameters() {

    var url = "../SalaryReport/GenerateSalaryJVReport?payrollPeriod="+ $("#ddlMonth option:selected ").text() +"&payrollProcessingPeriodId=" + $("#ddlMonth").val() + "&companyBranchId=" + $("#ddlCompanyBranch").val() + "&departmentId=" + $("#ddlDepartment").val() + "&designationId=" + $("#ddlDesignation").val() + "&employeeType=" + $("#ddlEmploymentType").val() + "&employeeCodes=" + $("#txtEmployeeCodes").val() + "&reportType=PDF";
    $('#btnPdf').attr('href', url);
    url = "../SalaryReport/GenerateSalaryJVReport?payrollPeriod=" + $("#ddlMonth  option:selected").text() + "&payrollProcessingPeriodId=" + $("#ddlMonth").val() + "&companyBranchId=" + $("#ddlCompanyBranch").val() + "&departmentId=" + $("#ddlDepartment").val() + "&designationId=" + $("#ddlDesignation").val() + "&employeeType=" + $("#ddlEmploymentType").val() + "&employeeCodes=" + $("#txtEmployeeCodes").val() + "&reportType=Excel";
    $('#btnExcel').attr('href', url);

}

function GenerateSalaryJVVoucher()
{
    if ($("#ddlPayrollLocked").val() != "1")
    {
          ShowModel("Alert", "Please first Lock Payroll Period !!!")
        return false;
    }


    if (confirm("Are you sure you want to generate Salary JV Voucher?")) {
        var ddlMonth = $("#ddlMonth");
        var ddlCompanyBranch = $("#ddlCompanyBranch");
        var ddlDepartment = $("#ddlDepartment");
        var ddlDesignation = $("#ddlDesignation");
        var ddlEmploymentType = $("#ddlEmploymentType");
        var txtEmployeeCodes = $("#txtEmployeeCodes");
        var requestData = {
            payrollProcessingPeriodId: ddlMonth.val(), companyBranchId: ddlCompanyBranch.val(),
            departmentId: ddlDepartment.val(), designationId: ddlDesignation.val(), employeeType: ddlEmploymentType.val(),
            employeeCodes: txtEmployeeCodes.val()        };
        $.ajax({
            url: "../SalaryReport/SalaryJV",
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
                           window.location.href = "../JournalVoucher/AddEditJournalVoucher?voucherId=" + data.trnId + "&AccessMode=3";
                       }, 1000);

                    
                    
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
}