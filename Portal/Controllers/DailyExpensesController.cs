using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Data;

namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class DailyExpensesController : BaseController
    {
        //
        // GET: /Company/
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_DailyExpenses, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditDailyExpenses(int dailyExpensesId = 0, int accessMode = 3)
        {

            try
            {
                if (dailyExpensesId != 0)
                {
                    ViewData["dailyExpensesId"] = dailyExpensesId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["dailyExpensesId"] = 0;
                    ViewData["accessMode"] = 0;
                    ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_DailyExpenses, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditDailyExpenses(DailyExpensesViewModel dailyExpensesViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            DailyExpenseBL dailyExpenseBL = new DailyExpenseBL();
            try
            {
                if (dailyExpensesViewModel != null)
                {
                    dailyExpensesViewModel.CompanyId = ContextUser.CompanyId;
                    responseOut = dailyExpenseBL.AddEditDailyExpense(dailyExpensesViewModel);
                }
                else
                {
                    responseOut.message = ActionMessage.ProbleminData;
                    responseOut.status = ActionStatus.Fail;
                }

            }
            catch (Exception ex)
            {
                responseOut.message = ActionMessage.ApplicationException;
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDailyExpensesDetail(long dailyExpensesId)
        {
            DailyExpenseBL dailyExpenseBL = new DailyExpenseBL();
            DailyExpensesViewModel dailyExpenses = new DailyExpensesViewModel();
            try
            {
                dailyExpenses = dailyExpenseBL.GetDailyExpensesDetail(dailyExpensesId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(dailyExpenses, JsonRequestBehavior.AllowGet);
        }

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_DailyExpenses, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListDailyExpenses()
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpGet]
        public PartialViewResult GetDailyExpensesList(long employeeId=0, long timeTypeId=0, long customerBranchid=0, string fromDate="",string toDate="")
        {
            List<DailyExpensesViewModel> dailyExpenses = new List<DailyExpensesViewModel>();
            DailyExpenseBL dailyExpenseBL = new DailyExpenseBL();
            try
            {
                int CompanyId = ContextUser.CompanyId;
                dailyExpenses = dailyExpenseBL.GetDailyExpensesList(employeeId,timeTypeId,customerBranchid,fromDate,toDate, CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(dailyExpenses);
        }
        

        [HttpGet]
        public JsonResult GetTimeTypeList()
        {
            DailyExpenseBL dailyExpenseBL = new DailyExpenseBL();
            List<TimeTypeViewModel> timeTypeList = new List<TimeTypeViewModel>();
            try
            {
                timeTypeList = dailyExpenseBL.GetTimeTypeList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(timeTypeList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFoodAndTeaExpensesByTimeType(string timeTypeName)
        {
            DailyExpenseBL dailyExpenseBL = new DailyExpenseBL();
            decimal FoodAndTeaExpense = 0;
            try
            {
                FoodAndTeaExpense = dailyExpenseBL.GetFoodAndTeaExpensesByTimeType(timeTypeName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(FoodAndTeaExpense, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetVagesByEmployeeId(int employeeId)
        {
            DailyExpenseBL dailyExpenseBL = new DailyExpenseBL();
            decimal Vages = 0;
            try
            {
                Vages = dailyExpenseBL.GetVagesByEmployeeId(employeeId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(Vages, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCustomerBranchListWithOutCustId()
        {
            CustomerBL customerBL = new CustomerBL();
            List<CustomerBranchViewModel> customerBranchList = new List<CustomerBranchViewModel>();
            try
            {
                customerBranchList = customerBL.GetCustomerBranchListWithOutCustId();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(customerBranchList, JsonRequestBehavior.AllowGet);
        }

        //Daily Expenses Report-------------
        public ActionResult DailyExpensesReport(long employeeId = 0, long timeTypeId = 0, long customerBranchid = 0, string fromDate = "", string toDate = "", string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            DailyExpenseBL dailyExpenseBL = new DailyExpenseBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "DailyExpensesReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            DataTable dt = new DataTable();
            dt = dailyExpenseBL.DailyExpensesReport(employeeId, timeTypeId, customerBranchid, fromDate, toDate,ContextUser.CompanyId);
            ReportDataSource rd = new ReportDataSource("DailyExpensesReportDataSet", dt);
            lr.DataSources.Add(rd);
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + reportType + "</OutputFormat>" +
                        "  <PageWidth>15in</PageWidth>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.50in</MarginTop>" +
                        "  <MarginLeft>.1in</MarginLeft>" +
                        "  <MarginRight>.1in</MarginRight>" +
                        "  <MarginBottom>0.2in</MarginBottom>" +
                        "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }
    }
}
