﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices;
using System.IO;


namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class QuotationRegisterController : BaseController
    {
        //
        // GET: /QuotationRegister/
         

        [ValidateRequest(true, UserInterfaceHelper.Add_QuotationRegister, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListQuotationRegister()
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
        public PartialViewResult GetQuotationRegisterList( int customerId = 0, int stateId = 0, string fromDate = "", string toDate = "", int createdBy = 0 ,string sortBy="", string sortOrder = "")
        {
            List<QuotationViewModel> quotations = new List<QuotationViewModel>();
            QuotationRegisterBL quotationregisterBL = new QuotationRegisterBL();
            try
             {
                
                quotations = quotationregisterBL.GetQuotationRegisterList(customerId, stateId, fromDate, toDate, ContextUser.CompanyId, createdBy, sortBy, sortOrder);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(quotations);
        }

        public ActionResult GenerateQuotationRegisterReports(int customerId = 0, int stateId = 0, string fromDate = "", string toDate = "", int createdBy = 0, string sortBy = "", string sortOrder = "", string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();            
            QuotationRegisterBL quotationRegisterBl = new QuotationRegisterBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "QuotationRegisterReports.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            ReportDataSource rd = new ReportDataSource("QuotationRegisterReportsDataSet", quotationRegisterBl.GenerateQuotationRegisterReports(customerId, stateId, fromDate, toDate, ContextUser.CompanyId, createdBy, sortBy, sortOrder));
            lr.DataSources.Add(rd);

            ReportParameter rp3 = new ReportParameter("fromdate", fromDate);
            ReportParameter rp4 = new ReportParameter("todate", toDate);
            lr.SetParameters(rp3);
            lr.SetParameters(rp4);

            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

             "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>17.8in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.50in</MarginTop>" +
            "  <MarginLeft>.1in</MarginLeft>" +
            "  <MarginRight>.1in</MarginRight>" +
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



    }
}
