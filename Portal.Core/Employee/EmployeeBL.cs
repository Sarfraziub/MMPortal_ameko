﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;
using System.Transactions;

namespace Portal.Core
{
  public  class EmployeeBL
    {
        DBInterface dbInterface;
        public EmployeeBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditEmployee(EmployeeViewModel employeeViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            ResponseOut responseOutSL = new ResponseOut();
            using (TransactionScope transactionscope = new TransactionScope())
            {
                try
                {
                    Employee employee = new Employee
                    {
                        EmployeeId = employeeViewModel.EmployeeId,
                        ApplicantId=employeeViewModel.ApplicantId,
                        CompanyBranchId=employeeViewModel.CompanyBranchId,
                        EmployeeCode = employeeViewModel.EmployeeCode,
                        FirstName = employeeViewModel.FirstName,
                        LastName = employeeViewModel.LastName,
                        FatherSpouseName = employeeViewModel.FatherSpouseName,
                        Gender = employeeViewModel.Gender,
                        DateOfBirth = string.IsNullOrEmpty(employeeViewModel.DateOfBirth) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.DateOfBirth),
                        MaritalStatus = employeeViewModel.MaritalStatus,
                        BloodGroup = employeeViewModel.BloodGroup,
                        Email = employeeViewModel.Email,
                        AlternateEmail = employeeViewModel.AlternateEmail,
                        ContactNo = employeeViewModel.ContactNo,
                        AlternateContactno = employeeViewModel.AlternateContactno,
                        MobileNo = employeeViewModel.MobileNo,
                        CAddress = employeeViewModel.CAddress,
                        CCity = employeeViewModel.CCity,
                        CStateId = employeeViewModel.CStateId,
                        CCountryId = employeeViewModel.CCountryId,
                        CPinCode = employeeViewModel.CPinCode,
                        PAddress = employeeViewModel.PAddress,
                        PCity = employeeViewModel.PCity,
                        PStateId = employeeViewModel.PStateId,
                        PCountryId = employeeViewModel.PCountryId,
                        PPinCode = employeeViewModel.PPinCode,
                        DateOfJoin = string.IsNullOrEmpty(employeeViewModel.DateOfJoin) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.DateOfJoin),
                        DateOfLeave = string.IsNullOrEmpty(employeeViewModel.DateOfLeave) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.DateOfLeave),
                        PANNo = employeeViewModel.PANNo,
                        AadharNo = employeeViewModel.AadharNo,
                        BankDetail = employeeViewModel.BankDetail,
                        BankAccountNo = employeeViewModel.BankAccountNo,
                        PFApplicable = employeeViewModel.PFApplicable,
                        PFNo = employeeViewModel.PFNo,
                        ESIApplicable = employeeViewModel.ESIApplicable,
                        ESINo = employeeViewModel.ESINo,
                        CompanyId = employeeViewModel.CompanyId,
                        Division = employeeViewModel.Division,
                        DepartmentId = employeeViewModel.DepartmentId,
                        DesignationId = employeeViewModel.DesignationId,
                        EmploymentType = employeeViewModel.EmploymentType,
                        EmployeeCurrentStatus = employeeViewModel.EmployeeCurrentStatus,
                        EmployeeStatusPeriod = employeeViewModel.EmployeeStatusPeriod,
                        EmployeeStatusStartDate = string.IsNullOrEmpty(employeeViewModel.EmployeeStatusStartDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.EmployeeStatusStartDate),
                        CreatedBy = employeeViewModel.CreatedBy,
                        Status = employeeViewModel.Emp_Status
                    };

