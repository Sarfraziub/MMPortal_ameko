$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });
    var hdnCurrentFinyearId = $("#hdnCurrentFinyearId");
    BindFinYearList(hdnCurrentFinyearId.val());
    $("#ddlFinYear").val(hdnCurrentFinyearId);
    GetDashboardPendingStoreRequisition();
    GetDashboardRecommendationPOList();
    GetDashboardRejectedStoreRequisition();
    GetTodayPOSumAmount();
    GetTodayPISumAmount();
    GetPOCountList();
    GetPICountList();
    GetPOPendingForMDApprovals();
    GetPORejectedByMD();
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
            GetPOCountList();
            GetPICountList();
        },
        error: function (Result) {
        
        }
    });
}

function GetPOCountList() {

    var requestData = { userId: 0, selfOrTeam: "SELF" };
    $.ajax({
        url: "../Dashboard/GetPOCountList",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivPOCount").html("");
            $("#DivPOCount").html(err);

        },
        success: function (data) {
            $("#DivPOCount").html("");
            $("#DivPOCount").html(data);

            //var ctx1 = document.getElementById("barcanvasPO").getContext("2d");
            //window.myBar = new Chart(ctx1,
            //    {
            //        type: 'bar',
            //        data: barChartData,
            //        options:
            //            {
            //                title:
            //                {
            //                    display: true,
            //                    text: ""
            //                },
            //                responsive: true,
            //                maintainAspectRatio: true
            //            }
            //    });


        }
    });
}
function GetPICountList() {

    var requestData = { userId: 0, selfOrTeam: "SELF" };
    $.ajax({
        url: "../Dashboard/GetPICountList",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivPIICount").html("");
            $("#DivPIICount").html(err);

        },
        success: function (data) {
            $("#DivPIICount").html("");
            $("#DivPIICount").html(data);

            //var ctx1 = document.getElementById("barcanvasPII").getContext("2d");
            //window.myBar = new Chart(ctx1,
            //    {
            //        type: 'bar',
            //        data: barChartData,
            //        options:
            //            {
            //                title:
            //                {
            //                    display: true,
            //                    text: ""
            //                },
            //                responsive: true,
            //                maintainAspectRatio: true
            //            }
            //    });


        }
    });
}

function GetTodayPOSumAmount() {
    var requestData = { userId: 0, selfOrTeam: "SELF" };
    $.ajax({
        url: "../Dashboard/GetTodayPOSumAmount",
        data: requestData,
        dataType: "json",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#lblTodayPOSum").html("");
            $("#lblTodayPOSum").html(err);

        },
        success: function (data) {
            $("#lblTodayPOSum").html("");
            $("#lblTodayPOSum").html(data.TodayPOSumAmount);
        }
    });
}

function GetTodayPISumAmount() {
    var requestData = { userId: 0, selfOrTeam: "SELF" };
    $.ajax({
        url: "../Dashboard/GetTodayPISumAmount",
        data: requestData,
        dataType: "json",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#lblTodayPISum").html("");
            $("#lblTodayPISum").html(err);

        },
        success: function (data) {
            $("#lblTodayPISum").html("");
            $("#lblTodayPISum").html(data.TodayPISumAmount);
        }
    });
}

function GetDashboardPendingStoreRequisition() {

    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetDashboardPendingStoreRequisition",
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
        url: "../Dashboard/GetDashboardRejectedStoreRequisition",
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

function GetPOPendingForMDApprovals() {

    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetPOPendingForMDApprovals",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivPOPendigMDApproval").html("");
            $("#DivPOPendigMDApproval").html(err);

        },
        success: function (data) {
            $("#DivPOPendigMDApproval").html("");
            $("#DivPOPendigMDApproval").html(data);

        }
    });
}

function GetPORejectedByMD() {

    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetPORejectedByMD",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivPORejectedByMD").html("");
            $("#DivPORejectedByMD").html(err);

        },
        success: function (data) {
            $("#DivPORejectedByMD").html("");
            $("#DivPORejectedByMD").html(data);

        }
    });
}

// Angular Section Start from here

//angular.module('purchase', ['ui-notification']).config(function (NotificationProvider) {
//    NotificationProvider.setOptions({
//        delay: 10000,
        
//        positionX: 'left',
//        positionY: 'bottom'
//    });
//});

//angular.module('purchase').controller('purchaseController', function ($scope, $http, Notification) {
    
//    $scope.MdPendingPoCount = function () {
//            var checkValue = $scope.TotalCount;
//            var req = {
//                method: 'POST',
//                url: '../Dashboard/GetPendingPOCountList',
//                headers: {
//                    'Content-Type': undefined
//                },
                
//            }
//            $http(req).success(function (data, status) {
//                if (data) {
//                    if (checkValue != data){
//                    $scope.TotalCount = data;
//                    Notification.success({ message: '<a href="/PO/ListApprovedPO" target="_blank">Pending MD PO Approval Count : </a>' + data, positionY: 'bottom', positionX: 'right' });
                   
//                  }
//                }
//            }).error(function (data, status) {
//                alert(status);
//            });
//        };

//        setInterval(function () { $scope.anyFunc(); }, 3000);
//    });
