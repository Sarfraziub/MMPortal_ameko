
$(document).ready(function () { 
   
    BindEmailTemplateList();
    SearchEmailTemplate();
    
   
});
 
function BindEmailTemplateList() {

    $.ajax({
        type: "GET",
        url: "../EmailTemplate/GetEmailTemplateTypeList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlEmailTemplateType").append($("<option></option>").val(0).html("-Select Email Template-"));
            $.each(data, function (i, item) {

                $("#ddlEmailTemplateType").append($("<option></option>").val(item.EmailTemplateTypeId).html(item.EmailTemplateName));
            });
        },
        error: function (Result) {
            $("#ddlEmailTemplateType").append($("<option></option>").val(0).html("-Select Email Template-"));
        }
    });
}
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
    $("#txtEmailTemplateName").val("");
    $("#ddlEmailTemplateType").val("0");
    $("#ddlStatus").val("");
}
function SearchEmailTemplate() {
    var txtEmailTemplateName = $("#txtEmailTemplateName");
    var ddlEmailTemplateType = $("#ddlEmailTemplateType");

    var ddlStatus = $("#ddlStatus");
    var emailID = 0;
    if (ddlEmailTemplateType.val() == null)
    {
        emailID = 0;
    }
    else
    {
        emailID = ddlEmailTemplateType.val();
    }
    var requestData = { emailTemplateSubject: txtEmailTemplateName.val().trim(), emailTemplateTypeId: emailID, status: ddlStatus.val() };
    $.ajax({
        url: "../EmailTemplate/GetEmailTemplateList",
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
