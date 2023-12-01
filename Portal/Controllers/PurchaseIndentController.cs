using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using System.Reflection;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Data;

namespace Portal.Controllers
{
    public class PurchaseIndentController : BaseController
    {
        //
        // GET: /StoreRequisition/

        #region PurchaseIndent
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_PurchaseIndent, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditPurchaseIndent(int purchaseIndentId = 0, int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                if (purchaseIndentId != 0)
                {

                    ViewData["purchaseIndentId"] = purchaseIndentId;
                    ViewData["accessMode"] = accessMode;
                    ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                }
                else
                {
                    ViewData["purchaseIndentId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_PurchaseIndent, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditPurchaseIndent(PurchaseIndentViewModel purchaseIndentViewModel, List<PurchaseIndentProductDetailViewModel> purchaseIndentProducts)
        {
            ResponseOut responseOut = new ResponseOut();
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();

            try
            {
                if (purchaseIndentViewModel != null)
                {
                    purchaseIndentViewModel.CreatedBy = ContextUser.UserId;
                    purchaseIndentViewModel.CompanyId = ContextUser.CompanyId;
                    purchaseIndentViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = purchaseIndentBL.AddEditPurchaseIndent(purchaseIndentViewModel, purchaseIndentProducts);

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
        public PartialViewResult GetPurchaseIndentProductList(List<PurchaseIndentProductDetailViewModel> purchaseIndentProducts, long indentId)
        {
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();
            try
            {
                if (purchaseIndentProducts == null)
                {
                    purchaseIndentProducts = purchaseIndentBL.GetPurchaseIndentProductList(indentId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(purchaseIndentProducts);
        }

        [HttpGet]
        public JsonResult GetPurchaseIndentDetail(long indentId)
        {
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();
            PurchaseIndentViewModel purchaseIndent = new PurchaseIndentViewModel();
            try
            {
                purchaseIndent = purchaseIndentBL.GetPurchaseIndentDetail(indentId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(purchaseIndent, JsonRequestBehavior.AllowGet);
        }

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_PurchaseIndent, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListPurchaseIndent()
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
        public PartialViewResult GetPurchaseIndentList(string indentNo = "", string indentType = "", string customerName = "", int companyBranchId = 0, string fromDate = "", string toDate = "", string displayType = "", string approvalStatus = "0")
        {
            List<PurchaseIndentViewModel> indents = new List<PurchaseIndentViewModel>();
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();

            try
            {
                indents = purchaseIndentBL.GetPurchaseIndentList(indentNo, indentType, customerName, companyBranchId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), ContextUser.CompanyId, displayType, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(indents);
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

        public ActionResult Report(long indentId, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "PurchaseIndentReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            DataTable dt = new DataTable();
            dt = purchaseIndentBL.GetPurchaseIndentDataTable(indentId);
            ReportDataSource rd = new ReportDataSource("PurchaseIndentDetailDataSet", dt);
            ReportDataSource rdProduct = new ReportDataSource("PurchaseIndentProductaDataSet", purchaseIndentBL.GetPurchaseIndentProductsDataTable(indentId));
            lr.DataSources.Add(rd);
            lr.DataSources.Add(rdProduct);
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + reportType + "</OutputFormat>" +
                        "  <PageWidth>8.5in</PageWidth>" +
                        "  <PageHeight>9in</PageHeight>" +
                        "  <MarginTop>0.50in</MarginTop>" +
                        "  <MarginLeft>.15in</MarginLeft>" +
                        "  <MarginRight>.05in</MarginRight>" +
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

        //Purchase Indent Escalation List
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_PurchaseIndent, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListPurchaseIndentEscalation()
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

        //Get Purchase Indent Escalation List
        [HttpGet]
        public PartialViewResult GetPurchaseIndentEscalationList()
        {
            List<PurchaseIndentEscalationViewModel> escalateIndents = new List<PurchaseIndentEscalationViewModel>();
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();
            int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
            try
            {
                escalateIndents = purchaseIndentBL.GetPurchaseIndentEscalationList(ContextUser.CompanyId, finYearId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(escalateIndents);
        }

        //Purchase Indent For Quoatation Escalation List
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_PurchaseIndent, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListPurchaseIndentForQuotationEscalation()
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

        //Get Purchase Indent For Quoatation Escalation List
        [HttpGet]
        public PartialViewResult GetPurchaseIndentForQuotationEscalationList()
        {
            List<PurchaseIndentEscalationViewModel> escalateIndents = new List<PurchaseIndentEscalationViewModel>();
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();
            int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
            try
            {
                escalateIndents = purchaseIndentBL.GetPurchaseIndentForQuotationEscalationList(ContextUser.CompanyId, finYearId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(escalateIndents);
        }

        #endregion


        #region Approval Purchase Indent


        [ValidateRequest(true, UserInterfaceHelper.ListApprovedPurchaseIndent, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        public ActionResult ApprovalPurchaseIndent(int purchaseIndentId = 0, int accessMode = 3)
        {

            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                if (purchaseIndentId != 0)
                {

                    ViewData["purchaseIndentId"] = purchaseIndentId;
                    ViewData["accessMode"] = accessMode;
                    ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                }
                else
                {
                    ViewData["purchaseIndentId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.ListApprovedPurchaseIndent, (int)AccessMode.AddAccess, (int)RequestMode.GetPost)]
        public ActionResult ApprovalPurchaseIndent(PurchaseIndentViewModel purchaseIndentViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();

            try
            {
                if (purchaseIndentViewModel != null)
                {
                    purchaseIndentViewModel.CompanyId = ContextUser.CompanyId;
                    purchaseIndentViewModel.ApprovedBy = ContextUser.UserId;
                    purchaseIndentViewModel.RejectedBy = ContextUser.UserId;
                    responseOut = purchaseIndentBL.ApproveRejectPurchaseIndent(purchaseIndentViewModel);
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

        [ValidateRequest(true, UserInterfaceHelper.ListApprovedPurchaseIndent, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListApprovedPurchaseIndent()
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
        public PartialViewResult GetPurchaseIndentProductApprovalList(List<PurchaseIndentProductDetailViewModel> purchaseIndentProducts, long indentId)
        {
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();
            try
            {
                if (purchaseIndentProducts == null)
                {
                    purchaseIndentProducts = purchaseIndentBL.GetPurchaseIndentProductList(indentId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(purchaseIndentProducts);
        }

        [HttpGet]
        public JsonResult GetPurchaseIndentApprovalDetail(long indentId)
        {
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();
            PurchaseIndentViewModel purchaseIndent = new PurchaseIndentViewModel();
            try
            {
                purchaseIndent = purchaseIndentBL.GetPurchaseIndentDetail(indentId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(purchaseIndent, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetPurchaseIndentApprovalList(string indentNo = "", string indentType = "0", string customerName = "", int companyBranchId = 0, string fromDate = "", string toDate = "", string displayType = "", string approvalStatus = "0")
        {
            List<PurchaseIndentViewModel> indents = new List<PurchaseIndentViewModel>();
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();

            try
            {
                indents = purchaseIndentBL.GetPurchaseIndentApprovelList(indentNo, indentType, customerName, companyBranchId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), ContextUser.CompanyId, displayType, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(indents);
        }

        [HttpPost]
        public PartialViewResult GetStoreRequisitionProductList(List<StoreRequisitionProductDetailViewModel> storeRequisitionProducts, long requisitionId)
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
        public PartialViewResult GetStoreRequisitionList(string requisitionNo = "", string workOrderNo = "", string requisitionType = "", int companyBranchId = 0, string fromDate = "", string toDate = "", string displayType = "", string approvalStatus = "")
        {
            List<StoreRequisitionViewModel> requisitions = new List<StoreRequisitionViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            PurchaseIndentBL purchaseIndentBL = new PurchaseIndentBL();
            try
            {
                requisitions = purchaseIndentBL.GetStoreRequisitionListForPurchaseIndent(requisitionNo, workOrderNo, requisitionType, companyBranchId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), ContextUser.CompanyId, displayType, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(requisitions);
        }


        #endregion

    }
}
