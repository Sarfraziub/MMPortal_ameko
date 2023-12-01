
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
    BindBookTypeList();
    GenerateReportParameters();
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
function BindBookTypeList() {
    var requestData = { bookType: "CN" };
    $.ajax({
        type: "GET",
        url: "../CreditNoteVoucher/GetBookList",
        data: requestData,
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlBook").append($("<option></option>").val(0).html("-Select Credit Note-"));
            $.each(data, function (i, item) {
                $("#ddlBook").append($("<option></option>").val(item.BookId).html(item.BookName));
            });
        },
        error: function (Result) {
            $("#ddlBook").append($("<option></option>").val(0).html("-Select Credit Note-"));
        }
    });
}
function ClearFields() {
    $("#ddlBook").val("0");
    $("#ddlVoucherMode").val("");
    $("#txtVoucherNo").val("");
    $("#txtFromDate").val($("#hdnFromDate").val());
    $("#txtToDate").val($("#hdnToDate").val());
    $("#ddlVoucherStatus").val("0");
    
}
function SearchCreditNoteVoucher() {
    var ddlBook = $("#ddlBook");
    var ddlVoucherMode = $("#ddlVoucherMode");
    var txtVoucherNo = $("#txtVoucherNo");
    var ddlVoucherStatus = $("#ddlVoucherStatus");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");

    if (ddlBook.val() == "" || ddlBook.val() == "0") {
        alert("Please select Bank Book")
        ddlBook.focus();
        return false;
    }

    var requestData = { bookId: ddlBook.val(), voucherMode: ddlVoucherMode.val(), voucherNo: txtVoucherNo.val().trim(), fromDate: txtFromDate.val(), toDate: txtToDate.val(), voucherStatus: ddlVoucherStatus.val() };
    $.ajax({
        url: "../CreditNoteVoucher/GetCreditNoteVoucherList",
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
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}

function OpenPrintPopup() {
    var ddlBook = $("#ddlBook");
    if (ddlBook.val() == "" || ddlBook.val() == "0") {
        alert("Please Select Credit Note Book")
        return false;
    }
    else {
        $("#printModel").modal();
        GenerateReportParameters();
    }
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

    var url = "../CreditNoteVoucher/GenerateCreditNoteVoucherReport?bookId=" + $("#ddlBook").val() + "&voucherMode=" + $("#ddlVoucherMode").val() + "&voucherNo=" + $("#txtVoucherNo").val() + "&voucherStatus=" + $("#ddlVoucherStatus").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=PDF";
    $('#btnPdf').attr('href', url);
    url = "../CreditNoteVoucher/GenerateCreditNoteVoucherReport?bookId=" + $("#ddlBook").val() + "&voucherMode=" + $("#ddlVoucherMode").val() + "&voucherNo=" + $("#txtVoucherNo").val() + "&voucherStatus=" + $("#ddlVoucherStatus").val() + "&fromDate=" + $("#txtFromDate").val() + "&toDate=" + $("#txtToDate").val() + "&reportType=Excel";
    $('#btnExcel').attr('href', url);

}