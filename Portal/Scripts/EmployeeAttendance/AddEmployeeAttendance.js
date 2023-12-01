$(document).ready(function () {

    var hdnEmployeeId = $("#hdnEmployeeId");
    var hdnEmployeeName = $("#hdnEmployeeName");
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnAttendanceDate = $("#hdnAttendanceDate");
    $("#txtAttendanceDate").attr('readOnly', true);

    $("#txtAttendanceDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {
            $("#hdnAttendanceDate").val($("#txtAttendanceDate").val());
            GetEmployeeInOutDetails();
        }
    });
    $('#txtInTime').datetimepicker({
        format: 'd-M-Y h:i a',
        formatTime: 'h:i a',
        time24h: true

    });

    $('#txtOutTime').datetimepicker({
        format: 'd-M-Y h:i a',
        formatTime: 'h:i a',
        time24h: true

    });

    if (hdnEmployeeId.val() != "" && hdnEmployeeId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {

        if (hdnAccessMode.val() == "3") {
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
        }
        else {
            $("#txtEmployee").val(hdnEmployeeName.val());
            $("#btnAddNew").show();
            GetEmployeeInOutDetails();
        }
    }


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
            $("#hdnEmployeeName").val(ui.item.label);
            GetEmployeeInOutDetails();
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


function SaveData() {
    var hdnEmployeeId = $("#hdnEmployeeId");
    var txtAttendanceDate = $("#txtAttendanceDate");
    var txtEmployee = $("#txtEmployee");
    var txtInTime = $("#txtInTime");
    var txtOutTime = $("#txtOutTime");
    var hdnEmployeeName = $("#hdnEmployeeName");
    if (txtAttendanceDate.val() == "") {
        ShowModel("Alert", "Please select Attendance Date")
        return false;
    }
    if (txtEmployee.val() == "" || txtEmployee.val() == "0") {
        ShowModel("Alert", "Please select Employee")
        return false;
    }

    if (txtEmployee.val().trim() == "") {
        ShowModel("Alert", "Please Enter Employee Name")
        txtEmployee.focus();
        return false;
    }

    if (txtInTime.val() == "" && txtOutTime.val() == "")
    {
        ShowModel("Alert", "Please Select IN/OUT Time ")
        txtEmployee.focus();
        return false;
    }

    var requestData = {
        employeeId: hdnEmployeeId.val(),
        attendanceDate: txtAttendanceDate.val(),
        presentAbsent: "P",
        inTime: txtInTime.val(),
        outTime: txtOutTime.val(),
        attendanceStatus: "Approved"
    };
    $.ajax({
        url: "../EmployeeAttendance/AddEditEmployeeAttendance",
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
                       window.location.href = "../EmployeeAttendance/AddEditEmployeeAttendance?employeeId=" + hdnEmployeeId.val() + "&employeeName=" + hdnEmployeeName.val() + "&AccessMode=2";
                   }, 2000);
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

function GetEmployeeInOutDetails() {
    var attendanceDate = $("#hdnAttendanceDate").val();
    var employeeID = $("#hdnEmployeeId").val();
    if (attendanceDate != "0" && employeeID != "0") {
        var requestData = { attendanceDate: attendanceDate, employeeId: employeeID };
        $.ajax({
            url: "../Attendance/GetEmployeeInOutDetails",
            cache: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (data) {
                if (data != null) {
                    if (data.length == 0) {
                        $("#txtInTime").val("");
                        $("#txtOutTime").val("");
                    }
                    if (data.length > 0) {
                        $.each(data, function (i, item) {
                            if (item.InOut == "IN") {
                                $("#txtInTime").val(item.TrnDateTime);
                            }
                            if (item.InOut == "OUT") {
                                $("#txtOutTime").val(item.TrnDateTime);
                            }
                        });
                    }
                }
                else {
                    ShowModel("No Data", "No Data Found")
                }
            },
            error: function (err) {
                ShowModel("Error", err)
            }
        });
    }
}
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}

function ClearFields() {
    $("#txtEmployee").val("");
    $("#hdnAttendanceId").val("0");
    $("#ddlAdvanceType").val("0");
    $("#txtAdvanceAmount").val("");
    $("#txtAdvanceInstallmentAmount").val("");
    $("#txtAdvanceReason").val("");
    $("#ddlAdvanceStatus").val("Final");
    $("#btnIn").hide();
    $("#btnOut").hide();
}




