$(document).ready(function () {
    var hdnCurrentFinyearId = $("#hdnCurrentFinyearId");
    BindFinYearList(hdnCurrentFinyearId.val());
    $("#ddlFinYear").val(hdnCurrentFinyearId);
    GetBookBalanceList();
    GetMonthWiseBankCashTransactionList();
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
            GetBookBalanceList();
            GetMonthWiseBankCashTransactionList();
        },
        error: function (Result) {
        
        }
    });

    
}

function GetBookBalanceList() {
    
    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetBookBalanceList",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc:false,
        error: function (err) {
            $("#DivBookBalance").html("");
            $("#DivBookBalance").html(err);

        },
        success: function (data) {
            $("#DivBookBalance").html("");
            $("#DivBookBalance").html(data);
            
            var ctx1 = document.getElementById("barcanvas").getContext("2d");
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
    });
}

function GetMonthWiseBankCashTransactionList() {

    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetMonthWiseBankCashTransactionSummary",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivMonthWiseBankCashTransaction").html("");
            $("#DivMonthWiseBankCashTransaction").html(err);

        },
        success: function (data) {
            $("#DivMonthWiseBankCashTransaction").html("");
            $("#DivMonthWiseBankCashTransaction").html(data);

            var ctx1 = document.getElementById("MonthWiseBankCashTrnBarCanvas").getContext("2d");
            window.myBar = new Chart(ctx1,
                {
                    type: 'bar',
                    data: monthWiseBankCashTrnChartData,
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
    });
}