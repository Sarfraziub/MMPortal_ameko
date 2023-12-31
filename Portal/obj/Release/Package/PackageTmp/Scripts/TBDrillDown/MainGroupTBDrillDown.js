﻿
$(document).ready(function () {
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
    $("#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {
         //   GenerateReportParameters();
        }
    });
});


function ClearFields() {
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
}
function GenerateDrillDown() {
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");

    
    var requestData = { fromDate: txtFromDate.val(), toDate: txtToDate.val() };
    $.ajax({
        url: "../TBDrillDown/TrialBalanceDrillDownGenerate",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                
                //GenerateReportParameters();
                if ($("input[name='ReportLevel']:checked").val() == "MainGroup") {
                    window.location.href = "../TBDrillDown/MainGroupWiseTBDrillDown?ReportLevel=" + $("input[name='ReportLevel']:checked").val() + "&FromDate=" + txtFromDate.val() + "&ToDate=" + txtToDate.val() + "&AccessMode=3";
                }
                else if ($("input[name='ReportLevel']:checked").val() == "SubGroup") {
                    window.location.href = "../TBDrillDown/SubGroupWiseTBDrillDown?ReportLevel=" + $("input[name='ReportLevel']:checked").val() + "&FromDate=" + txtFromDate.val() + "&ToDate=" + txtToDate.val() + "&AccessMode=3";
                }
                else if ($("input[name='ReportLevel']:checked").val() == "GLWise") {
                    window.location.href = "../TBDrillDown/GLWiseTBDrillDown?ReportLevel=" + $("input[name='ReportLevel']:checked").val() + "&FromDate=" + txtFromDate.val() + "&ToDate=" + txtToDate.val() + "&AccessMode=3";
                }
                else if ($("input[name='ReportLevel']:checked").val() == "SLWise") {
                    window.location.href = "../TBDrillDown/SLWiseTBDrillDown?ReportLevel=" + $("input[name='ReportLevel']:checked").val() + "&FromDate=" + txtFromDate.val() + "&ToDate=" + txtToDate.val() + "&AccessMode=3";
                }
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
function ShowPrintModel() {
    $("#printModel").modal();
    
}
//function GenerateReportParameters() {
//    if ($("#hdnGLId").val() != "0" && $("#hdnGLId").val() != "") {
//        var url = "../SubLedgerPrint/SubLedgerWoFySingleGLReport?fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
//        $('#btnPrintPDF').attr('href', url);
//        url = "../SubLedgerPrint/SubLedgerWoFySingleGLReport?fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=Excel";
//        $('#btnPrintExcel').attr('href', url);
//    }
//    else
//    {
//        var url = "../SubLedgerPrint/SubLedgerWoFyAllGLReport?fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
//        $('#btnPrintPDF').attr('href', url);
//        url = "../SubLedgerPrint/SubLedgerWoFyAllGLReport?fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=Excel";
//        $('#btnPrintExcel').attr('href', url);
//    }
//}
