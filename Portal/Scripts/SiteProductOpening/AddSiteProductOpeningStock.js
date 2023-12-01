
$(document).ready(function () {
    BindFinYearList();
    BindCompanyBranch();
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnSiteOpeningTrnd = $("#hdnSiteOpeningTrnId");
    $("#txtProductCode").attr('readOnly', true);
    $("#txtProductShortDesc").attr('readOnly', true);
    if (hdnSiteOpeningTrnd.val() != "" && hdnSiteOpeningTrnd.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0")
    {
        setTimeout(
        function () {
            GetSiteProductOpeningDetail(hdnSiteOpeningTrnd.val());
        }, 2000);
        
        
        if (hdnAccessMode.val() == "3")
        {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("select").attr('disabled', true);
        }
        else
        {
            $("#btnSave").hide();
            $("#btnUpdate").show();
            $("#btnReset").show();
        }
    }
    else
    {
        $("#btnSave").show();
        $("#btnUpdate").hide();
        $("#btnReset").show();
    }
  //  $("#txtProductName").focus();
  
    $("#txtProductName").autocomplete({
        minLength: 0,
        source:function(request,response){
            $.ajax({
                url: "../Product/GetProductAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.ProductName, value: item.Productid,desc:item.ProductShortDesc,code:item.ProductCode };
                    }))}
            })},
        focus: function (event, ui) {
            $("#txtProductName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtProductName").val(ui.item.label);
            $("#hdnProductId").val(ui.item.value);
            $("#txtProductShortDesc").val(ui.item.desc);
            $("#txtProductCode").val(ui.item.code);
            return false;
        },
        change:function(event,ui)
        {
            if (ui.item == null)
            {
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
function ValidEmailCheck(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};
function BindFinYearList()
{
    $.ajax({
        type: "GET",
        url: "../Product/GetFinYearList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlFinYear").append($("<option></option>").val(0).html("-Select Fin. Year-"));
            $.each(data, function (i, item) {
                $("#ddlFinYear").append($("<option></option>").val(item.FinYearId).html(item.FinYearDesc));
            });
        },
        error: function (Result) {
            $("#ddlFinYear").append($("<option></option>").val(0).html("-Select Fin. Year-"));
        }
    });
}

function BindCompanyBranch() {
    $.ajax({
        type: "GET",
        url: "../ProductOpeningStock/GetCompanyBranchList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlcompanybranch").append($("<option></option>").val(0).html("-Select Company Branch-"));
            $.each(data, function (i, item) {
                $("#ddlcompanybranch").append($("<option></option>").val(item.CompanyBranchId).html(item.BranchName));
            });
        },
        error: function (Result) {
            $("#ddlcompanybranch").append($("<option></option>").val(0).html("-Select Company Branch-"));
        }
    });
}




