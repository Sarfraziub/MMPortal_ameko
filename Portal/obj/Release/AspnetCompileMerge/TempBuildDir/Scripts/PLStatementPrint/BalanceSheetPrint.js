
$(document).ready(function () {
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
    $("#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {
            GenerateReportParameters();
        }
    });
   


    var url = "../PLStatementPrint/BalanceSheetReport?reportLevel=1&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#btnPrintPDF').attr('href', url);
    url = "../PLStatementPrint/BalanceSheetReport?reportLevel=1&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=Excel";
    $('#btnPrintExcel').attr('href', url);
});


function ClearFields() {
    $("#ddlReportLevel").val("1");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    var url = "../PLStatementPrint/BalanceSheetReport?reportLevel=1&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#btnPrintPDF').attr('href', url);
    url = "../PLStatementPrint/BalanceSheetReport?reportLevel=1&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=Excel";
    $('#btnPrintExcel').attr('href', url);

}
function SearchBankVoucher() {
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");

    

    var requestData = { asOnDate: txtToDate.val() };
    $.ajax({
        url: "../PLStatementPrint/BalanceSheetGenerate",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                GenerateReportParameters();
                ShowPrintModel();
                
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
function GenerateReportParameters() {
    var url = "../PLStatementPrint/BalanceSheetReport?reportLevel=" + $("#ddlReportLevel").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#btnPrintPDF').attr('href', url);
    url = "../PLStatementPrint/BalanceSheetReport?reportLevel=" + $("#ddlReportLevel").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=Excel";
    $('#btnPrintExcel').attr('href', url);
}
