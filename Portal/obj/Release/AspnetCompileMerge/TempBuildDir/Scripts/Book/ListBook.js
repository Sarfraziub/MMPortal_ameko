$(document).ready(function () { 
    SearchBook();
    //$('#tblCompanyList').paging({ limit: 2 });   
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
    $("#hdnBookId").val("0");
    $("#txtBookName").val("");
    $("#txtBookCode").val("");
    $("#ddlBookType").val("0");
    $("#ddlStatus").val("");
}
 
 
function SearchBook() {
    var txtBookName = $("#txtBookName");
    var txtBookCode = $("#txtBookCode");
    var ddlBookType = $("#ddlBookType");
 
    var ddlStatus = $("#ddlStatus");
    var requestData = { 
        bookName: txtBookName.val().trim(),
        bookCode: txtBookCode.val().trim(),
        bookType: ddlBookType.val().trim(),
        status: ddlStatus.val()
    }; 
    $.ajax({
        url: "../Book/GetBookList",
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
function Export() {
    var divList = $("#divList");
    ExporttoExcel(divList);

}