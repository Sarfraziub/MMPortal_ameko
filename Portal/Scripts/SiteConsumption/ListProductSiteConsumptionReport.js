
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

    $("#txtAssemblyName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../ProductBOM/GetProductAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term, assemblyType: $("#ddlAssemblyType").val() },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.ProductName, value: item.Productid, desc: item.ProductShortDesc, code: item.ProductCode };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtAssemblyName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtAssemblyName").val(ui.item.label);
            $("#hdnAssemblyId").val(ui.item.value);
            
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtAssemblyName").val("");
                $("#hdnAssemblyId").val("0");              
                ShowModel("Alert", "Please select Assembly Name")

            }
            return false;
        }

    })

  
});

// for Product List
$("#txtProductName").autocomplete({
    minLength: 0,
    source: function (request, response) {
        $.ajax({
            url: "../Product/GetProductAutoCompleteList",
            type: "GET",
            dataType: "json",
            data: { term: request.term },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.ProductName, value: item.Productid, desc: item.ProductShortDesc, code: item.ProductCode };
                }))
            }
        })
    },
    focus: function (event, ui) {
        $("#txtProductName").val(ui.item.label);
        return false;
    },
    select: function (event, ui) {
        $("#txtProductName").val(ui.item.label);
        $("#hdnProductId").val(ui.item.value);
     //   $("#txtProductShortDesc").val(ui.item.desc);
      //  $("#txtProductCode").val(ui.item.code);
      
        return false;
    },
    change: function (event, ui) {
        if (ui.item == null) {
            $("#txtProductName").val("");
            $("#hdnProductId").val("0");
            $("#txtProductShortDesc").val("");
            $("#txtProductCode").val("");
            ShowModel("Alert", "Please select Product from List")

        }
        return false;
    }

})
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.desc + "</div>")
      .appendTo(ul);
};
//for Customer Auto  Complete 

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
        $("#txtCustomerCode").val(ui.item.code)
        $("#ddlCustomerBranch option").remove();
        GetCustomerBranchListByID(ui.item.value, "");
        return false;
    },
    change: function (event, ui) {
        if (ui.item == null) {
            $("#txtCustomerName").val("");
            $("#hdnCustomerId").val("0");
            //   $("#txtCustomerCode").val("");
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

function GetCustomerBranchListByID(customerId, customerBranchId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Project/GetCustomerBranchListByID",
        data: { customerId: customerId },
        dataType: "json",
        success: function (data) {
           
            $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Customer Branch-"));
            $("#ddlCustomerBranch").append($("<option></option>").val(0).html("All Branch"));
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
function SearchAssemblyList() {

    var txtProductName = $("#txtProductName");
    var ddlAssemblyType = $("#ddlAssemblyType");
    
    var requestData = { assemblyName: txtProductName.val().trim(), assemblyType: ddlAssemblyType.val() };
    $.ajax({
        url: "../ProductBOM/GetAssemblyList",
        data: requestData,
        dataType: "html",
        asnc: true,
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

function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}
function OpenPrintPopup() {
    var txtCustomerName = $("#txtCustomerName");
    var hdnCustomerId = $("#hdnCustomerId");
    var ddlCustomerBranch = $("#ddlCustomerBranch");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var hdnProductId = $("#hdnProductId");
    if (txtCustomerName.val().trim() == "") {
        ShowModel("Alert", "Please enter Customer Name")
        txtCustomerName.focus();
        return false;
    }
    if (ddlCustomerBranch.val() == null) {
        ShowModel("Alert", "Customer Branch Does Not exist , Please Select Another Customer ");
        txtCustomerName.focus();
        return false;
    }
    if (ddlCustomerBranch.val() == ""||ddlCustomerBranch.val()==0) {
        ShowModel("Alert", "Please Select Customer Site");
        ddlCustomerBranch.focus();
        return false;
    }
    if (txtFromDate.val().trim() == "") {
        ShowModel("Alert", "Please enter From Date")
        txtFromDate.focus();
        return false;
    }
    if (txtToDate.val().trim() == "") {
        ShowModel("Alert", "Please enter To Date")
        txtToDate.focus();
        return false;
    }
    $("#printModel").modal(); 
        GenerateReportParameters();
   
}
function OpenPrintSitePopup() {
    var txtCustomerName = $("#txtCustomerName");
    var hdnCustomerId = $("#hdnCustomerId");
    var ddlCustomerBranch = $("#ddlCustomerBranch");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var hdnProductId = $("#hdnProductId");
    if (txtCustomerName.val().trim() == "") {
        ShowModel("Alert", "Please enter Customer Name")
        txtCustomerName.focus();
        return false;
    }
    $("#printModel").modal();
    GenerateSiteConsumptionDetailReportParameters();

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
}

function GenerateReportParameters() {
    
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var url = "../SiteConsumption/Report?customerId=" + $("#hdnCustomerId").val() + "&customerBranchId=" + $("#ddlCustomerBranch").val() + "&productId=" + $("#hdnProductId").val() + "&companyBranchId=" + "&fromDate=" + txtFromDate.val() + "&toDate=" + txtToDate.val() + "&reportType=PDF";
    $('#btnPdf').attr('href', url);
    var urlSummary = "../SiteConsumption/Report?customerId=" + $("#hdnCustomerId").val() + "&customerBranchId=" + $("#ddlCustomerBranch").val() + "&productId=" + $("#hdnProductId").val() + "&companyBranchId=" + "&fromDate=" + txtFromDate.val() + "&toDate=" + txtToDate.val() + "&reportType=PDF";
    $('#btnExcel').attr('href', urlSummary);


}
function GenerateSiteConsumptionDetailReportParameters() {

    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var url = "../SiteConsumption/SiteConsumptionDetailReport?customerId=" + $("#hdnCustomerId").val() + "&customerBranchId=" + $("#ddlCustomerBranch").val() + "&productId=" + $("#hdnProductId").val() + "&companyBranchId=" + "&fromDate=" + txtFromDate.val() + "&toDate=" + txtToDate.val() + "&reportType=PDF";
    $('#btnPdf').attr('href', url);
    var urlSummary = "../SiteConsumption/Report?customerId=" + $("#hdnCustomerId").val() + "&customerBranchId=" + $("#ddlCustomerBranch").val() + "&productId=" + $("#hdnProductId").val() + "&companyBranchId=" + "&fromDate=" + txtFromDate.val() + "&toDate=" + txtToDate.val() + "&reportType=PDF";
    $('#btnExcel').attr('href', urlSummary);


}
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}
