$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });
    $("#txtSINNo").attr('readOnly', true);
    $("#txtSINDate").attr('readOnly', true);
    $("#txtRequisitionNo").attr('readOnly', true);
    $("#txtJobNo").attr('readOnly', true);
    $("#txtJobDate").attr('readOnly', true);
    $("#txtRefDate").attr('readOnly', true);
    $("#txtCreatedBy").attr('readOnly', true);
    $("#txtCreatedDate").attr('readOnly', true);
    $("#txtModifiedBy").attr('readOnly', true);
    $("#txtModifiedDate").attr('readOnly', true);
    $("#txtRejectedDate").attr('readOnly', true);
    $("#txtUOMName").attr('readOnly', true);
    $("#txtProductName").attr('readOnly', true);
    $("#txtProductCode").attr('readOnly', true);
    $("#txtProductShortDesc").attr('readOnly', true);
    $("#txtDiscountAmount").attr('readOnly', true);
    $("#txtTotalPrice").attr('readOnly', true);
    $("#txtBasicValue").attr('readOnly', true);
    $("#txtTaxPercentage").attr('readOnly', true);
    $("#txtTaxAmount").attr('readOnly', true);

    $("#txtQuantity").attr('readOnly', true);
    $("#txtAvailableStock").attr('readOnly', true);
    $("#txtIssuedQuantity").attr('readOnly', true);
    $("#txtBalanceQuantity").attr('readOnly', true);

    $("#txtTotalValue").attr('readOnly', true);
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
    $("#txtRequisitionDate").attr('readOnly', true);

    BindDocumentTypeList();
    BindCompanyBranchList();
    $("#txtSINDate,#txtRefDate,#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    });


    $("#ddlFromLocation").append($("<option></option>").val(0).html("-Select From Department-"));
    $("#ddlToLocation").append($("<option></option>").val(0).html("-Select To Department-"));
    $("#txtSearchFromDate,#txtSearchToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    });
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnsINId = $("#hdnsINId");
    if (hdnsINId.val() != "" && hdnsINId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetSINDetail(hdnsINId.val());

       }, 2000);
        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
            $("#chkstatus").attr('disabled', true);
            $(".editonly").hide();
            if ($(".editonly").hide()) {
                $('#lblRequisitionDate').removeClass("col-sm-3 control-label").addClass("col-sm-4 control-label");
            }
        }
        else {
            $("#btnSave").hide();
            $("#btnUpdate").show();
            $("#btnReset").show();
            $(".editonly").show();
        }
    }
    else {
        $("#btnSave").show();
        $("#btnUpdate").hide();
        $("#btnReset").show();
        $(".editonly").show();
    }

    var sinProducts = [];
    GetSINProductList(sinProducts);

    var sinDocuments = [];
    GetSINDocumentList(sinDocuments);


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

function BindCompanyBranchList() {
    $("#ddlCompanyBranch,#ddlSearchCompanyBranch").val(0);
    $("#ddlCompanyBranch,#ddlSearchCompanyBranch").html("");
    $.ajax({
        type: "GET",
        url: "../SIN/GetCompanyBranchList",
        data: {},
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlCompanyBranch,#ddlSearchCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
            $.each(data, function (i, item) {
                $("#ddlCompanyBranch,#ddlSearchCompanyBranch").append($("<option></option>").val(item.CompanyBranchId).html(item.BranchName));
            });
        },
        error: function (Result) {
            $("#ddlCompanyBranch,#ddlSearchCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
        }
    });
}
function BindFromLocationList(fromLocationId) {
    var companyBranchID = $("#ddlCompanyBranch option:selected").val();
    $("#ddlFromLocation").val(0);
    $("#ddlFromLocation").html("");
    $.ajax({
        type: "GET",
        url: "../SIN/GetFromLocationList",
        data: { companyBranchID: companyBranchID },
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlFromLocation").append($("<option></option>").val(0).html("-Select From Department-"));
            $.each(data, function (i, item) {
                $("#ddlFromLocation").append($("<option></option>").val(item.LocationId).html(item.LocationName));
            });

            $("#ddlFromLocation").val(fromLocationId);
        },
        error: function (Result) {
            $("#ddlFromLocation").append($("<option></option>").val(0).html("-Select From Department-"));
        }
    });
}

function BindToLocationList(toLocationId) {
    var companyBranchID = $("#ddlCompanyBranch option:selected").val();
    $("#ddlToLocation").val(0);
    $("#ddlToLocation").html("");
    $.ajax({
        type: "GET",
        url: "../SIN/GetFromLocationList",
        data: { companyBranchID: companyBranchID },
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlToLocation").append($("<option></option>").val(0).html("-Select To Department-"));
            $.each(data, function (i, item) {
                $("#ddlToLocation").append($("<option></option>").val(item.LocationId).html(item.LocationName));
                $("#ddlToLocation").val(toLocationId);
            });
        },
        error: function (Result) {
            $("#ddlToLocation").append($("<option></option>").val(0).html("-Select To Department-"));
        }
    });
}

function RemoveDocumentRow(obj) {
    if (confirm("Do you want to remove selected Document?")) {
        var row = $(obj).closest("tr");
        var STNDocId = $(row).find("#hdnSTNDocId").val();
        ShowModel("Alert", "Document Removed from List.");
        row.remove();
    }
}

