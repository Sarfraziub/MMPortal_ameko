$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });
    $("#txtCustomerSiteMRNNo").attr('readOnly', true);
    $("#txtCustomerSiteMRNDate").attr('readOnly', true);
    $("#txtRequisitionNo").attr('readOnly', true);
    $("#txtRequisitionDate").attr('readOnly', true);
    $("#txtRequisitionType").attr('readOnly', true);
    $("#ddlCompanyBranch").attr('readOnly', true);
    $("#txtGRDate").attr('readOnly', true);
    $("#txtInvoiceNo").attr('readOnly', true);
    $("#txtInvoiceDate").attr('readOnly', true);
    $("#txtSearchFromDate").attr('readOnly', true);
    $("#txtSearchToDate").attr('readOnly', true);
    $("#txtDispatchRefDate").attr('readOnly', true);
    $("#txtLRDate").attr('readOnly', true);
    $("#txtVendorCode").attr('readOnly', true);
    $("#txtRefDate").attr('readOnly', true);
    $("#txtCreatedBy").attr('readOnly', true);
    $("#txtCreatedDate").attr('readOnly', true); 
    $("#txtModifiedBy").attr('readOnly', true);
    $("#txtModifiedDate").attr('readOnly', true);
    $("#txtRejectedDate").attr('readOnly', true);
    $("#txtProductCode").attr('readOnly', true);
    $("#txtDiscountAmount").attr('readOnly', true);
    $("#txtTaxAmount").attr('readOnly', true);
    $("#txtTotalPrice").attr('readOnly', true);
    $("#txtBasicValue").attr('readOnly', true);
    $("#txtTaxPercentage").attr('readOnly', true);
    $("#txtTaxAmount").attr('readOnly', true);
    $("#txtTotalValue").attr('readOnly', true);
    $("#txtRejectQuantity").attr('readOnly', true);
    $("#txtUOMName").attr('readOnly', true);
    $("#txtQuantity").attr('readOnly', true);
   
    
    
    BindCompanyBranchList();
    BindSearchCompanyBranchList();
    BindDocumentTypeList();
    

    $("#txtVendorName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../PO/GetVendorAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.VendorName, value: item.VendorId, Address: item.Address, code: item.VendorCode };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtVendorName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtVendorName").val(ui.item.label);
            $("#hdnVendorId").val(ui.item.value);
            $("#txtVendorCode").val(ui.item.code);
            GetVendorDetail(ui.item.value);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtVendorName").val("");
                $("#hdnVendorId").val("0");
                $("#txtVendorCode").val("");
                ShowModel("Alert", "Please select Vendor from List")

            }
            return false;
        }

    })
   .autocomplete("instance")._renderItem = function (ul, item) {
       return $("<li>")
         .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.Address + "</div>")
         .appendTo(ul);
   };

 
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
            $("#txtProductShortDesc").val(ui.item.desc);
            $("#txtProductCode").val(ui.item.code);
           
            GetProductDetail(ui.item.value);
            GetProductPurchasePrice(productId);
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

 

    $("#txtTaxName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../DeliveryChallan/GetTaxAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.TaxName, value: item.TaxId, percentage: item.TaxPercentage };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtTaxName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtTaxName").val(ui.item.label);
            $("#hdnTaxId").val(ui.item.value);
            $("#txtTaxPercentage").val(ui.item.percentage);

            if (parseFloat($("#txtBasicValue").val()) > 0) {
                var taxAmount = (parseFloat($("#txtBasicValue").val()) * (parseFloat($("#txtTaxPercentage").val()) / 100));
                $("#txtTaxAmount").val(taxAmount.toFixed(2));
            }
            else {
                $("#txtTaxAmount").val("0");
            }
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtTaxName").val("");
                $("#hdnTaxId").val("0");
                $("#txtTaxPercentage").val("0");
                $("#txtTaxAmount").val("0");
                ShowModel("Alert", "Please select Tax from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + "</b></div>")
      .appendTo(ul);
};


    $("#txtProductTaxName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../DeliveryChallan/GetTaxAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.TaxName, value: item.TaxId, percentage: item.TaxPercentage };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtProductTaxName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtProductTaxName").val(ui.item.label);
            $("#hdnProductTaxId").val(ui.item.value);
            $("#hdnProductTaxPerc").val(ui.item.percentage);
           // CalculateTotalCharges();
            //if (parseFloat($("#txtBasicValue").val()) > 0) {
            //    var taxAmount = (parseFloat($("#txtBasicValue").val()) * (parseFloat($("#txtTaxPercentage").val()) / 100));
            //    $("#txtProductTaxAmount").val(taxAmount.toFixed(2));
            //}
            //else {
            //    $("#txtProductTaxAmount").val("0");
            //}
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtProductTaxName").val("");
                $("#hdnProductTaxId").val("0");
                $("#hdnProductTaxPerc").val("0");
                $("#txtProductTaxAmount").val("0");
                ShowModel("Alert", "Please select Tax from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + "</b></div>")
      .appendTo(ul);
};

    $("#txtChallanDate,#txtLRDate,#txtDispatchRefDate,#txtGRDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
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
    $("#txtMRNDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        maxDate: '0',
        onSelect: function (selected) {

        }
    });
 var hdnAccessMode = $("#hdnAccessMode");
 var hdncustomerSiteMrnId = $("#hdncustomerSiteMrnId");
 if (hdncustomerSiteMrnId.val() != "" && hdncustomerSiteMrnId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetCustomerSiteMRNDetail(hdncustomerSiteMrnId.val());
           
       }, 2000);
        //var vendord = $("#hdnCustomerId").val();
    //    BindCustomerBranchList(customerId);
      

        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
            $("#chkstatus").attr('disabled', true);
            $("#btnSearchInvoice").attr("onclick", "");
            $("#btnAddNewProduct").hide();
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

    var customerSiteMrnProducts = [];
    GetCustomerSiteMRNProductList(customerSiteMrnProducts);
    var customerSiteMrnDocuments = [];
    GetCustomerSiteMRNDocumentList(customerSiteMrnDocuments);
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
function BindSearchCompanyBranchList() {
    $("#ddlSearchCompanyBranch").val(0);
    $("#ddlSearchCompanyBranch").html("");
    $.ajax({
        type: "GET",
        url: "../SIN/GetCompanyBranchList",
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
function RemoveDocumentRow(obj) {
    if (confirm("Do you want to remove selected Document?")) {
        var row = $(obj).closest("tr");
        var poDocId = $(row).find("#hdnPODocId").val();
        ShowModel("Alert", "Document Removed from List.");
        row.remove();
    }
}

function GetCustomerSiteMRNDocumentList(customerSiteMrnDocuments) {
    var hdncustomerSiteMrnId = $("#hdncustomerSiteMrnId");
    var requestData = { customerSiteMrnDocuments: customerSiteMrnDocuments, customerSiteMrnId: hdncustomerSiteMrnId.val() };
    $.ajax({
        url: "../CustomerSiteMRN/GetCustomerSiteMRNSupportingDocumentList",
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
            localSiteMrn();
        }
    });

  
}



function SaveDocument() {
    if ($("#ddlDocumentType").val() == "0")
    {
        ShowModel("Alert", "Please Select Document Type.")
        $("#FileUpload1").val('');
        return "";       
    }
    else
    {
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
            else {
                ShowModel("Alert", "Please Select File")
                return false;
            }

        } else {

            ShowModel("Alert", "FormData is not supported.")
            return "";
        }
        
    }
   

    $.ajax({
        url: "../CustomerSiteMRN/SaveSupportingDocument",
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
                var hdnCustomerSiteMrnDocId = $("#hdnCustomerSiteMrnDocId");
                var FileUpload1 = $("#FileUpload1");

                if (ddlDocumentType.val() == "" || ddlDocumentType.val() == "0") {
                    ShowModel("Alert", "Please select Document Type")
                    ddlDocumentType.focus();
                    return false;
                }

                if (FileUpload1.val() == undefined || FileUpload1.val() == "") {
                    ShowModel("Alert", "Please select File To Upload")
                    return false;
                }



                var customerSiteMrnDocumentList = [];
                if ((hdnDocumentSequence.val() == "" || hdnDocumentSequence.val() == "0")) {
                    docEntrySequence = 1;
                }
                $('#tblDocumentList tr').each(function (i, row) {
                    var $row = $(row);
                    var documentSequenceNo = $row.find("#hdnDocumentSequenceNo").val();
                    var hdnCustomerSiteMrnDocId = $row.find("#hdnCustomerSiteMrnDocId").val();
                    var documentTypeId = $row.find("#hdnDocumentTypeId").val();
                    var documentTypeDesc = $row.find("#hdnDocumentTypeDesc").val();
                    var documentName = $row.find("#hdnDocumentName").val();
                    var documentPath = $row.find("#hdnDocumentPath").val();

                    if (hdnCustomerSiteMrnDocId != undefined) {
                        if ((hdnDocumentSequence.val() != documentSequenceNo)) {



                            var customerSiteMrnDocument = {
                                CustomerSiteMRNDocId: hdnCustomerSiteMrnDocId,
                                DocumentSequenceNo: documentSequenceNo,
                                DocumentTypeId: documentTypeId,
                                DocumentTypeDesc: documentTypeDesc,
                                DocumentName: documentName,
                                DocumentPath: documentPath
                            };
                            customerSiteMrnDocumentList.push(customerSiteMrnDocument);
                            docEntrySequence = parseInt(docEntrySequence) + 1;
                        }
                        else if (hdnCustomerSiteMrnDocId.val() == customerSiteMRNDocId && hdnDocumentSequence.val() == documentSequenceNo) {
                            var customerSiteMrnDocument = {
                                CustomerSiteMRNDocId: hdnCustomerSiteMrnDocId.val(),
                                DocumentSequenceNo: hdnDocumentSequence.val(),
                                DocumentTypeId: ddlDocumentType.val(),
                                DocumentTypeDesc: $("#ddlDocumentType option:selected").text(),
                                DocumentName: newFileName,
                                DocumentPath: newFileName
                            };
                            customerSiteMrnDocumentList.push(customerSiteMrnDocument);
                        }
                    }
                });
                if ((hdnDocumentSequence.val() == "" || hdnDocumentSequence.val() == "0")) {
                    hdnDocumentSequence.val(docEntrySequence);
                }

                var customerSiteMrnDocumentAddEdit = {
                    CustomerSiteMRNDocId: hdnCustomerSiteMrnDocId.val(),
                    DocumentSequenceNo: hdnDocumentSequence.val(),
                    DocumentTypeId: ddlDocumentType.val(),
                    DocumentTypeDesc: $("#ddlDocumentType option:selected").text(),
                    DocumentName: newFileName,
                    DocumentPath: newFileName
                };
                customerSiteMrnDocumentList.push(customerSiteMrnDocumentAddEdit);
                hdnDocumentSequence.val("0");

                GetCustomerSiteMRNDocumentList(customerSiteMrnDocumentList);

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
        $("#hdnCustomerSiteMrnDocId").val("0");
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
function AddProduct(action) {
    var productEntrySequence = 0;
    var flag = true;
    var hdnSequenceNo = $("#hdnSequenceNo");
    var txtProductName = $("#txtProductName");
    var hdnCustomerSiteMRNProductDetailId = $("#hdnCustomerSiteMRNProductDetailId");
    var hdnProductId = $("#hdnProductId");
    var txtProductCode = $("#txtProductCode");
    var txtProductShortDesc = $("#txtProductShortDesc"); 
    var txtPrice = $("#txtPrice");
    var txtUOMName = $("#txtUOMName");
    var txtQuantity = $("#txtQuantity");
    var txtReceivedQuantity = $("#txtReceivedQuantity");
    var txtAcceptQuantity = $("#txtAcceptQuantity");
    var txtRejectQuantity = $("#txtRejectQuantity");
    //var hdnReceivedatSitefromReq=$()
    if (txtProductName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Product Name")
        txtProductName.focus();
        return false;
    }
    if (hdnProductId.val().trim() == "" || hdnProductId.val().trim() == "0") {
        ShowModel("Alert", "Please select Product from list")
        hdnProductId.focus();
        return false;
    }
    //if (txtPrice.val().trim() == "" || txtPrice.val().trim() == "0" || parseFloat(txtPrice.val().trim()) <= 0) {
    //    ShowModel("Alert", "Please enter Price")
    //    txtPrice.focus();
    //    return false;
    //}
    
    //if (txtQuantity.val().trim() == "" || txtQuantity.val().trim() == "0" || parseFloat(txtQuantity.val().trim()) <= 0) {
    //    ShowModel("Alert", "Please enter Order Quantity")
    //    txtQuantity.focus();
    //    return false;
    //}
    
    if (txtReceivedQuantity.val().trim() == "" || txtReceivedQuantity.val().trim() == "0" || parseFloat(txtReceivedQuantity.val().trim()) <= 0) {
        ShowModel("Alert", "Please enter Received Quantity")
        txtReceivedQuantity.focus();
        return false;
    }

    if (txtAcceptQuantity.val().trim() == "") {
        ShowModel("Alert", "Please enter Accept Quantity")
        txtAcceptQuantity.focus();
        return false;
    }
    var customerSiteMrnProductList = [];
    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        productEntrySequence = 1;
    }
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var sequenceNo = $row.find("#hdnSequenceNo").val();
        var customerSiteMrnProductDetailId = $row.find("#hdnCustomerSiteMRNProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var price = $row.find("#hdnPrice").val();
        var quantity = $row.find("#hdnQuantity").val();
        var totalReceivedQuantityatSite = $row.find("#hdnTotalReceivedQtyatSite").val();
        var receivedQuantity = $row.find("#hdnCurrentRcvdQuantity").val();
        var acceptQuantity = $row.find("#hdnAcceptQuantity").val();
        var newReceivedQuantity = $row.find("#hdnNewReceivedQty").val();
        var rejectQuantity = $row.find("#hdnRejectQuantity").val();
        if (productName != undefined) {
            if (action == 1 || (hdnSequenceNo.val() != sequenceNo)) {

                if (productId == hdnProductId.val()) {
                    ShowModel("Alert", "Product already added!!!")
                    txtProductName.focus();
                    flag = false;
                    return false;
                }

                var customerSiteMrnProduct = {
                    CustomerSiteMRNProductDetailId: customerSiteMrnProductDetailId,
                    SequenceNo: sequenceNo,
                    ProductId: productId,
                    ProductName: productName,
                    ProductCode: productCode,
                    ProductShortDesc: productShortDesc,
                    UOMName: uomName,
                    Price: price,
                    Quantity: quantity,
                    TotalReceivedQuantity: totalReceivedQuantityatSite,
                    ReceivedQuantity:receivedQuantity,
                    AcceptQuantity: acceptQuantity,
                    RejectQuantity:rejectQuantity
                };
                customerSiteMrnProductList.push(customerSiteMrnProduct);
                productEntrySequence = parseInt(productEntrySequence) + 1;
            }
            else if (hdnCustomerSiteMRNProductDetailId.val() == customerSiteMrnProductDetailId && hdnSequenceNo.val() == sequenceNo)
            {
                var customerSiteMrnProduct = {
                    CustomerSiteMRNProductDetailId: hdnCustomerSiteMRNProductDetailId.val(),
                    SequenceNo: hdnSequenceNo.val(),
                    ProductId: hdnProductId.val(),
                    ProductName: txtProductName.val().trim(),
                    ProductCode: txtProductCode.val().trim(),
                    ProductShortDesc: txtProductShortDesc.val().trim(),
                    Price: txtPrice.val().trim(),
                    UOMName: txtUOMName.val().trim(),
                    Quantity: txtQuantity.val().trim(),
                    TotalReceivedQuantity: totalReceivedQuantityatSite,
                    ReceivedQuantity: txtReceivedQuantity.val().trim(),
                    AcceptQuantity: txtAcceptQuantity.val().trim(),
                    NewReceivedQuantity: txtReceivedQuantity.val().trim(),
                    RejectQuantity: txtRejectQuantity.val().trim()
                };
                customerSiteMrnProductList.push(customerSiteMrnProduct);
                hdnSequenceNo.val("0");
            }
        }
    });
    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        hdnSequenceNo.val(productEntrySequence);
    }
    if (action == 1) {
        var customerSiteMrnProductAddEdit = {
            CustomerSiteMRNProductDetailId: hdnCustomerSiteMRNProductDetailId.val(),
            SequenceNo: hdnSequenceNo.val(),
            ProductId: hdnProductId.val(),
            ProductName: txtProductName.val().trim(),
            ProductCode: txtProductCode.val().trim(),
            ProductShortDesc: txtProductShortDesc.val().trim(),
            Price: txtPrice.val().trim(),
            UOMName: txtUOMName.val().trim(),
            Quantity: txtQuantity.val().trim(),
            ReceivedQuantity: txtReceivedQuantity.val().trim(),
            AcceptQuantity: txtAcceptQuantity.val().trim(),
            RejectQuantity: txtRejectQuantity.val().trim()
        };
        customerSiteMrnProductList.push(customerSiteMrnProductAddEdit);
        hdnSequenceNo.val("0");
    }
    if (flag == true) {
        GetCustomerSiteMRNProductList(customerSiteMrnProductList);
    }
    
}
function GetCustomerSiteMRNProductList(customerSiteMrnProducts) {
    var hdncustomerSiteMrnId = $("#hdncustomerSiteMrnId");
    var requestData = { customerSiteMrnProducts: customerSiteMrnProducts, customerSiteMrnId: hdncustomerSiteMrnId.val() };
    $.ajax({
        url: "../CustomerSiteMRN/GetCustomerSiteMRNProductList",
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

function GetStorRequisitionProductList(customerSiteMrnProducts) {
    var hdncustomerSiteMrnId = $("#hdncustomerSiteMrnId");
    var requestData = { customerSiteMrnProducts: customerSiteMrnProducts, customerSiteMrnId: hdncustomerSiteMrnId.val() };
    $.ajax({
        url: "../CustomerSiteMRN/GetStoreRequisitionProductList",
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


function EditProductRow(obj) { 
    var row = $(obj).closest("tr");
    var challanProductDetailId = $(row).find("#hdnCustomerSiteMRNProductDetailId").val();
    var sequenceNo = $(row).find("#hdnSequenceNo").val();
    var productId = $(row).find("#hdnProductId").val();
    var productName = $(row).find("#hdnProductName").val();
    var productCode = $(row).find("#hdnProductCode").val();
    var productShortDesc = $(row).find("#hdnProductShortDesc").val();
    var uomName = $(row).find("#hdnUOMName").val();
    var price = $(row).find("#hdnPrice").val();
    var quantity = $(row).find("#hdnQuantity").val();
    var hdnReceivedatSitefromRequisition = $(row).find("hdnTotalReceivedQtyatSite").val();
    var receivedQuantity = $(row).find("#hdnCurrentRcvdQuantity").val();
    var acceptQuantity = $(row).find("#hdnAcceptQuantity").val();
    var rejectQuantity = $(row).find("#hdnRejectQuantity").val();
    
    $("#txtProductName").val(productName);
    $("#hdnSequenceNo").val(sequenceNo);
    $("#hdnCustomerSiteMRNProductDetailId").val(challanProductDetailId);
    $("#hdnProductId").val(productId);
    $("#txtProductCode").val(productCode);
    $("#txtProductShortDesc").val(productShortDesc);
    $("#txtUOMName").val(uomName);
    $("#txtPrice").val(price);
    $("#txtQuantity").val(quantity);
    GetProductPurchasePrice(productId);
    //$("#txtPrice").val(0.00);
    $("#hdnReceivedatSitefromReq").val(hdnReceivedatSitefromRequisition);

    $("#txtRejectQuantity").val(rejectQuantity);
    $("#txtAcceptQuantity").val(acceptQuantity);
    $("#txtReceivedQuantity").val(receivedQuantity);
  
    $("#btnAddProduct").hide();
    $("#btnUpdateProduct").show();
    ShowHideProductPanel(1);
}

function RemoveProductRow(obj) {
    if (confirm("Do you want to remove selected Product?")) {
        var row = $(obj).closest("tr");
        var challanProductDetailId = $(row).find("#hdnChallanProductDetailId").val();
        ShowModel("Alert", "Product Removed from List.");
        row.remove();

    }
}


function SaveData() {
    var txtCustomerSiteMRNNo = $("#txtCustomerSiteMRNNo");
    var hdncustomerSiteMrnId = $("#hdncustomerSiteMrnId");
    var txtCustomerSiteMRNDate = $("#txtCustomerSiteMRNDate");
    var txtGRNo = $("#txtGRNo") ;
    var txtGRDate = $("#txtGRDate");
    var hdnRequisitionId = $("#hdnRequisitionId");
    var txtDCNo = $("#txtDCNo");
    var hdnVendorId = $("#hdnVendorId");
    var txtVendorName = $("#txtVendorName");

    var txtSAddress = $("#txtSAddress");
    var txtSCity = $("#txtSCity");
    var ddlSCountry = $("#ddlSCountry");
    var ddlSState = $("#ddlSState");
    var txtSPinCode = $("#txtSPinCode");
    var txtSTINNo = $("#txtSTINNo");
    var txtSEmail = $("#txtSEmail");
    var txtSMobileNo = $("#txtSMobileNo");
    var txtSContactNo = $("#txtSContactNo");
    var txtSFax = $("#txtSFax");
    var txtSContactPerson = $("#txtSContactPerson");

    var ddlCompanyBranch = $("#ddlCompanyBranch");

    var txtDispatchRefNo = $("#txtDispatchRefNo") == "" ? "0" : $("#txtDispatchRefNo");
    var txtDispatchRefDate = $("#txtDispatchRefDate") == "" ? "0" : $("#txtDispatchRefDate");
    var txtLRNo = $("#txtLRNo") == "" ? "0" : $("#txtLRNo");
    var txtLRDate = $("#txtLRDate") == "" ? "0" : $("#txtLRDate");
    var txtTransportVia = $("#txtTransportVia") == "" ? "0" : $("#txtTransportVia");
    var txtNoOfPackets = $("#txtNoOfPackets") == "" ? "0" : $("#txtNoOfPackets");
    var txtRemarks1 = $("#txtRemarks1") == "" ? "" : $("#txtRemarks1");
    var txtRemarks2 = $("#txtRemarks2") == "" ? "" : $("#txtRemarks2");

    var txtCustomerName = $("#txtCustomerName");
    var hdnCustomerId = $("#hdnCustomerId");
    var ddlCustomerBranch = $("#ddlCustomerBranch");

    var ddlApprovalStatus = $("#ddlApprovalStatus");
    var hdnIsLocal = $("#hdnIsLocal");
    //if (txtVendorName.val().trim() == "") {
    //    ShowModel("Alert", "Please Enter Vendor Name")
    //    txtVendorName.focus();
    //    return false;
    //}
    //if (hdnVendorId.val() == "" || hdnVendorId.val() == "0") {
    //    ShowModel("Alert", "Please select Vendor from list")
    //    txtSearchVendorName.focus();
    //    return false;
    //}
    if(hdnIsLocal.val()!="true"){
    if (hdnRequisitionId.val() == "" || hdnRequisitionId.val() == "0") {
        ShowModel("Alert", "Please select Requisition No")
        $("#txtRequisitionNo").focus();
        return false;
    }
    }
    if (txtDCNo.val() == "" || txtDCNo.val() == "0") {
        ShowModel("Alert", "Please enter delivery challan no")
        txtDCNo.focus();
        return false;
    }
  
    if (ddlCompanyBranch.val() == "" || ddlCompanyBranch.val() == "0") {
        ShowModel("Alert", "Please select Received At Location")
        ddlCompanyBranch.focus();
        return false;
    }
     
    if (txtCustomerName.val() == "") {
        ShowModel("Alert", "Please enter customer name")
        txtCustomerName.focus();
        return false;
    }

    if (ddlCustomerBranch.val() == "" || ddlCustomerBranch.val() == "0" || ddlCustomerBranch.val()==null) {
        ShowModel("Alert", "Please select Received At customer branch site")
        ddlCustomerBranch.focus();
        return false;
    }

    //if ($("#hdnPOFlag").val() == 1 && $("#txtPONo").val()=="") {
    //    ShowModel("Alert", "Please enter PO number")
    //    $("#txtPONo").focus();
    //    return false;
    //}

    

    var CustomerSiteMRNViewModel = {
        CustomerSiteMRNId: hdncustomerSiteMrnId.val(),
        CustomerSiteMRNNo: txtCustomerSiteMRNNo.val().trim(),
        CustomerSiteMRNDate: txtCustomerSiteMRNDate.val().trim(),
        GrNo: txtGRNo.val().trim() == "" ? "0" : txtGRNo.val().trim(),
        GrDate:txtGRDate.val().trim(),
        RequisitionId: hdnRequisitionId.val().trim(),
        DeliveryChallanNo: txtDCNo.val().trim(),
        PONo: $("#txtPONo").val(),
        POId: $("#hdnPOFlag").val(),
        VendorId: hdnVendorId.val().trim(),
        VendorName: txtVendorName.val().trim(),
        ContactPerson: txtSContactPerson.val().trim(),
        ShippingContactPerson: txtSContactPerson.val().trim(),
        ShippingBillingAddress: txtSAddress.val().trim(),
        ShippingCity: txtSCity.val().trim(),
        ShippingStateId: ddlSState.val(),
        ShippingCountryId: ddlSCountry.val(),
        ShippingPinCode: txtSPinCode.val().trim(),
        ShippingTINNo: txtSTINNo.val().trim(),
        ShippingEmail: txtSEmail.val().trim(),
        ShippingMobileNo: txtSMobileNo.val().trim(),
        ShippingContactNo: txtSContactNo.val().trim(),
        ShippingFax: txtSFax.val().trim(),
        CompanyBranchId: ddlCompanyBranch.val(),
        DispatchRefNo: txtDispatchRefNo.val().trim(),
        DispatchRefDate: txtDispatchRefDate.val(),
        LRNo: txtLRNo.val().trim(),
        LRDate: txtLRDate.val(),
        TransportVia: txtTransportVia.val().trim(),
        NoOfPackets: txtNoOfPackets.val(),
        Remarks1: txtRemarks1.val() == "" ? "0" : txtRemarks1.val(),
        Remarks2: txtRemarks2.val() == "" ? "0" : txtRemarks2.val(),

        ApprovalStatus: ddlApprovalStatus.val(),
        CustomerId: hdnCustomerId.val(),
        CustomerBranchId: ddlCustomerBranch.val(),
        IsLocal:hdnIsLocal.val()
    };

    if (CalculateNewReceivedQuantity()) {
        var customerSiteMrnProductList = [];
        var productSelected = false;
        $('#tblProductList tr').each(function (i, row) {
            var $row = $(row);
            var customerSiteMrnProductDetailId = $row.find("#hdnCustomerSiteMRNProductDetailId").val();
            var productId = $row.find("#hdnProductId").val();
            var productName = $row.find("#hdnProductName").val();
            var productCode = $row.find("#hdnProductCode").val();
            var productShortDesc = $row.find("#hdnProductShortDesc").val();
            var uomName = $row.find("#hdnUOMName").val();
            var price = $row.find("#hdnPrice").val();
            var quantity = $row.find("#hdnQuantity").val();
            var receivedQuantity = $row.find("#hdnCurrentRcvdQuantity").val();
            var acceptQuantity = $row.find("#hdnAcceptQuantity").val();
            var rejectQuantity = $row.find("#hdnRejectQuantity").val();

            if (productId != undefined) {
                productSelected = true;
                var customerSiteMrnProduct = {
                    CustomerSiteMRNProductDetailId: customerSiteMrnProductDetailId,
                    ProductId: productId,
                    ProductName: productName,
                    ProductCode: productCode,
                    ProductShortDesc: productShortDesc,
                    UOMName: uomName,
                    Price: price,
                    Quantity: quantity,
                    ReceivedQuantity: receivedQuantity,
                    AcceptQuantity: acceptQuantity,
                    RejectQuantity: rejectQuantity,
                    RequisitionId: hdnRequisitionId.val()
                };
                customerSiteMrnProductList.push(customerSiteMrnProduct);
            }
        });


        var customerSiteMrnDocumentList = [];
        $('#tblDocumentList tr').each(function (i, row) {
            var $row = $(row);
            var customerSiteMrnDocId = $row.find("#hdnCustomerSiteMrnDocId").val();
            var documentSequenceNo = $row.find("#hdnDocumentSequenceNo").val();
            var documentTypeId = $row.find("#hdnDocumentTypeId").val();
            var documentTypeDesc = $row.find("#hdnDocumentTypeDesc").val();
            var documentName = $row.find("#hdnDocumentName").val();
            var documentPath = $row.find("#hdnDocumentPath").val();

            if (customerSiteMrnDocId != undefined) {
                var customerSiteMrnDocument = {
                    CustomerSiteMRNDocId: customerSiteMrnDocId,
                    DocumentSequenceNo: documentSequenceNo,
                    DocumentTypeId: documentTypeId,
                    DocumentTypeDesc: documentTypeDesc,
                    DocumentName: documentName,
                    DocumentPath: documentPath
                };
                customerSiteMrnDocumentList.push(customerSiteMrnDocument);
            }

        });


        if (productSelected == false) {
            ShowModel("Alert", "Please select at least one Product")
            ddlCompanyBranch.focus();
            return false;
        }

        var accessMode = 1;//Add Mode
        if (hdncustomerSiteMrnId.val() != null && hdncustomerSiteMrnId.val() != 0) {
            accessMode = 2;//Edit Mode
        }
        var requestData = { customerSiteMrnViewModel: CustomerSiteMRNViewModel, customerSiteMrnProducts: customerSiteMrnProductList, customerSiteMrnDocuments: customerSiteMrnDocumentList };
        $.ajax({
            url: "../CustomerSiteMRN/AddEditCustomerSiteMRN?AccessMode=" + accessMode + "",
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
                           window.location.href = "../CustomerSiteMRN/AddEditCustomerSiteMRN?customerSiteMrnId=" + data.trnId + "&AccessMode=3&Local=" + hdnIsLocal.val();
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
        ShowModel('Alert', 'Please Enter Received Quantity.');
        return false;
    }

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
function ShowHideProductPanel(action) {
    if (action == 1) {
        $(".productsection").show();
    }
    else {
        $(".productsection").hide();
        $("#txtProductName").val("");
        $("#hdnProductId").val("0");
        $("#hdnCustomerSiteMRNProductDetailId").val("0");
        $("#txtProductCode").val("");
        $("#txtProductShortDesc").val("");
        $("#txtPrice").val("");
        $("#txtQuantity").val("");
        $("#txtReceivedQuantity").val("");
        $("#txtAcceptQuantity").val("");
        $("#txtRejectQuantity").val("");
        $("#txtUOMName").val("");
        $("#btnAddProduct").show();
        $("#btnUpdateProduct").hide();
    }
}
function OpenInvoiceSearchPopup() {
    $("#SearchRequisitionModel").modal();
}
function SearchRequisition() {
    var txtSearchRequisitionNo = $("#txtSearchRequisitionNo");
    var txtSearchWorkOrderNo = $("#txtSearchWorkOrderNo");
    var ddlSearchRequisitionType = $("#ddlSearchRequisitionType");
    var ddlSearchCompanyBranch = $("#ddlSearchCompanyBranch");
    var txtFromDate = $("#txtSearchFromDate");
    var txtToDate = $("#txtSearchToDate");

    var requestData = { requisitionNo: txtSearchRequisitionNo.val().trim(), workOrderNo: txtSearchWorkOrderNo.val().trim(), requisitionType: ddlSearchRequisitionType.val(), companyBranchId: ddlSearchCompanyBranch.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), displayType: "Popup", approvalStatus: "Final" };
    $.ajax({
        url: "../CustomerSiteMRN/GetStoreRequisitionList",
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
function SelectRequisition(requisitionId, requisitionNo, requisitionDate, workOrderId, workOrderNo, workOrderDate, companyBranchId, locationId, customerName, customerId, customerBranchName, customerBranchId, VendorName, VendorCode, VendorId, RequisitionType) {
    $("#txtRequisitionNo").val(requisitionNo);
    $("#hdnRequisitionId").val(requisitionId);
    $("#txtRequisitionDate").val(requisitionDate);
    $("#txtRequisitionType").val(RequisitionType);
    //if (RequisitionType == "With Order") {
    //    $("#spnPONo").css('display', '');
    //    $("#hdnPOFlag").val(1);
    //}
    //else {
    //    $("#spnPONo").css('display', 'none');
    //    $("#hdnPOFlag").val(0);
    //}
    $("#ddlCompanyBranch").val(companyBranchId);
    $("#txtCustomerName").val(customerName);
    $("#hdnCustomerId").val(customerId);
    $("#hdnCustomerBranchId").val(customerBranchId);
    $("#hdnVendorId").val(VendorId);
    $("#txtVendorName").val(VendorName);
    $("#txtVendorCode").val(VendorCode);
    GetCustomerBranchListByID(customerId, customerBranchId);
    $("#btnAddNewProduct").hide();
    //$("#hdnCustomerId").val(customerId);
    //$("#txtCustomerBranchName").val(customerBranchName);
    //$("#hdnCustomerBranchId").val(customerBranchId);

    $("#SearchRequisitionModel").modal('hide');
    var sinProducts = [];
    GetRequisitionProductList(sinProducts);

}
function GetRequisitionProductList(sinProducts) {
    var hdnRequisitionId = $("#hdnRequisitionId");
    var requestData = { sinProducts: sinProducts, requisitionId: hdnRequisitionId.val() };
    $.ajax({
        url: "../CustomerSiteMRN/GetStoreRequisitionProductList",
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

//function OpenRequisitionSearchPopup() {
//    $("#SearchRequisitionModel").modal();

//}

//function SearchInvoice() {
//    var txtInvoiceNo = $("#txtSearchInvoiceNo");
//    var txtVendorName = $("#txtSearchVendorName");
//    var txtRefNo = $("#txtSearchRefNo");
//    var txtFromDate = $("#txtSearchFromDate");
//    var txtToDate = $("#txtSearchToDate");

//    var requestData = { piNo: txtInvoiceNo.val().trim(), vendorName: txtVendorName.val().trim(), refNo: txtRefNo.val().trim(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), approvalStatus: "Final", displayType: "Popup" };
//    $.ajax({
//        url: "../MRN/GetPurchaseInvoiceList",
//        data: requestData,
//        dataType: "html",
//        type: "GET",
//        error: function (err) {
//            $("#divInvoiceList").html("");
//            $("#divInvoiceList").html(err);
//        },
//        success: function (data) {
//            $("#divInvoiceList").html("");
//            $("#divInvoiceList").html(data);
//        }
//    });
//}
//function SelectInvoice(invoiceId, invoiceNo, invoiceDate, vendorId, vendorCode, vendorName) {
//    //$("#txtInvoiceNo").val(invoiceNo);
//    //$("#txtInvoiceDate").val(invoiceDate);
//    //$("#hdnVendorId").val(vendorId);
//    //$("#txtVendorCode").val(vendorCode);
//    //$("#txtVendorName").val(vendorName);
//    //GetVendorDetail(vendorId);
//    $("#SearchInvoiceModel").modal('hide');
    
//    GetPIDetail(invoiceId);
//    var piProducts = [];
//    GetPIProductList(piProducts, invoiceId);
//}
//function GetPIDetail(invoiceId) {
//    $.ajax({
//        type: "GET",
//        asnc: false,
//        url: "../PurchaseInvoice/GetPIDetail",
//        data: { invoiceId: invoiceId },
//        dataType: "json",
//        success: function (data) {
//            $("#hdnInvoiceId").val(invoiceId);
//            $("#txtInvoiceNo").val(data.InvoiceNo);
//            $("#txtInvoiceDate").val(data.InvoiceDate);
//            $("#hdnVendorId").val(data.VendorId);
//            $("#txtVendorCode").val(data.VendorCode);
//            $("#txtVendorName").val(data.VendorName);
//            $("#txtRemarks1").val(data.Remarks);
//        },
//        error: function (Result) {
//            ShowModel("Alert", "Problem in Request");
//        }
//    });

//}
//function GetPIProductList(piProducts, invoiceId) {
    
//    var requestData = { piProducts: piProducts, invoiceId: invoiceId };
//    $.ajax({
//        url: "../PurchaseInvoice/GetPIMRNProductList",
//        cache: false,
//        data: JSON.stringify(requestData),
//        dataType: "html",
//        contentType: "application/json; charset=utf-8",
//        type: "POST",
//        error: function (err) {
//            $("#divProductList").html("");
//            $("#divProductList").html(err);
//        },
//        success: function (data) {
//            $("#divProductList").html("");
//            $("#divProductList").html(data);

//           // CalculateGrossandNetValues();
//            ShowHideProductPanel(2);
//        }
//    });
//}



function GetVendorDetail(vendorId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Vendor/GetVendorDetail",
        data: { vendorId: vendorId },
        dataType: "json",
        success: function (data) {
            $("#txtSAddress").val(data.Address);
            $("#txtSCity").val(data.City);
            $("#ddlSCountry").val(data.CountryId);
            $("#ddlSState").val(data.StateId);
            $("#txtSPinCode").val(data.PinCode);
            $("#txtSTINNo").val(data.TINNo);
            $("#txtSEmail").val(data.Email);
            $("#txtSMobileNo").val(data.MobileNo);
            $("#txtSContactNo").val(data.ContactNo);
            $("#txtSFax").val(data.Fax);
            $("#txtSContactPerson").val(data.ContactPersonName);
            
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}
function GetCustomerSiteMRNDetail(customerSiteMrnId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../CustomerSiteMRN/GetCustomerSiteMRNDetail",
        data: { customerSiteMrnId: customerSiteMrnId },
        dataType: "json",
        success: function (data) {
            $("#txtCustomerSiteMRNNo").val(data.CustomerSiteMRNNo);
            $("#txtCustomerSiteMRNDate").val(data.CustomerSiteMRNDate);
            $("#txtGRNo").val(data.GRNo);
            $("#txtGRDate").val(data.GRDate);
            $("#hdnRequisitionId").val(data.RequisitionId);
            $("#txtRequisitionNo").val(data.RequisitionNo);
            $("#txtDCNo").val(data.DeliveryChallanNo);
            if (data.RequisitionType == "With Order") {
                $("#spnPONo").css('display', '');
                $("#hdnPOFlag").val(1);
            }
            else {
                $("#spnPONo").css('display', 'none');
                $("#hdnPOFlag").val(0);
            }
            $("#txtPONo").val(data.PONo);
            $("#txtRequisitionType").val(data.RequisitionType);
            //$("#txtInvoiceNo").val(data.InvoiceNo);
            //$("#hdnInvoiceId").val(data.InvoiceId);
            //$("#txtInvoiceDate").val(data.InvoiceDate);

            $("#hdnVendorId").val(data.VendorId);
            $("#txtVendorCode").val(data.VendorCode);
            $("#txtVendorName").val(data.VendorName);

            $("#ddlApprovalStatus").val(data.ApprovalStatus);

            $("#txtSContactPerson").val(data.ShippingContactPerson);
            $("#txtSAddress").val(data.ShippingBillingAddress);
            $("#txtSCity").val(data.ShippingCity);
            $("#ddlSCountry").val(data.ShippingCountryId);
            $("#ddlSState").val(data.ShippingStateId);
            $("#txtSPinCode").val(data.ShippingPinCode);
            $("#txtSTINNo").val(data.ShippingTINNo);
            $("#txtSEmail").val(data.ShippingEmail);
            $("#txtSMobileNo").val(data.ShippingMobileNo);
            $("#txtSContactNo").val(data.ShippingContactNo);
            $("#txtSFax").val(data.ShippingFax);

            $("#ddlCompanyBranch").val(data.CompanyBranchId);
            $("#txtDispatchRefNo").val(data.DispatchRefNo);
            $("#txtDispatchRefDate").val(data.DispatchRefDate);
            $("#txtLRNo").val(data.LRNo);
            $("#txtLRDate").val(data.LRDate);
            $("#txtTransportVia").val(data.TransportVia);
            $("#txtNoOfPackets").val(data.NoOfPackets);


            $("#txtRemarks1").val(data.Remarks1);
            $("#txtRemarks2").val(data.Remarks2);
            $("#txtCustomerName").val(data.CustomerName);
            $("#hdnCustomerId").val(data.CustomerId);
            GetCustomerBranchListByID(data.CustomerId, data.CustomerBranchId);
            $("#ddlCustomerBranch").val(data.CustomerBranchId);
            $("#divCreated").show();
            $("#txtCreatedBy").val(data.CreatedByUserName);
            $("#txtCreatedDate").val(data.CreatedDate);
            if (data.ModifiedByUserName != "") {
                $("#divModified").show();
                $("#txtModifiedBy").val(data.ModifiedByUserName);
                $("#txtModifiedDate").val(data.ModifiedDate);
            }
            if (data.ApprovalStatus == "Final") {
                $("#btnUpdate").hide();
                if (data.IsLocal == true) {
                    $("#btnLocalAddNew").show();
                }
                else{
                $("#btnAddNew").show();
                $("#btnPrint").show();
                }
            }
        
          


        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });


}

function CalculateQuantity()
{
    var totalquantity = $("#txtReceivedQuantity").val();
    var acceptquantity = $("#txtAcceptQuantity").val();
    var requisitionQty = $("#txtQuantity").val();
    total = totalquantity == "" ? 0 : totalquantity;
    accept = acceptquantity == "" ? 0 : acceptquantity;
    if (parseFloat(accept) > parseFloat(total))
    {
        ShowModel("Alert", "Accept Quantity Should be less than  or equal Received Quantity!!!");
        $("#txtAcceptQuantity").val("0");
        $("#txtRejectQuantity").val(totalquantity);
        $("#txtAcceptQuantity").focus();
        return false;
      
    }
    else if (parseFloat(accept) > parseFloat(requisitionQty)) {
        ShowModel("Alert", "Accept Quantity Should Not Be Greater Than The Requisition Qty.!!!");
        $("#txtAcceptQuantity").val("0");
        $("#txtRejectQuantity").val(totalquantity);
        $("#txtAcceptQuantity").focus();
        return false;

    }
    else
    {
        var rejectquantity = (parseFloat(total) - parseFloat(accept)).toFixed(2);
        $("#txtRejectQuantity").val(parseFloat(rejectquantity).toFixed(2));
    }
    
}

function CalculateTotalCharges() {
    var price = $("#txtPrice").val();
    var quantity = $("#txtQuantity").val();
    var discountPerc = $("#txtDiscountPerc").val();
    var productTaxPerc = $("#hdnProductTaxPerc").val();
    var discountAmount = 0;
    var taxAmount = 0;
    price = price == "" ? 0 : price;
    quantity = quantity == "" ? 0 : quantity;
    var totalPrice = parseFloat(price) * parseFloat(quantity);
    if (parseFloat(discountPerc) > 0) {
        discountAmount = (parseFloat(totalPrice) * parseFloat(discountPerc)) / 100

    }
    $("#txtDiscountAmount").val(discountAmount.toFixed(2));

    if (parseFloat(productTaxPerc) > 0) {
        taxAmount = ((parseFloat(totalPrice) - parseFloat(discountAmount)) * parseFloat(productTaxPerc)) / 100;
    }
    $("#txtProductTaxAmount").val(taxAmount.toFixed(2));

    $("#txtTotalPrice").val((totalPrice - discountAmount + taxAmount).toFixed(2));


}
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

function OpenVendorMasterPopup() {
      $("#AddNewVendor").modal();
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
function localSiteMrn()
{
    var local = GetParameterValues('Local')
    if (local == "1" || local == "true")
    {
        $("#txtGRNo").hide();
        $("#txtRequisitionNo").hide();
        $("#txtRequisitionDate").hide();
        $("#txtDCNo").hide();
        $("#txtVendorName").hide();
        $("#txtVendorCode").hide();
        $("#txtDispatchRefNo").hide();
        $("#txtDispatchRefDate").hide();
        $("#txtLRNo").hide();
        $("#txtLRDate").hide();
        $("#txtTransportVia").hide();
        $("#txtNoOfPackets").hide();

        $("#divGr").hide();
        $("#divRequisition").hide();
        $("#divDeliveryChallan").hide();
        $("#divVendor").hide();
        $("#divDispatch").hide();
        $("#divLr").hide();
        $("#divTransport").hide();
        $("#btnAddNewProduct").show();
        $("#hdnIsLocal").val(true);
        $("#hdnRequisitionId").val('LocalRequisition');
        $("#txtDCNo").val('LocalDeliveryChallan');
        if ($("#hdnAccessMode").val() == "3") {
            $("#btnAddNewProduct").hide();
            $("#btnAddNew").hide();
            $("#btnLocalAddNew").show();
        }
    }
}


function CalculateNewReceivedQuantity() {
    var CalculateFlag = false;
    //Loop through product list table-----
    $('#tblProductList tr').each(function (i, row) {
        var input = $(this).closest('tr').find('.newReceivedQty');
        var txtNewReceivedQuantity = input.val();
        if (parseFloat(txtNewReceivedQuantity) > 0) {
            CalculateFlag = true;
        }
    });
    return CalculateFlag;
}

function GetProductPurchasePrice(productId) {
    var ProductId = 0;
    if (productId != null) {
        ProductId = productId;
    }
    var requestData = { productId: ProductId }
    $.ajax({
        url: "../CustomerSiteMRN/GetProductPurchasePrice",
        data: requestData,
        dataType: "json",
        type: "GET",
        asnc: false,
        success: function (data) {
            if (data != null) {
                $("#txtPrice").val(data);
            }
        },
        error: function (Result) {
            ShowModel("Alert", "Problem In Request");
        }
    });
}

function ResetPage() {
    if (confirm("Are you sure to reset all fields?")) {
        window.location.href = "../CustomerSiteMRN/AddEditCustomerSiteMRN?accessMode=1";
    }
}