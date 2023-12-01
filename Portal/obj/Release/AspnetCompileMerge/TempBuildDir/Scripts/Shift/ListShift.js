
$(document).ready(function () {
    BindShiftTypeList();
       SearchShift();
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
});
 
function BindShiftTypeList() {
    $.ajax({
        type: "GET",
        url: "../Shift/GetShiftTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlShiftType").append($("<option></option>").val(0).html("Select Shift Type"));
            $.each(data, function (i, item) {
                $("#ddlShiftType").append($("<option></option>").val(item.ShiftTypeId).html(item.ShiftTypeName));
            });
        },
        error: function (Result) {
            $("#ddlShiftType").append($("<option></option>").val(0).html("Select Shift Type"));
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

 


function ClearFields() {
    $("#txtShiftName").val(""); 
    $("#ddlShiftType").val("0");
    $("#ddlShiftStatus").val("");
    
}
function SearchShift() {
    var txtShiftName = $("#txtShiftName");
    var ddlShiftType = $("#ddlShiftType"); 
    var ddlShiftStatus = $("#ddlShiftStatus");
    var requestData = {
        shiftName: txtShiftName.val(), shiftTypeId: ddlShiftType.val(),  
        shiftTypeStatus: ddlShiftStatus.val(),
    };
    $.ajax({
        url: "../Shift/GetShiftList",
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
function Export() {
    var divList = $("#divList");
    ExporttoExcel(divList);
}