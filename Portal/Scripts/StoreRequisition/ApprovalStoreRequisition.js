$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });
    $("#txtRequisitionNo").attr('readOnly', true);
    $("#txtRequisitionDate").attr('readOnly', true);
    $("#txtCustomerCode").attr('readOnly', true);
    $("#txtWorkOrderNo").attr('readOnly', true);
    $("#txtWorkOrderDate").attr('readOnly', true);
    $("#txtCreatedBy").attr('readOnly', true);
    $("#txtCreatedDate").attr('readOnly', true);
    $("#txtModifiedBy").attr('readOnly', true);
    $("#txtModifiedDate").attr('readOnly', true);
    $("#txtRejectedDate").attr('readOnly', true);
    $("#ddlRequisitionType").attr('readOnly', true);
    $("#ddlCompanyBranch").attr('readOnly', true);
    $("#ddlLocation").attr('readOnly', true);
    $("#txtCustomerName").attr('readOnly', true);
    $("#ddlCustomerBranch").attr('readOnly', true);
    $("#txtRequisitionByUser").attr('readOnly', true);
    $("#txtRemarks1").attr('readOnly', true);
    $("#txtRemarks2").attr('readOnly', true);
    // $("#txtRejectedReason").attr("disabled", true);
    $("#txtTotalValue").attr('readOnly', true);
    $("#btnAddNewProduct").css('display', '');
    BindCompanyBranchList();
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnRequisitionId = $("#hdnRequisitionId");
    if (hdnRequisitionId.val() != "" && hdnRequisitionId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetStoreRequisitionDetail(hdnRequisitionId.val());
       }, 1000);

        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
            $("#chkstatus").attr('disabled', true);

            $("#btnSaveRequisition").hide();
            $("#btnUnlockRequisition").hide();
            $("#divAddProduct").hide();
        }
        else {
            $("#btnSave").hide();
            $("#btnUpdate").show();
            $("#btnSaveRequisition").hide();
            $("#btnUnlockRequisition").show();
            $("#EditRequisition").show();
            $("#divAddProduct").hide();

        }

    }
    else {
        $("#btnSave").show();
        $("#btnUpdate").hide();
    }

    var requisitionProducts = [];
    GetRequisitionProductList(requisitionProducts);

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
                        return { label: item.ProductName, value: item.Productid, desc: item.ProductShortDesc, code: item.ProductCode, CGST_Perc: item.CGST_Perc, SGST_Perc: item.SGST_Perc, IGST_Perc: item.IGST_Perc, HSN_Code: item.HSN_Code };
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

    if (hdnAccessMode.val() == "3") {
        $(".editonly").hide();
    }
    else {
        $(".editonly").hide();
    }
    
});

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

function GetRequisitionProductList(requisitionProducts) {
    var hdnRequisitionId = $("#hdnRequisitionId");
    var requestData = { storeRequisitionProducts: requisitionProducts, requisitionId: hdnRequisitionId.val() };
    $.ajax({
        url: "../StoreRequisition/GetStoreRequisitionProductApprovalList",
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
            CalculateGrossandNetValues();
            $(".editpr").hide();
           // $(".editonly").hide();
        }
    });
}

$('body').on('click', '#btnAddNewProduct', function () {
    $("#txtProductName").attr('readOnly', false);
    $("#btnAddPopupProduct").css('display', '');
   // $("#btnAddNewProduct").show();
    $("#btnUpdateProduct").hide();
});

$('body').on('click', '#editProduct', function () {
   // $("#btnAddNewProduct").hide();
    $("#btnUpdateProduct").show();
});

