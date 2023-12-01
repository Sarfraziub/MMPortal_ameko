$(document).ready(function () {
    $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Branch-"));
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
            // $("#txtCustomerCode").val(ui.item.code);
            BindCustomerBranchList(ui.item.value);
            //GenerateReportParameters();
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtCustomerName").val("");
                $("#hdnCustomerId").val("0");
                //  $("#txtCustomerCode").val("");
                ShowModel("Alert", "Please select Customer from List");
                //GenerateReportParameters();
            }
            return false;
        }

    })
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

function GenerateReportParameters() {
    var txtCustomerName = $("#txtCustomerName");
    var ddlCustomerBranch = $("#ddlCustomerBranch");
    if (txtCustomerName.val() == "")
    {
        ShowModel('Alert', 'Please enter customer name');
        txtCustomerName.focus();
        return false;
    }
    if (ddlCustomerBranch.val() == "0" || ddlCustomerBranch.val()==null) {
        ShowModel('Alert', 'Please enter customer site');
        ddlCustomerBranch.focus();
        return false;
    }
    var url = "../Customer/CustomerSiteWiseReport?customerId=" + $("#hdnCustomerId").val() + "&customerBranchId=" + $("#ddlCustomerBranch").val() + "&reportType=PDF";
    $('#lnkGenerateReport').attr({
        href: url,
        target: '_blank' });
                           
}


function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}

function ClearFields() {

    $("#txtCustomerSiteMRNNo").val("");
    $("#hdncustomerSiteMrnId").val("0");
    $("#txtCustomerSiteMRNDate").val($("#hdnCurrentDate").val());
    $("#hdnVendorId").val("0");
    $("#txtVendorName").val("");
    $("#txtVendorCode").val("");
    $("#txtInvoiceNo").val("");
    $("#txtInvoiceDate").val("");
    $("#txtInvoiceId").val("0"); 

    $("#txtSContactPerson").val("");
    $("#txtSAddress").val("");
    $("#txtSCity").val("");
    $("#ddlSCountry").val("0");
    $("#ddlSState").val("0");
    $("#txtSPinCode").val("");
    $("#txtSTINNo").val("");
    $("#txtSEmail").val("");
    $("#txtSMobileNo").val("");
    $("#txtSEmail").val("");
    $("#txtSFax").val(""); 

    $("#ddlCompanyBranch").val("0");

    $("#ddlApprovalStatus").val("Draft");


    $("#txtDispatchRefNo").val("");
    $("#txtDispatchRefDate").val(""); 
    $("#txtLRNo").val("");
    $("#txtLRDate").val("");
    $("#txtTransportVia").val("");
    $("#txtNoOfPackets").val("");
    $("#txtRemarks1").val("");
    $("#txtRemarks2").val("");

    $("#btnSave").show();
    $("#btnUpdate").hide();

    


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

function GetCustomerBranchListByID(customerId, customerBranchId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Project/GetCustomerBranchListByID",
        data: { customerId: customerId },
        dataType: "json",
        success: function (data) {

            $("#ddlCustomerType").append($("<option></option>").val(0).html("-Select Customer Branch-"));
            $.each(data, function (i, item) {

                $("#ddlCustomerBranch").append($("<option></option>").val(item.CustomerBranchId).html(item.BranchName));
            });
            if (customerBranchId != null && customerBranchId != "") {
                $("#ddlCustomerBranch").val(customerBranchId);
            }
        },
        error: function (Result) {
            $("#ddlCustomerType").append($("<option></option>").val(0).html("-Select Customer Branch-"));
        }
    });


}


/*Function Started here for Local Site MRN*/

function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}