function GetSiteProductOpeningDetail(siteOpeningTrnId) {
    $.ajax({
        type: "GET",
        asnc:false,
        url: "../SiteProductOpeningStock/GetSiteProductOpeningDetail",
        data: { siteOpeningTrnId: siteOpeningTrnId },
        dataType: "json",
        success: function (data) {
            $("#hdnProductId").val(data.ProductId);
            $("#txtProductName").val(data.ProductName);
            $("#txtProductCode").val(data.ProductCode);
            $("#txtProductShortDesc").val(data.ProductShortDesc);
            $("#ddlFinYear").val(data.FinYearId);
            $("#ddlcompanybranch").val(data.CompanyBranchId);
            $("#txtCustomerName").val(data.CustomerName);
            $("#hdnCustomerId").val(data.CustomerId);
            GetCustomerBranchListByID(data.CustomerId, data.CustomerBranchId);

            $("#txtOpeningQty").val(data.OpeningQty);
            
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
    
}
function SaveData()
{
    var hdnSiteOpeningTrnid = $("#hdnSiteOpeningTrnId");
    var hdnProductId = $("#hdnProductId");

    var txtProductName = $("#txtProductName");
    var txtProductCode = $("#txtProductCode");
    var txtProductShortDesc = $("#txtProductShortDesc");

    var txtOpeningQty = $("#txtOpeningQty");
    var ddlFinYear = $("#ddlFinYear");
    var ddlcompanybranch = $("#ddlcompanybranch");
     
    var txtCustomerName = $("#txtCustomerName");
    var hdnCustomerId = $("#hdnCustomerId");
    var ddlCustomerBranch = $("#ddlCustomerBranch");
    if (txtProductName.val().trim() == "")
    {
        ShowModel("Alert","Please enter Product Name")
        txtProductName.focus();
        return false;
    }

    if (hdnProductId.val() == "" || hdnProductId.val() == "0") {
        ShowModel("Alert", "Please select Product from list")
        txtProductName.focus();
        return false;
    }
 
    if (ddlFinYear.val() == "" || ddlFinYear.val() == "0") {
        ShowModel("Alert", "Please select Financial Year")
        ddlFinYear.focus();
        return false;
    }
    if (ddlcompanybranch.val() == "" || ddlcompanybranch.val() == "0" || ddlcompanybranch.val() == null) {
        ShowModel("Alert", "Please select Company Branch")
        ddlcompanybranch.focus();
        return false;
    }
    if (txtOpeningQty.val() == "") {
        ShowModel("Alert", "Please enter Product Opening Qty")
        txtOpeningQty.focus();
        return false;
    }
    if (txtCustomerName.val().trim() == "") {
        ShowModel("Alert", "Please enter Customer Name")
        txtCustomerName.focus();
        return false;
    }
    if (ddlCustomerBranch.val() == "" || ddlCustomerBranch.val() == "0" || ddlCustomerBranch.val() == null) {
        ShowModel("Alert", "Please select Customer Branch")
        ddlCustomerBranch.focus();
        return false;
    }
    var siteProductOpeningViewModel = {
        SiteOpeningTrnId: hdnSiteOpeningTrnid.val(), ProductId: hdnProductId.val(),
        FinYearId: ddlFinYear.val(), CompanyBranchId: ddlcompanybranch.val(), OpeningQty: txtOpeningQty.val(),
        CustomerId:hdnCustomerId.val(),CustomerBranchId:ddlCustomerBranch.val()
    };

    var accessMode = 1;//Add Mode
    if (hdnSiteOpeningTrnid.val() != null && hdnSiteOpeningTrnid.val() != 0) {
        accessMode = 2;//Edit Mode
    }
    var requestData = { siteProductOpeningViewModel: siteProductOpeningViewModel };
    $.ajax({
        url: "../SiteProductOpeningStock/AddEditSiteProductOpening?AccessMode=" + accessMode + "",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status=="SUCCESS")
            {
                ShowModel("Alert", data.message);
                ClearFields();
                setTimeout(
         function () {
             window.location.href = "../SiteProductOpeningStock/AddEditSiteProductOpening?accessMode=1";
         }, 2000);
                $("#btnSave").show();
                $("#btnUpdate").hide();
            }
            else
            {
                ShowModel("Error", data.message)
            }
        },
        error: function (err) {
            ShowModel("Error", err)
        }
    });
}
function ShowModel(headerText,bodyText)
{
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);
}
function ClearFields()
{
    $("#hdnSiteOpeningTrnid").val("0");
    $("#hdnAccessMode").val("3");
    $("#hdnProductId").val("0");
    $("#txtProductName").val("");
    $("#txtProductCode").val("");
    $("#txtProductShortDesc").val("");
    $("#ddlFinYear").val("0");
    $("#ddlcompanybranch").val("0");
    $("#txtOpeningQty").val("0");
    
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
function ResetPage() {
    if (confirm("Are you sure to reset all fields?")) {
        window.location.href = "../SiteProductOpeningStock/AddEditSiteProductOpening?accessMode=1";
    }
}