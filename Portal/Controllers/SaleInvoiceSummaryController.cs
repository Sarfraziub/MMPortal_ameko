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
using Microsoft.ReportingServices;
using System.IO;
using System.Data;
namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class SaleInvoiceSummaryController : BaseController
    {
        #region SaleInvoiceSummary

        //
        // GET: /SaleInvoiceRegister/ 


        [ValidateRequest(true, UserInterfaceHelper.Add_SaleInvoiceSummary, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListSaleInvoiceSummary()
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
        public PartialViewResult GetSaleInvoiceSummaryList(int customerId, int userId, int stateId,  string fromDate, string toDate)
        {
            List<SaleSummaryRegisterViewModel> saleInvoices = new List<SaleSummaryRegisterViewModel>();
            SaleInvoiceRegisterBL saleInvoiceRegisterBL = new SaleInvoiceRegisterBL();
            try
            {

                saleInvoices = saleInvoiceRegisterBL.GetSaleSummaryRegister(customerId,userId, stateId ,ContextUser.CompanyId ,Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate));
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleInvoices);
        }

        public ActionResult GenerateSaleInvoiceSummaryReports(int customerId, int userId, int stateId, string fromDate, string toDate, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            SaleInvoiceRegisterBL saleInvoiceRegisterBL = new SaleInvoiceRegisterBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "SaleInvoiceSummaryReports.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            ReportDataSource rd = new ReportDataSource("SaleInvoiceSummaryReportsDataSet", saleInvoiceRegisterBL.GenerateSaleInvoiceSummaryReports(customerId, userId, stateId, ContextUser.CompanyId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate)));
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
            "  <PageWidth>14.8in</PageWidth>" +
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

        #endregion
    }
}
