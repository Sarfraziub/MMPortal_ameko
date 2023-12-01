$(document).ready(function () {
   
    $("#txtSourcedFrom").attr('readOnly', true);
    $("#txtDailyExpenseDate").attr('readOnly', true);
    $("#txtFoodAndTeaExpense").attr('readOnly', true);
     
    $("#txtCustomerName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../CustomerPayment/GetCustomerAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.CustomerName, value: item.CustomerId, primaryAddress: item.PrimaryAddress, code: item.CustomerCode };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtCustomerName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtCustomerName").val(ui.item.label);
            $("#hdnCustomerId").val(ui.item.value);
            $("#txtCustomerCode").val(ui.item.code); 
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtCustomerName").val("");
                $("#hdnCustomerId").val("0");
                $("#txtCustomerCode").val("");
                ShowModel("Alert", "Please select Customer from List")

            }
            return false;
        }

    })
  .autocomplete("instance")._renderItem = function (ul, item) {
      return $("<li>")
        .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.primaryAddress + "</div>")
        .appendTo(ul);
  };

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
            GetVagesByEmployeeId(ui.item.value);
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

    $("#txtCustomerName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Quotation/GetCustomerAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.CustomerName, value: item.CustomerId, primaryAddress: item.PrimaryAddress, code: item.CustomerCode, gSTNo: item.GSTNo };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtCustomerName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtCustomerName").val(ui.item.label);
            $("#hdnCustomerId").val(ui.item.value);
            $("#txtCustomerCode").val(ui.item.code);

            BindCustomerBranchList();

            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtCustomerName").val("");
                $("#hdnCustomerId").val("0");
                $("#txtCustomerCode").val("");
                ShowModel("Alert", "Please select Customer from List")

            }
            return false;
        }

    })
  .autocomplete("instance")._renderItem = function (ul, item) {
      return $("<li>")
        .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.primaryAddress + "</div>")
        .appendTo(ul);
  };

    $("#txtDailyExpenseDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        maxDate:'0',
        onSelect: function (selected) {

        }
    });

    $("#txtSearchFromDate,#txtSearchToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    });

    BindCompanyBranchList();
    BindTimeTypeList();

    var hdnAccessMode = $("#hdnAccessMode");
    var hdnDailyExpensesId = $("#hdnDailyExpensesId");
    if (hdnDailyExpensesId.val() != "" && hdnDailyExpensesId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetDailyExpensesDetail(hdnDailyExpensesId.val());
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

  

    //$("#txtCustomerName").focus();

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

function OpenInvoiceSearchPopup() {
    $("#SearchInvoiceModel").modal();

}
function SearchInvoice() {
    var txtInvoiceNo = $("#txtSearchInvoiceNo");
    var txtCustomerName = $("#txtSearchCustomerName");

    var txtRefNo = $("#txtSearchRefNo");
    var txtFromDate = $("#txtSearchFromDate");
    var txtToDate = $("#txtSearchToDate");

    var requestData = { saleinvoiceNo: txtInvoiceNo.val().trim(), customerName: txtCustomerName.val().trim(), refNo: txtRefNo.val().trim(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), displayType: "Popup", approvalStatus:"Final" };
    $.ajax({
        url: "../CustomerPayment/GetSaleInvoiceList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divInvoiceList").html("");
            $("#divInvoiceList").html(err);
        },
        success: function (data) {
            $("#divInvoiceList").html("");
            $("#divInvoiceList").html(data);
        }
    });
}
function SelectInvoice(invoiceId, saleinvoiceNo, invoiceDate, customerId, customerCode, customerName) {
    $("#txtInvoiceNo").val(saleinvoiceNo);
    $("#hdnInvoiceId").val(invoiceId);
    $("#txtInvoiceDate").val(invoiceDate);
    $("#hdnCustomerId").val(customerId);
    $("#txtCustomerCode").val(customerCode);
    $("#txtCustomerName").val(customerName);
    $("#SearchInvoiceModel").modal('hide'); 
   
}

 

