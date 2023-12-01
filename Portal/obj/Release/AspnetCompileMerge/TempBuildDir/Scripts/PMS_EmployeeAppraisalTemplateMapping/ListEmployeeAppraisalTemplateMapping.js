
$(document).ready(function () {
    SearchEmployeeAppraisalTemplateMapping();
});

function ClearFields() {
    $("#txtTemplateName").val("");
    $("#txtEmployeeName").val("");

   
    $("#ddlStatus").val("");
 
    
}
function SearchEmployeeAppraisalTemplateMapping() {
    var txtTemplateName = $("#txtTemplateName");
    var txtEmployeeName = $("#txtEmployeeName");
  
    var ddlStatus = $("#ddlStatus");
    

    var requestData = {
        templateName: txtTemplateName.val().trim(),
        employeeName: txtEmployeeName.val().trim(),
        employeeMapping_Status: ddlStatus.val()
    };
    $.ajax({
        url: "../PMS_EmployeeAppraisalTemplateMapping/GetEmployeeAppraisalTemplateMappingList",
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
