﻿$(document).ready(function () {
    $("#txtTemplateName").attr('readOnly', true);
    $("#txtDepartment").attr('readOnly', true);
    $("#txtDesignation").attr('readOnly', true);
    $("#txtGoalStartDate,#txtNewGoalStartDate").attr('readOnly', true);
    $("#txtGoalDueDate,#txtNewGoalDueDate").attr('readOnly', true);
   

    $("#txtGoalStartDate,#txtNewGoalStartDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
       
        onSelect: function (selected) {

        }
    });
    $("#txtGoalDueDate,#txtNewGoalDueDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        yearRange: '-10:+100',
        minDate: '0',
        onSelect: function (selected) {

        }
    });

    $("#txtEmployee").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Employee/GetEmployeeAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: {
                    term: request.term, departmentId: $("#hdnDepartmentId").val(), designationId: $("#hdnDesignationId").val()
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.FirstName, value: item.EmployeeId, EmployeeCode: item.EmployeeCode, MobileNo: item.MobileNo
                        };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtEmployee").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtEmployee").val(ui.item.label);
            $("#hdnEmployeeId").val(ui.item.value);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtEmployee").val("");
                $("#hdnEmployeeId").val("0");
                ShowModel("Alert", "Please select Employee from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + " || " + item.EmployeeCode + "</b><br>" + item.MobileNo + "</div>")
      .appendTo(ul);
};

    $("#txtGoalName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../PMS_EmployeeAppraisalTemplateMapping/GetGoalAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.GoalName, value: item.GoalId, goalDescription: item.GoalDescription, sectionId: item.SectionId, weight: item.Weight, goalCategoryId: item.GoalCategoryId, performanceCycleId: item.PerformanceCycleId, finYearId: item.FinYearId, startDate: item.StartDate, dueDate: item.DueDate, weight: item.Weight, goalStatus: item.GoalStatus };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtGoalName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtGoalName").val(ui.item.label);
            $("#hdnGoalId").val(ui.item.value); 
            $("#txtGoalDescription").val(ui.item.goalDescription);
            $("#ddlSectionName").val(ui.item.sectionId);
            $("#ddlGoalCategoryName").val(ui.item.goalCategoryId);
            $("#txtGoalDueDate").val(ui.item.dueDate);
            $("#txtWeight").val(ui.item.weight);
            $("#txtGoalStartDate").val(ui.item.startDate);
           
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtGoalName").val("");
                $("#hdnGoalId").val("0");
                $("#txtGoalDescription").val("");
                $("#ddlSectionName").val("0");
                $("#ddlGoalCategoryName").val("0");

                $("#txtGoalDueDate").val("");
                $("#txtWeight").val("");
                $("#txtGoalStartDate").val("");
                ShowModel("Alert", "Please Select Goal from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.primaryAddress + "</div>")
      .appendTo(ul);
};


    BindFinYearList();
    BindPerformanceCycleList();
    BindDepartmentList();

    BindPMSGoalCategoryList();
    BindPMSSectionList();

    var hdnAccessMode = $("#hdnAccessMode");
    var hdnEmpAppraisalTemplateMappingId = $("#hdnEmpAppraisalTemplateMappingId");
    if (hdnEmpAppraisalTemplateMappingId.val() != "" && hdnEmpAppraisalTemplateMappingId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetEmployeeAppraisalTemplateMappingDetail(hdnEmpAppraisalTemplateMappingId.val());
       }, 2000);
         

        if (hdnAccessMode.val() == "3") {
           $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
           $("#chkstatus").attr('disabled', true);
        }
        else {
            $("#btnSave").hide();
            $("#btnUpdate").show();
            $("#btnReset").show();
        }
    }
    else {
        $("#btnSave").show();
        $("#btnUpdate").hide();
        $("#btnReset").show();
    }


    var goalList = [];
    GetEmployeeGoalList(goalList);
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
function checkDec(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = el.value.substring(0, el.value.length - 1);
    }

}
function BindDepartmentList() {
    $.ajax({
        type: "GET",
        url: "../Employee/GetDepartmentList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlDepartmentSearch").append($("<option></option>").val(0).html("-Select Department-"));
            $.each(data, function (i, item) {

                $("#ddlDepartmentSearch").append($("<option></option>").val(item.DepartmentId).html(item.DepartmentName));
            });
        },
        error: function (Result) {
            $("#ddlDepartmentSearch").append($("<option></option>").val(0).html("-Select Department-"));
        }
    });
}
function BindDesignationList(designationId) {
    var departmentId = $("#ddlDepartmentSearch option:selected").val();
    $("#ddlDesignationSearch").val(0);
    $("#ddlDesignationSearch").html("");
    if (departmentId != undefined && departmentId != "" && departmentId != "0") {
        var data = { departmentId: departmentId };
        $.ajax({
            type: "GET",
            url: "../Employee/GetDesignationList",
            data: data,
            asnc: false,
            dataType: "json",
            success: function (data) {
                $("#ddlDesignationSearch").append($("<option></option>").val(0).html("-Select Designation-"));
                $.each(data, function (i, item) {
                    $("#ddlDesignationSearch").append($("<option></option>").val(item.DesignationId).html(item.DesignationName));
                });
                $("#ddlDesignationSearch").val(designationId);
            },
            error: function (Result) {
                $("#ddlDesignationSearch").append($("<option></option>").val(0).html("-Select Designation-"));
            }
        });
    }
    else {

        $("#ddlDesignationSearch").append($("<option></option>").val(0).html("-Select Designation-"));
    }
}
function BindFinYearList() {
    $.ajax({
        type: "GET",
        url: "../PMS_EmployeeAppraisalTemplateMapping/GetFinancialYearForEmployeeAppraisalTemplateList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlFinYear").append($("<option></option>").val(0).html("Select Financial Year"));
            $.each(data, function (i, item) {
                $("#ddlFinYear").append($("<option></option>").val(item.FinYearId).html(item.FinYearDesc));
            });
        },
        error: function (Result) {
            $("#ddlFinYear").append($("<option></option>").val(0).html("Select Financial Year"));
        }
    });
}
function BindPMSGoalCategoryList() {
    $.ajax({
        type: "GET",
        url: "../PMSGoal/GetPMSGoalCategoryList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlGoalCategoryName,#ddlNewGoalCategoryName").append($("<option></option>").val(0).html("Select Goal Category"));
            $.each(data, function (i, item) {
                $("#ddlGoalCategoryName,#ddlNewGoalCategoryName").append($("<option></option>").val(item.GoalCategoryId).html(item.GoalCategoryName));
            });
        },
        error: function (Result) {
            $("#ddlGoalCategoryName,#ddlNewGoalCategoryName").append($("<option></option>").val(0).html("Select Goal Category"));
        }
    });
}
function BindPMSSectionList() {
    $.ajax({
        type: "GET",
        url: "../PMSGoal/GetPMSSectionList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlSectionName,#ddlNewSectionName").append($("<option></option>").val(0).html("Select Section"));
            $.each(data, function (i, item) {
                $("#ddlSectionName,#ddlNewSectionName").append($("<option></option>").val(item.SectionId).html(item.SectionName));
            });
        },
        error: function (Result) {
            $("#ddlSectionName,#ddlNewSectionName").append($("<option></option>").val(0).html("Select Section"));
        }
    });
}
function BindPerformanceCycleList() {
    $.ajax({
        type: "GET",
        url: "../PMSGoal/GetPMSPerformanceCycleList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlPerformanceCycle,#ddlPerformanceName").append($("<option></option>").val(0).html("Select Performance Cycle"));
            $.each(data, function (i, item) {
                $("#ddlPerformanceCycle,#ddlPerformanceName").append($("<option></option>").val(item.PerformanceCycleId).html(item.PerformanceCycleName));
            });
        },
        error: function (Result) {
            $("#ddlPerformanceCycle,#ddlPerformanceName").append($("<option></option>").val(0).html("Select Performance Cycle"));
        }
    });
}
function OpenApSearchPopup() {
    $("#SearchAppraisalTemplateModel").modal();

}
function SearchTemplate() {
    var txtTemplateNameSearch = $("#txtTemplateNameSearch");
    var ddlDepartment = $("#ddlDepartmentSearch");
    var ddlDesignation = $("#ddlDesignationSearch"); 
 
    var requestData = {
        templateName: txtTemplateNameSearch.val().trim(),
        department: ddlDepartment.val(),
        designation: ddlDesignation.val()
       };
    $.ajax({
        url: "../PMS_EmployeeAppraisalTemplateMapping/GetAppraisalTemplateDetailList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divTemplateList").html("");
            $("#divTemplateList").html(err);
        },
        success: function (data) {
            $("#divTemplateList").html("");
            $("#divTemplateList").html(data);
        }
    });
}
function SelectTemplate(templateId, templateName, departmentName, departmentId, designationName, designationId) {
    $("#hdnTemplateId").val(templateId);
    $("#txtTemplateName").val(templateName);
    $("#txtDepartment").val(departmentName);
    $("#hdnDepartmentId").val(departmentId);
    $("#txtDesignation").val(designationName);
    $("#hdnDesignationId").val(designationId);
    $("#SearchAppraisalTemplateModel").modal('hide');
    GetTemplateGoalDetailList(templateId);
}
function GetTemplateGoalDetailList(templateId) {
    
    var requestData = { templateId: templateId };
    $.ajax({
        url: "../PMS_EmployeeAppraisalTemplateMapping/GetAppraisalTemplateGoalDetailList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divTemplateGoalList").html("");
            $("#divTemplateGoalList").html(err);
        },
        success: function (data) {
            $("#divTemplateGoalList").html("");
            $("#divTemplateGoalList").html(data);

          
        }
    });



}
function SaveData() {
    var hdnEmpAppraisalTemplateMappingId = $("#hdnEmpAppraisalTemplateMappingId");
    var txtTemplateName = $("#txtTemplateName");
    var hdnTemplateId = $("#hdnTemplateId");
    var txtDepartment = $("#txtDepartment");
    var hdnDepartmentId = $("#hdnDepartmentId");
    var txtDesignation = $("#txtDesignation");
    var hdnDesignationId = $("#hdnDesignationId");
    var txtEmployee = $("#txtEmployee");
    var hdnEmployeeId = $("#hdnEmployeeId");
    var ddlFinYear = $("#ddlFinYear");
    var ddlPerformanceCycle = $("#ddlPerformanceCycle");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;
    
    if (txtTemplateName.val().trim() == "" || hdnTemplateId.val() == "" || hdnTemplateId.val() == "0") {
        ShowModel("Alert", "Please Select Template")
        txtTemplateName.focus();
        return false;
    }

    if (txtEmployee.val().trim() == "" || hdnEmployeeId.val() == "" || hdnEmployeeId.val() == "0") {
        ShowModel("Alert", "Please Select Employee")
        txtEmployee.focus();
        return false;
    }
    if (ddlFinYear.val() == "" || ddlFinYear.val() == "0") {
        ShowModel("Alert", "Please Select Financial Year")
        return false;
    }
    if (ddlPerformanceCycle.val() == "" || ddlPerformanceCycle.val() == "0") {
        ShowModel("Alert", "Please Select Performance Cycle")
        return false;
        }
    
    var empTemplateViewModel = {
        EmpAppraisalTemplateMappingId: hdnEmpAppraisalTemplateMappingId.val(),
        EmployeeId: hdnEmployeeId.val(),
        TemplateId: hdnTemplateId.val(),
        PerformanceCycleId: ddlPerformanceCycle.val(),
        FinYearId: ddlFinYear.val(),
        EmpAppraisalTemplateMapping_Status: chkstatus
    }; 
     

    var goalList = [];
    $('#tblEmployeeGoal tr').each(function (i, row) {
        var $row = $(row);
        var goalName = $row.find("#hdnGoalName").val();
        var goalId = $row.find("#hdnGoalId").val();
        var goalDescription = $row.find("#hdnGoalDescription").val();
        var sectionId = $row.find("#hdnSectionId").val();
        var goalCategoryId = $row.find("#hdnGoalCategoryId").val();
        var evalutionCriteria = $row.find("#hdnEvalutionCriteria").val();
        var startDate = $row.find("#hdnStartDate").val();
        var dueDate = $row.find("#hdnDueDate").val();
        var weight = $row.find("#hdnWeight").val();
        var fixedDynamic = $row.find("#hdnFixedDynamic").val();
        var employeeGoal_Status = $row.find("#hdnEmployeeGoal_Status").val() == "Active" ? true: false;
        if (goalId != undefined) {
            var goal = {
                        GoalId: goalId,
                        GoalName: goalName,
                        GoalDescription: goalDescription,
                        SectionId : sectionId,
                        GoalCategoryId: goalCategoryId,
                        EvalutionCriteria: evalutionCriteria,
                        StartDate: startDate,
                        DueDate: dueDate,
                        Weight: weight,
                        FixedDyanmic: fixedDynamic,
                        EmployeeGoal_Status: employeeGoal_Status
                        };
            goalList.push(goal);
        } 
    });


    var requestData = {
        employeeAppraisalTemplateMappingViewModel: empTemplateViewModel, employeeGoals: goalList
};
    $.ajax({
        url: "../PMS_EmployeeAppraisalTemplateMapping/AddEditEmployeeAppraisalTeamplateMapping",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                ShowModel("Alert", data.message);
                ClearFields();
                setTimeout(
                   function () {
                       window.location.href = "../PMS_EmployeeAppraisalTemplateMapping/AddEditEmployeeAppraisalTemplateMapping?empAppraisalTemplateMappingId=" + data.trnId + "&AccessMode=2";
                   }, 2000);

                $("#btnSave").show();
                $("#btnUpdate").hide();
            }
            else {
                ShowModel("Error", data.message)
            }
        },
        error: function (err) {
            ShowModel("Error", err)
        }
    });

} 
function ShowModel(headerText, bodyText) {
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}
function ClearFields() { 
    $("#txtTemplateName").val("");
    $("#hdnTemplateId").val("0");
    $("#hdnEmpAppraisalTemplateMappingId").val("0");
    $("#txtDepartment").val("");
    $("#hdnDepartmentId").val("0");
    $("#txtDesignation").val("");
    $("#hdnDesignationId").val("0");
    $("#txtEmployee").val("");
    $("#hdnEmployeeId").val("0");
    $("#ddlFinYear").val("0");
    $("#ddlPerformanceCycle").val("0");
    $("#chkstatus").prop("checked", true);
    $("#btnSave").show();
    $("#btnUpdate").hide();
    $("#divTemplateGoalList").html("");
    
}
function GetEmployeeGoalList(goalList) {
    var hdnEmpAppraisalTemplateMappingId = $("#hdnEmpAppraisalTemplateMappingId");
    var requestData = { goals: goalList, empAppraisalTemplateMappingId: hdnEmpAppraisalTemplateMappingId.val() };
    $.ajax({
        url: "../PMS_EmployeeAppraisalTemplateMapping/GetEmployeeGoalList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divTemplateGoalList").html("");
            $("#divTemplateGoalList").html(err);
        },
        success: function (data) {
            $("#divTemplateGoalList").html("");
            $("#divTemplateGoalList").html(data);
            ShowHideGoalPanel(2);
        }
    });
}
function AddGoal(action) {
    var goalEntrySequence = 0;
    var flag = true;
    var hdnEmployeeGoalSequenceNo = $("#hdnEmployeeGoalSequenceNo");
    var hdnEmployeeGoalId = $("#hdnEmployeeGoalId");
    var hdnGoalId = $("#hdnGoalId");
    var txtGoalName = $("#txtGoalName");
    var txtGoalDescription = $("#txtGoalDescription");
    var ddlSectionName = $("#ddlSectionName");
    var ddlGoalCategoryName = $("#ddlGoalCategoryName");
    var txtEvalutionCriteria = $("#txtEvalutionCriteria");
    var txtWeight = $("#txtWeight");
    var txtGoalStartDate = $("#txtGoalStartDate");
    var txtGoalDueDate = $("#txtGoalDueDate");
    var hdnFixedDynamic = $("#hdnFixedDynamic");
    var chkGoalStatus = $("#chkGoalStatus").is(':checked') ? true : false;

    if (txtGoalName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Goal Name")
        txtGoalName.focus();
        return false;
    }
    if (txtGoalDescription.val().trim() == "") {
        ShowModel("Alert", "Please Enter Goal Description")
        txtGoalDescription.focus();
        return false;
    }
    if (ddlSectionName.val() == "" || ddlSectionName.val() == "0") {
        ShowModel("Alert", "Please select Section")
        return false;
    }
    if (ddlGoalCategoryName.val() == "" || ddlGoalCategoryName.val() == "0") {
        ShowModel("Alert", "Please select Goal Category")
        return false;
    }
    if (txtEvalutionCriteria.val().trim() == "") {
        ShowModel("Alert", "Please Enter Goal Evalution Criteria")
        txtEvalutionCriteria.focus();
        return false;
    }
    if (txtWeight.val().trim() == "" || parseInt(txtWeight.val().trim())<= 0) {
        ShowModel("Alert", "Please enter Weightage(%)")
        return false;
    }

    if ((txtWeight.val().trim() <= 0) || (txtWeight.val().trim() > 100)) {
        ShowModel("Alert", "Please Enter the Correct Weightage(%)")
        txtWeight.focus();
        return false;
    }
    if (txtGoalStartDate.val().trim() == "" || txtGoalStartDate.val().trim() == "0") {
        ShowModel("Alert", "Please select Goal Start Date")
        return false;
    }
    if (txtGoalDueDate.val().trim() == "" || txtGoalDueDate.val().trim() == "0") {
        ShowModel("Alert", "Please select Goal Start Date")
        return false;
    }
    var eDate = new Date(txtGoalDueDate.val());
    var sDate = new Date(txtGoalStartDate.val());
    if (sDate > eDate) {
        ShowModel("Alert", "Goal Start Date Should be Less than Goal Due Date.")
        return false;
    }


    var goalList = [];
    if (action == 1 && (hdnEmployeeGoalSequenceNo.val() == "" || hdnEmployeeGoalSequenceNo.val() == "0")) {
        goalEntrySequence = 1;
    }
    $('#tblEmployeeGoal tr').each(function (i, row) {
        var $row = $(row);
        var employeeGoalSequenceNo = $row.find("#hdnEmployeeGoalSequenceNo").val();
        var employeeGoalId = $row.find("#hdnEmployeeGoalId").val();
        var goalName = $row.find("#hdnGoalName").val();
        var goalId = $row.find("#hdnGoalId").val();
        var goalDescription = $row.find("#hdnGoalDescription").val();
        var sectionName = $row.find("#hdnSectionName").val();
        var sectionId = $row.find("#hdnSectionId").val();
        var goalCategoryId = $row.find("#hdnGoalCategoryId").val();
        var goalCategoryName = $row.find("#hdnGoalCategoryName").val();
        var evalutionCriteria = $row.find("#hdnEvalutionCriteria").val();
        var startDate = $row.find("#hdnStartDate").val();
        var dueDate = $row.find("#hdnDueDate").val();
        var weight = $row.find("#hdnWeight").val();
        var fixedDynamic = $row.find("#hdnFixedDynamic").val();
        var employeeGoal_Status = $row.find("#hdnEmployeeGoal_Status").val() == "Active" ? true : false;

        if (goalId != undefined) {
            if (action == 1 || (hdnEmployeeGoalSequenceNo.val() != employeeGoalSequenceNo)) {
                if (goalName == txtGoalName.val()) {
                    ShowModel("Alert", "Goal Name already added!!!")
                    return false;
                }
                var goal = {
                    EmployeeGoal_SequenceNo: employeeGoalSequenceNo,
                    EmployeeGoalId: employeeGoalId,
                    GoalId: goalId,
                    GoalName: goalName,
                    GoalDescription: goalDescription,
                    SectionId: sectionId,
                    SectionName: sectionName,
                    GoalCategoryId: goalCategoryId,
                    GoalCategoryName: goalCategoryName,
                    EvalutionCriteria: evalutionCriteria,
                    StartDate: startDate,
                    DueDate: dueDate,
                    Weight: weight,
                    FixedDyanmic: fixedDynamic,
                    EmployeeGoal_Status: employeeGoal_Status
                };
                goalList.push(goal);
                goalEntrySequence = parseInt(goalEntrySequence) + 1;

            }
            else if (hdnEmployeeGoalId.val() == employeeGoalId && hdnEmployeeGoalSequenceNo.val() == employeeGoalSequenceNo) {
                var goal = {
                    EmployeeGoal_SequenceNo: hdnEmployeeGoalSequenceNo.val(),
                    EmployeeGoalId: hdnEmployeeGoalId.val(),
                    GoalId: hdnGoalId.val(),
                    GoalName: txtGoalName.val(),
                    GoalDescription: txtGoalDescription.val(),
                    SectionId: ddlSectionName.val(),
                    SectionName: $("#ddlSectionName option:Selected").text(),
                    GoalCategoryId: ddlGoalCategoryName.val(),
                    GoalCategoryName:  $("#ddlGoalCategoryName option:Selected").text(),
                    EvalutionCriteria: txtEvalutionCriteria.val(),
                    StartDate: txtGoalStartDate.val(),
                    DueDate: txtGoalDueDate.val(),
                    Weight: txtWeight.val(),
                    FixedDyanmic: hdnFixedDynamic.val(),
                    EmployeeGoal_Status: chkGoalStatus
                };
                goalList.push(goal);
                hdnEmployeeGoalSequenceNo.val("0");
            }
        }
    });
    if (action == 1 && (hdnEmployeeGoalSequenceNo.val() == "" || hdnEmployeeGoalSequenceNo.val() == "0")) {
        hdnEmployeeGoalSequenceNo.val(goalEntrySequence);
    }
    if (action == 1) {
        var goalAddEdit = {
            EmployeeGoal_SequenceNo: hdnEmployeeGoalSequenceNo.val(),
            EmployeeGoalId: hdnEmployeeGoalId.val(),
            GoalId: hdnGoalId.val(),
            GoalName: txtGoalName.val(),
            GoalDescription: txtGoalDescription.val(),
            SectionId: ddlSectionName.val(),
            SectionName: $("#ddlSectionName option:Selected").text(),
            GoalCategoryId: ddlGoalCategoryName.val(),
            GoalCategoryName: $("#ddlGoalCategoryName option:Selected").text(),
            EvalutionCriteria: txtEvalutionCriteria.val(),
            StartDate: txtGoalStartDate.val(),
            DueDate: txtGoalDueDate.val(),
            Weight: txtWeight.val(),
            FixedDyanmic: hdnFixedDynamic.val(),
            EmployeeGoal_Status: chkGoalStatus
        };
        goalList.push(goalAddEdit);
        hdnEmployeeGoalSequenceNo.val("0");
    }
    GetEmployeeGoalList(goalList);
    
}
function EditGoalRow(obj) {
    var row = $(obj).closest("tr");
    var employeeGoalSequenceNo = $(row).find("#hdnEmployeeGoalSequenceNo").val();
    var employeeGoalId = $(row).find("#hdnEmployeeGoalId").val();
    var goalName = $(row).find("#hdnGoalName").val();
    var goalId = $(row).find("#hdnGoalId").val();
    var goalDescription = $(row).find("#hdnGoalDescription").val();
    var sectionId = $(row).find("#hdnSectionId").val();
    var goalCategoryId = $(row).find("#hdnGoalCategoryId").val();
    var evalutionCriteria = $(row).find("#hdnEvalutionCriteria").val();
    var startDate = $(row).find("#hdnStartDate").val();
    var dueDate = $(row).find("#hdnDueDate").val();
    var weight = $(row).find("#hdnWeight").val();
    var fixedDynamic = $(row).find("#hdnFixedDynamic").val();
    var employeeGoal_Status = $(row).find("#hdnEmployeeGoal_Status").val() == "Active" ? true : false;

    $("#txtGoalName").val(goalName);
    $("#hdnGoalId").val(goalId);
    $("#hdnEmployeeGoalId").val(employeeGoalId);
    $("#hdnEmployeeGoalSequenceNo").val(employeeGoalSequenceNo);
    $("#txtGoalDescription").val(goalDescription);
    $("#ddlSectionName").val(sectionId);
    $("#ddlGoalCategoryName").val(goalCategoryId);
    $("#txtWeight").val(weight);
    $("#txtEvalutionCriteria").val(evalutionCriteria);
    $("#txtGoalStartDate").val(startDate);
    $("#txtGoalDueDate").val(dueDate);
    if (employeeGoal_Status)
    {
        $("#chkGoalStatus").prop("checked", true);
    }
    else
    {
        $("#chkGoalStatus").prop("checked", false);
    }
    $("#hdnFixedDynamic").val(fixedDynamic);
    if (fixedDynamic == "F")
    {
        $("#txtGoalName").attr("disabled",true);
        $("#ddlSectionName").attr("disabled",true)
        $("#ddlGoalCategoryName").attr("disabled", true)
    }
    else
    {
        $("#txtGoalName").attr("disabled", false);
        $("#ddlSectionName").attr("disabled", false)
        $("#ddlGoalCategoryName").attr("disabled", false)
    }

    $("#btnAddGoal").hide();
    $("#btnUpdateGoal").show();
    ShowHideGoalPanel(1);
}
function ShowHideGoalPanel(action) {
    if (action == 1) {
        $(".goalsection").show();
    }
    else {
        $(".goalsection").hide();

        $("#txtGoalName").val("");
        $("#hdnGoalId").val("0");
        $("#hdnEmployeeGoalId").val("0");
        $("#txtGoalDescription").val("");
        $("#ddlSectionName").val("0");
        $("#ddlGoalCategoryName").val("0");
        $("#txtWeight").val("");
        $("#txtEvalutionCriteria").val("");
        $("#txtGoalStartDate").val("");
        $("#txtGoalDueDate").val("");
        $("#chkGoalStatus").prop("checked", true);
        $("#hdnFixedDynamic").val("F");
        $("#txtGoalName").attr("disabled", false);
        $("#ddlSectionName").attr("disabled", false)
        $("#ddlGoalCategoryName").attr("disabled", false)
        $("#btnAddGoal").show();
        $("#btnUpdateGoal").hide();
    }
}