function BindBookTypeList() {
    $.ajax({
        type: "GET",
        url: "../VendorPayment/GetBookList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlBookType").append($("<option></option>").val(0).html("-Select Book Type-"));
            $.each(data, function (i, item) {

                $("#ddlBookType").append($("<option></option>").val(item.BookId).html(item.BookName + ": " + item.BankAccountNo + ":" + item.BankBranch));
            });
        },
        error: function (Result) {
            $("#ddlBookType").append($("<option></option>").val(0).html("-Select Book Type-"));
        }
    });
}






function BindPaymentModeList() {
    $.ajax({
        type: "GET",
        url: "../CustomerPayment/GetPaymentModeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlPaymentModeName").append($("<option></option>").val(0).html("-Select Payment Mode-"));
            $.each(data, function (i, item) {

                $("#ddlPaymentModeName").append($("<option></option>").val(item.PaymentModeId).html(item.PaymentModeName));
            });
        },
        error: function (Result) {
            $("#ddlPaymentModeName").append($("<option></option>").val(0).html("-Select Payment Mode-"));
        }
    });
}

 
function GetDailyExpensesDetail(dailyExpensesId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../DailyExpenses/GetDailyExpensesDetail",
        data: { dailyExpensesId: dailyExpensesId },
        dataType: "json",
        success: function (data) { 
            $("#hdnDailyExpensesId").val(dailyExpensesId);
            $("#txtDailyExpenseCode").val(data.DailyExpenseCode);
            $("#txtDailyExpenseDate").val(data.DailyExpenseDate);
            $("#txtEmployeeName").val(data.EmployeeName);
            $("#hdnEmployeeId").val(data.EmployeeId);
            $("#hdnTimeTypeId").val(data.TimeTypeId);
            $("#txtConveyanceAmt").val(data.ConveyanceAmt);
            $("#txtFoodAndTeaExpense").val(data.FoodAndTeaExpenses);
            $("#txtVagesAmt").val(data.Vages);
            $("#txtCustomerName").val(data.CustomerName);
            $("#hdnCustomerId").val(data.CustomerId);
            $("#ddlTimeTypeName").val(data.TimeTypeId);
            BindCustomerBranchList(data.CustomerBranchId);
            //BindTimeTypeList(data.TimeTypeId);
            $("#ddlCompanyBranch").val(data.CompanyBranchId);
            $("#ddlStatus").val(data.DailyExpenseStatus);
            if (data.DailyExpenseStatus == "Final") {
                $("#ddlStatus").val(data.DailyExpenseStatus);
                $("#btnUpdate").hide();
            } 
            $("#btnAddNew").show();
            $("#btnPrint").show();
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}
$(function () {
    $("#dialogForQuotationNo").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $("#opener").on("click", function () {
        $("#dialogForQuotationNo").dialog("open");
    });
});
function SaveData() {
    var hdnDailyExpensesId = $("#hdnDailyExpensesId");
    var txtDailyExpenseCode = $("#txtDailyExpenseCode");
    var txtDailyExpenseDate = $("#txtDailyExpenseDate");
    var hdnEmployeeId = $("#hdnEmployeeId");
    var ddlTimeTypeName = $("#ddlTimeTypeName");
    var txtConveyanceAmt = $("#txtConveyanceAmt");
    var txtFoodAndTeaExpense = $("#txtFoodAndTeaExpense");
    var hdnCustomerId = $("#hdnCustomerId");
    var ddlCustomerBranch = $("#ddlCustomerBranch");
    var ddlCompanyBranch = $("#ddlCompanyBranch");
    var ddlStatus = $("#ddlStatus");

    if (hdnEmployeeId.val() == "0") {
        ShowModel("Alert", "Please Enter Employee Name")
        txtEmployeeName.focus();
        return false;
    }

    if (ddlTimeTypeName.val() == "0") {
        ShowModel("Alert", "Please Select Time Type")
        ddlTimeTypeName.focus();
        return false;
    }
    if (hdnCustomerId.val() == "" || hdnCustomerId.val() == "0") {
        ShowModel("Alert", "Please select cutomer from list")
        txtCustomerName.focus();
        return false;
    }
    if (ddlCustomerBranch.val() == "0") {
        ShowModel("Alert", "Please Select customer site")
        ddlCustomerBranch.focus();
        return false;
    }
    if (ddlCompanyBranch.val() == "0") {
        ShowModel("Alert", "Please Select company branch")
        ddlCompanyBranch.focus();
        return false;
    }
   
    if (txtConveyanceAmt.val().trim() == "") {
        ShowModel("Alert", "Please enter conveyance amount")
        txtConveyanceAmt.focus();
        return false;
    }

    if (txtFoodAndTeaExpense.val().trim() == "") {
        ShowModel("Alert", "Please enter Food & Tea Expense")
        txtFoodAndTeaExpense.focus();
        return false;
    }
    
    var dailyExpensesViewModel = {
        DailyExpensesId: hdnDailyExpensesId.val(),
        DailyExpenseCode: txtDailyExpenseCode.val().trim(),
        DailyExpenseDate:txtDailyExpenseDate.val().trim(),
        EmployeeId: hdnEmployeeId.val().trim(),
        TimeTypeId: ddlTimeTypeName.val().trim(),
        ConveyanceAmt: txtConveyanceAmt.val().trim(),
        FoodAndTeaExpenses: txtFoodAndTeaExpense.val().trim(),
        CustomerId: hdnCustomerId.val().trim(),
        CustomerBranchId: ddlCustomerBranch.val(),
        CompanyBranchId: ddlCompanyBranch.val(),
        DailyExpenseStatus: ddlStatus.val()
    };
     
    var requestData = { dailyExpensesViewModel: dailyExpensesViewModel };
    $.ajax({
        url: "../DailyExpenses/AddEditDailyExpenses",
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
                       window.location.href = "../DailyExpenses/AddEditDailyExpenses?dailyExpensesId=" + data.trnId + "&AccessMode=2";
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

    $("#txtPaymentNo").val("");
    $("#hdnPaymentTrnId").val("0");
    $("#txtPaymentDate").val($("#hdnCurrentDate").val());
    $("#hdnInvoiceId").val("0");
    $("#txtCustomerName").val("");
    $("#txtInvoiceNo").val("");
    $("#ddlBookType").val(0);
    $("#txtCustomerCode").val("");  
   
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());


    $("#txtRefNo").val("");
    $("#txtRefDate").val("");

    $("#btnSave").show();
    $("#btnUpdate").hide();


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
                if (customerBranchId != null) {
                    $("#ddlCustomerBranch").val(customerBranchId);
                }
                else {
                    $("#ddlCustomerBranch").val(0);
                }
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
            if (timeTypeId != null)
            {
                $("#ddlTimeTypeName").val(timeTypeId);
            }
           
        },
        error: function (Result) {
            $("#ddlTimeTypeName").append($("<option></option>").val(0).html("-Select Time Type-"));
        }
    });
}
function GetFoodAndTeaExpensesByTimeType(timeTypeName)
{
    var TimeType = 0;
    if (timeTypeName != null)
    {
        TimeType = timeTypeName;
    }
    var requestData = { timeTypeName:TimeType }
    $.ajax({
        url: "../DailyExpenses/GetFoodAndTeaExpensesByTimeType",
        data: requestData,
        dataType: "json",
        type: "GET",
        asnc: false,
        success: function (data) {
            if (data != null) {
                $("#txtFoodAndTeaExpense").val(data);
            }
        },
        error: function (Result) {
            ShowModel("Alert", "Problem In Request");
        }
    });
}

function GetVagesByEmployeeId(employeeId) {
    var EmployeeId = 0;
    if (employeeId != null) {
        EmployeeId = employeeId;
    }
    var requestData = { employeeId: EmployeeId }
    $.ajax({
        url: "../DailyExpenses/GetVagesByEmployeeId",
        data: requestData,
        dataType: "json",
        type: "GET",
        asnc: false,
        success: function (data) {
            if (data != null) {
                $("#txtVagesAmt").val(data);
            }
        },
        error: function (Result) {
            ShowModel("Alert", "Problem In Request");
        }
    });
}

$("body").on('change', '#ddlTimeTypeName', function () {
    var TimeTypeName = $("#ddlTimeTypeName option:selected").text();
    if (TimeTypeName != "") {
        GetFoodAndTeaExpensesByTimeType(TimeTypeName);
    }
});