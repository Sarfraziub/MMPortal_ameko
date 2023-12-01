$(document).ready(function () {
  
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


  var hdnAccessMode = $("#hdnAccessMode");
  var hdnchasisSerialMappingId = $("#hdnchasisSerialMappingId");
  if (hdnchasisSerialMappingId.val() != "" && hdnchasisSerialMappingId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetChasisSerialMappingDetail(hdnchasisSerialMappingId.val());
       }, 2000);



        if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            //$("input").attr('readOnly', true);
            //$("textarea").attr('readOnly', true);
            //$("select").attr('disabled', true);
            //$("#chkstatus").attr('disabled', true);
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

function SaveData() {
    var hdnchasisSerialMappingId = $("#hdnchasisSerialMappingId");
    var hdnProductId = $("#hdnProductId");
    var txtChasisSerialNo = $("#txtChasisSerialNo");
    var txtMotorNo = $("#txtMotorNo");
    var txtControllerNo = $("#txtControllerNo");

    var ddlColor = $("#ddlColor");
    var txtBatteryPower = $("#txtBatteryPower");
    var txtBatterySerialNo1 = $("#txtBatterySerialNo1");

    var txtBatterySerialNo2 = $("#txtBatterySerialNo2");
    var txtBatterySerialNo3 = $("#txtBatterySerialNo3");
    var txtBatterySerialNo4 = $("#txtBatterySerialNo4");
    var txtTier = $("#txtTier");

    var chkstatus = $("#chkStatus").is(':checked') ? true : false;
    var chkFrontGlassAvailable = $("#chkFrontGlassAvailable").is(':checked') ? true : false;
    var checkViperAvailable = $("#checkViperAvailable").is(':checked') ? true : false;
    var checkRearShockerAvailable = $("#checkRearShockerAvailable").is(':checked') ? true : false;
    var checkChargerAvailable = $("#checkChargerAvailable").is(':checked') ? true : false;
 
    
    if (hdnProductId.val() == "" || hdnProductId.val() == "0") {
        ShowModel("Alert", "Please select Product")
        return false;
    }
  
    if (txtChasisSerialNo.val().trim() == "") {
        ShowModel("Alert", "Please Enter Chasis Serial No")
        txtChasisSerialNo.focus();
        return false;
    }
    if (txtMotorNo.val().trim() == "") {
        ShowModel("Alert", "Please Enter Motor No")
        txtMotorNo.focus();
        return false;
    }
    if (txtControllerNo.val().trim() == "") {
        ShowModel("Alert", "Please Enter Controller No")
        txtControllerNo.focus();
        return false;
    }
    if (ddlColor.val() == "" || ddlColor.val() == "0") {
        ShowModel("Alert", "Please select Color")
        ddlColor.focus();
        return false;
    }

    

    if (txtBatteryPower.val().trim() == "") {
        ShowModel("Alert", "Please Enter Battery Power")
        txtBatteryPower.focus();
        return false;
    }



    if (txtBatterySerialNo1.val().trim() == "") {
        ShowModel("Alert", "Please Enter Battery SerialNo1")
        txtBatterySerialNo1.focus();
        return false;
    }
    if (txtTier.val().trim() == "") {
        ShowModel("Alert", "Please Enter Tier")
        txtTier.focus();
        return false;
    }

    var chasisSerialMappingViewModel = {
                    MappingId:hdnchasisSerialMappingId.val(),
                    ProductId:hdnProductId.val(),
                    ChasisSerialNo:txtChasisSerialNo.val(),
                    MotorNo:txtMotorNo.val(),
                    ControllerNo:txtControllerNo.val(),
                    Color:ddlColor.val(),
                    BatteryPower: txtBatteryPower.val(),
                    BatterySerialNo1:txtBatterySerialNo1.val(),
                    BatterySerialNo2:txtBatterySerialNo2.val(),
                    BatterySerialNo3:txtBatterySerialNo3.val(),
                    BatterySerialNo4:txtBatterySerialNo4.val(),
                    Tier:txtTier.val(),
                    FrontGlassAvailable: chkFrontGlassAvailable,
                    ViperAvailable: checkViperAvailable,
                    RearShockerAvailable:checkRearShockerAvailable,
                    ChargerAvailable:checkChargerAvailable,
                    ChasisSerialMapping_Status: chkstatus
    };

    
    var requestData = { chasisSerialMappingViewModel: chasisSerialMappingViewModel};
    $.ajax({
        url: "../ChasisSerialMapping/AddEditChasisSerialMapping",
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
                      window.location.href = "../ChasisSerialMapping/AddEditChasisSerialMapping"; 
                     // window.location.href = "../ChasisSerialMapping/AddEditChasisSerialMapping?chasisSerialMappingId=" + data.trnId + "&AccessMode=2";
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
function GetChasisSerialMappingDetail(mappingId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../ChasisSerialMapping/GetChasisSerialMappingDetail",
        data: { mappingId: mappingId },
        dataType: "json",
        success: function (data) {

            $("#hdnchasisSerialMappingId").val(data.MappingId);
            $("#hdnProductId").val(data.ProductId);
            $("#txtProductName").val(data.ProductName);
            $("#txtChasisSerialNo").val(data.ChasisSerialNo);
            $("#txtMotorNo").val(data.MotorNo);
            $("#txtControllerNo").val(data.ControllerNo);
            $("#ddlColor").val(data.Color);
            $("#txtBatteryPower").val(data.BatteryPower);
            $("#txtBatterySerialNo1").val(data.BatterySerialNo1);
            $("#txtBatterySerialNo2").val(data.BatterySerialNo2);
            $("#txtBatterySerialNo3").val(data.BatterySerialNo3);
            $("#txtBatterySerialNo4").val(data.BatterySerialNo4);
            $("#txtTier").val(data.Tier);

            if (data.ChasisSerialMapping_Status == true) {
                $("#chkstatus").attr("checked", true);
            }
            else {
                $("#chkstatus").attr("checked", false);
            }

           
            if (data.FrontGlassAvailable == true) {
                $("#chkFrontGlassAvailable").attr('checked', true);
            }
            else {
                $("#chkFrontGlassAvailable").attr('checked', false);
            }

            if (data.ViperAvailable == true) {
                $("#checkViperAvailable").attr('checked', true);
            }
            else {
                $("#checkViperAvailable").attr('checked', false);
            }

            if (data.RearShockerAvailable == true) {
                $("#checkRearShockerAvailable").attr('checked', true);
            }
            else {
                $("#checkRearShockerAvailable").attr('checked', false);
            }

            if (data.ChargerAvailable == true) {
                $("#checkChargerAvailable").attr('checked',true);
            }
            else {
                $("#checkChargerAvailable").attr('checked', false);
            }

           
            $("#btnAddNew").show();
            $("#btnPrint").show();
            $("#btnEmail").show();

        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}
function ClearFields() {

    $("#hdnchasisSerialMappingId").val("0");
    $("#hdnProductId").val("0");
    $("#txtProductName").val("");
    $("#txtChasisSerialNo").val("");
    $("#txtMotorNo").val("");
    $("#txtControllerNo").val("");
    $("#ddlColor").val("");
    $("#txtBatteryPower").val("");
    $("#txtBatterySerialNo1").val("");
    $("#txtBatterySerialNo2").val("");
    $("#txtBatterySerialNo3").val("");
    $("#txtBatterySerialNo4").val("");
    $("#txtTier").val("");
    $("#chkstatus").attr('checked', false);
    
    
    $("#chkFrontGlassAvailable").attr('checked', false);
    $("#checkViperAvailable").attr('checked', false);
    $("#checkRearShockerAvailable").attr('checked', false);
    $("#checkChargerAvailable").attr('checked', false);

    $("#btnSave").show();
    $("#btnUpdate").hide();


}
