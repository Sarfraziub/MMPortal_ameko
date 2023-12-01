$(document).ready(function () {
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnSLDetailId = $("#sLDetailId");

    var bal='';
    var days='';
  
 

    $("#txtGL").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../SLDetail/GetGLAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term, slTypeId: $("#ddlSLTypeId").val() },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.GLHead, value: item.GLId, code: item.GLCode };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtGL").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            //$("#txtSLHead").val(ui.item.label);
            $("#hdnGLId").val(ui.item.value);
            $("#txtGL").val(ui.item.label);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                // $("#txtSLHead").val("");
                $("#hdnGLId").val("0");
                $("#txtGL").val("");
                ShowModel("Alert", "Please select GL from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + " || " + item.code + "</b></div>")
      .appendTo(ul);
};



    $("#txtSL").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Voucher/GetSLAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term, slTypeId: $("#ddlSLTypeId").val() },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.SLHead, value: item.SLId, code: item.SLCode };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtSL").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            //$("#txtSLHead").val(ui.item.label);
            $("#hdnSLId").val(ui.item.value);
            $("#txtSL").val(ui.item.label);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
               // $("#txtSLHead").val("");
                $("#hdnSLId").val("0");
                $("#txtSL").val("");
                ShowModel("Alert", "Please select SL from List")

            }
            return false;
        }

    })
 .autocomplete("instance")._renderItem = function (ul, item) {
     return $("<li>")
       .append("<div><b>" + item.label + " || " + item.code + "</b></div>")
       .appendTo(ul);
 };
    
});

function getCheckBalanceCredit(obj) {
    var row = $(obj).closest("tr");
    $(row).find("#txtOpeningBalanceDebit").on("input", function () {
        var regexp = /\D/g ;
        if ($(this).val().match(regexp)) {
            $(this).val($(this).val().replace(regexp, ''));
            $(this).val('0.00');
        }
    });
    var txtOpeningBalanceCredit = $(row).find("#txtOpeningBalanceCredit");
    var txtOpeningBalanceDebit = $(row).find("#txtOpeningBalanceDebit");
    if (txtOpeningBalanceCredit.val() != '0.00')
    {
        ShowModel("Alert", "Only One Value Accept Created or Debit");
        txtOpeningBalanceDebit.val("0.00");
        txtOpeningBalanceCredit.focus();
        return false;
    }
 }
function getCheckBalanceDebit(obj) {
    var row = $(obj).closest("tr");
    $(row).find("#txtOpeningBalanceCredit").on("input", function () {
        var regexp = /\D/g;
        if ($(this).val().match(regexp)) {
            $(this).val($(this).val().replace(regexp, ''));
            $(this).val('0.00');
        }
    });
    var txtOpeningBalanceDebit = $(row).find("#txtOpeningBalanceDebit");
    var txtOpeningBalanceCredit = $(row).find("#txtOpeningBalanceCredit");
    if (txtOpeningBalanceDebit.val() != '0.00') {
          ShowModel("Alert", "Only One Value Accept Created or Debit");
        txtOpeningBalanceCredit.val('0.00');
        txtOpeningBalanceDebit.focus();
    }

}


BindSLTypeList();

function BindSLTypeList() {
    $.ajax({
        type: "GET",
        url: "../SL/GetSLTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlSLTypeId").append($("<option></option>").val("0").html("Select SL Type"));
            $.each(data, function (i, item) {
                $("#ddlSLTypeId").append($("<option></option>").val(item.SLTypeId).html(item.SLTypeName));
            });
        },
        error: function (Result) {
            $("#ddlSLTypeId").append($("<option></option>").val(0).html("Select SL Type"));
        }
    });
}


function GetSLDetailList() {
    var ddlSLTypeId = $("#ddlSLTypeId");
    var hdnGLId = $("#hdnGLId");
    var hdnSLId = $("#hdnSLId");
    if (ddlSLTypeId.val() == 0)
    {
        ShowModel("Alert", "Please select SL Type")
        ddlSLTypeId.focus();
        return false;
    }
    if (hdnGLId.val() == "" || hdnGLId.val() == "0") {
        ShowModel("Alert", "Please select GL")
        $("#txtGL").focus();
        return false;
    }
    var requestData = { SLTypeId: ddlSLTypeId.val(), GLId: hdnGLId.val(), SLId: hdnSLId.val()};
    $.ajax({
        url: "../SLDetail/GetSLDetailList",
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



function SaveData() {
    var ddlSLTypeId = $("#ddlSLTypeId");
    var hdnGLId = $("#hdnGLId");

    if ( ddlSLTypeId.val() == "0") {
        ShowModel("Alert", "Please select SL Type")
        ddlSLTypeId.focus();
        return false;
    }
    
    if (hdnGLId.val() == "" || hdnGLId.val()=="0") {
        ShowModel("Alert", "Please select GL")
        ddlSLTypeId.focus();
        return false;
    }
    var mappingStatus = false;
    var SLDetailList = [];
    $('.sldetail-list tr').each(function (i, row) {
        var $row = $(row);
        var status = $row.find("#checkstatus").is(':checked') ? true : false;
        var hdnSLDetailId = $row.find("#hdnSLDetailId").val();
        var hdnGLId = $("#hdnGLId").val();
        var hdnSLId = $row.find("#hdnSLId").val();
        var hdnSLCode = $row.find("#hdnSLCode").val();
        var hdnSLHead = $row.find("#hdnSLHead").val();
        var txtOpeningBalanceDebit = $row.find("#txtOpeningBalanceDebit").val();;
        var txtOpeningBalanceCredit = $row.find("#txtOpeningBalanceCredit").val();
        if (hdnSLDetailId != undefined && status==true) {
            var slDetails = {
                SLDetailId: hdnSLDetailId,
                GLId: hdnGLId,
                SLId: hdnSLId,
                OpeningBalanceDebit: txtOpeningBalanceDebit,
                OpeningBalanceCredit: txtOpeningBalanceCredit,
                SLDetailStatus:true
            };
            SLDetailList.push(slDetails);
            mappingStatus = true;
        }
    });
    if (mappingStatus == false) {
        ShowModel("Alert", "Please select at least one SL Detail");
        return false;
    }
    var requestData = { sLDetailList: SLDetailList };
    $.ajax({
        url: "../SLDetail/AddEditSLDetail",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                //$("#btnUpdate").show();
          
                // $("#btnUpdate").hide();

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
function CheckAll(obj) {
   
    $('.checkstatus').prop('checked', obj.checked);
}

function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);
}
function ClearFields() {
    $("#hdnSLId").val("0");
    $("#hdnGLId").val("0");
    $("#txtSL").val("");
    $("#txtGL").val("");
    $("#txtSLHead").val("");
    $("#txtRefCode").val("");
    $("#ddlSLTypeId").val("0");
    $("#ddlCostCenterId").val("0");
   $("#chkstatus").prop("checked", true);

}
