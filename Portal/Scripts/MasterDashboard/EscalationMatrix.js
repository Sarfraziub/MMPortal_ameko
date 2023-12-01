$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });    
    SearchPurchaseIndentEscalation();
    SearchRequisitionEscalation();
    SearchPOEscalation();
    SearchPOLessThan25KEscalation();
    SearchPurchaseIndentForQuotationEscalation();
});


function SearchPurchaseIndentEscalation() {
    var requestData = {};
    $.ajax({
        url: "../PurchaseIndent/GetPurchaseIndentEscalationList",
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

function SearchRequisitionEscalation() {
    var requestData = {};
    $.ajax({
        url: "../StoreRequisition/GetStoreRequisitionEscalationList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divRequisitionList").html("");
            $("#divRequisitionList").html(err);
        },
        success: function (data) {
            $("#divRequisitionList").html("");
            $("#divRequisitionList").html(data);
        }
    });
}

function SearchPOEscalation() {
    var requestData = {};
    $.ajax({
        url: "../PO/GetPOEscalationList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divPOEscalationList").html("");
            $("#divPOEscalationList").html(err);
        },
        success: function (data) {
            $("#divPOEscalationList").html("");
            $("#divPOEscalationList").html(data);
        }
    });
}

function SearchPOLessThan25KEscalation() {
    var requestData = {};
    $.ajax({
        url: "../PO/GetPOLessThan25KEscalationList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divPOLessThan25KEscalationList").html("");
            $("#divPOLessThan25KEscalationList").html(err);
        },
        success: function (data) {
            $("#divPOLessThan25KEscalationList").html("");
            $("#divPOLessThan25KEscalationList").html(data);
        }
    });
}

function SearchPurchaseIndentForQuotationEscalation() {
    var requestData = {};
    $.ajax({
        url: "../PurchaseIndent/GetPurchaseIndentForQuotationEscalationList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divPurchaseIndentForQuotationList").html("");
            $("#divPurchaseIndentForQuotationList").html(err);
        },
        success: function (data) {
            $("#divPurchaseIndentForQuotationList").html("");
            $("#divPurchaseIndentForQuotationList").html(data);
        }
    });
}