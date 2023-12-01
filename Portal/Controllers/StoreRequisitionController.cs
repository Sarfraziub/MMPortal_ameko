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
    public class StoreRequisitionController :BaseController
    {
        //
        // GET: /StoreRequisition/

        #region StoreRequisition
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_StoreRequisition, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)] // By Hari Sir
        //[ValidateRequest(true, UserInterfaceHelper.Add_Edit_StoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditStoreRequisition(int storeRequisitionId = 0, int accessMode = 3)
        {

            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                ViewData["UserFullName"] = ContextUser.UserName;
                ViewData["UserId"] = ContextUser.UserId;
                if (storeRequisitionId != 0)
                {
                    ViewData["storeRequisitionId"] = storeRequisitionId;
                    ViewData["accessMode"] = accessMode;
                    
                }
                else
                {
                    ViewData["storeRequisitionId"] = 0;
                    ViewData["accessMode"] = 0;
                    
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_StoreRequisition, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)] // By Hari Sir For access permission change(29-12-2017)
      //[ValidateRequest(true, UserInterfaceHelper.Add_Edit_StoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditStoreRequisition(StoreRequisitionViewModel storeRequisitionViewModel, List<StoreRequisitionProductDetailViewModel> storeRequisitionProducts)
        {
            ResponseOut responseOut = new ResponseOut();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();

            try
            {
                if (storeRequisitionViewModel != null)
                {
                    storeRequisitionViewModel.CreatedBy = ContextUser.UserId;
                    storeRequisitionViewModel.CompanyId = ContextUser.CompanyId;
                    storeRequisitionViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = storeRequisitionBL.AddEditStoreRequisition(storeRequisitionViewModel, storeRequisitionProducts);

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

        [HttpPost]
        public PartialViewResult GetStoreRequisitionProductList(List<StoreRequisitionProductDetailViewModel> storeRequisitionProducts, long requisitionId)
            {
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                if (storeRequisitionProducts == null)
                {
                    storeRequisitionProducts = storeRequisitionBL.GetStoreRequisitionProducts(requisitionId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(storeRequisitionProducts);
        }


        [HttpGet]
        public JsonResult GetStoreRequisitionDetail(long requisitionId)
        {
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            StoreRequisitionViewModel storeRequisition = new StoreRequisitionViewModel();
            try
            {
                storeRequisition = storeRequisitionBL.GetStoreRequisitionDetail(requisitionId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(storeRequisition, JsonRequestBehavior.AllowGet);
        }

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_StoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListStoreRequisition(string approvalStatus="")
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                if (approvalStatus != "")
                {
                    ViewData["approvalStatus"] = approvalStatus;
                }
                else {
                    ViewData["approvalStatus"] = 0;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [HttpGet]
        public PartialViewResult GetRequisitionWorkOrderList(string workOrderNo, int companyBranchId, string fromDate, string toDate, string displayType = "", string approvalStatus = "")
        {
            List<WorkOrderViewModel> workOrders = new List<WorkOrderViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                workOrders = storeRequisitionBL.GetRequisitionWorkOrderList(workOrderNo, companyBranchId, fromDate, toDate, ContextUser.CompanyId, displayType,approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(workOrders);
        }

        [HttpGet]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_StoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public PartialViewResult GetStoreRequisitionList(string requisitionNo = "",string workOrderNo="",string requisitionType="", string customerName = "", int companyBranchId = 0, string fromDate = "", string toDate = "", string displayType = "", string requisitionStatus = "", string approvalStatus = "")
        {
            List<StoreRequisitionViewModel> requisitions = new List<StoreRequisitionViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();            
            try
            {
                requisitions = storeRequisitionBL.GetStoreRequisitionList(requisitionNo, workOrderNo, requisitionType, customerName, companyBranchId,Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate), ContextUser.CompanyId, displayType, requisitionStatus, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(requisitions);
        }

        [HttpPost]
        public PartialViewResult GetWorkOrderBOMProductList(List<StoreRequisitionProductDetailViewModel> storeRequisitionProducts, long workOrderId)
        {
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                if (storeRequisitionProducts == null)
                {
                    storeRequisitionProducts = storeRequisitionBL.GetWorkOrderBOMProductList(workOrderId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(storeRequisitionProducts);
        }
        public JsonResult GetBranchLocationList(int companyBranchID)
        {
            LocationBL locationBL = new LocationBL();
            List<LocationViewModel> locationViewModel = new List<LocationViewModel>();
            try
            {
                locationViewModel = locationBL.GetFromLocationList(companyBranchID);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(locationViewModel, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Report(long requisitionId, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();           
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "StoreRequisitionReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            DataTable dt = new DataTable();
            dt = storeRequisitionBL.GetStoreRequisitionDataTable(requisitionId);
            ReportDataSource rd = new ReportDataSource("StoreRequisitionDetailDataSet", dt);
            ReportDataSource rdProduct = new ReportDataSource("StoreRequisitionProductsDataSet", storeRequisitionBL.GetStoreRequisitionProductListDataTable(requisitionId));
            lr.DataSources.Add(rd);
            lr.DataSources.Add(rdProduct);
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + reportType + "</OutputFormat>" +
                        "  <PageWidth>8.5in</PageWidth>" +
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

        public ActionResult LogStoreRequisitionReport(long requisitionId, string requisitionNo = "", string fromDate = "", string toDate = "", string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "LogStoreRequisitionReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            DataTable dtLogReportNew = new DataTable();
            dtLogReportNew = storeRequisitionBL.GetLogReportNewStoreRequisitionDataTable(requisitionId);
            
            ReportDataSource rdLogReportNew = new ReportDataSource("MasterLogReportDetails", dtLogReportNew);
            ReportDataSource rdLogReportMaster = new ReportDataSource("MasterLogReport", storeRequisitionBL.GetLogReportMasterStoreRequisitionProductListDataTable(requisitionId,requisitionNo,fromDate,toDate));
            //ReportDataSource rdLogReportNewProduct = new ReportDataSource("LogReportNewProductDataSet", storeRequisitionBL.GetLogReportNewStoreRequisitionProductListDataTable(requisitionId, requisitionNo, fromDate, toDate));
            //ReportDataSource rdLogReportOldProduct = new ReportDataSource("LogReportOldProductDataset", storeRequisitionBL.GetLogReportOldStoreRequisitionProductListDataTable(requisitionId));
            lr.DataSources.Add(rdLogReportNew);
            //lr.DataSources.Add(rdLogReportOld);
            lr.DataSources.Add(rdLogReportMaster);
            //lr.DataSources.Add(rdLogReportOldProduct);
            //lr.DataSources.Add(rdLogReportNewProduct);
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo = "<DeviceInfo>" +
                       "  <OutputFormat>" + reportType + "</OutputFormat>" +
                       "  <PageWidth>11in</PageWidth>" +
                       "  <PageHeight>9in</PageHeight>" +
                       "  <MarginTop>0.50in</MarginTop>" +
                       "  <MarginLeft>.15in</MarginLeft>" +
                       "  <MarginRight>.15in</MarginRight>" +
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


        [ValidateRequest(true, UserInterfaceHelper.LogReportStoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult LogReportStoreRequisition()
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

        //Store Requisition Escalation List
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_StoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListStoreRequisitionEscalation()
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

        //Get Store Requisition Escalation List
        [HttpGet]
        public PartialViewResult GetStoreRequisitionEscalationList()
        {
            List<StoreRequisitionEscalationViewModel> requisitions = new List<StoreRequisitionEscalationViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
            try
            {
                requisitions = storeRequisitionBL.GetStoreRequisitionEscalationList(ContextUser.CompanyId,finYearId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(requisitions);
        }

        //Store Requisition list for MRN
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_PendingStoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListStoreRequisitionForMRN()
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
        public PartialViewResult GetStoreRequisitionListForMRN(string requisitionNo = "", string requisitionType = "",  string fromDate = "", string toDate = "", string displayType = "", string approvalStatus = "0")
        {
            List<StoreRequisitionViewModel> requisitions = new List<StoreRequisitionViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                requisitions = storeRequisitionBL.GetStoreRequisitionListForMRN(requisitionNo, requisitionType, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), ContextUser.CompanyId, displayType, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(requisitions);
        }


        //Pending Store Requisition for Approval Report
        [ValidateRequest(true, UserInterfaceHelper.ListPendingStoreRequisitionReport, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListPendingStoreRequisitionReport()
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

        public ActionResult GetPendingStoreRequisitionsReport(string requisitionNo = "", string requisitionType = "", string fromDate = "", string toDate = "", string displayType = "", string approvalStatus = "0", string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "PendingStoreRequisitionReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            DataTable dt = new DataTable();
            dt = storeRequisitionBL.GetPendingStoreRequisitionsReport(requisitionNo, requisitionType,Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate),ContextUser.CompanyId, displayType, approvalStatus);
            ReportDataSource rd = new ReportDataSource("PendingStoreRequisitionDataSet", dt);
            lr.DataSources.Add(rd);
            //ReportDataSource rdProduct = new ReportDataSource("StoreRequisitionProductsDataSet", storeRequisitionBL.GetStoreRequisitionProductListDataTable(requisitionId));
            //lr.DataSources.Add(rdProduct);
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


        //List Approved PO MRN Not Done Report
        [ValidateRequest(true, UserInterfaceHelper.ListApprovedPOMRNNotDone, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListApprovedPOMRNNotDone()
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

        public ActionResult GetApprovedPOsMRNNotDone(string fromDate = "", string toDate = "", int customerId = 0, int customerBranchId = 0, string displayType = "", string approvalStatus = "0", string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "ApprovedPOMRNNotDoneReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ListApprovedPOMRNNotDone");
            }
            DataTable dt = new DataTable();
            dt = storeRequisitionBL.GetApprovedPOsMRNNotDone(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), approvalStatus, ContextUser.CompanyId, customerId, customerBranchId, displayType);
            ReportDataSource rd = new ReportDataSource("ApprovedPOMRNNotDoneDataSet", dt);
            lr.DataSources.Add(rd);
            //ReportDataSource rdProduct = new ReportDataSource("StoreRequisitionProductsDataSet", storeRequisitionBL.GetStoreRequisitionProductListDataTable(requisitionId));
            //lr.DataSources.Add(rdProduct);
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + reportType + "</OutputFormat>" +
                        "  <PageWidth>14in</PageWidth>" +
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


        //List Approved PO CustomerSite MRN Not Done Report
        [ValidateRequest(true, UserInterfaceHelper.ListApprovedPOCustomerSiteMRNNotDone, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListApprovedPOCustomerSiteMRNNotDone()
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

        public ActionResult GetApprovedPOsCustomerSiteMRNNotDone(string fromDate = "", string toDate = "", int customerId = 0, int customerBranchId = 0, string displayType = "", string approvalStatus = "0", string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "ApprovedPOCustomerSiteMRNNotDoneReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ListApprovedPOCustomerSiteMRNNotDone");
            }
            DataTable dt = new DataTable();
            dt = storeRequisitionBL.GetApprovedPOsCustomerSiteMRNNotDone(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), approvalStatus, ContextUser.CompanyId, customerId, customerBranchId, displayType);
            ReportDataSource rd = new ReportDataSource("ApprovedPOCustomerSiteMRNNotDoneDataSet", dt);
            lr.DataSources.Add(rd);
            //ReportDataSource rdProduct = new ReportDataSource("StoreRequisitionProductsDataSet", storeRequisitionBL.GetStoreRequisitionProductListDataTable(requisitionId));
            //lr.DataSources.Add(rdProduct);
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + reportType + "</OutputFormat>" +
                        "  <PageWidth>14in</PageWidth>" +
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


        //Approved Store Requisitions Without PO
        [ValidateRequest(true, UserInterfaceHelper.ListApprovedStoreRequisitionsWithoutPO, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListApprovedStoreRequisitionsWithoutPO()
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

        public ActionResult GetApprovedStoreRequisitionsWithoutPO(string requisitionNo="",string fromDate = "", string toDate = "", int customerId = 0, int customerBranchId = 0, string displayType = "", string approvalStatus = "0", string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "ApprovedStoreRequisitionsWithoutPOReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ListApprovedStoreRequisitionsWithoutPO");
            }
            DataTable dt = new DataTable();
            dt = storeRequisitionBL.GetApprovedStoreRequisitionsWithoutPO(requisitionNo,Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), approvalStatus, ContextUser.CompanyId, customerId, customerBranchId, displayType);
            ReportDataSource rd = new ReportDataSource("ApprovedStoreRequisitionsWithoutPODataSet", dt);
            lr.DataSources.Add(rd);
            //ReportDataSource rdProduct = new ReportDataSource("StoreRequisitionProductsDataSet", storeRequisitionBL.GetStoreRequisitionProductListDataTable(requisitionId));
            //lr.DataSources.Add(rdProduct);
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + reportType + "</OutputFormat>" +
                        "  <PageWidth>11in</PageWidth>" +
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


        //Pending Store Requisition list
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_PendingStoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListPendingStoreRequisition(string requisitionNo = "", int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["requisitionNo"] = requisitionNo;
                ViewData["accessMode"] = accessMode;
                ViewData["toDate"] = finYear.EndDate;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [HttpGet]
        public PartialViewResult GetPendingStoreRequisitionList(string requisitionNo = "")
        {
            List<PendingStoreRequisitionViewModel> requisitions = new List<PendingStoreRequisitionViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                requisitions = storeRequisitionBL.GetPendingStoreRequisitionList(requisitionNo);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(requisitions);
        }


        #endregion


        #region Approval Store Requisition


        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ApproveStoreRequisition, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        public ActionResult ApprovalStoreRequisition(int storeRequisitionId = 0, int accessMode = 3)
        {

            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                if (storeRequisitionId != 0)
                {

                    ViewData["storeRequisitionId"] = storeRequisitionId;
                    ViewData["accessMode"] = accessMode;
                    ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                }
                else
                {
                    ViewData["storeRequisitionId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ApproveStoreRequisition, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        public ActionResult ApprovalStoreRequisition(StoreRequisitionViewModel storeRequisitionViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();

            try
            {
                if (storeRequisitionViewModel != null)
                {
                    storeRequisitionViewModel.CompanyId = ContextUser.CompanyId;
                    storeRequisitionViewModel.ApprovedBy = ContextUser.UserId;
                    storeRequisitionViewModel.RejectedBy = ContextUser.UserId;
                    responseOut = storeRequisitionBL.ApproveRejectStoreRequisition(storeRequisitionViewModel);
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

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_ApproveStoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListApprovedStoreRequisition(string type="0", string reqType="0", string reqstatus = "0")
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["requisitionType"] = type;
                ViewData["POType"] = reqType;
                ViewData["requisitionStatus"] = reqstatus;

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [HttpPost]
        public PartialViewResult GetStoreRequisitionProductApprovalList(List<StoreRequisitionProductDetailViewModel> storeRequisitionProducts, long requisitionId)
        {
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                if (storeRequisitionProducts == null)
                {
                    storeRequisitionProducts = storeRequisitionBL.GetStoreRequisitionProductList(requisitionId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(storeRequisitionProducts);
        }

        [HttpGet]
        public JsonResult GetStoreRequisitionApprovalDetail(long requisitionId)
        {
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            StoreRequisitionViewModel storeRequisition = new StoreRequisitionViewModel();
            try
            {
                storeRequisition = storeRequisitionBL.GetStoreRequisitionDetail(requisitionId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(storeRequisition, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetStoreRequisitionApprovalList(string requisitionNo = "", string workOrderNo = "", string requisitionType = "", string customerName = "", int companyBranchId = 0, string fromDate = "", string toDate = "", string displayType = "", string approvalStatus = "")
        {
            List<StoreRequisitionViewModel> requisitions = new List<StoreRequisitionViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();

            try
            {
                requisitions = storeRequisitionBL.GetStoreRequisitionApprovelList(requisitionNo, workOrderNo, requisitionType, customerName, companyBranchId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), ContextUser.CompanyId, displayType, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(requisitions);
        }

        #endregion

        #region Update StoreRequisition


        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_StoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ApprovalStoreRequisitionUpdate(int storeRequisitionId = 0, int accessMode = 3)
        {

            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                if (storeRequisitionId != 0)
                {

                    ViewData["storeRequisitionId"] = storeRequisitionId;
                    ViewData["accessMode"] = accessMode;
                    ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                }
                else
                {
                    ViewData["storeRequisitionId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_StoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ApprovalStoreRequisitionUpdate(StoreRequisitionViewModel storeRequisitionViewModel, List<StoreRequisitionProductDetailViewModel> storeRequisitionProducts)
        {
            ResponseOut responseOut = new ResponseOut();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();

            try
            {
                if (storeRequisitionViewModel != null)
                {
                    storeRequisitionViewModel.CompanyId = ContextUser.CompanyId;
                    storeRequisitionViewModel.ApprovedBy = ContextUser.UserId;
                    storeRequisitionViewModel.RejectedBy = ContextUser.UserId;
                    responseOut = storeRequisitionBL.ApproveUpdateStoreRequisition(storeRequisitionViewModel, storeRequisitionProducts);
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


        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_StoreRequisition, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListApprovedUpdateStoreRequisition()
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
        public PartialViewResult GetStoreRequisitionApprovalUpdateList(string requisitionNo = "", string workOrderNo = "", string requisitionType = "", string customerName = "", int companyBranchId = 0, string fromDate = "", string toDate = "", string displayType = "", string approvalStatus = "")
        {
            List<StoreRequisitionViewModel> requisitions = new List<StoreRequisitionViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                requisitions = storeRequisitionBL.GetStoreRequisitionApprovalUpdateList(requisitionNo, workOrderNo, requisitionType, customerName, companyBranchId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), ContextUser.CompanyId, displayType, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(requisitions);
        }


        [HttpGet]
        public JsonResult GetStoreRequisitionApprovalUpdateDetail(long requisitionId)
        {
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            StoreRequisitionViewModel storeRequisition = new StoreRequisitionViewModel();
            try
            {
                storeRequisition = storeRequisitionBL.GetStoreRequisitionDetail(requisitionId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(storeRequisition, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult GetStoreRequisitionUpdateProductList(List<StoreRequisitionProductDetailViewModel> storeRequisitionProducts, long requisitionId)
        {
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                if (storeRequisitionProducts == null)
                {
                    storeRequisitionProducts = storeRequisitionBL.GetStoreRequisitionUpdateProductUpdateList(requisitionId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(storeRequisitionProducts);
        }
        #endregion

    }
}
