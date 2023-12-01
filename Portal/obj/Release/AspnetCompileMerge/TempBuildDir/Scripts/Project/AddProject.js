
$(document).ready(function () {

    //for Customer Auto  Complete 

    $("#txtCustomerName").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "../Quotation/GetCustomerAutoCompleteList",
                type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.CustomerName, value: item.CustomerId, primaryAddress: item.PrimaryAddress, code: item.CustomerCode, gSTNo: item.GSTNo };
                    }))
                }
            })
        },
        focus: function (event, ui) {
            $("#txtCustomerName").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#txtCustomerName").val(ui.item.label);
            $("#hdnCustomerId").val(ui.item.value);
            GetCustomerBranchListByID(ui.item.value,"");
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#txtCustomerName").val("");
                $("#hdnCustomerId").val("0");
             //   $("#txtCustomerCode").val("");
                ShowModel("Alert", "Please select Customer from List")

            }
            return false;
        }

    })
.autocomplete("instance")._renderItem = function (ul, item) {
    return $("<li>")
      .append("<div><b>" + item.label + " || " + item.code + "</b><br>" + item.primaryAddress + "</div>")
      .appendTo(ul);
};

    // Get CustomerBranch List from CustomerId 

    var hdnAccessMode = $("#hdnAccessMode");
    var hdnProjectID = $("#hdnProjectID");
    if (hdnProjectID.val() != "" && hdnProjectID.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        GetProjectDetail(hdnProjectID.val());

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
    $("#txtProductTypeName").focus();



});



function GetCustomerBranchListByID(customerId,customerBranchId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Project/GetCustomerBranchListByID",
        data: { customerId: customerId },
        dataType: "json",
        success: function (data) {

            $("#ddlCustomerType").append($("<option></option>").val(0).html("-Select Customer Branch-"));
            $.each(data, function (i, item) {

                $("#ddlCustomerBranch").append($("<option></option>").val(item.CustomerBranchId).html(item.BranchName));
            });
            if (customerBranchId != null && customerBranchId != "") {
                $("#ddlCustomerBranch").val(customerBranchId);
            }
        },
        error: function (Result) {
            $("#ddlCustomerType").append($("<option></option>").val(0).html("-Select Customer Branch-"));
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


function GetProjectDetail(projectId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Project/GetProjectDetail",
        data: { projectId: projectId },
        dataType: "json",
        success: function (data) {
            $("#txtProjectName").val(data.ProjectName);;
            $("#hdnProjectID").val(data.ProjectId);;
            $("#txtProjectCode").val(data.ProjectCode);;
           
            $("#hdnCustomerId").val(data.CustomerId);;
            $("#txtCustomerName").val(data.CustomerName);          
            GetCustomerBranchListByID(data.CustomerId, data.CustomerBranchId);
            $("#ddlCustomerBranch").val("0");
            $("#ddlCustomerBranch").val(data.CustomerBranchId);
            if (data.ProjectStatus == 'True') {
                $("#chkstatus").attr("checked", true);
            }
            else {
                $("#chkstatus").attr("checked", false);
            }
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });

}

function SaveData() {
    var txtProjectName = $("#txtProjectName");
    var hdnProjectID = $("#hdnProjectID");
    var txtProjectCode = $("#txtProjectCode");
    var chkstatus = $("#chkstatus").is(':checked') ? true : false;
    var hdnCustomerId = $("#hdnCustomerId");
    var txtCustomerName = $("#txtCustomerName");
    var ddlCustomerBranch = $("#ddlCustomerBranch");
    if (txtProjectName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Project Name")
        txtProjectName.focus();
        return false;
    }
    if (txtProjectCode.val().trim() == "") {
        ShowModel("Alert", "Please Enter Project Code")
        txtProjectCode.focus();
        return false;
    }
    if (txtCustomerName.val().trim() == "") {
        ShowModel("Alert", "Please Enter Customer Name")
        txtCustomerName.focus();
        return false;
    }
   
    //if (ddlCustomerBranch.val() == "" || ddlCustomerBranch.val() == "0") {
    //    ShowModel("Alert", "Please select Customer Branch")
    //    ddlCustomerBranch.focus();
    //    return false;
    //}

    var projectViewModel = {
        ProjectId: hdnProjectID.val(),
        ProjectName: txtProjectName.val().trim(),
        ProjectCode: txtProjectCode.val().trim(),
        CustomerId: hdnCustomerId.val().trim(),
        CustomerBranchId: ddlCustomerBranch.val().trim(),
        ProjectStatus: chkstatus

    };
    var requestData = { projectViewModel: projectViewModel };
    $.ajax({
        url: "../Project/AddEditProject",
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
                    window.location.href = "../Project/AddEditProject";
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
    $("#txtProjectName").val("");
    $("#txtProjectCode").val("");
    $("#txtCustomerName").val("");
    $("#ddlCustomerBranch").val("0");
    $("#chkstatus").prop("checked", true);  
}
function stopRKey(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}
document.onkeypress = stopRKey;