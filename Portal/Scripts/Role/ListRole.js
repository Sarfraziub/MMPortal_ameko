
$(document).ready(function () {
    //$('#tblCompanyList').paging({ limit: 2 });
    SearchRole(); 
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
 
function ClearFields()
{
    $("#txtRoleName").val("");
    $("#txtRoleDescription").val("");
    $("#ddlIsAdmin").val("");
    $("#ddlStatus").val("");
}
 
function SearchRole() { 
    var txtRoleName = $("#txtRoleName"); 
    var txtRoleDescription = $("#txtRoleDescription");
    var ddlIsAdmin = $("#ddlIsAdmin");
    var ddlStatus = $("#ddlStatus");
    var requestData = { 
        RoleName: txtRoleName.val().trim(),
        RoleDesc: txtRoleDescription.val().trim(),
        IsAdmin: ddlIsAdmin.val(),
        Status: ddlStatus.val()
    };
   
    $.ajax({
        url: "../Role/GetRoleList",
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