function ShowHideProductPanel(action) {
    if (action == 1) {
        $(".productsection").show();
    }
    else {
        $(".productsection").hide();
        $("#txtProductName").val("");
        $("#hdnProductId").val("0");
        $("#hdnProductDetailId").val("0");
        $("#txtProductCode").val("");
        $("#txtProductShortDesc").val("");
        $("#txtUOMName").val("");
        $("#txtQuantity").val("");
        $("#btnAddProduct").show();
        $("#btnUpdateProduct").hide();
    }
}
function BindLocationList(locationId) {
    var companyBranchID = $("#ddlCompanyBranch option:selected").val();
    $("#ddlLocation").val(0);
    $("#ddlLocation").html("");
    $.ajax({
        type: "GET",
        url: "../StoreRequisition/GetBranchLocationList",
        data: { companyBranchID: companyBranchID },
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlLocation").append($("<option></option>").val(0).html("-Select Department-"));
            $.each(data, function (i, item) {
                $("#ddlLocation").append($("<option></option>").val(item.LocationId).html(item.LocationName));
            });

            $("#ddlLocation").val(locationId);
        },
        error: function (Result) {
            $("#ddlLocation").append($("<option></option>").val(0).html("-Select Department-"));
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

function SaveData() {

    var hdnRequisitionId = $("#hdnRequisitionId");
    var ddlRequisitionApprovel = $("#ddlRequisitionApprovel");
    var txtRejectedReason = $("#txtRejectedReason");

    if (ddlRequisitionApprovel.val() == "0" || ddlRequisitionApprovel.val() == undefined) {
        ShowModel("Alert", "Please select Store Requisition Approval Status")
        return false;

    }
    if (ddlRequisitionApprovel.val() == "Rejected") {
        if (txtRejectedReason.val() == "") {
            ShowModel("Alert", "Please Enter Store Requisition Rejected Reason")
            return false;

        }
    }
    if (ddlRequisitionApprovel.val() == "Approved") {
        if (txtRejectedReason.val() == "") {
            ShowModel("Alert", "Please Enter Store Requisition Approved Reason")
            return false;

        }
    }

    var storeRequisitionViewModel = {
        RequisitionId: hdnRequisitionId.val(),
        ApprovalStatus: ddlRequisitionApprovel.val(),
        RejectedReason: txtRejectedReason.val()
    };

    var requestData = { storeRequisitionViewModel: storeRequisitionViewModel };
    $.ajax({
        url: "../StoreRequisition/ApprovalStoreRequisition",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                //ClearFields();
                setTimeout(
                   function () {
                       window.location.href = "../StoreRequisition/ApprovalStoreRequisition?storeRequisitionId=" + hdnRequisitionId.val() + "&AccessMode=3";
                   }, 1000);

                $("#btnSave").show();
                $("#btnUpdate").hide();
                $("#btnUnlockRequisition").hide();
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
    $("#hdnRequisitionId").val("0");
    $("#txtRequisitionDate").val($("#hdnCurrentDate").val());
    $("#ddlRequisitionType").val("0");
    $("#ddlCompanyBranch").val("0");
    $("#hdnCustomerId").val("0");
    $("#hdnUserId").val("0");
    $("#ddlCustomerBranch").val("0");
    $("#txtRemarks1").val("");
    $("#txtRemarks2").val("");
    $("#ddlRequisitionStatus").val("0");
    $("#btnSave").show();
    $("#btnUpdate").hide();
}

function GetStoreRequisitionDetail(requisitionId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../StoreRequisition/GetStoreRequisitionDetail",
        data: { requisitionId: requisitionId },
        dataType: "json",
        success: function (data) {
            $("#txtRequisitionNo").val(data.RequisitionNo);
            $("#txtRequisitionDate").val(data.RequisitionDate);
            $("#txtWorkOrderNo").val(data.WorkOrderNo);
            $("#hdnWorkOrderId").val(data.WorkOrderId);
            $("#txtWorkOrderDate").val(data.WorkOrderDate);
            $("#ddlRequisitionType").val(data.RequisitionType)
            $("#ddlCompanyBranch").val(data.CompanyBranchId);
            BindLocationList(data.LocationId);
            $("#ddlLocation").val(data.LocationId);

            $("#txtRequisitionByUser").val(data.FullName);
            $("#hdnUserId").val(data.RequisitionByUserId);
            $("#hdnCustomerId").val(data.CustomerId);
            $("#txtCustomerCode").val(data.CustomerCode);
            $("#txtCustomerName").val(data.CustomerName);
            BindCustomerBranchList(data.CustomerBranchId);
            $("#ddlCustomerBranch").val(data.CustomerBranchId);
            $("#txtRemarks1").val(data.Remarks1);
            $("#txtRemarks2").val(data.Remarks2);
            $("#ddlRequisitionStatus").val(data.RequisitionStatus);
            //$("#divCreated").show();
            //$("#txtCreatedBy").val(data.CreatedByUserName);
            //$("#txtCreatedDate").val(data.CreatedDate);


            if (data.ModifiedByUserName != "") {
                $("#divModified").show();
                $("#txtModifiedBy").val(data.ModifiedByUserName);
                $("#txtModifiedDate").val(data.ModifiedDate);
            }

            if (data.CreatedByUserName != "") {
                $("#divCreated").show();
                $("#txtCreatedBy").val(data.CreatedByUserName);
                $("#txtCreatedDate").val(data.CreatedDate);
            }
            if (data.ApprovalStatus != "") {
                if (data.ApprovedByUserName != "") {
                    $("#divApproved").show();
                    $("#txtApprovedBy").val(data.ApprovedByUserName);
                    $("#txtApprovedDate").val(data.ApprovedDate);
                }
            }


            if (data.ApprovalStatus == "Approved") {
                $("#ddlRequisitionApprovel").val(data.ApprovalStatus);
                $("#txtRejectedReason").val(data.RejectedReason);
                $("#btnUpdate").hide();
            }
            else if (data.ApprovalStatus == "Rejected") {
                $("#ddlRequisitionApprovel").val(data.ApprovalStatus);
                $("#txtRejectedReason").val(data.RejectedReason);
                $("#btnUpdate").hide();
            }
            else {
                $("#ddlRequisitionApprovel").val("0");
            }

        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}

function EnableDisableRejectReason() {
    var approvalStatus = $("#ddlRequisitionApprovel option:selected").text();
    //if (approvalStatus == "Rejected") {
    //    $("#txtRejectedReason").attr("disabled", false);
    $("#spnRejectionReason").show();
    //}
    //else {
    //    $("#txtRejectedReason").attr("disabled", true);
    //    $("#txtRejectedReason").val("");
    //    $("#spnRejectionReason").hide();
    //}
}

function CalculateGrossandNetValues() {
    var basicValue = 0;
    var taxValue = 0;
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);

        var totalPrice = $row.find("#hdnTotalPrice").val();
        if (totalPrice != undefined) {
            basicValue += parseFloat(totalPrice);
        }

    });
    $("#txtTotalValue").val(parseFloat(parseFloat(basicValue)).toFixed(2));
}

/********** Unlock Functionality Start **********/
function UnlockRequisition() {
    $("#btnUpdate").hide();
    $("#txtRequisitionNo").attr('readOnly', false);
    $("#txtRequisitionDate").attr('readOnly', false);
    $("#txtCustomerCode").attr('readOnly', false);
    $("#txtWorkOrderNo").attr('readOnly', false);
    $("#txtWorkOrderDate").attr('readOnly', false);
    $("#txtCreatedBy").attr('readOnly', false);
    $("#txtCreatedDate").attr('readOnly', false);
    $("#txtModifiedBy").attr('readOnly', false);
    $("#txtModifiedDate").attr('readOnly', false);
    $("#txtRejectedDate").attr('readOnly', false);
    $("#ddlRequisitionType").attr('readOnly', false);
    $("#ddlCompanyBranch").attr('readOnly', false);
    $("#ddlLocation").attr('readOnly', false);
    $("#txtCustomerName").attr('readOnly', false);
    $("#ddlCustomerBranch").attr('readOnly', false);
    $("#txtRequisitionByUser").attr('readOnly', false);
    $("#txtRemarks1").attr('readOnly', false);
    $("#txtRemarks2").attr('readOnly', false);
    $("#txtRejectedReason").attr("disabled", false);
    $("#txtTotalValue").attr('readOnly', false);
    $("#btnSaveRequisition").show();
    $("#divAddProduct").show();
    if ($("#hdnAccessMode").val() == "2") {
        $(".editonly").show();
    }
}
function GetProductDetail(productId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetProductDetail",
        data: { productid: productId },
        dataType: "json",
        success: function (data) {
            //$("#txtPrice").val(data.SalePrice);
            $("#hdnProductPrice").val(data.PurchasePrice);
            $("#txtUOMName").val(data.PurchaseUOMName);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });


}
function AddProduct(action) {
    var productEntrySequence = 0;
    var flag = true;
    var txtProductName = $("#txtProductName");
    var hdnRequisitionProductDetailId = $("#hdnRequisitionProductDetailId");
    var hdnProductId = $("#hdnProductId");
    var txtProductCode = $("#txtProductCode");
    var txtProductShortDesc = $("#txtProductShortDesc");
    var txtUOMName = $("#txtUOMName");
    var txtQuantity = $("#txtQuantity");
    var hdnSequenceNo = $("#hdnSequenceNo");
    var hdnProductPrice = $("#hdnProductPrice");

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

    if (txtQuantity.val().trim() == "" || txtQuantity.val().trim() == "0" || parseFloat(txtQuantity.val().trim()) <= 0) {
        if (parseFloat(txtQuantity.val().trim()) <= 0) {
            ShowModel("Alert", "Quantity should be greater than 0")
            txtQuantity.focus();
            return false;
        }
        else {
            ShowModel("Alert", "Please enter Quantity")
            txtQuantity.focus();
            return false;
        }
    }
    var totalPrice = parseFloat(txtQuantity.val()) * parseFloat(hdnProductPrice.val());
    $("#hdnTotalPrice").val(totalPrice);
    var hdnTotalPrice = $("#hdnTotalPrice");
    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        productEntrySequence = 1;
    }

    var requisitionProductList = [];
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var requisitionProductDetailId = $row.find("#hdnRequisitionProductDetailId").val();
        var sequenceNo = $row.find("#hdnSequenceNo").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var quantity = $row.find("#hdnQuantity").val();
        var price = $row.find("#hdnPrice").val();

        if (productId != undefined) {
            if (action == 1 || (hdnSequenceNo.val() != sequenceNo)) {

                if (productId == hdnProductId.val()) {
                    ShowModel("Alert", "Product already added!!!")
                    txtProductName.focus();
                    flag = false;
                    return false;
                }

                var requisitionProduct = {
                    RequisitionProductDetailId: requisitionProductDetailId,
                    SequenceNo: sequenceNo,
                    ProductId: productId,
                    ProductName: productName,
                    ProductCode: productCode,
                    ProductShortDesc: productShortDesc,
                    UOMName: uomName,
                    Quantity: quantity,
                    Price: price

                };
                requisitionProductList.push(requisitionProduct);
                productEntrySequence = parseInt(productEntrySequence) + 1;

            }
            else if (hdnRequisitionProductDetailId.val() == requisitionProductDetailId && hdnSequenceNo.val() == sequenceNo) {
                var requisitionProduct = {
                    RequisitionProductDetailId: hdnRequisitionProductDetailId.val(),
                    SequenceNo: hdnSequenceNo.val(),
                    ProductId: hdnProductId.val(),
                    ProductName: txtProductName.val().trim(),
                    ProductCode: txtProductCode.val().trim(),
                    ProductShortDesc: txtProductShortDesc.val().trim(),
                    UOMName: txtUOMName.val().trim(),
                    Quantity: txtQuantity.val().trim(),
                    Price: price

                };
                requisitionProductList.push(requisitionProduct);
                hdnSequenceNo.val("0");
            }
        }

    });
    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        hdnSequenceNo.val(productEntrySequence);
    }
    if (action == 1) {
        var requisitionProductAddEdit = {
            RequisitionProductDetailId: hdnRequisitionProductDetailId.val(),
            SequenceNo: hdnSequenceNo.val(),
            ProductId: hdnProductId.val(),
            ProductName: txtProductName.val().trim(),
            ProductCode: txtProductCode.val().trim(),
            ProductShortDesc: txtProductShortDesc.val().trim(),
            UOMName: txtUOMName.val().trim(),
            Quantity: txtQuantity.val().trim(),

        };
        requisitionProductList.push(requisitionProductAddEdit);
        hdnSequenceNo.val("0");
    }
    if (flag == true) {
        GetRequisitionProductList(requisitionProductList);
    }


}
function EditProductRow(obj) {
    var hdnAccessMode = $("#hdnAccessMode");
    if (hdnAccessMode.val() == "3") {
        ShowModel("Alert", "You can't modify in view mode.")
        return false;
    }
    $("#txtProductName").attr('readOnly', true);
    $("#txtProductCode").attr('readOnly', true);
    $("#txtProductShortDesc").attr('readOnly', true);
    $("#txtUOMName").attr('readOnly', true);
    $("#btnAddPopupProduct").css('display', 'none');
    var row = $(obj).closest("tr");
    var requisitionProductDetailId = $(row).find("#hdnRequisitionProductDetailId").val();
    var sequenceNo = $(row).find("#hdnSequenceNo").val();
    var productId = $(row).find("#hdnProductId").val();
    var productName = $(row).find("#hdnProductName").val();
    var productCode = $(row).find("#hdnProductCode").val();
    var productShortDesc = $(row).find("#hdnProductShortDesc").val();
    var uomName = $(row).find("#hdnUOMName").val();
    var quantity = $(row).find("#hdnQuantity").val();

    $("#txtProductName").val(productName);
    $("#hdnRequisitionProductDetailId").val(requisitionProductDetailId);
    $("#hdnSequenceNo").val(sequenceNo);
    $("#hdnProductId").val(productId);
    $("#txtProductCode").val(productCode);
    $("#txtProductShortDesc").val(productShortDesc);
    $("#txtUOMName").val(uomName);
    $("#txtQuantity").val(quantity);
    $("#btnAddProduct").hide();
    $("#btnUpdateProduct").show();
    ShowHideProductPanel(1);
}
function RemoveProductRow(obj) {
    var hdnAccessMode = $("#hdnAccessMode");
    if (hdnAccessMode.val() == "3") {
        ShowModel("Alert", "You can't delete in view mode.")
        return false;
    }
    if (confirm("Do you want to remove selected Product?")) {
        var row = $(obj).closest("tr");
        ShowModel("Alert", "Product Removed from List.");
        row.remove();
        CalculateGrossandNetValues();

    }
}

