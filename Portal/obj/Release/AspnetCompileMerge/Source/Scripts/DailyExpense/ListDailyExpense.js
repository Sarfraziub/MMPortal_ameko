
$(document).ready(function () {
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
    BindTimeTypeList();
    SearchDailyExpenses();
    GetCustomerBranchListWithOutCustId();
    GenerateReportParameters();
    $("#txtEmployeeName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Employee/GetTempEmployeeAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: {
                    term: request.term, departmentId: $("#txtEmployeeName").val(), designationId: $("#txtEmployeeName").val()
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
            $("#txtEmployeeName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtEmployeeName").val(ui.item.label);
            $("#hdnEmployeeId").val(ui.item.value);
            GenerateReportParameters();
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtEmployeeName").val("");
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

function ClearFields() {
    $("#txtEmployeeName").val("");
    $("#hdnEmployeeId").val("0");
    $("#ddlTimeTypeName").val("0");
    $("#ddlCustomerBranch").val("0")
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    
}
function SearchDailyExpenses() {
    var hdnEmployeeId = $("#hdnEmployeeId");
    var ddlTimeTypeid = $("#ddlTimeTypeName");
    var ddlCustomerBranch = $("#ddlCustomerBranch");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");

    var requestData = { employeeId: hdnEmployeeId.val(), timeTypeId: ddlTimeTypeid.val(), customerBranchid: ddlCustomerBranch.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val() };
    $.ajax({
        url: "../DailyExpenses/GetDailyExpensesList",
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

function BindCustomerBranchList(customerBranchId) {
    var customerId = $("#hdnCustomerId").val();
    $("#ddlCustomerBranch").val(0);
    $("#ddlCustomerBranch").html("");
    if (customerId != undefined && customerId != "" && customerId != "0") {
        var data = { customerId: customerId };
        $.ajax({
            type: "GET",
            url: "../SO/GetCustomerBranchList",
            data: data,
            dataType: "json",
            asnc: false,
            success: function (data) {
                $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Branch-"));
                $.each(data, function (i, item) {
                    $("#ddlCustomerBranch").append($("<option></option>").val(item.CustomerBranchId).html(item.BranchName));
                });
                $("#ddlCustomerBranch").val(customerBranchId);
            },
            error: function (Result) {
                $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Branch-"));
            }
        });
    }
    else {
        $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Branch-"));
    }
}

function GetCustomerBranchListWithOutCustId() {
    $("#ddlCustomerBranch").val(0);
    $("#ddlCustomerBranch").html("");    
        var data = { };
        $.ajax({
            type: "GET",
            url: "../DailyExpenses/GetCustomerBranchListWithOutCustId",
            data: data,
            dataType: "json",
            asnc: false,
            success: function (data) {
                $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Branch-"));
                $.each(data, function (i, item) {
                    $("#ddlCustomerBranch").append($("<option></option>").val(item.CustomerBranchId).html(item.BranchName));
                });
            },
            error: function (Result) {
                $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Branch-"));
            }
        });
}
function BindTimeTypeList(timeTypeId) {
    $.ajax({
        type: "GET",
        url: "../DailyExpenses/GetTimeTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlTimeTypeName").append($("<option></option>").val(0).html("-Select Time Type-"));
            $.each(data, function (i, item) {

                $("#ddlTimeTypeName").append($("<option></option>").val(item.TimeTypeId).html(item.TimeTypeName));
            });
            if (timeTypeId != null) {
                $("#ddlTimeTypeName").val(timeTypeId);
            }

        },
        error: function (Result) {
            $("#ddlTimeTypeName").append($("<option></option>").val(0).html("-Select Time Type-"));
        }
    });
}

function GenerateReportParameters() {

    var url = "../DailyExpenses/DailyExpensesReport?employeeId=" + $("#hdnEmployeeId").val() + "&timeTypeId=" + $("#hdnTimeTypeId").val() + "&customerBranchid=" + $("#ddlCustomerBranch").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#lnkGenerateReport').attr({
        href: url,
        target: '_blank'
    });

}

$('body').on('change', '#ddlCustomerBranch', function () {
    GenerateReportParameters();
});
$('body').on('change', '#txtFromDate', function () {
    GenerateReportParameters();
});
$('body').on('change', '#txtToDate', function () {
    GenerateReportParameters();
});
$('body').on('change', '#ddlTimeTypeName', function () {
    GenerateReportParameters();
});
