$(document).ready(function () {
    $("#ddlCountry").html("");
    BindCountryList();
    $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
   
    
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnCityId = $("#hdnCityId");
    
    
    if (hdnCityId.val() != "" && hdnCityId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0")
    {
        GetCityDetail(hdnCityId.val());
        if (hdnAccessMode.val() == "3")
        {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
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
        BindCountryList();
    }
    $("#txtCityName").focus();
 


});

function BindCountryList() {
    
    $.ajax({
        type: "GET",
        url: "../City/GetCountryList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlCountry").append($("<option></option>").val(0).html("-Select Country-"));
            $.each(data, function (i, item) {

                $("#ddlCountry").append($("<option></option>").val(item.CountryId).html(item.CountryName));
            });
        },
        error: function (Result) {
            $("#ddlCountry").append($("<option></option>").val(0).html("-Select Country-"));
        }
    });
}



function BindStateList(stateId) {

    var countryId = $("#ddlCountry option:selected").val();
    //alert(stateId);
    $("#ddlState").val(0);
    $("#ddlState").html("");
    if (countryId != undefined && countryId != "" && countryId != "0") {
        var data = { countryId: countryId };
        $.ajax({
            type: "GET",
            url: "../City/GetStateList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {

                $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
                $.each(data, function (i, item) {
                    $("#ddlState").append($("<option></option>").val(item.StateId).html(item.StateName));
                });
                $("#ddlState").val(stateId);

            },
            error: function (Result) {
                $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
            }
        });
    }
    else {

        $("#ddlState").append($("<option></option>").val(0).html("-Select State-"));
    }

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
 

function GetCityDetail(cityId) {
    $.ajax({
        type: "GET",
        asnc:false,
        url: "../City/GetCityDetail",
        data: { cityId: cityId },
        dataType: "json",
        success: function (data) { 
            $("#txtCityName").val(data.CityName);
            $("#ddlCountry").val(data.CountryId);
            BindStateList(data.StateId);
            $("#ddlState").val(data.StateId);
            $("#chkstatus").val(data.CityStatus);
            
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
    
}

function SaveData()
{
    var txtCityName = $("#txtCityName");
    var hdnCityId = $("#hdnCityId"); 
    //var txtStateCode = $("#txtStateCode"); 
    var ddlCountry = $("#ddlCountry");
    var ddlState = $("#ddlState");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;
    if (ddlCountry.val().trim() == "0") {
        ShowModel("Alert", "Please select country name")
        ddlCountry.focus();
        return false;
    }
    if (ddlState.val().trim() == "0") {

        ShowModel("Alert", "Please select state name")
        ddlState.focus();
        return false;
    }
      if (txtCityName.val().trim() == "")
      {
          
        ShowModel("Alert","Please enter city name")
        txtCityName.focus();
        return false;
      }
     
     
     var cityViewModel = {
         CityId: hdnCityId.val(),
         CityName: txtCityName.val().trim(),
         StateId: ddlState.val(),
         CityStatus: chkstatus
    };
    var requestData ={cityViewModel:cityViewModel };
    $.ajax({
        url: "../City/AddEditCity",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify( requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status=="SUCCESS")
            {
                ShowModel("Alert", data.message);
                ClearFields();
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
     $("#txtCityName").val("");
     $("#hdnCityID").val("0");
     $("#hdnAccessMode").val("0");
     $("#ddlState").val("0");
     $("#ddlCountry").val("0");
     $("#chkstatus").prop('checked', false);
    
}
