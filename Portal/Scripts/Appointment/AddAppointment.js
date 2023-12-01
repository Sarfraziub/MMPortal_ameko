$(document).ready(function () {
    $("#tabs").tabs({
        collapsible: true
    });
   
    $("#txtFirstName").attr('readOnly', true);
    $("#txtLastName").attr('readOnly', true);
    $("#txtMobileNo").attr('readOnly', true);
    $("#txtEmail").attr('readOnly', true);
    $("#txtAppointLetterNo").attr('readOnly', true);
    $("#txtInterviewNo").attr('readOnly', true);
    
    $("#txtJoiningDate").attr('readOnly', true);
    $("#txtAppointDate").attr('readOnly', true);
    $("#txtCreatedBy").attr('readOnly', true);
    $("#txtCreatedDate").attr('readOnly', true);
    $("#txtModifiedBy").attr('readOnly', true);
    $("#txtModifiedDate").attr('readOnly', true);


    
    var hdnAccessMode = $("#hdnAccessMode");
    var hdnAppointLetterId = $("#hdnAppointLetterId");
    if (hdnAppointLetterId.val() != "" && hdnAppointLetterId.val() != "0" && hdnAccessMode.val() != "" && hdnAccessMode.val() != "0") {
        setTimeout(
       function () {
           GetAppointmentDetail(hdnAppointLetterId.val());
           GetAppointmentCTCDetail(hdnAppointLetterId.val());
       }, 1000);

         if (hdnAccessMode.val() == "3") {
            $("#btnSave").hide();
            $("#btnUpdate").hide();
            $("#btnReset").hide();
            $("input").attr('readOnly', true);
            $("textarea").attr('readOnly', true);
            $("select").attr('disabled', true);
       
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


$("#txtAppointDate,#txtJoiningDate").datepicker({
    changeMonth: true,
    changeYear: true,
    dateFormat: 'dd-M-yy',
    yearRange: '-10:+100',
    onSelect: function (selected) {

    }
});
 
 
function SaveData() {
    var hdnAppointLetterId = $("#hdnAppointLetterId");
    var hdnInterviewId = $("#hdnInterviewId");
    var txtAppointLetterNo = $("#txtAppointLetterNo");
    var txtAppointDate = $("#txtAppointDate");
    var txtAppointmentLetterDesc = $("#txtAppointmentLetterDesc");
    var txtJoiningDate = $("#txtJoiningDate");
    var ddlAppointStatus = $("#ddlAppointStatus");

    var txtBasic = $("#txtBasic");
    var txtHRAAmount = $("#txtHRAAmount");
    var txtConveyance = $("#txtConveyance");
    var txtMedicalAmount = $("#txtMedicalAmount");
    var txtChildEduAllow = $("#txtChildEduAllow");
    var txtLTA = $("#txtLTA");
    var txtSpecialAllow = $("#txtSpecialAllow");
    var txtOtherAllow = $("#txtOtherAllow");
    var txtGrossSalary = $("#txtGrossSalary");
    var txtEmployeePF = $("#txtEmployeePF");
    var txtEmployeeESI = $("#txtEmployeeESI");
    var txtProfessionalTax = $("#txtProfessionalTax");
    var txtNetSalary = $("#txtNetSalary");
    var txtProfessionalTax = $("#txtProfessionalTax");
    var txtEmployerPF = $("#txtEmployerPF");
    var txtEmployerESI = $("#txtEmployerESI");
    var txtMonthlyCTC = $("#txtMonthlyCTC");
    var txtYearlyCTC = $("#txtYearlyCTC");

    var txtInterviewNo = $("#txtInterviewNo");
    if (txtInterviewNo.val().trim() == "") {
        ShowModel("Alert", "Please select Interview No")
        return false;
    }

    if (txtAppointmentLetterDesc.val().trim() == "") {
        ShowModel("Alert", "Enter Appointment Letter Description")
        return false;
    }

    if (txtJoiningDate.val().trim() == "") {
        ShowModel("Alert", "Please select Joining Date")
        return false;
    }
   
   
    if (txtBasic.val().trim() == "" || txtBasic.val()=="0") {
        ShowModel("Alert", "Enter Basic Salary!!")
        return false;
    }

    if (txtGrossSalary.val().trim() == "" || txtGrossSalary.val() == "0") {
        ShowModel("Alert", "Enter Gross Salary!!")
        return false;
    }

    if (txtMonthlyCTC.val().trim() == "" || txtMonthlyCTC.val() == "0") {
        ShowModel("Alert", "Enter Monthly CTC!!")
        return false;
    }


    if (txtYearlyCTC.val().trim() == "" || txtYearlyCTC.val() == "0") {
        ShowModel("Alert", "Enter Yearly CTC!!")
        return false;
    }

    var appointmentViewModel =
        {
                    AppointLetterId: hdnAppointLetterId.val(),
                    AppointDate: txtAppointDate.val().trim(),
                    InterviewId: hdnInterviewId.val(),
                    JoiningDate: txtJoiningDate.val(),
                    AppointmentLetterDesc: txtAppointmentLetterDesc.val(),
                    AppointStatus: ddlAppointStatus.val().trim()
        };
    
    var appointmentCTCViewModel = {
        AppointLetterId: hdnAppointLetterId.val(),
        Basic: txtBasic.val(),
        HRAAmount: txtHRAAmount.val(),
        Conveyance: txtConveyance.val(),
        Medical: txtMedicalAmount.val(),
        ChildEduAllow: txtChildEduAllow.val(),
        LTA: txtLTA.val(),
        SpecialAllow: txtSpecialAllow.val(),
        OtherAllow: txtOtherAllow.val(),
        GrossSalary: txtGrossSalary.val(),
        EmployeePF: txtEmployeePF.val(),
        EmployeeESI: txtEmployeeESI.val(),
        ProfessionalTax: txtProfessionalTax.val(),
        NetSalary: txtNetSalary.val(),
        EmployerPF: txtEmployerPF.val(),
        EmployerESI: txtEmployerESI.val(),
        MonthlyCTC: txtMonthlyCTC.val(),
        YearlyCTC:txtYearlyCTC.val()
    }


    var requestData = { appointmentViewModel: appointmentViewModel, appointmentCTCViewModel: appointmentCTCViewModel };
    $.ajax({
        url: "../Appointment/AddEditAppointment",
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
                       window.location.href = "../Appointment/AddEditAppointment?appointmentId=" + data.trnId + "&AccessMode=2";
                   }, 1000);

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

function ShowModel(headerText, bodyText) {    $("#alertModel").modal();
    $("#modelHeader").html(headerText);
    $("#modelText").html(bodyText);
}

function ClearFields() {
    $("#hdnAppointLetterId").val("0");
    $("hdnInterviewId").val("");
    $("#txtAppointLetterNo").val("");
    $("#txtAppointDate").val("");
    $("#txtAppointmentLetterDesc").val("");
    $("#txtJoiningDate").val("");
    $("#ddlAppointStatus").val("Draft");

    $("#txtBasic").val("");
    $("#txtHRAAmount").val("");
    $("#txtConveyance").val("0");
    $("#txtMedicalAmount").val("0");
    $("#txtChildEduAllow").val("");
    $("#txtLTA").val("");
    $("#txtSpecialAllow").val("");
    $("#txtOtherAllow").val("");
    $("#txtGrossSalary").val("0");
    $("#txtEmployeePF").val("");
    $("#txtEmployeeESI").val("");
    $("#txtProfessionalTax").val("0");
    $("#txtProfessionalTax").val("");
    $("#txtEmployerPF").val("");
    $("#txtMonthlyCTC").val("");
    $("#txtYearlyCTC").val("");

    $("#btnSave").show();
    $("#btnUpdate").hide();


}


function GetAppointmentDetail(appointLetterId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Appointment/GetAppointmentDetail",
        data: { appointLetterId:appointLetterId },
        dataType: "json",
        success: function (data) {
            $("#txtInterviewNo").val(data.InterviewNo);
            $("#txtApplicant").val(data.ApplicantNo);
            $("#txtFirstName").val(data.FirstName);
            $("#txtLastName").val(data.LastName);
            $("#txtMobileNo").val(data.MobileNo);
            $("#txtEmail").val(data.Email);
            $("#txtAppointLetterNo").val(data.AppointLetterNo);
            $("#txtAppointDate").val(data.AppointDate);
            $("#hdnInterviewId").val(data.InterviewId);
            $("#txtAppointmentLetterDesc").val(data.AppointmentLetterDesc);
            $("#txtJoiningDate").val(data.JoiningDate);
            $("#ddlAppointStatus").val(data.AppointStatus);
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

function GetAppointmentCTCDetail(appointLetterId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Appointment/GetAppointmentCTCDetail",
            data: { appointLetterId: appointLetterId},
        dataType: "json",
        success: function (data) {
            $("#txtBasic").val(data.Basic);
            $("#txtHRAAmount").val(data.HRAAmount);
            $("#txtConveyance").val(data.Conveyance);
            $("#txtMedicalAmount").val(data.Medical);
            $("#txtChildEduAllow").val(data.ChildEduAllow);
            $("#txtLTA").val(data.LTA);
            $("#txtSpecialAllow").val(data.SpecialAllow);
            $("#txtOtherAllow").val(data.OtherAllow);
            $("#txtGrossSalary").val(data.GrossSalary);
            $("#txtEmployeePF").val(data.EmployeePF);
            $("#txtEmployeeESI").val(data.EmployeeESI);
            $("#txtProfessionalTax").val(data.ProfessionalTax);
            $("#txtNetSalary").val(data.NetSalary);
            $("#txtEmployerPF").val(data.EmployerPF);
            $("#txtEmployerESI").val(data.EmployerESI);
            $("#txtMonthlyCTC").val(data.MonthlyCTC);
            $("#txtYearlyCTC").val(data.YearlyCTC);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
        }

function OpenInterviewSearchPopup() {
    $("#SearchInterviewModel").modal();

}

function SearchInterviews() {
    var txtApplicantNo = $("#txtApplicantNo");
    var txtInterviewNo = $("#txtInterviewNo");
    var ddlInterviewFinalStatus = $("#ddlInterviewFinalStatus");
    var txtFromDate = $("#txtFromDate");
    var txtToDate = $("#txtToDate");
    var requestData = {
        interviewNo: txtInterviewNo.val().trim(), applicantNo: txtApplicantNo.val(), interviewFinalStatus: ddlInterviewFinalStatus.val(),fromDate: txtFromDate.val(), toDate: txtToDate.val()  };
    $.ajax({
        url: "../Appointment/GetInterviewList",
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

function SelectInterview(interviewNo, interviewId) {
    $("#txtInterviewNo").val(interviewNo);
    $("#hdnInterviewId").val(interviewId);
    GetApplicantDetailForAppoint(interviewId);
    GetApplicantCTCDetailForAppoint(interviewId);
    $("#SearchInterviewModel").modal('hide');
}

function GetApplicantDetailForAppoint(interviewId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Appointment/GetApplicantDetailForAppoint",
        data: { interviewId: interviewId },
        dataType: "json",
        success: function (data) {
            $("#txtApplicant").val(data.ApplicantNo);
            $("#txtFirstName").val(data.FirstName);
            $("#txtLastName").val(data.LastName);
            $("#txtMobileNo").val(data.MobileNo);
            $("#txtEmail").val(data.Email);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}

function GetApplicantCTCDetailForAppoint(interviewId) {
    $.ajax({
        type: "GET",
        asnc: false,
        url: "../Appointment/GetApplicantCTCDetailForAppoint",
        data: { interviewId: interviewId },
        dataType: "json",
        success: function (data) {
            $("#txtBasic").val(data.Basic);
            $("#txtHRAAmount").val(data.HRAAmount);
            $("#txtConveyance").val(data.Conveyance);
            $("#txtMedicalAmount").val(data.Medical);
            $("#txtChildEduAllow").val(data.ChildEduAllow);
            $("#txtLTA").val(data.LTA);
            $("#txtSpecialAllow").val(data.SpecialAllow);
            $("#txtOtherAllow").val(data.OtherAllow);
            $("#txtGrossSalary").val(data.GrossSalary);
            $("#txtEmployeePF").val(data.EmployeePF);
            $("#txtEmployeeESI").val(data.EmployeeESI);
            $("#txtProfessionalTax").val(data.ProfessionalTax);
            $("#txtNetSalary").val(data.NetSalary);
            $("#txtEmployerPF").val(data.EmployerPF);
            $("#txtEmployerESI").val(data.EmployerESI);
            $("#txtMonthlyCTC").val(data.MonthlyCTC);
            $("#txtYearlyCTC").val(data.YearlyCTC);
        },
        error: function (Result) {
            ShowModel("Alert", "Problem in Request");
        }
    });
}