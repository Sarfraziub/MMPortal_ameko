
$(document).ready(function () {
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
    $("#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {
            GenerateReportParameters();
        }
    }); 
    BindProductTypeList();
    BindProductMainGroupList();
   
    $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
    $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Customer Branch-"));
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnProductId = $("#hdnProductId");

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
            GenerateReportParameters();
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtProductName").val("");
                $("#hdnProductId").val("0");
                GenerateReportParameters();
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
            GenerateReportParameters();
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtCustomerName").val("");
                $("#hdnCustomerId").val("0");
                //  $("#txtCustomerCode").val("");
                ShowModel("Alert", "Please select Customer from List")

            }
            return false;
        }

    })

    var url = "../SiteLedger/Report?productTypeId=0&assemblyType=0&productMainGroupId=0&productSubGroupId=0&productId=0&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&customerId=" + $("#hdnCustomerId").val() + "&customerBranchId=" + $("#ddlCustomerBranch").val() + "&reportType=PDF";
    $('#lnkExport,#lnkExportSummary').attr('href', url);

    var urlSummary = "../SiteLedger/SummaryReport?productTypeId=0&assemblyType=0&productMainGroupId=0&productSubGroupId=0&productId=0&customerBranchId=0&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#lnkExportSummary').attr('href', urlSummary);
});

function BindProductTypeList() {
    $.ajax({
        type: "GET",
        url: "../Product/GetProductTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlProductType").append($("<option></option>").val(0).html("-Select Type-"));
            $.each(data, function (i, item) {
                $("#ddlProductType").append($("<option></option>").val(item.ProductTypeId).html(item.ProductTypeName));
            });
        },
        error: function (Result) {
            $("#ddlProductType").append($("<option></option>").val(0).html("-Select Type-"));
        }
    });
}
function BindProductMainGroupList() {
    $.ajax({
        type: "GET",
        url: "../Product/GetProductMainGroupList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlProductMainGroup").append($("<option></option>").val(0).html("-Select Main Group-"));
            $.each(data, function (i, item) {
                $("#ddlProductMainGroup").append($("<option></option>").val(item.ProductMainGroupId).html(item.ProductMainGroupName));
            });
        },
        error: function (Result) {
            $("#ddlProductMainGroup").append($("<option></option>").val(0).html("-Select Main Group-"));
        }
    });
}
function BindProductSubGroupList(productSubGroupId) {
    var productMainGroupId = $("#ddlProductMainGroup option:selected").val();
    $("#ddlProductSubGroup").val(0);
    $("#ddlProductSubGroup").html("");
    if (productMainGroupId != undefined && productMainGroupId != "" && productMainGroupId != "0") {
        var data = { productMainGroupId: productMainGroupId };
        $.ajax({
            type: "GET",
            url: "../Product/GetProductSubGroupList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
                $.each(data, function (i, item) {
                    $("#ddlProductSubGroup").append($("<option></option>").val(item.ProductSubGroupId).html(item.ProductSubGroupName));
                });
                $("#ddlProductSubGroup").val(productSubGroupId);
            },
            error: function (Result) {
                $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
            }
        });
    }
    else {

        $("#ddlProductSubGroup").append($("<option></option>").val(0).html("-Select Sub Group-"));
    }

}

