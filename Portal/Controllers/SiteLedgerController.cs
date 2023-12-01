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
    public class SiteLedgerController : BaseController
    {
        //
        // GET: /Product/
        //
        // GET: /User/
        
        [ValidateRequest(true, UserInterfaceHelper.Print_Site_Ledger, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult SitePrintStockLedger(int productId = 0, int accessMode = 3)
        {

            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;

                if (productId != 0)
                {
                    ViewData["productId"] = productId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["productId"] = 0;
                    ViewData["accessMode"] = 0;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        public ActionResult Report(int productTypeId=0,string assemblyType="",int productMainGroupId=0, int productSubGroupId=0,long productId=0, int customerId=0, int customerBranchId=0,string fromDate="", string toDate="", string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            SiteLedgerBL siteLedgerBL = new SiteLedgerBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "SiteLedger.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("SitePrintStockLedger");
            }

            DataTable dt = new DataTable();
            dt = siteLedgerBL.GetSiteLedgerDataTable(productTypeId, assemblyType, productMainGroupId, productSubGroupId, productId, Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate), ContextUser.CompanyId,customerId,customerBranchId);

            
            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            lr.DataSources.Add(rd);
            
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>11.5in</PageWidth>" +
            "  <PageHeight>8.5in</PageHeight>" +
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
        
    }
}
