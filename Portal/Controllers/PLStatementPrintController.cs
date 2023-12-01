using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class PLStatementPrintController : BaseController
    {
        //
     
        #region Profit Loss Statement Print
        [ValidateRequest(true, UserInterfaceHelper.PLStatementPrint, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult PLStatementPrint()
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

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.PLStatementPrint, (int)AccessMode.ViewAccess, (int)RequestMode.Ajax)]
        public ActionResult PLStatementGenerate(string fromDate, string toDate)
        {
            ResponseOut responseOut = new ResponseOut();
            ProfitLossStatementPrintBL plPrintBL = new ProfitLossStatementPrintBL();
            try
            {
                    
                    int finYearId= Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = plPrintBL.GenerateProfitLossStatement(ContextUser.CompanyId,finYearId,Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate),ContextUser.UserId,Session.SessionID);
            }
            catch (Exception ex)
            {
                responseOut.message = ActionMessage.ApplicationException;
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PLStatementReport(int reportLevel,string fromDate,string toDate,string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            ProfitLossStatementPrintBL plPrintBL = new ProfitLossStatementPrintBL();
            CompanyBL companyBL = new CompanyBL();
            string companyName = companyBL.GetCompanyDetail(ContextUser.CompanyId).CompanyName;


            string path = Path.Combine(Server.MapPath("~/RDLC"), "PLStatementPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("PLStatementPrint");
            }

            DataTable dt = new DataTable();
            dt = plPrintBL.GetProfitLossStatementDataTable(ContextUser.UserId,Session.SessionID);

            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            lr.DataSources.Add(rd);
            


            
            ReportParameter rp1 = new ReportParameter("CompanyName", companyName);
            ReportParameter rp2 = new ReportParameter("FromDate", fromDate);
            ReportParameter rp3 = new ReportParameter("ToDate", toDate);
            ReportParameter rp4 = new ReportParameter("ReportLevel",Convert.ToString(reportLevel));


            lr.SetParameters(rp1);
            lr.SetParameters(rp2);
            lr.SetParameters(rp3);
            lr.SetParameters(rp4);

            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo = "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.50in</MarginTop>" +
            "  <MarginLeft>.2in</MarginLeft>" +
            "  <MarginRight>.2in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
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



        #endregion

        #region Balance Sheet Print
        [ValidateRequest(true, UserInterfaceHelper.BalanceSheetPrint, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult BalanceSheetPrint()
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

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.BalanceSheetPrint, (int)AccessMode.ViewAccess, (int)RequestMode.Ajax)]
        public ActionResult BalanceSheetGenerate(string asOnDate)
        {
            ResponseOut responseOut = new ResponseOut();
            ProfitLossStatementPrintBL plPrintBL = new ProfitLossStatementPrintBL();
            try
            {

                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                responseOut = plPrintBL.GenerateBalanceSheet(ContextUser.CompanyId, finYearId, Convert.ToDateTime(asOnDate),  ContextUser.UserId, Session.SessionID);
            }
            catch (Exception ex)
            {
                responseOut.message = ActionMessage.ApplicationException;
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BalanceSheetReport(int reportLevel, string fromDate, string toDate, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            ProfitLossStatementPrintBL plPrintBL = new ProfitLossStatementPrintBL();
            CompanyBL companyBL = new CompanyBL();
            string companyName = companyBL.GetCompanyDetail(ContextUser.CompanyId).CompanyName;

            int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
            
            decimal netProfitLoss = plPrintBL.GetNetProfitLoss(ContextUser.CompanyId, finYearId, Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate), ContextUser.UserId, Session.SessionID);

            string path = Path.Combine(Server.MapPath("~/RDLC"), "BalanceSheetPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("BalanceSheetPrint");
            }

            DataTable dt = new DataTable();
            dt = plPrintBL.GetBalanceSheetDataTable(ContextUser.UserId, Session.SessionID);

            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            lr.DataSources.Add(rd);




            ReportParameter rp1 = new ReportParameter("CompanyName", companyName);
            ReportParameter rp2 = new ReportParameter("FromDate", fromDate);
            ReportParameter rp3 = new ReportParameter("ToDate", toDate);
            ReportParameter rp4 = new ReportParameter("ReportLevel", Convert.ToString(reportLevel));
            ReportParameter rp5 = new ReportParameter("NetProfitLoss", Convert.ToString(netProfitLoss));


            lr.SetParameters(rp1);
            lr.SetParameters(rp2);
            lr.SetParameters(rp3);
            lr.SetParameters(rp4);
            lr.SetParameters(rp5);
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo = "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.50in</MarginTop>" +
            "  <MarginLeft>.2in</MarginLeft>" +
            "  <MarginRight>.2in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
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



        #endregion


    }
}