function GetEmployeeAppraisalTemplateMappingDetail(empAppraisalTemplateMappingId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../PMS_EmployeeAppraisalTemplateMapping/GetEmployeeAppraisalTemplateMappingDetail",
        data: { empAppraisalTemplateMappingId: empAppraisalTemplateMappingId },
        dataType: "json",
        success: function (data) {
            $("#txtTemplateName").val(data.TemplateName);
            $("#hdnTemplateId").val(data.TemplateId);
            $("#txtDepartment").val(data.DepartmentName);
            $("#hdnDepartmentId").val(data.DepartmentId);
            $("#txtDesignation").val(data.DesignationName);
            $("#hdnDesignationId").val(data.DesignationId);
            $("#txtEmployee").val(data.EmployeeName);
            $("#hdnEmployeeId").val(data.EmployeeId);
            $("#ddlFinYear").val(data.FinYearId);
            $("#ddlPerformanceCycle").val(data.PerformanceCycleId);

            
            if (data.EmpAppraisalTemplateMapping_Status == true) {
                $("#chkstatus").attr("checked", true);
            }
            else {
                $("#chkstatus").attr("checked", false);
            }
            $("#divCreated").show();
            $("#txtCreatedBy").val(data.CreatedByUserName);
            $("#txtCreatedDate").val(data.CreatedDate);
            if (data.ModifiedByUserName != "") {
                $("#divModified").show();
                $("#txtModifiedBy").val(data.ModifiedByUserName);
                $("#txtModifiedDate").val(data.ModifiedDate);
            }

            $("#btnAddNew").show();
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });



}

