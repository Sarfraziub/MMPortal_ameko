using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;
using Portal.DAL.Infrastructure;

namespace Portal.Core
{
    public class PayrollProcessPeriodBL
    {
        HRMSDBInterface dbInterface;
        public PayrollProcessPeriodBL()
        {
            dbInterface = new HRMSDBInterface();
        }
        public ResponseOut AddEditPayrollProcessing(PayrollProcessPeriodViewModel payrollProcessPeriodViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            HRSQLDBInterface sqlDbInterface = new HRSQLDBInterface();
            try
            {
                PR_PayrollProcessPeriod payrollProcessing = new PR_PayrollProcessPeriod
                {
                    PayrollProcessingPeriodId = payrollProcessPeriodViewModel.PayrollProcessingPeriodId,
                    PayrollProcessingStartDate = Convert.ToDateTime(payrollProcessPeriodViewModel.PayrollProcessingStartDate),
                    PayrollProcessingEndDate = Convert.ToDateTime(payrollProcessPeriodViewModel.PayrollProcessingEndDate),
                    MonthId = payrollProcessPeriodViewModel.MonthId,
                    CompanyId = payrollProcessPeriodViewModel.CompanyId,
                    FinYearId = payrollProcessPeriodViewModel.FinYearId,
                    PayrollProcessStatus = payrollProcessPeriodViewModel.PayrollProcessStatus,
                    PayrollLocked = payrollProcessPeriodViewModel.PayrollLocked=="1"?true:false,
                    CreatedBy = payrollProcessPeriodViewModel.CreatedBy
                };
                
                responseOut = sqlDbInterface.AddEditPayrollProcessing(payrollProcessing);

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
        public List<PayrollProcessPeriodViewModel> GetPayrollProcessPeriodList(int monthId, string payrollProcessStatus, string payrollLocked, int companyId)
        {
            List<PayrollProcessPeriodViewModel> payrollProcessPeriodViewModel = new List<PayrollProcessPeriodViewModel>();
            HRSQLDBInterface sqlDbInterface = new HRSQLDBInterface();
            try
            {
                DataTable dtPayrollProcessPeriod = sqlDbInterface.GetPayrollProcessPeriodList(monthId, payrollProcessStatus, payrollLocked, companyId);
                if (dtPayrollProcessPeriod != null && dtPayrollProcessPeriod.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPayrollProcessPeriod.Rows)
                    {
                        payrollProcessPeriodViewModel.Add(new PayrollProcessPeriodViewModel
                        {
                            PayrollProcessingPeriodId = Convert.ToInt32(dr["PayrollProcessingPeriodId"]),
                            PayrollProcessingStartDate = Convert.ToString(dr["PayrollProcessingStartDate"]),
                            PayrollProcessingEndDate = Convert.ToString(dr["PayrollProcessingEndDate"]),
                            MonthId = Convert.ToInt32(dr["MonthId"]),
                            MonthShortName = Convert.ToString(dr["MonthShortName"]),
                            FinYearId = Convert.ToInt32(dr["FinYearId"]),
                            FinYearCode = Convert.ToString(dr["FinYearCode"]),
                            PayrollProcessStatus = Convert.ToString(dr["PayrollProcessStatus"]),
                            PayrollProcessDate = Convert.ToString(dr["PayrollProcessDate"]),
                            PayrollLocked = Convert.ToString(dr["PayrollLocked"]),
                            PayrollLockDate = Convert.ToString(dr["PayrollLockDate"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedByUserName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return payrollProcessPeriodViewModel;
        }
        public List<PayrollMonthViewModel> GetPayrollMonthList(int finYear)
        {
            List<PayrollMonthViewModel> payrollMonths = new List<PayrollMonthViewModel>();
            try
            {
                List<PR_PayrollMonth> payrollMonthList = dbInterface.GetPayrollMonth();
                int year = finYear;
                if (payrollMonthList != null && payrollMonthList.Count > 0)
                {
                    foreach (PR_PayrollMonth month in payrollMonthList)
                    {
                        year = Convert.ToInt16(month.MonthNo) >= 4 && Convert.ToInt16(month.MonthNo) <= 12 ? finYear : finYear + 1;
                        payrollMonths.Add(new PayrollMonthViewModel { MonthId = month.MonthId, MonthName = month.MonthName  , MonthShortName=month.MonthShortName + "-" + year.ToString()  ,MonthNo=Convert.ToInt16(month.MonthNo)});
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return payrollMonths;
        }
        public PayrollProcessPeriodViewModel GetPayrollMonthStartEndDate(int monthId,int finYear)
        {
            PayrollProcessPeriodViewModel payrollDates = new PayrollProcessPeriodViewModel();
            try
            {
                
                PR_PayrollMonth month = dbInterface.GetPayrollMonthDetail(monthId);
                int year = finYear;
                year = Convert.ToInt16(month.MonthNo) >= 4 && Convert.ToInt16(month.MonthNo) <= 12 ? finYear : finYear + 1;
                DateTime fromDate = new DateTime(year, Convert.ToInt16(month.MonthNo), 1);
                DateTime toDate = fromDate.AddMonths(1).AddDays(-1);
                payrollDates.PayrollProcessingStartDate = fromDate.ToString("dd-MMM-yyyy");
                payrollDates.PayrollProcessingEndDate = toDate.ToString("dd-MMM-yyyy");


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return payrollDates;
        }
        public PayrollProcessPeriodViewModel GetPayrollProcessPeriodDetail(long payrollProcessingPeriodId = 0)
        {
            PayrollProcessPeriodViewModel payrollProcessPeriodViewModel = new PayrollProcessPeriodViewModel();
            HRSQLDBInterface sqlDbInterface = new HRSQLDBInterface();
            try
            {
                DataTable dtPayrollProcessPeriod = sqlDbInterface.GetPayrollProcessPeriodDetail(payrollProcessingPeriodId);
                if (dtPayrollProcessPeriod != null && dtPayrollProcessPeriod.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPayrollProcessPeriod.Rows)
                    {
                        payrollProcessPeriodViewModel = new PayrollProcessPeriodViewModel
                        {
                            PayrollProcessingPeriodId = Convert.ToInt32(dr["PayrollProcessingPeriodId"]),
                            PayrollProcessingStartDate = Convert.ToString(dr["PayrollProcessingStartDate"]),
                            PayrollProcessingEndDate = Convert.ToString(dr["PayrollProcessingEndDate"]),
                            MonthId = Convert.ToInt32(dr["MonthId"]),
                            MonthShortName = Convert.ToString(dr["MonthShortName"]),
                            FinYearId = Convert.ToInt32(dr["FinYearId"]),
                            FinYearCode = Convert.ToString(dr["FinYearCode"]),
                            PayrollProcessStatus = Convert.ToString(dr["PayrollProcessStatus"]),
                            PayrollProcessDate = Convert.ToString(dr["PayrollProcessDate"]),
                            PayrollLocked = Convert.ToString(dr["PayrollLocked"]).ToUpper()=="LOCKED"?"1":"0",
                            PayrollLockDate = Convert.ToString(dr["PayrollLockDate"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedByUserName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return payrollProcessPeriodViewModel;
        }
        public List<PayrollProcessPeriodViewModel> GetPayrollProcessedMonthList(int companyId,int finYearId)
        {
            List<PayrollProcessPeriodViewModel> payrollMonths = new List<PayrollProcessPeriodViewModel>();
            try
            {
                List<PR_PayrollProcessPeriod> payrollMonthList = dbInterface.GetPayrollProcessedMonth(companyId,finYearId);
                
                if (payrollMonthList != null && payrollMonthList.Count > 0)
                {
                    foreach (PR_PayrollProcessPeriod month in payrollMonthList)
                    {
                        payrollMonths.Add(new PayrollProcessPeriodViewModel { PayrollProcessingPeriodId = month.PayrollProcessingPeriodId,PayrollProcessingStartDate = Convert.ToDateTime(month.PayrollProcessingStartDate).ToString("dd-MMM-yyyy"), PayrollProcessingEndDate = Convert.ToDateTime(month.PayrollProcessingEndDate).ToString("dd-MMM-yyyy")});
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return payrollMonths;
        }

        public DataTable GetSalarySummaryReport(long payrollProcessingPeriodId, int companyBranchId, int departmentId, int designationId, string employeeType, string employeeCodes)
        {
            DataTable dtSalarySummary = new DataTable();
            HRSQLDBInterface sqlDbInterface = new HRSQLDBInterface();
            try
            {
                dtSalarySummary= sqlDbInterface.GetSalarySummaryReport(payrollProcessingPeriodId, companyBranchId, departmentId, designationId, employeeType, employeeCodes);
                
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtSalarySummary;
        }
        public DataTable GetSalaryJVReport(long payrollProcessingPeriodId, int companyBranchId, int departmentId, int designationId, string employeeType, string employeeCodes)
        {
            DataTable dtSalaryJV= new DataTable();
            HRSQLDBInterface sqlDbInterface = new HRSQLDBInterface();
            try
            {
                dtSalaryJV = sqlDbInterface.GetSalaryJVReport(payrollProcessingPeriodId, companyBranchId, departmentId, designationId, employeeType, employeeCodes);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtSalaryJV;
        }

        public ResponseOut GenerateSalaryJV(long payrollProcessingPeriodId, int companyBranchId, int departmentId, int designationId, string employeeType,int createdBy, string employeeCodes)
        {
            ResponseOut responseOut = new ResponseOut();
            HRSQLDBInterface sqlDbInterface = new HRSQLDBInterface();
            try
            {
                responseOut = sqlDbInterface.GenerateSalaryJV(payrollProcessingPeriodId, companyBranchId, departmentId, designationId, employeeType,createdBy, employeeCodes);

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


        public ResponseOut LockUnlockPayrollProcessPeriod(PayrollProcessPeriodViewModel payrollProcessPeriodViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            HRSQLDBInterface sqlDbInterface = new HRSQLDBInterface();
            try
            {
                PR_PayrollProcessPeriod pR_PayrollProcessPeriod = new PR_PayrollProcessPeriod
                {
                    PayrollProcessingPeriodId = payrollProcessPeriodViewModel.PayrollProcessingPeriodId,
                    PayrollLocked = payrollProcessPeriodViewModel.PayrollLocked == "1" ? true : false,                 
                    CreatedBy = payrollProcessPeriodViewModel.CreatedBy
                };

                responseOut = sqlDbInterface.LockUnlockPayrollProcessing(pR_PayrollProcessPeriod);
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