                    EmployeePayInfo employeePayInfo = new EmployeePayInfo
                    {
                        PayInfoId = employeeViewModel.PayInfoId,
                        EmployeeId = employeeViewModel.EmployeeId,
                        OTRate = employeeViewModel.OTRate,
                        BasicPay = employeeViewModel.BasicPay,
                        HRA = employeeViewModel.HRA,
                        ConveyanceAllow = employeeViewModel.ConveyanceAllow,
                        SpecialAllow = employeeViewModel.SpecialAllow,
                        OtherAllow = employeeViewModel.OtherAllow,
                        OtherDeduction = employeeViewModel.OtherDeduction,
                        MedicalAllow= employeeViewModel.MedicalAllow,
                        ChildEduAllow = employeeViewModel.ChildEduAllow,
                        LTA = employeeViewModel.LTA,
                        EmployeePF = employeeViewModel.EmployeePF,
                        EmployeeESI = employeeViewModel.EmployeeESI,
                        EmployerPF = employeeViewModel.EmployerPF,
                        EmployerESI = employeeViewModel.EmployerESI,
                        ProfessinalTax = employeeViewModel.ProfessinalTax
                    };

                    EmployeeReportingInfo employeeReportingInfo = new EmployeeReportingInfo
                    {
                        EmployeeReportingId = employeeViewModel.EmployeeReportingId,
                        EmployeeId = employeeViewModel.EmployeeId,
                        ReportingEmployeeId = employeeViewModel.ReportingEmployeeId
                    };

                    responseOut = dbInterface.AddEditEmployee(employee, employeeReportingInfo, employeePayInfo);

                    SL sl = new SL
                    {
                        SLId = 0,
                        SLCode = employeeViewModel.EmployeeCode,
                        SLHead = employeeViewModel.FirstName +' '+ employeeViewModel.LastName,
                        RefCode = employeeViewModel.EmployeeCode,
                        SLTypeId = 3,
                        CostCenterId = 0,
                        SubCostCenterId = 0,
                        CompanyId = employeeViewModel.CompanyId,
                        CreatedBy = employeeViewModel.CreatedBy,
                        Status = true
                    };

