
$(document).ready(function () {
    SearchEmployeeAppraisalTemplateMapping();
});

function ClearFields() {
    $("#txtTemplateName").val("");
    $("#txtEmployeeName").val("");

 
    
}
function SearchEmployeeAppraisalTemplateMapping() {
    var txtTemplateName = $("#txtTemplateName");
    var txtEmployeeName = $("#txtEmployeeName");
  
    
    

    var requestData = {
        templateName: txtTemplateName.val().trim(),
        employeeName: txtEmployeeName.val().trim(),
        employeeMapping_Status: "1"
    };
    $.ajax({
        url: "../PMS_Assessment/GetFinalAssessmentList",
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