function OpenNewGoalPopup() {
    $("#AddNewGoalModel").modal();

}

function SaveNewGoal() {
    var txtGoalName = $("#txtNewGoalName");
    var txtGoalDescription = $("#txtNewGoalDescription");
    var ddlGoalCategoryName = $("#ddlNewGoalCategoryName");
    var ddlPerformanceName = $("#ddlPerformanceCycle");
    var ddlSectionName = $("#ddlNewSectionName");
    var txtWeight = $("#txtNewWeight");
    var txtGoalStartDate = $("#txtNewGoalStartDate");
    var txtGoalDueDate = $("#txtNewGoalDueDate");
    var chkstatus = true;


    if (txtGoalName.val().trim() == "") {
        alert("Enter Goal Name");
        return false;

    }
    if (txtGoalDescription.val().trim() == "") {
        alert("Enter Goal Description Name");
        return false;

    }
    if (ddlGoalCategoryName.val() == "" || ddlGoalCategoryName.val() == "0") {
        alert("Select Goal Category Name");
        return false;
    }

    
    if (txtWeight.val() == "") {
        alert("Enter Weight Name");
        return false;
    }
    if ((txtWeight.val().trim() <= 0) || (txtWeight.val().trim() > 100)) {
        alert("Please Enter the Correct Weightage(%)")
        return false;
    }
    if (ddlPerformanceName.val() == "" || ddlPerformanceName.val() == "0") {
        alert("Select Performance Name");
        return false;
    }
    if (ddlSectionName.val() == "" || ddlSectionName.val() == "0") {
        alert("Select Section Name");
        return false;
    }

    if (txtGoalStartDate.val() == "") {
        alert("Enter Start Date Name");
        return false;
    }
    if (txtGoalDueDate.val() == "") {
        alert("Enter Due Date Name");
        return false;
    }
    var eDate = new Date(txtGoalDueDate.val());
    var sDate = new Date(txtGoalStartDate.val());
    if (sDate > eDate) {
        alert("Goal Start Date Should be Less than Goal Due Date.")
        return false;
    }

    var goalViewModel = {
        GoalId: 0,
        GoalName: txtGoalName.val().trim(),
        GoalDescription: txtGoalDescription.val().trim(),
        SectionId: ddlSectionName.val(),
        GoalCategoryId: ddlGoalCategoryName.val(),
        PerformanceCycleId: ddlPerformanceName.val(),
        StartDate: txtGoalStartDate.val(),
        DueDate: txtGoalDueDate.val(),
        Weight: txtWeight.val(),
        GoalStatus: chkstatus
    };



    var requestData = { pmsgoalViewModel: goalViewModel };
    $.ajax({
        url: "../PMSGoal/AddEditGoal",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status == "SUCCESS") {
                

                setTimeout(
                   function () {
                       $("#txtGoalName").val($("#txtNewGoalName").val());
                       $("#hdnGoalId").val(data.trnId);
                       $("#txtGoalDescription").val($("#txtNewGoalDescription").val());
                       $("#ddlSectionName").val($("#ddlNewSectionName").val());
                       $("#ddlGoalCategoryName").val($("#ddlNewGoalCategoryName").val());
                       $("#txtWeight").val($("#txtNewWeight").val());
                       $("#txtGoalStartDate").val($("#txtNewGoalStartDate").val());
                       $("#txtGoalDueDate").val($("#txtNewGoalDueDate").val());
                   }, 1000);

                
                }
            else {
                
            }
            alert(data.message);
        },
        error: function (err) {
            alert(err);
        }
    });

}