                    responseOutSL = dbInterface.AddEditEmployeeSL(sl, employeeViewModel.EmployeeId == 0 ? "Add" : "Edit");
                    transactionscope.Complete();


                }
                catch (TransactionException ex1)
                {
                    transactionscope.Dispose();
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex1);
                    throw ex1;
                }


                catch (Exception ex)
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
            }
            return responseOut;
        }
        public ResponseOut UpdateEmployeePic(EmployeeViewModel employeeViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Employee employee = new Employee
                {
                    EmployeeId = employeeViewModel.EmployeeId,
                    EmployeePicName = employeeViewModel.EmployeePicName,
                    EmployeePicPath = employeeViewModel.EmployeePicPath
                };
                responseOut = dbInterface.UpdateEmployeePic(employee);
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }
        public List<EmployeeViewModel> GetEmployeeAutoCompleteList(string searchTerm,int departmentId,int designationId)
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            try
            {
                List<Employee> employeeList = dbInterface.GetEmployeeAutoCompleteList(searchTerm,departmentId,designationId);

                if (employeeList != null && employeeList.Count > 0)
                {
                    foreach (Employee employee in employeeList)
                    {
                        employees.Add(new EmployeeViewModel { EmployeeId = employee.EmployeeId, FirstName = employee.FirstName + " " + employee.LastName, EmployeeCode = employee.EmployeeCode, MobileNo = employee.MobileNo });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return employees;
        }

        public List<EmployeeViewModel> GetTempEmployeeAutoCompleteList(string searchTerm, int departmentId, int designationId)
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            try
            {
                List<Employee> employeeList = dbInterface.GetTempEmployeeAutoCompleteList(searchTerm, departmentId, designationId);

                if (employeeList != null && employeeList.Count > 0)
                {
                    foreach (Employee employee in employeeList)
                    {
                        employees.Add(new EmployeeViewModel { EmployeeId = employee.EmployeeId, FirstName = employee.FirstName + " " + employee.LastName, EmployeeCode = employee.EmployeeCode, MobileNo = employee.MobileNo });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return employees;
        }
        public List<EmployeeViewModel> GetEmployeeList(string employeeName, string employeeCode, string mobileNo, string email, string panNo, int departmentId, string employeeType, string currentStatus, int companyId, string employeeStatus)
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtEmployees = sqlDbInterface.GetEmployeeList(employeeName, employeeCode,  mobileNo,  email,  panNo, departmentId, employeeType, currentStatus,  companyId, employeeStatus);
                if (dtEmployees != null && dtEmployees.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtEmployees.Rows)
                    {
                        employees.Add(new EmployeeViewModel
                        {
                            EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                            FirstName = Convert.ToString(dr["FirstName"]),
                            LastName = Convert.ToString(dr["LastName"]),
                            EmployeeCode = Convert.ToString(dr["EmployeeCode"]),
                            FatherSpouseName = Convert.ToString(dr["FatherSpouseName"]),
                            Gender = Convert.ToString(dr["Gender"]),
                            DateOfBirth = Convert.ToString(dr["DateOfBirth"]),
                            MaritalStatus = Convert.ToString(dr["MaritalStatus"]),
                            Email = Convert.ToString(dr["Email"]),
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            ContactNo = Convert.ToString(dr["ContactNo"]),
                            CAddress = Convert.ToString(dr["CAddress"]),
                            CCity = Convert.ToString(dr["CCity"]),
                            DateOfJoin = Convert.ToString(dr["DateOfJoin"]),
                            PANNo = Convert.ToString(dr["PANNo"]),
                            DepartmentName = Convert.ToString(dr["DepartmentName"]),
                            DesignationName = Convert.ToString(dr["DesignationName"]),
                            EmploymentType = Convert.ToString(dr["EmploymentType"]),
                            EmployeeCurrentStatus = Convert.ToString(dr["EmployeeCurrentStatus"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            Emp_Status = Convert.ToBoolean(dr["Status"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return employees;
        }
        public EmployeeViewModel GetEmployeeDetail(int employeeId = 0)
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetEmployeeDetail(employeeId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        employee = new EmployeeViewModel
                        {
                            EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                            EmployeeCode = Convert.ToString(dr["EmployeeCode"]),
                            CompanyBranchId=Convert.ToInt32(dr["CompanyBranchId"]),
                            BranchName = Convert.ToString(dr["BranchName"]),
                            ApplicantId = Convert.ToInt32(dr["ApplicantId"]),
                            ApplicantNo = Convert.ToString(dr["ApplicantNo"]),
                            FirstName = Convert.ToString(dr["FirstName"]),
                            LastName = Convert.ToString(dr["LastName"]),
                            FatherSpouseName = Convert.ToString(dr["FatherSpouseName"]),
                            Gender = Convert.ToString(dr["Gender"]),
                            DateOfBirth = Convert.ToString(dr["DateOfBirth"]),
                            MaritalStatus = Convert.ToString(dr["MaritalStatus"]),
                            BloodGroup = Convert.ToString(dr["BloodGroup"]),
                            Email = Convert.ToString(dr["Email"]),
                            AlternateEmail = Convert.ToString(dr["AlternateEmail"]),
                            ContactNo = Convert.ToString(dr["ContactNo"]),
                            AlternateContactno = Convert.ToString(dr["AlternateContactno"]),
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            CAddress = Convert.ToString(dr["CAddress"]),
                            CCity = Convert.ToString(dr["CCity"]),
                            CStateId = Convert.ToInt32(dr["CStateId"]),
                            CCountryId = Convert.ToInt32(dr["CCountryId"]),
                            CPinCode = Convert.ToString(dr["CPinCode"]),
                            PAddress = Convert.ToString(dr["PAddress"]),
                            PCity = Convert.ToString(dr["PCity"]),
                            PStateId = Convert.ToInt32(dr["PStateId"]),
                            PCountryId = Convert.ToInt32(dr["PCountryId"]),
                            PPinCode = Convert.ToString(dr["PPinCode"]),
                            DateOfJoin = Convert.ToString(dr["DateOfJoin"]),
                            DateOfLeave = Convert.ToString(dr["DateOfLeave"]),
                            PANNo = Convert.ToString(dr["PANNo"]),
                            AadharNo = Convert.ToString(dr["AadharNo"]),
                            BankDetail = Convert.ToString(dr["BankDetail"]),
                            BankAccountNo = Convert.ToString(dr["BankAccountNo"]),
                            PFApplicable = Convert.ToBoolean(dr["PFApplicable"]),
                            PFNo = Convert.ToString(dr["PFNo"]),
                            ESIApplicable = Convert.ToBoolean(dr["ESIApplicable"]),
                            ESINo = Convert.ToString(dr["ESINo"]),
                            Division = Convert.ToString(dr["Division"]),
                            DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
                            DesignationId = Convert.ToInt32(dr["DesignationId"]),
                            EmploymentType = Convert.ToString(dr["EmploymentType"]),
                            EmployeeCurrentStatus = Convert.ToString(dr["EmployeeCurrentStatus"]),
                            EmployeeStatusPeriod = Convert.ToInt16(dr["EmployeeStatusPeriod"]),
                            EmployeeStatusStartDate = Convert.ToString(dr["EmployeeStatusStartDate"]),

                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            OTRate = Convert.ToDecimal(dr["OTRate"]),
                            BasicPay = Convert.ToDecimal(dr["BasicPay"]),
                            HRA = Convert.ToDecimal(dr["HRA"]),
                            ConveyanceAllow = Convert.ToDecimal(dr["ConveyanceAllow"]),
                            SpecialAllow = Convert.ToDecimal(dr["SpecialAllow"]),
                            OtherAllow= Convert.ToDecimal(dr["OtherAllow"]),
                            OtherDeduction= Convert.ToDecimal(dr["OtherDeduction"]),
                            MedicalAllow = Convert.ToDecimal(dr["MedicalAllow"]),
                            ChildEduAllow = Convert.ToDecimal(dr["ChildEduAllow"]),
                            LTA = Convert.ToDecimal(dr["LTA"]),
                            EmployeePF = Convert.ToDecimal(dr["EmployeePF"]),
                            EmployeeESI = Convert.ToDecimal(dr["EmployeeESI"]),
                            EmployerPF = Convert.ToDecimal(dr["EmployerPF"]),
                            EmployerESI = Convert.ToDecimal(dr["EmployerESI"]),
                            ProfessinalTax = Convert.ToDecimal(dr["ProfessinalTax"]),
                            ReportingEmployeeId = Convert.ToInt32(dr["ReportingEmployeeId"]),
                            ReportingEmployeeName = Convert.ToString(dr["ReportingEmployeeName"]),
                            ReportingDepartmentId = Convert.ToInt32(dr["ReportingDepartmentId"]),
                            ReportingDesignationId = Convert.ToInt32(dr["ReportingDesignationId"]),
                            Emp_Status = Convert.ToBoolean(dr["Status"]),
                            EmployeePicPath = Convert.ToString(dr["EmployeePicPath"]),
                            EmployeePicName = Convert.ToString(dr["EmployeePicName"]),
                           
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return employee;
        }

        public ResponseOut ImportEmployee(EmployeeViewModel employeeViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            ResponseOut responseOutSL = new ResponseOut();
            using (TransactionScope transactionscope = new TransactionScope())
            {
                try
                {
                    Employee employee = new Employee
                    {
                        EmployeeId = employeeViewModel.EmployeeId,
                        EmployeeCode = employeeViewModel.EmployeeCode,
                        FirstName = employeeViewModel.FirstName,
                        LastName = employeeViewModel.LastName,
                        FatherSpouseName = employeeViewModel.FatherSpouseName,
                        Gender = employeeViewModel.Gender,
                        DateOfBirth = string.IsNullOrEmpty(employeeViewModel.DateOfBirth) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.DateOfBirth),
                        MaritalStatus = employeeViewModel.MaritalStatus,
                        BloodGroup = employeeViewModel.BloodGroup,
                        Email = employeeViewModel.Email,
                        AlternateEmail = employeeViewModel.AlternateEmail,
                        ContactNo = employeeViewModel.ContactNo,
                        AlternateContactno = employeeViewModel.AlternateContactno,
                        CompanyBranchId = employeeViewModel.CompanyBranchId,
                        MobileNo = employeeViewModel.MobileNo,
                        CAddress = employeeViewModel.CAddress,
                        CCity = employeeViewModel.CCity,
                        CStateId = employeeViewModel.CStateId,
                        CCountryId = employeeViewModel.CCountryId,
                        CPinCode = employeeViewModel.CPinCode,
                        PAddress = employeeViewModel.PAddress,
                        PCity = employeeViewModel.PCity,
                        PStateId = employeeViewModel.PStateId,
                        PCountryId = employeeViewModel.PCountryId,
                        PPinCode = employeeViewModel.PPinCode,
                        DateOfJoin = string.IsNullOrEmpty(employeeViewModel.DateOfJoin) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.DateOfJoin),
                        DateOfLeave = string.IsNullOrEmpty(employeeViewModel.DateOfLeave) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.DateOfLeave),
                        PANNo = employeeViewModel.PANNo,
                        AadharNo = employeeViewModel.AadharNo,
                        BankDetail = employeeViewModel.BankDetail,
                        BankAccountNo = employeeViewModel.BankAccountNo,
                        PFApplicable = employeeViewModel.PFApplicable,
                        PFNo = employeeViewModel.PFNo,
                        ESIApplicable = employeeViewModel.ESIApplicable,
                        ESINo = employeeViewModel.ESINo,
                        CompanyId = employeeViewModel.CompanyId,
                        Division = employeeViewModel.Division,
                        DepartmentId = employeeViewModel.DepartmentId,
                        DesignationId = employeeViewModel.DesignationId,
                        EmploymentType = employeeViewModel.EmploymentType,
                        EmployeeCurrentStatus = employeeViewModel.EmployeeCurrentStatus,
                        EmployeeStatusPeriod = employeeViewModel.EmployeeStatusPeriod,
                        EmployeePicName = employeeViewModel.EmployeePicName,
                        EmployeePicPath = employeeViewModel.EmployeePicPath,
                        EmployeeStatusStartDate = string.IsNullOrEmpty(employeeViewModel.EmployeeStatusStartDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.EmployeeStatusStartDate),
                        CreatedBy = employeeViewModel.CreatedBy,
                        Status = employeeViewModel.Emp_Status
                    };
                    responseOut = dbInterface.ImportEmployee(employee);    
                    transactionscope.Complete();


                }
                catch (TransactionException ex1)
                {
                    transactionscope.Dispose();
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex1);
                    throw ex1;
                }


                catch (Exception ex)
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
            }
            return responseOut;
        }

        public EmployeeViewModel GetESSEmployeeDetail(int userId = 0,int companyId=0)
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetEssEmployeeDetail(userId,companyId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        employee = new EmployeeViewModel
                        {
                            EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                            EmployeeCode = Convert.ToString(dr["EmployeeCode"]),
                            FirstName = Convert.ToString(dr["FirstName"]),
                            LastName = Convert.ToString(dr["LastName"]),
                            FatherSpouseName = Convert.ToString(dr["FatherSpouseName"]),
                            Gender = Convert.ToString(dr["Gender"]),
                            DateOfBirth = Convert.ToString(dr["DateOfBirth"]),
                            MaritalStatus = Convert.ToString(dr["MaritalStatus"]),
                            BloodGroup = Convert.ToString(dr["BloodGroup"]),
                            Email = Convert.ToString(dr["Email"]),
                            AlternateEmail = Convert.ToString(dr["AlternateEmail"]),
                            ContactNo = Convert.ToString(dr["ContactNo"]),
                            AlternateContactno = Convert.ToString(dr["AlternateContactno"]),
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            CAddress = Convert.ToString(dr["CAddress"]),
                            CCity = Convert.ToString(dr["CCity"]),
                            CStateId = Convert.ToInt32(dr["CStateId"]),
                            CStateName = Convert.ToString(dr["StateName"]),
                            CCountryId = Convert.ToInt32(dr["CCountryId"]),
                            CCountryName = Convert.ToString(dr["CountryName"]),
                            CPinCode = Convert.ToString(dr["CPinCode"]),
                            PAddress = Convert.ToString(dr["PAddress"]),
                            PCity = Convert.ToString(dr["PCity"]),
                            PStateId = Convert.ToInt32(dr["PStateId"]),
                            PCountryId = Convert.ToInt32(dr["PCountryId"]),
                            PPinCode = Convert.ToString(dr["PPinCode"]),
                            DateOfJoin = Convert.ToString(dr["DateOfJoin"]),
                            DateOfLeave = Convert.ToString(dr["DateOfLeave"]),
                            PANNo = Convert.ToString(dr["PANNo"]),
                            AadharNo = Convert.ToString(dr["AadharNo"]),
                            BankDetail = Convert.ToString(dr["BankDetail"]),
                            BankAccountNo = Convert.ToString(dr["BankAccountNo"]),
                            PFApplicable = Convert.ToBoolean(dr["PFApplicable"]),
                            PFNo = Convert.ToString(dr["PFNo"]),
                            ESIApplicable = Convert.ToBoolean(dr["ESIApplicable"]),
                            ESINo = Convert.ToString(dr["ESINo"]),
                            Division = Convert.ToString(dr["Division"]),
                            DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
                            DepartmentName=Convert.ToString(dr["DepartmentName"]),
                            DesignationId = Convert.ToInt32(dr["DesignationId"]),
                            DesignationName=Convert.ToString(dr["DesignationName"]),
                            EmploymentType = Convert.ToString(dr["EmploymentType"]),
                            EmployeeCurrentStatus = Convert.ToString(dr["EmployeeCurrentStatus"]),
                            EmployeeStatusPeriod = Convert.ToInt16(dr["EmployeeStatusPeriod"]),
                            EmployeeStatusStartDate = Convert.ToString(dr["EmployeeStatusStartDate"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            Emp_Status = Convert.ToBoolean(dr["Status"]),
                            EmployeePicPath = Convert.ToString(dr["EmployeePicPath"]),
                            EmployeePicName = Convert.ToString(dr["EmployeePicName"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return employee;
        }

        public ResponseOut AddEditEmployeeProfile(EmployeeViewModel employeeViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            ResponseOut responseOutSL = new ResponseOut();
            using (TransactionScope transactionscope = new TransactionScope())
            {
                try
                {
                    Employee employee = new Employee
                    {
                        EmployeeId = employeeViewModel.EmployeeId,
                        ApplicantId = employeeViewModel.ApplicantId,
                        CompanyBranchId = employeeViewModel.CompanyBranchId,
                        EmployeeCode = employeeViewModel.EmployeeCode,
                        FirstName = employeeViewModel.FirstName,
                        LastName = employeeViewModel.LastName,
                        FatherSpouseName = employeeViewModel.FatherSpouseName,
                        Gender = employeeViewModel.Gender,
                        DateOfBirth = string.IsNullOrEmpty(employeeViewModel.DateOfBirth) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.DateOfBirth),
                        MaritalStatus = employeeViewModel.MaritalStatus,
                        BloodGroup = employeeViewModel.BloodGroup,
                        Email = employeeViewModel.Email,
                        AlternateEmail = employeeViewModel.AlternateEmail,
                        ContactNo = employeeViewModel.ContactNo,
                        AlternateContactno = employeeViewModel.AlternateContactno,
                        MobileNo = employeeViewModel.MobileNo,
                        CAddress = employeeViewModel.CAddress,
                        CCity = employeeViewModel.CCity,
                        CStateId = employeeViewModel.CStateId,
                        CCountryId = employeeViewModel.CCountryId,
                        CPinCode = employeeViewModel.CPinCode,
                        PAddress = employeeViewModel.PAddress,
                        PCity = employeeViewModel.PCity,
                        PStateId = employeeViewModel.PStateId,
                        PCountryId = employeeViewModel.PCountryId,
                        PPinCode = employeeViewModel.PPinCode,
                        DateOfJoin = string.IsNullOrEmpty(employeeViewModel.DateOfJoin) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.DateOfJoin),
                        DateOfLeave = string.IsNullOrEmpty(employeeViewModel.DateOfLeave) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.DateOfLeave),
                        PANNo = employeeViewModel.PANNo,
                        AadharNo = employeeViewModel.AadharNo,
                        BankDetail = employeeViewModel.BankDetail,
                        BankAccountNo = employeeViewModel.BankAccountNo,
                        PFApplicable = employeeViewModel.PFApplicable,
                        PFNo = employeeViewModel.PFNo,
                        ESIApplicable = employeeViewModel.ESIApplicable,
                        ESINo = employeeViewModel.ESINo,
                        CompanyId = employeeViewModel.CompanyId,
                        Division = employeeViewModel.Division,
                        DepartmentId = employeeViewModel.DepartmentId,
                        DesignationId = employeeViewModel.DesignationId,
                        EmploymentType = employeeViewModel.EmploymentType,
                        EmployeeCurrentStatus = employeeViewModel.EmployeeCurrentStatus,
                        EmployeeStatusPeriod = employeeViewModel.EmployeeStatusPeriod,
                        EmployeeStatusStartDate = string.IsNullOrEmpty(employeeViewModel.EmployeeStatusStartDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(employeeViewModel.EmployeeStatusStartDate),
                        CreatedBy = employeeViewModel.CreatedBy,
                        Status = employeeViewModel.Emp_Status
                    };

                    EmployeePayInfo employeePayInfo = new EmployeePayInfo
                    {
                        PayInfoId = employeeViewModel.PayInfoId,
                        EmployeeId = employeeViewModel.EmployeeId,
                        OTRate = employeeViewModel.OTRate,
                        BasicPay = employeeViewModel.BasicPay,
                        HRA = employeeViewModel.HRA,
                        ConveyanceAllow = employeeViewModel.ConveyanceAllow,
                        SpecialAllow = employeeViewModel.SpecialAllow,
                        OtherAllow = employeeViewModel.OtherAllow,
                        OtherDeduction = employeeViewModel.OtherDeduction,
                        MedicalAllow = employeeViewModel.MedicalAllow,
                        ChildEduAllow = employeeViewModel.ChildEduAllow,
                        LTA = employeeViewModel.LTA,
                        EmployeePF = employeeViewModel.EmployeePF,
                        EmployeeESI = employeeViewModel.EmployeeESI,
                        EmployerPF = employeeViewModel.EmployerPF,
                        EmployerESI = employeeViewModel.EmployerESI,
                        ProfessinalTax = employeeViewModel.ProfessinalTax
                    };

                    EmployeeReportingInfo employeeReportingInfo = new EmployeeReportingInfo
                    {
                        EmployeeReportingId = employeeViewModel.EmployeeReportingId,
                        EmployeeId = employeeViewModel.EmployeeId,
                        ReportingEmployeeId = employeeViewModel.ReportingEmployeeId
                    };

                    responseOut = dbInterface.AddEditEmployee(employee, employeeReportingInfo, employeePayInfo);

                    SL sl = new SL
                    {
                        SLId = 0,
                        SLCode = employeeViewModel.EmployeeCode,
                        SLHead = employeeViewModel.FirstName + ' ' + employeeViewModel.LastName,
                        RefCode = employeeViewModel.EmployeeCode,
                        SLTypeId = 3,
                        CostCenterId = 0,
                        SubCostCenterId = 0,
                        CompanyId = employeeViewModel.CompanyId,
                        CreatedBy = employeeViewModel.CreatedBy,
                        Status = true
                    };

                    responseOutSL = dbInterface.AddEditEmployeeSL(sl, employeeViewModel.EmployeeId == 0 ? "Add" : "Edit");
                    transactionscope.Complete();


                }
                catch (TransactionException ex1)
                {
                    transactionscope.Dispose();
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex1);
                    throw ex1;
                }


                catch (Exception ex)
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = ActionMessage.ApplicationException;
                    Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }
            }
            return responseOut;
        }
         

        public ResponseOut RemoveImage(long employeeId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                responseOut = dbInterface.RemoveImageEmployee(employeeId);
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }





    }
}
