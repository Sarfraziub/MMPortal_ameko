﻿
$(document).ready(function () {
    $("#txtFromDate,#txtToDate").datepicker({
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
    BindLeaveTypeList();
   // SearchEmployeeLeaveApplication();
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
    $("#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    });
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
function BindLeaveTypeList() {
    $.ajax({
        type: "GET",
        url: "../EmployeeLeaveApplication/GetLeaveTypeForEmpolyeeLeaveAppList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlLeaveType").append($("<option></option>").val(0).html("-Select Leave Type-"));
            $.each(data, function (i, item) {

                $("#ddlLeaveType").append($("<option></option>").val(item.LeaveTypeId).html(item.LeaveTypeName));
            });
        },
        error: function (Result) {
            $("#ddlLeaveType").append($("<option></option>").val(0).html("-Select Leave Type-"));
        }
    });
}


function ClearFields() {
    $("#txtApplicationNo").val("");
    $("#hdnApplicationId").val("0");
    $("#txtApplicationDate").val("");
    $("#ddlLeaveType").val("0");
    $("#hdnEmployeeId").val("0");
    $("#txtEmployee").val("");
    $("#ddlLeaveStatus").val("0");
    $("#ddlApprovalStatus").val("0");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
}
function SearchEmployeeLeaveApplication() {
    var txtApplicationNo = $("#txtApplicationNo");
    var hdnApplicationId = $("#hdnApplicationId");
    var hdnEmployeeId = $("#hdnEmployeeId");
    var txtEmployee = $("#txtEmployee");
    var ddlLeaveType = $("#ddlLeaveType");
    var ddlLeaveStatus = $("#ddlLeaveStatus");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var requestData = {
        applicationNo: txtApplicationNo.val().trim(), employeeId: hdnEmployeeId.val(), leaveTypeId: ddlLeaveType.val(),
        leaveStatus: ddlLeaveStatus.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val()
    };
    $.ajax({
        url: "../EmployeeLeaveApplication/GetEmployeeLeaveApplicationApprovalList",
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
function Export() {
    var divList = $("#divList");
    ExporttoExcel(divList);
}