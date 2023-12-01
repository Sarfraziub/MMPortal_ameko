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
using System.Transactions;
namespace Portal.Core
{
    public class DailyExpenseBL
    {
        DBInterface dbInterface;
        public DailyExpenseBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditDailyExpense(DailyExpensesViewModel dailyExpensesViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqldbinterface = new SQLDbInterface();
            try
            {
                DailyExpense dailyExpense = new DailyExpense
                {
                    DailyExpenseId = dailyExpensesViewModel.DailyExpensesId,
                    DailyExpenseCode = dailyExpensesViewModel.DailyExpenseCode,
                    DailyExpenseDate = Convert.ToDateTime(dailyExpensesViewModel.DailyExpenseDate),
                    EmployeeId = dailyExpensesViewModel.EmployeeId,
                    TimeTypeId = Convert.ToInt64(dailyExpensesViewModel.TimeTypeId),
                    ConveyanceAmt = dailyExpensesViewModel.ConveyanceAmt,
                    FoodTeaExpense = dailyExpensesViewModel.FoodAndTeaExpenses,
                    CustomerId = dailyExpensesViewModel.CustomerId,
                    CustomerBranchId = dailyExpensesViewModel.CustomerBranchId,
                    CompanyBranchId = dailyExpensesViewModel.CompanyBranchId,
                    DailyExpenseStatus = dailyExpensesViewModel.DailyExpenseStatus,
                    CompanyId = dailyExpensesViewModel.CompanyId

                }; 
                responseOut = sqldbinterface.AddEditDailyExpense(dailyExpense); 

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

        public DailyExpensesViewModel GetCustomerPaymentDetail(long dailyExpensesId = 0)
        {
            DailyExpensesViewModel dailyExpenses = new DailyExpensesViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtDailyExpenses = sqlDbInterface.GetDailyExpensesDetail(dailyExpensesId);
                if (dtDailyExpenses != null && dtDailyExpenses.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDailyExpenses.Rows)
                    {
                        dailyExpenses = new DailyExpensesViewModel
                        {
                            DailyExpensesId = Convert.ToInt32(dr["DailyExpenseId"]),
                            DailyExpenseCode = Convert.ToString(dr["DailyExpenseCode"]),
                            EmployeeName = Convert.ToString(dr["EmployeeName"]),
                            EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                            TimeTypeId = Convert.ToInt32(dr["TimeTypeId"]),
                            TimeTypeName = Convert.ToString(dr["TimeTypeName"]),
                            ConveyanceAmt = Convert.ToDecimal(dr["ConveyanceAmt"]),
                            FoodAndTeaExpenses = Convert.ToDecimal(dr["FoodTeaExpense"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            CustomerBranchName = Convert.ToString(dr["CustomerBranchName"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            CompanyBranchName = Convert.ToString(dr["CompanyBranchName"]),
                            DailyExpenseStatus = Convert.ToString(dr["DailyExpenseStatus"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dailyExpenses;
        }

        public DailyExpensesViewModel GetDailyExpensesDetail(long dailyExpensesId = 0)
        {
            DailyExpensesViewModel dailyExpenses = new DailyExpensesViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtDailyExpenses = sqlDbInterface.GetDailyExpensesDetail(dailyExpensesId);
                if (dtDailyExpenses != null && dtDailyExpenses.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDailyExpenses.Rows)
                    {
                        dailyExpenses = new DailyExpensesViewModel
                        {
                            DailyExpensesId = Convert.ToInt32(dr["DailyExpenseId"]),
                            DailyExpenseCode = Convert.ToString(dr["DailyExpenseCode"]),
                            DailyExpenseDate = Convert.ToString(dr["DailyExpenseDate"]),
                            EmployeeName = Convert.ToString(dr["EmployeeName"]),
                            EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                            TimeTypeId = Convert.ToInt32(dr["TimeTypeId"]),
                            TimeTypeName = Convert.ToString(dr["TimeTypeName"]),
                            ConveyanceAmt = Convert.ToDecimal(dr["ConveyanceAmt"]),
                            FoodAndTeaExpenses = Convert.ToDecimal(dr["FoodTeaExpense"]),
                            Vages = Convert.ToDecimal(dr["Vages"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerBranchId = Convert.ToInt32(dr["CustomerBranchId"]),
                            CustomerBranchName = Convert.ToString(dr["CustomerBranchName"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            CompanyBranchName = Convert.ToString(dr["CompanyBranchName"]),
                            DailyExpenseStatus = Convert.ToString(dr["DailyExpenseStatus"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dailyExpenses;
        }
        public List<DailyExpensesViewModel> GetDailyExpensesList(long employeeId, long timeTypeId, long customerBranchid, string fromDate, string toDate, int companyId)
        {
            List<DailyExpensesViewModel> dailyExpenses = new List<DailyExpensesViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtdailyExpenses = sqlDbInterface.GetDailyExpensesList(employeeId, timeTypeId, customerBranchid, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId);
                if (dtdailyExpenses != null && dtdailyExpenses.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtdailyExpenses.Rows)
                    {
                        dailyExpenses.Add(new DailyExpensesViewModel
                        {
                            DailyExpensesId = Convert.ToInt32(dr["DailyExpenseId"]),
                            DailyExpenseCode = Convert.ToString(dr["DailyExpenseCode"]),
                            DailyExpenseDate = Convert.ToString(dr["DailyExpenseDate"]),
                            EmployeeName = Convert.ToString(dr["EmployeeName"]),
                            EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                            TimeTypeId = Convert.ToInt32(dr["TimeTypeId"]),
                            TimeTypeName = Convert.ToString(dr["TimeTypeName"]),
                            ConveyanceAmt = Convert.ToDecimal(dr["ConveyanceAmt"]),
                            FoodAndTeaExpenses = Convert.ToDecimal(dr["FoodTeaExpense"]),
                            Vages = Convert.ToDecimal(dr["Vages"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            CustomerBranchName = Convert.ToString(dr["CustomerBranchName"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            CompanyBranchName = Convert.ToString(dr["CompanyBranchName"]),
                            DailyExpenseStatus = Convert.ToString(dr["DailyExpenseStatus"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dailyExpenses;
        }
        public List<CustomerViewModel> GetCustomerAutoCompleteList(string searchTerm, int companyId)
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            try
            {
                List<Customer> customerList = dbInterface.GetCustomerAutoCompleteList(searchTerm, companyId);

                if (customerList != null && customerList.Count > 0)
                {
                    foreach (Customer customer in customerList)
                    {
                        customers.Add(new CustomerViewModel { CustomerId = customer.CustomerId, CustomerName = customer.CustomerName, CustomerCode = customer.CustomerCode, PrimaryAddress = customer.PrimaryAddress });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customers;
        }

        public List<TimeTypeViewModel> GetTimeTypeList()
        {
            List<TimeTypeViewModel> timeTypeList = new List<TimeTypeViewModel>();
            try
            {
                List<Portal.DAL.TimeType> timeTypes = dbInterface.GetTimeTypeList();
                if (timeTypes != null && timeTypes.Count > 0)
                {
                    foreach (Portal.DAL.TimeType timetype in timeTypes)
                    {
                        timeTypeList.Add(new TimeTypeViewModel { TimeTypeId = timetype.TimeTypeId, TimeTypeName = timetype.TimeTypeName });
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return timeTypeList;
        }

        public decimal GetFoodAndTeaExpensesByTimeType(string timeTypeName)
        {
            decimal FoodAndTeaExpense = 0;
            try
            {
                FoodAndTeaExpense = dbInterface.GetFoodAndTeaExpensesByTimeType(timeTypeName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return FoodAndTeaExpense;
        }

        public decimal GetVagesByEmployeeId(int employeeId)
        {
            decimal Vages = 0;
            try
            {
                Vages = dbInterface.GetVagesByEmployeeId(employeeId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return Vages;
        }

        public DataTable DailyExpensesReport(long employeeId, long timeTypeId, long customerBranchid, string fromDate, string toDate, int companyId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtdailyExpenses = new DataTable();
            try
            {
                dtdailyExpenses = sqlDbInterface.GetDailyExpensesList(employeeId, timeTypeId, customerBranchid, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtdailyExpenses;
        }

    }
}