/* FOR Save data after unlock */
function SaveUnloackData() {

    var hdnRequisitionId = $("#hdnRequisitionId");
    var txtRequisitionDate = $("#txtRequisitionDate");
    var ddlRequisitionType = $("#ddlRequisitionType");
    var ddlCompanyBranch = $("#ddlCompanyBranch");
    var ddlLocation = $("#ddlLocation");
    var hdnCustomerId = $("#hdnCustomerId");
    var hdnUserId = $("#hdnUserId");
    var ddlCustomerBranch = $("#ddlCustomerBranch");
    var txtRequisitionByUser = $("#txtRequisitionByUser");
    var txtRemarks1 = $("#txtRemarks1");
    var txtRemarks2 = $("#txtRemarks2");
    var ddlRequisitionStatus = $("#ddlRequisitionStatus");
    var txtWorkOrderNo = $("#txtWorkOrderNo");
    var hdnWorkOrderId = $("#hdnWorkOrderId");
    var ddlRequisitionApprovel = $("#ddlRequisitionApprovel");
    var txtRejectedReason = $("#txtRejectedReason");
    //if (ddlRequisitionType.val() == "0" || ddlRequisitionType.val() == "") {
    //    ShowModel("Alert", "Please Select Requisition Type")
    //    return false;
    //}
    if (ddlCompanyBranch.val() == "0" || ddlCompanyBranch.val() == "") {
        ShowModel("Alert", "Please Select Company Branch")
        return false;
    }

    if (ddlLocation.val() == "0" || ddlLocation.val() == "") {
        ShowModel("Alert", "Please Select Department")
        return false;
    }


    if (ddlRequisitionApprovel.val() == "0" || ddlRequisitionApprovel.val() == undefined) {
        ShowModel("Alert", "Please select Store Requisition Approval Status")
        return false;

    }
    if (ddlRequisitionApprovel.val() == "Approved") {
        if (txtRejectedReason.val() == "") {
            ShowModel("Alert", "Please Enter Store Requisition Remarks")
            return false;

        }
    }
    if (ddlRequisitionApprovel.val() == "Rejected") {
        if (txtRejectedReason.val() == "") {
            ShowModel("Alert", "Please Enter Store Requisition Remarks ")
            return false;

        }
    }

    var storeRequisitionViewModel = {
        RequisitionId: hdnRequisitionId.val(),
        RequisitionDate: txtRequisitionDate.val().trim(),
        RequisitionType: ddlRequisitionType.val(),
        CompanyBranchId: ddlCompanyBranch.val().trim(),
        LocationId: ddlLocation.val(),
        RequisitionByUserId: hdnUserId.val().trim(),
        CustomerId: hdnCustomerId.val().trim(),
        CustomerBranchId: ddlCustomerBranch.val(),
        Remarks1: txtRemarks1.val(),
        Remarks2: txtRemarks2.val(),
        RequisitionStatus: "Final",
        WorkOrderId: hdnWorkOrderId.val(),
        workOrderNo: txtWorkOrderNo.val()
    };

    var storeRequisitionProductList = [];
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var requisitionProductDetailId = $row.find("#hdnRequisitionProductDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var quantity = $row.find("#hdnQuantity").val();
        var issuedQuantity = $row.find("#hdnIssuedQuantity").val();
        if (requisitionProductDetailId != undefined) {
            var requisitionProduct = {
                ProductId: productId,
                ProductShortDesc: productShortDesc,
                Quantity: quantity,
                IssuedQuantity: issuedQuantity

            };
            storeRequisitionProductList.push(requisitionProduct);
        }
    });

    if (storeRequisitionProductList.length == 0) {
        ShowModel("Alert", "Please Select at least 1 Product.")
        return false;
    }

    var requestData = { storeRequisitionViewModel: storeRequisitionViewModel, storeRequisitionProducts: storeRequisitionProductList };
    $.ajax({
        url: "../StoreRequisition/AddEditStoreRequisition",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                SaveData();
                //  ShowModel("Alert", data.message);
                //ClearFields();
                //setTimeout(
                //   function () {
                //       window.location.href = "../StoreRequisition/AddEditStoreRequisition?storeRequisitionId=" + data.trnId + "&AccessMode=2";
                //   }, 1000);

                //$("#btnSave").show();
                //$("#btnUpdate").hide();
                $("#btnUnlockRequisition").hide();
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

/********** Unlock Functionality End **********/

//open product master pop up----------
function ShowHideProductModel() {
    $("#AddProductModel").modal();
}
function BindProductDetail(productId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetProductDetail",
        data: { productid: productId },
        dataType: "json",
        success: function (data) {
            $("#txtProductName").val(data.ProductName);
            $("#hdnProductId").val(data.Productid);
            $("#txtProductShortDesc").val(data.ProductShortDesc);
            $("#txtProductCode").val(data.ProductCode);
            $("#txtHSN_Code").val(data.HSN_Code);
            var ddlSState = $("#ddlSState");
            var hdnBillingStateId = $("#ddlBState");
            if (ddlSState.val() == hdnBillingStateId.val()) {
                $("#txtCGSTPerc").val(data.CGST_Perc);
                $("#txtSGSTPerc").val(data.SGST_Perc);
                $("#txtIGSTPerc").val(0);
                $("#txtCGSTPercAmount").val(0);
                $("#txtSGSTPercAmount").val(0);
                $("#txtIGSTPercAmount").val(0);
            }
            else {
                $("#txtCGSTPerc").val(0);
                $("#txtSGSTPerc").val(0);
                $("#txtIGSTPerc").val(data.IGST_Perc);
                $("#txtCGSTPercAmount").val(0);
                $("#txtSGSTPercAmount").val(0);
                $("#txtIGSTPercAmount").val(0);
            }

            $("#txtPrice").val(data.PurchasePrice);
            $("#txtQuantity").val("");
            $("#txtUOMName").val(data.UOMName);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}