function ClearFields() {

    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    $("#ddlCompanyBranch").val("0");
    $("#txtProductName").val("");
    $("#hdnProductId").val("0");
    $("#ddlProductMainGroup").val("0");
    $("#ddlProductSubGroup").val("0");
    $("#ddlProductType").val("0");
    $("#ddlAssemblyType").val("0");
    $("#ddlCustomerBranch").val("0");
    $("#txtCustomerName").val("");
    $("#divList").html("");
    var url = "../StockLedger/Report?productTypeId=0&assemblyType=0&productMainGroupId=0&productSubGroupId=0&productId=0&customerBranchId=0&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&customerId=" + $("#hdnCustomerId").val() + "&customerBranchId=" + $("#ddlCustomerBranch").val() + "&reportType=PDF";
    $('#lnkExport,#lnkExportSummary').attr('href', url);

    var urlSummary = "../StockLedger/SummaryReport?productTypeId=0&assemblyType=0&productMainGroupId=0&productSubGroupId=0&productId=0&customerBranchId=0&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#lnkExportSummary').attr('href', urlSummary);
    
}
function GenerateReportParameters() {
    var ddlProductSubGroupval=0;   
    if ($("#ddlProductMainGroup").val() != "0" || $("#ddlProductType").val() != "0" || $("#ddlAssemblyType").val() != "0" || $("#hdnProductId").val() != "0")
    {
        if ($("#ddlProductSubGroup").val() == null) {
            ddlProductSubGroupval = 0;
        }
        else {
            ddlProductSubGroupval = $("#ddlProductSubGroup").val();
        }
    }
    

    var url = "../SiteLedger/Report?productTypeId=" + $("#ddlProductType").val() + "&assemblyType=" + $("#ddlAssemblyType").val() + "&productMainGroupId=" + $("#ddlProductMainGroup").val() + "&productSubGroupId=" + ddlProductSubGroupval + "&productId=" + $("#hdnProductId").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&customerId=" + $("#hdnCustomerId").val() + "&customerBranchId=" + $("#ddlCustomerBranch").val() + "&reportType=PDF";
    $('#lnkExport').attr('href', url);
    //var urlSummary = "../StockLedger/SummaryReport?productTypeId=" + $("#ddlProductType").val() + "&assemblyType=" + $("#ddlAssemblyType").val() + "&productMainGroupId=" + $("#ddlProductMainGroup").val() + "&productSubGroupId=" + ddlProductSubGroupval + "&productId=" + $("#hdnProductId").val() + "&customerBranchId=" + $("#ddlCompanyBranch").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    //$('#lnkExportSummary').attr('href', urlSummary);
     
  
}

