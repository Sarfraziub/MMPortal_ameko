
$(document).ready(function () {
    BindProductMainGroupList();
    $("#txtAvailableStock").attr('readOnly', true);
    $("#txtProductCode").attr('readOnly', true);
    // for Product List
    $("#txtProductName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            if ($("#ddlProductMainGroup").val() == "0") {
                ShowModel('Alert', 'Please select product main group first');
                $("#ddlProductMainGroup").focus();
                return false;
            }
            $.ajax({
                url: "../Product/GetProductAutoCompleteListByMainGroup",
                type: "GET",
                dataType: "json",
                data: { term: request.term, productMainGroupId: $("#ddlProductMainGroup").val() },
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
            GetSiteProductAvailableStock(ui.item.value, $("#hdnCustomerId").val(), $("#ddlCustomerBranch").val());
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                if ($("#ddlProductMainGroup").val() == "0") {
                    ShowModel('Alert', 'Please select product main group first');
                    $("#ddlProductMainGroup").focus();
                    $("#txtProductName").val("");
                    $("#hdnProductId").val("0");
                    $("#txtProductShortDesc").val("");
                    $("#txtProductCode").val("");
                    return false;
                }
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

    BindCompanyBranchList();
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

    // Get CustomerBranch List from CustomerId 

    var hdnAccessMode = $("#hdnAccessMode");
    var hdnSiteConsumptionId = $("#hdnsiteConsumptionId");
    if (hdnSiteConsumptionId.val() != "" && hdnSiteConsumptionId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0")
    {
        GetSiteConsumptionDetail(hdnSiteConsumptionId.val());

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
    $("#txtProductTypeName").focus();


      var siteConsumptionProducts = [];
    GetSiteConsumptionProductList(siteConsumptionProducts);

    $("#txtConsumptionDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    });
});


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

function BindCompanyBranchList() {
    $("#ddlCompanyBranch,#ddlSearchCompanyBranch").val(0);
    $("#ddlCompanyBranch,#ddlSearchCompanyBranch").html("");
    $.ajax({
        type: "GET",
        url: "../DeliveryChallan/GetCompanyBranchList",
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

function validateCustomerSelection(action) {
    var hdnCustomerId = $("#hdnCustomerId");
    var ddlCustomerBranch = $("#ddlCustomerBranch");
    $("#txtAvailableStock").val("");

    if (hdnCustomerId.val() == "0" || hdnCustomerId.val() == "") {
        ShowModel("Alert", "Please Select Customer");
        $("#txtCustomerName").focus();
        return false;
    }
    else if (ddlCustomerBranch.val() == "0" || ddlCustomerBranch.val() == null) {
        ShowModel("Alert", "Please Select Customer Branch");
        ddlCustomerBranch.focus();
        return false;
    }
    ShowHideProductPanel(action);
}

function ShowHideProductPanel(action) {
    if (action == 1) {
        $(".productsection").show();
    }
    else {
        $(".productsection").hide();
        $("#txtProductName").val("");
        $("#hdnProductId").val("0");
        $("#hdnWorkOrderProductDetailId").val("0");
        $("#txtProductCode").val("");
        $("#txtProductShortDesc").val("");
        $("#txtUOMName").val("");
        $("#txtQuantity").val("");
        $("#btnAddProduct").show();
        $("#btnUpdateProduct").hide();



    }
}
function AddProduct(action) {
    var productEntrySequence = 0;
    var flag = true;
    var txtProductName = $("#txtProductName");
    var hdnSiteConsumptionDetailId = $("#hdnSiteConsumptionDetailId");
    var hdnProductId = $("#hdnProductId");
    var ddlProductMainGroup = $("#ddlProductMainGroup");
    var hdnProductMainGroupId = $("#hdnProductMainGroupId");
    var hdnProductMainGroupName = $("#hdnProductMainGroupName");
    var txtProductCode = $("#txtProductCode");
    //var txtProductShortDesc = $("#txtProductShortDesc");
    var txtUOMName = $("#txtUOMName");
    var txtQuantity = $("#txtQuantity");
    var txtAvailableStock = $("#txtAvailableStock");
    var hdnSequenceNo = $("#hdnSequenceNo");

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

    if (ddlProductMainGroup.val() == "0") {
        ShowModel("Alert", "Please Select Product Group")
        ddlProductMainGroup.focus();
        return false;
    }

    if (hdnProductMainGroupId.val().trim() == "" || hdnProductMainGroupId.val().trim() == "0") {
        ShowModel("Alert", "Please select Product Group from list")
        ddlProductMainGroup.focus();
        return false;
    }
    if (txtQuantity.val().trim() == "" || txtQuantity.val().trim() == "0" || parseFloat(txtQuantity.val().trim()) <= 0) {
        ShowModel("Alert", "Please enter Quantity")
        txtQuantity.focus();
        return false;
    }
    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        productEntrySequence = 1;
    }

    if (parseFloat(txtQuantity.val().trim()) > parseFloat(txtAvailableStock.val().trim())) {
        ShowModel("Alert", "Consumption Quantity cannot be greater than Available Stock Quantity")
        txtQuantity.focus();
        return false;
    }

    var siteConsumptionProductList = [];
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var siteConsumptionDetailId = $row.find("#hdnSiteConsumptionDetailId").val();
        var sequenceNo = $row.find("#hdnSequenceNo").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productMainGroupId = $row.find("#hdnProductMainGroupId").val();
        var productMainGroupName = $row.find("#hdnProductMainGroupName").val();
        var productCode = $row.find("#hdnProductCode").val();
        //var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var quantity = $row.find("#hdnQuantity").val();

        if (productId != undefined) {
            if (action == 1 || (hdnSequenceNo.val() != sequenceNo)) {

                if (productId == hdnProductId.val()) {
                    ShowModel("Alert", "Product already added!!!")
                    txtProductName.focus();
                    flag = false;
                    return false;
                }

                var siteConsumptionProduct = {
                    SiteConsumptionDetailId: siteConsumptionDetailId,
                    SequenceNo: sequenceNo,
                    ProductId: productId,
                    ProductName: productName,
                    ProductMainGroupId: productMainGroupId,
                    ProductMainGroupName: productMainGroupName,
                    ProductCode: productCode,
                    //ProductShortDesc: productShortDesc,
                    UOMName: uomName,
                    Quantity: quantity

                };
                siteConsumptionProductList.push(siteConsumptionProduct);
                productEntrySequence = parseInt(productEntrySequence) + 1;

            }
            else if (hdnSiteConsumptionDetailId.val() == siteConsumptionDetailId && hdnSequenceNo.val() == sequenceNo) {
                var siteConsumptionProduct = {
                    SiteConsumptionDetailId: hdnSiteConsumptionDetailId.val(),
                    SequenceNo: hdnSequenceNo.val(),
                    ProductId: hdnProductId.val(),
                    ProductMainGroupId: hdnProductMainGroupId.val(),
                    ProductMainGroupName: $("#ddlProductMainGroup option:selected").text(),
                    ProductName: txtProductName.val().trim(),
                    ProductCode: txtProductCode.val().trim(),
                    //ProductShortDesc: txtProductShortDesc.val().trim(),
                    UOMName: txtUOMName.val().trim(),
                    Quantity: txtQuantity.val().trim()
                };
                siteConsumptionProductList.push(siteConsumptionProduct);
                hdnSequenceNo.val("0");
            }
        }

    });
    if (action == 1 && (hdnSequenceNo.val() == "" || hdnSequenceNo.val() == "0")) {
        hdnSequenceNo.val(productEntrySequence);
    }
    if (action == 1) {
        var siteConsumptionProductAddEdit = {
            SiteConsumptionDetailId: hdnSiteConsumptionDetailId.val(),
            SequenceNo: hdnSequenceNo.val(),
            ProductId: hdnProductId.val(),
            ProductMainGroupId: hdnProductMainGroupId.val(),
            ProductMainGroupName: $("#ddlProductMainGroup option:selected").text(),
            ProductName: txtProductName.val().trim(),
            ProductCode: txtProductCode.val().trim(),
            //ProductShortDesc: txtProductShortDesc.val().trim(),
            UOMName: txtUOMName.val().trim(),
            Quantity: txtQuantity.val().trim()
        };
        siteConsumptionProductList.push(siteConsumptionProductAddEdit);
        hdnSequenceNo.val("0");
    }
    if (flag == true) {
        GetSiteConsumptionProductList(siteConsumptionProductList);
    }
}
function EditProductRow(obj) {
    var row = $(obj).closest("tr");
    var sequenceNo = $(row).find("#hdnSequenceNo").val();
    var siteConsumptionDetailId = $(row).find("#hdnSiteConsumptionDetailId").val();
    var productId = $(row).find("#hdnProductId").val();
    var productName = $(row).find("#hdnProductName").val();
    var productMainGroupId = $(row).find("#hdnProductMainGroupId").val();
    var productMainGroupName = $(row).find("#hdnProductMainGroupName").val();
    var productCode = $(row).find("#hdnProductCode").val();
    //var productShortDesc = $(row).find("#hdnProductShortDesc").val();
    var uomName = $(row).find("#hdnUOMName").val();
    var quantity = $(row).find("#hdnQuantity").val();

    $("#txtProductName").val(productName);
    $("#hdnSiteConsumptionDetailId").val(siteConsumptionDetailId);
    $("#hdnSequenceNo").val(sequenceNo);
    $("#hdnProductId").val(productId);
    $("#ddlProductMainGroup").val(productMainGroupId);
    $("#hdnProductMainGroupName").val(productMainGroupName);
    $("#txtProductCode").val(productCode);
    //$("#txtProductShortDesc").val(productShortDesc);
    $("#txtUOMName").val(uomName);
    $("#txtQuantity").val(quantity);


    $("#btnAddProduct").hide();
    $("#btnUpdateProduct").show();

    ShowHideProductPanel(1);
}

function RemoveProductRow(obj) {
    if (confirm("Do you want to remove selected Product?")) {
        var row = $(obj).closest("tr");
        var fabricationDetailId = $(row).find("#hdnSiteConsumptionDetailId").val();
        ShowModel("Alert", "Product Removed from List.");
        row.remove();
    }
}

function GetSiteConsumptionProductList(siteConsumpitonProducts) {
    var hdnsiteConsumptionId = $("#hdnsiteConsumptionId");
    var requestData = { siteConsumptionProducts: siteConsumpitonProducts, siteConsumptionId: hdnsiteConsumptionId.val() };
    $.ajax({
        url: "../SiteConsumption/GetSiteConsumptionProductList",
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
function GetProductDetail(productId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetProductDetail",
        data: { productid: productId },
        dataType: "json",
        success: function (data) {
            $("#txtUOMName").val(data.UOMName);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}

function GetSiteConsumptionDetail(siteConsumptionId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../SiteConsumption/GetSiteConsumptionDetail",
        data: { siteConsumptionId: siteConsumptionId },
        dataType: "json",
        success: function (data) {
            $("#txtSiteConsumptionNo").val(data.SiteConsumptionNo);;
            $("#hdnsiteConsumptionId").val(data.SiteConsumptionId);;
            $("#txtConsumptionDate").val(data.SiteConsumptionDate);;
            $("#hdnCustomerId").val(data.CustomerId);;
            $("#txtCustomerName").val(data.CustomerName);
            GetCustomerBranchListByID(data.CustomerId, data.CustomerBranchId);
            $("#ddlCustomerBranch").val(data.CustomerBranchId);
            $("#ddlConsumptionStatus").val(data.ConsumptionStatus);
            $("#txtCustomerCode").val(data.CustomerCode);
            $("#ddlCompanyBranch").val(data.CompanyBranchId);
            $("#txtRemarks1").val(data.Remarks1);
            $("#txtRemarks2").val(data.Remarks2);
            $("#txtConsumedByUser").val(data.ConsumedByUser);
            if (data.ConsumptionStatus != null) {
                if (data.ConsumptionStatus.trim() == "Final") {
                    $("#btnUpdate").hide();
                    $("#btnReset").hide();
                }
                //else {
                //    $("#btnUpdate").show();
                //    $("#btnReset").show();
                //}
            }
            //if (data.ProjectStatus == 'True') {
            //    $("#chkstatus").attr("checked", true);
            //}
            //else {
            //    $("#chkstatus").attr("checked", false);
            //}
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}

function SaveData() {
    var txtSiteConsumptionNo = $("#txtSiteConsumptionNo");
    var hdnsiteConsumptionId = $("#hdnsiteConsumptionId");
    var txtConsumptionDate = $("#txtConsumptionDate");
    var txtCustomerName = $("#txtCustomerName");
    var hdnCustomerId = $("#hdnCustomerId");
    var ddlCustomerBranch = $("#ddlCustomerBranch");
    var ddlConsumptionStatus = $("#ddlConsumptionStatus");
    var txtCustomerCode = $("#txtCustomerCode");
    var ddlCompanyBranch = $("#ddlCompanyBranch");
    var txtRemarks1 = $("#txtRemarks1");
    var txtRemarks2 = $("#txtRemarks2");
    var txtConsumedByUser = $("#txtConsumedByUser");
    
    if (txtConsumptionDate.val().trim() == "" ) {
        ShowModel("Alert", "Please enter Consumption Date")
        txtConsumptionDate.focus();
        return false;
    }
    if (txtCustomerName.val().trim() == "") {
        ShowModel("Alert", "Please enter Customer Name")
        txtCustomerName.focus();
        return false;
    }
    if (ddlCustomerBranch.val() == null)
    {
        ShowModel("Alert", "Customer Branch Does Not exist , Please Select Another Customer ");
        txtCustomerName.focus();
        return false;
    }
    if (ddlCustomerBranch.val() == "" || ddlCustomerBranch.val() == 0) {
        ShowModel("Alert", "Please select Customer Branch Location")
        ddlCustomerBranch.focus();
        return false;
    }
    if (ddlCompanyBranch.val() == "" || ddlCompanyBranch.val() == 0) {
        ShowModel("Alert", "Please select Company Branch Location")
        ddlCompanyBranch.focus();
        return false;
    }
    if (txtConsumedByUser.val().trim() == "") {
        ShowModel("Alert", "Please enter Consumed User Name")
        txtConsumedByUser.focus();
        return false;
    }
    

    

    var siteConsumptionViewModel = {
        SiteConsumptionId: hdnsiteConsumptionId.val(),
        SiteConsumptionNo: txtSiteConsumptionNo.val().trim(),
        SiteConsumptionDate: txtConsumptionDate.val().trim(),
        CustomerId: hdnCustomerId.val(),
        CustomerBranchId: ddlCustomerBranch.val().trim(),
        ConsumptionStatus:ddlConsumptionStatus.val(),
       // CompanyId: ddlCompanyBranch.val().trim(),
        CompanyBranchId: ddlCompanyBranch.val(),
        ConsumedByUser: txtConsumedByUser.val(),
        Remarks1: txtRemarks1.val(),
        Remarks2: txtRemarks2.val()
    };

    var siteConsumptionProductList = [];
    $('#tblProductList tr').each(function (i, row) {
        var $row = $(row);
        var siteConsumptionDetailId = $row.find("#hdnSiteConsumptionDetailId").val();
        var productId = $row.find("#hdnProductId").val();
        var productName = $row.find("#hdnProductName").val();
        var productCode = $row.find("#hdnProductCode").val();
        var productShortDesc = $row.find("#hdnProductShortDesc").val();
        var uomName = $row.find("#hdnUOMName").val();
        var quantity = $row.find("#hdnQuantity").val();
        if (siteConsumptionDetailId != undefined) {

            var siteConsumptionProduct = {
                FabricationDetailId: siteConsumptionDetailId,
                ProductId: productId,
                ProductName: productName,
                ProductCode: productCode,
                ProductShortDesc: productShortDesc,
                UOMName: uomName,
                Quantity: quantity
            };
            siteConsumptionProductList.push(siteConsumptionProduct);
        }
    });


    //if (fabricationProductList.length == 0) {
    //    ShowModel("Alert", "Please Select at least 1 Product.")
    //    return false;
    //}
    var accessMode = 1;//Add Mode
    if (hdnsiteConsumptionId.val() != null && hdnsiteConsumptionId.val() != 0) {
        accessMode = 2;//Edit Mode
    }
    var requestData = { siteConsumptionViewModel: siteConsumptionViewModel, siteConsumptionProductsList: siteConsumptionProductList };
    $.ajax({
        url: "../SiteConsumption/AddEditSiteConsumption?AccessMode=" + accessMode + "",
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
                       window.location.href = "../SiteConsumption/AddEditSiteConsumption?consumptionSiteId=" + data.trnId + "&accessMode=3";
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
    $("#txtProjectName").val("");
    $("#txtProjectCode").val("");
    $("#txtCustomerName").val("");
    $("#ddlCustomerBranch").val("0");
    $("#chkstatus").prop("checked", true);
}

function stopRKey(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}
document.onkeypress = stopRKey;

function GetSiteProductAvailableStock(productId, customerId, customerBranchId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Product/GetSiteProductAvailableStock",
        data: { productid: productId, customerId: customerId, customerBranchId: customerBranchId },
        dataType: "json",
        success: function (data) {
            $("#txtAvailableStock").val(data);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}

function ResetPage() {
    if (confirm("Are you sure to reset all fields?")) {
        window.location.href = "../SiteConsumption/AddEditSiteConsumption?accessMode=1";
    }
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

$('body').on('change', '#ddlProductMainGroup', function () {
    $("#hdnProductMainGroupId").val($("#ddlProductMainGroup").val());
    $("#hdnProductMainGroupName").val($("#ddlProductMainGroup option:selected").text());
    $("#txtProductName").val("");
});