function GetSINDocumentList(sinDocuments) {
    var hdnsinId = $("#hdnsINId");
    var requestData = { sinDocuments: sinDocuments, sinId: hdnsinId.val() };
    $.ajax({
        url: "../SIN/GetSINSupportingDocumentList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divDocumentList").html("");
            $("#divDocumentList").html(err);
        },
        success: function (data) {
            $("#divDocumentList").html("");
            $("#divDocumentList").html(data);
            ShowHideDocumentPanel(2);
        }
    });
}
function SaveDocument() {
    if (window.FormData !== undefined) {
        var uploadfile = document.getElementById('FileUpload1');
        var fileData = new FormData();
        if (uploadfile.value != '') {
            var fileUpload = $("#FileUpload1").get(0);
            var files = fileUpload.files;

            if (uploadfile.files[0].size > 50000000) {
                uploadfile.files[0].name.length = 0;
                ShowModel("Alert", "File is too big")
                uploadfile.value = "";
                return "";
            }

            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
        }


    } else {

        ShowModel("Alert", "FormData is not supported.")
        return "";
    }

    $.ajax({
        url: "../SIN/SaveSupportingDocument",
        type: "POST",
        asnc: false,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data  
        data: fileData,
        error: function () {
            ShowModel("Alert", "An error occured")
            return "";
        },
        success: function (result) {
            if (result.status == "SUCCESS") {
                var newFileName = result.message;

                var docEntrySequence = 0;
                var hdnDocumentSequence = $("#hdnDocumentSequence");
                var ddlDocumentType = $("#ddlDocumentType");
                var hdnSINDocId = $("#hdnSINDocId");
                var FileUpload1 = $("#FileUpload1");

                if (ddlDocumentType.val().trim() == "" || ddlDocumentType.val().trim() == "0") {
                    ShowModel("Alert", "Please select Document Type")
                    ddlDocumentType.focus();
                    return false;
                }

                if (FileUpload1.val() == undefined || FileUpload1.val() == "") {
                    ShowModel("Alert", "Please select File To Upload")
                    return false;
                }



                var sinDocumentList = [];
                if ((hdnDocumentSequence.val() == "" || hdnDocumentSequence.val() == "0")) {
                    docEntrySequence = 1;
                }
                $('#tblDocumentList tr').each(function (i, row) {
                    var $row = $(row);
                    var documentSequenceNo = $row.find("#hdnDocumentSequenceNo").val();
                    var hdnSINDocId = $row.find("#hdnSINDocId").val();
                    var documentTypeId = $row.find("#hdnDocumentTypeId").val();
                    var documentTypeDesc = $row.find("#hdnDocumentTypeDesc").val();
                    var documentName = $row.find("#hdnDocumentName").val();
                    var documentPath = $row.find("#hdnDocumentPath").val();

                    if (hdnSINDocId != undefined) {
                        if ((hdnDocumentSequence.val() != documentSequenceNo)) {

                            var stnDocument = {
                                SINDocId: hdnSINDocId,
                                DocumentSequenceNo: documentSequenceNo,
                                DocumentTypeId: documentTypeId,
                                DocumentTypeDesc: documentTypeDesc,
                                DocumentName: documentName,
                                DocumentPath: documentPath
                            };
                            sinDocumentList.push(stnDocument);
                            docEntrySequence = parseInt(docEntrySequence) + 1;
                        }
                        else if (hdnSINDocId.val() == sINDocId && hdnDocumentSequence.val() == documentSequenceNo) {
                            var stnDocument = {
                                SINDocId: hdnSINDocId.val(),
                                DocumentSequenceNo: hdnDocumentSequence.val(),
                                DocumentTypeId: ddlDocumentType.val(),
                                DocumentTypeDesc: $("#ddlDocumentType option:selected").text(),
                                DocumentName: newFileName,
                                DocumentPath: newFileName
                            };
                            sinDocumentList.push(stnDocument);
                        }
                    }
                });
                if ((hdnDocumentSequence.val() == "" || hdnDocumentSequence.val() == "0")) {
                    hdnDocumentSequence.val(docEntrySequence);
                }

                var stnDocumentAddEdit = {
                    SINDocId: hdnSINDocId.val(),
                    DocumentSequenceNo: hdnDocumentSequence.val(),
                    DocumentTypeId: ddlDocumentType.val(),
                    DocumentTypeDesc: $("#ddlDocumentType option:selected").text(),
                    DocumentName: newFileName,
                    DocumentPath: newFileName
                };
                sinDocumentList.push(stnDocumentAddEdit);
                hdnDocumentSequence.val("0");

                GetSINDocumentList(sinDocumentList);



            }
            else {
                ShowModel("Alert", result.message);
            }
        }
    });
}


function ShowHideDocumentPanel(action) {
    if (action == 1) {
        $(".documentsection").show();
    }
    else {
        $(".documentsection").hide();
        $("#ddlDocumentType").val("0");
        $("#hdnSTNDocId").val("0");
        $("#FileUpload1").val("");

        $("#btnAddDocument").show();
        $("#btnUpdateDocument").hide();
    }
}

function BindDocumentTypeList() {
    $.ajax({
        type: "GET",
        url: "../PO/GetDocumentTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlDocumentType").append($("<option></option>").val(0).html("-Select Document Type-"));
            $.each(data, function (i, item) {

                $("#ddlDocumentType").append($("<option></option>").val(item.DocumentTypeId).html(item.DocumentTypeDesc));
            });
        },
        error: function (Result) {
            $("#ddlDocumentType").append($("<option></option>").val(0).html("-Select Document Type-"));
        }
    });
}

function GetProductDetail(productId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetProductDetail",
        data: { productid: productId },
        dataType: "json",
        success: function (data) {
            $("#txtPrice").val(data.SalePrice);
            $("#txtUOMName").val(data.UOMName);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}
function AddProduct(action, hdnSequenceNo, txtProductName, hdnSINProductDetailId, hdnProductId,
                        txtProductCode, txtProductShortDesc, txtUOMName, txtQuantity, txtAvailableStock,
                        txtIssuedQuantity, txtBalanceQuantity, txtIssueQuantity) {


    var productEntrySequence = 0;
    var flag = true;
    /*assign value*/
    $("#hdnSequenceNo").val(hdnSequenceNo);
    $("#txtProductName").val(txtProductName);
    $("#hdnSINProductDetailId").val(hdnSINProductDetailId);
    $("#hdnProductId").val(hdnProductId);
    $("#txtProductCode").val(txtProductCode);
    $("#txtProductShortDesc").val(txtProductShortDesc);
    $("#txtUOMName").val(txtUOMName);
    $("#txtQuantity").val(txtQuantity);
    $("#txtAvailableStock").val(txtAvailableStock);
    $("#txtIssuedQuantity").val(txtIssuedQuantity);
    $("#txtBalanceQuantity").val(txtBalanceQuantity);
    $("#txtIssueQuantity").val(txtIssueQuantity);
    /*end section*/
    var hdnSequenceNo = $("#hdnSequenceNo");

    var txtProductName = $("#txtProductName");
    var hdnSINProductDetailId = $("#hdnSINProductDetailId");
    var hdnProductId = $("#hdnProductId");
    var txtProductCode = $("#txtProductCode");
    var txtProductShortDesc = $("#txtProductShortDesc");
    var txtUOMName = $("#txtUOMName");
    var txtQuantity = $("#txtQuantity");
    var txtAvailableStock = $("#txtAvailableStock");
    var txtIssuedQuantity = $("#txtIssuedQuantity");
    var txtBalanceQuantity = $("#txtBalanceQuantity");
    var txtIssueQuantity = $("#txtIssueQuantity");


    //if (txtQuantity.val().trim() == "" || txtQuantity.val().trim() == "0" || parseFloat(txtQuantity.val().trim()) <= 0) {
    //    ShowModel("Alert", "Please enter Requisition Quantity")
    //    return false;
    //}
    //if (txtAvailableStock.val().trim() == "" || txtAvailableStock.val().trim() == "0" || parseFloat(txtAvailableStock.val().trim()) <= 0) {
    //    ShowModel("Alert", "Stock is not avaiable")
    //    return false;
    //}
    //if (txtIssueQuantity.val().trim() == "" || txtIssueQuantity.val().trim() == "0" || parseFloat(txtIssueQuantity.val().trim()) <= 0) {
    //    ShowModel("Alert", "Please enter Issue Quantity")
    //    return false;
    //}
    //if (parseFloat(txtIssueQuantity.val().trim()) > parseFloat(txtBalanceQuantity.val().trim())) {
    //    ShowModel("Alert", "Please enter Issue Quantity less than or equal to Balance Quantity")
    //    return false;
    //}
    //if (parseFloat(txtIssueQuantity.val().trim()) > parseFloat(txtAvailableStock.val().trim())) {
    //    ShowModel("Alert", "Please enter Issue Quantity less than or equal to Avaiable Stock Quantity")
    //    return false;
    //}
    var sinProductList = [];
    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        productEntrySequence = 1;
    }

    $('#tblSINProductList tr').each(function (i, row) {
        var $row = $(row);
        var sequenceNo = $row.find("#hdnSequenceNo").val();
        var sinProductDetailId = $row.find("#hdnSINProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var quantity = $row.find("#hdnQuantity").val();
        var issuedQuantity = $row.find("#hdnIssuedQuantity").val();
        var balanceQuantity = $row.find("#hdnBalanceQuantity").val();
        var issueQuantity = $row.find("#hdnIssueQuantity").val();
        var availableStock = $row.find("#hdnAvailableStock").val()
        if (sinProductDetailId != undefined) {
            if (action == 1 || (hdnSequenceNo.val() != sequenceNo)) {

                if (productId == hdnProductId.val()) {
                    ShowModel("Alert", "Product already added!!!");
                    flag = false;
                    return false;
                }

                var sinProduct = {
                    SINProductDetailId: sinProductDetailId,
                    SequenceNo: sequenceNo,
                    ProductId: productId,
                    ProductName: productName,
                    ProductCode: productCode,
                    ProductShortDesc: productShortDesc,
                    UOMName: uomName,
                    Quantity: quantity,
                    IssuedQuantity: issuedQuantity,
                    BalanceQuantity: balanceQuantity,
                    IssueQuantity: issueQuantity,
                    AvailableStock: availableStock
                };
                sinProductList.push(sinProduct);
                productEntrySequence = parseInt(productEntrySequence) + 1;
            }
            else if (hdnProductId.val() == productId && hdnSequenceNo.val() == sequenceNo) {
                var sinProduct = {
                    SINProductDetailId: hdnSINProductDetailId.val(),
                    SequenceNo: hdnSequenceNo.val(),
                    ProductId: hdnProductId.val(),
                    ProductName: txtProductName.val(),
                    ProductCode: txtProductCode.val(),
                    ProductShortDesc: txtProductShortDesc.val(),
                    UOMName: txtUOMName.val(),
                    Quantity: txtQuantity.val(),
                    IssuedQuantity: txtIssuedQuantity.val(),
                    BalanceQuantity: txtBalanceQuantity.val(),
                    IssueQuantity: txtIssueQuantity.val(),
                    AvailableStock: txtAvailableStock.val()
                };
                sinProductList.push(sinProduct);

            }

        }

    });

    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        hdnSequenceNo.val(productEntrySequence);
    }
    if (action == 1) {
        var sinProductAddEdit = {
            SINProductDetailId: hdnSINProductDetailId.val(),
            SequenceNo: hdnSequenceNo.val(),
            ProductId: hdnProductId.val(),
            ProductName: txtProductName.val().trim(),
            ProductCode: txtProductCode.val().trim(),
            ProductShortDesc: txtProductShortDesc.val().trim(),
            UOMName: txtUOMName.val().trim(),
            Quantity: txtQuantity.val(),
            IssuedQuantity: txtIssuedQuantity.val(),
            BalanceQuantity: txtBalanceQuantity.val(),
            IssueQuantity: txtIssueQuantity.val()
        };
        sinProductList.push(sinProductAddEdit);
        hdnSequenceNo.val("0");
    }
    if (flag == true) {
        GetSINProductList(sinProductList);
    }


}
function GetSINProductList(sinProducts) {
    var hdnsinId = $("#hdnsINId");
    var requestData = { sinProducts: sinProducts, sinId: hdnsinId.val() };
    $.ajax({
        url: "../SIN/GetSINProductList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divProductList").html("");
            $("#divProductList").html(err);
        },
        success: function (data) {
            $("#divProductList").html("");
            $("#divProductList").html(data);


            //  CalculateGrossandNetValues();
            ShowHideProductPanel(2);
        }
    });
}


