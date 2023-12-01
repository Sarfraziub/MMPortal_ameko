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
using System.Text;

namespace Portal.Controllers
{
    public class SiteConsumptionController : BaseController
    {
        //
        // GET: /SiteConsumption/
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SiteConsumption, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSiteConsumption(int consumptionSiteId = 0, int accessMode = 3)
        {

            try
            {
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                if (consumptionSiteId != 0)
                {
                    ViewData["consumptionSiteId"] = consumptionSiteId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["consumptionSiteId"] = 0;
                    ViewData["accessMode"] = 0;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SiteConsumption, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListSiteConsumption()
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
        public PartialViewResult GetSiteConsumptionProductList(List<SiteConsumptionProductViewModel> siteConsumptionProducts, int siteConsumptionId)
        {
            SiteConsumptionBL siteConsumptionBL = new SiteConsumptionBL();
            try
            {
                if (siteConsumptionProducts == null)
                {
                    siteConsumptionProducts = siteConsumptionBL.GetSiteConsumptionProductList(siteConsumptionId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(siteConsumptionProducts);
        }

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SiteConsumption, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditSiteConsumption(SiteConsumptionViewModel siteConsumptionViewModel, List<SiteConsumptionProductViewModel> siteConsumptionProductsList)
        {
            ResponseOut responseOut = new ResponseOut();
            SiteConsumptionBL packingListBL = new SiteConsumptionBL();
            try
            {
                if (siteConsumptionViewModel != null)
                {
                    siteConsumptionViewModel.CreatedBy = ContextUser.UserId;
                    siteConsumptionViewModel.CompanyId = ContextUser.CompanyId;
                    siteConsumptionViewModel.ModifiedBy = ContextUser.UserId;
                    FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                    siteConsumptionViewModel.FinYearId = currentFinYear.FinYearId;
                    responseOut = packingListBL.AddEditSiteConsumption(siteConsumptionViewModel, siteConsumptionProductsList);

                }
                else
                {
                    responseOut.message = ActionMessage.ProbleminData;
                    responseOut.status = ActionStatus.Fail;
                    responseOut.trnId = 0;
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
        public PartialViewResult GetSiteConsumptionList(string siteConsumptionNo = "",  int companyBranchId = 0, string fromDate = "", string toDate = "", string consumptionStatus = "")
        {
            List<SiteConsumptionViewModel> siteConsumption = new List<SiteConsumptionViewModel>();
            SiteConsumptionBL siteConsumptionBL = new SiteConsumptionBL();
            try
            {
                siteConsumption = siteConsumptionBL.GetSiteConsumptionList(siteConsumptionNo,  companyBranchId, fromDate, toDate, ContextUser.CompanyId, consumptionStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(siteConsumption);
        }

        [HttpGet]
        public JsonResult GetSiteConsumptionDetail(long siteConsumptionId)
        {
            SiteConsumptionBL siteConsumptionBL = new SiteConsumptionBL();
            SiteConsumptionViewModel siteConsumptionViewModel = new SiteConsumptionViewModel();
            try
            {
                siteConsumptionViewModel = siteConsumptionBL.GetSiteConsumptionDetail(siteConsumptionId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(siteConsumptionViewModel, JsonRequestBehavior.AllowGet);
        }

        [ValidateRequest(true, UserInterfaceHelper.ListProductSiteConsumptionReport, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListProductSiteConsumptionReport()
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

        public ActionResult Report(int customerId,int customerBranchId, int productId = 0, int companyBranchId=0, string fromDate="",string toDate="", string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            SiteConsumptionBL siteConsumptionBL = new SiteConsumptionBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "ProductSiteConsumptionReports.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("PrintStockLedger");
            }
            
            DataTable dt = new DataTable();
            dt = siteConsumptionBL.GetProductSiteConsumptionListReport(customerId, customerBranchId, productId, companyBranchId, fromDate, toDate, ContextUser.CompanyId);
            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            lr.DataSources.Add(rd);
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>9.5in</PageWidth>" +
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

        public ActionResult SiteConsumptionDetailReport(int customerId, int customerBranchId, int productId = 0, int companyBranchId = 0, string fromDate = "", string toDate = "", string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            SiteConsumptionBL siteConsumptionBL = new SiteConsumptionBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "ProductSiteConsumptionDetailReports.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("PrintStockLedger");
            }

            DataTable dt = new DataTable();
            dt = siteConsumptionBL.GetProductSiteConsumptionListReport(customerId, customerBranchId, productId, companyBranchId, fromDate, toDate, ContextUser.CompanyId);
            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            lr.DataSources.Add(rd);
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>10.7in</PageWidth>" +
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