function GetSiteLedgerList() {
    var ddlProductType = $("#ddlProductType");
    var ddlAssemblyType = $("#ddlAssemblyType");
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var ddlProductSubGroup = $("#ddlProductSubGroup");
    var hdnProductId = $("#hdnProductId"); 
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var productType = 0; 
    var productMainGroup = 0;
    var productSubGroup = 0;

    if (ddlProductType.val() == null)
    {
        productType = 0;
    }
    else
    {
        productType = ddlProductType.val();
    }
    if (ddlProductMainGroup.val() == null) {
        productMainGroup = 0;
    }
    else {
        productMainGroup = ddlProductMainGroup.val();
    }

    if (ddlProductSubGroup.val() == null) {
        productSubGroup = 0;
    }
    else {
        productSubGroup = ddlProductSubGroup.val();
    }

    var requestData = { productTypeId: productType, assemblyType: ddlAssemblyType.val(), productMainGroupId: productMainGroup, productSubGroupId: productSubGroup, productId: hdnProductId.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val() };
    $.ajax({
        url: "../SiteLedger/GetSiteLedgerList",
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

function GetStockLedgerDrilDownList(obj) {

    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var id = $(obj).attr("id");
    var productId = id.split('_')[1];
    if ($("#divStockLedgerDrilDown_" + productId).css('display') == 'none') {
        $("#divStockLedgerDrilDown_" + productId).show();
    }
    else {
        $("#divStockLedgerDrilDown_" + productId).hide();
        return false;
    }

    var requestData = { productId: productId ,fromDate: txtFromDate.val(), toDate: txtToDate.val()  };
    $.ajax({
        url: "../StockLedger/GetStockLedgerDrilDownList",
        data: requestData,
        dataType: "html",
        asnc: true,
        type: "GET",
        error: function (err) {
            $("#divStockLedgerDrilDown_" + productId).html("");
            $("#divStockLedgerDrilDown_" + productId).html(err);
        },
        success: function (data) {
            $("#divStockLedgerDrilDown_" + productId).html("");
            $("#divStockLedgerDrilDown_" + productId).html(data);

        }
    });
}

function OpenPrintPopup() {
    $("#printModelStockSummary").modal(); 
    GenerateStockSummaryReports();
}

function OpenPrintPopupLedger() {
    $("#printModelSiteLedger").modal();
    GenerateSiteLedgerParameters();
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

function GenerateStockSummaryReports() {
    var ddlProductSubGroupval=0;
    if ($("#ddlProductMainGroup").val() != "0" || $("#ddlProductType").val() != "0" || $("#ddlAssemblyType").val() != "0" || $("#ddlCompanyBranch").val() != "0" || $("#hdnProductId").val() != "0") {
        if ($("#ddlProductSubGroup").val() == null) {
            ddlProductSubGroupval = 0;
        }
        else {
            ddlProductSubGroupval = $("#ddlProductSubGroup").val();
        }
    }


    var url = "../StockLedger/GenerateStockSummaryReport?productTypeId=" + $("#ddlProductType").val() + "&assemblyType=" + $("#ddlAssemblyType").val() + "&productMainGroupId=" + $("#ddlProductMainGroup").val() + "&productSubGroupId=" + ddlProductSubGroupval + "&productId=" + $("#hdnProductId").val() + "&customerBranchId=" + $("#ddlCompanyBranch").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#btnPdf').attr('href', url);
    var urlSummary = "../StockLedger/GenerateStockSummaryReport?productTypeId=" + $("#ddlProductType").val() + "&assemblyType=" + $("#ddlAssemblyType").val() + "&productMainGroupId=" + $("#ddlProductMainGroup").val() + "&productSubGroupId=" + ddlProductSubGroupval + "&productId=" + $("#hdnProductId").val() + "&customerBranchId=" + $("#ddlCompanyBranch").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=Excel";
    $('#btnExcel').attr('href', urlSummary);


}
function OpenPrintPopupSiteLedger(obj) {
    var id = $(obj).attr("id");
    var productId = id.split('_')[1];
    $("#hdnProductId").val(productId)
    $("#printModel").modal();
    GenerateSiteLedgerReports();
}


function ShowHideStockSummaryPrintOption() {
    var reportOption = $("#ddlPrintOption2").val();
    if (reportOption == "PDF") {
        $("#btnPdfStock").show();
        $("#btnExcelStock").hide();
    }
    else if (reportOption == "Excel") {
        $("#btnExcelStock").show();
        $("#btnPdfStock").hide();
    }
}

function GenerateSiteLedgerReports() {   
   
    var url = "../SiteLedger/GenerateSiteLedgerReport?productId=" + $("#hdnProductId").val() + "&fromDate=" + $("#hdnfromdate").val() + "&toDate=" + $("#hdntodate").val() + "&reportType=PDF";
    $('#btnPdfStock').attr('href', url);
    var urlSummary = "../StockLedger/GenerateStockLedgerReport?productId=" + $("#hdnProductId").val() + "&fromDate=" + $("#hdnfromdate").val() + "&toDate=" + $("#hdntodate").val() + "&reportType=Excel";
    $('#btnExcelStock').attr('href', urlSummary);


}

function ShowHideSiteLedgerPrintOption() {
    var reportOption = $("#ddlPrintOption1").val();
    if (reportOption == "PDF") {
        $("#btnPdfLedger").show();
        $("#btnExcelLedger").hide();
    }
    else if (reportOption == "Excel") {
        $("#btnExcelLedger").show();
        $("#btnPdfLedger").hide();
    }
}
function GenerateSiteLedgerParameters() {
    var ddlProductSubGroupval=0;
    if ($("#ddlProductMainGroup").val() != "0" || $("#ddlProductType").val() != "0" || $("#ddlAssemblyType").val() != "0" || $("#ddlCompanyBranch").val() != "0" || $("#hdnProductId").val() != "0") {
        if ($("#ddlProductSubGroup").val() == null) {
            ddlProductSubGroupval = 0;
        }
        else {
            ddlProductSubGroupval = $("#ddlProductSubGroup").val();
        }
    }


    var url = "../StockLedger/StockLedgerReport?productTypeId=" + $("#ddlProductType").val() + "&assemblyType=" + $("#ddlAssemblyType").val() + "&productMainGroupId=" + $("#ddlProductMainGroup").val() + "&productSubGroupId=" + ddlProductSubGroupval + "&productId=" + $("#hdnProductId").val() + "&customerBranchId=" + $("#ddlCompanyBranch").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#btnPdfLedger').attr('href', url);
    var urlSummary = "../StockLedger/StockLedgerReport?productTypeId=" + $("#ddlProductType").val() + "&assemblyType=" + $("#ddlAssemblyType").val() + "&productMainGroupId=" + $("#ddlProductMainGroup").val() + "&productSubGroupId=" + ddlProductSubGroupval + "&productId=" + $("#hdnProductId").val() + "&customerBranchId=" + $("#ddlCompanyBranch").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=Excel";
    $('#btnExcelLedger').attr('href', urlSummary);
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
                $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Customer Branch-"));
                $.each(data, function (i, item) {
                    $("#ddlCustomerBranch").append($("<option></option>").val(item.CustomerBranchId).html(item.BranchName));
                });
                $("#ddlCustomerBranch").val(customerBranchId);
            },
            error: function (Result) {
                $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Customer Branch-"));
            }
        });
    }
    else {
        $("#ddlCustomerBranch").append($("<option></option>").val(0).html("-Select Customer Branch-"));
    }
}
