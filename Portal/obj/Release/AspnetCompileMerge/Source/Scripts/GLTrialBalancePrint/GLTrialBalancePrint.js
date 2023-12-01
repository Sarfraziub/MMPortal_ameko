
$(document).ready(function () {
    $("#txtAsOnDate").attr('readOnly', true);
    
    $("#txtAsOnDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {
            GenerateReportParameters();
        }
    });
    
    
  


    var url = "../GLTrialBalancePrint/GLTrialBalance2ColumnReport?asOnDate=" + $("#txtAsOnDate").val() + "&reportType=PDF";
    $('#btnPrintPDF').attr('href', url);
    url = "../GLTrialBalancePrint/GLTrialBalance2ColumnReport?asOnDate=" + $("#txtAsOnDate").val() + "&reportType=Excel";
    $('#btnPrintExcel').attr('href', url);
});


function ClearFields() {
    $("#txtAsOnDate").val($("#hdnFromDate").val());
    var url = "../GLTrialBalancePrint/GLTrialBalance2ColumnReport?asOnDate=" + $("#txtAsOnDate").val() + "&reportType=PDF";
    $('#btnPrintPDF').attr('href', url);
    url = "../GLTrialBalancePrint/GLTrialBalance2ColumnReport?asOnDate=" + $("#txtAsOnDate").val() + "&reportType=Excel";
    $('#btnPrintExcel').attr('href', url);

}

function SearchBankVoucher() {
    
    var txtFromDate = $("#txtAsOnDate");
    

    
    
    var requestData = { asOnDate: txtFromDate.val()};
    $.ajax({
        url: "../GLTrialBalancePrint/GLTrialBalance2ColumnGenerate",
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
    
    var url = "../GLTrialBalancePrint/GLTrialBalance2ColumnReport?asOnDate=" + $("#txtAsOnDate").val() + "&reportType=PDF";
        $('#btnPrintPDF').attr('href', url);
        url = "../GLTrialBalancePrint/GLTrialBalance2ColumnReport?asOnDate=" + $("#txtAsOnDate").val() + "&reportType=Excel";
        $('#btnPrintExcel').attr('href', url);
    
}
