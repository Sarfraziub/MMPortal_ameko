$(document).ready(function () {
   // BindUser();
    GetAdminDashboardUsersList();
    GetAdminDashboardRolesList();
});

function BindUser() {

    var txtUserName = $("#txtUserName");
    var txtPhoneNo = $("#txtPhoneNo");
    var txtFullName = $("#txtFullName");
    var txtEmail = $("#txtEmail");
    var ddlRole = $("#ddlRole");

    var requestData = { };
    $.ajax({
        url: "../User/GetDashboardUser",
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

//function drawCharts() {

function GetAdminDashboardUsersList() {

    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetAdminDashboardUsersList",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivUsersList").html("");
            $("#DivUsersList").html(err);

        },
        success: function (data) {
            $("#DivUsersList").html("");
            $("#DivUsersList").html(data);

        }
    });
}

function GetAdminDashboardRolesList() {

    var requestData = {};
    $.ajax({
        url: "../Dashboard/GetAdminDashboardRolesList",
        data: requestData,
        dataType: "html",
        type: "POST",
        asnc: false,
        error: function (err) {
            $("#DivRolesList").html("");
            $("#DivRolesList").html(err);

        },
        success: function (data) {
            $("#DivRolesList").html("");
            $("#DivRolesList").html(data);

        }
    });
}