﻿$(document).ready(function () {
    //$('#tblCompanyList').paging({ limit: 2 });
   // SearchDepartment();
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

function ClearFields() {
    $("#txtDepartmentName").val("");
    $("#txtDepartmentCode").val("");
    $("#ddlStatus").val("");
}

function SearchDepartment() {
    var txtDepartmentName = $("#txtDepartmentName");
    var txtDepartmentCode = $("#txtDepartmentCode");
    var ddlStatus = $("#ddlStatus");
    var requestData = {
        DepartmentName: txtDepartmentName.val().trim(),
        DepartmentCode: txtDepartmentCode.val().trim(),
        Status: ddlStatus.val()
    }; 
    $.ajax({
        url: "../Department/GetDepartmentList",
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