function EditProductRow(obj) {

    var row = $(obj).closest("tr");
    var sequenceNo = $(row).find("#hdnSequenceNo").val();
    var stnProductDetailId = $(row).find("#hdnSINProductDetailId").val();
    var productId = $(row).find("#hdnProductId").val();
    var productName = $(row).find("#hdnProductName").val();
    var productCode = $(row).find("#hdnProductCode").val();
    var productShortDesc = $(row).find("#hdnProductShortDesc").val();
    var uomName = $(row).find("#hdnUOMName").val();
    var quantity = $(row).find("#hdnQuantity").val();
    var issuedQuantity = $(row).find("#hdnIssuedQuantity").val();
    var balanceQuantity = $(row).find("#hdnBalanceQuantity").val();
    var issueQuantity = $(row).find("#hdnIssueQuantity").val();
    var availableStock = $(row).find("#hdnAvailableStock").val();

    $("#hdnSequenceNo").val(sequenceNo);
    $("#txtProductName").val(productName);
    $("#hdnSTNProductDetailId").val(stnProductDetailId);
    $("#hdnProductId").val(productId);
    $("#txtProductCode").val(productCode);
    $("#txtProductShortDesc").val(productShortDesc);
    $("#txtUOMName").val(uomName);
    $("#txtQuantity").val(quantity);
    $("#txtIssuedQuantity").val(issuedQuantity);
    $("#txtBalanceQuantity").val(balanceQuantity);
    $("#txtIssueQuantity").val(issueQuantity);

    $("#btnUpdateProduct").show();
    GetProductAvailableStock(productId);
    ShowHideProductPanel(1);
}
function GetProductAvailableStock(productId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetProductAvailableStock",
        data: { productid: productId, companyBranchId: $("#ddlCompanyBranch").val(), trnId: 0, trnType: "DC" },
        dataType: "json",
        success: function (data) {
            $("#txtAvailableStock").val(data);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}
function RemoveProductRow(obj) {
    if (confirm("Do you want to remove selected Product?")) {
        var row = $(obj).closest("tr");
        ShowModel("Alert", "Product Removed from List.");
        row.remove();

    }
}


function SaveData() {
    var txtSINNo = $("#txtSINNo");
    var hdnsINId = $("#hdnsINId");
    var txtSINDate = $("#txtSINDate");
    var txtRequisitionNo = $("#txtRequisitionNo");
    var hdnRequisitionId = $("#hdnRequisitionId");
    var txtRequisitionDate = $("#txtRequisitionDate");
    var hdnJobId = $("#hdnJobId");
    var txtJobNo = $("#txtJobNo");
    var txtJobDate = $("#txtJobDate");
    var ddlCompanyBranch = $("#ddlCompanyBranch");
    var hdnCustomerId = $("#hdnCustomerId");
    var hdnCustomerBranchId = $("#hdnCustomerBranchId");
    var txtEmployeeName = $("#txtEmployeeName");
    var ddlFromLocation = $("#ddlFromLocation");
    var ddlToLocation = $("#ddlToLocation");
    var hdnReceivedByUserId = $("#hdnReceivedByUserId");
    var txtRefNo = $("#txtRefNo");
    var txtRefDate = $("#txtRefDate");
    var txtRemarks1 = $("#txtRemarks1");
    var txtRemarks2 = $("#txtRemarks2");
    var ddlSINStatus = $("#ddlSINStatus");

    if (Date.parse(txtSINDate.val().trim()) < Date.parse(txtRequisitionDate.val().trim())) {
        ShowModel("Alert", "SIN date should be greater than or equal requisition date.")
        txtSINDate.focus();
        return false;
    }
    if (txtRequisitionNo.val().trim() == "" || txtRequisitionNo.val().trim() == "0") {
        ShowModel("Alert", "Please select Requisition")
        return false;
    }

    if (ddlCompanyBranch.val() == "" || ddlCompanyBranch.val() == "0") {
        ShowModel("Alert", "Please Select Location")
        return false;
    }

    if (ddlFromLocation.val() == "" || ddlFromLocation.val() == "0") {
        ShowModel("Alert", "Please Select From Department")
        return false;
    }
    if (ddlToLocation.val() == "" || ddlToLocation.val() == "0") {
        ShowModel("Alert", "Please Select To Department")
        ddlToLocation.focus();
        return false;
    }
    if (ddlFromLocation.val() == ddlToLocation.val()) {
        ShowModel("Alert", "From Department and To Department can not be same")
        return false;
    }

    if (txtEmployeeName.val().trim() == "" || txtEmployeeName.val().trim() == "0") {
        ShowModel("Alert", "Please Enter Receiver Name")
        txtEmployeeName.focus();
        return false;
    }

    

    var sinViewModel = {
        SINId: hdnsINId.val(),
        SINNo: txtSINNo.val().trim(),
        SINDate: txtSINDate.val().trim(),
        RequisitionId: hdnRequisitionId.val(),
        RequisitionNo: txtRequisitionNo.val().trim(),
        JobId: hdnJobId.val(),
        JobNo: txtJobNo.val().trim(),
        JobDate: txtJobDate.val().trim(),
        CompanyBranchId: ddlCompanyBranch.val(),
        CustomerId: hdnCustomerId.val(),
        CustomerBranchId: hdnCustomerBranchId.val(),
        EmployeeName: txtEmployeeName.val().trim(),
        FromLocationId: ddlFromLocation.val(),
        ToLocationId: ddlToLocation.val(),
        RefNo: txtRefNo.val(),
        RefDate: txtRefDate.val().trim(),
        Remarks1: txtRemarks1.val().trim(),
        Remarks2: txtRemarks2.val().trim(),
        ReceivedByUserId: hdnReceivedByUserId.val(),
        SINStatus: ddlSINStatus.val()
    };
    //Calculate issued quantity for store requisition product list-----------
    if (CalculateIssuedQuantity()) {
        var issueFlag = false;

        var sinProductList = [];
        $('#tblSINProductList tr:not(:has(th))').each(function (i, row) {
            var $row = $(row);
            var sinProductDetailId = $row.find("#hdnSINProductDetailId").val();
            var productId = $row.find("#hdnProductId").val();
            var productShortDesc = $row.find("#hdnProductShortDesc").val();
            var hdnissueQuantity = $row.find("#hdnIssueQuantity").val();
            var issueQuantity = $(this).closest('tr').find('.modify').val();
            if (sinProductDetailId != undefined) {
                if (parseFloat(issueQuantity) > 0) {
                    issueFlag = true;
                }

                var sinProduct = {
                    ProductId: productId,
                    ProductShortDesc: productShortDesc,
                    IssueQuantity: issueQuantity
                };
                sinProductList.push(sinProduct);
            }
        });
        if (issueFlag == false) {
            ShowModel("Alert", "Please Issue at least 1 Quantity.")
            return false;
        }
        var sinDocumentList = [];
        $('#tblDocumentList tr').each(function (i, row) {
            var $row = $(row);
            var sINDocId = $row.find("#hdnSINDocId").val();
            var documentSequenceNo = $row.find("#hdnDocumentSequenceNo").val();
            var documentTypeId = $row.find("#hdnDocumentTypeId").val();
            var documentTypeDesc = $row.find("#hdnDocumentTypeDesc").val();
            var documentName = $row.find("#hdnDocumentName").val();
            var documentPath = $row.find("#hdnDocumentPath").val();

            if (sINDocId != undefined) {
                var stnDocument = {
                    SINDocId: sINDocId,
                    DocumentSequenceNo: documentSequenceNo,
                    DocumentTypeId: documentTypeId,
                    DocumentTypeDesc: documentTypeDesc,
                    DocumentName: documentName,
                    DocumentPath: documentPath
                };
                sinDocumentList.push(stnDocument);
            }

        });
        var accessMode = "";
        if (ddlSINStatus.val() == "Final") {
            accessMode = 3;
        }
        else {
            accessMode = 2;
        }
        var accessMode = 1;//Add Mode
        if (hdnsINId.val() != null && hdnsINId.val() != 0) {
            accessMode = 2;//Edit Mode
        }
        var requestData = { sinViewModel: sinViewModel, sinProductDetailViewModel: sinProductList, sinDocuments: sinDocumentList };
        $.ajax({
            url: "../SIN/AddEditSIN?AccessMode=" + accessMode + "",
            cache: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (data) {
                if (data.status == "SUCCESS") {
                    SaveIndent();
                    setTimeout(function () {
                        ShowModel("Alert", data.message);
                    }, 2000);
                    ClearFields();
                    setTimeout(
                       function () {
                           window.location.href = "../SIN/AddEditSIN?sinId=" + data.trnId + "&AccessMode=3";
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
    else {
        
        //ShowModel("Alert", "Please Enter Issue Quantity");
        if ($("#hdnQtyMessage").val() != 0) {
            ShowModel("Alert", $("#hdnQtyMessage").val());
        }
        else {
            ShowModel("Alert", "Please Enter Issue Quantity");
        }
    }
}
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}
function ClearFields() {

    $("#txtSINNo").val("");
    $("#hdnsINId").val("0");
    $("#txtJobNo").val("");
    $("#hdnJobId").val("0");
    $("#ddlFromLocation").val("0");
    $("#ddlToLocation").val("0");
    $("#ddlCompanyBranch").val("0");
    $("#txtUser").val("");
    $("#hdnReceivedByUserId").val("0");
    $("#txtRefNo").val("");
    $("#txtRefDate").val("");
    $("#ddlSINStatus").val("Final");
    $("#btnSave").show();
    $("#btnUpdate").hide();


}
function ShowHideProductPanel(action) {
    if (action == 1) {
        $(".productsection").show();
    }
    else {
        $(".productsection").hide();
        $("#hdnSequenceNo").val("0");
        $("#txtProductName").val("");
        $("#hdnProductId").val("0");
        $("#hdnSINProductDetailId").val("0");
        $("#txtProductCode").val("");
        $("#txtProductShortDesc").val("");
        $("#txtUOMName").val("");
        $("#txtQuantity").val("");
        $("#txtAvailableStock").val("");
        $("#txtIssuedQuantity").val("");
        $("#txtBalanceQuantity").val("");
        $("#txtIssueQuantity").val("");
        $("#btnUpdateProduct").hide();
    }
}
function GetSINDetail(sinId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../SIN/GetSINDetail",
        data: { sinId: sinId },
        dataType: "json",
        success: function (data) {
            $("#txtSINNo").val(data.SINNo);
            $("#txtSINDate").val(data.SINDate);
            $("#hdnRequisitionId").val(data.RequisitionId),
            $("#txtRequisitionNo").val(data.RequisitionNo),
            $("#txtRequisitionDate").val(data.RequisitionDate),
            $("#hdnJobId").val(data.JobId),
            $("#txtJobNo").val(data.JobNo),
            //$("#txtJobDate").val(data.JobDate),

            $("#ddlCompanyBranch").val(data.CompanyBranchId);
            BindFromLocationList(data.FromLocationId);
            BindToLocationList(data.ToLocationId)
            $("#ddlFromLocation").val(data.FromLocationId);
            $("#ddlToLocation").val(data.ToLocationId);

            $("#hdnReceivedByUserId").val(data.ReceivedByUserId);
            $("#txtEmployeeName").val(data.EmployeeName);


            $("#txtRefNo").val(data.RefNo);
            $("#txtRefDate").val(data.RefDate);
            $("#ddlSINStatus").val(data.SINStatus);
            $("#txtRemarks1").val(data.Remarks1);
            $("#txtRemarks2").val(data.Remarks2);
            $("#txtCustomerName").val(data.CustomerName);
            $("#hdnCustomerId").val(data.CustomerId);
            GetCustomerBranchListByID(data.CustomerId, data.CustomerBranchId);
            $("#hdnCustomerBranchId").val(data.CustomerBranchId);
            $("#txtCustomerBranchName").val(data.CustomerBranchName);

            $("#divCreated").show();
            $("#txtCreatedBy").val(data.CreatedByUserName);
            $("#txtCreatedDate").val(data.CreatedDate);
            if (data.ModifiedByUserName != "") {
                $("#divModified").show();
                $("#txtModifiedBy").val(data.ModifiedByUserName);
                $("#txtModifiedDate").val(data.ModifiedDate);
            }
            if (data.SINStatus == "Final") {
                $("#btnUpdate").hide();
                $("#btnSaveIndent").hide();
                if (data.IsDeliveryChallan == "True"&&data.CustomerBranchId!=""&&data.CustomerBranchId!=0&&data.CustomerId!=""&&data.CustomerId!=0) {

                    $("#btnCreateChallan").show();
                }
                else { $("#btnCreateChallan").hide(); }
            }

            $("#btnAddNew").show();
            $("#btnPrint").show();


        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });


}


function BindSearchCompanyBranchList() {
    $("#ddlSearchCompanyBranch").val(0);
    $("#ddlSearchCompanyBranch").html("");
    $.ajax({
        type: "GET",
        url: "../DeliveryChallan/GetCompanyBranchList",
        data: {},
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlSearchCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
            $.each(data, function (i, item) {
                $("#ddlSearchCompanyBranch").append($("<option></option>").val(item.CompanyBranchId).html(item.BranchName));
            });
        },
        error: function (Result) {
            $("#ddlSearchCompanyBranch").append($("<option></option>").val(0).html("-Select Location-"));
        }
    });
}

function OpenInvoiceSearchPopup() {
    $("#SearchRequisitionModel").modal();
}
function SearchRequisition() {
    var txtSearchRequisitionNo = $("#txtSearchRequisitionNo");
    var txtSearchWorkOrderNo = $("#txtSearchWorkOrderNo");
    var ddlSearchRequisitionType = $("#ddlRequisitionType");
    var ddlSearchCompanyBranch = $("#ddlSearchCompanyBranch");
    var txtFromDate = $("#txtSearchFromDate");
    var txtToDate = $("#txtSearchToDate");

    var requestData = { requisitionNo: txtSearchRequisitionNo.val().trim(), workOrderNo: txtSearchWorkOrderNo.val().trim(), requisitionType: ddlSearchRequisitionType.val(), companyBranchId: ddlSearchCompanyBranch.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), displayType: "Popup", approvalStatus: "Final" };
    $.ajax({
        url: "../SIN/GetStoreRequisitionList",
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
function SelectRequisition(requisitionId, requisitionNo, requisitionDate, workOrderId, workOrderNo, workOrderDate, companyBranchId, locationId, customerName, customerId, customerBranchName, customerBranchId) {
    $("#txtRequisitionNo").val(requisitionNo);
    $("#hdnRequisitionId").val(requisitionId);
    $("#txtRequisitionDate").val(requisitionDate);
    $("#hdnJobId").val(workOrderId);
    $("#txtJobNo").val(workOrderNo);
    $("#txtJobDate").val(workOrderDate);
    $("#ddlCompanyBranch").val(companyBranchId);
    $("#txtCustomerName").val(customerName);
    $("#hdnCustomerId").val(customerId);
    $("#txtCustomerBranchName").val(customerBranchName);
    $("#hdnCustomerBranchId").val(customerBranchId);
    BindToLocationList(locationId);
    BindFromLocationList(0);
    $("#ddlToLocation").val(locationId);


    $("#SearchRequisitionModel").modal('hide');
    var sinProducts = [];
    GetRequisitionProductList(sinProducts);

}
function GetRequisitionProductList(sinProducts) {
    var hdnRequisitionId = $("#hdnRequisitionId");
    var requestData = { sinProducts: sinProducts, requisitionId: hdnRequisitionId.val() };
    $.ajax({
        url: "../SIN/GetStoreRequisitionProductList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divProductList").html("");
            $("#divProductList").html(err);
        },
        success: function (data) {
            $("#divProductList").html("");
            $("#divProductList").html(data);
            ShowHideProductPanel(2);
        }
    });
}

//function CalculateTotalCharges() {
//    var price = $("#txtPrice").val();
//    var quantity = $("#txtQuantity").val();
//    price = price == "" ? 0 : price;
//    quantity = quantity == "" ? 0 : quantity;
//    var totalPrice = parseFloat(price) * parseFloat(quantity);
//    $("#txtTotalPrice").val((totalPrice).toFixed(2));
//}

function CalculateGrossandNetValues() {
    var basicValue = 0;
    var taxValue = 0;
    $('#tblSTNProductList tr').each(function (i, row) {
        var $row = $(row);
        var stnProductDetailId = $row.find("#hdnSTNProductDetailId").val();
        var totalPrice = $row.find("#hdnTotalPrice").val();
        if (stnProductDetailId != undefined) {
            basicValue += parseFloat(totalPrice);
        }

    });
    var freightValue = $("#txtFreightValue").val() == "" ? "0" : $("#txtFreightValue").val();
    var loadingValue = $("#txtLoadingValue").val() == "" ? "0" : $("#txtLoadingValue").val();
    if (parseFloat(freightValue) <= 0) {
        freightValue = 0;
    }
    if (parseFloat(loadingValue) <= 0) {
        loadingValue = 0;
    }

    $("#txtBasicValue").val(basicValue.toFixed(2));

    $("#txtTotalValue").val(parseFloat(parseFloat(basicValue) + parseFloat(freightValue) + parseFloat(loadingValue)).toFixed(2));
}

function CalculatePendingQuantity() {
    var quantity = $("#txtQuantity").val();
    var issuedQuantity = $("#txtIssuedQuantity").val();
    var issueQuantity = $("#txtIssueQuantity").val();
    quantity = quantity == "" ? 0 : quantity;
    issuedQuantity = issuedQuantity == "" ? 0 : issuedQuantity;
    issueQuantity = issueQuantity == "" ? 0 : issueQuantity;
    var pendingQty = parseFloat(quantity) - (parseFloat(issuedQuantity) + parseFloat(issueQuantity));
    $("#txtPendingQuantity").val((pendingQty).toFixed(2));


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

function IssueQuantity(obj) {
    var row = $(obj).closest("tr");

    var hdnIssuedQuantity = $(row).children('td')[8].innerText;//$(row).find("#hdnIssuedQuantity").val();
    var issuedQuantity = $(row).find("#hdnIssuedQuantity").val();
    var balanceQuantity = $(row).find("#hdnBalanceQuantity").val();
    var pendingQuantity = $(row).find("#hdnBalanceQuantity").val();
    var availableStock = $(row).find("#hdnAvailableStock").val();
    if (hdnIssuedQuantity > balanceQuantity) {
        ShowModel("Alert", "Issue Quantity cannot be greater than the Pending Quantity.")
        return false;
    }
    if (parseFloat(availableStock) < parseFloat(hdnIssuedQuantity)) {
        ShowModel("Alert", "Stock is not available for the issued quantity. ")
        return false;
    }
    var changePendingQuantity = parseFloat(pendingQuantity) - parseFloat(hdnIssuedQuantity);
    $(row).children('td')[6].innerText = changePendingQuantity;
    $(row).find("#hdnBalanceQuantity").val(changePendingQuantity);
    var changeIssueQuantity = parseFloat(issuedQuantity) + parseFloat(hdnIssuedQuantity);
    $(row).find("#hdnIssuedQuantity").val(changeIssueQuantity);
    $(row).children('td')[5].innerText = changeIssueQuantity;



}

/**********Calculate issued quantity for store requisition product list***********/

function CalculateIssuedQuantity()
{
    var CalculateFlag = false;
    //Loop through product list table-----
    $('#tblSINProductList tr').each(function (i, row) {
        var input = $(this).closest('tr').find('.modify');
                    var txtIssueQuantity = input.val(); 
                    if (parseFloat(txtIssueQuantity) > 0 || $(row).find("#hdnIssueQuantity").val()>0) {
                        var issuedQuantity = $(row).find("#hdnIssuedQuantity").val();
                        var balanceQuantity = $(row).find("#hdnBalanceQuantity").val();
                      
                        if (parseFloat(txtIssueQuantity) > parseFloat(balanceQuantity)) {
                            ShowModel("Alert", "Issue Quantity cannot be greater than the Pending Quantity.")
                            $("#hdnQtyMessage").val("0");
                            $("#hdnQtyMessage").val("Issue Quantity cannot be greater than the Pending Quantity.");
                            input.val("0.00");
                            input.focus();
                            CalculateFlag = false;
                            return false;
                        }
                     
                        var availableStock = $(row).find("#hdnAvailableStock").val();
                        if (parseFloat(availableStock) < parseFloat(txtIssueQuantity)) {
                            ShowModel("Alert", "Stock is not available for issue. ");
                            $("#hdnQtyMessage").val("0");
                            $("#hdnQtyMessage").val("Stock is not available for issue.");
                            CalculateFlag = false;
                            return false;
                        }
                        CalculateFlag = true;
                    }
    });
    return CalculateFlag;
}

/**********End Calculate issued quantity***********/

/*Auto Generate Site Delivery Challan from SIN  , if customer is available for that SIN*/

/********** Start Section *********************/
var GetConsigneeDetailData = [];
function SaveSiteDeliveryChallanData() {
    var txtSiteChallanNo = "";
    var hdnSiteChallanId = "";
    var txtSiteChallanDate = $("#txtSINDate").val();
    var hdnInvoiceId = "";
    var txtInvoiceNo = "";
    var hdnSinId = $("#hdnsINId").val();
    var txtSinNo = $("#txtSINNo").val();
    var hdnCustomerId = $("#hdnCustomerId").val();
    var txtCustomerName = $("#txtCustomerName").val();
    var hdnConsigneeId = $("#hdnCustomerId").val();
    var txtConsigneeName = $("#txtCustomerName").val();
    var ddlSCustomerBranch = $("#hdnCustomerBranchId").val();
    var txtSAddress = ""
    var txtSCity = "";
    var ddlSCountry = "";
    var ddlSState = "";
    var txtSPinCode = "";
    var txtSTINNo = "";
    var txtSGSTNo = "";
    var txtSEmail = "";
    var txtSMobileNo = "";
    var txtSContactNo = "";
    var txtSFax = "";
    var txtSContactPerson = "";
    if (ddlSCustomerBranch != "" || ddlSCustomerBranch != 0) {
        var data = [];
        GetConsigneeDetailData = [];
        GetConsigneeDetail(hdnConsigneeId)
        data = GetConsigneeDetailData
        txtSAddress = data.PrimaryAddress;
        txtSCity = data.City;
        ddlSCountry = data.CountryId;
        ddlSState = data.StateId;
        txtSPinCode = data.PinCode;
        txtSTINNo = data.TINNo;
        txtSEmail = data.Email;
        txtSMobileNo = data.MobileNo;
        txtSContactNo = data.ContactNo;
        txtSFax = data.Fax;
        txtSGSTNo = data.GSTNo;
    }

    var ddlApprovalStatus = "Draft";
    var ddlCompanyBranch = $("#ddlCompanyBranch").val();
    var txtDispatchRefNo = "";
    var txtDispatchRefDate = "";
    var txtLRNo = "";
    var txtLRDate = "";
    var txtTransportVia = "";
    var txtNoOfPackets = "";
    var txtBasicValue = "";
    var txtFreightValue = "";
    var hdnFreightCGST_Perc = "";
    var txtFreightCGST_Amt = "";
    var hdnFreightSGST_Perc = "";
    var txtFreightSGST_Amt = "";
    var hdnFreightIGST_Perc = "";
    var txtFreightIGST_Amt = "";
    var txtLoadingValue = "";
    var hdnLoadingCGST_Perc = "";
    var txtLoadingCGST_Amt = "";
    var hdnLoadingSGST_Perc = "";
    var txtLoadingSGST_Amt = "";
    var hdnLoadingIGST_Perc = "";
    var txtLoadingIGST_Amt = "";
    var txtInsuranceValue = "";
    var hdnInsuranceCGST_Perc = "";
    var txtInsuranceCGST_Amt = "";
    var hdnInsuranceSGST_Perc = "";
    var txtInsuranceSGST_Amt = "";
    var hdnInsuranceIGST_Perc = "";
    var txtInsuranceIGST_Amt = "";
    var txtTotalValue = 0;
    var chkReverseChargeApplicable = "";
    var txtReverseChargeAmount = 0;
    var txtRemarks1 = "";
    var txtRemarks2 = "";
   

    if (txtCustomerName.trim() == "") {
        ShowModel("Alert", "Please Enter Party Name")
        txtCustomerName.focus();
        return false;
    }
    if (hdnCustomerId == "" || hdnCustomerId == "0") {
        ShowModel("Alert", "Please select Party from list")
        txtCustomerName.focus();
        return false;
    }
    if (txtConsigneeName.trim() == "") {
        ShowModel("Alert", "Please Enter Consignee Name")
        return false;
    }
    if (hdnConsigneeId == "" || hdnConsigneeId == "0") {
        ShowModel("Alert", "Please select Consignee from list")
        return false;
    }
     var loadingCGSTPerc = 0;
    if (txtLoadingCGST_Amt == "" || parseFloat(txtLoadingCGST_Amt) == 0) {
        loadingCGSTPerc = 0;
    }
    else {
        loadingCGSTPerc = 0;
    }

    var loadingSGSTPerc = 0;
    if (txtLoadingSGST_Amt == "" || parseFloat(txtLoadingSGST_Amt) == 0) {
        loadingSGSTPerc = 0;
    }
    else {
        loadingSGSTPerc = 0;
    }

    var loadingIGSTPerc = 0;
    if (txtLoadingIGST_Amt == "" || parseFloat(txtLoadingIGST_Amt) == 0) {
        loadingIGSTPerc = 0;
    }
    else {
        loadingIGSTPerc = 0;
    }

    var freightCGSTPerc = 0;
    if (txtFreightCGST_Amt == "" || parseFloat(txtFreightCGST_Amt) == 0) {
        freightCGSTPerc = 0;
    }
    else {
        freightCGSTPerc = 0;
    }

    var freightSGSTPerc = 0;
    if (txtFreightSGST_Amt == "" || parseFloat(txtFreightSGST_Amt) == 0) {
        freightSGSTPerc = 0;
    }
    else {
        freightSGSTPerc =0;
    }

    var freightIGSTPerc = 0;
    if (txtFreightIGST_Amt == "" || parseFloat(txtFreightIGST_Amt) == 0) {
        freightIGSTPerc = 0;
    }
    else {
        freightIGSTPerc = 0;
    }

    var insuranceCGSTPerc = 0;
    if (txtInsuranceCGST_Amt == "" || parseFloat(txtInsuranceCGST_Amt) == 0) {
        insuranceCGSTPerc = 0;
    }
    else {
        insuranceCGSTPerc = 0;
    }

    var insuranceSGSTPerc = 0;
    if (txtInsuranceSGST_Amt == "" || parseFloat(txtInsuranceSGST_Amt) == 0) {
        insuranceSGSTPerc = 0;
    }
    else {
        insuranceSGSTPerc = 0;
    }

    var insuranceIGSTPerc = 0;
    if (txtInsuranceIGST_Amt == "" || parseFloat(txtInsuranceIGST_Amt) == 0) {
        insuranceIGSTPerc = 0;
    }
    else {
        insuranceIGSTPerc = 0;
    }



    var siteChallanViewModel = {
        SiteChallanId: 0,
        SiteChallanNo: "",
        SiteChallanDate: txtSiteChallanDate.trim(),
        InvoiceId: 0,
        InvoiceNo: '',
        SinId: hdnSinId.trim(),
        SinNo: txtSinNo,
        CustomerId: hdnCustomerId.trim(),
        CustomerName: txtCustomerName.trim(),
        ConsigneeId: hdnConsigneeId.trim(),
        ConsigneeName: txtConsigneeName.trim(),
        ContactPerson: txtSContactPerson.trim(),
        ShippingContactPerson: txtSContactPerson.trim(),
        ShippingBillingAddress: txtSAddress.trim(),
        ShippingCity: txtSCity.trim(),
        ShippingStateId: ddlSState,
        ShippingCountryId: ddlSCountry,
        ShippingPinCode: txtSPinCode.trim(),
        ShippingTINNo: txtSGSTNo.trim(),
        ShippingEmail: txtSEmail.trim(),
        ShippingMobileNo: txtSMobileNo.trim(),
        ShippingContactNo: txtSContactNo.trim(),
        ShippingFax: txtSFax.trim(),
        CompanyBranchId: ddlCompanyBranch,
        DispatchRefNo: txtDispatchRefNo.trim(),
        DispatchRefDate: txtDispatchRefDate,
        LRNo: txtLRNo.trim(),
        LRDate: txtLRDate,

        TransportVia: txtTransportVia.trim(),
        NoOfPackets: txtNoOfPackets,
        ApprovalStatus: ddlApprovalStatus,
        BasicValue: txtBasicValue,
        FreightValue: txtFreightValue,
        FreightCGST_Perc: freightCGSTPerc,
        FreightCGST_Amt: txtFreightCGST_Amt,
        FreightSGST_Perc: freightSGSTPerc,
        FreightSGST_Amt: txtFreightSGST_Amt,
        FreightIGST_Perc: freightIGSTPerc,
        FreightIGST_Amt: txtFreightIGST_Amt,
        LoadingValue: txtLoadingValue,
        LoadingCGST_Perc: loadingCGSTPerc,
        LoadingCGST_Amt: txtLoadingCGST_Amt,
        LoadingSGST_Perc: loadingSGSTPerc,
        LoadingSGST_Amt: txtLoadingSGST_Amt,
        LoadingIGST_Perc: loadingIGSTPerc,
        LoadingIGST_Amt: txtLoadingIGST_Amt,
        InsuranceValue: txtInsuranceValue,
        InsuranceCGST_Perc: insuranceCGSTPerc,
        InsuranceCGST_Amt: txtInsuranceCGST_Amt,
        InsuranceSGST_Perc: insuranceSGSTPerc,
        InsuranceSGST_Amt: txtInsuranceSGST_Amt,
        InsuranceIGST_Perc: insuranceIGSTPerc,
        InsuranceIGST_Amt: txtInsuranceIGST_Amt,
        ReverseChargeApplicable:0,
        ReverseChargeAmount: txtReverseChargeAmount,
        TotalValue: txtTotalValue,
        Remarks1: "Remarks 1",
        Remarks2: "Remarks 2",
        CustomerBranchId: ddlSCustomerBranch
    };

    var siteChallanProductList = [];
    $('#tblSINProductList tr').each(function (i, row) {
        var $row = $(row);
        var siteChallanProductDetailId = 0;
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var price = 0.00;
        var quantity = $row.find("#hdnIssueQuantity").val();
        var discountPerc = 0.00;
        var discountAmount = 0.00;
        var productTaxId = 0;
        var productTaxPerc = 0;
        var productTaxAmount = 0;
        var productTaxName = "";


        var totalPrice = 0;
        var cGSTPerc = 0;
        var cGSTPercAmount = 0;
        var sGSTPerc = 0;
        var sGSTPercAmount = 0.00;
        var iGSTPerc = 0.00;
        var iGSTPercAmount = 0;
        var hsn_Code = 0;

        if (productId != undefined) {

            var siteChallanProduct = {
                SiteChallanProductDetailId: siteChallanProductDetailId,
                ProductId: productId,
                ProductName: productName,
                ProductCode: productCode,
                ProductShortDesc: productShortDesc,
                UOMName: uomName,
                Price: price,
                Quantity: quantity,
                DiscountPercentage: discountPerc,
                DiscountAmount: discountAmount,
                TaxId: productTaxId,
                TaxName: productTaxName,
                TaxPercentage: productTaxPerc,
                TaxAmount: productTaxAmount,
                TotalPrice: totalPrice,
                CGST_Perc: cGSTPerc,
                CGST_Amount: cGSTPercAmount,
                SGST_Perc: sGSTPerc,
                SGST_Amount: sGSTPercAmount,
                IGST_Perc: iGSTPerc,
                IGST_Amount: iGSTPercAmount,
                HSN_Code: hsn_Code
            };
            siteChallanProductList.push(siteChallanProduct);
        }
    });


    var challanTaxList = [];
    $('#tblTaxList tr').each(function (i, row) {
        var $row = $(row);
        var challanTaxDetailId = $row.find("#hdnChallanTaxDetailId").val();
        var taxId = $row.find("#hdnTaxId").val();
        var taxName = $row.find("#hdnTaxName").val();
        var taxPercentage = $row.find("#hdnTaxPercentage").val();
        var taxAmount = $row.find("#hdnTaxAmount").val();

        if (taxId != undefined) {
            var challanTax = {
                ChallanTaxDetailId: challanTaxDetailId,
                TaxId: taxId,
                TaxName: taxName,
                TaxPercentage: taxPercentage,
                TaxAmount: taxAmount
            };
            challanTaxList.push(challanTax);
        }

    });

    var siteChallanTermList = [];
    $('#tblTermList tr').each(function (i, row) {
        var $row = $(row);
        var siteChallanTermDetailId = $row.find("#hdnSiteChallanTermDetailId").val();
        var termDesc = $row.find("#hdnTermDesc").val();
        var termSequence = $row.find("#hdnTermSequence").val();

        if (termDesc != undefined) {
            var siteChallanTerm = {
                SiteChallanTermDetailId: siteChallanTermDetailId,
                TermDesc: termDesc,
                TermSequence: termSequence
            };
            siteChallanTermList.push(siteChallanTerm);
        }

    });


    var siteDeliveryChallanDocumentList = [];
    $('#tblDocumentList tr').each(function (i, row) {
        var $row = $(row);
        var siteDeliveryChallanDocId = $row.find("#hdnSiteDeliveryChallanDocId").val();
        var documentSequenceNo = $row.find("#hdnDocumentSequenceNo").val();
        var documentTypeId = $row.find("#hdnDocumentTypeId").val();
        var documentTypeDesc = $row.find("#hdnDocumentTypeDesc").val();
        var documentName = $row.find("#hdnDocumentName").val();
        var documentPath = $row.find("#hdnDocumentPath").val();

        if (siteDeliveryChallanDocId != undefined) {
            var siteDeliveryChallanDocument = {
                SiteDeliveryChallanDocId: siteDeliveryChallanDocId,
                DocumentSequenceNo: documentSequenceNo,
                DocumentTypeId: documentTypeId,
                DocumentTypeDesc: documentTypeDesc,
                DocumentName: documentName,
                DocumentPath: documentPath
            };
            siteDeliveryChallanDocumentList.push(siteDeliveryChallanDocument);
        }

    });


    var requestData = { siteChallanViewModel: siteChallanViewModel, siteChallanProducts: siteChallanProductList, siteChallanTerms: siteChallanTermList, siteDeliveryChallanDocuments: siteDeliveryChallanDocumentList };
    $.ajax({
        url: "../SiteDeliveryChallan/AddEditSiteDeliveryChallan",
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
                       window.location.href = "../SiteDeliveryChallan/AddEditSiteDeliveryChallan?SiteChallanId=" + data.trnId + "&AccessMode=2";
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

function GetConsigneeDetail(consigneeId) {
    var temp = null;
    $.ajax({
        type: "GET",
        async: false,
        url: "../Customer/GetCustomerDetail",
        data: { customerId: consigneeId },
        dataType: "json",
        success: function (data) {
            GetConsigneeDetailData = data;
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
    //return temp;
}

/********** End Section *********************/

/**************Auto Generate Purchase Indent from SIN************************/

/* This function used to  generate Auto Indent when Clicked AutoGenerateIndentButton w.r.t checkbox is checked*/
function SaveIndentData() {
    var ddlCompanyBranch = $("#ddlCompanyBranch");
    var txtSINDate = $("#txtSINDate");
    var hdnCustomerId = $("#hdnCustomerId");
    var hdnCustomerBranchId = $("#hdnCustomerBranchId");
    var indentStatus = 'Final';
    var hdnIndentByUserId = $("#hdnIndentByUserId");
    var txtRequisitionNo = $("#txtRequisitionNo");
    var hdnRequisitionId = $("#hdnRequisitionId");
    if (ddlCompanyBranch.val() == "" || ddlCompanyBranch.val() == "0") {
        ShowModel("Alert", "Please Select Location");
        return false;
    }

    if (txtRequisitionNo.val().trim() == "" || hdnRequisitionId.val() == "0")
    {
        ShowModel("Alert", "Please enter requisition no.");
        return false;
    }

    var purchaseIndentViewModel = {
        IndentId: 0,
        IndentDate: txtSINDate.val(),
        IndentType: "PO",
        RequisitionId: hdnRequisitionId.val(),
        RequisitionNo: txtRequisitionNo.val().trim(),
        CompanyBranchId: ddlCompanyBranch.val(),
        IndentByUserId: hdnIndentByUserId.val(),
        CustomerId: hdnCustomerId.val(),
        CustomerBranchId: hdnCustomerBranchId.val(),
        Remarks1: "Indent From Stock Issue Note!!",
        Remarks2: "Indent From Stock Issue Note!!",
        IndentStatus: indentStatus
    };

    var purchaseIndentProductList = [];
    $('#tblSINProductList tr').each(function (i, row) {
        var $row = $(row);
        var indentProductDetailId = $row.find("#hdnIndentProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var quantity = $row.find("#hdnBalanceQuantity").val();
        var chkIndent = $row.find("#chkIndent").prop("checked");

        if (productId != undefined) {
            if (quantity > 0 && chkIndent == true) {
                var indentProduct = {
                    ProductId: productId,
                    ProductShortDesc: productShortDesc,
                    Quantity: quantity
                };
                purchaseIndentProductList.push(indentProduct);
            }
        }
    });

    if (purchaseIndentProductList.length > 0) {
        var requestData = { purchaseIndentViewModel: purchaseIndentViewModel, purchaseIndentProducts: purchaseIndentProductList };
        $.ajax({
            url: "../PurchaseIndent/AddEditPurchaseIndent",
            cache: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (data) {
                if (data.status == "SUCCESS") {
                    ShowModel("Message", data.indentMessage);
                    var sinProducts = [];
                    GetRequisitionProductList(sinProducts);
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
    else {
        ShowModel("Message", "Plase Check at least 1 Product for Indent!!!");
        return false;
    }
}

/* This function used to save SIN to generate Auto Indent w.r.t checkbox is checked*/
function SaveIndent() {
    var ddlCompanyBranch = $("#ddlCompanyBranch");
    var txtSINDate = $("#txtSINDate");
    var hdnCustomerId = $("#hdnCustomerId");
    var hdnCustomerBranchId = $("#hdnCustomerBranchId");
    var indentStatus = 'Draft';
    var hdnIndentByUserId = $("#hdnIndentByUserId");
    if (ddlCompanyBranch.val() == "" || ddlCompanyBranch.val() == "0") {
        ShowModel("Alert", "Please Select Location")
        return false;
    }
    var purchaseIndentViewModel = {
        IndentId: 0,
        IndentDate: txtSINDate.val(),
        IndentType: "PO",
        CompanyBranchId: ddlCompanyBranch.val(),
        IndentByUserId: hdnIndentByUserId.val(),
        CustomerId: hdnCustomerId.val(),
        CustomerBranchId: hdnCustomerBranchId.val(),
        Remarks1: "Indent From Stock Issue Note!!",
        Remarks2: "Indent From Stock Issue Note!!",
        IndentStatus: indentStatus
    };

    var purchaseIndentProductList = [];
    $('#tblSINProductList tr').each(function (i, row) {
        var $row = $(row);
        var indentProductDetailId = $row.find("#hdnIndentProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var quantity = $row.find("#hdnBalanceQuantity").val();
        var chkIndent = $row.find("#chkIndent").prop("checked");

        if (productId != undefined) {
            if (quantity > 0 && chkIndent == true) {
                var indentProduct = {
                    ProductId: productId,
                    ProductShortDesc: productShortDesc,
                    Quantity: quantity
                };
                purchaseIndentProductList.push(indentProduct);
            }
        }
    });

    if (purchaseIndentProductList.length > 0) {
        var requestData = { purchaseIndentViewModel: purchaseIndentViewModel, purchaseIndentProducts: purchaseIndentProductList };
        $.ajax({
            url: "../PurchaseIndent/AddEditPurchaseIndent",
            cache: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (data) {
                if (data.status == "SUCCESS") {
                    ShowModel("Message", data.indentMessage);
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
/**************End Section for Auto Generate Indent from SIN ****************/

function ResetPage() {
    if (confirm("Are you sure to reset all fields?")) {
        window.location.href = "../SIN/AddEditSIN?accessMode=1";
    }
}

function CheckMasterPermission() {
    var IsAuthorized = false;
    var AccessMode = 1;
    var InterfaceId=12213;
    var RoleId = $("#hdnRoleId").val();
    $.ajax({
        type: "GET",
        url: "../Role/CheckMasterPermission",
        data: { roleId: RoleId, interfaceId: InterfaceId, accessMode: AccessMode },
        dataType: "json",
        asnc: true,
        success: function (data) {
            if (data != null) {
                if (data == true) {
                    IsAuthorized = true;
                    if (IsAuthorized == true) {
                        SaveIndentData();
                    }
                }
                else {
                    ShowModel('Alert', 'You are not authorized for this action.');
                }
            }
        },
        error: function (Result) {
            ShowModel('Alert', 'Problem in Request');
        }
    });

}