$(document).ready(function () {
    
   

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
    $("#txtResourceRequisitionNo").val("");
    $("#txtJobOpeningNo").val("");
    $("#txtJobTitle").val("");
    $("#txtJobPortalRefNo").val("");
    $("#ddlJobStatus").val("0");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
}
function SearchJobOpenings() {
    var txtResourceRequisitionNo = $("#txtResourceRequisitionNo");
    var txtJobOpeningNo = $("#txtJobOpeningNo");
    var txtJobPortalRefNo = $("#txtJobPortalRefNo");
    var txtJobTitle = $("#txtJobTitle");
    var ddlJobStatus = $("#ddlJobStatus");
  
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var requestData = {
        jobOpeningNo: txtJobOpeningNo.val().trim(), requisitionNo: txtResourceRequisitionNo.val().trim(), jobPortalRefNo: txtJobPortalRefNo.val().trim(),
        jobTitle: txtJobTitle.val(), fromDate: txtFromDate.val(), toDate: txtToDate.val(),jobStatus:ddlJobStatus.val() };
    $.ajax({
        url: "../JobOpening/GetJobOpeningList",
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