$(document).ready(function () {
    $("#ddlDepartment").attr('disabled', true);
    $("#txtRoasterStartDate").attr('readOnly', true);
    $("#txtRoasterEndDate").attr('readOnly', true);
    BindDepartmentList();
    BindShiftList();
    BindRosterList();
    var hdnAccessMode = $("#hdnAccessMode"); 
    var hdnEmployeeRoasterId = $("#hdnEmployeeRoasterId");
    if (hdnEmployeeRoasterId.val() != "" && hdnEmployeeRoasterId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0")
    {
        //setTimeout(
        //function () {
        //   // GetRoasterDetail(hdnRoasterID.val());
        //}, 1000);

     
        if (hdnAccessMode.val() == "3")
        {
            
            $("#btnUpdate").show();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true); 
            $("#chkstatus").attr('disabled', true);
        }
        else
        {
            $("#btnUpdate").show();
            $("#btnReset").show();
        }
    }
    else
    {
        
        $("#btnUpdate").show();
        $("#btnReset").show();
    }
    $("#ddlRoster").focus();
    $("#txtRoasterStartDate,#txtRoasterEndDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        minDate: '0D',
        onSelect: function (selected) {
        }
    });
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
function BindDepartmentList() {
    $.ajax({
        type: "GET",
        url: "../Employee/GetDepartmentList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlDepartment").append($("<option></option>").val(0).html("-Select Department-"));
            $.each(data, function (i, item) {

                $("#ddlDepartment").append($("<option></option>").val(item.DepartmentId).html(item.DepartmentName));
            });
        },
        error: function (Result) {
            $("#ddlDepartment").append($("<option></option>").val(0).html("-Select Department-"));
        }
    });
}
function BindShiftList() {
    $.ajax({
        type: "GET",
        url: "../Roaster/GetShiftList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlShift").append($("<option></option>").val(0).html("-Select Shift-"));
            $.each(data, function (i, item) {

                $("#ddlShift").append($("<option></option>").val(item.ShiftId).html(item.ShiftName));
            });
        },
        error: function (Result) {
            $("#ddlShift").append($("<option></option>").val(0).html("-Select Shift-"));
        }
    });
}
function BindRosterList() {
    $.ajax({
        type: "GET",
        url: "../Roaster/GetRosterList",
        data: "{}",
        dataType: "json",
        asnc: false,
        success: function (data) {
            $("#ddlRoster").append($("<option></option>").val(0).html("-Select Roster-"));
            $.each(data, function (i, item) {

                $("#ddlRoster").append($("<option></option>").val(item.RoasterId).html(item.RoasterName));
            });
        },
        error: function (Result) {
            $("#ddlRoster").append($("<option></option>").val(0).html("-Select Roster-"));
        }
    });
}
function GetRoasterDetail() {
    var roasterId = $("#ddlRoster option:selected").val();
    $.ajax({
        type: "GET",
        asnc:false,
        url: "../Roaster/GetRoasterDetail",
        data: { roasterId: roasterId },
        dataType: "json",
        success: function (data) {
            $("#ddlDepartment").val(data.DepartmentId);
            $("#txtRoasterStartDate").val(data.RoasterStartDate);
            $("#txtRoasterEndDate").val(data.RoasterEndDate);
            GetRosterLinkedEmployeeList(roasterId);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    }); 
}
function GetRosterLinkedEmployeeList(rosterId) {
    
    var requestData = {
        roasterId: rosterId
    };
    $.ajax({
        url: "../Roaster/GetRosterLinkedEmployeeList",
        data: requestData,
        dataType: "html",
        type: "GET",
        error: function (err) {
            $("#divEmployeeList").html("");
            $("#divEmployeeList").html(err);
        },
        success: function (data) {
            $("#divEmployeeList").html("");
            $("#divEmployeeList").html(data);
        }
    });
}

function SaveData()
{
    var ddlRoster = $("#ddlRoster");
    var ddlDepartment = $("#ddlDepartment");
    var txtRoasterStartDate = $("#txtRoasterStartDate");
    var txtRoasterEndDate = $("#txtRoasterEndDate");
    var ddlShift = $("#ddlShift");
    
    //var chkstatus = $("#chkstatus").is(':checked') ? true : false;
    if (ddlRoster.val() == "" || ddlRoster.val() == "0")
    {
        ShowModel("Alert", "Please select Roster")
        return false;
    }
    if (ddlDepartment.val() == "" || ddlDepartment.val() == "0") {
        ShowModel("Alert", "Please select Department")
        return false;
    }
    if (txtRoasterStartDate.val().trim() == "") {
        ShowModel("Alert", "Please select Shift Start Date")
        txtRoasterStartDate.focus();
        return false;
    }
    if (txtRoasterEndDate.val().trim() == "") {
        ShowModel("Alert", "Please select Shift End Date")
        txtRoasterEndDate.focus();
        return false;
    }
    if (ddlShift.val() == "" || ddlShift.val() == "0") {
        ShowModel("Alert", "Please select Shift")
        return false;
    }
    
    var mappingStatus = false;
    var employeeRosterList = [];
    //$('#tblRoleList > tr').each(function (row) {
    $('.mapping-list tr').each(function (i, row) {
        var $row = $(row);
        var employeeId = $row.find("#hdnEmployeeId").val();
        var chkSelect = $row.find("#chkSelect").is(':checked') ? true : false;
        
        if (employeeId != undefined && chkSelect==true ) {
            var employeeRoster = {
                EmployeeRoasterId:0,
                EmployeeId: employeeId
            };
            employeeRosterList.push(employeeRoster);
            mappingStatus = true;
        }
    });

    if (mappingStatus == false) {
        ShowModel("Alert", "Please select atleast one Employee")
        return false;
    }

    

    var requestData = {rosterId:ddlRoster.val(), shiftId:ddlShift.val(), startDate: txtRoasterStartDate.val(), endDate: txtRoasterEndDate.val(), employeeRosters: employeeRosterList };
    $.ajax({
        url: "../Roaster/UpdateDateWiseShift",
        cache: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify( requestData),
        contentType: 'application/json',
        success: function (data) {
            if (data.status=="SUCCESS")
            {
                ShowModel("Alert", data.message);
                ClearFields();
                $("#btnUpdate").show();
            }
            else
            {
                ShowModel("Error", data.message)
            }
        },
        error: function (err) {
            ShowModel("Error", err)
        }
    });

}
function ShowModel(headerText,bodyText)
{
    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);

}
function ClearFields()
{ 
    $("#ddlRoster").val("0");
    $("#hdnEmployeeRoasterId").val("0");
    $("#ddlDepartment").val("0");
    $("#txtRoasterStartDate").val("");
    $("#txtRoasterEndDate").val("");
    $("#ddlShift").val("0");
    $("#divEmployeeList").html("");
}
function GetRoasterWeekList(weeks) {
    var hdnRoasterID = $("#hdnRoasterID");
    var requestData = { weeks: weeks, roasterId: hdnRoasterID.val() };
    $.ajax({
        url: "../Roaster/GetRoasterWeekList",
        cache: false,
        data: JSON.stringify(requestData),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        error: function (err) {
            $("#divWeekList").html("");
            $("#divWeekList").html(err);
        },
        success: function (data) {
            $("#divWeekList").html("");
            $("#divWeekList").html(data);

            ShowHideWeekPanel(2);
        }
    });
}
function CheckSelectAll(obj) {
    $('.selectEmployee').prop('checked', obj.checked);

}