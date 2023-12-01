$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });
    var hdnCurrentFinyearId = $("#hdnCurrentFinyearId");
    BindFinYearList(hdnCurrentFinyearId.val());
    $("#ddlFinYear").val(hdnCurrentFinyearId);

    GetDashboardPendingStoreRequisition();
    GetDashboardRejectedStoreRequisition();
});

function BindFinYearList(selectedFinYear) {
    $.ajax({
        type: "GET",
        url: "../Product/GetFinYearList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $.each(data, function (i, item) {
                $("#ddlFinYear").append($("<option></option>").val(item.FinYearId).html(item.FinYearDesc));
            });
            $("#ddlFinYear").val(selectedFinYear);
        },
        error: function (Result) {
            
        }
    });
}

function SetFinancialYearSession()
{
    var finYearId = $("#ddlFinYear option:selected").val();
    var data = { finYearId: finYearId };
    $.ajax({
        type: "POST",
        url: "../Dashboard/SetFinancialYear",
        data: data,
        asnc: false,
        success: function (data) {
            GetReorderPointProductCountList();
        },
        error: function (Result) {
        
        }
    });
}

function GetReorderPointProductCountList() {

    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetReorderPointProductCountList",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivReorderProductCount").html("");
            $("#DivReorderProductCount").html(err);

        },
        success: function (data) {
            $("#DivReorderProductCount").html("");
            $("#DivReorderProductCount").html(data);

        }
    });
}
function GetInOutProductQuantityCountList() {

    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetInOutProductQuantityCountList",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivInOutProductQuantityCount").html("");
            $("#DivInOutProductQuantityCount").html(err);

        },
        success: function (data) {
            $("#DivInOutProductQuantityCount").html("");
            $("#DivInOutProductQuantityCount").html(data);

        }
    });
}

function GetSINProductQuantityCountList() {

    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetSINProductQuantityCountList",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivSINQuantityCount").html("");
            $("#DivSINQuantityCount").html(err);

        },
        success: function (data) {
            $("#DivSINQuantityCount").html("");
            $("#DivSINQuantityCount").html(data);

        }
    });
}

function GetDashboardPendingStoreRequisition() {

    var requestData = {};
    $.ajax({
        url: "../SupervisorDashboard/GetSupervisorDashboardPendingStoreRequisition",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivPendingStoreRequisitionList").html("");
            $("#DivPendingStoreRequisitionList").html(err);

        },
        success: function (data) {
            $("#DivPendingStoreRequisitionList").html("");
            $("#DivPendingStoreRequisitionList").html(data);

        }
    });
}

function GetDashboardRecommendationPOList() {

    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetDashboardRecommendationPOList",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivRecommendationPOList").html("");
            $("#DivRecommendationPOList").html(err);

        },
        success: function (data) {
            $("#DivRecommendationPOList").html("");
            $("#DivRecommendationPOList").html(data);

        }
    });
}

function GetDashboardRejectedStoreRequisition() {

    var requestData = {};
    $.ajax({
        url: "../SupervisorDashboard/GetSupervisorDashboardRejectedStoreRequisition",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivRejectedStoreRequisitionList").html("");
            $("#DivRejectedStoreRequisitionList").html(err);

        },
        success: function (data) {
            $("#DivRejectedStoreRequisitionList").html("");
            $("#DivRejectedStoreRequisitionList").html(data);

        }
    });
}