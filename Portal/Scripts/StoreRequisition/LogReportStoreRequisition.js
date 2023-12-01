
$(document).ready(function () {
    $("#txtFromDate,#txtToDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        onSelect: function (selected) {

        }
    });
    $("#txtFromDate").attr('readOnly', true);
    $("#txtToDate").attr('readOnly', true);
});
function OpenPrintPopup() {
    //var txtCustomerName = $("#txtCustomerName");
    //var hdnCustomerId = $("#hdnCustomerId");
    //var ddlCustomerBranch = $("#ddlCustomerBranch");
    //var txtFromDate = $("#txtFromDate");
    //var txtToDate = $("#txtToDate");
    //var hdnProductId = $("#hdnProductId");
    //if (txtCustomerName.val().trim() == "") {
    //    ShowModel("Alert", "Please enter Customer Name")
    //    txtCustomerName.focus();
    //    return false;
    //}
    //if (ddlCustomerBranch.val() == null) {
    //    ShowModel("Alert", "Customer Branch Does Not exist , Please Select Another Customer ");
    //    txtCustomerName.focus();
    //    return false;
    //}
    //if (ddlCustomerBranch.val() == ""||ddlCustomerBranch.val()==0) {
    //    ShowModel("Alert", "Please Select Customer Site");
    //    ddlCustomerBranch.focus();
    //    return false;
    //}
    //if (txtFromDate.val().trim() == "") {
    //    ShowModel("Alert", "Please enter From Date")
    //    txtFromDate.focus();
    //    return false;
    //}
    //if (txtToDate.val().trim() == "") {
    //    ShowModel("Alert", "Please enter To Date")
    //    txtToDate.focus();
    //    return false;
    //}
    $("#printModel").modal(); 
    GenerateReportParameters();
   
}
function OpenPrintSitePopup() {
    //var txtCustomerName = $("#txtCustomerName");
    //var hdnCustomerId = $("#hdnCustomerId");
    //var ddlCustomerBranch = $("#ddlCustomerBranch");
    //var txtFromDate = $("#txtFromDate");
    //var txtToDate = $("#txtToDate");
    //var hdnProductId = $("#hdnProductId");
    //if (txtCustomerName.val().trim() == "") {
    //    ShowModel("Alert", "Please enter Customer Name")
    //    txtCustomerName.focus();
    //    return false;
    //}
    $("#printModel").modal();
    //GenerateSiteConsumptionDetailReportParameters();

}
function ShowHidePrintOption() {
    var reportOption = $("#ddlPrintOption").val();
    if (reportOption == "PDF") {
        $("#btnPdf").show();
        $("#btnExcel").hide();
    }
    else if (reportOption == "Excel") {
        $("#btnExcel").show();
        $("#btnPdf").hide();
    }
}
function GenerateReportParameters() {    
    //var txtFromDate = $("#txtFromDate");
    //var txtToDate = $("#txtToDate");
    var url = "../StoreRequisition/LogStoreRequisitionReport?requisitionId=0" + "&requisitionNo=" + $("#txtRequisitionNo").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#btnPdf').attr('href', url);
    var urlSummary = "../StoreRequisition/LogStoreRequisitionReport?requisitionId=0" + "&requisitionNo=" + $("#txtRequisitionNo").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=Excel";
    $('#btnExcel').attr('href', urlSummary);


}
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}

function ChangeSearchRequisition()
{
    var ddlRequisition = $("#ddlRequisition").val();
    if (ddlRequisition == "With Requisitions No") {
        $("#divRequisitionNo").show();
    }
    else {
        $("#divRequisitionNo").hide();
    }
}
