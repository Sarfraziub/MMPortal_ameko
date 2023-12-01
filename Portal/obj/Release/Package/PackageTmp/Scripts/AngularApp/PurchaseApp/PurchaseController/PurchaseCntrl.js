angular.module('purchase').controller('purchaseController', function ($scope, $http, Notification) {

    $scope.MdPendingPoCount = function () {
        var checkValue = $scope.TotalCount;
        var req = {
            method: 'POST',
            url: '../Dashboard/GetPendingPOCountList',
            headers: {
                'Content-Type': undefined
            },

        }
        $http(req).success(function (data, status) {
            if (data) {
                if (checkValue != data) {
                    $scope.TotalCount = data;
                    Notification.success({ message: '<a href="/PO/ListApprovedPO" target="_blank">Pending MD PO Approval Count : </a>' + data, positionY: 'bottom', positionX: 'right' });

                }
            }
        }).error(function (data, status) {
            //alert(status);
        });
    };

    $scope.PoLessThan25KCount = function () {
        var checkValue = $scope.TotalPOLess25KCount;
        var req = {
            method: 'POST',
            url: '../Dashboard/GetPendingPOLessThan25KCountList',
            headers: {
                'Content-Type': undefined
            },

        }
        $http(req).success(function (data, status) {
            if (data) {
                if (checkValue != data) {
                    $scope.TotalPOLess25KCount = data;
                    Notification.success({ message: '<a href="/PO/ListPOLessThan25K" target="_blank">PO Less Than ₹25000 Count : </a>' + data, positionY: 'bottom', positionX: 'left' });

                }
            }
        }).error(function (data, status) {
            //alert(status);
        });
    };

    //setInterval(function () { $scope.MdPendingPoCount(); $scope.GetEscalationMatrixData(); $scope.PoLessThan25KCount();}, 3000);

    /*Get Escalated Items List at Dashboard*/
    $scope.GetEscalationMatrixData = function () {
        var requestData = { userId: 0, selfOrTeam: "SELF" };

        var req = {
            method: 'POST',
            url: '../MasterDashboard/GetEscalatedMatrix',
            headers: {
                'Content-Type': undefined
            },
            data: requestData,
            dataType: "html",
        }
        $http(req).success(function (data, status) {
            if (data) {
                if (data!="") {
                    $("#DivEscalationCount").html("");
                     $("#DivEscalationCount").html(data);

                }
                var ctx1 = document.getElementById("barcanvasEscalation").getContext("2d");
                window.myBar = new Chart(ctx1,
                    {
                        type: 'bar',
                        data: barChartData,
                        options:
                            {
                                title:
                                {
                                    display: true,
                                    text: ""
                                },
                                responsive: true,
                                maintainAspectRatio: true
                            }
                    });
            }
        }).error(function (data, status) {
            $("#DivEscalationCount").html("");
            $("#DivEscalationCount").html(err);
        });
    }
});
